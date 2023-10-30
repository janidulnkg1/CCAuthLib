using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.IV
{
    public class EncryptionIVProvider : IEncryptionIVProvider
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private byte[] iv;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
