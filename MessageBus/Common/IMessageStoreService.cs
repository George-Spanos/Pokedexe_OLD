using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MessageBus.Common {
    public interface IMessageStoreService {
        Task<IEnumerable<ITableMessage>> RetrieveAsync();
        Task<ITableMessage> InsertOrUpdateAsync(ITableMessage tableMessage);
    }
}
