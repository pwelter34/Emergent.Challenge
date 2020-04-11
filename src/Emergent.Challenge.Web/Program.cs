using System.Threading.Tasks;
using Emergent.Challenge.Core;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace Emergent.Challenge.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

			builder.Services.AddChallengeServices();
            builder.Services.AddBaseAddressHttpClient();		
			
            await builder.Build().RunAsync();
        }
    }
}
