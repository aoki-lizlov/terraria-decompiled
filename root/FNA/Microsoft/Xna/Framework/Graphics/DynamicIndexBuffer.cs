using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000D0 RID: 208
	public class DynamicIndexBuffer : IndexBuffer
	{
		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x000136EB File Offset: 0x000118EB
		public bool IsContentLost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600152D RID: 5421 RVA: 0x00033B94 File Offset: 0x00031D94
		// (remove) Token: 0x0600152E RID: 5422 RVA: 0x00033BCC File Offset: 0x00031DCC
		public event EventHandler<EventArgs> ContentLost
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = this.ContentLost;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.ContentLost, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = this.ContentLost;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref this.ContentLost, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00033C01 File Offset: 0x00031E01
		public DynamicIndexBuffer(GraphicsDevice graphicsDevice, IndexElementSize indexElementSize, int indexCount, BufferUsage usage)
			: base(graphicsDevice, indexElementSize, indexCount, usage, true)
		{
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00033C0F File Offset: 0x00031E0F
		public DynamicIndexBuffer(GraphicsDevice graphicsDevice, Type indexType, int indexCount, BufferUsage usage)
			: base(graphicsDevice, indexType, indexCount, usage, true)
		{
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00033C20 File Offset: 0x00031E20
		public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, SetDataOptions options) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetIndexBufferData(base.GraphicsDevice.GLDevice, this.buffer, offsetInBytes, gchandle.AddrOfPinnedObject() + startIndex * MarshalHelper.SizeOf<T>(), elementCount * MarshalHelper.SizeOf<T>(), options);
			gchandle.Free();
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00033C70 File Offset: 0x00031E70
		public void SetData<T>(T[] data, int startIndex, int elementCount, SetDataOptions options) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetIndexBufferData(base.GraphicsDevice.GLDevice, this.buffer, 0, gchandle.AddrOfPinnedObject() + startIndex * MarshalHelper.SizeOf<T>(), elementCount * MarshalHelper.SizeOf<T>(), options);
			gchandle.Free();
		}

		// Token: 0x04000A44 RID: 2628
		[CompilerGenerated]
		private EventHandler<EventArgs> ContentLost;
	}
}
