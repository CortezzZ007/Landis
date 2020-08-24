using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public static class CustomValidateInput
    {
        public static int GetInteger(string texto, string mensagemDeErro)
        {
            System.Console.WriteLine(texto);
            string input = Console.ReadLine();
            int valor = 0;

            if (int.TryParse(input, out valor))
            {
                return valor;
            }
            else
            {
                System.Console.WriteLine(mensagemDeErro);
                return GetInteger(texto, mensagemDeErro);
            }
        }
    }
}
