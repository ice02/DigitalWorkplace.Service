using DigitalWorkplace.Service.Common;
using DigitalWorkplace.Service.Endpoints;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Williablog.Core.Configuration;

namespace WCFTestModule
{
    //[HttpEndpoint]
    [ExportService("WCFDuplexService", typeof(WCFDuplexService))]
    public class WCFDuplexService : IWCFDuplexService
    {
        public WCFDuplexService()
        {
            ConfigSystem.Install();
        }

        public int Add(int operandA, int operandB)
        {
            return 0;
        }

        public void Initialize()
        {
            ConfigSystem.Install();
        }
    }
}
