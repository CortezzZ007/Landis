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
        static ServiceProvider RegistrarServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IEndpointPersistence, EndpointPersistence>();
            services.AddTransient<Controller>();

            return services.BuildServiceProvider();
        }

        static void Main(string[] args)
        {
            using (ServiceProvider container = RegistrarServices())
            {
                var controller = container.GetRequiredService<Controller>();
                controller.MenuInitial();
            }
          
        }
    }
}
