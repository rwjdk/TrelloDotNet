using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    /// <summary>
    /// The Main Client to communicate with the Trello API (aka everything is done via this)
    /// </summary>
    public interface ITrelloClient
    {
        //todo - Need To Have (Needed for v1)
        //- What about return limits (max records back?) - Investigate
        //- Determine if things should be called something else than "xxxController" (perhaps xxxOperations instead)
        //- Determine if I should get rid of the groupings (hard to group logically and make it less understandable) and have all method directly on the TrelloClient
        //- Actions
        //- Check other APIs for features that I might have missed
        //- Web-hooks (+ reaction to it)
        //- Use Branches, lock master and get github actions fully automated for push of nuget packages

        //todo - Nice To Have (Not needed for v1)
        //- Common Scenario/Actions List (aka things that is not a one to one API call... Example: "Move Card to List with name" so user do not need to set everything up themselves)
        //- Create unit-test suite (borderline Need)
        //- Batch-system (why???)
        //- Organizations (how to gain access?)

        /// <summary>
        /// Board-related API Methods
        /// </summary>
        IBoardController Boards { get; }

        /// <summary>
        /// List-related API Methods
        /// </summary>
        IListController Lists { get; }

        /// <summary>
        /// Cards-related API Methods
        /// </summary>
        ICardController Cards { get; }

        /// <summary>
        /// Label-related API Methods
        /// </summary>
        ILabelController Labels { get; }

        /// <summary>
        /// Checklist-related API Methods
        /// </summary>
        IChecklistController Checklists { get; }

        /// <summary>
        /// Custom Fields-related API Methods (NB: Feature NOT available in Free Edition of Trello)
        /// </summary>
        ICustomFieldController CustomFields { get; }

        /// <summary>
        /// Member-relates API Methods
        /// </summary>
        IMembersController Members { get; }

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        Task<T> GetAsync<T>(string suffix, params UriParameter[] parameters);

        /// <summary>
        /// Custom Get Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        Task<string> GetAsync(string suffix, params UriParameter[] parameters);

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        Task<T> PostAsync<T>(string suffix, params UriParameter[] parameters);

        /// <summary>
        /// Custom Post Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        Task<string> PostAsync(string suffix, params UriParameter[] parameters);

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API. Please use System.Text.Json.Serialization.JsonPropertyName on you class to match Json Properties
        /// </summary>
        /// <typeparam name="T">Object to Return</typeparam>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>The Object specified to be returned</returns>
        Task<T> PutAsync<T>(string suffix, params UriParameter[] parameters);

        /// <summary>
        /// Custom Put Method to be used on unexposed features of the API delivered back as JSON.
        /// </summary>
        /// <param name="suffix">API Suffix (aka anything needed after https://api.trello.com/1/ but before that URI Parameters)</param>
        /// <param name="parameters">Additional Parameters</param>
        /// <returns>JSON Representation of response</returns>
        Task<string> PutAsync(string suffix, params UriParameter[] parameters);
    }
}