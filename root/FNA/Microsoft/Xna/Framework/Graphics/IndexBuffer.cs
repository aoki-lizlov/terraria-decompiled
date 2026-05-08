using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000D2 RID: 210
	public class IndexBuffer : GraphicsResource
	{
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x00033DED File Offset: 0x00031FED
		// (set) Token: 0x0600153B RID: 5435 RVA: 0x00033DF5 File Offset: 0x00031FF5
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

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00033DFE File Offset: 0x00031FFE
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x00033E06 File Offset: 0x00032006
		public int IndexCount
		{
			[CompilerGenerated]
			get
			{
				return this.<IndexCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IndexCount>k__BackingField = value;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x00033E0F File Offset: 0x0003200F
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x00033E17 File Offset: 0x00032017
		public IndexElementSize IndexElementSize
		{
			[CompilerGenerated]
			get
			{
				return this.<IndexElementSize>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IndexElementSize>k__BackingField = value;
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00033E20 File Offset: 0x00032020
		public IndexBuffer(GraphicsDevice graphicsDevice, IndexElementSize indexElementSize, int indexCount, BufferUsage bufferUsage)
			: this(graphicsDevice, indexElementSize, indexCount, bufferUsage, false)
		{
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00033E2E File Offset: 0x0003202E
		public IndexBuffer(GraphicsDevice graphicsDevice, Type indexType, int indexCount, BufferUsage usage)
			: this(graphicsDevice, IndexBuffer.SizeForType(graphicsDevice, indexType), indexCount, usage, false)
		{
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00033E42 File Offset: 0x00032042
		protected IndexBuffer(GraphicsDevice graphicsDevice, Type indexType, int indexCount, BufferUsage usage, bool dynamic)
			: this(graphicsDevice, IndexBuffer.SizeForType(graphicsDevice, indexType), indexCount, usage, dynamic)
		{
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00033E58 File Offset: 0x00032058
		protected IndexBuffer(GraphicsDevice graphicsDevice, IndexElementSize indexElementSize, int indexCount, BufferUsage usage, bool dynamic)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			base.GraphicsDevice = graphicsDevice;
			this.IndexElementSize = indexElementSize;
			this.IndexCount = indexCount;
			this.BufferUsage = usage;
			int num = ((indexElementSize == IndexElementSize.ThirtyTwoBits) ? 4 : 2);
			this.buffer = FNA3D.FNA3D_GenIndexBuffer(base.GraphicsDevice.GLDevice, (dynamic > false) ? 1 : 0, usage, this.IndexCount * num);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00033EC8 File Offset: 0x000320C8
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				IntPtr intPtr = Interlocked.Exchange(ref this.buffer, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeIndexBuffer(base.GraphicsDevice.GLDevice, intPtr);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00033F13 File Offset: 0x00032113
		public void GetData<T>(T[] data) where T : struct
		{
			this.GetData<T>(0, data, 0, data.Length);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00033F21 File Offset: 0x00032121
		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			this.GetData<T>(0, data, startIndex, elementCount);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00033F30 File Offset: 0x00032130
		public void GetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < startIndex + elementCount)
			{
				throw new InvalidOperationException("The array specified in the data parameter is not the correct size for the amount of data requested.");
			}
			if (this.BufferUsage == BufferUsage.WriteOnly)
			{
				throw new NotSupportedException("This IndexBuffer was created with a usage type of BufferUsage.WriteOnly. Calling GetData on a resource that was created with BufferUsage.WriteOnly is not supported.");
			}
			int num = MarshalHelper.SizeOf<T>();
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_GetIndexBufferData(base.GraphicsDevice.GLDevice, this.buffer, offsetInBytes, gchandle.AddrOfPinnedObject() + startIndex * num, elementCount * num);
			gchandle.Free();
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00033FB4 File Offset: 0x000321B4
		public void SetData<T>(T[] data) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetIndexBufferData(base.GraphicsDevice.GLDevice, this.buffer, 0, gchandle.AddrOfPinnedObject(), data.Length * MarshalHelper.SizeOf<T>(), SetDataOptions.None);
			gchandle.Free();
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00033FF8 File Offset: 0x000321F8
		public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetIndexBufferData(base.GraphicsDevice.GLDevice, this.buffer, 0, gchandle.AddrOfPinnedObject() + startIndex * MarshalHelper.SizeOf<T>(), elementCount * MarshalHelper.SizeOf<T>(), SetDataOptions.None);
			gchandle.Free();
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00034048 File Offset: 0x00032248
		public void SetData<T>(int offsetInBytes, T[] data, int startIndex, int elementCount) where T : struct
		{
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetIndexBufferData(base.GraphicsDevice.GLDevice, this.buffer, offsetInBytes, gchandle.AddrOfPinnedObject() + startIndex * MarshalHelper.SizeOf<T>(), elementCount * MarshalHelper.SizeOf<T>(), SetDataOptions.None);
			gchandle.Free();
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00034097 File Offset: 0x00032297
		public void SetDataPointerEXT(int offsetInBytes, IntPtr data, int dataLength, SetDataOptions options)
		{
			FNA3D.FNA3D_SetIndexBufferData(base.GraphicsDevice.GLDevice, this.buffer, offsetInBytes, data, dataLength, options);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x000340B4 File Offset: 0x000322B4
		[Conditional("DEBUG")]
		internal void ErrorCheck<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (data.Length < startIndex + elementCount)
			{
				throw new InvalidOperationException("The array specified in the data parameter is not the correct size for the amount of data requested.");
			}
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00009E6B File Offset: 0x0000806B
		protected internal override void GraphicsDeviceResetting()
		{
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000340D8 File Offset: 0x000322D8
		private static IndexElementSize SizeForType(GraphicsDevice graphicsDevice, Type type)
		{
			int num = Marshal.SizeOf(type);
			if (num == 2)
			{
				return IndexElementSize.SixteenBits;
			}
			if (num == 4)
			{
				return IndexElementSize.ThirtyTwoBits;
			}
			throw new ArgumentOutOfRangeException("type", "Index buffers can only be created for types that are sixteen or thirty two bits in length");
		}

		// Token: 0x04000A46 RID: 2630
		[CompilerGenerated]
		private BufferUsage <BufferUsage>k__BackingField;

		// Token: 0x04000A47 RID: 2631
		[CompilerGenerated]
		private int <IndexCount>k__BackingField;

		// Token: 0x04000A48 RID: 2632
		[CompilerGenerated]
		private IndexElementSize <IndexElementSize>k__BackingField;

		// Token: 0x04000A49 RID: 2633
		internal IntPtr buffer;
	}
}
