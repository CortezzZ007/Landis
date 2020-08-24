using Adapters;
using CommandDotNet.Rendering;
using ConsoleApp.Dto;
using ConsoleApp.Factories;
using ConsoleTables;
using Domain;
using Stories;
using Stories.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp
{
    public class Controller
    {
        private readonly CreateEndpoint createEndpoint;
        private readonly ListAllEndpoints listAllEndpoints;
        private readonly ValidateSerialNumberOfEndpoint validateSerialNumberOfEndpoint;
        private readonly SearchBySerialNumber searchBySerialNumber;
        private readonly EditStateEndpoint editStateEndpoint;
        private readonly DeleteEndpoint deleteEndpoint;

        public Controller(Stories.Interfaces.IEndpointPersistence endpointPersistence)
        {
            this.createEndpoint = new CreateEndpoint(endpointPersistence);
            this.listAllEndpoints = new ListAllEndpoints(endpointPersistence);
            this.validateSerialNumberOfEndpoint = new ValidateSerialNumberOfEndpoint(endpointPersistence);
            this.searchBySerialNumber = new SearchBySerialNumber(endpointPersistence);
            this.editStateEndpoint = new EditStateEndpoint(endpointPersistence);
            this.deleteEndpoint = new DeleteEndpoint(endpointPersistence);
        }

        public void MenuInitial()
        {
            int chosenItem;

            Console.WriteLine("Bem vindo à Landis+Gyr");
            Console.WriteLine("\n---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nEscolha a ação que deseja realizar:");
            Console.WriteLine("\n1- Criar Endpoint");
            Console.WriteLine("\n2- Editar Endpoint");
            Console.WriteLine("\n3- Listar todos Endpoints");
            Console.WriteLine("\n4- Procurar um Endpoint");
            Console.WriteLine("\n5- Deletar um Endpoint");

            Console.WriteLine("\nDigite o numero da ação e pressione enter");
            chosenItem = int.Parse(Console.ReadLine());

            switch (chosenItem)
            {
                case 1:
                    this.CreateEndpoint();
                    break;

                case 2:
                    this.Edit();
                    break;

                case 3:
                    this.Listar();
                    break;

                case 4:
                    this.SearchEndpoint();
                    break;

                case 5:
                    this.Delete();
                    break;
                default:
                    Console.WriteLine("\nEscolha uma opção válida.");
                    Console.Clear();
                    this.MenuInitial();
                    break;
            }
        }
        public void CreateEndpoint()
        {
            var endpointDto = new EndpointDto();
            Console.WriteLine("Digite o numero de série do endpoint:");
            endpointDto.SerialNumber = Console.ReadLine();

            endpointDto.MeterModelId = this.validateSerialNumberOfEndpoint.Executar(endpointDto.SerialNumber);
            if (this.validateSerialNumberOfEndpoint.Erros.Count > 0)
            {
                Console.WriteLine(this.validateSerialNumberOfEndpoint.Erros.First());
                this.CreateEndpoint();
            }

            if (endpointDto.MeterModelId == 0)
            {
                endpointDto.MeterModelId = CustomValidateInput.GetInteger("Digite o id do modelo:", "Valor invalido");
            }
            else
            {
                Console.WriteLine("ID do modelo do medido: " + endpointDto.MeterModelId.ToString());
            }

            endpointDto.MeterNumber = CustomValidateInput.GetInteger("Digite o numero do medidor:", "Valor invalido");
            endpointDto.MeterFirmwareVersion = CustomValidateInput.GetInteger("Digite a versão da Firmware do medidor:", "Valor invalido");
            Console.WriteLine("Escolha em qual estado se encontra o endpoint: ");
            Console.WriteLine("1- Desconectado");
            Console.WriteLine("2- Conectado");
            Console.WriteLine("3- Armado");

            endpointDto.State = CustomValidateInput.GetInteger("Digite a opção da sua escolha e pressione enter:", "Valor invalido");

            this.createEndpoint.Executar(EndpointFactory.MapearEndpoint(endpointDto));
            Console.WriteLine("Operação realizada com sucesso!");
            this.BackMenuInitial();
        }

        public void Edit()
        {
            var endpointDto = new EndpointDto();
            Console.WriteLine("Digite o serial number do endpoint que deseja editar:");
            endpointDto.SerialNumber = Console.ReadLine();

            endpointDto = EndpointFactory.MapearEndpointDto(this.searchBySerialNumber.Executar(endpointDto.SerialNumber));
            if (this.searchBySerialNumber.Erros.Count > 0)
            {

                Console.WriteLine(this.searchBySerialNumber.Erros.First());
                this.Edit();
            }
            Console.WriteLine("Endpoint encontrado!");
            var dadosTable = new List<EndpointDto>();
            dadosTable.Add(endpointDto);

            var table = ConsoleTable.From(dadosTable);
            Console.Write(table);

            var chosenItem = CustomValidateInput.GetInteger("Deseja alterar o estado do endpoint?\n1- Sim\n2- Não", "Valor invalido");
            if (chosenItem == 1)
            {
                Console.WriteLine("Escolha em qual estado se encontra o endpoint: ");
                Console.WriteLine("1- Desconectado");
                Console.WriteLine("2- Conectado");
                Console.WriteLine("3- Armado");

                endpointDto.State = CustomValidateInput.GetInteger("Digite a opção da sua escolha e pressione enter:", "Valor invalido");

                while (!(endpointDto.State == 1 || endpointDto.State == 2 || endpointDto.State == 3))
                {
                    Console.WriteLine("Opção invalida.");
                    endpointDto.State = CustomValidateInput.GetInteger("Digite a opção da sua escolha e pressione enter:", "Valor invalido");

                }

                this.editStateEndpoint.Executar(endpointDto.SerialNumber, endpointDto.State);
                Console.WriteLine("Operação realizada com sucesso.");
            }
            this.BackMenuInitial();
        }

        public void Delete()
        {
            var endpointDto = new EndpointDto();
            Console.WriteLine("Digite o serial number do endpoint que deseja excluir:");
            endpointDto.SerialNumber = Console.ReadLine();

            endpointDto = EndpointFactory.MapearEndpointDto(this.searchBySerialNumber.Executar(endpointDto.SerialNumber));
            if (this.searchBySerialNumber.Erros.Count > 0)
            {
                Console.WriteLine(this.searchBySerialNumber.Erros.First());
                this.Delete();
            }
            Console.WriteLine("Endpoint encontrado!");
            var dadosTable = new List<EndpointDto>();
            dadosTable.Add(endpointDto);

            var table = ConsoleTable.From(dadosTable);
            Console.Write(table);

            var chosenItem = CustomValidateInput.GetInteger("\neseja realmente excluir este endpoint?\n1- Sim\n2- Não", "Valor invalido");

            while (!(chosenItem == 1 || chosenItem == 2))
            {
                chosenItem = CustomValidateInput.GetInteger("\neseja realmente excluir este endpoint?\n1- Sim\n2- Não", "Valor invalido");
            }
            if(chosenItem == 1)
            {
                this.deleteEndpoint.Executar(endpointDto.SerialNumber);
                if(this.deleteEndpoint.Erros.Count > 0)
                {
                    Console.WriteLine(this.searchBySerialNumber.Erros.First());
                    this.Delete();
                }
                Console.WriteLine("Operação realizada com sucesso.");
            }
            this.BackMenuInitial();
        }

        public void SearchEndpoint()
        {
            var endpointDto = new EndpointDto();
            Console.WriteLine("Digite o serial number do endpoint que deseja encontrar:");
            endpointDto.SerialNumber = Console.ReadLine();

            endpointDto = EndpointFactory.MapearEndpointDto(this.searchBySerialNumber.Executar(endpointDto.SerialNumber));
            if (this.searchBySerialNumber.Erros.Count > 0)
            {
                Console.WriteLine(this.searchBySerialNumber.Erros.First());
                this.SearchEndpoint();
            }
            Console.WriteLine("Endpoint encontrado!");
            var dadosTable = new List<EndpointDto>();
            dadosTable.Add(endpointDto);

            var table = ConsoleTable.From(dadosTable);
            Console.Write(table);
            this.BackMenuInitial();
        }

        public void Listar()
        {
            Console.WriteLine("Listagem de todos os endpoints");

            var endpoints = this.listAllEndpoints.Executar();

            var table = ConsoleTable.From(endpoints);
            Console.Write(table);
            this.BackMenuInitial();
        }

        public void BackMenuInitial()
        {
            int returnItem;
            Console.WriteLine("\n------------------------------------------------------------------------------------------------------");
            returnItem = CustomValidateInput.GetInteger("Digite 0 para retornar ao menu inicial.", "Valor digitado inválido");

            while (returnItem != 0)
            {
                Console.WriteLine("\nOpcão Invalida");
                returnItem = CustomValidateInput.GetInteger("Digite 0 para retornar ao menu inicial.", "Valor digitado inválido");

            }

            this.MenuInitial();
            Console.Clear();
        }
    }
}
