using System;
using System.ComponentModel;
using Akka.Actor;
using Akka.TestKit.Xunit2;
using Xunit;
using Xunit.Abstractions;
using Akkatecture.Demo.Domain.Model.Device;
using Akkatecture.Demo.Domain.Model.Device.Commands;
using Akkatecture.Demo.Domain.Model.Device.Events;
using Akkatecture.Aggregates;

namespace Akkatecture.Demo.Tests
{
    [Collection("AggregateTests")]
    public class DomainTests : TestKit
    {
        private const string Category = "Aggregates";

        private readonly ITestOutputHelper output;

        private DeviceId TestAggregateId = DeviceId.With("device-891c40a0-f84d-43d2-ba18-64a73db5d897");

        public static string Config =
            @"  akka.loglevel = ""DEBUG""
                akka.stdout-loglevel = ""DEBUG""
                akka.actor.serialize-messages = on
                akka.persistence.journal.plugin = ""akka.persistence.journal.eventstore""

                akka.persistence {
                    journal {
                        plugin = ""akka.persistence.journal.eventstore""
                        eventstore {
                            class = ""Akka.Persistence.EventStore.Journal.EventStoreJournal, Akka.Persistence.EventStore""
                            connection-string = ""ConnectTo=tcp://admin:changeit@localhost:1113; HeartBeatTimeout=500""
                            connection-name = ""Akka""
                            adapter = ""Akkatecture.Demo.Tests.CustomAdapter, Akkatecture.Demo.Tests""
                        }
                        event-adapters {
                            color-tagger  = ""Akkatecture.Events.AggregateEventTagger, Akkatecture""
                            
                        }
                        event-adapter-bindings = {
    
		                    ""Akkatecture.Aggregates.ICommittedEvent, Akkatecture"" = aggregate-event-tagger
    
                        }
                    }
                }
            ";

        public DomainTests(ITestOutputHelper output)
            : base(Config)
        {
            this.output = output;
        }

        [Fact]
        [Category(Category)]
        public void InitialState_AfterAddDevice_DeviceAddedEventEmitted_UsingAsk()
        {
            var probe = CreateTestActor("probeActor");
            Sys.EventStream.Subscribe(probe, typeof(DomainEvent<Device, DeviceId, DeviceAddedEvent>));
            var aggregateManager = Sys.ActorOf(Props.Create(() => new DeviceManager()), "test-devicemanager");

            var aggregateId = TestAggregateId;
            var command = new AddDeviceCommand(aggregateId, "12345", "127.0.0.1");
            aggregateManager.Ask(command);

            ExpectMsg<DomainEvent<Device, DeviceId, DeviceAddedEvent>>(
                x => x.AggregateIdentity.Equals(aggregateId), TimeSpan.FromSeconds(10));
        }

        [Fact]
        [Category(Category)]
        public void InitialState_AfterSetLocationCommand_LocationSetEventEmitted_UsingTell()
        {
            var probe = CreateTestActor("probeActor");
            Sys.EventStream.Subscribe(probe, typeof(DomainEvent<Device, DeviceId, LocationSetEvent>));
            var aggregateManager = Sys.ActorOf(Props.Create(() => new DeviceManager()), "test-devicemanager");

            var aggregateId = TestAggregateId;

            var command = new AddDeviceCommand(aggregateId, "16777215", "127.0.0.1");
            var nextCommand = new SetLocationCommand(aggregateId, "Default");
            aggregateManager.Tell(command);
            aggregateManager.Tell(nextCommand);

            ExpectMsg<DomainEvent<Device, DeviceId, LocationSetEvent>>(
                x => x.AggregateIdentity.Equals(aggregateId)
                     && x.AggregateEvent.Location == "Default");

        }

        [Fact]
        [Category(Category)]
        public void RecoveredState_AfterSetLocationCommand_LocationSetEventEmitted_UsingAsk()
        {
            var probe = CreateTestActor("probeActor");
            Sys.EventStream.Subscribe(probe, typeof(DomainEvent<Device, DeviceId, LocationSetEvent>));
            var aggregateManager = Sys.ActorOf(Props.Create(() => new DeviceManager()), "test-devicemanager");

            var aggregateId = TestAggregateId;

            var cmd = new SetLocationCommand(aggregateId, "Ask Updated Location");

            aggregateManager.Ask(cmd);

            ExpectMsg<DomainEvent<Device, DeviceId, LocationSetEvent>>(
                x => x.AggregateIdentity.Equals(aggregateId)
                     && x.AggregateEvent.Location == "Updated Location", TimeSpan.FromSeconds(10));

        }

        [Fact]
        [Category(Category)]
        public void InitialState_AfterAddDevice_DeviceAddedEventEmitted_UsingTell()
        {
            var probe = CreateTestActor("probeActor");
            Sys.EventStream.Subscribe(probe, typeof(DomainEvent<Device, DeviceId, DeviceAddedEvent>));
            var aggregateManager = Sys.ActorOf(Props.Create(() => new DeviceManager()), "test-devicemanager");

            var aggregateId = TestAggregateId;
            var command = new AddDeviceCommand(aggregateId, "12345", "127.0.0.1");
            aggregateManager.Tell(command);

            ExpectMsg<DomainEvent<Device, DeviceId, DeviceAddedEvent>>(
                x => x.AggregateIdentity.Equals(aggregateId), TimeSpan.FromSeconds(10));
        }

        [Fact]
        [Category(Category)]
        public void RecoveredState_AfterSetLocationCommand_LocationSetEventEmitted_UsingTell()
        {
            var probe = CreateTestActor("probeActor");
            Sys.EventStream.Subscribe(probe, typeof(DomainEvent<Device, DeviceId, LocationSetEvent>));
            var aggregateManager = Sys.ActorOf(Props.Create(() => new DeviceManager()), "test-devicemanager");

            var aggregateId = TestAggregateId;

            var cmd = new SetLocationCommand(aggregateId, "Updated Location");

            aggregateManager.Tell(cmd);

            ExpectMsg<DomainEvent<Device, DeviceId, LocationSetEvent>>(
                x => x.AggregateIdentity.Equals(aggregateId)
                     && x.AggregateEvent.Location == "Updated Location", TimeSpan.FromSeconds(10));

        }
    }
}
