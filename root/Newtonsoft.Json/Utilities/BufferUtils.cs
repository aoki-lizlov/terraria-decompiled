using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000068 RID: 104
	internal static class BufferUtils
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00015981 File Offset: 0x00013B81
		public static char[] RentBuffer(IArrayPool<char> bufferPool, int minSize)
		{
			if (bufferPool == null)
			{
				return new char[minSize];
			}
			return bufferPool.Rent(minSize);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00015994 File Offset: 0x00013B94
		public static void ReturnBuffer(IArrayPool<char> bufferPool, char[] buffer)
		{
			if (bufferPool != null)
			{
				bufferPool.Return(buffer);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000159A0 File Offset: 0x00013BA0
		public static char[] EnsureBufferSize(IArrayPool<char> bufferPool, int size, char[] buffer)
		{
			if (bufferPool == null)
			{
				return new char[size];
			}
			if (buffer != null)
			{
				bufferPool.Return(buffer);
			}
			return bufferPool.Rent(size);
		}
	}
}
