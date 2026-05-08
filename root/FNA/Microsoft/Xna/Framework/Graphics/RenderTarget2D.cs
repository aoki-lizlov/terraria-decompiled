using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000B1 RID: 177
	public class RenderTarget2D : Texture2D, IRenderTarget
	{
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x0002F6BC File Offset: 0x0002D8BC
		public DepthFormat DepthStencilFormat
		{
			[CompilerGenerated]
			get
			{
				return this.<DepthStencilFormat>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<DepthStencilFormat>k__BackingField = value;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0002F6C5 File Offset: 0x0002D8C5
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x0002F6CD File Offset: 0x0002D8CD
		public int MultiSampleCount
		{
			[CompilerGenerated]
			get
			{
				return this.<MultiSampleCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MultiSampleCount>k__BackingField = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0002F6D6 File Offset: 0x0002D8D6
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x0002F6DE File Offset: 0x0002D8DE
		public RenderTargetUsage RenderTargetUsage
		{
			[CompilerGenerated]
			get
			{
				return this.<RenderTargetUsage>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RenderTargetUsage>k__BackingField = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x000136EB File Offset: 0x000118EB
		public bool IsContentLost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0002F6E7 File Offset: 0x0002D8E7
		IntPtr IRenderTarget.DepthStencilBuffer
		{
			get
			{
				return this.glDepthStencilBuffer;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0002F6EF File Offset: 0x0002D8EF
		IntPtr IRenderTarget.ColorBuffer
		{
			get
			{
				return this.glColorBuffer;
			}
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001438 RID: 5176 RVA: 0x0002F6F8 File Offset: 0x0002D8F8
		// (remove) Token: 0x06001439 RID: 5177 RVA: 0x0002F730 File Offset: 0x0002D930
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

		// Token: 0x0600143A RID: 5178 RVA: 0x0002F768 File Offset: 0x0002D968
		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height)
			: this(graphicsDevice, width, height, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.DiscardContents)
		{
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0002F784 File Offset: 0x0002D984
		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat)
			: this(graphicsDevice, width, height, mipMap, preferredFormat, preferredDepthFormat, 0, RenderTargetUsage.DiscardContents)
		{
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
		public RenderTarget2D(GraphicsDevice graphicsDevice, int width, int height, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
			: base(graphicsDevice, width, height, mipMap, preferredFormat)
		{
			this.DepthStencilFormat = preferredDepthFormat;
			this.MultiSampleCount = FNA3D.FNA3D_GetMaxMultiSampleCount(graphicsDevice.GLDevice, base.Format, MathHelper.ClosestMSAAPower(preferredMultiSampleCount));
			this.RenderTargetUsage = usage;
			if (this.MultiSampleCount > 0)
			{
				this.glColorBuffer = FNA3D.FNA3D_GenColorRenderbuffer(graphicsDevice.GLDevice, base.Width, base.Height, base.Format, this.MultiSampleCount, this.texture);
			}
			if (this.DepthStencilFormat == DepthFormat.None)
			{
				return;
			}
			this.glDepthStencilBuffer = FNA3D.FNA3D_GenDepthStencilRenderbuffer(graphicsDevice.GLDevice, base.Width, base.Height, this.DepthStencilFormat, this.MultiSampleCount);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0002F858 File Offset: 0x0002DA58
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				for (int i = 0; i < base.GraphicsDevice.renderTargetCount; i++)
				{
					if (base.GraphicsDevice.renderTargetBindings[i].RenderTarget == this)
					{
						throw new InvalidOperationException("Disposing target that is still bound");
					}
				}
				IntPtr intPtr = Interlocked.Exchange(ref this.glColorBuffer, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeRenderbuffer(base.GraphicsDevice.GLDevice, intPtr);
				}
				intPtr = Interlocked.Exchange(ref this.glDepthStencilBuffer, IntPtr.Zero);
				if (intPtr != IntPtr.Zero)
				{
					FNA3D.FNA3D_AddDisposeRenderbuffer(base.GraphicsDevice.GLDevice, intPtr);
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0002F90F File Offset: 0x0002DB0F
		protected internal override void GraphicsDeviceResetting()
		{
			base.GraphicsDeviceResetting();
		}

		// Token: 0x04000978 RID: 2424
		[CompilerGenerated]
		private DepthFormat <DepthStencilFormat>k__BackingField;

		// Token: 0x04000979 RID: 2425
		[CompilerGenerated]
		private int <MultiSampleCount>k__BackingField;

		// Token: 0x0400097A RID: 2426
		[CompilerGenerated]
		private RenderTargetUsage <RenderTargetUsage>k__BackingField;

		// Token: 0x0400097B RID: 2427
		private IntPtr glDepthStencilBuffer;

		// Token: 0x0400097C RID: 2428
		private IntPtr glColorBuffer;

		// Token: 0x0400097D RID: 2429
		[CompilerGenerated]
		private EventHandler<EventArgs> ContentLost;
	}
}
