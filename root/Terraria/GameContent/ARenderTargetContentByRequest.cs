using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x02000251 RID: 593
	public abstract class ARenderTargetContentByRequest : INeedRenderTargetContent
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0053CF4B File Offset: 0x0053B14B
		public bool IsReady
		{
			get
			{
				return this._wasPrepared;
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x0053CF53 File Offset: 0x0053B153
		public void Request()
		{
			this._wasRequested = true;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0053CF5C File Offset: 0x0053B15C
		public RenderTarget2D GetTarget()
		{
			return this._target;
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x0053CF64 File Offset: 0x0053B164
		public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
		{
			this._wasPrepared = false;
			if (!this._wasRequested)
			{
				return;
			}
			this._wasRequested = false;
			this.HandleUseRequest(device, spriteBatch);
		}

		// Token: 0x06002333 RID: 9011
		protected abstract void HandleUseRequest(GraphicsDevice device, SpriteBatch spriteBatch);

		// Token: 0x06002334 RID: 9012 RVA: 0x0053CF88 File Offset: 0x0053B188
		protected void PrepareARenderTarget_AndListenToEvents(ref RenderTarget2D target, GraphicsDevice device, int neededWidth, int neededHeight, RenderTargetUsage usage)
		{
			if (target == null || target.IsDisposed || target.Width != neededWidth || target.Height != neededHeight)
			{
				if (target != null)
				{
					target.ContentLost -= this.target_ContentLost;
					target.Disposing -= this.target_Disposing;
				}
				target = new RenderTarget2D(device, neededWidth, neededHeight, false, device.PresentationParameters.BackBufferFormat, DepthFormat.None, 0, usage);
				target.ContentLost += this.target_ContentLost;
				target.Disposing += this.target_Disposing;
			}
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x0053D022 File Offset: 0x0053B222
		private void target_Disposing(object sender, EventArgs e)
		{
			this._wasPrepared = false;
			this._target = null;
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x0053D032 File Offset: 0x0053B232
		private void target_ContentLost(object sender, EventArgs e)
		{
			this._wasPrepared = false;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x0053D03B File Offset: 0x0053B23B
		public void Reset()
		{
			this._wasPrepared = false;
			this._wasRequested = false;
			this._target = null;
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0053D054 File Offset: 0x0053B254
		protected void PrepareARenderTarget_WithoutListeningToEvents(ref RenderTarget2D target, GraphicsDevice device, int neededWidth, int neededHeight, RenderTargetUsage usage)
		{
			if (target == null || target.IsDisposed || target.Width != neededWidth || target.Height != neededHeight)
			{
				target = new RenderTarget2D(device, neededWidth, neededHeight, false, device.PresentationParameters.BackBufferFormat, DepthFormat.None, 0, usage);
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0000357B File Offset: 0x0000177B
		protected ARenderTargetContentByRequest()
		{
		}

		// Token: 0x04004D55 RID: 19797
		protected RenderTarget2D _target;

		// Token: 0x04004D56 RID: 19798
		protected bool _wasPrepared;

		// Token: 0x04004D57 RID: 19799
		private bool _wasRequested;
	}
}
