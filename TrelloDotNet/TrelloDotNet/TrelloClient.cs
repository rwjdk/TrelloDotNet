using System.Net.Http;
using System.Threading.Tasks;
using TrelloDotNet.Control;
using TrelloDotNet.Interface;
using TrelloDotNet.Model;

namespace TrelloDotNet
{
    /// <inheritdoc />
    public class TrelloClient : ITrelloClient
    {
        private static readonly HttpClient StaticHttpClient = new HttpClient();

        private readonly ApiRequestController _apiRequestController;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiKey">The Trello API Key you generate on https://trello.com/power-ups/admin/</param>
        /// <param name="token">Your Authorization Token</param>
        public TrelloClient(string apiKey, string token)
        {
            _apiRequestController = new ApiRequestController(StaticHttpClient, apiKey, token);
            Boards = new BoardController(_apiRequestController, this);
            Lists = new ListController(_apiRequestController, this);
            Cards = new CardController(_apiRequestController, this);
            Labels = new LabelController(_apiRequestController, this);
            Checklists = new ChecklistController(_apiRequestController, this);
            CustomFields = new CustomFieldController(_apiRequestController); //todo - consider if support should be removed for now
            Members = new MembersController(_apiRequestController, this);
        }
        
        /// <inheritdoc />
        public IBoardController Boards { get; }

        /// <inheritdoc />
        public IListController Lists { get; }

        /// <inheritdoc />
        public ICardController Cards { get; }

        /// <inheritdoc />
        public ILabelController Labels { get; }

        /// <inheritdoc />
        public IChecklistController Checklists { get; }

        /// <inheritdoc />
        public ICustomFieldController CustomFields { get; }

        /// <inheritdoc />
        public IMembersController Members { get; }

        /// <inheritdoc />
        public async Task<T> GetAsync<T>(string suffix, params UriParameter[] parameters)
        {
            return await _apiRequestController.Get<T>(suffix, parameters);
        }
        
        /// <inheritdoc />
        public async Task<string> GetAsync(string suffix, params UriParameter[] parameters)
        {
            return await _apiRequestController.Get(suffix, parameters);
        }
        
        /// <inheritdoc />
        public async Task<T> PostAsync<T>(string suffix, params UriParameter[] parameters)
        {
            return await _apiRequestController.Post<T>(suffix, parameters);
        }
        
        /// <inheritdoc />
        public async Task<string> PostAsync(string suffix, params UriParameter[] parameters)
        {
            return await _apiRequestController.Post(suffix, parameters);
        }
        
        /// <inheritdoc />
        public async Task<T> PutAsync<T>(string suffix, params UriParameter[] parameters)
        {
            return await _apiRequestController.Put<T>(suffix, parameters);
        }

        /// <inheritdoc />
        public async Task<string> PutAsync(string suffix, params UriParameter[] parameters)
        {
            return await _apiRequestController.Put(suffix, parameters);
        }
    }
}
