using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Attemp2.Services.interfaces
{
    public interface ISMSService
    {
        void SendSingleSmsAsync(string phone, string message);
        bool SendSingleSms(string phone, string message);
        string GetBalance();
        void SendBulkSmsAsync(List<string> to, string message);
        bool SendBulkSms(List<string> to, string message);
    }
}
