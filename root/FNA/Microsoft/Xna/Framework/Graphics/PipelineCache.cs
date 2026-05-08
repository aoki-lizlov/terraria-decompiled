using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000AC RID: 172
	internal class PipelineCache
	{
		// Token: 0x060013FE RID: 5118 RVA: 0x0002E350 File Offset: 0x0002C550
		public PipelineCache(GraphicsDevice graphicsDevice)
		{
			this.device = graphicsDevice;
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0002E3AC File Offset: 0x0002C5AC
		private static StateHash GetBlendHash(BlendFunction alphaBlendFunc, Blend alphaDestBlend, Blend alphaSrcBlend, BlendFunction colorBlendFunc, Blend colorDestBlend, Blend colorSrcBlend, ColorWriteChannels channels, ColorWriteChannels channels1, ColorWriteChannels channels2, ColorWriteChannels channels3, Color blendFactor, int multisampleMask)
		{
			long num = (long)(((long)alphaBlendFunc << 4) | colorBlendFunc);
			int num2 = (int)(((int)alphaDestBlend << 28) | ((int)alphaSrcBlend << 24) | ((int)colorDestBlend << 20) | ((int)colorSrcBlend << 16) | (Blend)((int)channels << 12) | (Blend)((int)channels1 << 8) | (Blend)((int)channels2 << 4) | (Blend)channels3);
			return new StateHash((ulong)((num << 32) | (long)num2), (ulong)(((long)multisampleMask << 32) | (long)((ulong)blendFactor.PackedValue)));
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0002E404 File Offset: 0x0002C604
		public static StateHash GetBlendHash(BlendState state)
		{
			return PipelineCache.GetBlendHash(state.AlphaBlendFunction, state.AlphaDestinationBlend, state.AlphaSourceBlend, state.ColorBlendFunction, state.ColorDestinationBlend, state.ColorSourceBlend, state.ColorWriteChannels, state.ColorWriteChannels1, state.ColorWriteChannels2, state.ColorWriteChannels3, state.BlendFactor, state.MultiSampleMask);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0002E460 File Offset: 0x0002C660
		public void BeginApplyBlend()
		{
			BlendState blendState = this.device.BlendState;
			this.AlphaBlendFunction = blendState.AlphaBlendFunction;
			this.AlphaDestinationBlend = blendState.AlphaDestinationBlend;
			this.AlphaSourceBlend = blendState.AlphaSourceBlend;
			this.ColorBlendFunction = blendState.ColorBlendFunction;
			this.ColorDestinationBlend = blendState.ColorDestinationBlend;
			this.ColorSourceBlend = blendState.ColorSourceBlend;
			this.ColorWriteChannels = blendState.ColorWriteChannels;
			this.ColorWriteChannels1 = blendState.ColorWriteChannels1;
			this.ColorWriteChannels2 = blendState.ColorWriteChannels2;
			this.ColorWriteChannels3 = blendState.ColorWriteChannels3;
			this.BlendFactor = blendState.BlendFactor;
			this.MultiSampleMask = blendState.MultiSampleMask;
			this.SeparateAlphaBlend = this.ColorBlendFunction != this.AlphaBlendFunction || this.ColorDestinationBlend != this.AlphaDestinationBlend;
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0002E534 File Offset: 0x0002C734
		public void EndApplyBlend()
		{
			StateHash blendHash = PipelineCache.GetBlendHash(this.AlphaBlendFunction, this.AlphaDestinationBlend, this.AlphaSourceBlend, this.ColorBlendFunction, this.ColorDestinationBlend, this.ColorSourceBlend, this.ColorWriteChannels, this.ColorWriteChannels1, this.ColorWriteChannels2, this.ColorWriteChannels3, this.BlendFactor, this.MultiSampleMask);
			BlendState blendState;
			if (!this.blendCache.TryGetValue(blendHash, out blendState))
			{
				blendState = new BlendState();
				blendState.AlphaBlendFunction = this.AlphaBlendFunction;
				blendState.AlphaDestinationBlend = this.AlphaDestinationBlend;
				blendState.AlphaSourceBlend = this.AlphaSourceBlend;
				blendState.ColorBlendFunction = this.ColorBlendFunction;
				blendState.ColorDestinationBlend = this.ColorDestinationBlend;
				blendState.ColorSourceBlend = this.ColorSourceBlend;
				blendState.ColorWriteChannels = this.ColorWriteChannels;
				blendState.ColorWriteChannels1 = this.ColorWriteChannels1;
				blendState.ColorWriteChannels2 = this.ColorWriteChannels2;
				blendState.ColorWriteChannels3 = this.ColorWriteChannels3;
				blendState.BlendFactor = this.BlendFactor;
				blendState.MultiSampleMask = this.MultiSampleMask;
				this.blendCache.Add(blendHash, blendState);
			}
			this.device.BlendState = blendState;
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0002E654 File Offset: 0x0002C854
		private static StateHash GetDepthStencilHash(bool depthBufferEnable, bool depthWriteEnable, CompareFunction depthFunc, bool stencilEnable, CompareFunction stencilFunc, StencilOperation stencilPass, StencilOperation stencilFail, StencilOperation stencilDepthFail, bool twoSidedStencil, CompareFunction ccwStencilFunc, StencilOperation ccwStencilPass, StencilOperation ccwStencilFail, StencilOperation ccwStencilDepthFail, int stencilMask, int stencilWriteMask, int referenceStencil)
		{
			int num = ((depthBufferEnable > false) ? 1 : 0);
			int num2 = ((depthWriteEnable > false) ? 1 : 0);
			int num3 = ((stencilEnable > false) ? 1 : 0);
			int num4 = ((twoSidedStencil > false) ? 1 : 0);
			int num5 = (num << 30) | (num2 << 29) | (num3 << 28) | (num4 << 27) | (int)((int)depthFunc << 24) | (int)((int)stencilFunc << 21) | (int)((int)ccwStencilFunc << 18) | (int)((int)stencilPass << 15) | (int)((int)stencilFail << 12) | (int)((int)stencilDepthFail << 9) | (int)((int)ccwStencilFail << 6) | (int)((int)ccwStencilPass << 3) | (int)ccwStencilDepthFail;
			return new StateHash((ulong)(((long)stencilMask << 32) | (long)num5), (ulong)(((long)referenceStencil << 32) | (long)stencilWriteMask));
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0002E6D0 File Offset: 0x0002C8D0
		public static StateHash GetDepthStencilHash(DepthStencilState state)
		{
			return PipelineCache.GetDepthStencilHash(state.DepthBufferEnable, state.DepthBufferWriteEnable, state.DepthBufferFunction, state.StencilEnable, state.StencilFunction, state.StencilPass, state.StencilFail, state.StencilDepthBufferFail, state.TwoSidedStencilMode, state.CounterClockwiseStencilFunction, state.CounterClockwiseStencilPass, state.CounterClockwiseStencilFail, state.CounterClockwiseStencilDepthBufferFail, state.StencilMask, state.StencilWriteMask, state.ReferenceStencil);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0002E744 File Offset: 0x0002C944
		public void BeginApplyDepthStencil()
		{
			DepthStencilState depthStencilState = this.device.DepthStencilState;
			this.DepthBufferEnable = depthStencilState.DepthBufferEnable;
			this.DepthBufferWriteEnable = depthStencilState.DepthBufferWriteEnable;
			this.DepthBufferFunction = depthStencilState.DepthBufferFunction;
			this.StencilEnable = depthStencilState.StencilEnable;
			this.StencilFunction = depthStencilState.StencilFunction;
			this.StencilPass = depthStencilState.StencilPass;
			this.StencilFail = depthStencilState.StencilFail;
			this.StencilDepthBufferFail = depthStencilState.StencilDepthBufferFail;
			this.TwoSidedStencilMode = depthStencilState.TwoSidedStencilMode;
			this.CCWStencilFunction = depthStencilState.CounterClockwiseStencilFunction;
			this.CCWStencilFail = depthStencilState.CounterClockwiseStencilFail;
			this.CCWStencilPass = depthStencilState.CounterClockwiseStencilPass;
			this.CCWStencilDepthBufferFail = depthStencilState.CounterClockwiseStencilDepthBufferFail;
			this.StencilMask = depthStencilState.StencilMask;
			this.StencilWriteMask = depthStencilState.StencilWriteMask;
			this.ReferenceStencil = depthStencilState.ReferenceStencil;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0002E820 File Offset: 0x0002CA20
		public void EndApplyDepthStencil()
		{
			StateHash depthStencilHash = PipelineCache.GetDepthStencilHash(this.DepthBufferEnable, this.DepthBufferWriteEnable, this.DepthBufferFunction, this.StencilEnable, this.StencilFunction, this.StencilPass, this.StencilFail, this.StencilDepthBufferFail, this.TwoSidedStencilMode, this.CCWStencilFunction, this.CCWStencilPass, this.CCWStencilFail, this.CCWStencilDepthBufferFail, this.StencilMask, this.StencilWriteMask, this.ReferenceStencil);
			DepthStencilState depthStencilState;
			if (!this.depthStencilCache.TryGetValue(depthStencilHash, out depthStencilState))
			{
				depthStencilState = new DepthStencilState();
				depthStencilState.DepthBufferEnable = this.DepthBufferEnable;
				depthStencilState.DepthBufferWriteEnable = this.DepthBufferWriteEnable;
				depthStencilState.DepthBufferFunction = this.DepthBufferFunction;
				depthStencilState.StencilEnable = this.StencilEnable;
				depthStencilState.StencilFunction = this.StencilFunction;
				depthStencilState.StencilPass = this.StencilPass;
				depthStencilState.StencilFail = this.StencilFail;
				depthStencilState.StencilDepthBufferFail = this.StencilDepthBufferFail;
				depthStencilState.TwoSidedStencilMode = this.TwoSidedStencilMode;
				depthStencilState.CounterClockwiseStencilFunction = this.CCWStencilFunction;
				depthStencilState.CounterClockwiseStencilFail = this.CCWStencilFail;
				depthStencilState.CounterClockwiseStencilPass = this.CCWStencilPass;
				depthStencilState.CounterClockwiseStencilDepthBufferFail = this.CCWStencilDepthBufferFail;
				depthStencilState.StencilMask = this.StencilMask;
				depthStencilState.StencilWriteMask = this.StencilWriteMask;
				depthStencilState.ReferenceStencil = this.ReferenceStencil;
				this.depthStencilCache.Add(depthStencilHash, depthStencilState);
			}
			this.device.DepthStencilState = depthStencilState;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0002E988 File Offset: 0x0002CB88
		private static StateHash GetRasterizerHash(CullMode cullMode, FillMode fillMode, float depthBias, bool msaa, bool scissor, float slopeScaleDepthBias)
		{
			int num = ((msaa > false) ? 1 : 0);
			int num2 = ((scissor > false) ? 1 : 0);
			return new StateHash((ulong)((long)((num << 4) | (num2 << 3) | (int)((int)cullMode << 1) | (int)fillMode)), (PipelineCache.FloatToULong(slopeScaleDepthBias) << 32) | PipelineCache.FloatToULong(depthBias));
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0002E9C2 File Offset: 0x0002CBC2
		public static StateHash GetRasterizerHash(RasterizerState state)
		{
			return PipelineCache.GetRasterizerHash(state.CullMode, state.FillMode, state.DepthBias, state.MultiSampleAntiAlias, state.ScissorTestEnable, state.SlopeScaleDepthBias);
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0002E9F0 File Offset: 0x0002CBF0
		public void BeginApplyRasterizer()
		{
			RasterizerState rasterizerState = this.device.RasterizerState;
			this.CullMode = rasterizerState.CullMode;
			this.FillMode = rasterizerState.FillMode;
			this.DepthBias = rasterizerState.DepthBias;
			this.MultiSampleAntiAlias = rasterizerState.MultiSampleAntiAlias;
			this.ScissorTestEnable = rasterizerState.ScissorTestEnable;
			this.SlopeScaleDepthBias = rasterizerState.SlopeScaleDepthBias;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0002EA54 File Offset: 0x0002CC54
		public void EndApplyRasterizer()
		{
			StateHash rasterizerHash = PipelineCache.GetRasterizerHash(this.CullMode, this.FillMode, this.DepthBias, this.MultiSampleAntiAlias, this.ScissorTestEnable, this.SlopeScaleDepthBias);
			RasterizerState rasterizerState;
			if (!this.rasterizerCache.TryGetValue(rasterizerHash, out rasterizerState))
			{
				rasterizerState = new RasterizerState();
				rasterizerState.CullMode = this.CullMode;
				rasterizerState.FillMode = this.FillMode;
				rasterizerState.DepthBias = this.DepthBias;
				rasterizerState.MultiSampleAntiAlias = this.MultiSampleAntiAlias;
				rasterizerState.ScissorTestEnable = this.ScissorTestEnable;
				rasterizerState.SlopeScaleDepthBias = this.SlopeScaleDepthBias;
				this.rasterizerCache.Add(rasterizerHash, rasterizerState);
			}
			this.device.RasterizerState = rasterizerState;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0002EB04 File Offset: 0x0002CD04
		private static StateHash GetSamplerHash(TextureAddressMode addressU, TextureAddressMode addressV, TextureAddressMode addressW, int maxAnisotropy, int maxMipLevel, float mipLODBias, TextureFilter filter)
		{
			int num = (int)(((int)filter << 6) | (TextureFilter)((int)addressU << 4) | (TextureFilter)((int)addressV << 2) | (TextureFilter)addressW);
			return new StateHash((ulong)(((long)maxAnisotropy << 32) | (long)num), (PipelineCache.FloatToULong(mipLODBias) << 32) | (ulong)((long)maxMipLevel));
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0002EB3B File Offset: 0x0002CD3B
		public static StateHash GetSamplerHash(SamplerState state)
		{
			return PipelineCache.GetSamplerHash(state.AddressU, state.AddressV, state.AddressW, state.MaxAnisotropy, state.MaxMipLevel, state.MipMapLevelOfDetailBias, state.Filter);
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0002EB6C File Offset: 0x0002CD6C
		public void BeginApplySampler(SamplerStateCollection samplers, int register)
		{
			SamplerState samplerState = samplers[register];
			this.AddressU = samplerState.AddressU;
			this.AddressV = samplerState.AddressV;
			this.AddressW = samplerState.AddressW;
			this.MaxAnisotropy = samplerState.MaxAnisotropy;
			this.MaxMipLevel = samplerState.MaxMipLevel;
			this.MipMapLODBias = samplerState.MipMapLevelOfDetailBias;
			this.Filter = samplerState.Filter;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0002EBD8 File Offset: 0x0002CDD8
		public void EndApplySampler(SamplerStateCollection samplers, int register)
		{
			StateHash samplerHash = PipelineCache.GetSamplerHash(this.AddressU, this.AddressV, this.AddressW, this.MaxAnisotropy, this.MaxMipLevel, this.MipMapLODBias, this.Filter);
			SamplerState samplerState;
			if (!this.samplerCache.TryGetValue(samplerHash, out samplerState))
			{
				samplerState = new SamplerState();
				samplerState.Filter = this.Filter;
				samplerState.AddressU = this.AddressU;
				samplerState.AddressV = this.AddressV;
				samplerState.AddressW = this.AddressW;
				samplerState.MaxAnisotropy = this.MaxAnisotropy;
				samplerState.MaxMipLevel = this.MaxMipLevel;
				samplerState.MipMapLevelOfDetailBias = this.MipMapLODBias;
				this.samplerCache.Add(samplerHash, samplerState);
			}
			samplers[register] = samplerState;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0002EC94 File Offset: 0x0002CE94
		public static ulong GetVertexDeclarationHash(VertexDeclaration declaration, ulong vertexShader)
		{
			ulong num = vertexShader;
			for (int i = 0; i < declaration.elements.Length; i++)
			{
				num = num * 39UL + (ulong)((long)declaration.elements[i].GetHashCode());
			}
			return num * 39UL + (ulong)((long)declaration.VertexStride);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0002ECE4 File Offset: 0x0002CEE4
		public static ulong GetVertexBindingHash(VertexBufferBinding[] bindings, int numBindings, ulong vertexShader)
		{
			ulong num = vertexShader;
			for (int i = 0; i < numBindings; i++)
			{
				VertexBufferBinding vertexBufferBinding = bindings[i];
				num = num * 39UL + (ulong)((long)vertexBufferBinding.InstanceFrequency);
				num = num * 39UL + PipelineCache.GetVertexDeclarationHash(vertexBufferBinding.VertexBuffer.VertexDeclaration, vertexShader);
			}
			return num;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0002ED30 File Offset: 0x0002CF30
		private unsafe static ulong FloatToULong(float f)
		{
			return (ulong)(*(uint*)(&f));
		}

		// Token: 0x0400091A RID: 2330
		private GraphicsDevice device;

		// Token: 0x0400091B RID: 2331
		public BlendFunction AlphaBlendFunction;

		// Token: 0x0400091C RID: 2332
		public Blend AlphaDestinationBlend;

		// Token: 0x0400091D RID: 2333
		public Blend AlphaSourceBlend;

		// Token: 0x0400091E RID: 2334
		public BlendFunction ColorBlendFunction;

		// Token: 0x0400091F RID: 2335
		public Blend ColorDestinationBlend;

		// Token: 0x04000920 RID: 2336
		public Blend ColorSourceBlend;

		// Token: 0x04000921 RID: 2337
		public ColorWriteChannels ColorWriteChannels;

		// Token: 0x04000922 RID: 2338
		public ColorWriteChannels ColorWriteChannels1;

		// Token: 0x04000923 RID: 2339
		public ColorWriteChannels ColorWriteChannels2;

		// Token: 0x04000924 RID: 2340
		public ColorWriteChannels ColorWriteChannels3;

		// Token: 0x04000925 RID: 2341
		public Color BlendFactor;

		// Token: 0x04000926 RID: 2342
		public int MultiSampleMask;

		// Token: 0x04000927 RID: 2343
		public bool SeparateAlphaBlend;

		// Token: 0x04000928 RID: 2344
		private Dictionary<StateHash, BlendState> blendCache = new Dictionary<StateHash, BlendState>(StateHashComparer.Instance);

		// Token: 0x04000929 RID: 2345
		public bool DepthBufferEnable;

		// Token: 0x0400092A RID: 2346
		public bool DepthBufferWriteEnable;

		// Token: 0x0400092B RID: 2347
		public CompareFunction DepthBufferFunction;

		// Token: 0x0400092C RID: 2348
		public bool StencilEnable;

		// Token: 0x0400092D RID: 2349
		public CompareFunction StencilFunction;

		// Token: 0x0400092E RID: 2350
		public StencilOperation StencilPass;

		// Token: 0x0400092F RID: 2351
		public StencilOperation StencilFail;

		// Token: 0x04000930 RID: 2352
		public StencilOperation StencilDepthBufferFail;

		// Token: 0x04000931 RID: 2353
		public bool TwoSidedStencilMode;

		// Token: 0x04000932 RID: 2354
		public CompareFunction CCWStencilFunction;

		// Token: 0x04000933 RID: 2355
		public StencilOperation CCWStencilFail;

		// Token: 0x04000934 RID: 2356
		public StencilOperation CCWStencilPass;

		// Token: 0x04000935 RID: 2357
		public StencilOperation CCWStencilDepthBufferFail;

		// Token: 0x04000936 RID: 2358
		public int StencilMask;

		// Token: 0x04000937 RID: 2359
		public int StencilWriteMask;

		// Token: 0x04000938 RID: 2360
		public int ReferenceStencil;

		// Token: 0x04000939 RID: 2361
		private Dictionary<StateHash, DepthStencilState> depthStencilCache = new Dictionary<StateHash, DepthStencilState>(StateHashComparer.Instance);

		// Token: 0x0400093A RID: 2362
		public CullMode CullMode;

		// Token: 0x0400093B RID: 2363
		public FillMode FillMode;

		// Token: 0x0400093C RID: 2364
		public float DepthBias;

		// Token: 0x0400093D RID: 2365
		public bool MultiSampleAntiAlias;

		// Token: 0x0400093E RID: 2366
		public bool ScissorTestEnable;

		// Token: 0x0400093F RID: 2367
		public float SlopeScaleDepthBias;

		// Token: 0x04000940 RID: 2368
		private Dictionary<StateHash, RasterizerState> rasterizerCache = new Dictionary<StateHash, RasterizerState>(StateHashComparer.Instance);

		// Token: 0x04000941 RID: 2369
		public TextureAddressMode AddressU;

		// Token: 0x04000942 RID: 2370
		public TextureAddressMode AddressV;

		// Token: 0x04000943 RID: 2371
		public TextureAddressMode AddressW;

		// Token: 0x04000944 RID: 2372
		public int MaxAnisotropy;

		// Token: 0x04000945 RID: 2373
		public int MaxMipLevel;

		// Token: 0x04000946 RID: 2374
		public float MipMapLODBias;

		// Token: 0x04000947 RID: 2375
		public TextureFilter Filter;

		// Token: 0x04000948 RID: 2376
		private Dictionary<StateHash, SamplerState> samplerCache = new Dictionary<StateHash, SamplerState>(StateHashComparer.Instance);

		// Token: 0x04000949 RID: 2377
		private const ulong HASH_FACTOR = 39UL;
	}
}
