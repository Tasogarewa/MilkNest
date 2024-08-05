using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Interfaces
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }
}
