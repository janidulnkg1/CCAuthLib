using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.IV
{
    public interface IEncryptionIVProvider
    {
        byte[] GetEncryptionIV();
        void SetEncryptionIV(byte[] iv);
    }
}
