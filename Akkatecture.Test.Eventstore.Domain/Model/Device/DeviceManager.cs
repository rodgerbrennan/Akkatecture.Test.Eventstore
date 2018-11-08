using System;
using System.Collections.Generic;
using System.Text;
using Akkatecture.Aggregates;
using Akkatecture.Commands;

namespace Akkatecture.Demo.Domain.Model.Device
{
    public class DeviceManager : AggregateManager<Device, DeviceId, Command<Device, DeviceId>>
    {
    }
}
