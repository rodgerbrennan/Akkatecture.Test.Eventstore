using System;
using System.Collections.Generic;
using System.Text;
using Akkatecture.Aggregates;
using Akkatecture.Events;

namespace Akkatecture.Demo.Domain.Model.Device.Events
{
    [EventVersion("DeviceCreated", 1)]
    public class DeviceAddedEvent : AggregateEvent<Device, DeviceId>
    {
        public string SerialNumber { get; }
        public string IpAddress { get; }

        public DeviceAddedEvent(string serialNumber, string ipAddress)
        {
            SerialNumber = serialNumber;
            IpAddress = ipAddress;
        }
    }
}
