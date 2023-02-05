using System.Net.Http;

namespace TrelloDotNet
{
    /// <summary>
    /// Control level of URL Details are shown in Exceptions from calls to the API
    /// </summary>
    public enum ApiCallExceptionOption
    {
        /// <summary>
        /// Exception show entire URL + Credentials
        /// </summary>
        IncludeUrlAndCredentials,
        /// <summary>
        /// Exception show entire URL but credentials are replaced with XXXXX
        /// </summary>
        IncludeUrlButMaskCredentials,
        /// <summary>
        /// The URL that caused the exception are not shown
        /// </summary>
        DoNotIncludeTheUrl
    }
}