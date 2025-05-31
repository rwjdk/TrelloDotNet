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
        /// Retrieves the membership information for a specific board, including the membership type (Admin, Normal, Observer) for each user.
        /// </summary>
        /// <param name="boardId">ID of the board to retrieve membership information for</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of memberships for the board</returns>
        public async Task<List<Membership>> GetMembershipsOfBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Membership>>(GetUrlBuilder.GetMembershipsOfBoard(boardId), cancellationToken);
        }

        /// <summary>
        /// Changes the membership type of a member on a board (for example, promote a member to Admin).
        /// </summary>
        /// <param name="boardId">ID of the board</param>
        /// <param name="membershipId">ID of the member's membership (not the member ID; obtain this via 'GetMembershipsOfBoardAsync')</param>
        /// <param name="membershipType">The new membership type to assign (Admin, Normal, Observer, etc.)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task UpdateMembershipTypeOfMemberOnBoardAsync(string boardId, string membershipId, MembershipType membershipType, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Put($"{UrlPaths.Boards}/{boardId}/memberships/{membershipId}", cancellationToken, 0,
                new QueryParameter("type", membershipType.GetJsonPropertyName()));
        }

        /// <summary>
        /// Retrieves an overview of workspace and board memberships for the current token user. If the user is an Admin on a workspace, they are also considered Admin on all boards in that workspace.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Overview of memberships for the current token user</returns>
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
        /// Retrieves an overview of workspace and board memberships for the current token user, with options for retrieving boards and organizations. If the user is an Admin on a workspace, they are also considered Admin on all boards in that workspace.
        /// </summary>
        /// <param name="boardOptions">Options for retrieving boards</param>
        /// <param name="organizationOptions">Options for retrieving organizations</param>
        /// <param name="cancellationToken">Cancellation Token</param>
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