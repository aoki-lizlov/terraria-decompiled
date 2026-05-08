using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReLogic.Graphics
{
	// Token: 0x02000082 RID: 130
	public class BasicDebugDrawer : IDebugDrawer, IDisposable
	{
		// Token: 0x060002EF RID: 751 RVA: 0x0000B0D8 File Offset: 0x000092D8
		public BasicDebugDrawer(GraphicsDevice graphicsDevice)
		{
			this._spriteBatch = new SpriteBatch(graphicsDevice);
			this._texture = new Texture2D(graphicsDevice, 4, 4);
			Color[] array = new Color[16];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Color.White;
			}
			this._texture.SetData<Color>(array);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B133 File Offset: 0x00009333
		public void Begin(Matrix matrix)
		{
			this._spriteBatch.Begin(0, null, null, null, null, null, matrix);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B147 File Offset: 0x00009347
		public void Begin()
		{
			this._spriteBatch.Begin();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B154 File Offset: 0x00009354
		public void DrawSquare(Vector4 positionAndSize, Color color)
		{
			this._spriteBatch.Draw(this._texture, new Vector2(positionAndSize.X, positionAndSize.Y), default(Rectangle?), color, 0f, Vector2.Zero, new Vector2(positionAndSize.Z, positionAndSize.W) / 4f, 0, 1f);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000B1B8 File Offset: 0x000093B8
		public void DrawSquare(Vector2 position, Vector2 size, Color color)
		{
			this._spriteBatch.Draw(this._texture, position, default(Rectangle?), color, 0f, Vector2.Zero, size / 4f, 0, 1f);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000B1FC File Offset: 0x000093FC
		public void DrawSquareFromCenter(Vector2 center, Vector2 size, float rotation, Color color)
		{
			this._spriteBatch.Draw(this._texture, center, default(Rectangle?), color, rotation, new Vector2(2f, 2f), size / 4f, 0, 1f);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B248 File Offset: 0x00009448
		public void DrawLine(Vector2 start, Vector2 end, float width, Color color)
		{
			Vector2 vector = end - start;
			float num = (float)Math.Atan2((double)vector.Y, (double)vector.X);
			Vector2 vector2;
			vector2..ctor(vector.Length(), width);
			this._spriteBatch.Draw(this._texture, start, default(Rectangle?), color, num, new Vector2(0f, 2f), vector2 / 4f, 0, 1f);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000B2BF File Offset: 0x000094BF
		public void End()
		{
			this._spriteBatch.End();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B2CC File Offset: 0x000094CC
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing)
				{
					if (this._spriteBatch != null)
					{
						this._spriteBatch.Dispose();
						this._spriteBatch = null;
					}
					if (this._texture != null)
					{
						this._texture.Dispose();
						this._texture = null;
					}
				}
				this._disposedValue = true;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000B31F File Offset: 0x0000951F
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x040004E7 RID: 1255
		private SpriteBatch _spriteBatch;

		// Token: 0x040004E8 RID: 1256
		private Texture2D _texture;

		// Token: 0x040004E9 RID: 1257
		private bool _disposedValue;
	}
}
