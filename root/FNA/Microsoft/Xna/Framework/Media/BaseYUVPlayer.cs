using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000055 RID: 85
	internal abstract class BaseYUVPlayer : IDisposable
	{
		// Token: 0x06000F55 RID: 3925 RVA: 0x0001FE04 File Offset: 0x0001E004
		protected unsafe void GL_initialize(byte[] shaderProgramBytes)
		{
			this.shaderProgram = new Effect(this.currentDevice, shaderProgramBytes);
			this.stateChangesPtr = FNAPlatform.Malloc(sizeof(Effect.MOJOSHADER_effectStateChanges));
			this.vertBuffer = new VertexBufferBinding(new VertexBuffer(this.currentDevice, VertexPositionTexture.VertexDeclaration, 4, BufferUsage.WriteOnly));
			this.vertBuffer.VertexBuffer.SetData<VertexPositionTexture>(BaseYUVPlayer.vertices);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0001FE6C File Offset: 0x0001E06C
		protected void GL_dispose()
		{
			if (this.currentDevice == null)
			{
				return;
			}
			this.currentDevice = null;
			if (this.shaderProgram != null)
			{
				this.shaderProgram.Dispose();
			}
			if (this.stateChangesPtr != IntPtr.Zero)
			{
				FNAPlatform.Free(this.stateChangesPtr);
			}
			if (this.vertBuffer.VertexBuffer != null)
			{
				this.vertBuffer.VertexBuffer.Dispose();
			}
			for (int i = 0; i < 3; i++)
			{
				if (this.yuvTextures[i] != null)
				{
					this.yuvTextures[i].Dispose();
				}
			}
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0001FF00 File Offset: 0x0001E100
		protected void GL_setupTextures(int yWidth, int yHeight, int uvWidth, int uvHeight, SurfaceFormat surfaceFormat)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.yuvTextures[i] != null)
				{
					this.yuvTextures[i].Dispose();
				}
			}
			this.yuvTextures[0] = new Texture2D(this.currentDevice, yWidth, yHeight, false, surfaceFormat);
			this.yuvTextures[1] = new Texture2D(this.currentDevice, uvWidth, uvHeight, false, surfaceFormat);
			this.yuvTextures[2] = new Texture2D(this.currentDevice, uvWidth, uvHeight, false, surfaceFormat);
			this.viewport = new Viewport(0, 0, yWidth, yHeight);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0001FF8C File Offset: 0x0001E18C
		protected unsafe void GL_pushState()
		{
			FNA3D.FNA3D_BeginPassRestore(this.currentDevice.GLDevice, this.shaderProgram.glEffect, this.stateChangesPtr);
			for (int i = 0; i < 3; i++)
			{
				this.oldTextures[i] = this.currentDevice.Textures[i];
				this.oldSamplers[i] = this.currentDevice.SamplerStates[i];
				this.currentDevice.Textures[i] = this.yuvTextures[i];
				this.currentDevice.SamplerStates[i] = SamplerState.LinearClamp;
			}
			this.oldBuffers = this.currentDevice.GetVertexBuffers();
			this.currentDevice.SetVertexBuffers(new VertexBufferBinding[] { this.vertBuffer });
			int renderTargetsNoAllocEXT = this.currentDevice.GetRenderTargetsNoAllocEXT(null);
			Array.Resize<RenderTargetBinding>(ref this.oldTargets, renderTargetsNoAllocEXT);
			this.currentDevice.GetRenderTargetsNoAllocEXT(this.oldTargets);
			fixed (FNA3D.FNA3D_RenderTargetBinding* ptr = &this.nativeVideoTexture[0])
			{
				FNA3D.FNA3D_RenderTargetBinding* ptr2 = ptr;
				GraphicsDevice.PrepareRenderTargetBindings(ptr2, this.videoTexture);
				FNA3D.FNA3D_SetRenderTargets(this.currentDevice.GLDevice, ptr2, this.videoTexture.Length, IntPtr.Zero, DepthFormat.None, 0);
			}
			this.prevBlend = this.currentDevice.BlendState;
			this.prevDepthStencil = this.currentDevice.DepthStencilState;
			this.prevRasterizer = this.currentDevice.RasterizerState;
			this.currentDevice.BlendState = BlendState.Opaque;
			this.currentDevice.DepthStencilState = DepthStencilState.None;
			this.currentDevice.RasterizerState = RasterizerState.CullNone;
			this.prevViewport = this.currentDevice.Viewport;
			FNA3D.FNA3D_SetViewport(this.currentDevice.GLDevice, ref this.viewport.viewport);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00020150 File Offset: 0x0001E350
		protected unsafe void GL_popState()
		{
			FNA3D.FNA3D_EndPassRestore(this.currentDevice.GLDevice, this.shaderProgram.glEffect);
			this.currentDevice.BlendState = this.prevBlend;
			this.currentDevice.DepthStencilState = this.prevDepthStencil;
			this.currentDevice.RasterizerState = this.prevRasterizer;
			this.prevBlend = null;
			this.prevDepthStencil = null;
			this.prevRasterizer = null;
			if (this.oldTargets == null || this.oldTargets.Length == 0)
			{
				FNA3D.FNA3D_SetRenderTargets(this.currentDevice.GLDevice, IntPtr.Zero, 0, IntPtr.Zero, DepthFormat.None, 0);
			}
			else
			{
				IRenderTarget renderTarget = this.oldTargets[0].RenderTarget as IRenderTarget;
				fixed (FNA3D.FNA3D_RenderTargetBinding* ptr = &this.nativeOldTargets[0])
				{
					FNA3D.FNA3D_RenderTargetBinding* ptr2 = ptr;
					GraphicsDevice.PrepareRenderTargetBindings(ptr2, this.oldTargets);
					FNA3D.FNA3D_SetRenderTargets(this.currentDevice.GLDevice, ptr2, this.oldTargets.Length, renderTarget.DepthStencilBuffer, renderTarget.DepthStencilFormat, (renderTarget.RenderTargetUsage > RenderTargetUsage.DiscardContents) ? 1 : 0);
				}
			}
			this.oldTargets = null;
			FNA3D.FNA3D_SetViewport(this.currentDevice.GLDevice, ref this.prevViewport.viewport);
			this.currentDevice.SetVertexBuffers(this.oldBuffers);
			this.oldBuffers = null;
			this.currentDevice.Textures.ignoreTargets = true;
			for (int i = 0; i < 3; i++)
			{
				if (this.oldTextures[i] == null || !this.oldTextures[i].IsDisposed)
				{
					this.currentDevice.Textures[i] = this.oldTextures[i];
				}
				this.currentDevice.SamplerStates[i] = this.oldSamplers[i];
				this.oldTextures[i] = null;
				this.oldSamplers[i] = null;
			}
			this.currentDevice.Textures.ignoreTargets = false;
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0002031A File Offset: 0x0001E51A
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x00020322 File Offset: 0x0001E522
		public bool IsDisposed
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDisposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsDisposed>k__BackingField = value;
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002032B File Offset: 0x0001E52B
		protected void checkDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException("VideoPlayer");
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00020340 File Offset: 0x0001E540
		internal BaseYUVPlayer()
		{
			this.IsDisposed = false;
			this.videoTexture = new RenderTargetBinding[1];
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x000203A4 File Offset: 0x0001E5A4
		public virtual void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			this.GL_dispose();
			if (this.videoTexture[0].RenderTarget != null)
			{
				this.videoTexture[0].RenderTarget.Dispose();
			}
			this.IsDisposed = true;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x000203F0 File Offset: 0x0001E5F0
		// Note: this type is marked as 'beforefieldinit'.
		static BaseYUVPlayer()
		{
		}

		// Token: 0x04000607 RID: 1543
		protected Effect shaderProgram;

		// Token: 0x04000608 RID: 1544
		private IntPtr stateChangesPtr;

		// Token: 0x04000609 RID: 1545
		protected Texture2D[] yuvTextures = new Texture2D[3];

		// Token: 0x0400060A RID: 1546
		private Viewport viewport;

		// Token: 0x0400060B RID: 1547
		private static VertexPositionTexture[] vertices = new VertexPositionTexture[]
		{
			new VertexPositionTexture(new Vector3(-1f, -1f, 0f), new Vector2(0f, 1f)),
			new VertexPositionTexture(new Vector3(1f, -1f, 0f), new Vector2(1f, 1f)),
			new VertexPositionTexture(new Vector3(-1f, 1f, 0f), new Vector2(0f, 0f)),
			new VertexPositionTexture(new Vector3(1f, 1f, 0f), new Vector2(1f, 0f))
		};

		// Token: 0x0400060C RID: 1548
		private VertexBufferBinding vertBuffer;

		// Token: 0x0400060D RID: 1549
		private Texture[] oldTextures = new Texture[3];

		// Token: 0x0400060E RID: 1550
		private SamplerState[] oldSamplers = new SamplerState[3];

		// Token: 0x0400060F RID: 1551
		private RenderTargetBinding[] oldTargets;

		// Token: 0x04000610 RID: 1552
		private VertexBufferBinding[] oldBuffers;

		// Token: 0x04000611 RID: 1553
		private BlendState prevBlend;

		// Token: 0x04000612 RID: 1554
		private DepthStencilState prevDepthStencil;

		// Token: 0x04000613 RID: 1555
		private RasterizerState prevRasterizer;

		// Token: 0x04000614 RID: 1556
		private Viewport prevViewport;

		// Token: 0x04000615 RID: 1557
		private FNA3D.FNA3D_RenderTargetBinding[] nativeVideoTexture = new FNA3D.FNA3D_RenderTargetBinding[3];

		// Token: 0x04000616 RID: 1558
		private FNA3D.FNA3D_RenderTargetBinding[] nativeOldTargets = new FNA3D.FNA3D_RenderTargetBinding[4];

		// Token: 0x04000617 RID: 1559
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x04000618 RID: 1560
		protected RenderTargetBinding[] videoTexture;

		// Token: 0x04000619 RID: 1561
		protected GraphicsDevice currentDevice;
	}
}
