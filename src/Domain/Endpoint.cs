using System;

namespace Domain
{
    public class Endpoint
    {
        public Endpoint(string serialNumber, int meterModelId, int meterNumber, int meterFirmwareVersion, int state)
        {
            SerialNumber = serialNumber;
            MeterModelId = meterModelId;
            MeterNumber = meterNumber;
            MeterFirmwareVersion = meterFirmwareVersion;
            State = state;
        }

        public string SerialNumber { get; private set; }
        public int MeterModelId { get; private set; }
        public int MeterNumber { get; private set; }
        public int MeterFirmwareVersion { get; private set; }
        public int State { get; private set; }

        public void EditState(int state)
        {
            this.State = state;
        }
    }
}
