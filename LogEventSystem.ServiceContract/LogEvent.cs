using System;

namespace LogEventSystem.ServiceContract
{
    public class LogEvent
    {
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public LogEventType EventType { get; set; }
    }
}
