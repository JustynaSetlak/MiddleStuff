using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationSample.Results
{
    public interface ILoginResult
    {
        public bool IsSuccessful { get;  }

        public string Token { get; }
    }
}
