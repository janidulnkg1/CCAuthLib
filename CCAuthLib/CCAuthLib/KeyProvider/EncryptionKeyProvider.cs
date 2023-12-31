﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCAuthLib.Key
{
    public class EncryptionKeyProvider: IEncryptionKeyProvider
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private byte[] key;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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
