using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020001CA RID: 458
	public class WorldSceneLayerTarget
	{
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0051B852 File Offset: 0x00519A52
		public Texture2D Texture
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x0051B85A File Offset: 0x00519A5A
		public Vector2 Position
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06001F6B RID: 8043 RVA: 0x0051B864 File Offset: 0x00519A64
		public bool IsPartiallyOffscreen
		{
			get
			{
				if (this._position == Vector2.Zero)
				{
					return true;
				}
				Vector2 vector = new Vector2((float)this._target.Width, (float)this._target.Height);
				Vector2 vector2 = this.Position + vector / 2f - Main.Camera.Center;
				Vector2 vector3 = (vector - Main.Camera.ScaledSize) / 2f;
				return Math.Abs(vector2.X) > vector3.X || Math.Abs(vector2.Y) > vector3.Y;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x0051B90D File Offset: 0x00519B0D
		public bool IsContentLost
		{
			get
			{
				return this._target.IsContentLost;
			}
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0051B91A File Offset: 0x00519B1A
		public WorldSceneLayerTarget(GraphicsDevice graphicsDevice, int width, int height)
		{
			this._target = new RenderTarget2D(graphicsDevice, width, height, false, graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.None);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0051B940 File Offset: 0x00519B40
		public void UpdateContent(Action render)
		{
			Vector2 screenPosition = Main.screenPosition;
			Point screenSize = Main.ScreenSize;
			Vector2 zoom = Main.GameViewMatrix.Zoom;
			Vector2 center = Main.Camera.Center;
			Main.screenWidth = this._target.Width - Main.offScreenRange * 2;
			Main.screenHeight = this._target.Height - Main.offScreenRange * 2;
			Main.screenPosition = Utils.Round(center - Main.ScreenSize.ToVector2() / 2f);
			Main.GameViewMatrix.Zoom = Vector2.One;
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			RenderTargetBinding[] renderTargets = graphicsDevice.GetRenderTargets();
			graphicsDevice.SetRenderTarget(this._target);
			graphicsDevice.Clear(Color.Transparent);
			this._position = Main.screenPosition - new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			render();
			graphicsDevice.SetRenderTargets(renderTargets);
			Main.screenPosition = screenPosition;
			Main.screenWidth = screenSize.X;
			Main.screenHeight = screenSize.Y;
			Main.GameViewMatrix.Zoom = zoom;
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0051BA4A File Offset: 0x00519C4A
		public void Dispose()
		{
			this._target.Dispose();
		}

		// Token: 0x04004A0F RID: 18959
		private readonly RenderTarget2D _target;

		// Token: 0x04004A10 RID: 18960
		private Vector2 _position;
	}
}
