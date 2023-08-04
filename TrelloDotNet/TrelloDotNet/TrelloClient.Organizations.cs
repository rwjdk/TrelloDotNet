using System.Collections.Generic;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Get an Organization (also known as Workspace)
        /// </summary>
        /// <param name="organizationId">ID of an Organization</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Organization</returns>
        public async Task<Organization> GetOrganizationAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Organization>($"{UrlPaths.Organizations}/{organizationId}", cancellationToken);
        }

        /// <summary>
        /// Create a new Organization (Workspace)
        /// </summary>
        /// <param name="newOrganization">The new Organization</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The New Organization</returns>
        public async Task<Organization> AddOrganizationAsync(Organization newOrganization, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Post<Organization>($"{UrlPaths.Organizations}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(newOrganization));
        }

        /// <summary>
        /// Update an Organization (Workspace)
        /// </summary>
        /// <param name="organizationWithChanges">Organization with changes</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The updated Organization</returns>
        public async Task<Organization> UpdateOrganizationAsync(Organization organizationWithChanges, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Put<Organization>($"{UrlPaths.Organizations}/{organizationWithChanges.Id}", cancellationToken, _queryParametersBuilder.GetViaQueryParameterAttributes(organizationWithChanges));
        }

        /// <summary>
        /// Get the Organizations that the specified member has access to
        /// </summary>
        /// <param name="memberId">Id of the Member to find organizations for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Organizations there is access to</returns>
        public async Task<List<Organization>> GetOrganizationsForMemberAsync(string memberId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Organization>>($"{UrlPaths.Members}/{memberId}/organizations", cancellationToken);
        }

        /// <summary>
        /// Get the Organizations that the token provided to the TrelloClient can Access
        /// </summary>
        /// <returns>The Organizations there is access to</returns>
        public async Task<List<Organization>> GetOrganizationsCurrentTokenCanAccessAsync(CancellationToken cancellationToken = default)
        {
            var tokenMember = await GetTokenMemberAsync(cancellationToken);
            return await GetOrganizationsForMemberAsync(tokenMember.Id, cancellationToken);
        }

        /// <summary>
        /// Delete an entire Organization including all Boards it contains (WARNING: THERE IS NO WAY GOING BACK!!!).
        /// </summary>
        /// <remarks>
        /// As this is a major thing, there is a secondary confirmation needed by setting: Options.AllowDeleteOfOrganizations = true
        /// </remarks>
        /// <param name="organizationId">The id of the Organization to Delete</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task DeleteOrganizationAsync(string organizationId, CancellationToken cancellationToken = default)
        {
            if (Options.AllowDeleteOfOrganizations)
            {
                await _apiRequestController.Delete($"{UrlPaths.Organizations}/{organizationId}", cancellationToken, 0);
            }
            else
            {
                throw new SecurityException(@"Deletion of Organizations are disabled via Options.AllowDeleteOfOrganizations (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back)");
            }
        }
    }
}