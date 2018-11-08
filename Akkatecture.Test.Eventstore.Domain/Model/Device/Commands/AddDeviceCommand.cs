using System;
using System.Collections.Generic;
using System.Text;
using Akkatecture;
using Akkatecture.Commands;

namespace Akkatecture.Demo.Domain.Model.Device.Commands
{
    public class AddDeviceCommand : Command<Device, DeviceId>
    {
        public string SerialNumber { get; }
        public string IpAddress { get; }

        public AddDeviceCommand(
            DeviceId aggregateId,
            string serialNumber,
            string ipAddress)
            : base(aggregateId)
        {
            if (serialNumber == null) throw new ArgumentNullException(nameof(serialNumber));
            if (ipAddress == null) throw new ArgumentNullException(nameof(ipAddress));

            SerialNumber = serialNumber;
            IpAddress = ipAddress;
        }
    }
}
