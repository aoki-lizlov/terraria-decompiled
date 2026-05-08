using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000080 RID: 128
	public class Effect : GraphicsResource
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00024F60 File Offset: 0x00023160
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x00024F68 File Offset: 0x00023168
		public EffectTechnique CurrentTechnique
		{
			get
			{
				return this.INTERNAL_currentTechnique;
			}
			set
			{
				FNA3D.FNA3D_SetEffectTechnique(base.GraphicsDevice.GLDevice, this.glEffect, value.TechniquePointer);
				this.INTERNAL_currentTechnique = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x00024F8D File Offset: 0x0002318D
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x00024F95 File Offset: 0x00023195
		public EffectParameterCollection Parameters
		{
			[CompilerGenerated]
			get
			{
				return this.<Parameters>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Parameters>k__BackingField = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x00024F9E File Offset: 0x0002319E
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x00024FA6 File Offset: 0x000231A6
		public EffectTechniqueCollection Techniques
		{
			[CompilerGenerated]
			get
			{
				return this.<Techniques>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Techniques>k__BackingField = value;
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00024FB0 File Offset: 0x000231B0
		public Effect(GraphicsDevice graphicsDevice, byte[] effectCode)
		{
			base.GraphicsDevice = graphicsDevice;
			IntPtr intPtr;
			FNA3D.FNA3D_CreateEffect(graphicsDevice.GLDevice, effectCode, effectCode.Length, out this.glEffect, out intPtr);
			this.effectData = intPtr;
			this.INTERNAL_parseEffectStruct(intPtr);
			this.CurrentTechnique = this.Techniques[0];
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00025014 File Offset: 0x00023214
		protected Effect(Effect cloneSource)
		{
			base.GraphicsDevice = cloneSource.GraphicsDevice;
			IntPtr intPtr;
			FNA3D.FNA3D_CloneEffect(base.GraphicsDevice.GLDevice, cloneSource.glEffect, out this.glEffect, out intPtr);
			this.effectData = intPtr;
			this.INTERNAL_parseEffectStruct(intPtr);
			for (int i = 0; i < cloneSource.Parameters.Count; i++)
			{
				this.Parameters[i].texture = cloneSource.Parameters[i].texture;
			}
			for (int j = 0; j < cloneSource.Techniques.Count; j++)
			{
				if (cloneSource.Techniques[j] == cloneSource.CurrentTechnique)
				{
					this.CurrentTechnique = this.Techniques[j];
				}
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x000250E3 File Offset: 0x000232E3
		public virtual Effect Clone()
		{
			return new Effect(this);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x000250EC File Offset: 0x000232EC
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				IntPtr intPtr = Interlocked.Exchange(ref this.glEffect, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeEffect(base.GraphicsDevice.GLDevice, intPtr);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00009E6B File Offset: 0x0000806B
		protected internal virtual void OnApply()
		{
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00025138 File Offset: 0x00023338
		internal unsafe void INTERNAL_applyEffect(uint pass)
		{
			FNA3D.FNA3D_ApplyEffect(base.GraphicsDevice.GLDevice, this.glEffect, pass, base.GraphicsDevice.effectStateChangesPtr);
			Effect.MOJOSHADER_effectStateChanges* ptr = (Effect.MOJOSHADER_effectStateChanges*)(void*)base.GraphicsDevice.effectStateChangesPtr;
			if (ptr->render_state_change_count > 0U)
			{
				PipelineCache pipelineCache = base.GraphicsDevice.PipelineCache;
				pipelineCache.BeginApplyBlend();
				pipelineCache.BeginApplyDepthStencil();
				pipelineCache.BeginApplyRasterizer();
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				Effect.MOJOSHADER_effectState* ptr2 = (Effect.MOJOSHADER_effectState*)(void*)ptr->render_state_changes;
				int num = 0;
				while ((long)num < (long)((ulong)ptr->render_state_change_count))
				{
					Effect.MOJOSHADER_renderStateType type = ptr2[num].type;
					if (type != Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_VERTEXSHADER && type != Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_PIXELSHADER)
					{
						if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_ZENABLE)
						{
							Effect.MOJOSHADER_zBufferType* ptr3 = (Effect.MOJOSHADER_zBufferType*)(void*)ptr2[num].value.values;
							pipelineCache.DepthBufferEnable = *ptr3 == Effect.MOJOSHADER_zBufferType.MOJOSHADER_ZB_TRUE;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_FILLMODE)
						{
							Effect.MOJOSHADER_fillMode* ptr4 = (Effect.MOJOSHADER_fillMode*)(void*)ptr2[num].value.values;
							if (*ptr4 == Effect.MOJOSHADER_fillMode.MOJOSHADER_FILL_SOLID)
							{
								pipelineCache.FillMode = FillMode.Solid;
							}
							else if (*ptr4 == Effect.MOJOSHADER_fillMode.MOJOSHADER_FILL_WIREFRAME)
							{
								pipelineCache.FillMode = FillMode.WireFrame;
							}
							flag3 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_ZWRITEENABLE)
						{
							int* ptr5 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.DepthBufferWriteEnable = *ptr5 == 1;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_SRCBLEND)
						{
							Effect.MOJOSHADER_blendMode* ptr6 = (Effect.MOJOSHADER_blendMode*)(void*)ptr2[num].value.values;
							pipelineCache.ColorSourceBlend = Effect.XNABlend[(int)(*ptr6)];
							if (!pipelineCache.SeparateAlphaBlend)
							{
								pipelineCache.AlphaSourceBlend = Effect.XNABlend[(int)(*ptr6)];
							}
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_DESTBLEND)
						{
							Effect.MOJOSHADER_blendMode* ptr7 = (Effect.MOJOSHADER_blendMode*)(void*)ptr2[num].value.values;
							pipelineCache.ColorDestinationBlend = Effect.XNABlend[(int)(*ptr7)];
							if (!pipelineCache.SeparateAlphaBlend)
							{
								pipelineCache.AlphaDestinationBlend = Effect.XNABlend[(int)(*ptr7)];
							}
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_CULLMODE)
						{
							Effect.MOJOSHADER_cullMode* ptr8 = (Effect.MOJOSHADER_cullMode*)(void*)ptr2[num].value.values;
							if (*ptr8 == Effect.MOJOSHADER_cullMode.MOJOSHADER_CULL_NONE)
							{
								pipelineCache.CullMode = CullMode.None;
							}
							else if (*ptr8 == Effect.MOJOSHADER_cullMode.MOJOSHADER_CULL_CW)
							{
								pipelineCache.CullMode = CullMode.CullClockwiseFace;
							}
							else if (*ptr8 == Effect.MOJOSHADER_cullMode.MOJOSHADER_CULL_CCW)
							{
								pipelineCache.CullMode = CullMode.CullCounterClockwiseFace;
							}
							flag3 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_ZFUNC)
						{
							Effect.MOJOSHADER_compareFunc* ptr9 = (Effect.MOJOSHADER_compareFunc*)(void*)ptr2[num].value.values;
							pipelineCache.DepthBufferFunction = Effect.XNACompare[(int)(*ptr9)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_ALPHABLENDENABLE)
						{
							int* ptr10 = (int*)(void*)ptr2[num].value.values;
							if (*ptr10 == 0)
							{
								pipelineCache.ColorSourceBlend = Blend.One;
								pipelineCache.ColorDestinationBlend = Blend.Zero;
								pipelineCache.AlphaSourceBlend = Blend.One;
								pipelineCache.AlphaDestinationBlend = Blend.Zero;
								flag = true;
							}
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_STENCILENABLE)
						{
							int* ptr11 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.StencilEnable = *ptr11 == 1;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_STENCILFAIL)
						{
							Effect.MOJOSHADER_stencilOp* ptr12 = (Effect.MOJOSHADER_stencilOp*)(void*)ptr2[num].value.values;
							pipelineCache.StencilFail = Effect.XNAStencilOp[(int)(*ptr12)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_STENCILZFAIL)
						{
							Effect.MOJOSHADER_stencilOp* ptr13 = (Effect.MOJOSHADER_stencilOp*)(void*)ptr2[num].value.values;
							pipelineCache.StencilDepthBufferFail = Effect.XNAStencilOp[(int)(*ptr13)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_STENCILPASS)
						{
							Effect.MOJOSHADER_stencilOp* ptr14 = (Effect.MOJOSHADER_stencilOp*)(void*)ptr2[num].value.values;
							pipelineCache.StencilPass = Effect.XNAStencilOp[(int)(*ptr14)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_STENCILFUNC)
						{
							Effect.MOJOSHADER_compareFunc* ptr15 = (Effect.MOJOSHADER_compareFunc*)(void*)ptr2[num].value.values;
							pipelineCache.StencilFunction = Effect.XNACompare[(int)(*ptr15)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_STENCILREF)
						{
							int* ptr16 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.ReferenceStencil = *ptr16;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_STENCILMASK)
						{
							int* ptr17 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.StencilMask = *ptr17;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_STENCILWRITEMASK)
						{
							int* ptr18 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.StencilWriteMask = *ptr18;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_MULTISAMPLEANTIALIAS)
						{
							int* ptr19 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.MultiSampleAntiAlias = *ptr19 == 1;
							flag3 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_MULTISAMPLEMASK)
						{
							int* ptr20 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.MultiSampleMask = *ptr20;
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_COLORWRITEENABLE)
						{
							int* ptr21 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.ColorWriteChannels = (ColorWriteChannels)(*ptr21);
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_BLENDOP)
						{
							Effect.MOJOSHADER_blendOp* ptr22 = (Effect.MOJOSHADER_blendOp*)(void*)ptr2[num].value.values;
							pipelineCache.ColorBlendFunction = Effect.XNABlendOp[(int)(*ptr22)];
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_SCISSORTESTENABLE)
						{
							int* ptr23 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.ScissorTestEnable = *ptr23 == 1;
							flag3 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_SLOPESCALEDEPTHBIAS)
						{
							float* ptr24 = (float*)(void*)ptr2[num].value.values;
							pipelineCache.SlopeScaleDepthBias = *ptr24;
							flag3 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_TWOSIDEDSTENCILMODE)
						{
							int* ptr25 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.TwoSidedStencilMode = *ptr25 == 1;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_CCW_STENCILFAIL)
						{
							Effect.MOJOSHADER_stencilOp* ptr26 = (Effect.MOJOSHADER_stencilOp*)(void*)ptr2[num].value.values;
							pipelineCache.CCWStencilFail = Effect.XNAStencilOp[(int)(*ptr26)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_CCW_STENCILZFAIL)
						{
							Effect.MOJOSHADER_stencilOp* ptr27 = (Effect.MOJOSHADER_stencilOp*)(void*)ptr2[num].value.values;
							pipelineCache.CCWStencilDepthBufferFail = Effect.XNAStencilOp[(int)(*ptr27)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_CCW_STENCILPASS)
						{
							Effect.MOJOSHADER_stencilOp* ptr28 = (Effect.MOJOSHADER_stencilOp*)(void*)ptr2[num].value.values;
							pipelineCache.CCWStencilPass = Effect.XNAStencilOp[(int)(*ptr28)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_CCW_STENCILFUNC)
						{
							Effect.MOJOSHADER_compareFunc* ptr29 = (Effect.MOJOSHADER_compareFunc*)(void*)ptr2[num].value.values;
							pipelineCache.CCWStencilFunction = Effect.XNACompare[(int)(*ptr29)];
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_COLORWRITEENABLE1)
						{
							int* ptr30 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.ColorWriteChannels1 = (ColorWriteChannels)(*ptr30);
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_COLORWRITEENABLE2)
						{
							int* ptr31 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.ColorWriteChannels2 = (ColorWriteChannels)(*ptr31);
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_COLORWRITEENABLE3)
						{
							int* ptr32 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.ColorWriteChannels3 = (ColorWriteChannels)(*ptr32);
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_BLENDFACTOR)
						{
							int* ptr33 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.BlendFactor = new Color((*ptr33 >> 24) & 255, (*ptr33 >> 16) & 255, (*ptr33 >> 8) & 255, *ptr33 & 255);
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_DEPTHBIAS)
						{
							float* ptr34 = (float*)(void*)ptr2[num].value.values;
							pipelineCache.DepthBias = *ptr34;
							flag3 = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_SEPARATEALPHABLENDENABLE)
						{
							int* ptr35 = (int*)(void*)ptr2[num].value.values;
							pipelineCache.SeparateAlphaBlend = *ptr35 == 1;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_SRCBLENDALPHA)
						{
							Effect.MOJOSHADER_blendMode* ptr36 = (Effect.MOJOSHADER_blendMode*)(void*)ptr2[num].value.values;
							pipelineCache.AlphaSourceBlend = Effect.XNABlend[(int)(*ptr36)];
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_DESTBLENDALPHA)
						{
							Effect.MOJOSHADER_blendMode* ptr37 = (Effect.MOJOSHADER_blendMode*)(void*)ptr2[num].value.values;
							pipelineCache.AlphaDestinationBlend = Effect.XNABlend[(int)(*ptr37)];
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_renderStateType.MOJOSHADER_RS_BLENDOPALPHA)
						{
							Effect.MOJOSHADER_blendOp* ptr38 = (Effect.MOJOSHADER_blendOp*)(void*)ptr2[num].value.values;
							pipelineCache.AlphaBlendFunction = Effect.XNABlendOp[(int)(*ptr38)];
							flag = true;
						}
						else if (type != (Effect.MOJOSHADER_renderStateType)178)
						{
							throw new NotImplementedException("Unhandled render state! " + type.ToString());
						}
					}
					num++;
				}
				if (flag)
				{
					pipelineCache.EndApplyBlend();
				}
				if (flag2)
				{
					pipelineCache.EndApplyDepthStencil();
				}
				if (flag3)
				{
					pipelineCache.EndApplyRasterizer();
				}
			}
			if (ptr->sampler_state_change_count > 0U)
			{
				this.INTERNAL_updateSamplers(ptr->sampler_state_change_count, (Effect.MOJOSHADER_samplerStateRegister*)(void*)ptr->sampler_state_changes, base.GraphicsDevice.Textures, base.GraphicsDevice.SamplerStates);
			}
			if (ptr->vertex_sampler_state_change_count > 0U)
			{
				this.INTERNAL_updateSamplers(ptr->vertex_sampler_state_change_count, (Effect.MOJOSHADER_samplerStateRegister*)(void*)ptr->vertex_sampler_state_changes, base.GraphicsDevice.VertexTextures, base.GraphicsDevice.VertexSamplerStates);
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00025B10 File Offset: 0x00023D10
		private unsafe void INTERNAL_updateSamplers(uint changeCount, Effect.MOJOSHADER_samplerStateRegister* registers, TextureCollection textures, SamplerStateCollection samplers)
		{
			int num = 0;
			while ((long)num < (long)((ulong)changeCount))
			{
				if (registers[num].sampler_state_count != 0U)
				{
					int sampler_register = (int)registers[num].sampler_register;
					PipelineCache pipelineCache = base.GraphicsDevice.PipelineCache;
					pipelineCache.BeginApplySampler(samplers, sampler_register);
					bool flag = false;
					bool flag2 = false;
					TextureFilter filter = pipelineCache.Filter;
					Effect.MOJOSHADER_textureFilterType mojoshader_textureFilterType = Effect.XNAMag[(int)filter];
					Effect.MOJOSHADER_textureFilterType mojoshader_textureFilterType2 = Effect.XNAMin[(int)filter];
					Effect.MOJOSHADER_textureFilterType mojoshader_textureFilterType3 = Effect.XNAMip[(int)filter];
					Effect.MOJOSHADER_effectSamplerState* ptr = (Effect.MOJOSHADER_effectSamplerState*)(void*)registers[num].sampler_states;
					int num2 = 0;
					while ((long)num2 < (long)((ulong)registers[num].sampler_state_count))
					{
						Effect.MOJOSHADER_samplerStateType type = ptr[num2].type;
						if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_TEXTURE)
						{
							EffectParameter effectParameter;
							if (this.samplerMap.TryGetValue(registers[num].sampler_name, out effectParameter))
							{
								Texture texture = effectParameter.texture;
								if (texture != null)
								{
									textures[sampler_register] = texture;
								}
							}
						}
						else if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_ADDRESSU)
						{
							Effect.MOJOSHADER_textureAddress* ptr2 = (Effect.MOJOSHADER_textureAddress*)(void*)ptr[num2].value.values;
							pipelineCache.AddressU = Effect.XNAAddress[(int)(*ptr2)];
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_ADDRESSV)
						{
							Effect.MOJOSHADER_textureAddress* ptr3 = (Effect.MOJOSHADER_textureAddress*)(void*)ptr[num2].value.values;
							pipelineCache.AddressV = Effect.XNAAddress[(int)(*ptr3)];
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_ADDRESSW)
						{
							Effect.MOJOSHADER_textureAddress* ptr4 = (Effect.MOJOSHADER_textureAddress*)(void*)ptr[num2].value.values;
							pipelineCache.AddressW = Effect.XNAAddress[(int)(*ptr4)];
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_MAGFILTER)
						{
							Effect.MOJOSHADER_textureFilterType* ptr5 = (Effect.MOJOSHADER_textureFilterType*)(void*)ptr[num2].value.values;
							mojoshader_textureFilterType = *ptr5;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_MINFILTER)
						{
							Effect.MOJOSHADER_textureFilterType* ptr6 = (Effect.MOJOSHADER_textureFilterType*)(void*)ptr[num2].value.values;
							mojoshader_textureFilterType2 = *ptr6;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_MIPFILTER)
						{
							Effect.MOJOSHADER_textureFilterType* ptr7 = (Effect.MOJOSHADER_textureFilterType*)(void*)ptr[num2].value.values;
							mojoshader_textureFilterType3 = *ptr7;
							flag2 = true;
						}
						else if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_MIPMAPLODBIAS)
						{
							float* ptr8 = (float*)(void*)ptr[num2].value.values;
							pipelineCache.MipMapLODBias = *ptr8;
							flag = true;
						}
						else if (type == Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_MAXMIPLEVEL)
						{
							int* ptr9 = (int*)(void*)ptr[num2].value.values;
							pipelineCache.MaxMipLevel = *ptr9;
							flag = true;
						}
						else
						{
							if (type != Effect.MOJOSHADER_samplerStateType.MOJOSHADER_SAMP_MAXANISOTROPY)
							{
								throw new NotImplementedException("Unhandled sampler state! " + type.ToString());
							}
							int* ptr10 = (int*)(void*)ptr[num2].value.values;
							pipelineCache.MaxAnisotropy = *ptr10;
							flag = true;
						}
						num2++;
					}
					if (flag2)
					{
						if (mojoshader_textureFilterType == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT)
						{
							if (mojoshader_textureFilterType2 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT)
							{
								if (mojoshader_textureFilterType3 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_NONE || mojoshader_textureFilterType3 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT)
								{
									pipelineCache.Filter = TextureFilter.Point;
								}
								else
								{
									if (mojoshader_textureFilterType3 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR && mojoshader_textureFilterType3 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC)
									{
										throw new NotImplementedException("Unhandled mipfilter type! " + mojoshader_textureFilterType3.ToString());
									}
									pipelineCache.Filter = TextureFilter.PointMipLinear;
								}
							}
							else
							{
								if (mojoshader_textureFilterType2 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR && mojoshader_textureFilterType2 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC)
								{
									throw new NotImplementedException("Unhandled minfilter type! " + mojoshader_textureFilterType2.ToString());
								}
								if (mojoshader_textureFilterType3 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_NONE || mojoshader_textureFilterType3 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT)
								{
									pipelineCache.Filter = TextureFilter.MinLinearMagPointMipPoint;
								}
								else
								{
									if (mojoshader_textureFilterType3 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR && mojoshader_textureFilterType3 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC)
									{
										throw new NotImplementedException("Unhandled mipfilter type! " + mojoshader_textureFilterType3.ToString());
									}
									pipelineCache.Filter = TextureFilter.MinLinearMagPointMipLinear;
								}
							}
						}
						else
						{
							if (mojoshader_textureFilterType != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR && mojoshader_textureFilterType != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC)
							{
								throw new NotImplementedException("Unhandled magfilter type! " + mojoshader_textureFilterType.ToString());
							}
							if (mojoshader_textureFilterType2 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT)
							{
								if (mojoshader_textureFilterType3 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_NONE || mojoshader_textureFilterType3 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT)
								{
									pipelineCache.Filter = TextureFilter.MinPointMagLinearMipPoint;
								}
								else
								{
									if (mojoshader_textureFilterType3 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR && mojoshader_textureFilterType3 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC)
									{
										throw new NotImplementedException("Unhandled mipfilter type! " + mojoshader_textureFilterType3.ToString());
									}
									pipelineCache.Filter = TextureFilter.MinPointMagLinearMipLinear;
								}
							}
							else
							{
								if (mojoshader_textureFilterType2 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR && mojoshader_textureFilterType2 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC)
								{
									throw new NotImplementedException("Unhandled minfilter type! " + mojoshader_textureFilterType2.ToString());
								}
								if (mojoshader_textureFilterType3 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_NONE || mojoshader_textureFilterType3 == Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT)
								{
									pipelineCache.Filter = TextureFilter.LinearMipPoint;
								}
								else
								{
									if (mojoshader_textureFilterType3 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR && mojoshader_textureFilterType3 != Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC)
									{
										throw new NotImplementedException("Unhandled mipfilter type! " + mojoshader_textureFilterType3.ToString());
									}
									pipelineCache.Filter = TextureFilter.Linear;
								}
							}
						}
						flag = true;
					}
					if (flag)
					{
						pipelineCache.EndApplySampler(samplers, sampler_register);
					}
				}
				num++;
			}
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00025FD8 File Offset: 0x000241D8
		private unsafe void INTERNAL_parseEffectStruct(IntPtr effectData)
		{
			Effect.MOJOSHADER_effect* ptr = (Effect.MOJOSHADER_effect*)(void*)effectData;
			Effect.MOJOSHADER_effectParam* ptr2 = (Effect.MOJOSHADER_effectParam*)(void*)ptr->parameters;
			List<EffectParameter> list = new List<EffectParameter>();
			for (int i = 0; i < ptr->param_count; i++)
			{
				Effect.MOJOSHADER_effectParam mojoshader_effectParam = ptr2[i];
				if (mojoshader_effectParam.value.type.parameter_type != Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_VERTEXSHADER && mojoshader_effectParam.value.type.parameter_type != Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_PIXELSHADER)
				{
					if (mojoshader_effectParam.value.type.parameter_type >= Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_SAMPLER && mojoshader_effectParam.value.type.parameter_type <= Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_SAMPLERCUBE)
					{
						string text = string.Empty;
						Effect.MOJOSHADER_effectSamplerState* ptr3 = (Effect.MOJOSHADER_effectSamplerState*)(void*)mojoshader_effectParam.value.values;
						int num = 0;
						while ((long)num < (long)((ulong)mojoshader_effectParam.value.value_count))
						{
							if (ptr3[num].value.type.parameter_type >= Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_TEXTURE && ptr3[num].value.type.parameter_type <= Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_TEXTURECUBE)
							{
								Effect.MOJOSHADER_effectObject* ptr4 = (Effect.MOJOSHADER_effectObject*)(void*)ptr->objects;
								int* ptr5 = (int*)(void*)ptr3[num].value.values;
								text = Marshal.PtrToStringAnsi(ptr4[*ptr5].mapping.name);
								break;
							}
							num++;
						}
						for (int j = 0; j < list.Count; j++)
						{
							if (text.Equals(list[j].Name))
							{
								this.samplerMap[mojoshader_effectParam.value.name] = list[j];
								break;
							}
						}
					}
					else
					{
						EffectParameter effectParameter = new EffectParameter(MarshalHelper.PtrToInternedStringAnsi(mojoshader_effectParam.value.name), MarshalHelper.PtrToInternedStringAnsi(mojoshader_effectParam.value.semantic), (int)mojoshader_effectParam.value.type.rows, (int)mojoshader_effectParam.value.type.columns, (int)mojoshader_effectParam.value.type.elements, Effect.XNAClass[(int)mojoshader_effectParam.value.type.parameter_class], Effect.XNAType[(int)mojoshader_effectParam.value.type.parameter_type], new IntPtr((void*)(&ptr2[i].value.type)), this.INTERNAL_readAnnotations(mojoshader_effectParam.annotations, mojoshader_effectParam.annotation_count), mojoshader_effectParam.value.values, mojoshader_effectParam.value.value_count * 4U, this);
						if (mojoshader_effectParam.value.type.parameter_type == Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_STRING)
						{
							int* ptr6 = (int*)(void*)mojoshader_effectParam.value.values;
							effectParameter.cachedString = this.INTERNAL_GetStringFromObjectTable(*ptr6);
						}
						list.Add(effectParameter);
					}
				}
			}
			this.Parameters = new EffectParameterCollection(list);
			Effect.MOJOSHADER_effectTechnique* ptr7 = (Effect.MOJOSHADER_effectTechnique*)(void*)ptr->techniques;
			List<EffectTechnique> list2 = new List<EffectTechnique>(ptr->technique_count);
			int k = 0;
			while (k < list2.Capacity)
			{
				Effect.MOJOSHADER_effectPass* ptr8 = (Effect.MOJOSHADER_effectPass*)(void*)ptr7->passes;
				EffectPassCollection effectPassCollection;
				if (ptr7->pass_count == 1U)
				{
					effectPassCollection = new EffectPassCollection(this.INTERNAL_readPass(ref *ptr8, (IntPtr)((void*)ptr7), 0U));
				}
				else
				{
					List<EffectPass> list3 = new List<EffectPass>((int)ptr7->pass_count);
					for (int l = 0; l < list3.Capacity; l++)
					{
						list3.Add(this.INTERNAL_readPass(ref ptr8[l], (IntPtr)((void*)ptr7), (uint)l));
					}
					effectPassCollection = new EffectPassCollection(list3);
				}
				list2.Add(new EffectTechnique(MarshalHelper.PtrToInternedStringAnsi(ptr7->name), (IntPtr)((void*)ptr7), effectPassCollection, this.INTERNAL_readAnnotations(ptr7->annotations, ptr7->annotation_count)));
				k++;
				ptr7++;
			}
			this.Techniques = new EffectTechniqueCollection(list2);
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x000263C0 File Offset: 0x000245C0
		internal unsafe static EffectParameterCollection INTERNAL_readEffectParameterStructureMembers(EffectParameter parameter, IntPtr _type, Effect outer)
		{
			if (_type == IntPtr.Zero)
			{
				return new EffectParameterCollection(new List<EffectParameter>(0));
			}
			Effect.MOJOSHADER_symbolTypeInfo mojoshader_symbolTypeInfo = *(Effect.MOJOSHADER_symbolTypeInfo*)(void*)_type;
			List<EffectParameter> list = new List<EffectParameter>();
			Effect.MOJOSHADER_symbolStructMember* ptr = (Effect.MOJOSHADER_symbolStructMember*)(void*)mojoshader_symbolTypeInfo.members;
			IntPtr intPtr = IntPtr.Zero;
			int num = 0;
			while ((long)num < (long)((ulong)mojoshader_symbolTypeInfo.member_count))
			{
				uint num2 = ptr[num].info.rows * ptr[num].info.columns;
				if (ptr[num].info.elements > 0U)
				{
					num2 *= ptr[num].info.elements;
				}
				EffectParameter effectParameter = new EffectParameter(MarshalHelper.PtrToInternedStringAnsi(ptr[num].name), null, (int)ptr[num].info.rows, (int)ptr[num].info.columns, (int)ptr[num].info.elements, Effect.XNAClass[(int)ptr[num].info.parameter_class], Effect.XNAType[(int)ptr[num].info.parameter_type], null, null, parameter.values + intPtr.ToInt32(), num2 * 4U, outer);
				if (ptr[num].info.parameter_type == Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_STRING)
				{
					int* ptr2 = (int*)(void*)(parameter.values + intPtr.ToInt32());
					effectParameter.cachedString = outer.INTERNAL_GetStringFromObjectTable(*ptr2);
				}
				list.Add(effectParameter);
				intPtr += (int)(num2 * 4U);
				num++;
			}
			return new EffectParameterCollection(list);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00026598 File Offset: 0x00024798
		private unsafe string INTERNAL_GetStringFromObjectTable(int index)
		{
			Effect.MOJOSHADER_effect* ptr = (Effect.MOJOSHADER_effect*)(void*)this.effectData;
			Effect.MOJOSHADER_effectObject* ptr2 = (Effect.MOJOSHADER_effectObject*)(void*)ptr->objects;
			if (index < ptr->object_count)
			{
				return Marshal.PtrToStringAnsi(ptr2[index].stringvalue.stringvalue);
			}
			throw new InvalidOperationException("Invalid effect object index");
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x000265EB File Offset: 0x000247EB
		private EffectPass INTERNAL_readPass(ref Effect.MOJOSHADER_effectPass pass, IntPtr techPtr, uint index)
		{
			return new EffectPass(MarshalHelper.PtrToInternedStringAnsi(pass.name), this.INTERNAL_readAnnotations(pass.annotations, pass.annotation_count), this, techPtr, index);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00026614 File Offset: 0x00024814
		private unsafe EffectAnnotationCollection INTERNAL_readAnnotations(IntPtr rawAnnotations, uint numAnnotations)
		{
			if (numAnnotations == 0U)
			{
				return EffectAnnotationCollection.Empty;
			}
			Effect.MOJOSHADER_effectAnnotation* ptr = (Effect.MOJOSHADER_effectAnnotation*)(void*)rawAnnotations;
			List<EffectAnnotation> list = new List<EffectAnnotation>((int)numAnnotations);
			int num = 0;
			while ((long)num < (long)((ulong)numAnnotations))
			{
				Effect.MOJOSHADER_effectAnnotation mojoshader_effectAnnotation = ptr[num];
				EffectAnnotation effectAnnotation = new EffectAnnotation(MarshalHelper.PtrToInternedStringAnsi(mojoshader_effectAnnotation.name), MarshalHelper.PtrToInternedStringAnsi(mojoshader_effectAnnotation.semantic), (int)mojoshader_effectAnnotation.type.rows, (int)mojoshader_effectAnnotation.type.columns, Effect.XNAClass[(int)mojoshader_effectAnnotation.type.parameter_class], Effect.XNAType[(int)mojoshader_effectAnnotation.type.parameter_type], mojoshader_effectAnnotation.values);
				if (mojoshader_effectAnnotation.type.parameter_type == Effect.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_STRING)
				{
					int* ptr2 = (int*)(void*)mojoshader_effectAnnotation.values;
					effectAnnotation.cachedString = this.INTERNAL_GetStringFromObjectTable(*ptr2);
				}
				list.Add(effectAnnotation);
				num++;
			}
			return new EffectAnnotationCollection(list);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x000266F4 File Offset: 0x000248F4
		// Note: this type is marked as 'beforefieldinit'.
		static Effect()
		{
			CompareFunction[] array = new CompareFunction[9];
			array[0] = (CompareFunction)(-1);
			array[1] = CompareFunction.Never;
			array[2] = CompareFunction.Less;
			array[3] = CompareFunction.Equal;
			array[4] = CompareFunction.LessEqual;
			array[5] = CompareFunction.Greater;
			array[6] = CompareFunction.NotEqual;
			array[7] = CompareFunction.GreaterEqual;
			Effect.XNACompare = array;
			Effect.XNAStencilOp = new StencilOperation[]
			{
				(StencilOperation)(-1),
				StencilOperation.Keep,
				StencilOperation.Zero,
				StencilOperation.Replace,
				StencilOperation.IncrementSaturation,
				StencilOperation.DecrementSaturation,
				StencilOperation.Invert,
				StencilOperation.Increment,
				StencilOperation.Decrement
			};
			Effect.XNAAddress = new TextureAddressMode[]
			{
				(TextureAddressMode)(-1),
				TextureAddressMode.Wrap,
				TextureAddressMode.Mirror,
				TextureAddressMode.Clamp
			};
			Effect.XNAMag = new Effect.MOJOSHADER_textureFilterType[]
			{
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR
			};
			Effect.XNAMin = new Effect.MOJOSHADER_textureFilterType[]
			{
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT
			};
			Effect.XNAMip = new Effect.MOJOSHADER_textureFilterType[]
			{
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_ANISOTROPIC,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_LINEAR,
				Effect.MOJOSHADER_textureFilterType.MOJOSHADER_TEXTUREFILTER_POINT
			};
		}

		// Token: 0x040007CD RID: 1997
		private EffectTechnique INTERNAL_currentTechnique;

		// Token: 0x040007CE RID: 1998
		[CompilerGenerated]
		private EffectParameterCollection <Parameters>k__BackingField;

		// Token: 0x040007CF RID: 1999
		[CompilerGenerated]
		private EffectTechniqueCollection <Techniques>k__BackingField;

		// Token: 0x040007D0 RID: 2000
		internal IntPtr glEffect;

		// Token: 0x040007D1 RID: 2001
		private Dictionary<IntPtr, EffectParameter> samplerMap = new Dictionary<IntPtr, EffectParameter>(new Effect.IntPtrBoxlessComparer());

		// Token: 0x040007D2 RID: 2002
		private IntPtr effectData;

		// Token: 0x040007D3 RID: 2003
		private static readonly EffectParameterType[] XNAType = new EffectParameterType[]
		{
			EffectParameterType.Void,
			EffectParameterType.Bool,
			EffectParameterType.Int32,
			EffectParameterType.Single,
			EffectParameterType.String,
			EffectParameterType.Texture,
			EffectParameterType.Texture1D,
			EffectParameterType.Texture2D,
			EffectParameterType.Texture3D,
			EffectParameterType.TextureCube
		};

		// Token: 0x040007D4 RID: 2004
		private static readonly EffectParameterClass[] XNAClass = new EffectParameterClass[]
		{
			EffectParameterClass.Scalar,
			EffectParameterClass.Vector,
			EffectParameterClass.Matrix,
			EffectParameterClass.Matrix,
			EffectParameterClass.Object,
			EffectParameterClass.Struct
		};

		// Token: 0x040007D5 RID: 2005
		private static readonly Blend[] XNABlend = new Blend[]
		{
			(Blend)(-1),
			Blend.Zero,
			Blend.One,
			Blend.SourceColor,
			Blend.InverseSourceColor,
			Blend.SourceAlpha,
			Blend.InverseSourceAlpha,
			Blend.DestinationAlpha,
			Blend.InverseDestinationAlpha,
			Blend.DestinationColor,
			Blend.InverseDestinationColor,
			Blend.SourceAlphaSaturation,
			(Blend)(-1),
			(Blend)(-1),
			Blend.BlendFactor,
			Blend.InverseBlendFactor
		};

		// Token: 0x040007D6 RID: 2006
		private static readonly BlendFunction[] XNABlendOp = new BlendFunction[]
		{
			(BlendFunction)(-1),
			BlendFunction.Add,
			BlendFunction.Subtract,
			BlendFunction.ReverseSubtract,
			BlendFunction.Min,
			BlendFunction.Max
		};

		// Token: 0x040007D7 RID: 2007
		private static readonly CompareFunction[] XNACompare;

		// Token: 0x040007D8 RID: 2008
		private static readonly StencilOperation[] XNAStencilOp;

		// Token: 0x040007D9 RID: 2009
		private static readonly TextureAddressMode[] XNAAddress;

		// Token: 0x040007DA RID: 2010
		private static readonly Effect.MOJOSHADER_textureFilterType[] XNAMag;

		// Token: 0x040007DB RID: 2011
		private static readonly Effect.MOJOSHADER_textureFilterType[] XNAMin;

		// Token: 0x040007DC RID: 2012
		private static readonly Effect.MOJOSHADER_textureFilterType[] XNAMip;

		// Token: 0x020003A3 RID: 931
		private class IntPtrBoxlessComparer : IEqualityComparer<IntPtr>
		{
			// Token: 0x06001ABE RID: 6846 RVA: 0x0003F791 File Offset: 0x0003D991
			public bool Equals(IntPtr x, IntPtr y)
			{
				return x == y;
			}

			// Token: 0x06001ABF RID: 6847 RVA: 0x0003F79A File Offset: 0x0003D99A
			public int GetHashCode(IntPtr obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x06001AC0 RID: 6848 RVA: 0x000136F5 File Offset: 0x000118F5
			public IntPtrBoxlessComparer()
			{
			}
		}

		// Token: 0x020003A4 RID: 932
		private enum MOJOSHADER_symbolClass
		{
			// Token: 0x04001C4D RID: 7245
			MOJOSHADER_SYMCLASS_SCALAR,
			// Token: 0x04001C4E RID: 7246
			MOJOSHADER_SYMCLASS_VECTOR,
			// Token: 0x04001C4F RID: 7247
			MOJOSHADER_SYMCLASS_MATRIX_ROWS,
			// Token: 0x04001C50 RID: 7248
			MOJOSHADER_SYMCLASS_MATRIX_COLUMNS,
			// Token: 0x04001C51 RID: 7249
			MOJOSHADER_SYMCLASS_OBJECT,
			// Token: 0x04001C52 RID: 7250
			MOJOSHADER_SYMCLASS_STRUCT,
			// Token: 0x04001C53 RID: 7251
			MOJOSHADER_SYMCLASS_TOTAL
		}

		// Token: 0x020003A5 RID: 933
		private enum MOJOSHADER_symbolType
		{
			// Token: 0x04001C55 RID: 7253
			MOJOSHADER_SYMTYPE_VOID,
			// Token: 0x04001C56 RID: 7254
			MOJOSHADER_SYMTYPE_BOOL,
			// Token: 0x04001C57 RID: 7255
			MOJOSHADER_SYMTYPE_INT,
			// Token: 0x04001C58 RID: 7256
			MOJOSHADER_SYMTYPE_FLOAT,
			// Token: 0x04001C59 RID: 7257
			MOJOSHADER_SYMTYPE_STRING,
			// Token: 0x04001C5A RID: 7258
			MOJOSHADER_SYMTYPE_TEXTURE,
			// Token: 0x04001C5B RID: 7259
			MOJOSHADER_SYMTYPE_TEXTURE1D,
			// Token: 0x04001C5C RID: 7260
			MOJOSHADER_SYMTYPE_TEXTURE2D,
			// Token: 0x04001C5D RID: 7261
			MOJOSHADER_SYMTYPE_TEXTURE3D,
			// Token: 0x04001C5E RID: 7262
			MOJOSHADER_SYMTYPE_TEXTURECUBE,
			// Token: 0x04001C5F RID: 7263
			MOJOSHADER_SYMTYPE_SAMPLER,
			// Token: 0x04001C60 RID: 7264
			MOJOSHADER_SYMTYPE_SAMPLER1D,
			// Token: 0x04001C61 RID: 7265
			MOJOSHADER_SYMTYPE_SAMPLER2D,
			// Token: 0x04001C62 RID: 7266
			MOJOSHADER_SYMTYPE_SAMPLER3D,
			// Token: 0x04001C63 RID: 7267
			MOJOSHADER_SYMTYPE_SAMPLERCUBE,
			// Token: 0x04001C64 RID: 7268
			MOJOSHADER_SYMTYPE_PIXELSHADER,
			// Token: 0x04001C65 RID: 7269
			MOJOSHADER_SYMTYPE_VERTEXSHADER,
			// Token: 0x04001C66 RID: 7270
			MOJOSHADER_SYMTYPE_PIXELFRAGMENT,
			// Token: 0x04001C67 RID: 7271
			MOJOSHADER_SYMTYPE_VERTEXFRAGMENT,
			// Token: 0x04001C68 RID: 7272
			MOJOSHADER_SYMTYPE_UNSUPPORTED,
			// Token: 0x04001C69 RID: 7273
			MOJOSHADER_SYMTYPE_TOTAL
		}

		// Token: 0x020003A6 RID: 934
		private struct MOJOSHADER_symbolTypeInfo
		{
			// Token: 0x04001C6A RID: 7274
			public Effect.MOJOSHADER_symbolClass parameter_class;

			// Token: 0x04001C6B RID: 7275
			public Effect.MOJOSHADER_symbolType parameter_type;

			// Token: 0x04001C6C RID: 7276
			public uint rows;

			// Token: 0x04001C6D RID: 7277
			public uint columns;

			// Token: 0x04001C6E RID: 7278
			public uint elements;

			// Token: 0x04001C6F RID: 7279
			public uint member_count;

			// Token: 0x04001C70 RID: 7280
			public IntPtr members;
		}

		// Token: 0x020003A7 RID: 935
		private struct MOJOSHADER_symbolStructMember
		{
			// Token: 0x04001C71 RID: 7281
			public IntPtr name;

			// Token: 0x04001C72 RID: 7282
			public Effect.MOJOSHADER_symbolTypeInfo info;
		}

		// Token: 0x020003A8 RID: 936
		private enum MOJOSHADER_renderStateType
		{
			// Token: 0x04001C74 RID: 7284
			MOJOSHADER_RS_ZENABLE,
			// Token: 0x04001C75 RID: 7285
			MOJOSHADER_RS_FILLMODE,
			// Token: 0x04001C76 RID: 7286
			MOJOSHADER_RS_SHADEMODE,
			// Token: 0x04001C77 RID: 7287
			MOJOSHADER_RS_ZWRITEENABLE,
			// Token: 0x04001C78 RID: 7288
			MOJOSHADER_RS_ALPHATESTENABLE,
			// Token: 0x04001C79 RID: 7289
			MOJOSHADER_RS_LASTPIXEL,
			// Token: 0x04001C7A RID: 7290
			MOJOSHADER_RS_SRCBLEND,
			// Token: 0x04001C7B RID: 7291
			MOJOSHADER_RS_DESTBLEND,
			// Token: 0x04001C7C RID: 7292
			MOJOSHADER_RS_CULLMODE,
			// Token: 0x04001C7D RID: 7293
			MOJOSHADER_RS_ZFUNC,
			// Token: 0x04001C7E RID: 7294
			MOJOSHADER_RS_ALPHAREF,
			// Token: 0x04001C7F RID: 7295
			MOJOSHADER_RS_ALPHAFUNC,
			// Token: 0x04001C80 RID: 7296
			MOJOSHADER_RS_DITHERENABLE,
			// Token: 0x04001C81 RID: 7297
			MOJOSHADER_RS_ALPHABLENDENABLE,
			// Token: 0x04001C82 RID: 7298
			MOJOSHADER_RS_FOGENABLE,
			// Token: 0x04001C83 RID: 7299
			MOJOSHADER_RS_SPECULARENABLE,
			// Token: 0x04001C84 RID: 7300
			MOJOSHADER_RS_FOGCOLOR,
			// Token: 0x04001C85 RID: 7301
			MOJOSHADER_RS_FOGTABLEMODE,
			// Token: 0x04001C86 RID: 7302
			MOJOSHADER_RS_FOGSTART,
			// Token: 0x04001C87 RID: 7303
			MOJOSHADER_RS_FOGEND,
			// Token: 0x04001C88 RID: 7304
			MOJOSHADER_RS_FOGDENSITY,
			// Token: 0x04001C89 RID: 7305
			MOJOSHADER_RS_RANGEFOGENABLE,
			// Token: 0x04001C8A RID: 7306
			MOJOSHADER_RS_STENCILENABLE,
			// Token: 0x04001C8B RID: 7307
			MOJOSHADER_RS_STENCILFAIL,
			// Token: 0x04001C8C RID: 7308
			MOJOSHADER_RS_STENCILZFAIL,
			// Token: 0x04001C8D RID: 7309
			MOJOSHADER_RS_STENCILPASS,
			// Token: 0x04001C8E RID: 7310
			MOJOSHADER_RS_STENCILFUNC,
			// Token: 0x04001C8F RID: 7311
			MOJOSHADER_RS_STENCILREF,
			// Token: 0x04001C90 RID: 7312
			MOJOSHADER_RS_STENCILMASK,
			// Token: 0x04001C91 RID: 7313
			MOJOSHADER_RS_STENCILWRITEMASK,
			// Token: 0x04001C92 RID: 7314
			MOJOSHADER_RS_TEXTUREFACTOR,
			// Token: 0x04001C93 RID: 7315
			MOJOSHADER_RS_WRAP0,
			// Token: 0x04001C94 RID: 7316
			MOJOSHADER_RS_WRAP1,
			// Token: 0x04001C95 RID: 7317
			MOJOSHADER_RS_WRAP2,
			// Token: 0x04001C96 RID: 7318
			MOJOSHADER_RS_WRAP3,
			// Token: 0x04001C97 RID: 7319
			MOJOSHADER_RS_WRAP4,
			// Token: 0x04001C98 RID: 7320
			MOJOSHADER_RS_WRAP5,
			// Token: 0x04001C99 RID: 7321
			MOJOSHADER_RS_WRAP6,
			// Token: 0x04001C9A RID: 7322
			MOJOSHADER_RS_WRAP7,
			// Token: 0x04001C9B RID: 7323
			MOJOSHADER_RS_WRAP8,
			// Token: 0x04001C9C RID: 7324
			MOJOSHADER_RS_WRAP9,
			// Token: 0x04001C9D RID: 7325
			MOJOSHADER_RS_WRAP10,
			// Token: 0x04001C9E RID: 7326
			MOJOSHADER_RS_WRAP11,
			// Token: 0x04001C9F RID: 7327
			MOJOSHADER_RS_WRAP12,
			// Token: 0x04001CA0 RID: 7328
			MOJOSHADER_RS_WRAP13,
			// Token: 0x04001CA1 RID: 7329
			MOJOSHADER_RS_WRAP14,
			// Token: 0x04001CA2 RID: 7330
			MOJOSHADER_RS_WRAP15,
			// Token: 0x04001CA3 RID: 7331
			MOJOSHADER_RS_CLIPPING,
			// Token: 0x04001CA4 RID: 7332
			MOJOSHADER_RS_LIGHTING,
			// Token: 0x04001CA5 RID: 7333
			MOJOSHADER_RS_AMBIENT,
			// Token: 0x04001CA6 RID: 7334
			MOJOSHADER_RS_FOGVERTEXMODE,
			// Token: 0x04001CA7 RID: 7335
			MOJOSHADER_RS_COLORVERTEX,
			// Token: 0x04001CA8 RID: 7336
			MOJOSHADER_RS_LOCALVIEWER,
			// Token: 0x04001CA9 RID: 7337
			MOJOSHADER_RS_NORMALIZENORMALS,
			// Token: 0x04001CAA RID: 7338
			MOJOSHADER_RS_DIFFUSEMATERIALSOURCE,
			// Token: 0x04001CAB RID: 7339
			MOJOSHADER_RS_SPECULARMATERIALSOURCE,
			// Token: 0x04001CAC RID: 7340
			MOJOSHADER_RS_AMBIENTMATERIALSOURCE,
			// Token: 0x04001CAD RID: 7341
			MOJOSHADER_RS_EMISSIVEMATERIALSOURCE,
			// Token: 0x04001CAE RID: 7342
			MOJOSHADER_RS_VERTEXBLEND,
			// Token: 0x04001CAF RID: 7343
			MOJOSHADER_RS_CLIPPLANEENABLE,
			// Token: 0x04001CB0 RID: 7344
			MOJOSHADER_RS_POINTSIZE,
			// Token: 0x04001CB1 RID: 7345
			MOJOSHADER_RS_POINTSIZE_MIN,
			// Token: 0x04001CB2 RID: 7346
			MOJOSHADER_RS_POINTSPRITEENABLE,
			// Token: 0x04001CB3 RID: 7347
			MOJOSHADER_RS_POINTSCALEENABLE,
			// Token: 0x04001CB4 RID: 7348
			MOJOSHADER_RS_POINTSCALE_A,
			// Token: 0x04001CB5 RID: 7349
			MOJOSHADER_RS_POINTSCALE_B,
			// Token: 0x04001CB6 RID: 7350
			MOJOSHADER_RS_POINTSCALE_C,
			// Token: 0x04001CB7 RID: 7351
			MOJOSHADER_RS_MULTISAMPLEANTIALIAS,
			// Token: 0x04001CB8 RID: 7352
			MOJOSHADER_RS_MULTISAMPLEMASK,
			// Token: 0x04001CB9 RID: 7353
			MOJOSHADER_RS_PATCHEDGESTYLE,
			// Token: 0x04001CBA RID: 7354
			MOJOSHADER_RS_DEBUGMONITORTOKEN,
			// Token: 0x04001CBB RID: 7355
			MOJOSHADER_RS_POINTSIZE_MAX,
			// Token: 0x04001CBC RID: 7356
			MOJOSHADER_RS_INDEXEDVERTEXBLENDENABLE,
			// Token: 0x04001CBD RID: 7357
			MOJOSHADER_RS_COLORWRITEENABLE,
			// Token: 0x04001CBE RID: 7358
			MOJOSHADER_RS_TWEENFACTOR,
			// Token: 0x04001CBF RID: 7359
			MOJOSHADER_RS_BLENDOP,
			// Token: 0x04001CC0 RID: 7360
			MOJOSHADER_RS_POSITIONDEGREE,
			// Token: 0x04001CC1 RID: 7361
			MOJOSHADER_RS_NORMALDEGREE,
			// Token: 0x04001CC2 RID: 7362
			MOJOSHADER_RS_SCISSORTESTENABLE,
			// Token: 0x04001CC3 RID: 7363
			MOJOSHADER_RS_SLOPESCALEDEPTHBIAS,
			// Token: 0x04001CC4 RID: 7364
			MOJOSHADER_RS_ANTIALIASEDLINEENABLE,
			// Token: 0x04001CC5 RID: 7365
			MOJOSHADER_RS_MINTESSELLATIONLEVEL,
			// Token: 0x04001CC6 RID: 7366
			MOJOSHADER_RS_MAXTESSELLATIONLEVEL,
			// Token: 0x04001CC7 RID: 7367
			MOJOSHADER_RS_ADAPTIVETESS_X,
			// Token: 0x04001CC8 RID: 7368
			MOJOSHADER_RS_ADAPTIVETESS_Y,
			// Token: 0x04001CC9 RID: 7369
			MOJOSHADER_RS_ADAPTIVETESS_Z,
			// Token: 0x04001CCA RID: 7370
			MOJOSHADER_RS_ADAPTIVETESS_W,
			// Token: 0x04001CCB RID: 7371
			MOJOSHADER_RS_ENABLEADAPTIVETESSELLATION,
			// Token: 0x04001CCC RID: 7372
			MOJOSHADER_RS_TWOSIDEDSTENCILMODE,
			// Token: 0x04001CCD RID: 7373
			MOJOSHADER_RS_CCW_STENCILFAIL,
			// Token: 0x04001CCE RID: 7374
			MOJOSHADER_RS_CCW_STENCILZFAIL,
			// Token: 0x04001CCF RID: 7375
			MOJOSHADER_RS_CCW_STENCILPASS,
			// Token: 0x04001CD0 RID: 7376
			MOJOSHADER_RS_CCW_STENCILFUNC,
			// Token: 0x04001CD1 RID: 7377
			MOJOSHADER_RS_COLORWRITEENABLE1,
			// Token: 0x04001CD2 RID: 7378
			MOJOSHADER_RS_COLORWRITEENABLE2,
			// Token: 0x04001CD3 RID: 7379
			MOJOSHADER_RS_COLORWRITEENABLE3,
			// Token: 0x04001CD4 RID: 7380
			MOJOSHADER_RS_BLENDFACTOR,
			// Token: 0x04001CD5 RID: 7381
			MOJOSHADER_RS_SRGBWRITEENABLE,
			// Token: 0x04001CD6 RID: 7382
			MOJOSHADER_RS_DEPTHBIAS,
			// Token: 0x04001CD7 RID: 7383
			MOJOSHADER_RS_SEPARATEALPHABLENDENABLE,
			// Token: 0x04001CD8 RID: 7384
			MOJOSHADER_RS_SRCBLENDALPHA,
			// Token: 0x04001CD9 RID: 7385
			MOJOSHADER_RS_DESTBLENDALPHA,
			// Token: 0x04001CDA RID: 7386
			MOJOSHADER_RS_BLENDOPALPHA,
			// Token: 0x04001CDB RID: 7387
			MOJOSHADER_RS_VERTEXSHADER = 146,
			// Token: 0x04001CDC RID: 7388
			MOJOSHADER_RS_PIXELSHADER
		}

		// Token: 0x020003A9 RID: 937
		private enum MOJOSHADER_zBufferType
		{
			// Token: 0x04001CDE RID: 7390
			MOJOSHADER_ZB_FALSE,
			// Token: 0x04001CDF RID: 7391
			MOJOSHADER_ZB_TRUE,
			// Token: 0x04001CE0 RID: 7392
			MOJOSHADER_ZB_USEW
		}

		// Token: 0x020003AA RID: 938
		private enum MOJOSHADER_fillMode
		{
			// Token: 0x04001CE2 RID: 7394
			MOJOSHADER_FILL_POINT = 1,
			// Token: 0x04001CE3 RID: 7395
			MOJOSHADER_FILL_WIREFRAME,
			// Token: 0x04001CE4 RID: 7396
			MOJOSHADER_FILL_SOLID
		}

		// Token: 0x020003AB RID: 939
		private enum MOJOSHADER_blendMode
		{
			// Token: 0x04001CE6 RID: 7398
			MOJOSHADER_BLEND_ZERO = 1,
			// Token: 0x04001CE7 RID: 7399
			MOJOSHADER_BLEND_ONE,
			// Token: 0x04001CE8 RID: 7400
			MOJOSHADER_BLEND_SRCCOLOR,
			// Token: 0x04001CE9 RID: 7401
			MOJOSHADER_BLEND_INVSRCCOLOR,
			// Token: 0x04001CEA RID: 7402
			MOJOSHADER_BLEND_SRCALPHA,
			// Token: 0x04001CEB RID: 7403
			MOJOSHADER_BLEND_INVSRCALPHA,
			// Token: 0x04001CEC RID: 7404
			MOJOSHADER_BLEND_DESTALPHA,
			// Token: 0x04001CED RID: 7405
			MOJOSHADER_BLEND_INVDESTALPHA,
			// Token: 0x04001CEE RID: 7406
			MOJOSHADER_BLEND_DESTCOLOR,
			// Token: 0x04001CEF RID: 7407
			MOJOSHADER_BLEND_INVDESTCOLOR,
			// Token: 0x04001CF0 RID: 7408
			MOJOSHADER_BLEND_SRCALPHASAT,
			// Token: 0x04001CF1 RID: 7409
			MOJOSHADER_BLEND_BOTHSRCALPHA,
			// Token: 0x04001CF2 RID: 7410
			MOJOSHADER_BLEND_BOTHINVSRCALPHA,
			// Token: 0x04001CF3 RID: 7411
			MOJOSHADER_BLEND_BLENDFACTOR,
			// Token: 0x04001CF4 RID: 7412
			MOJOSHADER_BLEND_INVBLENDFACTOR,
			// Token: 0x04001CF5 RID: 7413
			MOJOSHADER_BLEND_SRCCOLOR2,
			// Token: 0x04001CF6 RID: 7414
			MOJOSHADER_BLEND_INVSRCCOLOR2
		}

		// Token: 0x020003AC RID: 940
		private enum MOJOSHADER_cullMode
		{
			// Token: 0x04001CF8 RID: 7416
			MOJOSHADER_CULL_NONE = 1,
			// Token: 0x04001CF9 RID: 7417
			MOJOSHADER_CULL_CW,
			// Token: 0x04001CFA RID: 7418
			MOJOSHADER_CULL_CCW
		}

		// Token: 0x020003AD RID: 941
		private enum MOJOSHADER_compareFunc
		{
			// Token: 0x04001CFC RID: 7420
			MOJOSHADER_CMP_NEVER = 1,
			// Token: 0x04001CFD RID: 7421
			MOJOSHADER_CMP_LESS,
			// Token: 0x04001CFE RID: 7422
			MOJOSHADER_CMP_EQUAL,
			// Token: 0x04001CFF RID: 7423
			MOJOSHADER_CMP_LESSEQUAL,
			// Token: 0x04001D00 RID: 7424
			MOJOSHADER_CMP_GREATER,
			// Token: 0x04001D01 RID: 7425
			MOJOSHADER_CMP_NOTEQUAL,
			// Token: 0x04001D02 RID: 7426
			MOJOSHADER_CMP_GREATEREQUAL,
			// Token: 0x04001D03 RID: 7427
			MOJOSHADER_CMP_ALWAYS
		}

		// Token: 0x020003AE RID: 942
		private enum MOJOSHADER_stencilOp
		{
			// Token: 0x04001D05 RID: 7429
			MOJOSHADER_STENCILOP_KEEP = 1,
			// Token: 0x04001D06 RID: 7430
			MOJOSHADER_STENCILOP_ZERO,
			// Token: 0x04001D07 RID: 7431
			MOJOSHADER_STENCILOP_REPLACE,
			// Token: 0x04001D08 RID: 7432
			MOJOSHADER_STENCILOP_INCRSAT,
			// Token: 0x04001D09 RID: 7433
			MOJOSHADER_STENCILOP_DECRSAT,
			// Token: 0x04001D0A RID: 7434
			MOJOSHADER_STENCILOP_INVERT,
			// Token: 0x04001D0B RID: 7435
			MOJOSHADER_STENCILOP_INCR,
			// Token: 0x04001D0C RID: 7436
			MOJOSHADER_STENCILOP_DECR
		}

		// Token: 0x020003AF RID: 943
		private enum MOJOSHADER_blendOp
		{
			// Token: 0x04001D0E RID: 7438
			MOJOSHADER_BLENDOP_ADD = 1,
			// Token: 0x04001D0F RID: 7439
			MOJOSHADER_BLENDOP_SUBTRACT,
			// Token: 0x04001D10 RID: 7440
			MOJOSHADER_BLENDOP_REVSUBTRACT,
			// Token: 0x04001D11 RID: 7441
			MOJOSHADER_BLENDOP_MIN,
			// Token: 0x04001D12 RID: 7442
			MOJOSHADER_BLENDOP_MAX
		}

		// Token: 0x020003B0 RID: 944
		private enum MOJOSHADER_samplerStateType
		{
			// Token: 0x04001D14 RID: 7444
			MOJOSHADER_SAMP_UNKNOWN0,
			// Token: 0x04001D15 RID: 7445
			MOJOSHADER_SAMP_UNKNOWN1,
			// Token: 0x04001D16 RID: 7446
			MOJOSHADER_SAMP_UNKNOWN2,
			// Token: 0x04001D17 RID: 7447
			MOJOSHADER_SAMP_UNKNOWN3,
			// Token: 0x04001D18 RID: 7448
			MOJOSHADER_SAMP_TEXTURE,
			// Token: 0x04001D19 RID: 7449
			MOJOSHADER_SAMP_ADDRESSU,
			// Token: 0x04001D1A RID: 7450
			MOJOSHADER_SAMP_ADDRESSV,
			// Token: 0x04001D1B RID: 7451
			MOJOSHADER_SAMP_ADDRESSW,
			// Token: 0x04001D1C RID: 7452
			MOJOSHADER_SAMP_BORDERCOLOR,
			// Token: 0x04001D1D RID: 7453
			MOJOSHADER_SAMP_MAGFILTER,
			// Token: 0x04001D1E RID: 7454
			MOJOSHADER_SAMP_MINFILTER,
			// Token: 0x04001D1F RID: 7455
			MOJOSHADER_SAMP_MIPFILTER,
			// Token: 0x04001D20 RID: 7456
			MOJOSHADER_SAMP_MIPMAPLODBIAS,
			// Token: 0x04001D21 RID: 7457
			MOJOSHADER_SAMP_MAXMIPLEVEL,
			// Token: 0x04001D22 RID: 7458
			MOJOSHADER_SAMP_MAXANISOTROPY,
			// Token: 0x04001D23 RID: 7459
			MOJOSHADER_SAMP_SRGBTEXTURE,
			// Token: 0x04001D24 RID: 7460
			MOJOSHADER_SAMP_ELEMENTINDEX,
			// Token: 0x04001D25 RID: 7461
			MOJOSHADER_SAMP_DMAPOFFSET
		}

		// Token: 0x020003B1 RID: 945
		private enum MOJOSHADER_textureAddress
		{
			// Token: 0x04001D27 RID: 7463
			MOJOSHADER_TADDRESS_WRAP = 1,
			// Token: 0x04001D28 RID: 7464
			MOJOSHADER_TADDRESS_MIRROR,
			// Token: 0x04001D29 RID: 7465
			MOJOSHADER_TADDRESS_CLAMP,
			// Token: 0x04001D2A RID: 7466
			MOJOSHADER_TADDRESS_BORDER,
			// Token: 0x04001D2B RID: 7467
			MOJOSHADER_TADDRESS_MIRRORONCE
		}

		// Token: 0x020003B2 RID: 946
		private enum MOJOSHADER_textureFilterType
		{
			// Token: 0x04001D2D RID: 7469
			MOJOSHADER_TEXTUREFILTER_NONE,
			// Token: 0x04001D2E RID: 7470
			MOJOSHADER_TEXTUREFILTER_POINT,
			// Token: 0x04001D2F RID: 7471
			MOJOSHADER_TEXTUREFILTER_LINEAR,
			// Token: 0x04001D30 RID: 7472
			MOJOSHADER_TEXTUREFILTER_ANISOTROPIC,
			// Token: 0x04001D31 RID: 7473
			MOJOSHADER_TEXTUREFILTER_PYRAMIDALQUAD,
			// Token: 0x04001D32 RID: 7474
			MOJOSHADER_TEXTUREFILTER_GAUSSIANQUAD,
			// Token: 0x04001D33 RID: 7475
			MOJOSHADER_TEXTUREFILTER_CONVOLUTIONMONO
		}

		// Token: 0x020003B3 RID: 947
		private struct MOJOSHADER_effectValue
		{
			// Token: 0x04001D34 RID: 7476
			public IntPtr name;

			// Token: 0x04001D35 RID: 7477
			public IntPtr semantic;

			// Token: 0x04001D36 RID: 7478
			public Effect.MOJOSHADER_symbolTypeInfo type;

			// Token: 0x04001D37 RID: 7479
			public uint value_count;

			// Token: 0x04001D38 RID: 7480
			public IntPtr values;
		}

		// Token: 0x020003B4 RID: 948
		private struct MOJOSHADER_effectState
		{
			// Token: 0x04001D39 RID: 7481
			public Effect.MOJOSHADER_renderStateType type;

			// Token: 0x04001D3A RID: 7482
			public Effect.MOJOSHADER_effectValue value;
		}

		// Token: 0x020003B5 RID: 949
		private struct MOJOSHADER_effectSamplerState
		{
			// Token: 0x04001D3B RID: 7483
			public Effect.MOJOSHADER_samplerStateType type;

			// Token: 0x04001D3C RID: 7484
			public Effect.MOJOSHADER_effectValue value;
		}

		// Token: 0x020003B6 RID: 950
		private struct MOJOSHADER_effectAnnotation
		{
			// Token: 0x04001D3D RID: 7485
			public IntPtr name;

			// Token: 0x04001D3E RID: 7486
			public IntPtr semantic;

			// Token: 0x04001D3F RID: 7487
			public Effect.MOJOSHADER_symbolTypeInfo type;

			// Token: 0x04001D40 RID: 7488
			public uint value_count;

			// Token: 0x04001D41 RID: 7489
			public IntPtr values;
		}

		// Token: 0x020003B7 RID: 951
		private struct MOJOSHADER_effectParam
		{
			// Token: 0x04001D42 RID: 7490
			public Effect.MOJOSHADER_effectValue value;

			// Token: 0x04001D43 RID: 7491
			public uint annotation_count;

			// Token: 0x04001D44 RID: 7492
			public IntPtr annotations;
		}

		// Token: 0x020003B8 RID: 952
		private struct MOJOSHADER_effectPass
		{
			// Token: 0x04001D45 RID: 7493
			public IntPtr name;

			// Token: 0x04001D46 RID: 7494
			public uint state_count;

			// Token: 0x04001D47 RID: 7495
			public IntPtr states;

			// Token: 0x04001D48 RID: 7496
			public uint annotation_count;

			// Token: 0x04001D49 RID: 7497
			public IntPtr annotations;
		}

		// Token: 0x020003B9 RID: 953
		private struct MOJOSHADER_effectTechnique
		{
			// Token: 0x04001D4A RID: 7498
			public IntPtr name;

			// Token: 0x04001D4B RID: 7499
			public uint pass_count;

			// Token: 0x04001D4C RID: 7500
			public IntPtr passes;

			// Token: 0x04001D4D RID: 7501
			public uint annotation_count;

			// Token: 0x04001D4E RID: 7502
			public IntPtr annotations;
		}

		// Token: 0x020003BA RID: 954
		private struct MOJOSHADER_effectShader
		{
			// Token: 0x04001D4F RID: 7503
			public Effect.MOJOSHADER_symbolType type;

			// Token: 0x04001D50 RID: 7504
			public uint technique;

			// Token: 0x04001D51 RID: 7505
			public uint pass;

			// Token: 0x04001D52 RID: 7506
			public uint is_preshader;

			// Token: 0x04001D53 RID: 7507
			public uint preshader_param_count;

			// Token: 0x04001D54 RID: 7508
			public IntPtr preshader_params;

			// Token: 0x04001D55 RID: 7509
			public uint param_count;

			// Token: 0x04001D56 RID: 7510
			public IntPtr parameters;

			// Token: 0x04001D57 RID: 7511
			public uint sampler_count;

			// Token: 0x04001D58 RID: 7512
			public IntPtr samplers;

			// Token: 0x04001D59 RID: 7513
			public IntPtr shader;
		}

		// Token: 0x020003BB RID: 955
		private struct MOJOSHADER_effectSamplerMap
		{
			// Token: 0x04001D5A RID: 7514
			public Effect.MOJOSHADER_symbolType type;

			// Token: 0x04001D5B RID: 7515
			public IntPtr name;
		}

		// Token: 0x020003BC RID: 956
		private struct MOJOSHADER_effectString
		{
			// Token: 0x04001D5C RID: 7516
			public Effect.MOJOSHADER_symbolType type;

			// Token: 0x04001D5D RID: 7517
			public IntPtr stringvalue;
		}

		// Token: 0x020003BD RID: 957
		private struct MOJOSHADER_effectTexture
		{
			// Token: 0x04001D5E RID: 7518
			public Effect.MOJOSHADER_symbolType type;
		}

		// Token: 0x020003BE RID: 958
		[StructLayout(LayoutKind.Explicit)]
		private struct MOJOSHADER_effectObject
		{
			// Token: 0x04001D5F RID: 7519
			[FieldOffset(0)]
			public Effect.MOJOSHADER_symbolType type;

			// Token: 0x04001D60 RID: 7520
			[FieldOffset(0)]
			public Effect.MOJOSHADER_effectShader shader;

			// Token: 0x04001D61 RID: 7521
			[FieldOffset(0)]
			public Effect.MOJOSHADER_effectSamplerMap mapping;

			// Token: 0x04001D62 RID: 7522
			[FieldOffset(0)]
			public Effect.MOJOSHADER_effectString stringvalue;

			// Token: 0x04001D63 RID: 7523
			[FieldOffset(0)]
			public Effect.MOJOSHADER_effectTexture texture;
		}

		// Token: 0x020003BF RID: 959
		private struct MOJOSHADER_samplerStateRegister
		{
			// Token: 0x04001D64 RID: 7524
			public IntPtr sampler_name;

			// Token: 0x04001D65 RID: 7525
			public uint sampler_register;

			// Token: 0x04001D66 RID: 7526
			public uint sampler_state_count;

			// Token: 0x04001D67 RID: 7527
			public IntPtr sampler_states;
		}

		// Token: 0x020003C0 RID: 960
		internal struct MOJOSHADER_effectStateChanges
		{
			// Token: 0x04001D68 RID: 7528
			public uint render_state_change_count;

			// Token: 0x04001D69 RID: 7529
			public IntPtr render_state_changes;

			// Token: 0x04001D6A RID: 7530
			public uint sampler_state_change_count;

			// Token: 0x04001D6B RID: 7531
			public IntPtr sampler_state_changes;

			// Token: 0x04001D6C RID: 7532
			public uint vertex_sampler_state_change_count;

			// Token: 0x04001D6D RID: 7533
			public IntPtr vertex_sampler_state_changes;
		}

		// Token: 0x020003C1 RID: 961
		private struct MOJOSHADER_effect
		{
			// Token: 0x04001D6E RID: 7534
			public int error_count;

			// Token: 0x04001D6F RID: 7535
			public IntPtr errors;

			// Token: 0x04001D70 RID: 7536
			public int param_count;

			// Token: 0x04001D71 RID: 7537
			public IntPtr parameters;

			// Token: 0x04001D72 RID: 7538
			public int technique_count;

			// Token: 0x04001D73 RID: 7539
			public IntPtr techniques;

			// Token: 0x04001D74 RID: 7540
			public int object_count;

			// Token: 0x04001D75 RID: 7541
			public IntPtr objects;
		}
	}
}
