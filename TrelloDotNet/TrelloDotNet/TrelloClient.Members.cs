using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    public partial class TrelloClient
    {
        /// <summary>
        /// Get the Members (users) of a board
        /// </summary>
        /// <param name="boardId">Id of the Board (in its long or short version)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Members</returns>
        public async Task<List<Member>> GetMembersOfBoardAsync(string boardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Member>>($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Members}/", cancellationToken);
        }

        /// <summary>
        /// Get the Members (users) of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>List of Members</returns>
        public async Task<List<Member>> GetMembersOfCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<List<Member>>($"{UrlPaths.Cards}/{cardId}/{UrlPaths.Members}/", cancellationToken);
        }

        /// <summary>
        /// Get a Member with a specific Id
        /// </summary>
        /// <param name="memberId">Id of the Member</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>The Member</returns>
        public async Task<Member> GetMemberAsync(string memberId, CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Member>($"{UrlPaths.Members}/{memberId}", cancellationToken);
        }

        /// <summary>
        /// Add one or more Members to a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="memberIdsToAdd">One or more Ids of Members to add</param>
        public async Task<Card> AddMembersToCardAsync(string cardId, params string[] memberIdsToAdd)
        {
            return await AddMembersToCardAsync(cardId, CancellationToken.None, memberIdsToAdd);
        }

        /// <summary>
        /// Add one or more Members to a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="memberIdsToAdd">One or more Ids of Members to add</param>
        public async Task<Card> AddMembersToCardAsync(string cardId, CancellationToken cancellationToken = default, params string[] memberIdsToAdd)
        {
            var card = await GetCardAsync(cardId, cancellationToken);
            var missing = memberIdsToAdd.Where(x => !card.MemberIds.Contains(x)).ToList();

            if (missing.Count == 0)
            {
                return card; //Everyone already There
            }

            //Need update
            card.MemberIds.AddRange(missing);
            return await UpdateCardAsync(card, cancellationToken);
        }

        /// <summary>
        /// Remove a Member of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="memberIdsToRemove">One or more Ids of Members to remove</param>
        public async Task<Card> RemoveMembersFromCardAsync(string cardId, params string[] memberIdsToRemove)
        {
            return await RemoveMembersFromCardAsync(cardId, CancellationToken.None, memberIdsToRemove);
        }

        /// <summary>
        /// Remove a Member of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellation">Cancellation Token</param>
        /// <param name="memberIdsToRemove">One or more Ids of Members to remove</param>
        public async Task<Card> RemoveMembersFromCardAsync(string cardId, CancellationToken cancellation = default, params string[] memberIdsToRemove)
        {
            var card = await GetCardAsync(cardId, cancellation);
            var toRemove = memberIdsToRemove.Where(x => card.MemberIds.Contains(x)).ToList();
            if (toRemove.Count == 0)
            {
                return card; //Everyone not there
            }

            //Need update
            card.MemberIds = card.MemberIds.Except(toRemove).ToList();
            return await UpdateCardAsync(card, cancellation);
        }

        /// <summary>
        /// Remove all Members of a Card
        /// </summary>
        /// <param name="cardId">Id of the Card</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task<Card> RemoveAllMembersFromCardAsync(string cardId, CancellationToken cancellationToken = default)
        {
            var card = await GetCardAsync(cardId, cancellationToken);
            if (card.MemberIds.Any())
            {
                //Need update
                card.MemberIds = new List<string>();
                return await UpdateCardAsync(card, cancellationToken);
            }

            return card;
        }

        /// <summary>
        /// Add a Member to a board (aka give them access)
        /// </summary>
        /// <param name="boardId">Id of the Board to give access to</param>
        /// <param name="memberId">Id of the Member that need access</param>
        /// <param name="membershipType">What type of access the member should be given</param>
        /// <param name="allowBillableGuest">Optional param that allows organization admins to add multi-board guests onto a board.</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task AddMemberToBoardAsync(string boardId, string memberId, MembershipType membershipType, bool allowBillableGuest = false, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Put($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Members}/{memberId}", cancellationToken,
                new QueryParameter(@"type", membershipType.GetJsonPropertyName()),
                new QueryParameter(@"allowBillableGuest", allowBillableGuest));
        }

        /// <summary>
        /// Invite a Member to a board via email (aka give them access)
        /// </summary>
        /// <param name="boardId">Id of the Board to give access to</param>
        /// <param name="email">Email to invite</param>
        /// <param name="membershipType">What type of access the member should be given</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task InviteMemberToBoardViaEmailAsync(string boardId, string email, MembershipType membershipType, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Put($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Members}", cancellationToken,
                new QueryParameter(@"type", membershipType.GetJsonPropertyName()),
                new QueryParameter(@"email", email));
        }

        /// <summary>
        /// Remove a Member from a board (aka revoke access)
        /// </summary>
        /// <param name="boardId">Id of the Board the member should be removed from</param>
        /// <param name="memberId">Id of the Member that should be removed</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        public async Task RemoveMemberFromBoardAsync(string boardId, string memberId, CancellationToken cancellationToken = default)
        {
            await _apiRequestController.Delete($"{UrlPaths.Boards}/{boardId}/{UrlPaths.Members}/{memberId}", cancellationToken);
        }

        /// <summary>
        /// Get information about the Member that own the token used by this TrelloClient
        /// </summary>
        /// <returns>The Member</returns>
        public async Task<Member> GetTokenMemberAsync(CancellationToken cancellationToken = default)
        {
            return await _apiRequestController.Get<Member>($"{UrlPaths.Tokens}/{_apiRequestController.Token}/member", cancellationToken);
        }
    }
}