using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000D5 RID: 213
	public class VertexBuffer : GraphicsResource
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00034107 File Offset: 0x00032307
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x0003410F File Offset: 0x0003230F
		public BufferUsage BufferUsage
		{
			[CompilerGenerated]
			get
			{
				return this.<BufferUsage>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<BufferUsage>k__BackingField = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00034118 File Offset: 0x00032318
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x00034120 File Offset: 0x00032320
		public int VertexCount
		{
			[CompilerGenerated]
			get
			{
				return this.<VertexCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VertexCount>k__BackingField = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00034129 File Offset: 0x00032329
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x00034131 File Offset: 0x00032331
		public VertexDeclaration VertexDeclaration
		{
			[CompilerGenerated]
			get
			{
				return this.<VertexDeclaration>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VertexDeclaration>k__BackingField = value;
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0003413A File Offset: 0x0003233A
		public VertexBuffer(GraphicsDevice graphicsDevice, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage bufferUsage)
			: this(graphicsDevice, vertexDeclaration, vertexCount, bufferUsage, false)
		{
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00034148 File Offset: 0x00032348
		public VertexBuffer(GraphicsDevice graphicsDevice, Type type, int vertexCount, BufferUsage bufferUsage)
			: this(graphicsDevice, VertexDeclaration.FromType(type), vertexCount, bufferUsage, false)
		{
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x0003415C File Offset: 0x0003235C
		protected VertexBuffer(GraphicsDevice graphicsDevice, VertexDeclaration vertexDeclaration, int vertexCount, BufferUsage bufferUsage, bool dynamic)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			base.GraphicsDevice = graphicsDevice;
			this.VertexDeclaration = vertexDeclaration;
			this.VertexCount = vertexCount;
			this.BufferUsage = bufferUsage;
			if (vertexDeclaration.GraphicsDevice != graphicsDevice)
			{
				vertexDeclaration.GraphicsDevice = graphicsDevice;
			}
			this.buffer = FNA3D.FNA3D_GenVertexBuffer(base.GraphicsDevice.GLDevice, (dynamic > false) ? 1 : 0, bufferUsage, this.VertexCount * this.VertexDeclaration.VertexStride);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x000341DC File Offset: 0x000323DC
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				IntPtr intPtr = Interlocked.Exchange(ref this.buffer, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeVertexBuffer(base.GraphicsDevice.GLDevice, intPtr);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00034227 File Offset: 0x00032427
		public void GetData<T>(T[] data) where T : struct
		{
			this.GetData<T>(0, data, 0, data.Length, MarshalHelper.SizeOf<T>());
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x0003423A File Offset: 0x0003243A
		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			this.GetData<T>(0, data, startIndex, elementCount, MarshalHelper.SizeOf<T>());
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0003424C File Offset: 0x0003244C
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < startIndex + elementCount)
			{
				throw new ArgumentOutOfRangeException("elementCount", "This parameter must be a valid index within the array.");
			}
			if (this.BufferUsage == BufferUsage.WriteOnly)
			{
				throw new NotSupportedException("Calling GetData on a resource that was created with BufferUsage.WriteOnly is not supported.");
			}
			int num = MarshalHelper.SizeOf<T>();
			if (vertexStride == 0)
			{
				vertexStride = num;
			}
			else if (vertexStride < num)
			{
				throw new ArgumentOutOfRangeException("vertexStride", "The vertex stride is too small for the type of data requested. This is not allowed.");
			}
			if (elementCount > 1 && elementCount * vertexStride > this.VertexCount * this.VertexDeclaration.VertexStride)
			{
				throw new InvalidOperationException("The array is not the correct size for the amount of data requested.");
			}
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_GetVertexBufferData(base.GraphicsDevice.GLDevice, this.buffer, offsetInBytes, gchandle.AddrOfPinnedObject() + startIndex * num, elementCount, num, vertexStride);
			gchandle.Free();
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0003431B File Offset: 0x0003251B
		public void SetData<T>(T[] data) where T : struct
		{
			this.SetData<T>(0, data, 0, data.Length, MarshalHelper.SizeOf<T>());
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0003432E File Offset: 0x0003252E
		public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			this.SetData<T>(0, data, startIndex, elementCount, MarshalHelper.SizeOf<T>());
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00034340 File Offset: 0x00032540
		public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
		{
			int num = MarshalHelper.SizeOf<T>();
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetVertexBufferData(base.GraphicsDevice.GLDevice, this.buffer, offsetInBytes, gchandle.AddrOfPinnedObject() + startIndex * num, elementCount, num, vertexStride, SetDataOptions.None);
			gchandle.Free();
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0003438E File Offset: 0x0003258E
		public void SetDataPointerEXT(int offsetInBytes, IntPtr data, int dataLength, SetDataOptions options)
		{
			FNA3D.FNA3D_SetVertexBufferData(base.GraphicsDevice.GLDevice, this.buffer, offsetInBytes, data, dataLength, 1, 1, options);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000343B0 File Offset: 0x000325B0
		[Conditional("DEBUG")]
		internal void ErrorCheck<T>(T[] data, int startIndex, int elementCount, int vertexStride) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (startIndex + elementCount > data.Length || elementCount <= 0)
			{
				throw new InvalidOperationException("The array specified in the data parameter is not the correct size for the amount of data requested.");
			}
			if (elementCount > 1 && elementCount * vertexStride > this.VertexCount * this.VertexDeclaration.VertexStride)
			{
				throw new InvalidOperationException("The vertex stride is larger than the vertex buffer.");
			}
			int num = MarshalHelper.SizeOf<T>();
			if (vertexStride == 0)
			{
				vertexStride = num;
			}
			if (vertexStride < num)
			{
				throw new ArgumentOutOfRangeException("The vertex stride must be greater than or equal to the size of the specified data (" + num.ToString() + ").");
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00009E6B File Offset: 0x0000806B
		protected internal override void GraphicsDeviceResetting()
		{
		}

		// Token: 0x04000A4D RID: 2637
		[CompilerGenerated]
		private BufferUsage <BufferUsage>k__BackingField;

		// Token: 0x04000A4E RID: 2638
		[CompilerGenerated]
		private int <VertexCount>k__BackingField;

		// Token: 0x04000A4F RID: 2639
		[CompilerGenerated]
		private VertexDeclaration <VertexDeclaration>k__BackingField;

		// Token: 0x04000A50 RID: 2640
		internal IntPtr buffer;
	}
}
