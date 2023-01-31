using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    public interface ITrelloClient
    {
        IBoardController Boards { get; }
        IListController Lists { get; }
        ICardController Cards { get; }
        Task<T> GetAsync<T>(string suffix);
        Task<string> GetAsync(string suffix);
        Task<T> Post<T>(string suffix, params UriParameter[] parameters);
        Task<string> Post(string suffix, params UriParameter[] parameters);

        //todo - what about return limits (max records back?)

        //Todo: Actions
        //Todo: Applications
        //Todo: Batch
        //Todo: Checklists
        //Todo: CustomFields
        //Todo: Emoji
        //Todo: Enterprises
        //Todo: Labels
        //Todo: Members
        //Todo: Notifications
        //Todo: Organizations
        //Todo: Plugins
        //Todo: Search
        //Todo: Tokens
        //Todo: Webhooks
    }
}