using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000B3 RID: 179
	public class RenderTargetCube : TextureCube, IRenderTarget
	{
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0002F97E File Offset: 0x0002DB7E
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x0002F986 File Offset: 0x0002DB86
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

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0002F98F File Offset: 0x0002DB8F
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x0002F997 File Offset: 0x0002DB97
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

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0002F9A0 File Offset: 0x0002DBA0
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
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

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x000136EB File Offset: 0x000118EB
		public bool IsContentLost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0002F9B1 File Offset: 0x0002DBB1
		int IRenderTarget.Width
		{
			get
			{
				return base.Size;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0002F9B1 File Offset: 0x0002DBB1
		int IRenderTarget.Height
		{
			get
			{
				return base.Size;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0002F9B9 File Offset: 0x0002DBB9
		IntPtr IRenderTarget.DepthStencilBuffer
		{
			get
			{
				return this.glDepthStencilBuffer;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0002F9C1 File Offset: 0x0002DBC1
		IntPtr IRenderTarget.ColorBuffer
		{
			get
			{
				return this.glColorBuffer;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x0600144F RID: 5199 RVA: 0x0002F9CC File Offset: 0x0002DBCC
		// (remove) Token: 0x06001450 RID: 5200 RVA: 0x0002FA04 File Offset: 0x0002DC04
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

		// Token: 0x06001451 RID: 5201 RVA: 0x0002FA39 File Offset: 0x0002DC39
		public RenderTargetCube(GraphicsDevice graphicsDevice, int size, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat)
			: this(graphicsDevice, size, mipMap, preferredFormat, preferredDepthFormat, 0, RenderTargetUsage.DiscardContents)
		{
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0002FA4C File Offset: 0x0002DC4C
		public RenderTargetCube(GraphicsDevice graphicsDevice, int size, bool mipMap, SurfaceFormat preferredFormat, DepthFormat preferredDepthFormat, int preferredMultiSampleCount, RenderTargetUsage usage)
			: base(graphicsDevice, size, mipMap, preferredFormat)
		{
			this.DepthStencilFormat = preferredDepthFormat;
			this.MultiSampleCount = FNA3D.FNA3D_GetMaxMultiSampleCount(graphicsDevice.GLDevice, base.Format, MathHelper.ClosestMSAAPower(preferredMultiSampleCount));
			this.RenderTargetUsage = usage;
			if (this.MultiSampleCount > 0)
			{
				this.glColorBuffer = FNA3D.FNA3D_GenColorRenderbuffer(graphicsDevice.GLDevice, base.Size, base.Size, base.Format, this.MultiSampleCount, this.texture);
			}
			if (this.DepthStencilFormat == DepthFormat.None)
			{
				return;
			}
			this.glDepthStencilBuffer = FNA3D.FNA3D_GenDepthStencilRenderbuffer(graphicsDevice.GLDevice, base.Size, base.Size, this.DepthStencilFormat, this.MultiSampleCount);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0002FAFC File Offset: 0x0002DCFC
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

		// Token: 0x04000980 RID: 2432
		[CompilerGenerated]
		private DepthFormat <DepthStencilFormat>k__BackingField;

		// Token: 0x04000981 RID: 2433
		[CompilerGenerated]
		private int <MultiSampleCount>k__BackingField;

		// Token: 0x04000982 RID: 2434
		[CompilerGenerated]
		private RenderTargetUsage <RenderTargetUsage>k__BackingField;

		// Token: 0x04000983 RID: 2435
		private IntPtr glDepthStencilBuffer;

		// Token: 0x04000984 RID: 2436
		private IntPtr glColorBuffer;

		// Token: 0x04000985 RID: 2437
		[CompilerGenerated]
		private EventHandler<EventArgs> ContentLost;
	}
}
