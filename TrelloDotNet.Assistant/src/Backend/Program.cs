using Azure;
using Azure.Search.Documents.Indexes;
using Backend.Constants;
using Backend.Models;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel.Connectors.AzureAISearch;
using Microsoft.SemanticKernel.Connectors.SqlServer;
using SimpleRag.Extensions;
using SimpleRag.VectorStorage;

#pragma warning disable SKEXP0010
var builder = FunctionsApplication.CreateBuilder(args);
builder.ConfigureFunctionsWebApplication();
builder.EnableMcpToolMetadata();
builder.ConfigureMcpTool(ToolIds.ToolName).WithProperty(ToolIds.ParameterQuestion, ToolIds.StringType, ToolIds.ParameterQuestionDescription);

//Configuration
string endpoint = builder.Configuration[SecretIds.AiEndpoint]!;
string key = builder.Configuration[SecretIds.AiKey]!;
string embeddingDeploymentName = builder.Configuration[SecretIds.AiEmbeddingDeploymentName]!;
string sqlServerConnectionString = builder.Configuration[SecretIds.SqlServerConnectionString]!;
string azureAiSearchEndpoint = builder.Configuration[SecretIds.AzureAiSearchEndpoint]!;
string azureAiSearchKey = builder.Configuration[SecretIds.AzureAiSearchKey]!;
string githubToken = builder.Configuration[SecretIds.GitHubToken]!;
builder.Services.AddAzureOpenAIEmbeddingGenerator(embeddingDeploymentName, endpoint, key);

VectorStoreConfiguration vectorStoreConfiguration = new(VectorStoreIds.VectorStoreName, VectorStoreIds.MaxRecords);

switch (VectorStoreIds.VectorStoreToUse)
{
    // ReSharper disable once UnreachableSwitchCaseDueToIntegerAnalysis
    case VectorStoreToUse.SqlServer:
        builder.Services.AddSimpleRagWithGithubIntegration(vectorStoreConfiguration, options => new SqlServerVectorStore(sqlServerConnectionString, new SqlServerVectorStoreOptions
        {
            EmbeddingGenerator = options.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>()
        }), githubToken);

        break;
    // ReSharper disable once UnreachableSwitchCaseDueToIntegerAnalysis
    case VectorStoreToUse.AzureAiSearch:
        builder.Services.AddSimpleRagWithGithubIntegration(vectorStoreConfiguration, options =>
        {
            SearchIndexClient searchIndexClient = new(new Uri(azureAiSearchEndpoint), new AzureKeyCredential(azureAiSearchKey));
            return new AzureAISearchVectorStore(searchIndexClient, new AzureAISearchVectorStoreOptions
            {
                EmbeddingGenerator = options.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>()
            });
        }, githubToken);

        break;
    default:
        throw new ArgumentOutOfRangeException();
}

builder.Build().Run();