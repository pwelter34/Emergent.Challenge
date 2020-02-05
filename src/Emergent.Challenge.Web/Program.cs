﻿using System.Threading.Tasks;
using Emergent.Challenge.Core;
using Microsoft.AspNetCore.Blazor.Hosting;

namespace Emergent.Challenge.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddChallengeServices();
            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }
    }
}
