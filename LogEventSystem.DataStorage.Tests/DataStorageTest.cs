using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using LogEventSystem.ServiceContract;

namespace LogEventSystem.DataStorage.Tests
{
    [TestClass]
    public class DataStorageTest
    {

        [TestInitialize]
        public void TestInitialize()
        {
            using (var dbContext = new LogEventSystemModelContainer())
            {
                dbContext.Database.ExecuteSqlCommand("delete from [dbo].[LogEvents]");
            }
        }

        [TestMethod]
        public void TestAddLogEvent()
        {
            var savedDate = new DateTime(2014, 3, 15, 5, 10, 40);

            using (var dbContext = new LogEventSystemModelContainer())
            {
                var logEvent = new LogEvent
                {
                    Description = "some message there",
                    DateCreated = savedDate,
                    EventType = ServiceContract.LogEventType.Info
                };

                var dataStorage = new DataStorage(dbContext);
                dataStorage.AddLogEvent(logEvent);
                dbContext.SaveChanges();
            }

            using (var dbContext = new LogEventSystemModelContainer())
            {
                Assert.AreEqual(1, dbContext.LogEvents.Count());

                var logEvent = dbContext.LogEvents.FirstOrDefault();
                Assert.IsNotNull(logEvent);
                Assert.AreEqual("some message there", logEvent.Description);
                Assert.IsTrue(DateTime.Equals(savedDate, logEvent.DateCreated));
                Assert.AreEqual(ServiceContract.LogEventType.Info, logEvent.EventType);
            }
        }

        private void WriteTestData()
        {
            using (var dbContext = new LogEventSystemModelContainer())
            {
                var dataStorage = new DataStorage(dbContext);

                var logEvent = new LogEvent
                {
                    Description = "some message info",
                    DateCreated = new DateTime(2014, 3, 15, 10, 10, 0),
                    EventType = ServiceContract.LogEventType.Info
                };
                dataStorage.AddLogEvent(logEvent);

                logEvent = new LogEvent
                {
                    Description = "some message info",
                    DateCreated = new DateTime(2014, 3, 15, 10, 30, 0),
                    EventType = ServiceContract.LogEventType.Info
                };
                dataStorage.AddLogEvent(logEvent);

                logEvent = new LogEvent
                {
                    Description = "some message info",
                    DateCreated = new DateTime(2014, 3, 17, 5, 5, 0),
                    EventType = ServiceContract.LogEventType.Info
                };
                dataStorage.AddLogEvent(logEvent);

                logEvent = new LogEvent
                {
                    Description = "some message warn",
                    DateCreated = new DateTime(2014, 3, 18, 10, 10, 0),
                    EventType = ServiceContract.LogEventType.Warning
                };
                dataStorage.AddLogEvent(logEvent);

                logEvent = new LogEvent
                {
                    Description = "some message warn",
                    DateCreated = new DateTime(2014, 3, 19),
                    EventType = ServiceContract.LogEventType.Warning
                };
                dataStorage.AddLogEvent(logEvent);

                logEvent = new LogEvent
                {
                    Description = "some message err",
                    DateCreated = new DateTime(2014, 3, 20, 5, 15, 0),
                    EventType = ServiceContract.LogEventType.Error
                };
                dataStorage.AddLogEvent(logEvent);

                logEvent = new LogEvent
                {
                    Description = "some message err",
                    DateCreated = new DateTime(2014, 3, 20, 5, 25, 0),
                    EventType = ServiceContract.LogEventType.Error
                };
                dataStorage.AddLogEvent(logEvent);

                dbContext.SaveChanges();
            }
        }

        [TestMethod]
        public void TestFetchLogEventsByFilter()
        {
            WriteTestData();

            using (var dbContext = new LogEventSystemModelContainer())
            {
                var dataStorage = new DataStorage(dbContext);

                // all
                var events = dataStorage.FetchLogEventsByFilter(new QueryFilter());
                Assert.AreEqual(7, events.ToList().Count);

                // info
                var filter = new QueryFilter
                {
                    EventType = ServiceContract.LogEventType.Info
                };
                events = dataStorage.FetchLogEventsByFilter(filter);
                Assert.AreEqual(3, events.ToList().Count);

                filter = new QueryFilter
                {
                   DateFrom = new DateTime(2014, 3, 15),
                   DateTo = new DateTime(2014, 3, 17),
                   EventType = ServiceContract.LogEventType.Info
                };
                events = dataStorage.FetchLogEventsByFilter(filter);
                Assert.AreEqual(2, events.ToList().Count);

                filter = new QueryFilter
                {
                   DateFrom = new DateTime(2014, 3, 23),
                   DateTo = new DateTime(2014, 3, 24),
                   EventType = ServiceContract.LogEventType.Info
                };
                events = dataStorage.FetchLogEventsByFilter(filter);
                Assert.AreEqual(0, events.ToList().Count);

                // warn
                filter = new QueryFilter
                {
                   DateFrom = new DateTime(2014, 3, 18),
                   DateTo = new DateTime(2014, 3, 19),
                   EventType = ServiceContract.LogEventType.Warning
                };
                events = dataStorage.FetchLogEventsByFilter(filter);
                Assert.AreEqual(2, events.ToList().Count);

                // err
                filter = new QueryFilter
                {
                   DateFrom = new DateTime(2014, 3, 20),
                   DateTo = new DateTime(2014, 3, 20, 12, 59, 0),
                   EventType = ServiceContract.LogEventType.Error
                };
                events = dataStorage.FetchLogEventsByFilter(filter);
                Assert.AreEqual(2, events.ToList().Count);
            }
        }


    }
}
