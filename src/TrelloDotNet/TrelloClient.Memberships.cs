using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Options.GetBoardOptions;
using TrelloDotNet.Model.Options.GetOrganizationOptions;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// The Membership Information for a board (aka if Users are Admin, Normal, or Observer)
        /// </summary>
        /// <param name="boardId">Id of the Board that you wish information on</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The List of Memberships</returns>
        public async Task<List<Membership>> GetMembershipsOfBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Membership>>(GetUrlBuilder.GetMembershipsOfBoard(boardId), cancellationToken);
        }

        /// <summary>
        /// Change the membership-type of a Member on a board (Example make them Admin)
        /// </summary>
        /// <param name="boardId">Id of the Board</param>
        /// <param name="membershipId">Id of the Member's Membership (NB: This is NOT the memberId..., you get this via method 'GetMembershipsOfBoardAsync')</param>
        /// <param name="membershipType">What type of access the member should be given instead</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateMembershipTypeOfMemberOnBoardAsync(string boardId, string membershipId, MembershipType membershipType, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Put($"{UrlPaths.Boards}/{boardId}/memberships/{membershipId}", cancellationToken, 0,
                new QueryParameter("type", membershipType.GetJsonPropertyName()));
        }

        /// <summary>
        /// Get the Workspace and Board Memberships for current Token User (It is assumed that if user is Admin on Workspace they are also Admin on Boards underneath that workspace)
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public async Task<TokenMembershipOverview> GetCurrentTokenMembershipsAsync(CancellationToken cancellationToken = default)
        {
            return await GetCurrentTokenMembershipsAsync(new GetBoardOptions
            {
                BoardFields = BoardFields.All,
                Filter = GetBoardOptionsFilter.Open
            }, new GetOrganizationOptions
            {
                OrganizationFields = OrganizationFields.All
            }, cancellationToken);
        }

        /// <summary>
        /// Get the Workspace and Board Memberships for current Token User (It is assumed that if user is Admin on Workspace they are also Admin on Boards underneath that workspace)
        /// </summary>
        /// <param name="boardOptions">Options for retrieving boards</param>
        /// <param name="organizationOptions">Options for retrieving organizations</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Overview of memberships for the current token user</returns>
        public async Task<TokenMembershipOverview> GetCurrentTokenMembershipsAsync(GetBoardOptions boardOptions, GetOrganizationOptions organizationOptions, CancellationToken cancellationToken = default)
        {
            Member member = await GetTokenMemberAsync(cancellationToken);
            var organizations = await GetOrganizationsCurrentTokenCanAccessAsync(organizationOptions, cancellationToken);
            var boards = await GetBoardsCurrentTokenCanAccessAsync(boardOptions, cancellationToken);
            var organizationMemberships = new Dictionary<Organization, MembershipType>();
            var boardMemberships = new Dictionary<Board, MembershipType>();

            foreach (Organization organization in organizations)
            {
                Membership orgMemberShip = organization.Memberships.FirstOrDefault(x => x.MemberId == member.Id);
                if (orgMemberShip != null)
                {
                    organizationMemberships.Add(organization, orgMemberShip.MemberType);
                    if (orgMemberShip.MemberType == MembershipType.Admin)
                    {
                        //Since user is workspace admin, they are automatically also board admin for all boards under
                        foreach (var boardId in organization.BoardIds)
                        {
                            Board board = boards.FirstOrDefault(x => x.Id == boardId);
                            if (board != null)
                            {
                                boardMemberships.Add(board, MembershipType.Admin);
                            }
                        }
                    }
                }
            }

            foreach (Board board in boards.Where(board => !boardMemberships.ContainsKey(board)))
            {
                var membershipsOfBoard = await GetMembershipsOfBoardAsync(board.Id, cancellationToken);
                Membership boardMemberShip = membershipsOfBoard.FirstOrDefault(x => x.MemberId == member.Id);
                if (boardMemberShip != null)
                {
                    boardMemberships.Add(board, boardMemberShip.MemberType);
                }
            }

            return new TokenMembershipOverview
            {
                OrganizationMemberships = organizationMemberships,
                BoardMemberships = boardMemberships
            };
        }
    }
}