using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.IV
{
    public class EncryptionIVProvider : IEncryptionIVProvider
    {
        private byte[] iv;

        public byte[] GetEncryptionIV()
        {
            return iv;
        }

        public void SetEncryptionIV(byte[] iv)
        {
            this.iv = iv;
        }
    }
}
