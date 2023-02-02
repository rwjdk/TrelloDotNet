using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    /// <summary>
    /// Operations on Custom Fields Management
    /// </summary>
    public interface ICustomFieldController
    {
        /// <summary>
        /// Get a specific Custom Field by its Id
        /// </summary>
        /// <param name="customFieldId">Id of the Custom Field</param>
        /// <returns></returns>
        Task<CustomField> GetCustomFieldAsync(string customFieldId);

        //todo - Re-define signature (class with params)
        Task<CustomField> AddCustomFieldAsync(string boardId, string name, CustomFieldType type, bool displayOnTheFrontOfCard = true, List<CustomFieldOption> options = null);

        //todo - Re-define signature (class with params)
        Task<CustomField> AddCustomFieldAsync(Board board, string name, CustomFieldType type, bool displayOnTheFrontOfCard = true, List<CustomFieldOption> options = null);
    }
}