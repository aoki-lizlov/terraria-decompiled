using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x0200058C RID: 1420
	public class SpriteDrawBuffer
	{
		// Token: 0x06003823 RID: 14371 RVA: 0x00630BB8 File Offset: 0x0062EDB8
		public SpriteDrawBuffer(GraphicsDevice graphicsDevice, int bufferSize = 2048)
		{
			this.graphicsDevice = graphicsDevice;
			this.bufferSize = bufferSize;
			this.ResizeArrays(bufferSize);
			this.spriteBatch = new SpriteBatch(graphicsDevice);
		}

		// Token: 0x06003824 RID: 14372 RVA: 0x00630BE8 File Offset: 0x0062EDE8
		public void ResizeArrays(int count)
		{
			Array.Resize<VertexPositionColorTexture>(ref this.vertices, count * 4);
			Array.Resize<Texture>(ref this.textures, count);
		}

		// Token: 0x06003825 RID: 14373 RVA: 0x00630C04 File Offset: 0x0062EE04
		public void ApplyDefaultSpriteEffect(RasterizerState rasterizer, Matrix transformation)
		{
			this.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, rasterizer, null, transformation);
			this.spriteBatch.End();
		}

		// Token: 0x06003826 RID: 14374 RVA: 0x00630C23 File Offset: 0x0062EE23
		public void ApplyDefaultSpriteEffect()
		{
			this.spriteBatch.Begin();
			this.spriteBatch.End();
		}

		// Token: 0x06003827 RID: 14375 RVA: 0x00630C3C File Offset: 0x0062EE3C
		private void CheckBuffers()
		{
			if (this.vertexBuffer == null || this.vertexBuffer.IsDisposed)
			{
				if (this.vertexBuffer != null)
				{
					this.vertexBuffer.Dispose();
				}
				this.vertexBuffer = new DynamicVertexBuffer(this.graphicsDevice, typeof(VertexPositionColorTexture), this.bufferSize * 4, BufferUsage.WriteOnly);
			}
			if (this.indexBuffer == null || this.indexBuffer.IsDisposed)
			{
				if (this.indexBuffer != null)
				{
					this.indexBuffer.Dispose();
				}
				this.indexBuffer = new IndexBuffer(this.graphicsDevice, typeof(ushort), this.bufferSize * 6, BufferUsage.WriteOnly);
				this.indexBuffer.SetData<ushort>(SpriteDrawBuffer.GenIndexBuffer(this.bufferSize));
			}
		}

		// Token: 0x06003828 RID: 14376 RVA: 0x00630CF8 File Offset: 0x0062EEF8
		private static ushort[] GenIndexBuffer(int maxSprites)
		{
			ushort[] array = new ushort[maxSprites * 6];
			int i = 0;
			ushort num = 0;
			while (i < maxSprites)
			{
				array[i++] = num;
				array[i++] = num + 1;
				array[i++] = num + 2;
				array[i++] = num + 3;
				array[i++] = num + 2;
				array[i++] = num + 1;
				num += 4;
			}
			return array;
		}

		// Token: 0x06003829 RID: 14377 RVA: 0x00630D60 File Offset: 0x0062EF60
		private void Bind()
		{
			if (this.preBindVertexBuffers != null)
			{
				return;
			}
			this.preBindVertexBuffers = this.graphicsDevice.GetVertexBuffers();
			this.preBindIndexBuffer = this.graphicsDevice.Indices;
			this.graphicsDevice.SetVertexBuffer(this.vertexBuffer);
			this.graphicsDevice.Indices = this.indexBuffer;
		}

		// Token: 0x0600382A RID: 14378 RVA: 0x00630DBA File Offset: 0x0062EFBA
		public void Unbind()
		{
			if (this.preBindVertexBuffers == null)
			{
				return;
			}
			this.graphicsDevice.SetVertexBuffers(this.preBindVertexBuffers);
			this.graphicsDevice.Indices = this.preBindIndexBuffer;
			this.preBindVertexBuffers = null;
			this.preBindIndexBuffer = null;
		}

		// Token: 0x0600382B RID: 14379 RVA: 0x00630DF8 File Offset: 0x0062EFF8
		public int DrawRange(int index, int count)
		{
			this.vertexCount = 0;
			this.CheckBuffers();
			this.Bind();
			this.graphicsDevice.Textures[0] = this.textures[index];
			int num = 0;
			while (count > 0)
			{
				if (this.uploadedSpriteIndex < 0 || index < this.uploadedSpriteIndex || index + count > this.uploadedSpriteIndex + this.bufferSize)
				{
					this.vertexBuffer.SetData<VertexPositionColorTexture>(this.vertices, index * 4, Math.Min(this.vertices.Length - index * 4, this.bufferSize * 4), SetDataOptions.Discard);
					this.uploadedSpriteIndex = index;
				}
				int num2 = Math.Min(count, this.bufferSize);
				int num3 = index - this.uploadedSpriteIndex;
				this.graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, num3 * 4, 0, num2 * 4, 0, num2 * 2);
				count -= num2;
				index += num2;
				num++;
			}
			return num;
		}

		// Token: 0x0600382C RID: 14380 RVA: 0x00630ED4 File Offset: 0x0062F0D4
		public void DrawSingle(int index)
		{
			this.DrawRange(index, 1);
		}

		// Token: 0x0600382D RID: 14381 RVA: 0x00630EE0 File Offset: 0x0062F0E0
		public int DrawAll()
		{
			if (this.vertexCount == 0)
			{
				return 0;
			}
			int num = this.vertexCount / 4;
			Texture texture = this.textures[0];
			int num2 = 0;
			int num3 = 0;
			for (int i = 1; i < num; i++)
			{
				Texture texture2 = this.textures[i];
				if (texture2 != texture)
				{
					num3 += this.DrawRange(num2, i - num2);
					num2 = i;
					texture = texture2;
				}
			}
			num3 += this.DrawRange(num2, num - num2);
			this.Unbind();
			return num3;
		}

		// Token: 0x0600382E RID: 14382 RVA: 0x00630F58 File Offset: 0x0062F158
		public void Draw(Texture2D texture, Vector2 position, VertexColors colors)
		{
			this.Draw(texture, position, null, colors, 0f, Vector2.Zero, 1f, SpriteEffects.None);
		}

		// Token: 0x0600382F RID: 14383 RVA: 0x00630F88 File Offset: 0x0062F188
		public void Draw(Texture2D texture, Rectangle destination, VertexColors colors)
		{
			this.Draw(texture, destination, null, colors);
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x00630FA7 File Offset: 0x0062F1A7
		public void Draw(Texture2D texture, Rectangle destination, Rectangle? sourceRectangle, VertexColors colors)
		{
			this.Draw(texture, destination, sourceRectangle, colors, 0f, Vector2.Zero, SpriteEffects.None);
		}

		// Token: 0x06003831 RID: 14385 RVA: 0x00630FC0 File Offset: 0x0062F1C0
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, VertexColors color, float rotation, Vector2 origin, float scale, SpriteEffects effects)
		{
			this.Draw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale, scale), effects);
		}

		// Token: 0x06003832 RID: 14386 RVA: 0x00630FE8 File Offset: 0x0062F1E8
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, VertexColors colors, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects)
		{
			float num;
			float num2;
			if (sourceRectangle != null)
			{
				num = (float)sourceRectangle.Value.Width * scale.X;
				num2 = (float)sourceRectangle.Value.Height * scale.Y;
			}
			else
			{
				num = (float)texture.Width * scale.X;
				num2 = (float)texture.Height * scale.Y;
			}
			this.Draw(texture, new Vector4(position.X, position.Y, num, num2), sourceRectangle, colors, rotation, origin, effects, 0f);
		}

		// Token: 0x06003833 RID: 14387 RVA: 0x00631074 File Offset: 0x0062F274
		public void Draw(Texture2D texture, Rectangle destination, Rectangle? sourceRectangle, VertexColors colors, float rotation, Vector2 origin, SpriteEffects effects)
		{
			this.Draw(texture, new Vector4((float)destination.X, (float)destination.Y, (float)destination.Width, (float)destination.Height), sourceRectangle, colors, rotation, origin, effects, 0f);
		}

		// Token: 0x06003834 RID: 14388 RVA: 0x006310B8 File Offset: 0x0062F2B8
		public void Draw(Texture2D texture, Vector4 destination, VertexColors colors, float rotation = 0f, Vector2 origin = default(Vector2), SpriteEffects effects = SpriteEffects.None)
		{
			this.Draw(texture, destination, null, colors, rotation, origin, effects, 0f);
		}

		// Token: 0x06003835 RID: 14389 RVA: 0x006310E4 File Offset: 0x0062F2E4
		public void Draw(Texture2D texture, Vector4 destinationRectangle, Rectangle? sourceRectangle, VertexColors colors, float rotation = 0f, Vector2 origin = default(Vector2), SpriteEffects effect = SpriteEffects.None, float depth = 0f)
		{
			Vector4 vector;
			if (sourceRectangle != null)
			{
				vector.X = (float)sourceRectangle.Value.X;
				vector.Y = (float)sourceRectangle.Value.Y;
				vector.Z = (float)sourceRectangle.Value.Width;
				vector.W = (float)sourceRectangle.Value.Height;
			}
			else
			{
				vector.X = 0f;
				vector.Y = 0f;
				vector.Z = (float)texture.Width;
				vector.W = (float)texture.Height;
			}
			Vector2 vector2;
			vector2.X = vector.X / (float)texture.Width;
			vector2.Y = vector.Y / (float)texture.Height;
			Vector2 vector3;
			vector3.X = (vector.X + vector.Z) / (float)texture.Width;
			vector3.Y = (vector.Y + vector.W) / (float)texture.Height;
			if ((effect & SpriteEffects.FlipVertically) != SpriteEffects.None)
			{
				float y = vector3.Y;
				vector3.Y = vector2.Y;
				vector2.Y = y;
			}
			if ((effect & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
			{
				float x = vector3.X;
				vector3.X = vector2.X;
				vector2.X = x;
			}
			this.QueueSprite(destinationRectangle, -origin, colors, vector, vector2, vector3, texture, depth, rotation);
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x00631240 File Offset: 0x0062F440
		private void QueueSprite(Vector4 destinationRect, Vector2 origin, VertexColors colors, Vector4 sourceRectangle, Vector2 texCoordTL, Vector2 texCoordBR, Texture2D texture, float depth, float rotation)
		{
			this.uploadedSpriteIndex = -1;
			float num = origin.X / sourceRectangle.Z;
			float num2 = origin.Y / sourceRectangle.W;
			float x = destinationRect.X;
			float y = destinationRect.Y;
			float z = destinationRect.Z;
			float w = destinationRect.W;
			float num3 = num * z;
			float num4 = num2 * w;
			float num5;
			float num6;
			if (rotation != 0f)
			{
				num5 = (float)Math.Cos((double)rotation);
				num6 = (float)Math.Sin((double)rotation);
			}
			else
			{
				num5 = 1f;
				num6 = 0f;
			}
			int num7 = this.vertexCount / 4;
			if (num7 >= this.textures.Length)
			{
				this.ResizeArrays(this.textures.Length * 2);
			}
			this.textures[num7] = texture;
			this.PushVertex(new Vector3(x + num3 * num5 - num4 * num6, y + num3 * num6 + num4 * num5, depth), colors.TopLeftColor, texCoordTL);
			this.PushVertex(new Vector3(x + (num3 + z) * num5 - num4 * num6, y + (num3 + z) * num6 + num4 * num5, depth), colors.TopRightColor, new Vector2(texCoordBR.X, texCoordTL.Y));
			this.PushVertex(new Vector3(x + num3 * num5 - (num4 + w) * num6, y + num3 * num6 + (num4 + w) * num5, depth), colors.BottomLeftColor, new Vector2(texCoordTL.X, texCoordBR.Y));
			this.PushVertex(new Vector3(x + (num3 + z) * num5 - (num4 + w) * num6, y + (num3 + z) * num6 + (num4 + w) * num5, depth), colors.BottomRightColor, texCoordBR);
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x006313EC File Offset: 0x0062F5EC
		private void PushVertex(Vector3 pos, Color color, Vector2 texCoord)
		{
			VertexPositionColorTexture[] array = this.vertices;
			int num = this.vertexCount;
			this.vertexCount = num + 1;
			SpriteDrawBuffer.SetVertex(ref array[num], pos, color, texCoord);
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x0063141D File Offset: 0x0062F61D
		private static void SetVertex(ref VertexPositionColorTexture vertex, Vector3 pos, Color color, Vector2 texCoord)
		{
			vertex.Position = pos;
			vertex.Color = color;
			vertex.TextureCoordinate = texCoord;
		}

		// Token: 0x04005C52 RID: 23634
		private readonly GraphicsDevice graphicsDevice;

		// Token: 0x04005C53 RID: 23635
		private readonly SpriteBatch spriteBatch;

		// Token: 0x04005C54 RID: 23636
		private readonly int bufferSize;

		// Token: 0x04005C55 RID: 23637
		private DynamicVertexBuffer vertexBuffer;

		// Token: 0x04005C56 RID: 23638
		private IndexBuffer indexBuffer;

		// Token: 0x04005C57 RID: 23639
		private int vertexCount;

		// Token: 0x04005C58 RID: 23640
		private VertexPositionColorTexture[] vertices;

		// Token: 0x04005C59 RID: 23641
		private Texture[] textures;

		// Token: 0x04005C5A RID: 23642
		private int uploadedSpriteIndex = -1;

		// Token: 0x04005C5B RID: 23643
		private VertexBufferBinding[] preBindVertexBuffers;

		// Token: 0x04005C5C RID: 23644
		private IndexBuffer preBindIndexBuffer;
	}
}
