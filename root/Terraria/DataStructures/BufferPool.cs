using System;
using System.Collections.Generic;

namespace Terraria.DataStructures
{
	// Token: 0x0200058D RID: 1421
	public static class BufferPool
	{
		// Token: 0x06003839 RID: 14393 RVA: 0x00631434 File Offset: 0x0062F634
		public static CachedBuffer Request(int size)
		{
			object obj = BufferPool.bufferLock;
			CachedBuffer cachedBuffer;
			lock (obj)
			{
				if (size <= 32)
				{
					if (BufferPool.SmallBufferQueue.Count == 0)
					{
						cachedBuffer = new CachedBuffer(new byte[32]);
					}
					else
					{
						cachedBuffer = BufferPool.SmallBufferQueue.Dequeue().Activate();
					}
				}
				else if (size <= 256)
				{
					if (BufferPool.MediumBufferQueue.Count == 0)
					{
						cachedBuffer = new CachedBuffer(new byte[256]);
					}
					else
					{
						cachedBuffer = BufferPool.MediumBufferQueue.Dequeue().Activate();
					}
				}
				else if (size <= 16384)
				{
					if (BufferPool.LargeBufferQueue.Count == 0)
					{
						cachedBuffer = new CachedBuffer(new byte[16384]);
					}
					else
					{
						cachedBuffer = BufferPool.LargeBufferQueue.Dequeue().Activate();
					}
				}
				else if (size <= 65536)
				{
					if (BufferPool.HugeBufferQueue.Count == 0)
					{
						cachedBuffer = new CachedBuffer(new byte[65536]);
					}
					else
					{
						cachedBuffer = BufferPool.HugeBufferQueue.Dequeue().Activate();
					}
				}
				else
				{
					cachedBuffer = new CachedBuffer(new byte[size]);
				}
			}
			return cachedBuffer;
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x00631560 File Offset: 0x0062F760
		public static CachedBuffer Request(byte[] data, int offset, int size)
		{
			CachedBuffer cachedBuffer = BufferPool.Request(size);
			Buffer.BlockCopy(data, offset, cachedBuffer.Data, 0, size);
			return cachedBuffer;
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x00631584 File Offset: 0x0062F784
		public static void Recycle(CachedBuffer buffer)
		{
			int length = buffer.Length;
			object obj = BufferPool.bufferLock;
			lock (obj)
			{
				if (length <= 32)
				{
					BufferPool.SmallBufferQueue.Enqueue(buffer);
				}
				else if (length <= 256)
				{
					BufferPool.MediumBufferQueue.Enqueue(buffer);
				}
				else if (length <= 16384)
				{
					BufferPool.LargeBufferQueue.Enqueue(buffer);
				}
				else if (length <= 65536)
				{
					BufferPool.HugeBufferQueue.Enqueue(buffer);
				}
			}
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x00631614 File Offset: 0x0062F814
		public static void PrintBufferSizes()
		{
			object obj = BufferPool.bufferLock;
			lock (obj)
			{
				Console.WriteLine("SmallBufferQueue.Count: " + BufferPool.SmallBufferQueue.Count);
				Console.WriteLine("MediumBufferQueue.Count: " + BufferPool.MediumBufferQueue.Count);
				Console.WriteLine("LargeBufferQueue.Count: " + BufferPool.LargeBufferQueue.Count);
				Console.WriteLine("HugeBufferQueue.Count: " + BufferPool.HugeBufferQueue.Count);
				Console.WriteLine("");
			}
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x006316D0 File Offset: 0x0062F8D0
		// Note: this type is marked as 'beforefieldinit'.
		static BufferPool()
		{
		}

		// Token: 0x04005C5D RID: 23645
		private const int SMALL_BUFFER_SIZE = 32;

		// Token: 0x04005C5E RID: 23646
		private const int MEDIUM_BUFFER_SIZE = 256;

		// Token: 0x04005C5F RID: 23647
		private const int LARGE_BUFFER_SIZE = 16384;

		// Token: 0x04005C60 RID: 23648
		private const int HUGE_BUFFER_SIZE = 65536;

		// Token: 0x04005C61 RID: 23649
		private static object bufferLock = new object();

		// Token: 0x04005C62 RID: 23650
		private static Queue<CachedBuffer> SmallBufferQueue = new Queue<CachedBuffer>();

		// Token: 0x04005C63 RID: 23651
		private static Queue<CachedBuffer> MediumBufferQueue = new Queue<CachedBuffer>();

		// Token: 0x04005C64 RID: 23652
		private static Queue<CachedBuffer> LargeBufferQueue = new Queue<CachedBuffer>();

		// Token: 0x04005C65 RID: 23653
		private static Queue<CachedBuffer> HugeBufferQueue = new Queue<CachedBuffer>();
	}
}
