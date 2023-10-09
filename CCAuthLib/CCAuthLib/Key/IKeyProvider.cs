using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.Key
{
    public interface IKeyProvider
    {
        string GetKey();
        void SetKey(string key);
    }
}
