using Adapters;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Stories.Endpoints;
using Stories.Interfaces;
using System;

namespace ConsoleApp
{
    class Program
    {
        public static ServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IEndpointPersistence, EndpointPersistence>();
            services.AddTransient<EndpointConsole>();

            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            using (ServiceProvider container = RegisterServices())
            {
                var endpointConsole = container.GetRequiredService<EndpointConsole>();
                endpointConsole.MenuInitial();
            }
          
        }
    }
}
