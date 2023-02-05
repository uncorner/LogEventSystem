using LogEventSystem.ServiceContract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LogEventSystem.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var channelFactory = new ChannelFactory<ILogEventService>("basicHttpBinding_ILogEventService");
            var service = channelFactory.CreateChannel();

            while (true)
            {
                string select;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Выберите: [1] - запись в лог, [2] - отобразить записи из лога. Подтвердить [Enter]");
                    select = Console.ReadLine();
                }
                while (select != "1" && select != "2");

                if (select == "1")
                {
                    DateTime? dateCreated;
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Введите дату для записи лога, формат [YYYY-MM-DD]");
                        select = Console.ReadLine();
                        dateCreated = ParseDate(select);
                    }
                    while (dateCreated == null);

                    LogEventType eventType;
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Введите статус важности записи: [0] - Info, [1] - Warn, [2] - Error");
                        select = Console.ReadLine();

                        try
                        {
                            eventType = (LogEventType)Enum.Parse(typeof(LogEventType), select, true);
                            if ((int)eventType >= 0 && (int)eventType <= 3)
                            {
                                break;
                            }
                        }
                        catch{}
                    }

                    string description;
                    do
                    {
                        Console.WriteLine();
                        Console.WriteLine("Введите текстовое описание записи лога");
                        description = Console.ReadLine();
                    }
                    while (string.IsNullOrEmpty(description));

                    // write
                    var logEvent = new LogEvent
                    {
                        DateCreated = dateCreated.Value,
                        Description = description,
                        EventType = eventType
                    };

                    service.SaveLogEvent(logEvent);

                    Console.WriteLine();
                    Console.WriteLine("Запись в лог успешшно произведена");
                }
                else
                if (select == "2")
                {
                    Console.WriteLine();
                    Console.WriteLine("Введите параметры поиска записей лога:");

                    var filter = new QueryFilter();

                    while (true)
                    {
                        Console.WriteLine("Введите начальную дату поиска (формат [YYYY-MM-DD]) или оставьте пустым");
                        select = Console.ReadLine();
                        if (string.IsNullOrEmpty(select))
                        {
                            break;
                        }
                        var date = ParseDate(select);
                        if (date != null)
                        {
                            filter.DateFrom = date;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Введите конечную дату поиска (формат [YYYY-MM-DD]) или оставьте пустым");
                        select = Console.ReadLine();
                        if (string.IsNullOrEmpty(select))
                        {
                            break;
                        }
                        var date = ParseDate(select);
                        if (date != null)
                        {
                            filter.DateTo = date;
                            break;
                        }
                    }

                    while (true)
                    {
                        Console.WriteLine("Введите статус важности события ([0] - Info, [1] - Warn, [2] - Error) или оставьте пустым");
                        select = Console.ReadLine();
                        if (string.IsNullOrEmpty(select))
                        {
                            break;
                        }

                        try
                        {
                            filter.EventType = (LogEventType)Convert.ToInt32(select);
                            break;
                        }
                        catch {};
                    }

                    // output
                    var logEvents = service.GetLogEvents(filter);

                    Console.WriteLine("Вывод записей лога:");
                    if (logEvents.ToList().Count == 0)
                    {
                        Console.WriteLine("записи не найдены");
                    }
                    else
                    {
                        foreach (var logEvent in logEvents)
                        {
                            Console.WriteLine(string.Format("| {0,25} | {1,15} | {2:yyyy-MM-dd HH:mm:ss} |",
                                logEvent.Description, logEvent.EventType, logEvent.DateCreated));
                        }
                    }

                }
            }
        }

        private static DateTime? ParseDate(string formatted)
        {
            try
            {
                return DateTime.ParseExact(formatted, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch
            {
                Console.WriteLine("Неверный формат");
                return null;
            }
        }

    }
}
