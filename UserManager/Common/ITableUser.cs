using Azure.Data.Tables;
namespace UserManager.Common {
    public interface ITableUser : ITableEntity {
        string Sub { get; set; }

        string PictureUrl { get; set; }

        string Name { get; set; }
    }

}
