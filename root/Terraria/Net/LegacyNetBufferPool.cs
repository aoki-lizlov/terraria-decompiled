using System;
using System.Collections.Generic;

namespace Terraria.Net
{
	// Token: 0x0200016D RID: 365
	public class LegacyNetBufferPool
	{
		// Token: 0x06001DCC RID: 7628 RVA: 0x00502178 File Offset: 0x00500378
		public static byte[] RequestBuffer(int size)
		{
			object obj = LegacyNetBufferPool.bufferLock;
			byte[] array;
			lock (obj)
			{
				if (size <= 256)
				{
					if (LegacyNetBufferPool._smallBufferQueue.Count == 0)
					{
						LegacyNetBufferPool._smallBufferCount++;
						array = new byte[256];
					}
					else
					{
						array = LegacyNetBufferPool._smallBufferQueue.Dequeue();
					}
				}
				else if (size <= 1024)
				{
					if (LegacyNetBufferPool._mediumBufferQueue.Count == 0)
					{
						LegacyNetBufferPool._mediumBufferCount++;
						array = new byte[1024];
					}
					else
					{
						array = LegacyNetBufferPool._mediumBufferQueue.Dequeue();
					}
				}
				else if (size <= 16384)
				{
					if (LegacyNetBufferPool._largeBufferQueue.Count == 0)
					{
						LegacyNetBufferPool._largeBufferCount++;
						array = new byte[16384];
					}
					else
					{
						array = LegacyNetBufferPool._largeBufferQueue.Dequeue();
					}
				}
				else
				{
					LegacyNetBufferPool._customBufferCount++;
					array = new byte[size];
				}
			}
			return array;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x0050227C File Offset: 0x0050047C
		public static byte[] RequestBuffer(byte[] data, int offset, int size)
		{
			byte[] array = LegacyNetBufferPool.RequestBuffer(size);
			Buffer.BlockCopy(data, offset, array, 0, size);
			return array;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0050229C File Offset: 0x0050049C
		public static void ReturnBuffer(byte[] buffer)
		{
			int num = buffer.Length;
			object obj = LegacyNetBufferPool.bufferLock;
			lock (obj)
			{
				if (num <= 256)
				{
					LegacyNetBufferPool._smallBufferQueue.Enqueue(buffer);
				}
				else if (num <= 1024)
				{
					LegacyNetBufferPool._mediumBufferQueue.Enqueue(buffer);
				}
				else if (num <= 16384)
				{
					LegacyNetBufferPool._largeBufferQueue.Enqueue(buffer);
				}
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00502318 File Offset: 0x00500518
		public static void DisplayBufferSizes()
		{
			object obj = LegacyNetBufferPool.bufferLock;
			lock (obj)
			{
				Main.NewText(string.Concat(new object[]
				{
					"Small Buffers:  ",
					LegacyNetBufferPool._smallBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._smallBufferCount
				}), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText(string.Concat(new object[]
				{
					"Medium Buffers: ",
					LegacyNetBufferPool._mediumBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._mediumBufferCount
				}), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText(string.Concat(new object[]
				{
					"Large Buffers:  ",
					LegacyNetBufferPool._largeBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._largeBufferCount
				}), byte.MaxValue, byte.MaxValue, byte.MaxValue);
				Main.NewText("Custom Buffers: 0 queued of " + LegacyNetBufferPool._customBufferCount, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00502470 File Offset: 0x00500670
		public static void PrintBufferSizes()
		{
			object obj = LegacyNetBufferPool.bufferLock;
			lock (obj)
			{
				Console.WriteLine(string.Concat(new object[]
				{
					"Small Buffers:  ",
					LegacyNetBufferPool._smallBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._smallBufferCount
				}));
				Console.WriteLine(string.Concat(new object[]
				{
					"Medium Buffers: ",
					LegacyNetBufferPool._mediumBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._mediumBufferCount
				}));
				Console.WriteLine(string.Concat(new object[]
				{
					"Large Buffers:  ",
					LegacyNetBufferPool._largeBufferQueue.Count,
					" queued of ",
					LegacyNetBufferPool._largeBufferCount
				}));
				Console.WriteLine("Custom Buffers: 0 queued of " + LegacyNetBufferPool._customBufferCount);
				Console.WriteLine("");
			}
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0000357B File Offset: 0x0000177B
		public LegacyNetBufferPool()
		{
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0050258C File Offset: 0x0050078C
		// Note: this type is marked as 'beforefieldinit'.
		static LegacyNetBufferPool()
		{
		}

		// Token: 0x04001670 RID: 5744
		private const int SMALL_BUFFER_SIZE = 256;

		// Token: 0x04001671 RID: 5745
		private const int MEDIUM_BUFFER_SIZE = 1024;

		// Token: 0x04001672 RID: 5746
		private const int LARGE_BUFFER_SIZE = 16384;

		// Token: 0x04001673 RID: 5747
		private static object bufferLock = new object();

		// Token: 0x04001674 RID: 5748
		private static Queue<byte[]> _smallBufferQueue = new Queue<byte[]>();

		// Token: 0x04001675 RID: 5749
		private static Queue<byte[]> _mediumBufferQueue = new Queue<byte[]>();

		// Token: 0x04001676 RID: 5750
		private static Queue<byte[]> _largeBufferQueue = new Queue<byte[]>();

		// Token: 0x04001677 RID: 5751
		private static int _smallBufferCount;

		// Token: 0x04001678 RID: 5752
		private static int _mediumBufferCount;

		// Token: 0x04001679 RID: 5753
		private static int _largeBufferCount;

		// Token: 0x0400167A RID: 5754
		private static int _customBufferCount;
	}
}
