using Backend.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Mcp;
using SimpleRag;
using SimpleRag.VectorStorage.Models;

namespace Backend.Functions
{
    public class AskQuestion(Search search)
    {
        [Function("AskQuestion")]
        public async Task<IActionResult> Run(
            [McpToolTrigger(ToolIds.ToolName, ToolIds.ToolDescription)]
            ToolInvocationContext context,
            [McpToolProperty(ToolIds.ParameterQuestion, ToolIds.StringType, ToolIds.ParameterQuestionDescription)]
            string searchQuery)
        {
            SearchResult searchResult = await search.SearchAsync(new SearchOptions
            {
                SearchQuery = searchQuery,
                NumberOfRecordsBack = 10,
                Filter = entity => entity.SourceCollectionId == VectorStoreIds.CollectionId
            });
            return new OkObjectResult(searchResult.GetAsStringResult());
        }
    }
}