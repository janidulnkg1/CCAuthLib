using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.IV
{
    public interface IProviderIV
    {
        Guid GetIV();
        void SetIV(Guid iv);
    }
}
