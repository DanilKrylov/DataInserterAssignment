using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataInserter
{
    internal class RetryPolicy
    {
        private int _currentRetryCount = 0;
        private readonly int _retryMaxCount;

        public RetryPolicy(int retryCount)
        {
            _retryMaxCount = retryCount;
        }

        public void OnFailTry()
        {
            if(_currentRetryCount < _retryMaxCount)
            {
                throw new InvalidOperationException("Failed after retries");
            }

            _currentRetryCount++;
        }
    }
}
