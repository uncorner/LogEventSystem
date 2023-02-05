using System;

namespace LogEventSystem.ServiceContract
{
    public class QueryFilter
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public LogEventType? EventType { get; set; }
    }
}
