using System.Threading.Tasks;
using Model;
namespace MessageBus.Services {
    public interface IChatHub {
        Task BroadcastMessage(Message message);
    }
}
