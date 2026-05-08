using System;
using System.Buffers;
using System.IO;

namespace System.Security.Cryptography
{
	// Token: 0x02000441 RID: 1089
	public abstract class HashAlgorithm : IDisposable, ICryptoTransform
	{
		// Token: 0x06002DA5 RID: 11685 RVA: 0x000025BE File Offset: 0x000007BE
		protected HashAlgorithm()
		{
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000A5B20 File Offset: 0x000A3D20
		public static HashAlgorithm Create()
		{
			return CryptoConfigForwarder.CreateDefaultHashAlgorithm();
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000A5B27 File Offset: 0x000A3D27
		public static HashAlgorithm Create(string hashName)
		{
			return (HashAlgorithm)CryptoConfigForwarder.CreateFromName(hashName);
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x000A5B34 File Offset: 0x000A3D34
		public virtual int HashSize
		{
			get
			{
				return this.HashSizeValue;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x000A5B3C File Offset: 0x000A3D3C
		public virtual byte[] Hash
		{
			get
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException(null);
				}
				if (this.State != 0)
				{
					throw new CryptographicUnexpectedOperationException("Hash must be finalized before the hash value is retrieved.");
				}
				byte[] hashValue = this.HashValue;
				return (byte[])((hashValue != null) ? hashValue.Clone() : null);
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000A5B77 File Offset: 0x000A3D77
		public byte[] ComputeHash(byte[] buffer)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			this.HashCore(buffer, 0, buffer.Length);
			return this.CaptureHashCodeAndReinitialize();
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000A5BA8 File Offset: 0x000A3DA8
		public bool TryComputeHash(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			if (destination.Length < this.HashSizeValue / 8)
			{
				bytesWritten = 0;
				return false;
			}
			this.HashCore(source);
			if (!this.TryHashFinal(destination, out bytesWritten))
			{
				throw new InvalidOperationException("The algorithm's implementation is incorrect.");
			}
			this.HashValue = null;
			this.Initialize();
			return true;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000A5C04 File Offset: 0x000A3E04
		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Non-negative number required.");
			}
			if (count < 0 || count > buffer.Length)
			{
				throw new ArgumentException("Argument {0} should be larger than {1}.");
			}
			if (buffer.Length - count < offset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			this.HashCore(buffer, offset, count);
			return this.CaptureHashCodeAndReinitialize();
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000A5C7C File Offset: 0x000A3E7C
		public byte[] ComputeHash(Stream inputStream)
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
			byte[] array = ArrayPool<byte>.Shared.Rent(4096);
			byte[] array2;
			try
			{
				int num;
				while ((num = inputStream.Read(array, 0, array.Length)) > 0)
				{
					this.HashCore(array, 0, num);
				}
				array2 = this.CaptureHashCodeAndReinitialize();
			}
			finally
			{
				CryptographicOperations.ZeroMemory(array);
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return array2;
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000A5CF8 File Offset: 0x000A3EF8
		private byte[] CaptureHashCodeAndReinitialize()
		{
			this.HashValue = this.HashFinal();
			byte[] array = (byte[])this.HashValue.Clone();
			this.Initialize();
			return array;
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000A5D1C File Offset: 0x000A3F1C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000A5D2B File Offset: 0x000A3F2B
		public void Clear()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000A5D33 File Offset: 0x000A3F33
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._disposed = true;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002DB2 RID: 11698 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual int InputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002DB3 RID: 11699 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual int OutputBlockSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002DB4 RID: 11700 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool CanTransformMultipleBlocks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06002DB5 RID: 11701 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000A5D3F File Offset: 0x000A3F3F
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			this.ValidateTransformBlock(inputBuffer, inputOffset, inputCount);
			this.State = 1;
			this.HashCore(inputBuffer, inputOffset, inputCount);
			if (outputBuffer != null && (inputBuffer != outputBuffer || inputOffset != outputOffset))
			{
				Buffer.BlockCopy(inputBuffer, inputOffset, outputBuffer, outputOffset, inputCount);
			}
			return inputCount;
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000A5D78 File Offset: 0x000A3F78
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			this.ValidateTransformBlock(inputBuffer, inputOffset, inputCount);
			this.HashCore(inputBuffer, inputOffset, inputCount);
			this.HashValue = this.CaptureHashCodeAndReinitialize();
			byte[] array;
			if (inputCount != 0)
			{
				array = new byte[inputCount];
				Buffer.BlockCopy(inputBuffer, inputOffset, array, 0, inputCount);
			}
			else
			{
				array = Array.Empty<byte>();
			}
			this.State = 0;
			return array;
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000A5DC8 File Offset: 0x000A3FC8
		private void ValidateTransformBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", "Non-negative number required.");
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException("Argument {0} should be larger than {1}.");
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
			}
			if (this._disposed)
			{
				throw new ObjectDisposedException(null);
			}
		}

		// Token: 0x06002DB9 RID: 11705
		protected abstract void HashCore(byte[] array, int ibStart, int cbSize);

		// Token: 0x06002DBA RID: 11706
		protected abstract byte[] HashFinal();

		// Token: 0x06002DBB RID: 11707
		public abstract void Initialize();

		// Token: 0x06002DBC RID: 11708 RVA: 0x000A5E30 File Offset: 0x000A4030
		protected virtual void HashCore(ReadOnlySpan<byte> source)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(source.Length);
			try
			{
				source.CopyTo(array);
				this.HashCore(array, 0, source.Length);
			}
			finally
			{
				Array.Clear(array, 0, source.Length);
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000A5E98 File Offset: 0x000A4098
		protected virtual bool TryHashFinal(Span<byte> destination, out int bytesWritten)
		{
			int num = this.HashSizeValue / 8;
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			byte[] array = this.HashFinal();
			if (array.Length == num)
			{
				new ReadOnlySpan<byte>(array).CopyTo(destination);
				bytesWritten = array.Length;
				return true;
			}
			throw new InvalidOperationException("The algorithm's implementation is incorrect.");
		}

		// Token: 0x04001FDF RID: 8159
		private bool _disposed;

		// Token: 0x04001FE0 RID: 8160
		protected int HashSizeValue;

		// Token: 0x04001FE1 RID: 8161
		protected internal byte[] HashValue;

		// Token: 0x04001FE2 RID: 8162
		protected int State;
	}
}
