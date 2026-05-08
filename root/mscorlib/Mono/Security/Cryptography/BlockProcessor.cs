using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000068 RID: 104
	internal class BlockProcessor
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000EAB0 File Offset: 0x0000CCB0
		public BlockProcessor(ICryptoTransform transform)
			: this(transform, transform.InputBlockSize)
		{
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000EABF File Offset: 0x0000CCBF
		public BlockProcessor(ICryptoTransform transform, int blockSize)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			if (blockSize <= 0)
			{
				throw new ArgumentOutOfRangeException("blockSize");
			}
			this.transform = transform;
			this.blockSize = blockSize;
			this.block = new byte[blockSize];
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000EB00 File Offset: 0x0000CD00
		~BlockProcessor()
		{
			Array.Clear(this.block, 0, this.blockSize);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000EB38 File Offset: 0x0000CD38
		public void Initialize()
		{
			Array.Clear(this.block, 0, this.blockSize);
			this.blockCount = 0;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000EB53 File Offset: 0x0000CD53
		public void Core(byte[] rgb)
		{
			this.Core(rgb, 0, rgb.Length);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public void Core(byte[] rgb, int ib, int cb)
		{
			int num = Math.Min(this.blockSize - this.blockCount, cb);
			Buffer.BlockCopy(rgb, ib, this.block, this.blockCount, num);
			this.blockCount += num;
			if (this.blockCount == this.blockSize)
			{
				this.transform.TransformBlock(this.block, 0, this.blockSize, this.block, 0);
				int num2 = (cb - num) / this.blockSize;
				for (int i = 0; i < num2; i++)
				{
					this.transform.TransformBlock(rgb, num + ib, this.blockSize, this.block, 0);
					num += this.blockSize;
				}
				this.blockCount = cb - num;
				if (this.blockCount > 0)
				{
					Buffer.BlockCopy(rgb, num + ib, this.block, 0, this.blockCount);
				}
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000EC39 File Offset: 0x0000CE39
		public byte[] Final()
		{
			return this.transform.TransformFinalBlock(this.block, 0, this.blockCount);
		}

		// Token: 0x04000DD6 RID: 3542
		private ICryptoTransform transform;

		// Token: 0x04000DD7 RID: 3543
		private byte[] block;

		// Token: 0x04000DD8 RID: 3544
		private int blockSize;

		// Token: 0x04000DD9 RID: 3545
		private int blockCount;
	}
}
