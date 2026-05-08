using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Security.Cryptography
{
	// Token: 0x0200044E RID: 1102
	[ComVisible(true)]
	public class ToBase64Transform : ICryptoTransform, IDisposable
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x00019B62 File Offset: 0x00017D62
		public int InputBlockSize
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x0001A197 File Offset: 0x00018397
		public int OutputBlockSize
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x0000408A File Offset: 0x0000228A
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual bool CanReuseTransform
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000A63A4 File Offset: 0x000A45A4
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, 3, array, 0);
			byte[] bytes = Encoding.ASCII.GetBytes(array);
			if (bytes.Length != 4)
			{
				throw new CryptographicException(Environment.GetResourceString("Length of the data to encrypt is invalid."));
			}
			Buffer.BlockCopy(bytes, 0, outputBuffer, outputOffset, bytes.Length);
			return bytes.Length;
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x000A6450 File Offset: 0x000A4650
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (inputBuffer == null)
			{
				throw new ArgumentNullException("inputBuffer");
			}
			if (inputOffset < 0)
			{
				throw new ArgumentOutOfRangeException("inputOffset", Environment.GetResourceString("Non-negative number required."));
			}
			if (inputCount < 0 || inputCount > inputBuffer.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Value was invalid."));
			}
			if (inputBuffer.Length - inputCount < inputOffset)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			if (inputCount == 0)
			{
				return EmptyArray<byte>.Value;
			}
			char[] array = new char[4];
			Convert.ToBase64CharArray(inputBuffer, inputOffset, inputCount, array, 0);
			return Encoding.ASCII.GetBytes(array);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000A64DC File Offset: 0x000A46DC
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000A64E4 File Offset: 0x000A46E4
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x00004088 File Offset: 0x00002288
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000A64F4 File Offset: 0x000A46F4
		~ToBase64Transform()
		{
			this.Dispose(false);
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000025BE File Offset: 0x000007BE
		public ToBase64Transform()
		{
		}
	}
}
