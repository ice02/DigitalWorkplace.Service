using DigitalWorkplace.Service.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWorkplace.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            var catalog = new DirectoryCatalog(".");
            var manager = new ServiceManager(ep => new CompositionContainer(catalog, ep));

            foreach (var service in manager.Services)
            {
                foreach (var address in service.Description.Endpoints)
                {
                    Console.WriteLine("Hosting Service: " + service.Meta.Name + " at " + address.Address.Uri);
                }

                service.Open();
            }

            Console.ReadKey();

            foreach (var service in manager.Services)
                service.Close();
        }
    }
}
