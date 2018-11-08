using System;
using System.Collections.Generic;
using System.Text;
using Akkatecture.Aggregates;
using Akkatecture.Events;

namespace Akkatecture.Demo.Domain.Model.Device.Events
{
    [EventVersion("LocationSet", 1)]
    public class LocationSetEvent : AggregateEvent<Device, DeviceId>
    {
        public string Location { get; }

        public LocationSetEvent(string location)
        {
            Location = location;
        }
    }
}
