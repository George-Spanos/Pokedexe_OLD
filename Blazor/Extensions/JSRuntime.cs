using System.Threading.Tasks;
using Microsoft.JSInterop;
namespace PokedexChat.Extensions {
    public static class JsRuntime {
        public static async Task Log(this IJSRuntime js, object obj)
        {
            await js.InvokeVoidAsync("console.log", obj);
        }
    }
}
