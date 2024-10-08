﻿using System;

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
        /// Control if cards should retrieve Custom Fields when retrieving cards (WARNING: Non-Get-Methods returning Card will NOT include Custom fields)
        /// </summary>
        [Obsolete("Use Card Methods with 'GetCardOptions' parameter Instead [Will be removed in v2.0 of this nuGet Package]")]
        public bool IncludeCustomFieldsInCardGetMethods { get; set; }

        /// <summary>
        /// Control if cards should retrieve Attachments when retrieving cards (WARNING: Non-Get-Methods returning Card will NOT include Attachments)
        /// </summary>
        [Obsolete("Use Card Methods with 'GetCardOptions' parameter Instead [Will be removed in v2.0 of this nuGet Package]")]
        public bool IncludeAttachmentsInCardGetMethods { get; set; }

        /// <summary>
        /// Controls if it is allowed to delete Organizations (secondary confirmation)
        /// </summary>
        public bool AllowDeleteOfOrganizations { get; set; }

        /// <summary>
        /// Controls how many automated Retries the API should try in case if you get an 'API_TOKEN_LIMIT_EXCEEDED' error from Trello (Default 3) set to -1 to disable the system
        /// </summary>
        public int MaxRetryCountForTokenLimitExceeded { get; set; }

        /// <summary>
        /// Controls how long in seconds system should wait between retries, should it receive an 'API_TOKEN_LIMIT_EXCEEDED' error from Trello (Default 1 sec)
        /// </summary>
        public double DelayInSecondsToWaitInTokenLimitExceededRetry { get; set; }

        /// <summary>
        /// Trello API secret for Webhook signature validation
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TrelloClientOptions()
        {
            ApiCallExceptionOption = ApiCallExceptionOption.IncludeUrlButMaskCredentials;
            AllowDeleteOfBoards = false;
            AllowDeleteOfOrganizations = false;
            MaxRetryCountForTokenLimitExceeded = 3;
            DelayInSecondsToWaitInTokenLimitExceededRetry = 1;
            Secret = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiCallExceptionOption">Control level of URL Details are shown in Exceptions from calls to the API</param>
        /// <param name="allowDeleteOfBoards">Controls if it is allowed to delete Boards (secondary confirmation)</param>
        /// <param name="includeCustomFieldsInCardGetMethods">Control if cards should retrieve Custom Fields when retrieving cards (WARNING: Non Get-Methods returning Card will NOT include Custom fields)</param>
        /// <param name="includeAttachmentsInCardGetMethods">Control if cards should retrieve Attachments when retrieving cards (WARNING: Non Get-Methods returning Card will NOT include Attachments)</param>
        /// <param name="allowDeleteOfOrganizations">Controls if it is allowed to delete Organizations (secondary confirmation)</param>
        /// <param name="maxRetryCountForTokenLimitExceeded">Controls how many automated Retries the API should try in case if you get an 'API_TOKEN_LIMIT_EXCEEDED' error from Trello (Default 3) set to -1 to disable the system</param>
        /// <param name="delayInSecondsToWaitInTokenLimitExceededRetry">Controls how long in seconds system should wait between retries, should it receive an 'API_TOKEN_LIMIT_EXCEEDED' error from Trello (Default 1 sec)</param>
        /// <param name="secret">
        /// Trello API secret for Webhook signature validation.
        /// When passing a secret, signature validation will be turned on for all incoming Webhooks.
        /// </param>
        [Obsolete("Use empty constructor instead [Will be removed in v2.0 of this nuGet Package]")]
        public TrelloClientOptions(
            ApiCallExceptionOption apiCallExceptionOption = ApiCallExceptionOption.IncludeUrlButMaskCredentials,
            bool allowDeleteOfBoards = false,
            bool includeCustomFieldsInCardGetMethods = false,
            bool includeAttachmentsInCardGetMethods = false,
            bool allowDeleteOfOrganizations = false,
            int maxRetryCountForTokenLimitExceeded = 3,
            double delayInSecondsToWaitInTokenLimitExceededRetry = 1,
            string secret = null)
        {
            ApiCallExceptionOption = apiCallExceptionOption;
            AllowDeleteOfBoards = allowDeleteOfBoards;
#pragma warning disable CS0618 // Type or member is obsolete
            IncludeCustomFieldsInCardGetMethods = includeCustomFieldsInCardGetMethods;
            IncludeAttachmentsInCardGetMethods = includeAttachmentsInCardGetMethods;
#pragma warning restore CS0618 // Type or member is obsolete
            AllowDeleteOfOrganizations = allowDeleteOfOrganizations;
            MaxRetryCountForTokenLimitExceeded = maxRetryCountForTokenLimitExceeded;
            DelayInSecondsToWaitInTokenLimitExceededRetry = delayInSecondsToWaitInTokenLimitExceededRetry;
            Secret = secret;
        }
    }
}