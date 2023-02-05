using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogEventSystem.ServiceHost
{
    public static class EntityMapper
    {
        static EntityMapper()
        {
            Mapper.CreateMap<LogEventSystem.DataStorage.LogEvent, LogEventSystem.ServiceContract.LogEvent>();
            Mapper.CreateMap<LogEventSystem.ServiceContract.LogEvent, LogEventSystem.DataStorage.LogEvent>();
        }

        public static IEnumerable<LogEventSystem.ServiceContract.LogEvent> ToContractLogEvents(IEnumerable<LogEventSystem.DataStorage.LogEvent> events)
        {
            return Mapper.Map<IEnumerable<LogEventSystem.DataStorage.LogEvent>,
                IEnumerable<LogEventSystem.ServiceContract.LogEvent>>(events);
        }

        public static LogEventSystem.DataStorage.LogEvent ToStorageLogEvent(LogEventSystem.ServiceContract.LogEvent logEvent)
        {
            return Mapper.Map<LogEventSystem.ServiceContract.LogEvent, LogEventSystem.DataStorage.LogEvent>(logEvent);
        }


    }
}
