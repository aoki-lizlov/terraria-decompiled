using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020001D6 RID: 470
	public class SpriteViewMatrix
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x0051D408 File Offset: 0x0051B608
		// (set) Token: 0x06001FAE RID: 8110 RVA: 0x0051D410 File Offset: 0x0051B610
		public Vector2 Zoom
		{
			get
			{
				return this._zoom;
			}
			set
			{
				if (this._zoom != value)
				{
					this._zoom = value;
					this._needsRebuild = true;
				}
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x0051D42E File Offset: 0x0051B62E
		public Vector2 Translation
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._translation;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001FB0 RID: 8112 RVA: 0x0051D444 File Offset: 0x0051B644
		public Matrix ZoomMatrix
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._zoomMatrix;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x0051D45A File Offset: 0x0051B65A
		public Matrix TransformationMatrix
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._transformationMatrix;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06001FB2 RID: 8114 RVA: 0x0051D470 File Offset: 0x0051B670
		public Matrix NormalizedTransformationMatrix
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._normalizedTransformationMatrix;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x0051D486 File Offset: 0x0051B686
		public Vector2 RenderZoom
		{
			get
			{
				return new Vector2(this.ZoomMatrix.M11, this.ZoomMatrix.M22);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001FB4 RID: 8116 RVA: 0x0051D4A3 File Offset: 0x0051B6A3
		// (set) Token: 0x06001FB5 RID: 8117 RVA: 0x0051D4AB File Offset: 0x0051B6AB
		public SpriteEffects Effects
		{
			get
			{
				return this._effects;
			}
			set
			{
				if (this._effects != value)
				{
					this._effects = value;
					this._needsRebuild = true;
				}
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001FB6 RID: 8118 RVA: 0x0051D4C4 File Offset: 0x0051B6C4
		public Matrix EffectMatrix
		{
			get
			{
				if (this.ShouldRebuild())
				{
					this.Rebuild();
				}
				return this._effectMatrix;
			}
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0051D4DC File Offset: 0x0051B6DC
		public SpriteViewMatrix(GraphicsDevice graphicsDevice)
		{
			this._graphicsDevice = graphicsDevice;
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x0051D534 File Offset: 0x0051B734
		private void Rebuild()
		{
			if (!this._overrideSystemViewport)
			{
				this._viewport = this._graphicsDevice.Viewport;
			}
			Vector2 vector = new Vector2((float)this._viewport.Width, (float)this._viewport.Height);
			Matrix matrix = Matrix.Identity;
			if ((this._effects & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
			{
				matrix *= Matrix.CreateScale(-1f, 1f, 1f) * Matrix.CreateTranslation(vector.X, 0f, 0f);
			}
			if ((this._effects & SpriteEffects.FlipVertically) != SpriteEffects.None)
			{
				matrix *= Matrix.CreateScale(1f, -1f, 1f) * Matrix.CreateTranslation(0f, vector.Y, 0f);
			}
			Vector2 vector2 = Utils.Round(this._zoom / 0.0078125f) * 0.0078125f;
			Vector2 vector3 = vector * 0.5f;
			Vector2 vector4 = Utils.Round(vector3 - vector3 / vector2);
			Matrix matrix2 = Matrix.CreateOrthographicOffCenter(0f, vector.X, vector.Y, 0f, 0f, 1f);
			this._translation = vector4;
			this._zoomMatrix = Matrix.CreateTranslation(-vector4.X, -vector4.Y, 0f) * Matrix.CreateScale(vector2.X, vector2.Y, 1f);
			this._effectMatrix = matrix;
			this._transformationMatrix = matrix * this._zoomMatrix;
			Matrix matrix3 = Matrix.CreateTranslation(0.00390625f, 0.00390625f, 0f);
			this._transformationMatrix *= matrix3;
			this._normalizedTransformationMatrix = Matrix.Invert(matrix) * this._zoomMatrix * matrix2;
			this._needsRebuild = false;
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0051D708 File Offset: 0x0051B908
		public void SetViewportOverride(Viewport viewport)
		{
			this._viewport = viewport;
			this._overrideSystemViewport = true;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0051D718 File Offset: 0x0051B918
		public void ClearViewportOverride()
		{
			this._overrideSystemViewport = false;
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0051D724 File Offset: 0x0051B924
		private bool ShouldRebuild()
		{
			return this._needsRebuild || (!this._overrideSystemViewport && !this._graphicsDevice.IsDisposed && (this._graphicsDevice.Viewport.Width != this._viewport.Width || this._graphicsDevice.Viewport.Height != this._viewport.Height));
		}

		// Token: 0x04004A2E RID: 18990
		private Vector2 _zoom = Vector2.One;

		// Token: 0x04004A2F RID: 18991
		private Vector2 _translation = Vector2.Zero;

		// Token: 0x04004A30 RID: 18992
		private Matrix _zoomMatrix = Matrix.Identity;

		// Token: 0x04004A31 RID: 18993
		private Matrix _transformationMatrix = Matrix.Identity;

		// Token: 0x04004A32 RID: 18994
		private Matrix _normalizedTransformationMatrix = Matrix.Identity;

		// Token: 0x04004A33 RID: 18995
		private SpriteEffects _effects;

		// Token: 0x04004A34 RID: 18996
		private Matrix _effectMatrix;

		// Token: 0x04004A35 RID: 18997
		private GraphicsDevice _graphicsDevice;

		// Token: 0x04004A36 RID: 18998
		private Viewport _viewport;

		// Token: 0x04004A37 RID: 18999
		private bool _overrideSystemViewport;

		// Token: 0x04004A38 RID: 19000
		private bool _needsRebuild = true;

		// Token: 0x04004A39 RID: 19001
		private const float PixelPerfectOffset = 0.00390625f;

		// Token: 0x04004A3A RID: 19002
		private const float PixelPerfectSafeZoomLevelStep = 0.0078125f;
	}
}
