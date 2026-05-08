using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020001DD RID: 477
	public class CaptureManager : IDisposable
	{
		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06002010 RID: 8208 RVA: 0x00521055 File Offset: 0x0051F255
		public bool IsCapturing
		{
			get
			{
				return !Main.dedServ && this._camera.IsCapturing;
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0052106B File Offset: 0x0051F26B
		public CaptureManager()
		{
			this._interface = new CaptureInterface();
			if (!Main.dedServ)
			{
				this._camera = new CaptureCamera(Main.instance.GraphicsDevice);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06002012 RID: 8210 RVA: 0x0052109A File Offset: 0x0051F29A
		// (set) Token: 0x06002013 RID: 8211 RVA: 0x005210A7 File Offset: 0x0051F2A7
		public bool Active
		{
			get
			{
				return this._interface.Active;
			}
			set
			{
				if (Main.CaptureModeDisabled)
				{
					return;
				}
				if (this._interface.Active != value)
				{
					this._interface.ToggleCamera(value);
				}
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06002014 RID: 8212 RVA: 0x005210CB File Offset: 0x0051F2CB
		public bool UsingMap
		{
			get
			{
				return this.Active && this._interface.UsingMap();
			}
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x005210E2 File Offset: 0x0051F2E2
		public void Scrolling()
		{
			this._interface.Scrolling();
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x005210EF File Offset: 0x0051F2EF
		public void Update(CaptureInterface.SelectionContext context)
		{
			this._interface.Update(context);
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x005210FD File Offset: 0x0051F2FD
		public void Draw(SpriteBatch sb)
		{
			this._interface.Draw(sb);
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0052110B File Offset: 0x0051F30B
		public float GetProgress()
		{
			return this._camera.GetProgress();
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00521118 File Offset: 0x0051F318
		public void Capture()
		{
			CaptureSettings captureSettings = new CaptureSettings
			{
				Area = new Rectangle(2660, 100, 1000, 1000),
				UseScaling = false
			};
			this.Capture(captureSettings);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00521155 File Offset: 0x0051F355
		public void Capture(CaptureSettings settings)
		{
			this._camera.Capture(settings);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00521163 File Offset: 0x0051F363
		public void DrawTick()
		{
			this._interface.UpdateCameraCountdown();
			this._camera.DrawTick();
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0052117B File Offset: 0x0051F37B
		public void Dispose()
		{
			this._camera.Dispose();
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00521188 File Offset: 0x0051F388
		// Note: this type is marked as 'beforefieldinit'.
		static CaptureManager()
		{
		}

		// Token: 0x04004A89 RID: 19081
		public static CaptureManager Instance = new CaptureManager();

		// Token: 0x04004A8A RID: 19082
		private CaptureInterface _interface;

		// Token: 0x04004A8B RID: 19083
		private CaptureCamera _camera;
	}
}
