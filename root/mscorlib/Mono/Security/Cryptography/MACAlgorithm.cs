using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000076 RID: 118
	internal class MACAlgorithm
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00012E04 File Offset: 0x00011004
		public MACAlgorithm(SymmetricAlgorithm algorithm)
		{
			this.algo = algorithm;
			this.algo.Mode = CipherMode.CBC;
			this.blockSize = this.algo.BlockSize >> 3;
			this.algo.IV = new byte[this.blockSize];
			this.block = new byte[this.blockSize];
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00012E64 File Offset: 0x00011064
		public void Initialize(byte[] key)
		{
			this.algo.Key = key;
			if (this.enc == null)
			{
				this.enc = this.algo.CreateEncryptor();
			}
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00012EA4 File Offset: 0x000110A4
		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = Math.Min(this.blockSize - this.blockCount, cb);
			Array.Copy(rgb, ib, this.block, this.blockCount, num);
			this.blockCount += num;
			if (this.blockCount == this.blockSize)
			{
				this.enc.TransformBlock(this.block, 0, this.blockSize, this.block, 0);
				int num2 = (cb - num) / this.blockSize;
				for (int i = 0; i < num2; i++)
				{
					this.enc.TransformBlock(rgb, num, this.blockSize, this.block, 0);
					num += this.blockSize;
				}
				this.blockCount = cb - num;
				if (this.blockCount > 0)
				{
					Array.Copy(rgb, num, this.block, 0, this.blockCount);
				}
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00012F7C File Offset: 0x0001117C
		public byte[] Final()
		{
			byte[] array;
			if (this.blockCount > 0 || (this.algo.Padding != PaddingMode.Zeros && this.algo.Padding != PaddingMode.None))
			{
				array = this.enc.TransformFinalBlock(this.block, 0, this.blockCount);
			}
			else
			{
				array = (byte[])this.block.Clone();
			}
			if (!this.enc.CanReuseTransform)
			{
				this.enc.Dispose();
				this.enc = null;
			}
			return array;
		}

		// Token: 0x04000E2D RID: 3629
		private SymmetricAlgorithm algo;

		// Token: 0x04000E2E RID: 3630
		private ICryptoTransform enc;

		// Token: 0x04000E2F RID: 3631
		private byte[] block;

		// Token: 0x04000E30 RID: 3632
		private int blockSize;

		// Token: 0x04000E31 RID: 3633
		private int blockCount;
	}
}
