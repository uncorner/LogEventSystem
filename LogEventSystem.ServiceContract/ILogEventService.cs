﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace LogEventSystem.ServiceContract
{
    [ServiceContract]
    public interface ILogEventService
    {
        [OperationContract]
        IEnumerable<LogEvent> GetLogEvents(QueryFilter filter);

        [OperationContract]
        void SaveLogEvent(LogEvent logEvent);
    }
}
