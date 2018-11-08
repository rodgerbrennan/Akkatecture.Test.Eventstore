using System;
using System.Collections.Generic;
using System.Text;
using Akkatecture.Aggregates;
using Akkatecture.Extensions;
using Akkatecture.Specifications.Provided;
using Akkatecture.Demo.Domain.Model.Device;
using Akkatecture.Demo.Domain.Model.Device.Commands;
using Akkatecture.Demo.Domain.Model.Device.Events;

namespace Akkatecture.Demo.Domain.Model.Device
{
    public class Device : AggregateRoot<Device, DeviceId, DeviceState>
    {
        public Device(Model.Device.DeviceId aggregateId)
            : base(aggregateId)
        {
            Command<AddDeviceCommand>(Execute);
            Command<SetLocationCommand>(Execute);
        }

        private bool Execute(AddDeviceCommand command)
        {
            var aggregateEvent = new DeviceAddedEvent(command.SerialNumber, command.IpAddress);
            Emit(aggregateEvent);

            return true;
        }

        private bool Execute(SetLocationCommand command)
        {
            var aggregateEvent = new LocationSetEvent(command.Location);
            Emit(aggregateEvent);

            return true;

        }
    }
}
