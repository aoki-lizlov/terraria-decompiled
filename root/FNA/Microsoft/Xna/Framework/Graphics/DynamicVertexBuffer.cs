using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000D1 RID: 209
	public class DynamicVertexBuffer : VertexBuffer
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x000136EB File Offset: 0x000118EB
		public bool IsContentLost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06001534 RID: 5428 RVA: 0x00033CC0 File Offset: 0x00031EC0
		// (remove) Token: 0x06001535 RID: 5429 RVA: 0x00033CF8 File Offset: 0x00031EF8
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

		// Token: 0x06001536 RID: 5430 RVA: 0x00033D2D File Offset: 0x00031F2D
		public DynamicVertexBuffer(GraphicsDevice graphicsDevice, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage bufferUsage)
			: base(graphicsDevice, vertexDeclaration, vertexCount, bufferUsage, true)
		{
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00033D3B File Offset: 0x00031F3B
		public DynamicVertexBuffer(GraphicsDevice graphicsDevice, Type type, int vertexCount, BufferUsage bufferUsage)
			: base(graphicsDevice, VertexDeclaration.FromType(type), vertexCount, bufferUsage, true)
		{
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00033D50 File Offset: 0x00031F50
		public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride, SetDataOptions options) where T : struct
		{
			int num = MarshalHelper.SizeOf<T>();
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetVertexBufferData(base.GraphicsDevice.GLDevice, this.buffer, offsetInBytes, gchandle.AddrOfPinnedObject() + startIndex * num, elementCount, num, vertexStride, options);
			gchandle.Free();
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00033DA0 File Offset: 0x00031FA0
		public void SetData<T>(T[] data, int startIndex, int elementCount, SetDataOptions options) where T : struct
		{
			int num = MarshalHelper.SizeOf<T>();
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetVertexBufferData(base.GraphicsDevice.GLDevice, this.buffer, 0, gchandle.AddrOfPinnedObject() + startIndex * num, elementCount, num, num, options);
			gchandle.Free();
		}

		// Token: 0x04000A45 RID: 2629
		[CompilerGenerated]
		private EventHandler<EventArgs> ContentLost;
	}
}
