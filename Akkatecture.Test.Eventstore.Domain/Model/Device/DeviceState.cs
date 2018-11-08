using System;
using System.Collections.Generic;
using System.Text;
using Akkatecture.Aggregates;
using Akkatecture.Demo.Domain.Model.Device.Events;

namespace Akkatecture.Demo.Domain.Model.Device
{
    public class DeviceState : AggregateState<Device, DeviceId>
    {
        public string ipAddress { get; private set; }
        public string serialNumber { get; private set; }
        public string location { get; private set; }

        public void Apply(DeviceAddedEvent aggregateEvent)
        {
            ipAddress = aggregateEvent.IpAddress;
            serialNumber = aggregateEvent.SerialNumber;
        }

        public void Apply(LocationSetEvent aggregateEvent)
        {
            location = aggregateEvent.Location;
        }
    }
}
