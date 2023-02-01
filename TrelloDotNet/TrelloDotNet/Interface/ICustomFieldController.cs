using System.Collections.Generic;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Interface
{
    public interface ICustomFieldController
    {
        Task<CustomField> GetAsync(string customFieldId);
        Task<CustomField> AddAsync(string longBoardId, string name, CustomFieldType type, CustomFieldPosition position, bool displayOnTheFrontOfCard = true, List<CustomFieldOption> options = null);
        Task<CustomField> AddAsync(Board board, string name, CustomFieldType type, CustomFieldPosition position, bool displayOnTheFrontOfCard = true, List<CustomFieldOption> options = null);
    }
}