using System.Collections.Generic;
using System.Threading.Tasks;
namespace MessageBus.Common {
    internal interface IMessageStoreService {
        Task<IEnumerable<ITableMessage>> RetrieveAsync();
        Task InsertOrUpdateAsync(ITableMessage tableMessage);
    }
}
