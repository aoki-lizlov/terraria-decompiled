using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using Mono.Security;
using Mono.Security.Cryptography;

namespace System.Reflection
{
	// Token: 0x020008D5 RID: 2261
	[ComVisible(true)]
	[Serializable]
	public class StrongNameKeyPair : ISerializable, IDeserializationCallback
	{
		// Token: 0x06004DAC RID: 19884 RVA: 0x000F5816 File Offset: 0x000F3A16
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(byte[] keyPairArray)
		{
			if (keyPairArray == null)
			{
				throw new ArgumentNullException("keyPairArray");
			}
			this.LoadKey(keyPairArray);
			this.GetRSA();
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x000F583C File Offset: 0x000F3A3C
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(FileStream keyPairFile)
		{
			if (keyPairFile == null)
			{
				throw new ArgumentNullException("keyPairFile");
			}
			byte[] array = new byte[keyPairFile.Length];
			keyPairFile.Read(array, 0, array.Length);
			this.LoadKey(array);
			this.GetRSA();
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x000F5884 File Offset: 0x000F3A84
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public StrongNameKeyPair(string keyPairContainer)
		{
			if (keyPairContainer == null)
			{
				throw new ArgumentNullException("keyPairContainer");
			}
			this._keyPairContainer = keyPairContainer;
			this.GetRSA();
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x000F58A8 File Offset: 0x000F3AA8
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			this._publicKey = (byte[])info.GetValue("_publicKey", typeof(byte[]));
			this._keyPairContainer = info.GetString("_keyPairContainer");
			this._keyPairExported = info.GetBoolean("_keyPairExported");
			this._keyPairArray = (byte[])info.GetValue("_keyPairArray", typeof(byte[]));
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x000F5920 File Offset: 0x000F3B20
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_publicKey", this._publicKey, typeof(byte[]));
			info.AddValue("_keyPairContainer", this._keyPairContainer);
			info.AddValue("_keyPairExported", this._keyPairExported);
			info.AddValue("_keyPairArray", this._keyPairArray, typeof(byte[]));
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x00004088 File Offset: 0x00002288
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x06004DB2 RID: 19890 RVA: 0x000F5988 File Offset: 0x000F3B88
		private RSA GetRSA()
		{
			if (this._rsa != null)
			{
				return this._rsa;
			}
			if (this._keyPairArray != null)
			{
				try
				{
					this._rsa = CryptoConvert.FromCapiKeyBlob(this._keyPairArray);
					goto IL_005A;
				}
				catch
				{
					this._keyPairArray = null;
					goto IL_005A;
				}
			}
			if (this._keyPairContainer != null)
			{
				this._rsa = new RSACryptoServiceProvider(new CspParameters
				{
					KeyContainerName = this._keyPairContainer
				});
			}
			IL_005A:
			return this._rsa;
		}

		// Token: 0x06004DB3 RID: 19891 RVA: 0x000F5A08 File Offset: 0x000F3C08
		private void LoadKey(byte[] key)
		{
			try
			{
				if (key.Length == 16)
				{
					int i = 0;
					int num = 0;
					while (i < key.Length)
					{
						num += (int)key[i++];
					}
					if (num == 4)
					{
						this._publicKey = (byte[])key.Clone();
					}
				}
				else
				{
					this._keyPairArray = key;
				}
			}
			catch
			{
			}
		}

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x06004DB4 RID: 19892 RVA: 0x000F5A68 File Offset: 0x000F3C68
		public byte[] PublicKey
		{
			get
			{
				if (this._publicKey == null)
				{
					RSA rsa = this.GetRSA();
					if (rsa == null)
					{
						throw new ArgumentException("invalid keypair");
					}
					byte[] array = CryptoConvert.ToCapiKeyBlob(rsa, false);
					this._publicKey = new byte[array.Length + 12];
					this._publicKey[0] = 0;
					this._publicKey[1] = 36;
					this._publicKey[2] = 0;
					this._publicKey[3] = 0;
					this._publicKey[4] = 4;
					this._publicKey[5] = 128;
					this._publicKey[6] = 0;
					this._publicKey[7] = 0;
					int num = array.Length;
					this._publicKey[8] = (byte)(num % 256);
					this._publicKey[9] = (byte)(num / 256);
					this._publicKey[10] = 0;
					this._publicKey[11] = 0;
					Buffer.BlockCopy(array, 0, this._publicKey, 12, array.Length);
				}
				return this._publicKey;
			}
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x000F5B4C File Offset: 0x000F3D4C
		internal StrongName StrongName()
		{
			RSA rsa = this.GetRSA();
			if (rsa != null)
			{
				return new StrongName(rsa);
			}
			if (this._publicKey != null)
			{
				return new StrongName(this._publicKey);
			}
			return null;
		}

		// Token: 0x04002FFD RID: 12285
		private byte[] _publicKey;

		// Token: 0x04002FFE RID: 12286
		private string _keyPairContainer;

		// Token: 0x04002FFF RID: 12287
		private bool _keyPairExported;

		// Token: 0x04003000 RID: 12288
		private byte[] _keyPairArray;

		// Token: 0x04003001 RID: 12289
		[NonSerialized]
		private RSA _rsa;
	}
}
