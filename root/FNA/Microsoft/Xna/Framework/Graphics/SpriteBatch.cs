using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000B9 RID: 185
	public class SpriteBatch : GraphicsResource
	{
		// Token: 0x0600145F RID: 5215 RVA: 0x0002FC74 File Offset: 0x0002DE74
		public SpriteBatch(GraphicsDevice graphicsDevice)
		{
			if (graphicsDevice == null)
			{
				throw new ArgumentNullException("graphicsDevice");
			}
			base.GraphicsDevice = graphicsDevice;
			this.vertexInfo = new SpriteBatch.VertexPositionColorTexture4[2048];
			this.textureInfo = new Texture2D[2048];
			this.spriteInfos = new SpriteBatch.SpriteInfo[2048];
			this.sortedSpriteInfos = new IntPtr[2048];
			this.vertexBuffer = new DynamicVertexBuffer(graphicsDevice, typeof(VertexPositionColorTexture), 8192, BufferUsage.WriteOnly);
			this.indexBuffer = new IndexBuffer(graphicsDevice, IndexElementSize.SixteenBits, 12288, BufferUsage.WriteOnly);
			this.indexBuffer.SetData<short>(SpriteBatch.indexData);
			this.spriteEffect = new Effect(graphicsDevice, SpriteBatch.spriteEffectCode);
			this.spriteMatrixTransform = this.spriteEffect.Parameters["MatrixTransform"].values;
			this.spriteEffectPass = this.spriteEffect.CurrentTechnique.Passes[0];
			this.beginCalled = false;
			this.numSprites = 0;
			this.supportsNoOverwrite = FNA3D.FNA3D_SupportsNoOverwrite(base.GraphicsDevice.GLDevice) == 1;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0002FD8F File Offset: 0x0002DF8F
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				this.spriteEffect.Dispose();
				this.indexBuffer.Dispose();
				this.vertexBuffer.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0002FDC1 File Offset: 0x0002DFC1
		public void Begin()
		{
			this.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0002FDE4 File Offset: 0x0002DFE4
		public void Begin(SpriteSortMode sortMode, BlendState blendState)
		{
			this.Begin(sortMode, blendState, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0002FE03 File Offset: 0x0002E003
		public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState)
		{
			this.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, null, Matrix.Identity);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0002FE18 File Offset: 0x0002E018
		public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect)
		{
			this.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, Matrix.Identity);
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0002FE30 File Offset: 0x0002E030
		public void Begin(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
		{
			if (this.beginCalled)
			{
				throw new InvalidOperationException("Begin has been called before calling End after the last call to Begin. Begin cannot be called again until End has been successfully called.");
			}
			this.beginCalled = true;
			this.sortMode = sortMode;
			this.blendState = blendState ?? BlendState.AlphaBlend;
			this.samplerState = samplerState ?? SamplerState.LinearClamp;
			this.depthStencilState = depthStencilState ?? DepthStencilState.None;
			this.rasterizerState = rasterizerState ?? RasterizerState.CullCounterClockwise;
			this.customEffect = effect;
			this.transformMatrix = transformMatrix;
			if (sortMode == SpriteSortMode.Immediate)
			{
				this.PrepRenderState();
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0002FEBA File Offset: 0x0002E0BA
		public void End()
		{
			if (!this.beginCalled)
			{
				throw new InvalidOperationException("End was called, but Begin has not yet been called. You must call Begin  successfully before you can call End.");
			}
			this.beginCalled = false;
			if (this.sortMode != SpriteSortMode.Immediate)
			{
				this.FlushBatch();
			}
			this.customEffect = null;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0002FEEC File Offset: 0x0002E0EC
		public void Draw(Texture2D texture, Vector2 position, Color color)
		{
			this.CheckBegin("Draw");
			this.PushSprite(texture, 0f, 0f, 1f, 1f, position.X, position.Y, (float)texture.Width, (float)texture.Height, color, 0f, 0f, 0f, 1f, 0f, 0);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0002FF54 File Offset: 0x0002E154
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
		{
			float num;
			float num2;
			float num3;
			float num4;
			float num5;
			float num6;
			if (sourceRectangle != null)
			{
				num = (float)sourceRectangle.Value.X / (float)texture.Width;
				num2 = (float)sourceRectangle.Value.Y / (float)texture.Height;
				num3 = (float)sourceRectangle.Value.Width / (float)texture.Width;
				num4 = (float)sourceRectangle.Value.Height / (float)texture.Height;
				num5 = (float)sourceRectangle.Value.Width;
				num6 = (float)sourceRectangle.Value.Height;
			}
			else
			{
				num = 0f;
				num2 = 0f;
				num3 = 1f;
				num4 = 1f;
				num5 = (float)texture.Width;
				num6 = (float)texture.Height;
			}
			this.CheckBegin("Draw");
			this.PushSprite(texture, num, num2, num3, num4, position.X, position.Y, num5, num6, color, 0f, 0f, 0f, 1f, 0f, 0);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00030050 File Offset: 0x0002E250
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			this.CheckBegin("Draw");
			float num;
			float num2;
			float num3;
			float num4;
			float num5;
			float num6;
			if (sourceRectangle != null)
			{
				num = (float)sourceRectangle.Value.X / (float)texture.Width;
				num2 = (float)sourceRectangle.Value.Y / (float)texture.Height;
				num3 = (float)Math.Sign(sourceRectangle.Value.Width) * Math.Max((float)Math.Abs(sourceRectangle.Value.Width), MathHelper.MachineEpsilonFloat) / (float)texture.Width;
				num4 = (float)Math.Sign(sourceRectangle.Value.Height) * Math.Max((float)Math.Abs(sourceRectangle.Value.Height), MathHelper.MachineEpsilonFloat) / (float)texture.Height;
				num5 = scale * (float)sourceRectangle.Value.Width;
				num6 = scale * (float)sourceRectangle.Value.Height;
			}
			else
			{
				num = 0f;
				num2 = 0f;
				num3 = 1f;
				num4 = 1f;
				num5 = scale * (float)texture.Width;
				num6 = scale * (float)texture.Height;
			}
			this.PushSprite(texture, num, num2, num3, num4, position.X, position.Y, num5, num6, color, origin.X / num3 / (float)texture.Width, origin.Y / num4 / (float)texture.Height, (float)Math.Sin((double)rotation), (float)Math.Cos((double)rotation), layerDepth, (byte)(effects & (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically)));
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x000301C8 File Offset: 0x0002E3C8
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			this.CheckBegin("Draw");
			float num;
			float num2;
			float num3;
			float num4;
			if (sourceRectangle != null)
			{
				num = (float)sourceRectangle.Value.X / (float)texture.Width;
				num2 = (float)sourceRectangle.Value.Y / (float)texture.Height;
				num3 = (float)Math.Sign(sourceRectangle.Value.Width) * Math.Max((float)Math.Abs(sourceRectangle.Value.Width), MathHelper.MachineEpsilonFloat) / (float)texture.Width;
				num4 = (float)Math.Sign(sourceRectangle.Value.Height) * Math.Max((float)Math.Abs(sourceRectangle.Value.Height), MathHelper.MachineEpsilonFloat) / (float)texture.Height;
				scale.X *= (float)sourceRectangle.Value.Width;
				scale.Y *= (float)sourceRectangle.Value.Height;
			}
			else
			{
				num = 0f;
				num2 = 0f;
				num3 = 1f;
				num4 = 1f;
				scale.X *= (float)texture.Width;
				scale.Y *= (float)texture.Height;
			}
			this.PushSprite(texture, num, num2, num3, num4, position.X, position.Y, scale.X, scale.Y, color, origin.X / num3 / (float)texture.Width, origin.Y / num4 / (float)texture.Height, (float)Math.Sin((double)rotation), (float)Math.Cos((double)rotation), layerDepth, (byte)(effects & (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically)));
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00030358 File Offset: 0x0002E558
		public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
		{
			this.CheckBegin("Draw");
			this.PushSprite(texture, 0f, 0f, 1f, 1f, (float)destinationRectangle.X, (float)destinationRectangle.Y, (float)destinationRectangle.Width, (float)destinationRectangle.Height, color, 0f, 0f, 0f, 1f, 0f, 0);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000303C4 File Offset: 0x0002E5C4
		public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color)
		{
			this.CheckBegin("Draw");
			float num;
			float num2;
			float num3;
			float num4;
			if (sourceRectangle != null)
			{
				num = (float)sourceRectangle.Value.X / (float)texture.Width;
				num2 = (float)sourceRectangle.Value.Y / (float)texture.Height;
				num3 = (float)sourceRectangle.Value.Width / (float)texture.Width;
				num4 = (float)sourceRectangle.Value.Height / (float)texture.Height;
			}
			else
			{
				num = 0f;
				num2 = 0f;
				num3 = 1f;
				num4 = 1f;
			}
			this.PushSprite(texture, num, num2, num3, num4, (float)destinationRectangle.X, (float)destinationRectangle.Y, (float)destinationRectangle.Width, (float)destinationRectangle.Height, color, 0f, 0f, 0f, 1f, 0f, 0);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0003049C File Offset: 0x0002E69C
		public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
		{
			this.CheckBegin("Draw");
			float num;
			float num2;
			float num3;
			float num4;
			if (sourceRectangle != null)
			{
				num = (float)sourceRectangle.Value.X / (float)texture.Width;
				num2 = (float)sourceRectangle.Value.Y / (float)texture.Height;
				num3 = (float)Math.Sign(sourceRectangle.Value.Width) * Math.Max((float)Math.Abs(sourceRectangle.Value.Width), MathHelper.MachineEpsilonFloat) / (float)texture.Width;
				num4 = (float)Math.Sign(sourceRectangle.Value.Height) * Math.Max((float)Math.Abs(sourceRectangle.Value.Height), MathHelper.MachineEpsilonFloat) / (float)texture.Height;
			}
			else
			{
				num = 0f;
				num2 = 0f;
				num3 = 1f;
				num4 = 1f;
			}
			this.PushSprite(texture, num, num2, num3, num4, (float)destinationRectangle.X, (float)destinationRectangle.Y, (float)destinationRectangle.Width, (float)destinationRectangle.Height, color, origin.X / num3 / (float)texture.Width, origin.Y / num4 / (float)texture.Height, (float)Math.Sin((double)rotation), (float)Math.Cos((double)rotation), layerDepth, (byte)(effects & (SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically)));
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x000305DC File Offset: 0x0002E7DC
		public void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			this.DrawString(spriteFont, text, position, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00030618 File Offset: 0x0002E818
		public void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			this.DrawString(spriteFont, text, position, color, rotation, origin, new Vector2(scale), effects, layerDepth);
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x00030650 File Offset: 0x0002E850
		public void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			this.CheckBegin("DrawString");
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 0)
			{
				return;
			}
			effects &= SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
			Texture2D textureValue = spriteFont.textureValue;
			List<Rectangle> glyphData = spriteFont.glyphData;
			List<Rectangle> croppingData = spriteFont.croppingData;
			List<Vector3> kerning = spriteFont.kerning;
			Dictionary<char, int> characterIndexMap = spriteFont.characterIndexMap;
			Vector2 vector = origin;
			float num = SpriteBatch.axisDirectionX[(int)effects];
			float num2 = SpriteBatch.axisDirectionY[(int)effects];
			float num3 = 0f;
			float num4 = 0f;
			if (effects != SpriteEffects.None)
			{
				Vector2 vector2 = spriteFont.MeasureString(text);
				vector.X -= vector2.X * SpriteBatch.axisIsMirroredX[(int)effects];
				vector.Y -= vector2.Y * SpriteBatch.axisIsMirroredY[(int)effects];
				num3 = SpriteBatch.axisIsMirroredX[(int)effects];
				num4 = SpriteBatch.axisIsMirroredY[(int)effects];
			}
			Vector2 zero = Vector2.Zero;
			bool flag = true;
			for (int i = 0; i < text.Length; i++)
			{
				char c = text[i];
				if (c != '\r')
				{
					if (c == '\n')
					{
						zero.X = 0f;
						zero.Y += (float)spriteFont.LineSpacing;
						flag = true;
					}
					else
					{
						int num5;
						if (!characterIndexMap.TryGetValue(c, out num5))
						{
							if (spriteFont.DefaultCharacter == null)
							{
								throw new ArgumentException("Text contains characters that cannot be resolved by this SpriteFont.", "text");
							}
							num5 = characterIndexMap[spriteFont.DefaultCharacter.Value];
						}
						Vector3 vector3 = kerning[num5];
						if (flag)
						{
							zero.X += Math.Abs(vector3.X);
							flag = false;
						}
						else
						{
							zero.X += spriteFont.Spacing + vector3.X;
						}
						Rectangle rectangle = croppingData[num5];
						Rectangle rectangle2 = glyphData[num5];
						float num6 = vector.X + (zero.X + (float)rectangle.X) * num;
						float num7 = vector.Y + (zero.Y + (float)rectangle.Y) * num2;
						if (effects != SpriteEffects.None)
						{
							num6 += (float)rectangle2.Width * num3;
							num7 += (float)rectangle2.Height * num4;
						}
						float num8 = (float)Math.Sign(rectangle2.Width) * Math.Max((float)Math.Abs(rectangle2.Width), MathHelper.MachineEpsilonFloat) / (float)textureValue.Width;
						float num9 = (float)Math.Sign(rectangle2.Height) * Math.Max((float)Math.Abs(rectangle2.Height), MathHelper.MachineEpsilonFloat) / (float)textureValue.Height;
						this.PushSprite(textureValue, (float)rectangle2.X / (float)textureValue.Width, (float)rectangle2.Y / (float)textureValue.Height, num8, num9, position.X, position.Y, (float)rectangle2.Width * scale.X, (float)rectangle2.Height * scale.Y, color, num6 / num8 / (float)textureValue.Width, num7 / num9 / (float)textureValue.Height, (float)Math.Sin((double)rotation), (float)Math.Cos((double)rotation), layerDepth, (byte)effects);
						zero.X += vector3.Y + vector3.Z;
					}
				}
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00030988 File Offset: 0x0002EB88
		public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color)
		{
			this.DrawString(spriteFont, text, position, color, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000309B8 File Offset: 0x0002EBB8
		public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
		{
			this.DrawString(spriteFont, text, position, color, rotation, origin, new Vector2(scale), effects, layerDepth);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x000309E0 File Offset: 0x0002EBE0
		public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
		{
			this.CheckBegin("DrawString");
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 0)
			{
				return;
			}
			effects &= SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;
			Texture2D textureValue = spriteFont.textureValue;
			List<Rectangle> glyphData = spriteFont.glyphData;
			List<Rectangle> croppingData = spriteFont.croppingData;
			List<Vector3> kerning = spriteFont.kerning;
			Dictionary<char, int> characterIndexMap = spriteFont.characterIndexMap;
			Vector2 vector = origin;
			float num = SpriteBatch.axisDirectionX[(int)effects];
			float num2 = SpriteBatch.axisDirectionY[(int)effects];
			float num3 = 0f;
			float num4 = 0f;
			if (effects != SpriteEffects.None)
			{
				Vector2 vector2 = spriteFont.MeasureString(text);
				vector.X -= vector2.X * SpriteBatch.axisIsMirroredX[(int)effects];
				vector.Y -= vector2.Y * SpriteBatch.axisIsMirroredY[(int)effects];
				num3 = SpriteBatch.axisIsMirroredX[(int)effects];
				num4 = SpriteBatch.axisIsMirroredY[(int)effects];
			}
			Vector2 zero = Vector2.Zero;
			bool flag = true;
			foreach (char c in text)
			{
				if (c != '\r')
				{
					if (c == '\n')
					{
						zero.X = 0f;
						zero.Y += (float)spriteFont.LineSpacing;
						flag = true;
					}
					else
					{
						int num5;
						if (!characterIndexMap.TryGetValue(c, out num5))
						{
							if (spriteFont.DefaultCharacter == null)
							{
								throw new ArgumentException("Text contains characters that cannot be resolved by this SpriteFont.", "text");
							}
							num5 = characterIndexMap[spriteFont.DefaultCharacter.Value];
						}
						Vector3 vector3 = kerning[num5];
						if (flag)
						{
							zero.X += Math.Abs(vector3.X);
							flag = false;
						}
						else
						{
							zero.X += spriteFont.Spacing + vector3.X;
						}
						Rectangle rectangle = croppingData[num5];
						Rectangle rectangle2 = glyphData[num5];
						float num6 = vector.X + (zero.X + (float)rectangle.X) * num;
						float num7 = vector.Y + (zero.Y + (float)rectangle.Y) * num2;
						if (effects != SpriteEffects.None)
						{
							num6 += (float)rectangle2.Width * num3;
							num7 += (float)rectangle2.Height * num4;
						}
						float num8 = (float)Math.Sign(rectangle2.Width) * Math.Max((float)Math.Abs(rectangle2.Width), MathHelper.MachineEpsilonFloat) / (float)textureValue.Width;
						float num9 = (float)Math.Sign(rectangle2.Height) * Math.Max((float)Math.Abs(rectangle2.Height), MathHelper.MachineEpsilonFloat) / (float)textureValue.Height;
						this.PushSprite(textureValue, (float)rectangle2.X / (float)textureValue.Width, (float)rectangle2.Y / (float)textureValue.Height, num8, num9, position.X, position.Y, (float)rectangle2.Width * scale.X, (float)rectangle2.Height * scale.Y, color, num6 / num8 / (float)textureValue.Width, num7 / num9 / (float)textureValue.Height, (float)Math.Sin((double)rotation), (float)Math.Cos((double)rotation), layerDepth, (byte)effects);
						zero.X += vector3.Y + vector3.Z;
					}
				}
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00030D1C File Offset: 0x0002EF1C
		private unsafe void PushSprite(Texture2D texture, float sourceX, float sourceY, float sourceW, float sourceH, float destinationX, float destinationY, float destinationW, float destinationH, Color color, float originX, float originY, float rotationSin, float rotationCos, float depth, byte effects)
		{
			if (this.numSprites >= this.vertexInfo.Length)
			{
				if (this.vertexInfo.Length >= 699050)
				{
					this.FlushBatch();
				}
				else
				{
					int num = Math.Min(this.vertexInfo.Length * 2, 699050);
					Array.Resize<SpriteBatch.VertexPositionColorTexture4>(ref this.vertexInfo, num);
					Array.Resize<Texture2D>(ref this.textureInfo, num);
					Array.Resize<SpriteBatch.SpriteInfo>(ref this.spriteInfos, num);
					Array.Resize<IntPtr>(ref this.sortedSpriteInfos, num);
				}
			}
			if (this.sortMode == SpriteSortMode.Immediate)
			{
				int num2;
				fixed (SpriteBatch.VertexPositionColorTexture4* ptr = &this.vertexInfo[0])
				{
					SpriteBatch.VertexPositionColorTexture4* ptr2 = ptr;
					SpriteBatch.GenerateVertexInfo(ptr2, sourceX, sourceY, sourceW, sourceH, destinationX, destinationY, destinationW, destinationH, color, originX, originY, rotationSin, rotationCos, depth, effects);
					if (this.supportsNoOverwrite)
					{
						num2 = this.UpdateVertexBuffer(0, 1);
					}
					else
					{
						num2 = 0;
						this.vertexBuffer.SetDataPointerEXT(0, (IntPtr)((void*)ptr2), 96, SetDataOptions.None);
					}
				}
				this.DrawPrimitives(texture, num2, 1);
				return;
			}
			if (this.sortMode == SpriteSortMode.Deferred)
			{
				fixed (SpriteBatch.VertexPositionColorTexture4* ptr = &this.vertexInfo[this.numSprites])
				{
					SpriteBatch.GenerateVertexInfo(ptr, sourceX, sourceY, sourceW, sourceH, destinationX, destinationY, destinationW, destinationH, color, originX, originY, rotationSin, rotationCos, depth, effects);
				}
				this.textureInfo[this.numSprites] = texture;
				this.numSprites++;
				return;
			}
			fixed (SpriteBatch.SpriteInfo* ptr3 = &this.spriteInfos[this.numSprites])
			{
				SpriteBatch.SpriteInfo* ptr4 = ptr3;
				ptr4->textureHash = texture.GetHashCode();
				ptr4->sourceX = sourceX;
				ptr4->sourceY = sourceY;
				ptr4->sourceW = sourceW;
				ptr4->sourceH = sourceH;
				ptr4->destinationX = destinationX;
				ptr4->destinationY = destinationY;
				ptr4->destinationW = destinationW;
				ptr4->destinationH = destinationH;
				ptr4->color = color;
				ptr4->originX = originX;
				ptr4->originY = originY;
				ptr4->rotationSin = rotationSin;
				ptr4->rotationCos = rotationCos;
				ptr4->depth = depth;
				ptr4->effects = effects;
			}
			this.textureInfo[this.numSprites] = texture;
			this.numSprites++;
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00030F1C File Offset: 0x0002F11C
		private unsafe void FlushBatch()
		{
			this.PrepRenderState();
			if (this.numSprites == 0)
			{
				return;
			}
			if (this.sortMode != SpriteSortMode.Deferred)
			{
				IComparer<IntPtr> comparer;
				if (this.sortMode == SpriteSortMode.Texture)
				{
					comparer = SpriteBatch.TextureCompare;
				}
				else if (this.sortMode == SpriteSortMode.BackToFront)
				{
					comparer = SpriteBatch.BackToFrontCompare;
				}
				else
				{
					comparer = SpriteBatch.FrontToBackCompare;
				}
				fixed (SpriteBatch.SpriteInfo* ptr = &this.spriteInfos[0])
				{
					SpriteBatch.SpriteInfo* ptr2 = ptr;
					fixed (IntPtr* ptr3 = &this.sortedSpriteInfos[0])
					{
						IntPtr* ptr4 = ptr3;
						fixed (SpriteBatch.VertexPositionColorTexture4* ptr5 = &this.vertexInfo[0])
						{
							SpriteBatch.VertexPositionColorTexture4* ptr6 = ptr5;
							for (int i = 0; i < this.numSprites; i++)
							{
								ptr4[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)] = (IntPtr)((void*)(ptr2 + i));
							}
							Array.Sort<IntPtr, Texture2D>(this.sortedSpriteInfos, this.textureInfo, 0, this.numSprites, comparer);
							for (int j = 0; j < this.numSprites; j++)
							{
								SpriteBatch.SpriteInfo* ptr7 = (SpriteBatch.SpriteInfo*)(void*)ptr4[(IntPtr)j * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
								SpriteBatch.GenerateVertexInfo(ptr6 + j, ptr7->sourceX, ptr7->sourceY, ptr7->sourceW, ptr7->sourceH, ptr7->destinationX, ptr7->destinationY, ptr7->destinationW, ptr7->destinationH, ptr7->color, ptr7->originX, ptr7->originY, ptr7->rotationSin, ptr7->rotationCos, ptr7->depth, ptr7->effects);
							}
						}
					}
				}
			}
			int num = 0;
			for (;;)
			{
				int num2 = Math.Min(this.numSprites, 2048);
				int num3 = this.UpdateVertexBuffer(num, num2);
				int num4 = 0;
				Texture2D texture2D = this.textureInfo[num];
				for (int k = 1; k < num2; k++)
				{
					Texture2D texture2D2 = this.textureInfo[num + k];
					if (texture2D2 != texture2D)
					{
						this.DrawPrimitives(texture2D, num3 + num4, k - num4);
						texture2D = texture2D2;
						num4 = k;
					}
				}
				this.DrawPrimitives(texture2D, num3 + num4, num2 - num4);
				if (this.numSprites <= 2048)
				{
					break;
				}
				this.numSprites -= 2048;
				num += 2048;
			}
			this.numSprites = 0;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00031154 File Offset: 0x0002F354
		private unsafe int UpdateVertexBuffer(int start, int count)
		{
			int num;
			SetDataOptions setDataOptions;
			if (this.bufferOffset + count > 2048 || !this.supportsNoOverwrite)
			{
				num = 0;
				setDataOptions = SetDataOptions.Discard;
			}
			else
			{
				num = this.bufferOffset;
				setDataOptions = SetDataOptions.NoOverwrite;
			}
			fixed (SpriteBatch.VertexPositionColorTexture4* ptr = &this.vertexInfo[start])
			{
				SpriteBatch.VertexPositionColorTexture4* ptr2 = ptr;
				this.vertexBuffer.SetDataPointerEXT(num * 96, (IntPtr)((void*)ptr2), count * 96, setDataOptions);
			}
			this.bufferOffset = num + count;
			return num;
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000311C0 File Offset: 0x0002F3C0
		private unsafe static void GenerateVertexInfo(SpriteBatch.VertexPositionColorTexture4* sprite, float sourceX, float sourceY, float sourceW, float sourceH, float destinationX, float destinationY, float destinationW, float destinationH, Color color, float originX, float originY, float rotationSin, float rotationCos, float depth, byte effects)
		{
			float num = -originX * destinationW;
			float num2 = -originY * destinationH;
			sprite->Position0.X = -rotationSin * num2 + rotationCos * num + destinationX;
			sprite->Position0.Y = rotationCos * num2 + rotationSin * num + destinationY;
			num = (1f - originX) * destinationW;
			num2 = -originY * destinationH;
			sprite->Position1.X = -rotationSin * num2 + rotationCos * num + destinationX;
			sprite->Position1.Y = rotationCos * num2 + rotationSin * num + destinationY;
			num = -originX * destinationW;
			num2 = (1f - originY) * destinationH;
			sprite->Position2.X = -rotationSin * num2 + rotationCos * num + destinationX;
			sprite->Position2.Y = rotationCos * num2 + rotationSin * num + destinationY;
			num = (1f - originX) * destinationW;
			num2 = (1f - originY) * destinationH;
			sprite->Position3.X = -rotationSin * num2 + rotationCos * num + destinationX;
			sprite->Position3.Y = rotationCos * num2 + rotationSin * num + destinationY;
			fixed (float* ptr = &SpriteBatch.CornerOffsetX[0])
			{
				float* ptr2 = ptr;
				fixed (float* ptr3 = &SpriteBatch.CornerOffsetY[0])
				{
					float* ptr4 = ptr3;
					sprite->TextureCoordinate0.X = ptr2[0 ^ effects] * sourceW + sourceX;
					sprite->TextureCoordinate0.Y = ptr4[0 ^ effects] * sourceH + sourceY;
					sprite->TextureCoordinate1.X = ptr2[1 ^ effects] * sourceW + sourceX;
					sprite->TextureCoordinate1.Y = ptr4[1 ^ effects] * sourceH + sourceY;
					sprite->TextureCoordinate2.X = ptr2[2 ^ effects] * sourceW + sourceX;
					sprite->TextureCoordinate2.Y = ptr4[2 ^ effects] * sourceH + sourceY;
					sprite->TextureCoordinate3.X = ptr2[3 ^ effects] * sourceW + sourceX;
					sprite->TextureCoordinate3.Y = ptr4[3 ^ effects] * sourceH + sourceY;
				}
			}
			sprite->Position0.Z = depth;
			sprite->Position1.Z = depth;
			sprite->Position2.Z = depth;
			sprite->Position3.Z = depth;
			sprite->Color0 = color;
			sprite->Color1 = color;
			sprite->Color2 = color;
			sprite->Color3 = color;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00031424 File Offset: 0x0002F624
		private unsafe void PrepRenderState()
		{
			base.GraphicsDevice.BlendState = this.blendState;
			base.GraphicsDevice.SamplerStates[0] = this.samplerState;
			base.GraphicsDevice.DepthStencilState = this.depthStencilState;
			base.GraphicsDevice.RasterizerState = this.rasterizerState;
			base.GraphicsDevice.SetVertexBuffer(this.vertexBuffer);
			base.GraphicsDevice.Indices = this.indexBuffer;
			Viewport viewport = base.GraphicsDevice.Viewport;
			float num = (float)(2.0 / (double)viewport.Width);
			float num2 = (float)(-2.0 / (double)viewport.Height);
			float* ptr = (float*)(void*)this.spriteMatrixTransform;
			*ptr = num * this.transformMatrix.M11 - this.transformMatrix.M14;
			ptr[1] = num * this.transformMatrix.M21 - this.transformMatrix.M24;
			ptr[2] = num * this.transformMatrix.M31 - this.transformMatrix.M34;
			ptr[3] = num * this.transformMatrix.M41 - this.transformMatrix.M44;
			ptr[4] = num2 * this.transformMatrix.M12 + this.transformMatrix.M14;
			ptr[5] = num2 * this.transformMatrix.M22 + this.transformMatrix.M24;
			ptr[6] = num2 * this.transformMatrix.M32 + this.transformMatrix.M34;
			ptr[7] = num2 * this.transformMatrix.M42 + this.transformMatrix.M44;
			ptr[8] = this.transformMatrix.M13;
			ptr[9] = this.transformMatrix.M23;
			ptr[10] = this.transformMatrix.M33;
			ptr[11] = this.transformMatrix.M43;
			ptr[12] = this.transformMatrix.M14;
			ptr[13] = this.transformMatrix.M24;
			ptr[14] = this.transformMatrix.M34;
			ptr[15] = this.transformMatrix.M44;
			this.spriteEffectPass.Apply();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00031678 File Offset: 0x0002F878
		private void DrawPrimitives(Texture texture, int baseSprite, int batchSize)
		{
			if (this.customEffect != null)
			{
				using (List<EffectPass>.Enumerator enumerator = this.customEffect.CurrentTechnique.Passes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EffectPass effectPass = enumerator.Current;
						effectPass.Apply();
						base.GraphicsDevice.Textures[0] = texture;
						base.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, baseSprite * 4, 0, batchSize * 4, 0, batchSize * 2);
					}
					return;
				}
			}
			base.GraphicsDevice.Textures[0] = texture;
			base.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, baseSprite * 4, 0, batchSize * 4, 0, batchSize * 2);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0003172C File Offset: 0x0002F92C
		private void CheckBegin(string method)
		{
			if (!this.beginCalled)
			{
				throw new InvalidOperationException(method + " was called, but Begin has not yet been called. Begin must be called successfully before you can call " + method + ".");
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00031750 File Offset: 0x0002F950
		private static short[] GenerateIndexArray()
		{
			short[] array = new short[12288];
			int i = 0;
			int num = 0;
			while (i < 12288)
			{
				array[i] = (short)num;
				array[i + 1] = (short)(num + 1);
				array[i + 2] = (short)(num + 2);
				array[i + 3] = (short)(num + 3);
				array[i + 4] = (short)(num + 2);
				array[i + 5] = (short)(num + 1);
				i += 6;
				num += 4;
			}
			return array;
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000317B4 File Offset: 0x0002F9B4
		// Note: this type is marked as 'beforefieldinit'.
		static SpriteBatch()
		{
		}

		// Token: 0x04000993 RID: 2451
		private const int MAX_SPRITES = 2048;

		// Token: 0x04000994 RID: 2452
		private const int MAX_VERTICES = 8192;

		// Token: 0x04000995 RID: 2453
		private const int MAX_INDICES = 12288;

		// Token: 0x04000996 RID: 2454
		private const int MAX_ARRAYSIZE = 699050;

		// Token: 0x04000997 RID: 2455
		private static readonly float[] axisDirectionX = new float[] { -1f, 1f, -1f, 1f };

		// Token: 0x04000998 RID: 2456
		private static readonly float[] axisDirectionY = new float[] { -1f, -1f, 1f, 1f };

		// Token: 0x04000999 RID: 2457
		private static readonly float[] axisIsMirroredX = new float[] { 0f, 1f, 0f, 1f };

		// Token: 0x0400099A RID: 2458
		private static readonly float[] axisIsMirroredY = new float[] { 0f, 0f, 1f, 1f };

		// Token: 0x0400099B RID: 2459
		private static readonly float[] CornerOffsetX = new float[] { 0f, 1f, 0f, 1f };

		// Token: 0x0400099C RID: 2460
		private static readonly float[] CornerOffsetY = new float[] { 0f, 0f, 1f, 1f };

		// Token: 0x0400099D RID: 2461
		private DynamicVertexBuffer vertexBuffer;

		// Token: 0x0400099E RID: 2462
		private IndexBuffer indexBuffer;

		// Token: 0x0400099F RID: 2463
		private SpriteBatch.SpriteInfo[] spriteInfos;

		// Token: 0x040009A0 RID: 2464
		private IntPtr[] sortedSpriteInfos;

		// Token: 0x040009A1 RID: 2465
		private SpriteBatch.VertexPositionColorTexture4[] vertexInfo;

		// Token: 0x040009A2 RID: 2466
		private Texture2D[] textureInfo;

		// Token: 0x040009A3 RID: 2467
		private Effect spriteEffect;

		// Token: 0x040009A4 RID: 2468
		private IntPtr spriteMatrixTransform;

		// Token: 0x040009A5 RID: 2469
		private EffectPass spriteEffectPass;

		// Token: 0x040009A6 RID: 2470
		private bool beginCalled;

		// Token: 0x040009A7 RID: 2471
		private SpriteSortMode sortMode;

		// Token: 0x040009A8 RID: 2472
		private BlendState blendState;

		// Token: 0x040009A9 RID: 2473
		private SamplerState samplerState;

		// Token: 0x040009AA RID: 2474
		private DepthStencilState depthStencilState;

		// Token: 0x040009AB RID: 2475
		private RasterizerState rasterizerState;

		// Token: 0x040009AC RID: 2476
		private int numSprites;

		// Token: 0x040009AD RID: 2477
		private int bufferOffset;

		// Token: 0x040009AE RID: 2478
		private bool supportsNoOverwrite;

		// Token: 0x040009AF RID: 2479
		private Matrix transformMatrix;

		// Token: 0x040009B0 RID: 2480
		private Effect customEffect;

		// Token: 0x040009B1 RID: 2481
		private static readonly byte[] spriteEffectCode = Resources.SpriteEffect;

		// Token: 0x040009B2 RID: 2482
		private static readonly short[] indexData = SpriteBatch.GenerateIndexArray();

		// Token: 0x040009B3 RID: 2483
		private static readonly SpriteBatch.TextureComparer TextureCompare = new SpriteBatch.TextureComparer();

		// Token: 0x040009B4 RID: 2484
		private static readonly SpriteBatch.BackToFrontComparer BackToFrontCompare = new SpriteBatch.BackToFrontComparer();

		// Token: 0x040009B5 RID: 2485
		private static readonly SpriteBatch.FrontToBackComparer FrontToBackCompare = new SpriteBatch.FrontToBackComparer();

		// Token: 0x020003D4 RID: 980
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct VertexPositionColorTexture4 : IVertexType
		{
			// Token: 0x170003B1 RID: 945
			// (get) Token: 0x06001AED RID: 6893 RVA: 0x000136EE File Offset: 0x000118EE
			VertexDeclaration IVertexType.VertexDeclaration
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x04001DC5 RID: 7621
			public const int RealStride = 96;

			// Token: 0x04001DC6 RID: 7622
			public Vector3 Position0;

			// Token: 0x04001DC7 RID: 7623
			public Color Color0;

			// Token: 0x04001DC8 RID: 7624
			public Vector2 TextureCoordinate0;

			// Token: 0x04001DC9 RID: 7625
			public Vector3 Position1;

			// Token: 0x04001DCA RID: 7626
			public Color Color1;

			// Token: 0x04001DCB RID: 7627
			public Vector2 TextureCoordinate1;

			// Token: 0x04001DCC RID: 7628
			public Vector3 Position2;

			// Token: 0x04001DCD RID: 7629
			public Color Color2;

			// Token: 0x04001DCE RID: 7630
			public Vector2 TextureCoordinate2;

			// Token: 0x04001DCF RID: 7631
			public Vector3 Position3;

			// Token: 0x04001DD0 RID: 7632
			public Color Color3;

			// Token: 0x04001DD1 RID: 7633
			public Vector2 TextureCoordinate3;
		}

		// Token: 0x020003D5 RID: 981
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct SpriteInfo
		{
			// Token: 0x04001DD2 RID: 7634
			public int textureHash;

			// Token: 0x04001DD3 RID: 7635
			public float sourceX;

			// Token: 0x04001DD4 RID: 7636
			public float sourceY;

			// Token: 0x04001DD5 RID: 7637
			public float sourceW;

			// Token: 0x04001DD6 RID: 7638
			public float sourceH;

			// Token: 0x04001DD7 RID: 7639
			public float destinationX;

			// Token: 0x04001DD8 RID: 7640
			public float destinationY;

			// Token: 0x04001DD9 RID: 7641
			public float destinationW;

			// Token: 0x04001DDA RID: 7642
			public float destinationH;

			// Token: 0x04001DDB RID: 7643
			public Color color;

			// Token: 0x04001DDC RID: 7644
			public float originX;

			// Token: 0x04001DDD RID: 7645
			public float originY;

			// Token: 0x04001DDE RID: 7646
			public float rotationSin;

			// Token: 0x04001DDF RID: 7647
			public float rotationCos;

			// Token: 0x04001DE0 RID: 7648
			public float depth;

			// Token: 0x04001DE1 RID: 7649
			public byte effects;
		}

		// Token: 0x020003D6 RID: 982
		private class TextureComparer : IComparer<IntPtr>
		{
			// Token: 0x06001AEE RID: 6894 RVA: 0x0003F914 File Offset: 0x0003DB14
			public unsafe int Compare(IntPtr i1, IntPtr i2)
			{
				SpriteBatch.SpriteInfo* ptr = (SpriteBatch.SpriteInfo*)(void*)i1;
				SpriteBatch.SpriteInfo* ptr2 = (SpriteBatch.SpriteInfo*)(void*)i2;
				return ptr->textureHash.CompareTo(ptr2->textureHash);
			}

			// Token: 0x06001AEF RID: 6895 RVA: 0x000136F5 File Offset: 0x000118F5
			public TextureComparer()
			{
			}
		}

		// Token: 0x020003D7 RID: 983
		private class BackToFrontComparer : IComparer<IntPtr>
		{
			// Token: 0x06001AF0 RID: 6896 RVA: 0x0003F940 File Offset: 0x0003DB40
			public unsafe int Compare(IntPtr i1, IntPtr i2)
			{
				SpriteBatch.SpriteInfo* ptr = (SpriteBatch.SpriteInfo*)(void*)i1;
				SpriteBatch.SpriteInfo* ptr2 = (SpriteBatch.SpriteInfo*)(void*)i2;
				return ptr2->depth.CompareTo(ptr->depth);
			}

			// Token: 0x06001AF1 RID: 6897 RVA: 0x000136F5 File Offset: 0x000118F5
			public BackToFrontComparer()
			{
			}
		}

		// Token: 0x020003D8 RID: 984
		private class FrontToBackComparer : IComparer<IntPtr>
		{
			// Token: 0x06001AF2 RID: 6898 RVA: 0x0003F96C File Offset: 0x0003DB6C
			public unsafe int Compare(IntPtr i1, IntPtr i2)
			{
				SpriteBatch.SpriteInfo* ptr = (SpriteBatch.SpriteInfo*)(void*)i1;
				SpriteBatch.SpriteInfo* ptr2 = (SpriteBatch.SpriteInfo*)(void*)i2;
				return ptr->depth.CompareTo(ptr2->depth);
			}

			// Token: 0x06001AF3 RID: 6899 RVA: 0x000136F5 File Offset: 0x000118F5
			public FrontToBackComparer()
			{
			}
		}
	}
}
