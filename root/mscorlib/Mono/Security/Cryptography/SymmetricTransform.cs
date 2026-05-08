using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000072 RID: 114
	internal abstract class SymmetricTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0001159C File Offset: 0x0000F79C
		public SymmetricTransform(SymmetricAlgorithm symmAlgo, bool encryption, byte[] rgbIV)
		{
			this.algo = symmAlgo;
			this.encrypt = encryption;
			this.BlockSizeByte = this.algo.BlockSize >> 3;
			if (rgbIV == null)
			{
				rgbIV = KeyBuilder.IV(this.BlockSizeByte);
			}
			else
			{
				rgbIV = (byte[])rgbIV.Clone();
			}
			if (rgbIV.Length < this.BlockSizeByte)
			{
				throw new CryptographicException(Locale.GetText("IV is too small ({0} bytes), it should be {1} bytes long.", new object[] { rgbIV.Length, this.BlockSizeByte }));
			}
			this.padmode = this.algo.Padding;
			this.temp = new byte[this.BlockSizeByte];
			Buffer.BlockCopy(rgbIV, 0, this.temp, 0, Math.Min(this.BlockSizeByte, rgbIV.Length));
			this.temp2 = new byte[this.BlockSizeByte];
			this.FeedBackByte = this.algo.FeedbackSize >> 3;
			this.workBuff = new byte[this.BlockSizeByte];
			this.workout = new byte[this.BlockSizeByte];
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000116B0 File Offset: 0x0000F8B0
		~SymmetricTransform()
		{
			this.Dispose(false);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000116E0 File Offset: 0x0000F8E0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000116F0 File Offset: 0x0000F8F0
		protected virtual void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (disposing)
				{
					Array.Clear(this.temp, 0, this.BlockSizeByte);
					this.temp = null;
					Array.Clear(this.temp2, 0, this.BlockSizeByte);
					this.temp2 = null;
				}
				this.m_disposed = true;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool CanReuseTransform
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00011741 File Offset: 0x0000F941
		public virtual int InputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00011741 File Offset: 0x0000F941
		public virtual int OutputBlockSize
		{
			get
			{
				return this.BlockSizeByte;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001174C File Offset: 0x0000F94C
		protected virtual void Transform(byte[] input, byte[] output)
		{
			switch (this.algo.Mode)
			{
			case CipherMode.CBC:
				this.CBC(input, output);
				return;
			case CipherMode.ECB:
				this.ECB(input, output);
				return;
			case CipherMode.OFB:
				this.OFB(input, output);
				return;
			case CipherMode.CFB:
				this.CFB(input, output);
				return;
			case CipherMode.CTS:
				this.CTS(input, output);
				return;
			default:
				throw new NotImplementedException("Unkown CipherMode" + this.algo.Mode.ToString());
			}
		}

		// Token: 0x06000319 RID: 793
		protected abstract void ECB(byte[] input, byte[] output);

		// Token: 0x0600031A RID: 794 RVA: 0x000117D8 File Offset: 0x0000F9D8
		protected virtual void CBC(byte[] input, byte[] output)
		{
			if (this.encrypt)
			{
				for (int i = 0; i < this.BlockSizeByte; i++)
				{
					byte[] array = this.temp;
					int num = i;
					array[num] ^= input[i];
				}
				this.ECB(this.temp, output);
				Buffer.BlockCopy(output, 0, this.temp, 0, this.BlockSizeByte);
				return;
			}
			Buffer.BlockCopy(input, 0, this.temp2, 0, this.BlockSizeByte);
			this.ECB(input, output);
			for (int j = 0; j < this.BlockSizeByte; j++)
			{
				int num2 = j;
				output[num2] ^= this.temp[j];
			}
			Buffer.BlockCopy(this.temp2, 0, this.temp, 0, this.BlockSizeByte);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00011890 File Offset: 0x0000FA90
		protected virtual void CFB(byte[] input, byte[] output)
		{
			if (this.encrypt)
			{
				for (int i = 0; i < this.BlockSizeByte; i++)
				{
					this.ECB(this.temp, this.temp2);
					output[i] = this.temp2[0] ^ input[i];
					Buffer.BlockCopy(this.temp, 1, this.temp, 0, this.BlockSizeByte - 1);
					Buffer.BlockCopy(output, i, this.temp, this.BlockSizeByte - 1, 1);
				}
				return;
			}
			for (int j = 0; j < this.BlockSizeByte; j++)
			{
				this.encrypt = true;
				this.ECB(this.temp, this.temp2);
				this.encrypt = false;
				Buffer.BlockCopy(this.temp, 1, this.temp, 0, this.BlockSizeByte - 1);
				Buffer.BlockCopy(input, j, this.temp, this.BlockSizeByte - 1, 1);
				output[j] = this.temp2[0] ^ input[j];
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0001197C File Offset: 0x0000FB7C
		protected virtual void OFB(byte[] input, byte[] output)
		{
			throw new CryptographicException("OFB isn't supported by the framework");
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011988 File Offset: 0x0000FB88
		protected virtual void CTS(byte[] input, byte[] output)
		{
			throw new CryptographicException("CTS isn't supported by the framework");
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00011994 File Offset: 0x0000FB94
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
				throw new ArgumentException("inputBuffer", Locale.GetText("Overflow"));
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000119F4 File Offset: 0x0000FBF4
		public virtual int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Object is disposed");
			}
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (outputBuffer == null)
			{
				throw new ArgumentNullException("outputBuffer");
			}
			if (outputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("outputOffset", "< 0");
			}
			int num = outputBuffer.Length - inputCount - outputOffset;
			if (!this.encrypt && 0 > num && (this.padmode == PaddingMode.None || this.padmode == PaddingMode.Zeros))
			{
				throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
			}
			if (this.KeepLastBlock)
			{
				if (0 > num + this.BlockSizeByte)
				{
					throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
				}
			}
			else if (0 > num)
			{
				if (inputBuffer.Length - inputOffset - outputBuffer.Length != this.BlockSizeByte)
				{
					throw new CryptographicException("outputBuffer", Locale.GetText("Overflow"));
				}
				inputCount = outputBuffer.Length - outputOffset;
			}
			return this.InternalTransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00011AE9 File Offset: 0x0000FCE9
		private bool KeepLastBlock
		{
			get
			{
				return !this.encrypt && this.padmode != PaddingMode.None && this.padmode != PaddingMode.Zeros;
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00011B0C File Offset: 0x0000FD0C
		private int InternalTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			int num = inputOffset;
			int num2;
			if (inputCount != this.BlockSizeByte)
			{
				if (inputCount % this.BlockSizeByte != 0)
				{
					throw new CryptographicException("Invalid input block size.");
				}
				num2 = inputCount / this.BlockSizeByte;
			}
			else
			{
				num2 = 1;
			}
			if (this.KeepLastBlock)
			{
				num2--;
			}
			int num3 = 0;
			if (this.lastBlock)
			{
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, outputBuffer, outputOffset, this.BlockSizeByte);
				outputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
				this.lastBlock = false;
			}
			for (int i = 0; i < num2; i++)
			{
				Buffer.BlockCopy(inputBuffer, num, this.workBuff, 0, this.BlockSizeByte);
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, outputBuffer, outputOffset, this.BlockSizeByte);
				num += this.BlockSizeByte;
				outputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
			}
			if (this.KeepLastBlock)
			{
				Buffer.BlockCopy(inputBuffer, num, this.workBuff, 0, this.BlockSizeByte);
				this.lastBlock = true;
			}
			return num3;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00011C28 File Offset: 0x0000FE28
		private void Random(byte[] buffer, int start, int length)
		{
			if (this._rng == null)
			{
				this._rng = RandomNumberGenerator.Create();
			}
			byte[] array = new byte[length];
			this._rng.GetBytes(array);
			Buffer.BlockCopy(array, 0, buffer, start, length);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00011C68 File Offset: 0x0000FE68
		private void ThrowBadPaddingException(PaddingMode padding, int length, int position)
		{
			string text = string.Format(Locale.GetText("Bad {0} padding."), padding);
			if (length >= 0)
			{
				text += string.Format(Locale.GetText(" Invalid length {0}."), length);
			}
			if (position >= 0)
			{
				text += string.Format(Locale.GetText(" Error found at position {0}."), position);
			}
			throw new CryptographicException(text);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00011CD4 File Offset: 0x0000FED4
		protected virtual byte[] FinalEncrypt(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int num = inputCount / this.BlockSizeByte * this.BlockSizeByte;
			int num2 = inputCount - num;
			int i = num;
			PaddingMode paddingMode = this.padmode;
			if (paddingMode == PaddingMode.PKCS7 || paddingMode - PaddingMode.ANSIX923 <= 1)
			{
				i += this.BlockSizeByte;
			}
			else
			{
				if (inputCount == 0)
				{
					return new byte[0];
				}
				if (num2 != 0)
				{
					if (this.padmode == PaddingMode.None)
					{
						throw new CryptographicException("invalid block length");
					}
					byte[] array = new byte[num + this.BlockSizeByte];
					Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
					inputBuffer = array;
					inputOffset = 0;
					inputCount = array.Length;
					i = inputCount;
				}
			}
			byte[] array2 = new byte[i];
			int num3 = 0;
			while (i > this.BlockSizeByte)
			{
				this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array2, num3);
				inputOffset += this.BlockSizeByte;
				num3 += this.BlockSizeByte;
				i -= this.BlockSizeByte;
			}
			byte b = (byte)(this.BlockSizeByte - num2);
			switch (this.padmode)
			{
			case PaddingMode.PKCS7:
			{
				int num4 = array2.Length;
				while (--num4 >= array2.Length - (int)b)
				{
					array2[num4] = b;
				}
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			}
			case PaddingMode.ANSIX923:
				array2[array2.Length - 1] = b;
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			case PaddingMode.ISO10126:
				this.Random(array2, array2.Length - (int)b, (int)(b - 1));
				array2[array2.Length - 1] = b;
				Buffer.BlockCopy(inputBuffer, inputOffset, array2, num, num2);
				this.InternalTransformBlock(array2, num, this.BlockSizeByte, array2, num);
				return array2;
			}
			this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array2, num3);
			return array2;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00011E84 File Offset: 0x00010084
		protected virtual byte[] FinalDecrypt(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			int i = inputCount;
			int num = inputCount;
			if (this.lastBlock)
			{
				num += this.BlockSizeByte;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			while (i > 0)
			{
				int num3 = this.InternalTransformBlock(inputBuffer, inputOffset, this.BlockSizeByte, array, num2);
				inputOffset += this.BlockSizeByte;
				num2 += num3;
				i -= this.BlockSizeByte;
			}
			if (this.lastBlock)
			{
				this.Transform(this.workBuff, this.workout);
				Buffer.BlockCopy(this.workout, 0, array, num2, this.BlockSizeByte);
				num2 += this.BlockSizeByte;
				this.lastBlock = false;
			}
			byte b = ((num > 0) ? array[num - 1] : 0);
			switch (this.padmode)
			{
			case PaddingMode.PKCS7:
			{
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.padmode, (int)b, -1);
				}
				for (int j = (int)(b - 1); j > 0; j--)
				{
					if (array[num - 1 - j] != b)
					{
						this.ThrowBadPaddingException(this.padmode, -1, j);
					}
				}
				num -= (int)b;
				break;
			}
			case PaddingMode.ANSIX923:
			{
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.padmode, (int)b, -1);
				}
				for (int k = (int)(b - 1); k > 0; k--)
				{
					if (array[num - 1 - k] != 0)
					{
						this.ThrowBadPaddingException(this.padmode, -1, k);
					}
				}
				num -= (int)b;
				break;
			}
			case PaddingMode.ISO10126:
				if (b == 0 || (int)b > this.BlockSizeByte)
				{
					this.ThrowBadPaddingException(this.padmode, (int)b, -1);
				}
				num -= (int)b;
				break;
			}
			if (num > 0)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				Array.Clear(array, 0, array.Length);
				return array2;
			}
			return new byte[0];
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00012043 File Offset: 0x00010243
		public virtual byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("Object is disposed");
			}
			this.CheckInput(inputBuffer, inputOffset, inputCount);
			if (this.encrypt)
			{
				return this.FinalEncrypt(inputBuffer, inputOffset, inputCount);
			}
			return this.FinalDecrypt(inputBuffer, inputOffset, inputCount);
		}

		// Token: 0x04000E0E RID: 3598
		protected SymmetricAlgorithm algo;

		// Token: 0x04000E0F RID: 3599
		protected bool encrypt;

		// Token: 0x04000E10 RID: 3600
		protected int BlockSizeByte;

		// Token: 0x04000E11 RID: 3601
		protected byte[] temp;

		// Token: 0x04000E12 RID: 3602
		protected byte[] temp2;

		// Token: 0x04000E13 RID: 3603
		private byte[] workBuff;

		// Token: 0x04000E14 RID: 3604
		private byte[] workout;

		// Token: 0x04000E15 RID: 3605
		protected PaddingMode padmode;

		// Token: 0x04000E16 RID: 3606
		protected int FeedBackByte;

		// Token: 0x04000E17 RID: 3607
		private bool m_disposed;

		// Token: 0x04000E18 RID: 3608
		protected bool lastBlock;

		// Token: 0x04000E19 RID: 3609
		private RandomNumberGenerator _rng;
	}
}
