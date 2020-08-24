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
        private readonly string error = "Invalid value";
        private readonly string sucess = "Operation performed successfully!";

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
            Console.Clear();
            Console.WriteLine("\nWelcome to Landis+Gyr");
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nChoose the action you want to perform.");
            Console.WriteLine("\n1- New Endpoint");
            Console.WriteLine("\n2- Edit Endpoint");
            Console.WriteLine("\n3- List all endpoints");
            Console.WriteLine("\n4- Search for an endpoint");
            Console.WriteLine("\n5- Delete an Endpoint");
            Console.WriteLine("\n6- Exit");

            Console.WriteLine("\nObs: To return to the initial menu, type EXIT at any time.");

            chosenItem = CustomInputValidation.GetInteger("\nType the action number and press enter: ", this.error);

            switch (chosenItem)
            {
                case 1:
                    this.CreateEndpoint();
                    break;

                case 2:
                    this.Edit();
                    break;

                case 3:
                    this.ListEndpoints();
                    break;

                case 4:
                    this.SearchEndpoint();
                    break;

                case 5:
                    this.Delete();
                    break;

                case 6:
                    this.Exit();
                    break;
                default:
                    Console.WriteLine("\nChoose a valid option: ");
                    Console.Clear();
                    this.MenuInitial();
                    break;
            }
        }
        public void CreateEndpoint()
        {
            var endpointDto = new EndpointDto();
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nStarting an endpoint registration");
            endpointDto.SerialNumber = CustomInputValidation.GetString("\nType the serial number of the endpoint and press enter: ", this.error);

            endpointDto.MeterModelId = this.validateSerialNumberOfEndpoint.Execute(endpointDto.SerialNumber);
            if (this.validateSerialNumberOfEndpoint.Error.Count > 0)
            {
                Console.WriteLine(this.validateSerialNumberOfEndpoint.Error.First());
                this.CreateEndpoint();
            }

            if (endpointDto.MeterModelId == 0)
            {
                endpointDto.MeterModelId = CustomInputValidation.GetInteger("\nType the model meter id and press enter: ", this.error);
            }
            else
            {
                Console.WriteLine("Model Meter ID: " + endpointDto.MeterModelId.ToString());
            }

            endpointDto.MeterNumber = CustomInputValidation.GetInteger("\nType the meter number and press enter: ", this.error);
            endpointDto.MeterFirmwareVersion = CustomInputValidation.GetString("\nType the Meter Firmware version and press enter: ", this.error);
            Console.WriteLine("\nChoose which state the endpoint is in: ");
            Console.WriteLine("1- Disconnected");
            Console.WriteLine("2- Connected");
            Console.WriteLine("3- Armed");

            endpointDto.State = CustomInputValidation.GetInteger("Type the option of your choice and press enter: ", this.error);

            this.createEndpoint.Execute(EndpointFactory.MapEndpoint(endpointDto));
            Console.WriteLine("\nRegistration completed");
            Console.WriteLine(this.sucess);
            this.BackMenuInitial();

        }

        public void Edit()
        {
            var endpointDto = new EndpointDto();
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");
            endpointDto.SerialNumber = CustomInputValidation.GetString("\nType the serial number of the endpoint you want to edit and press enter: ", this.error);

            endpointDto = EndpointFactory.MapEndpointDto(this.searchBySerialNumber.Execute(endpointDto.SerialNumber));
            if (this.searchBySerialNumber.Error.Count > 0)
            {

                Console.WriteLine(this.searchBySerialNumber.Error.First());
                this.Edit();
            }
            Console.WriteLine("\nEndpoint found!");
            var tableData = new List<EndpointDto>();
            tableData.Add(endpointDto);

            var table = ConsoleTable.From(tableData).Configure(y => y.EnableCount = false);
            Console.Write(table);

            var chosenItem = CustomInputValidation.GetInteger("\nDo you want to change the endpoint state?\n1- Yes\n2- No\nType the desired option and press enter:  ", this.error);
            if (chosenItem == 1)
            {
                Console.WriteLine("\nStarting endpoint editing.");
                Console.WriteLine("\nChoose which state the endpoint is in: ");
                Console.WriteLine("1- Disconnected");
                Console.WriteLine("2- Connected");
                Console.WriteLine("3- Armed");

                endpointDto.State = CustomInputValidation.GetInteger("Type the option of your choice and press enter: ", this.error);

                while (!(endpointDto.State == 1 || endpointDto.State == 2 || endpointDto.State == 3))
                {
                    Console.WriteLine("Option invalid.");
                    endpointDto.State = CustomInputValidation.GetInteger("Type the option of your choice and press enter: ", this.error);

                }

                this.editStateEndpoint.Execute(endpointDto.SerialNumber, endpointDto.State);
                if (editStateEndpoint.Error.Count > 0)
                {
                    Console.WriteLine(editStateEndpoint.Error.First());
                }
                else
                {
                    Console.WriteLine("\nFinished editing");
                    Console.WriteLine(sucess);
                }
            }

            this.BackMenuInitial();
        }

        public void Delete()
        {
            var endpointDto = new EndpointDto();
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nDelete Endpoint");
            endpointDto.SerialNumber = CustomInputValidation.GetString("\nType the serial number of the endpoint you want to delete and press enter: ", this.error);

            endpointDto = EndpointFactory.MapEndpointDto(this.searchBySerialNumber.Execute(endpointDto.SerialNumber));
            if (this.searchBySerialNumber.Error.Count > 0)
            {
                Console.WriteLine(this.searchBySerialNumber.Error.First());
                this.Delete();
            }
            Console.WriteLine("\nEndpoint found!");
            var tableData = new List<EndpointDto>();
            tableData.Add(endpointDto);

            var table = ConsoleTable.From(tableData).Configure(y => y.EnableCount = false);
            Console.Write(table);

            var chosenItem = CustomInputValidation.GetInteger("\nDo you really want to delete this endpoint?\n1- Yes\n2- No\nType the desired option and press enter: ", this.error);

            while (!(chosenItem == 1 || chosenItem == 2))
            {
                chosenItem = CustomInputValidation.GetInteger("\nDo you really want to delete this endpoint?\n1- Yes\n2- No\nType the desired option and press enter: ", this.error);
            }
            if (chosenItem == 1)
            {
                this.deleteEndpoint.Execute(endpointDto.SerialNumber);
                if (this.deleteEndpoint.Error.Count > 0)
                {
                    Console.WriteLine(this.searchBySerialNumber.Error.First());
                    this.Delete();
                }
                Console.WriteLine("\n" + sucess);
            }
            this.BackMenuInitial();
        }

        public void SearchEndpoint()
        {
            var endpointDto = new EndpointDto();
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Search for an endpoint");
            endpointDto.SerialNumber = CustomInputValidation.GetString("\nType the serial number of the endpoint you want to find and press enter: ", this.error);

            endpointDto = EndpointFactory.MapEndpointDto(this.searchBySerialNumber.Execute(endpointDto.SerialNumber));
            if (this.searchBySerialNumber.Error.Count > 0)
            {
                Console.WriteLine(this.searchBySerialNumber.Error.First());
                this.SearchEndpoint();
            }

            Console.WriteLine("\nEndpoint found!");
            var tableData = new List<EndpointDto>();
            tableData.Add(endpointDto);

            var table = ConsoleTable.From(tableData).Configure(y => y.EnableCount = false);
            Console.Write(table);
            this.BackMenuInitial();
        }

        public void ListEndpoints()
        {
            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("\nList of all endpoints");

            var endpoints = this.listAllEndpoints.Execute();

            var table = ConsoleTable.From(endpoints);

            Console.WriteLine("\n");
            Console.Write(table);
            this.BackMenuInitial();
        }

        public void BackMenuInitial()
        {
            int returnItem;

            Console.WriteLine("\n-----------------------------------------------------------------------------------------------------------------------");
            returnItem = CustomInputValidation.GetInteger("Type 0 and press enter to return to the home menu: ", this.error);

            while (returnItem != 0)
            {
                Console.WriteLine("\nInvalid Option.");
                returnItem = CustomInputValidation.GetInteger("Type 0 and press enter to return to the home menu: ", this.error);

            }

            Console.Clear();
            this.MenuInitial();
        }

        public void Exit()
        {
            var chosenItem = CustomInputValidation.GetInteger("\nDo you really want to leave?\n1- Yes\n2- No\nType the desired option and press enter: ", this.error);
            if (chosenItem == 1)
            {
                Environment.Exit(0);
            }
            else
            {
                this.MenuInitial();
            }

        }
    }
}
