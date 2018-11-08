using System;
using System.Collections.Generic;
using System.Text;
using Akkatecture;
using Akkatecture.Commands;

namespace Akkatecture.Demo.Domain.Model.Device.Commands
{
    public class SetLocationCommand : Command<Device, DeviceId>
    {
        public string Location { get; }

        public SetLocationCommand(DeviceId aggregateId, string location) : base(aggregateId)
        {
            Location = location;
        }
    }
}
