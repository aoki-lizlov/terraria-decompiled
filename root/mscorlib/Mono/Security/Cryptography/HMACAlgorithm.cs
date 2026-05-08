using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000075 RID: 117
	internal class HMACAlgorithm
	{
		// Token: 0x0600033F RID: 831 RVA: 0x00012BF5 File Offset: 0x00010DF5
		public HMACAlgorithm(string algoName)
		{
			this.CreateHash(algoName);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00012C04 File Offset: 0x00010E04
		~HMACAlgorithm()
		{
			this.Dispose();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00012C30 File Offset: 0x00010E30
		private void CreateHash(string algoName)
		{
			this.algo = HashAlgorithm.Create(algoName);
			this.hashName = algoName;
			this.block = new BlockProcessor(this.algo, 8);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00012C57 File Offset: 0x00010E57
		public void Dispose()
		{
			if (this.key != null)
			{
				Array.Clear(this.key, 0, this.key.Length);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00012C75 File Offset: 0x00010E75
		public HashAlgorithm Algo
		{
			get
			{
				return this.algo;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00012C7D File Offset: 0x00010E7D
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00012C85 File Offset: 0x00010E85
		public string HashName
		{
			get
			{
				return this.hashName;
			}
			set
			{
				this.CreateHash(value);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00012C8E File Offset: 0x00010E8E
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00012C96 File Offset: 0x00010E96
		public byte[] Key
		{
			get
			{
				return this.key;
			}
			set
			{
				if (value != null && value.Length > 64)
				{
					this.key = this.algo.ComputeHash(value);
					return;
				}
				this.key = (byte[])value.Clone();
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00012CC8 File Offset: 0x00010EC8
		public void Initialize()
		{
			this.hash = null;
			this.block.Initialize();
			byte[] array = this.KeySetup(this.key, 54);
			this.algo.Initialize();
			this.block.Core(array);
			Array.Clear(array, 0, array.Length);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00012D18 File Offset: 0x00010F18
		private byte[] KeySetup(byte[] key, byte padding)
		{
			byte[] array = new byte[64];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = key[i] ^ padding;
			}
			for (int j = key.Length; j < 64; j++)
			{
				array[j] = padding;
			}
			return array;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00012D58 File Offset: 0x00010F58
		public void Core(byte[] rgb, int ib, int cb)
		{
			this.block.Core(rgb, ib, cb);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00012D68 File Offset: 0x00010F68
		public byte[] Final()
		{
			this.block.Final();
			byte[] array = this.algo.Hash;
			byte[] array2 = this.KeySetup(this.key, 92);
			this.algo.Initialize();
			this.algo.TransformBlock(array2, 0, array2.Length, array2, 0);
			this.algo.TransformFinalBlock(array, 0, array.Length);
			this.hash = this.algo.Hash;
			this.algo.Clear();
			Array.Clear(array2, 0, array2.Length);
			Array.Clear(array, 0, array.Length);
			return this.hash;
		}

		// Token: 0x04000E28 RID: 3624
		private byte[] key;

		// Token: 0x04000E29 RID: 3625
		private byte[] hash;

		// Token: 0x04000E2A RID: 3626
		private HashAlgorithm algo;

		// Token: 0x04000E2B RID: 3627
		private string hashName;

		// Token: 0x04000E2C RID: 3628
		private BlockProcessor block;
	}
}
