using ConsoleApp.Dto;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Factories
{
    public static class EndpointFactory
    {
        public static Endpoint MapearEndpoint(EndpointDto endpointDto)
        {
            var endpoint = new Endpoint(endpointDto.SerialNumber, endpointDto.MeterModelId, endpointDto.MeterNumber,
                endpointDto.MeterFirmwareVersion, endpointDto.State);
            
            return endpoint;
                          
        }

        public static EndpointDto MapearEndpointDto(Endpoint endpoint)
        {
            var endpointDto = new EndpointDto();
            if(endpoint != null)
            {

                endpointDto.SerialNumber = endpoint.SerialNumber;
                endpointDto.MeterFirmwareVersion = endpoint.MeterFirmwareVersion;
                endpointDto.MeterModelId = endpoint.MeterModelId;
                endpointDto.MeterNumber = endpoint.MeterNumber;
                endpointDto.State = endpoint.State;
                
            };
            return endpointDto;
        }
    }
}
