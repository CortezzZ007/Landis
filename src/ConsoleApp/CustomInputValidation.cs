using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public static class CustomInputValidation
    {

        public static int GetInteger(string text, string errorMensage)
        {
            System.Console.Write(text);
            string input = Console.ReadLine();
            int value = 0;
            
            if(input.ToUpper() == "EXIT")
            {
                using (ServiceProvider container = Program.RegisterServices())
                {
                    var controller = container.GetRequiredService<EndpointConsole>();
                    controller.MenuInitial();
                }
            }
            
            if (int.TryParse(input, out value))
            {
                return value;
            }
            else
            {
                System.Console.WriteLine(errorMensage);
                return GetInteger(text, errorMensage);
            }
        }

        public static string GetString(string text, string errorMensage)
        {
            System.Console.Write(text);
            string input = Console.ReadLine();

            if (input.ToUpper() == "EXIT")
            {
                using (ServiceProvider container = Program.RegisterServices())
                {
                    var controller = container.GetRequiredService<EndpointConsole>();
                    controller.MenuInitial();
                }
            }

            if (input != "")
            {
                return input.ToUpper();
            }
            else
            {
                System.Console.WriteLine(errorMensage);
                return GetString(text, errorMensage);
            }
        }
    }
}
