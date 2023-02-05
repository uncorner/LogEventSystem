using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogEventSystem.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceType = typeof(LogEventService);

            using (var host = new System.ServiceModel.ServiceHost(serviceType))
            {
                host.Open();

                OutputListening(host);

                Console.WriteLine();
                Console.WriteLine("Press <ENTER> to terminate Host");
                Console.WriteLine();
                Console.ReadLine();

                Console.WriteLine("Stop service");
            }
        }

        private static void OutputListening(System.ServiceModel.ServiceHost host)
        {
            foreach (Uri uri in host.BaseAddresses)
            {
                Console.WriteLine("\t{0}", uri.ToString());
            }
            Console.WriteLine();
            Console.WriteLine("Number of dispatchers listening : {0}", host.ChannelDispatchers.Count);
            foreach (System.ServiceModel.Dispatcher.ChannelDispatcher dispatcher in host.ChannelDispatchers)
            {
                Console.WriteLine("\t{0}, {1}", dispatcher.Listener.Uri.ToString(), dispatcher.BindingName);
            }
        }

    }
}
