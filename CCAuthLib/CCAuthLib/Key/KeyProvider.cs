using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.Key
{
    public class KeyProvider
    {
        private string key;

        public string GetKey()
        {
            return key;
        }

        public void SetKey(string key)
        {
            //ensuring use of a 256 bit key and not null
            if (key == null || key.Length != 32)
            {
                throw new ArgumentException("Invalid key length. The key must be 256 bits (32 bytes).");
            }

            this.key = key;
        }
    }
}
