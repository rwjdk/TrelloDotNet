using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    public class BoardController : IBoardController //todo - determine if things should be called something else than "xxxController"
    {
        private readonly ApiRequestController _apiRequestController;

        internal BoardController(ApiRequestController apiRequestController)
        {
            _apiRequestController = apiRequestController;
        }

        public async Task<Board> GetAsync(string longOrShortBoardId)
        {
            return await _apiRequestController.GetResponse<Board>($"{Constants.UrlSuffixGroup.Boards}/{longOrShortBoardId}");
        }
        
        /// <inheritdoc />
        public async Task<ListWithRawJsonIncluded<List>> GetListsAsync(string longOrShortBoardId, ListFilter filter = ListFilter.Open)
        {
            var parameters = new []
            {
                new UriParameter("filter", filter.GetJsonPropertyName())
            };
            return await _apiRequestController.GetResponse<ListWithRawJsonIncluded<List>>($"{Constants.UrlSuffixGroup.Boards}/{longOrShortBoardId}/lists", parameters);
        }

        public async Task<ListWithRawJsonIncluded<Label>> GetLabelsAsync(string longOrShortBoardId)
        {
            return await _apiRequestController.GetResponse<ListWithRawJsonIncluded<Label>>($"{Constants.UrlSuffixGroup.Boards}/{longOrShortBoardId}/labels");
        }

        /// <summary>
        /// Get all open cards on un-archived lists
        /// </summary>
        /// <param name="longOrShortBoardId">Id of the board (short or long version)</param>
        /// <returns>TODO</returns>
        public async Task<ListWithRawJsonIncluded<Card>> GetCardsAsync(string longOrShortBoardId)
        {
            return await _apiRequestController.GetResponse<ListWithRawJsonIncluded<Card>>($"{Constants.UrlSuffixGroup.Boards}/{longOrShortBoardId}/cards/");
        }

        /// <summary>
        /// The cards on list based on their status regardless if they are on archived lists
        /// </summary>
        /// <param name="longOrShortBoardId">TODO</param>
        /// <param name="filter">TODO</param>
        /// <returns>TODO</returns>
        public async Task<ListWithRawJsonIncluded<Card>> GetCardsFilteredAsync(string longOrShortBoardId, CardsFilter filter)
        {
            return await _apiRequestController.GetResponse<ListWithRawJsonIncluded<Card>>($"{Constants.UrlSuffixGroup.Boards}/{longOrShortBoardId}/cards/{filter.GetJsonPropertyName()}");
        }

        public async Task<ListWithRawJsonIncluded<Member>> GetMembersAsync(string longOrShortBoardId)
        {
            return await _apiRequestController.GetResponse<ListWithRawJsonIncluded<Member>>($"{Constants.UrlSuffixGroup.Boards}/{longOrShortBoardId}/members/");
        }


        //todo: Get Memberships of a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-memberships-get)
        //todo: Update a board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-put)
        //todo: Delete a board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-delete)
        //todo: Get Field on a board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-field-get)
        //todo: Get Actions of a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-boardid-actions-get)
        //todo: Get a Card on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-cards-idcard-get)
        //todo: Get boardStars on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-boardid-boardstars-get)
        //todo: Get Checklists on a board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-checklists-get)
        //todo: Get Custom Fields for Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-customfields-get)
        //todo: Create Label on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-labels-post)
        //todo: Create a list on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-lists-post)
        //todo: Get filtered Lists on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-lists-filter-get)
        //todo: Get Members on a board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-get)
        //todo: Invite Member to board via Email (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-put)
        //todo: Add Member to Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-put)
        //todo: Remove Member from Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-members-idmember-delete)
        //todo: Update Membership of Member on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-memberships-idmembership-put)
        //todo: Update emailPosition Pref on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-myprefs-emailposition-put)
        //todo: Update idEmailList Pref on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-myprefs-idemaillist-put)
        //todo: Update showListGuide Pref on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-myprefs-showlistguide-put)
        //todo: Update showSidebar Pref on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-myprefs-showsidebar-put)
        //todo: Update showSidebarActivity Pref on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-myprefs-showsidebaractivity-put)
        //todo: Update showSidebarBoardActions Pref on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-myprefs-showsidebarboardactions-put)
        //todo: Update showSidebarMembers Pref on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-myprefs-showsidebarmembers-put)
        //todo: Create a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-post)
        //todo: Create a calendarKey for a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-calendarkey-generate-post)
        //todo: Create a emailKey for a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-emailkey-generate-post)
        //todo: Create a Tag for a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-idtags-post)
        //todo: Mark board as viewed (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-markedasviewed-post)
        //todo: Get Enabled Powerups on Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-boardplugins-get)
        //todo: Get Power-Ups on a Board (https://developer.atlassian.com/cloud/trello/rest/api-group-boards/#api-boards-id-plugins-get)
    }
}