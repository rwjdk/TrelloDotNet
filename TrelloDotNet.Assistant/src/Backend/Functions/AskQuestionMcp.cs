using Backend.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using SimpleRag;
using SimpleRag.VectorStorage.Models;

namespace Backend.Functions
{
    public class AskQuestionMcp(Search search)
    {
        [Function("AskQuestion")]
        public async Task<IActionResult> Run(
            [McpToolTrigger(ToolIds.ToolName, ToolIds.ToolDescription)]
            ToolInvocationContext context,
            [McpToolProperty(ToolIds.ParameterQuestion, ToolIds.StringType, ToolIds.ParameterQuestionDescription)]
            string searchQuery)
        {
            SearchResult docsSearchResult = await search.SearchAsync(new SearchOptions
            {
                SearchQuery = searchQuery,
                NumberOfRecordsBack = 10,
                Filter = entity => entity.SourceCollectionId == VectorStoreIds.CollectionId && entity.SourceId == VectorStoreIds.SourceIdMarkdownInCode
            });

            SearchResult codeSearchResult = await search.SearchAsync(new SearchOptions
            {
                SearchQuery = searchQuery,
                NumberOfRecordsBack = 20,
                Filter = entity => entity.SourceCollectionId == VectorStoreIds.CollectionId && entity.SourceId == VectorStoreIds.SourceIdCode
            });

            SearchResult result = new SearchResult
            {
                Entities = docsSearchResult.Entities.Union(codeSearchResult.Entities).ToArray()
            };

            return new OkObjectResult(result.GetAsStringResult(citationBuilder: entity =>
            {
                return entity.SourceKind switch
                {
                    SimpleRag.DataSources.CSharp.CSharpDataSourceCommand.SourceKind => "https://github.com/rwjdk/TrelloDotNet/blob/main/src" + entity.SourcePath,
                    SimpleRag.DataSources.Markdown.MarkdownDataSourceCommand.SourceKind => "https://github.com/rwjdk/TrelloDotNet/wiki/" + Path.GetFileNameWithoutExtension(entity.SourcePath.Replace("TrelloDotNet.wiki", "")),
                    _ => "https://github.com/rwjdk/TrelloDotNet"
                };
            }));
        }
    }
}