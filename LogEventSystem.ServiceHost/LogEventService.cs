using LogEventSystem.DataStorage;
using LogEventSystem.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LogEventSystem.ServiceHost
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LogEventService : ILogEventService
    {

        public IEnumerable<LogEventSystem.ServiceContract.LogEvent> GetLogEvents(QueryFilter filter)
        {
            using (var dbContext = new LogEventSystemModelContainer())
            {
                var dataStorage = new LogEventSystem.DataStorage.DataStorage(dbContext);
                var logEvents = dataStorage.FetchLogEventsByFilter(filter);

                return EntityMapper.ToContractLogEvents(logEvents);
            }
        }

        public void SaveLogEvent(LogEventSystem.ServiceContract.LogEvent logEvent)
        {
            using (var dbContext = new LogEventSystemModelContainer())
            {
                var storageLogEvent = EntityMapper.ToStorageLogEvent(logEvent);
                var dataStorage = new LogEventSystem.DataStorage.DataStorage(dbContext);
                dataStorage.AddLogEvent(storageLogEvent);

                dbContext.SaveChanges();
            }
        }
        
    }
}
