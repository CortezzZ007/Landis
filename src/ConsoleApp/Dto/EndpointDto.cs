using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Dto
{
    public class EndpointDto
    {
        public string SerialNumber { get; set; }
        public int MeterModelId { get; set; }
        public int MeterNumber { get; set; }
        public int MeterFirmwareVersion { get; set; }
        public int State { get; set; }
    }
}
