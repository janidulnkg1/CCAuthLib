using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.IV
{
    public class ProviderIV: IProviderIV
    {
        public Guid? iv;

        public Guid? fallbackiv { get; set; }


        public Guid? GetIV()
        {
            return iv;
        }

        public void SetIV(Guid iv)
        { 
            this.iv = iv;
        }

        Guid IProviderIV.GetIV()
        {
            throw new NotImplementedException();
        }
    }
}
