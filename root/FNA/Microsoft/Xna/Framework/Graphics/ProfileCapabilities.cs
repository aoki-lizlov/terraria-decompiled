using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000B0 RID: 176
	internal class ProfileCapabilities
	{
		// Token: 0x06001429 RID: 5161 RVA: 0x0002EF58 File Offset: 0x0002D158
		static ProfileCapabilities()
		{
			ProfileCapabilities.Reach.Profile = GraphicsProfile.Reach;
			ProfileCapabilities.Reach.VertexShaderVersion = 512U;
			ProfileCapabilities.Reach.PixelShaderVersion = 512U;
			ProfileCapabilities.Reach.OcclusionQuery = false;
			ProfileCapabilities.Reach.GetBackBufferData = false;
			ProfileCapabilities.Reach.SeparateAlphaBlend = false;
			ProfileCapabilities.Reach.DestBlendSrcAlphaSat = false;
			ProfileCapabilities.Reach.MinMaxSrcDestBlend = false;
			ProfileCapabilities.Reach.MaxPrimitiveCount = 65535;
			ProfileCapabilities.Reach.IndexElementSize32 = false;
			ProfileCapabilities.Reach.MaxVertexStreams = 16;
			ProfileCapabilities.Reach.MaxStreamStride = 255;
			ProfileCapabilities.Reach.MaxVertexBufferSize = 67108863;
			ProfileCapabilities.Reach.MaxIndexBufferSize = 67108863;
			ProfileCapabilities.Reach.MaxTextureSize = 2048;
			ProfileCapabilities.Reach.MaxCubeSize = 512;
			ProfileCapabilities.Reach.MaxVolumeExtent = 0;
			ProfileCapabilities.Reach.MaxTextureAspectRatio = 2048;
			ProfileCapabilities.Reach.MaxSamplers = 16;
			ProfileCapabilities.Reach.MaxVertexSamplers = 0;
			ProfileCapabilities.Reach.MaxRenderTargets = 1;
			ProfileCapabilities.Reach.NonPow2Unconditional = false;
			ProfileCapabilities.Reach.NonPow2Cube = false;
			ProfileCapabilities.Reach.NonPow2Volume = false;
			ProfileCapabilities.Reach.ValidTextureFormats = new List<SurfaceFormat>
			{
				SurfaceFormat.Color,
				SurfaceFormat.Bgr565,
				SurfaceFormat.Bgra5551,
				SurfaceFormat.Bgra4444,
				SurfaceFormat.Dxt1,
				SurfaceFormat.Dxt3,
				SurfaceFormat.Dxt5,
				SurfaceFormat.NormalizedByte2,
				SurfaceFormat.NormalizedByte4
			};
			ProfileCapabilities.Reach.ValidCubeFormats = new List<SurfaceFormat>
			{
				SurfaceFormat.Color,
				SurfaceFormat.Bgr565,
				SurfaceFormat.Bgra5551,
				SurfaceFormat.Bgra4444,
				SurfaceFormat.Dxt1,
				SurfaceFormat.Dxt3,
				SurfaceFormat.Dxt5
			};
			ProfileCapabilities.Reach.ValidVolumeFormats = new List<SurfaceFormat>();
			ProfileCapabilities.Reach.ValidVertexTextureFormats = new List<SurfaceFormat>();
			ProfileCapabilities.Reach.InvalidFilterFormats = new List<SurfaceFormat>();
			ProfileCapabilities.Reach.InvalidBlendFormats = new List<SurfaceFormat>();
			ProfileCapabilities.Reach.ValidDepthFormats = new List<DepthFormat>
			{
				DepthFormat.Depth16,
				DepthFormat.Depth24,
				DepthFormat.Depth24Stencil8
			};
			ProfileCapabilities.Reach.ValidVertexFormats = new List<VertexElementFormat>
			{
				VertexElementFormat.Color,
				VertexElementFormat.Single,
				VertexElementFormat.Vector2,
				VertexElementFormat.Vector3,
				VertexElementFormat.Vector4,
				VertexElementFormat.Byte4,
				VertexElementFormat.Short2,
				VertexElementFormat.Short4,
				VertexElementFormat.NormalizedShort2,
				VertexElementFormat.NormalizedShort4
			};
			ProfileCapabilities.HiDef = new ProfileCapabilities();
			ProfileCapabilities.HiDef.Profile = GraphicsProfile.HiDef;
			ProfileCapabilities.HiDef.VertexShaderVersion = 768U;
			ProfileCapabilities.HiDef.PixelShaderVersion = 768U;
			ProfileCapabilities.HiDef.OcclusionQuery = true;
			ProfileCapabilities.HiDef.GetBackBufferData = true;
			ProfileCapabilities.HiDef.SeparateAlphaBlend = true;
			ProfileCapabilities.HiDef.DestBlendSrcAlphaSat = true;
			ProfileCapabilities.HiDef.MinMaxSrcDestBlend = true;
			ProfileCapabilities.HiDef.MaxPrimitiveCount = 1048575;
			ProfileCapabilities.HiDef.IndexElementSize32 = true;
			ProfileCapabilities.HiDef.MaxVertexStreams = 16;
			ProfileCapabilities.HiDef.MaxStreamStride = 255;
			ProfileCapabilities.HiDef.MaxVertexBufferSize = 67108863;
			ProfileCapabilities.HiDef.MaxIndexBufferSize = 67108863;
			ProfileCapabilities.HiDef.MaxTextureSize = 8192;
			ProfileCapabilities.HiDef.MaxCubeSize = 8192;
			ProfileCapabilities.HiDef.MaxVolumeExtent = 2048;
			ProfileCapabilities.HiDef.MaxTextureAspectRatio = 2048;
			ProfileCapabilities.HiDef.MaxSamplers = 16;
			ProfileCapabilities.HiDef.MaxVertexSamplers = 4;
			ProfileCapabilities.HiDef.MaxRenderTargets = 4;
			ProfileCapabilities.HiDef.NonPow2Unconditional = true;
			ProfileCapabilities.HiDef.NonPow2Cube = true;
			ProfileCapabilities.HiDef.NonPow2Volume = true;
			ProfileCapabilities.HiDef.ValidTextureFormats = new List<SurfaceFormat>
			{
				SurfaceFormat.Color,
				SurfaceFormat.Bgr565,
				SurfaceFormat.Bgra5551,
				SurfaceFormat.Bgra4444,
				SurfaceFormat.Dxt1,
				SurfaceFormat.Dxt3,
				SurfaceFormat.Dxt5,
				SurfaceFormat.NormalizedByte2,
				SurfaceFormat.NormalizedByte4,
				SurfaceFormat.Rgba1010102,
				SurfaceFormat.Rg32,
				SurfaceFormat.Rgba64,
				SurfaceFormat.Alpha8,
				SurfaceFormat.Single,
				SurfaceFormat.Vector2,
				SurfaceFormat.Vector4,
				SurfaceFormat.HalfSingle,
				SurfaceFormat.HalfVector2,
				SurfaceFormat.HalfVector4,
				SurfaceFormat.HdrBlendable
			};
			ProfileCapabilities.HiDef.ValidCubeFormats = new List<SurfaceFormat>
			{
				SurfaceFormat.Color,
				SurfaceFormat.Bgr565,
				SurfaceFormat.Bgra5551,
				SurfaceFormat.Bgra4444,
				SurfaceFormat.Dxt1,
				SurfaceFormat.Dxt3,
				SurfaceFormat.Dxt5,
				SurfaceFormat.Rgba1010102,
				SurfaceFormat.Rg32,
				SurfaceFormat.Rgba64,
				SurfaceFormat.Alpha8,
				SurfaceFormat.Single,
				SurfaceFormat.Vector2,
				SurfaceFormat.Vector4,
				SurfaceFormat.HalfSingle,
				SurfaceFormat.HalfVector2,
				SurfaceFormat.HalfVector4,
				SurfaceFormat.HdrBlendable
			};
			ProfileCapabilities.HiDef.ValidVolumeFormats = new List<SurfaceFormat>
			{
				SurfaceFormat.Color,
				SurfaceFormat.Bgr565,
				SurfaceFormat.Bgra5551,
				SurfaceFormat.Bgra4444,
				SurfaceFormat.Rgba1010102,
				SurfaceFormat.Rg32,
				SurfaceFormat.Rgba64,
				SurfaceFormat.Alpha8,
				SurfaceFormat.Single,
				SurfaceFormat.Vector2,
				SurfaceFormat.Vector4,
				SurfaceFormat.HalfSingle,
				SurfaceFormat.HalfVector2,
				SurfaceFormat.HalfVector4,
				SurfaceFormat.HdrBlendable
			};
			ProfileCapabilities.HiDef.ValidVertexTextureFormats = new List<SurfaceFormat>
			{
				SurfaceFormat.Single,
				SurfaceFormat.Vector2,
				SurfaceFormat.Vector4,
				SurfaceFormat.HalfSingle,
				SurfaceFormat.HalfVector2,
				SurfaceFormat.HalfVector4,
				SurfaceFormat.HdrBlendable
			};
			ProfileCapabilities.HiDef.InvalidFilterFormats = new List<SurfaceFormat>
			{
				SurfaceFormat.Single,
				SurfaceFormat.Vector2,
				SurfaceFormat.Vector4,
				SurfaceFormat.HalfSingle,
				SurfaceFormat.HalfVector2,
				SurfaceFormat.HalfVector4,
				SurfaceFormat.HdrBlendable
			};
			ProfileCapabilities.HiDef.InvalidBlendFormats = new List<SurfaceFormat>
			{
				SurfaceFormat.Single,
				SurfaceFormat.Vector2,
				SurfaceFormat.Vector4,
				SurfaceFormat.HalfSingle,
				SurfaceFormat.HalfVector2,
				SurfaceFormat.HalfVector4,
				SurfaceFormat.HdrBlendable
			};
			ProfileCapabilities.HiDef.ValidDepthFormats = new List<DepthFormat>
			{
				DepthFormat.Depth16,
				DepthFormat.Depth24,
				DepthFormat.Depth24Stencil8
			};
			ProfileCapabilities.HiDef.ValidVertexFormats = new List<VertexElementFormat>
			{
				VertexElementFormat.Color,
				VertexElementFormat.Single,
				VertexElementFormat.Vector2,
				VertexElementFormat.Vector3,
				VertexElementFormat.Vector4,
				VertexElementFormat.Byte4,
				VertexElementFormat.Short2,
				VertexElementFormat.Short4,
				VertexElementFormat.NormalizedShort2,
				VertexElementFormat.NormalizedShort4,
				VertexElementFormat.HalfVector2,
				VertexElementFormat.HalfVector4
			};
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0002F63D File Offset: 0x0002D83D
		internal void ThrowNotSupportedException(string message)
		{
			throw new NotSupportedException(message);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0002F645 File Offset: 0x0002D845
		internal void ThrowNotSupportedException(string message, object obj)
		{
			throw new NotSupportedException(message + " " + obj.ToString());
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0002F65D File Offset: 0x0002D85D
		internal void ThrowNotSupportedException(string message, object obj1, object obj2)
		{
			throw new NotSupportedException(string.Concat(new string[]
			{
				message,
				" ",
				obj1.ToString(),
				" ",
				obj2.ToString()
			}));
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0002F695 File Offset: 0x0002D895
		internal static ProfileCapabilities GetInstance(GraphicsProfile profile)
		{
			if (profile == GraphicsProfile.Reach)
			{
				return ProfileCapabilities.Reach;
			}
			if (profile == GraphicsProfile.HiDef)
			{
				return ProfileCapabilities.HiDef;
			}
			throw new ArgumentException("profile");
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x000136F5 File Offset: 0x000118F5
		public ProfileCapabilities()
		{
		}

		// Token: 0x04000956 RID: 2390
		internal GraphicsProfile Profile;

		// Token: 0x04000957 RID: 2391
		internal uint VertexShaderVersion;

		// Token: 0x04000958 RID: 2392
		internal uint PixelShaderVersion;

		// Token: 0x04000959 RID: 2393
		internal bool OcclusionQuery;

		// Token: 0x0400095A RID: 2394
		internal bool GetBackBufferData;

		// Token: 0x0400095B RID: 2395
		internal bool SeparateAlphaBlend;

		// Token: 0x0400095C RID: 2396
		internal bool DestBlendSrcAlphaSat;

		// Token: 0x0400095D RID: 2397
		internal bool MinMaxSrcDestBlend;

		// Token: 0x0400095E RID: 2398
		internal int MaxPrimitiveCount;

		// Token: 0x0400095F RID: 2399
		internal bool IndexElementSize32;

		// Token: 0x04000960 RID: 2400
		internal int MaxVertexStreams;

		// Token: 0x04000961 RID: 2401
		internal int MaxStreamStride;

		// Token: 0x04000962 RID: 2402
		internal int MaxVertexBufferSize;

		// Token: 0x04000963 RID: 2403
		internal int MaxIndexBufferSize;

		// Token: 0x04000964 RID: 2404
		internal int MaxTextureSize;

		// Token: 0x04000965 RID: 2405
		internal int MaxCubeSize;

		// Token: 0x04000966 RID: 2406
		internal int MaxVolumeExtent;

		// Token: 0x04000967 RID: 2407
		internal int MaxTextureAspectRatio;

		// Token: 0x04000968 RID: 2408
		internal int MaxSamplers;

		// Token: 0x04000969 RID: 2409
		internal int MaxVertexSamplers;

		// Token: 0x0400096A RID: 2410
		internal int MaxRenderTargets;

		// Token: 0x0400096B RID: 2411
		internal bool NonPow2Unconditional;

		// Token: 0x0400096C RID: 2412
		internal bool NonPow2Cube;

		// Token: 0x0400096D RID: 2413
		internal bool NonPow2Volume;

		// Token: 0x0400096E RID: 2414
		internal List<SurfaceFormat> ValidTextureFormats;

		// Token: 0x0400096F RID: 2415
		internal List<SurfaceFormat> ValidCubeFormats;

		// Token: 0x04000970 RID: 2416
		internal List<SurfaceFormat> ValidVolumeFormats;

		// Token: 0x04000971 RID: 2417
		internal List<SurfaceFormat> ValidVertexTextureFormats;

		// Token: 0x04000972 RID: 2418
		internal List<SurfaceFormat> InvalidFilterFormats;

		// Token: 0x04000973 RID: 2419
		internal List<SurfaceFormat> InvalidBlendFormats;

		// Token: 0x04000974 RID: 2420
		internal List<DepthFormat> ValidDepthFormats;

		// Token: 0x04000975 RID: 2421
		internal List<VertexElementFormat> ValidVertexFormats;

		// Token: 0x04000976 RID: 2422
		internal static ProfileCapabilities Reach = new ProfileCapabilities();

		// Token: 0x04000977 RID: 2423
		internal static ProfileCapabilities HiDef;
	}
}
