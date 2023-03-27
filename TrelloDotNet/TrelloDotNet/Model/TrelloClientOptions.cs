namespace TrelloDotNet.Model
{
    /// <summary>
    /// Options for the Trello Client
    /// </summary>
    public class TrelloClientOptions
    {
        /// <summary>
        /// Control level of URL Details are shown in Exceptions from calls to the API
        /// </summary>
        public ApiCallExceptionOption ApiCallExceptionOption { get; set; }
        /// <summary>
        /// Controls if it is allowed to delete Boards (secondary confirmation)
        /// </summary>
        public bool AllowDeleteOfBoards { get; set; }

        /// <summary>
        /// Control if cards should retrieve Custom Fields when retrieving cards
        /// </summary>
        public bool IncludeCustomFieldsInCardGetMethods { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiCallExceptionOption">Control level of URL Details are shown in Exceptions from calls to the API</param>
        /// <param name="allowDeleteOfBoards">Controls if it is allowed to delete Boards (secondary confirmation)</param>
        /// <param name="includeCustomFieldsInCardGetMethods">Control if cards should retrieve Custom Fields when retrieving cards</param>
        public TrelloClientOptions(
            ApiCallExceptionOption apiCallExceptionOption = ApiCallExceptionOption.IncludeUrlButMaskCredentials,
            bool allowDeleteOfBoards = false,
            bool includeCustomFieldsInCardGetMethods = false)
        {
            ApiCallExceptionOption = apiCallExceptionOption;
            AllowDeleteOfBoards = allowDeleteOfBoards;
            IncludeCustomFieldsInCardGetMethods = includeCustomFieldsInCardGetMethods;
        }
    }
}