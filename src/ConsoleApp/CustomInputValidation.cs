using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class CustomInputValidation
    {
        private readonly EndpointConsole endpointConsole;

        public CustomInputValidation(EndpointConsole endpointConsole)
        {
            this.endpointConsole = endpointConsole;
        }

        public int GetInteger(string text, string errorMensage)
        {
            System.Console.Write(text);
            string input = Console.ReadLine();
            int value = 0;
            
            if(input.ToUpper() == "EXIT")
            {
                this.endpointConsole.MenuInitial();
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

        public string GetString(string text, string errorMensage)
        {
            System.Console.Write(text);
            string input = Console.ReadLine();

            if (input.ToUpper() == "EXIT")
            {
                this.endpointConsole.MenuInitial();
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
