using Backend.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using SimpleRag;
using SimpleRag.DataSources.CSharp.Models;
using SimpleRag.DataSources.Markdown.Models;

namespace Backend.Functions;

public class SyncRepo(Ingestion ingestion, IConfiguration configuration)
{
    [Function("SyncRepo")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req, CancellationToken cancellationToken)
    {
        if (req.Headers["x-api-key"] != configuration[SecretIds.SyncAuthKey])
        {
            return new UnauthorizedResult();
        }

        const string gitHubOwner = "rwjdk";
        const string gitHubRepo = "TrelloDotNet";

        try
        {
            await ingestion.IngestAsync(
            [
                new CSharpDataSourceGitHub
                {
                    CollectionId = VectorStoreIds.CollectionId,
                    Id = VectorStoreIds.SourceIdCode,
                    Recursive = true,
                    Path = "src",
                    FileIgnorePatterns = "TrelloDotNet.Tests",
                    GitHubOwner = gitHubOwner,
                    GitHubRepo = gitHubRepo,
                },
                new MarkdownDataSourceGitHub
                {
                    CollectionId = VectorStoreIds.CollectionId,
                    Id = VectorStoreIds.SourceIdMarkdownInCode,
                    Recursive = true,
                    Path = "/",
                    GitHubOwner = gitHubOwner,
                    GitHubRepo = gitHubRepo,
                    LevelsToChunk = 3,
                }
            ], onProgressNotification: notification => Console.WriteLine(notification.GetFormattedMessageWithDetails()), cancellationToken: cancellationToken);
        }
        catch (OperationCanceledException)
        {
            //Empty
        }

        return new OkObjectResult("Done");
    }
}