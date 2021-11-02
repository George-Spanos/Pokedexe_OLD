using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure.Data.Tables;

namespace UserManager.Common {
    internal interface IUserStoreService<T> where T : ITableEntity {
        Task<IEnumerable<T>> RetrieveAsync();
        Task<Result<bool>> InsertOrUpdateAsync(T tableUser);
    }
}
