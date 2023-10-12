using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.Key
{
    public interface IEncryptionKeyProvider
    {
        byte[] GetEncryptionKey();
        void SetEncryptionKey(byte[] key);
    }
}
