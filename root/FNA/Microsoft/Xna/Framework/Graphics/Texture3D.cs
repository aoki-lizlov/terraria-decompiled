using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000CC RID: 204
	public class Texture3D : Texture
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00033278 File Offset: 0x00031478
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00033280 File Offset: 0x00031480
		public int Width
		{
			[CompilerGenerated]
			get
			{
				return this.<Width>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Width>k__BackingField = value;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00033289 File Offset: 0x00031489
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x00033291 File Offset: 0x00031491
		public int Height
		{
			[CompilerGenerated]
			get
			{
				return this.<Height>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Height>k__BackingField = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x0003329A File Offset: 0x0003149A
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x000332A2 File Offset: 0x000314A2
		public int Depth
		{
			[CompilerGenerated]
			get
			{
				return this.<Depth>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Depth>k__BackingField = value;
			}
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x000332AC File Offset: 0x000314AC
		public Texture3D(GraphicsDevice graphicsDevice, int width, int height, int depth, bool mipMap, SurfaceFormat format)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			base.GraphicsDevice = graphicsDevice;
			this.Width = width;
			this.Height = height;
			this.Depth = depth;
			base.LevelCount = (mipMap ? Texture.CalculateMipLevels(width, height, 0) : 1);
			base.Format = format;
			this.texture = FNA3D.FNA3D_CreateTexture3D(base.GraphicsDevice.GLDevice, base.Format, this.Width, this.Height, this.Depth, base.LevelCount);
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0003333B File Offset: 0x0003153B
		public void SetData<T>(T[] data) where T : struct
		{
			this.SetData<T>(data, 0, data.Length);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00033348 File Offset: 0x00031548
		public void SetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			this.SetData<T>(0, 0, 0, this.Width, this.Height, 0, this.Depth, data, startIndex, elementCount);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00033374 File Offset: 0x00031574
		public void SetData<T>(int level, int left, int top, int right, int bottom, int front, int back, T[] data, int startIndex, int elementCount) where T : struct
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			int num = MarshalHelper.SizeOf<T>();
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_SetTextureData3D(base.GraphicsDevice.GLDevice, this.texture, left, top, front, right - left, bottom - top, back - front, level, gchandle.AddrOfPinnedObject() + startIndex * num, elementCount * num);
			gchandle.Free();
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000333E4 File Offset: 0x000315E4
		public void SetDataPointerEXT(int level, int left, int top, int right, int bottom, int front, int back, IntPtr data, int dataLength)
		{
			if (data == IntPtr.Zero)
			{
				throw new ArgumentNullException("data");
			}
			FNA3D.FNA3D_SetTextureData3D(base.GraphicsDevice.GLDevice, this.texture, left, top, front, right - left, bottom - top, back - front, level, data, dataLength);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x00033436 File Offset: 0x00031636
		public void GetData<T>(T[] data) where T : struct
		{
			this.GetData<T>(data, 0, data.Length);
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00033444 File Offset: 0x00031644
		public void GetData<T>(T[] data, int startIndex, int elementCount) where T : struct
		{
			this.GetData<T>(0, 0, 0, this.Width, this.Height, 0, this.Depth, data, startIndex, elementCount);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00033470 File Offset: 0x00031670
		public void GetData<T>(int level, int left, int top, int right, int bottom, int front, int back, T[] data, int startIndex, int elementCount) where T : struct
		{
			if (data == null || data.Length == 0)
			{
				throw new ArgumentException("data cannot be null");
			}
			if (data.Length < startIndex + elementCount)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"The data passed has a length of ",
					data.Length.ToString(),
					" but ",
					elementCount.ToString(),
					" pixels have been requested."
				}));
			}
			if (left < 0 || left >= right || top < 0 || top >= bottom || front < 0 || front >= back)
			{
				throw new ArgumentException("Neither box size nor box position can be negative");
			}
			int num = MarshalHelper.SizeOf<T>();
			Texture.ValidateGetDataFormat(base.Format, num);
			GCHandle gchandle = GCHandle.Alloc(data, GCHandleType.Pinned);
			FNA3D.FNA3D_GetTextureData3D(base.GraphicsDevice.GLDevice, this.texture, left, top, front, right - left, bottom - top, back - front, level, gchandle.AddrOfPinnedObject() + startIndex * num, elementCount * num);
			gchandle.Free();
		}

		// Token: 0x04000A3A RID: 2618
		[CompilerGenerated]
		private int <Width>k__BackingField;

		// Token: 0x04000A3B RID: 2619
		[CompilerGenerated]
		private int <Height>k__BackingField;

		// Token: 0x04000A3C RID: 2620
		[CompilerGenerated]
		private int <Depth>k__BackingField;
	}
}
