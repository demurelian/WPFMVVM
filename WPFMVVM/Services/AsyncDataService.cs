using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMVVM.Services
{
    class AsyncDataService : IAsyncDataService
    {
        private const int __SleepTime = 2000;
        public string GetResult(DateTime Time)
        {
            Thread.Sleep(__SleepTime);
            return $"Result value {Time}";
        }
    }
}
