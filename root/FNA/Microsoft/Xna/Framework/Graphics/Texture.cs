using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000CA RID: 202
	public abstract class Texture : GraphicsResource
	{
		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00032437 File Offset: 0x00030637
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x0003243F File Offset: 0x0003063F
		public SurfaceFormat Format
		{
			[CompilerGenerated]
			get
			{
				return this.<Format>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Format>k__BackingField = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00032448 File Offset: 0x00030648
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x00032450 File Offset: 0x00030650
		public int LevelCount
		{
			[CompilerGenerated]
			get
			{
				return this.<LevelCount>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<LevelCount>k__BackingField = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x00032459 File Offset: 0x00030659
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x00032461 File Offset: 0x00030661
		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				if (value == base.Name)
				{
					return;
				}
				base.Name = value;
				if (value == null)
				{
					value = string.Empty;
				}
				FNA3D.FNA3D_SetTextureName(base.GraphicsDevice.GLDevice, this.texture, value);
			}
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x0003249C File Offset: 0x0003069C
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				base.GraphicsDevice.Textures.RemoveDisposedTexture(this);
				base.GraphicsDevice.VertexTextures.RemoveDisposedTexture(this);
				IntPtr intPtr = Interlocked.Exchange(ref this.texture, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeTexture(base.GraphicsDevice.GLDevice, intPtr);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00009E6B File Offset: 0x0000806B
		protected internal override void GraphicsDeviceResetting()
		{
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x0003250C File Offset: 0x0003070C
		public static int GetBlockSizeSquaredEXT(SurfaceFormat format)
		{
			switch (format)
			{
			case SurfaceFormat.Color:
			case SurfaceFormat.Bgr565:
			case SurfaceFormat.Bgra5551:
			case SurfaceFormat.Bgra4444:
			case SurfaceFormat.NormalizedByte2:
			case SurfaceFormat.NormalizedByte4:
			case SurfaceFormat.Rgba1010102:
			case SurfaceFormat.Rg32:
			case SurfaceFormat.Rgba64:
			case SurfaceFormat.Alpha8:
			case SurfaceFormat.Single:
			case SurfaceFormat.Vector2:
			case SurfaceFormat.Vector4:
			case SurfaceFormat.HalfSingle:
			case SurfaceFormat.HalfVector2:
			case SurfaceFormat.HalfVector4:
			case SurfaceFormat.HdrBlendable:
			case SurfaceFormat.ColorBgraEXT:
			case SurfaceFormat.ColorSrgbEXT:
			case SurfaceFormat.ByteEXT:
			case SurfaceFormat.UShortEXT:
				return 1;
			case SurfaceFormat.Dxt1:
			case SurfaceFormat.Dxt3:
			case SurfaceFormat.Dxt5:
			case SurfaceFormat.Dxt5SrgbEXT:
			case SurfaceFormat.Bc7EXT:
			case SurfaceFormat.Bc7SrgbEXT:
				return 16;
			default:
				throw new ArgumentException("Should be a value defined in SurfaceFormat", "Format");
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000325A4 File Offset: 0x000307A4
		public static int GetFormatSizeEXT(SurfaceFormat format)
		{
			switch (format)
			{
			case SurfaceFormat.Color:
			case SurfaceFormat.NormalizedByte4:
			case SurfaceFormat.Rgba1010102:
			case SurfaceFormat.Rg32:
			case SurfaceFormat.Single:
			case SurfaceFormat.HalfVector2:
			case SurfaceFormat.ColorBgraEXT:
			case SurfaceFormat.ColorSrgbEXT:
				return 4;
			case SurfaceFormat.Bgr565:
			case SurfaceFormat.Bgra5551:
			case SurfaceFormat.Bgra4444:
			case SurfaceFormat.NormalizedByte2:
			case SurfaceFormat.HalfSingle:
			case SurfaceFormat.UShortEXT:
				return 2;
			case SurfaceFormat.Dxt1:
				return 8;
			case SurfaceFormat.Dxt3:
			case SurfaceFormat.Dxt5:
			case SurfaceFormat.Dxt5SrgbEXT:
			case SurfaceFormat.Bc7EXT:
			case SurfaceFormat.Bc7SrgbEXT:
				return 16;
			case SurfaceFormat.Rgba64:
			case SurfaceFormat.Vector2:
			case SurfaceFormat.HalfVector4:
			case SurfaceFormat.HdrBlendable:
				return 8;
			case SurfaceFormat.Alpha8:
			case SurfaceFormat.ByteEXT:
				return 1;
			case SurfaceFormat.Vector4:
				return 16;
			default:
				throw new ArgumentException("Should be a value defined in SurfaceFormat", "Format");
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00032644 File Offset: 0x00030844
		internal static int GetPixelStoreAlignment(SurfaceFormat format)
		{
			return Math.Min(8, Texture.GetFormatSizeEXT(format));
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00032652 File Offset: 0x00030852
		internal static void ValidateGetDataFormat(SurfaceFormat format, int elementSizeInBytes)
		{
			if (Texture.GetFormatSizeEXT(format) % elementSizeInBytes != 0)
			{
				throw new ArgumentException("The type you are using for T in this method is an invalid size for this resource");
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0003266C File Offset: 0x0003086C
		internal static int CalculateMipLevels(int width, int height = 0, int depth = 0)
		{
			int num = 1;
			int i = Math.Max(Math.Max(width, height), depth);
			while (i > 1)
			{
				i /= 2;
				num++;
			}
			return num;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00032698 File Offset: 0x00030898
		internal static int CalculateDDSLevelSize(int width, int height, SurfaceFormat format)
		{
			if (format == SurfaceFormat.Color || format == SurfaceFormat.ColorBgraEXT)
			{
				return (width * 32 + 7) / 8 * height;
			}
			if (format == SurfaceFormat.HalfVector4)
			{
				return (width * 64 + 7) / 8 * height;
			}
			if (format == SurfaceFormat.Vector4)
			{
				return (width * 128 + 7) / 8 * height;
			}
			int num = 16;
			if (format == SurfaceFormat.Dxt1)
			{
				num = 8;
			}
			width = Math.Max(width, 1);
			height = Math.Max(height, 1);
			return (width + 3) / 4 * ((height + 3) / 4) * num;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00032704 File Offset: 0x00030904
		internal static void ParseDDS(BinaryReader reader, out SurfaceFormat format, out int width, out int height, out int levels, out bool isCube)
		{
			if (reader.ReadUInt32() != 542327876U)
			{
				throw new NotSupportedException("Not a DDS!");
			}
			if (reader.ReadUInt32() != 124U)
			{
				throw new NotSupportedException("Invalid DDS header!");
			}
			uint num = reader.ReadUInt32();
			if ((num & 6U) != 6U)
			{
				throw new NotSupportedException("Invalid DDS flags!");
			}
			if ((num & 524296U) == 524296U)
			{
				throw new NotSupportedException("Invalid DDS flags!");
			}
			height = reader.ReadInt32();
			width = reader.ReadInt32();
			reader.ReadUInt32();
			reader.ReadUInt32();
			levels = reader.ReadInt32();
			reader.ReadBytes(44);
			if (reader.ReadUInt32() != 32U)
			{
				throw new NotSupportedException("Bogus PIXFMTSIZE!");
			}
			uint num2 = reader.ReadUInt32();
			uint num3 = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			uint num5 = reader.ReadUInt32();
			uint num6 = reader.ReadUInt32();
			uint num7 = reader.ReadUInt32();
			uint num8 = reader.ReadUInt32();
			uint num9 = reader.ReadUInt32();
			if ((num9 & 4096U) == 0U)
			{
				throw new NotSupportedException("Not a texture!");
			}
			isCube = false;
			uint num10 = reader.ReadUInt32();
			if (num10 != 0U)
			{
				if ((num10 & 512U) != 512U)
				{
					throw new NotSupportedException("Invalid caps2!");
				}
				isCube = true;
			}
			reader.ReadUInt32();
			reader.ReadUInt32();
			reader.ReadUInt32();
			if ((num9 & 4194304U) != 4194304U)
			{
				levels = 1;
			}
			if ((num2 & 4U) == 4U)
			{
				if (num3 <= 808540228U)
				{
					if (num3 == 113U)
					{
						format = SurfaceFormat.HalfVector4;
						return;
					}
					if (num3 == 116U)
					{
						format = SurfaceFormat.Vector4;
						return;
					}
					if (num3 == 808540228U)
					{
						uint num11 = reader.ReadUInt32();
						if (num11 <= 71U)
						{
							if (num11 == 2U)
							{
								format = SurfaceFormat.Vector4;
								goto IL_0215;
							}
							if (num11 == 10U)
							{
								format = SurfaceFormat.HalfVector4;
								goto IL_0215;
							}
							if (num11 == 71U)
							{
								format = SurfaceFormat.Dxt1;
								goto IL_0215;
							}
						}
						else if (num11 <= 77U)
						{
							if (num11 == 74U)
							{
								format = SurfaceFormat.Dxt3;
								goto IL_0215;
							}
							if (num11 == 77U)
							{
								format = SurfaceFormat.Dxt5;
								goto IL_0215;
							}
						}
						else
						{
							if (num11 == 98U)
							{
								format = SurfaceFormat.Bc7EXT;
								goto IL_0215;
							}
							if (num11 == 99U)
							{
								format = SurfaceFormat.Bc7SrgbEXT;
								goto IL_0215;
							}
						}
						throw new NotSupportedException("Unsupported DDS texture format");
						IL_0215:
						uint num12 = reader.ReadUInt32();
						if (num12 <= 1U)
						{
							throw new NotSupportedException("Unsupported DDS texture format");
						}
						reader.ReadUInt32();
						if (reader.ReadUInt32() > 1U)
						{
							throw new NotSupportedException("Unsupported DDS texture format");
						}
						reader.ReadUInt32();
						return;
					}
				}
				else
				{
					if (num3 == 827611204U)
					{
						format = SurfaceFormat.Dxt1;
						return;
					}
					if (num3 == 861165636U)
					{
						format = SurfaceFormat.Dxt3;
						return;
					}
					if (num3 == 894720068U)
					{
						format = SurfaceFormat.Dxt5;
						return;
					}
				}
				throw new NotSupportedException("Unsupported DDS texture format");
			}
			if ((num2 & 64U) != 64U)
			{
				throw new NotSupportedException("Unsupported DDS texture format");
			}
			if (num4 != 32U)
			{
				throw new NotSupportedException("Unsupported DDS texture format: Alpha channel required");
			}
			bool flag = num5 == 16711680U && num6 == 65280U && num7 == 255U && num8 == 4278190080U;
			bool flag2 = num5 == 255U && num6 == 65280U && num7 == 16711680U && num8 == 4278190080U;
			if (flag)
			{
				format = SurfaceFormat.ColorBgraEXT;
				return;
			}
			if (flag2)
			{
				format = SurfaceFormat.Color;
				return;
			}
			throw new NotSupportedException("Unsupported DDS texture format: Only RGBA and BGRA are supported");
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000329FA File Offset: 0x00030BFA
		protected Texture()
		{
		}

		// Token: 0x04000A35 RID: 2613
		[CompilerGenerated]
		private SurfaceFormat <Format>k__BackingField;

		// Token: 0x04000A36 RID: 2614
		[CompilerGenerated]
		private int <LevelCount>k__BackingField;

		// Token: 0x04000A37 RID: 2615
		internal IntPtr texture;
	}
}
