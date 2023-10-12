using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.Key
{
    public class EncryptionKeyProvider: IEncryptionKeyProvider
    {
        private byte[] key;

        public byte[] GetEncryptionKey()
        {
            return key;
        }

        public void SetEncryptionKey(byte[] key)
        {
            this.key = key;
        }
    }
}
