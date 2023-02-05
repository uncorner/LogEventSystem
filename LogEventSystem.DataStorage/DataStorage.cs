using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using LogEventSystem.ServiceContract;

namespace LogEventSystem.DataStorage
{
    public class DataStorage
    {
        private LogEventSystemModelContainer dbContext;

        public DataStorage(LogEventSystemModelContainer dbContext)
        {
            if (dbContext == null)
            {
                throw new NullReferenceException();
            }
            this.dbContext = dbContext;            
        }

        public void AddLogEvent(LogEvent logEvent)
        {
            if (logEvent == null)
            {
                throw new NullReferenceException();
            }

            dbContext.LogEvents.AddOrUpdate(logEvent);
        }

        public IEnumerable<LogEvent> FetchLogEventsByFilter(QueryFilter filter)
        {
            IQueryable<LogEvent> query = dbContext.LogEvents;

            if (filter.DateFrom.HasValue)
            {
                query = query.Where(e => e.DateCreated >= filter.DateFrom);
            }
            if (filter.DateTo.HasValue)
            {
                query = query.Where(e => e.DateCreated <= filter.DateTo);
            }
            if (filter.EventType.HasValue)
            {
                query = query.Where(e => (int)e.EventType == (int)filter.EventType);              
            }
            
            return query.OrderBy(e => e.DateCreated).ToList();
        }

    }
}
