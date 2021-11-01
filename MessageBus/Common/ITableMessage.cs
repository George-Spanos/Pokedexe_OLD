using Azure.Data.Tables;
namespace MessageBus.Common {
    public interface ITableMessage : ITableEntity {

        string Text { get; set; }

        string UserEmail { get; set; }
    }

}
