using DigitalWorkplace.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFTestModule
{
    [ServiceContract]
    interface IWCFDuplexService : IHostedService
    {
        [OperationContract]
        int Add(int operandA, int operandB);

        void Initialize();

    }
}
