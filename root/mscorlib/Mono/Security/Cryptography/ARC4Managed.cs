using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000065 RID: 101
	internal class ARC4Managed : RC4, ICryptoTransform, IDisposable
	{
		// Token: 0x06000263 RID: 611 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		public ARC4Managed()
		{
			this.state = new byte[256];
			this.m_disposed = false;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D700 File Offset: 0x0000B900
		~ARC4Managed()
		{
			this.Dispose(true);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D730 File Offset: 0x0000B930
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				this.x = 0;
				this.y = 0;
				if (this.key != null)
				{
					Array.Clear(this.key, 0, this.key.Length);
					this.key = null;
				}
				Array.Clear(this.state, 0, this.state.Length);
				this.state = null;
				GC.SuppressFinalize(this);
				this.m_disposed = true;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000D79E File Offset: 0x0000B99E
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		public override byte[] Key
		{
			get
			{
				if (this.KeyValue == null)
				{
					this.GenerateKey();
				}
				return (byte[])this.KeyValue.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Key");
				}
				this.KeyValue = (this.key = (byte[])value.Clone());
				this.KeySetup(this.key);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000408A File Offset: 0x0000228A
		public bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D801 File Offset: 0x0000BA01
		public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D80B File Offset: 0x0000BA0B
		public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgvIV)
		{
			this.Key = rgbKey;
			return this.CreateEncryptor();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D81A File Offset: 0x0000BA1A
		public override void GenerateIV()
		{
			this.IV = new byte[0];
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D828 File Offset: 0x0000BA28
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00003FB7 File Offset: 0x000021B7
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00003FB7 File Offset: 0x000021B7
		public int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00003FB7 File Offset: 0x000021B7
		public int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D840 File Offset: 0x0000BA40
		private void KeySetup(byte[] key)
		{
			byte b = 0;
			byte b2 = 0;
			for (int i = 0; i < 256; i++)
			{
				this.state[i] = (byte)i;
			}
			this.x = 0;
			this.y = 0;
			for (int j = 0; j < 256; j++)
			{
				b2 = key[(int)b] + this.state[j] + b2;
				byte b3 = this.state[j];
				this.state[j] = this.state[(int)b2];
				this.state[(int)b2] = b3;
				b = (byte)((int)(b + 1) % key.Length);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		private void CheckInput(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "< 0");
			}
			if (inputCount < 0)
			{
				throw new ArgumentOutOfRangeException("inputCount", "< 0");
			}
			if (inputOffset > inputBuffer.Length - inputCount)
			{
				throw new ArgumentException(Locale.GetText("Overflow"), "inputBuffer");
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D928 File Offset: 0x0000BB28
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			if (outputOffset > outputBuffer.Length - inputCount)
			{
				throw new ArgumentException(Locale.GetText("Overflow"), "outputBuffer");
			}
			return this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D990 File Offset: 0x0000BB90
		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			for (int i = 0; i < inputCount; i++)
			{
				this.x += 1;
				this.y = this.state[(int)this.x] + this.y;
				byte b = this.state[(int)this.x];
				this.state[(int)this.x] = this.state[(int)this.y];
				this.state[(int)this.y] = b;
				byte b2 = this.state[(int)this.x] + this.state[(int)this.y];
				outputBuffer[outputOffset + i] = inputBuffer[inputOffset + i] ^ this.state[(int)b2];
			}
			return inputCount;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000DA44 File Offset: 0x0000BC44
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			byte[] array = new byte[inputCount];
			this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, array, 0);
			return array;
		}

		// Token: 0x04000DD0 RID: 3536
		private byte[] key;

		// Token: 0x04000DD1 RID: 3537
		private byte[] state;

		// Token: 0x04000DD2 RID: 3538
		private byte x;

		// Token: 0x04000DD3 RID: 3539
		private byte y;

		// Token: 0x04000DD4 RID: 3540
		private bool m_disposed;
	}
}
