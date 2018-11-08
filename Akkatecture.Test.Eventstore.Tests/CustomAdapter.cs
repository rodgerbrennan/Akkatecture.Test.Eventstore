using System;
using System.Collections.Generic;
using System.Text;
using Akka.Persistence.EventStore;
using Newtonsoft.Json.Linq;
using Akkatecture;
using Akkatecture.Aggregates;
using Akkatecture.Core;

namespace Akkatecture.Demo.Tests
{
    public class CustomAdapter : DefaultEventAdapter
    {
        protected override byte[] ToBytes(object @event, JObject metadata, out string type, out bool isJson)
        {

            var bytes = base.ToBytes(@event, metadata, out type, out isJson);

            //Add some additional metadata:
            metadata["additionalProp"] = true;

            type = @event.GetType().GetGenericArguments()[2].Name.ToEventCase();

            return bytes;

        }
        protected override object ToEvent(byte[] bytes, JObject metadata)
        {
            //Use the metadata to determine if you need to do something additional to the data
            //Do something additional with bytes before handing it off to be deserialized.
            return base.ToEvent(bytes, metadata);
        }
    }
}
