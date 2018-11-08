using Akkatecture.Core;
using Akkatecture.ValueObjects;
using Newtonsoft.Json;

namespace Akkatecture.Demo.Domain.Model.Device
{
    [JsonConverter(typeof(SingleValueObjectConverter))]
    public class DeviceId : Identity<DeviceId>
    {
        public DeviceId(string value) :base(value)
        {

        }
    }
}
