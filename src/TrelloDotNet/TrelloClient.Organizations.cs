using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.GetOrganizationOptions;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Retrieves an organization (also known as a workspace).
        /// </summary>
        /// <param name="organizationId">ID of the organization to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The requested organization</returns>
        public async Task<Organization> GetOrganizationAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Organization>(GetUrlBuilder.GetOrganization(organizationId), cancellationToken);
        }

        /// <summary>
        /// Retrieves an organization (also known as a workspace), with additional options for selection.
        /// </summary>
        /// <param name="organizationId">ID of the organization to retrieve</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="options">Options for retrieving the organization</param>
        /// <returns>The requested organization</returns>
        public async Task<Organization> GetOrganizationAsync(string organizationId, GetOrganizationOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Organization>(GetUrlBuilder.GetOrganization(organizationId), cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Creates a new organization (workspace).
        /// </summary>
        /// <param name="newOrganization">The organization object to create</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The newly created organization</returns>
        public async Task<Organization> AddOrganizationAsync(Organization newOrganization, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Post<Organization>($"{UrlPaths.Organizations}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(newOrganization));
        }

        /// <summary>
        /// Updates the properties of an existing organization (workspace).
        /// </summary>
        /// <param name="organizationWithChanges">The organization object containing the updated properties</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated organization</returns>
        public async Task<Organization> UpdateOrganizationAsync(Organization organizationWithChanges, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Organization>($"{UrlPaths.Organizations}/{organizationWithChanges.Id}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(organizationWithChanges));
        }

        /// <summary>
        /// Retrieves all organizations that a specific member has access to.
        /// </summary>
        /// <param name="memberId">ID of the member to find organizations for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of organizations the member has access to</returns>
        public async Task<List<Organization>> GetOrganizationsForMemberAsync(string memberId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Organization>>(GetUrlBuilder.GetOrganizationsForMember(memberId), cancellationToken);
        }

        /// <summary>
        /// Retrieves all organizations that a specific member has access to, with additional options for selection.
        /// </summary>
        /// <param name="memberId">ID of the member to find organizations for</param>
        /// <param name="options">Options for retrieving the organizations</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of organizations the member has access to</returns>
        public async Task<List<Organization>> GetOrganizationsForMemberAsync(string memberId, GetOrganizationOptions options, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Organization>>(GetUrlBuilder.GetOrganizationsForMember(memberId), cancellationToken, options.GetParameters());
        }

        /// <summary>
        /// Retrieves all organizations that the token provided to the TrelloClient can access.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of organizations accessible to the current token</returns>
        public async Task<List<Organization>> GetOrganizationsCurrentTokenCanAccessAsync(CancellationToken cancellationToken = default)
        {
            var tokenMember = await GetTokenMemberAsync(cancellationToken);
            return await GetOrganizationsForMemberAsync(tokenMember.Id, cancellationToken);
        }

        /// <summary>
        /// Retrieves all organizations that the token provided to the TrelloClient can access, with additional options for selection.
        /// </summary>
        /// <param name="options">Options for retrieving the organizations</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of organizations accessible to the current token</returns>
        public async Task<List<Organization>> GetOrganizationsCurrentTokenCanAccessAsync(GetOrganizationOptions options, CancellationToken cancellationToken = default)
        {
            var tokenMember = await GetTokenMemberAsync(cancellationToken);
            return await GetOrganizationsForMemberAsync(tokenMember.Id, options, cancellationToken);
        }

        /// <summary>
        /// Deletes an entire organization (workspace), including all boards it contains. This operation is irreversible and requires secondary confirmation via Options.AllowDeleteOfOrganizations.
        /// </summary>
        /// <remarks>
        /// As this is a major operation, secondary confirmation is required by setting: Options.AllowDeleteOfOrganizations = true
        /// </remarks>
        /// <param name="organizationId">ID of the organization to delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteOrganizationAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            if (Options.AllowDeleteOfOrganizations)
            {
                await _apiRequestController.Delete($"{UrlPaths.Organizations}/{organizationId}", cancellationToken, 0);
            }
            else
            {
                throw new SecurityException("Deletion of Organizations are disabled via Options.AllowDeleteOfOrganizations (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back)");
            }
        }

        /// <summary>
        /// Retrieves plan information for a specific workspace (Free, Standard, Premium, Enterprise), including supported features.
        /// </summary>
        /// <param name="organizationId">ID of the workspace</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The plan information for the workspace</returns>
        public async Task<TrelloPlanInformation> GetTrelloPlanInformationForOrganizationAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<TrelloPlanInformation>(GetUrlBuilder.GetOrganization(organizationId), cancellationToken, new QueryParameter("fields", "id,name,premiumFeatures"));
        }
    }
}