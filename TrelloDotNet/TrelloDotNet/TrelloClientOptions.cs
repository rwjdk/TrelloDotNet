using System.Net.Http;

namespace TrelloDotNet
{
    /// <summary>
    /// Options for the Trello Client
    /// </summary>
    public class TrelloClientOptions
    {
        public ApiCallExceptionOption ApiCallExceptionOption { get; set; }
        public bool AllowDeleteOfBoards { get; set; }

        public TrelloClientOptions(
            ApiCallExceptionOption apiCallExceptionOption = ApiCallExceptionOption.IncludeUrlButMaskCredentials,
            bool allowDeleteOfBoards = false)
        {
            ApiCallExceptionOption = apiCallExceptionOption;
            AllowDeleteOfBoards = allowDeleteOfBoards;
        }
    }

    public enum DeleteMethodsUsage
    {
        Enabled,
        Disabled
    }

    public enum ApiCallExceptionOption
    {
        IncludeUrlAndCredentials,
        IncludeUrlButMaskCredentials,
        DoNotIncludeTheUrl
    }
}