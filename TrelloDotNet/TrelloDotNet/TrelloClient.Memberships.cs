using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// The Membership Information for a board (aka if Users are Admin, Normal or Observer)
        /// </summary>
        /// <param name="boardId">Id of the Board that you wish information on</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The List of Memberships</returns>
        public async Task<List<Membership>> GetMembershipsOfBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Membership>>($"{UrlPaths.Boards}/{boardId}/memberships", cancellationToken);
        }

        /// <summary>
        /// Change the membership-type of a member Member on a board (Example make them Admin)
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="membershipId">Id of the Member's Membership (NB: This is NOT the memberId..., you get this via method 'GetMembershipsOfBoardAsync')</param>
        /// <param name="membershipType">What type of access the member should be given instead</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateMembershipTypeOfMemberOnBoardAsync(string boardId, string membershipId, MembershipType membershipType, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Put($"{UrlPaths.Boards}/{boardId}/memberships/{membershipId}", cancellationToken,
                new QueryParameter(@"type", membershipType.GetJsonPropertyName()));
        }
    }
}