using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020001D8 RID: 472
	public class TileBatch
	{
		// Token: 0x06001FC3 RID: 8131 RVA: 0x0051D8F0 File Offset: 0x0051BAF0
		public TileBatch(GraphicsDevice graphicsDevice)
		{
			this._graphicsDevice = graphicsDevice;
			this._spriteBatch = new SpriteBatch(graphicsDevice);
			this.Allocate();
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0051D9A4 File Offset: 0x0051BBA4
		private void Allocate()
		{
			if (this._vertexBuffer == null || this._vertexBuffer.IsDisposed)
			{
				this._vertexBuffer = new DynamicVertexBuffer(this._graphicsDevice, typeof(VertexPositionColorTexture), 8192, BufferUsage.WriteOnly);
				this._vertexBufferPosition = 0;
				this._vertexBuffer.ContentLost += delegate
				{
					this._vertexBufferPosition = 0;
				};
			}
			if (this._indexBuffer == null || this._indexBuffer.IsDisposed)
			{
				if (this._fallbackIndexData == null)
				{
					this._fallbackIndexData = new short[12288];
					for (int i = 0; i < 2048; i++)
					{
						this._fallbackIndexData[i * 6] = (short)(i * 4);
						this._fallbackIndexData[i * 6 + 1] = (short)(i * 4 + 1);
						this._fallbackIndexData[i * 6 + 2] = (short)(i * 4 + 2);
						this._fallbackIndexData[i * 6 + 3] = (short)(i * 4);
						this._fallbackIndexData[i * 6 + 4] = (short)(i * 4 + 2);
						this._fallbackIndexData[i * 6 + 5] = (short)(i * 4 + 3);
					}
				}
				this._indexBuffer = new DynamicIndexBuffer(this._graphicsDevice, typeof(short), 12288, BufferUsage.WriteOnly);
				this._indexBuffer.SetData<short>(this._fallbackIndexData);
				this._indexBuffer.ContentLost += delegate
				{
					this._indexBuffer.SetData<short>(this._fallbackIndexData);
				};
			}
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x0051DAF8 File Offset: 0x0051BCF8
		private void FlushRenderState()
		{
			this.Allocate();
			this._graphicsDevice.SetVertexBuffer(this._vertexBuffer);
			this._graphicsDevice.Indices = this._indexBuffer;
			this._graphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
			this._drawCalls = 0;
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0051DB4A File Offset: 0x0051BD4A
		public void Dispose()
		{
			if (this._vertexBuffer != null)
			{
				this._vertexBuffer.Dispose();
			}
			if (this._indexBuffer != null)
			{
				this._indexBuffer.Dispose();
			}
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0051DB72 File Offset: 0x0051BD72
		public void Begin(RasterizerState rasterizer, Matrix transformation)
		{
			this._spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, rasterizer, null, transformation);
			this._spriteBatch.End();
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0051DB91 File Offset: 0x0051BD91
		public void Begin()
		{
			this.Begin(RasterizerState.CullCounterClockwise, Matrix.Identity);
			if (this._queuedSpriteCount > 0)
			{
				throw new InvalidOperationException("Sprites have already been added before calling Begin");
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0051DBB7 File Offset: 0x0051BDB7
		public int Restart()
		{
			return this.End();
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0051DBC0 File Offset: 0x0051BDC0
		public void SetLayer(uint layer, ushort stack = 0)
		{
			if (layer >= 16777216U)
			{
				throw new ArgumentOutOfRangeException("Max Layer Exceeded");
			}
			if (!this._layeredSortingEnabled)
			{
				if (this._queuedSpriteCount > 0)
				{
					throw new InvalidOperationException("Sprites have already been added before setting the first layer");
				}
				this._layeredSortingEnabled = true;
			}
			this._nextLayerStack = new uint?((layer << 16) | (uint)stack);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0051DC14 File Offset: 0x0051BE14
		public void Draw(Texture2D texture, Vector2 position, VertexColors colors)
		{
			Vector4 vector = default(Vector4);
			vector.X = position.X;
			vector.Y = position.Y;
			vector.Z = 1f;
			vector.W = 1f;
			this.InternalDraw(texture, ref vector, true, ref TileBatch._nullRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0051DC78 File Offset: 0x0051BE78
		public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, VertexColors colors, Vector2 origin, float scale, SpriteEffects effects)
		{
			Vector4 vector = default(Vector4);
			vector.X = position.X;
			vector.Y = position.Y;
			vector.Z = scale;
			vector.W = scale;
			this.InternalDraw(texture, ref vector, true, ref sourceRectangle, ref colors, ref origin, effects, 0f);
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x0051DCD0 File Offset: 0x0051BED0
		public void Draw(Texture2D texture, Vector4 destination, VertexColors colors)
		{
			this.InternalDraw(texture, ref destination, false, ref TileBatch._nullRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0051DCFC File Offset: 0x0051BEFC
		public void Draw(Texture2D texture, Vector2 position, VertexColors colors, Vector2 scale)
		{
			Vector4 vector = default(Vector4);
			vector.X = position.X;
			vector.Y = position.Y;
			vector.Z = scale.X;
			vector.W = scale.Y;
			this.InternalDraw(texture, ref vector, true, ref TileBatch._nullRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0051DD64 File Offset: 0x0051BF64
		public void Draw(Texture2D texture, Vector4 destination, Rectangle? sourceRectangle, VertexColors colors)
		{
			this.InternalDraw(texture, ref destination, false, ref sourceRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0051DD8C File Offset: 0x0051BF8C
		public void Draw(Texture2D texture, Vector4 destination, Rectangle? sourceRectangle, VertexColors colors, Vector2 origin, SpriteEffects effects, float rotation)
		{
			this.InternalDraw(texture, ref destination, false, ref sourceRectangle, ref colors, ref origin, effects, rotation);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0051DDB0 File Offset: 0x0051BFB0
		public void Draw(Texture2D texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, VertexColors colors)
		{
			Vector4 vector = default(Vector4);
			vector.X = (float)destinationRectangle.X;
			vector.Y = (float)destinationRectangle.Y;
			vector.Z = (float)destinationRectangle.Width;
			vector.W = (float)destinationRectangle.Height;
			this.InternalDraw(texture, ref vector, false, ref sourceRectangle, ref colors, ref TileBatch._vector2Zero, SpriteEffects.None, 0f);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0051DE18 File Offset: 0x0051C018
		private static short[] CreateIndexData()
		{
			short[] array = new short[12288];
			for (int i = 0; i < 2048; i++)
			{
				array[i * 6] = (short)(i * 4);
				array[i * 6 + 1] = (short)(i * 4 + 1);
				array[i * 6 + 2] = (short)(i * 4 + 2);
				array[i * 6 + 3] = (short)(i * 4);
				array[i * 6 + 4] = (short)(i * 4 + 2);
				array[i * 6 + 5] = (short)(i * 4 + 3);
			}
			return array;
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0051DE8C File Offset: 0x0051C08C
		private unsafe void InternalDraw(Texture2D texture, ref Vector4 destination, bool scaleDestination, ref Rectangle? sourceRectangle, ref VertexColors colors, ref Vector2 origin, SpriteEffects effects, float rotation)
		{
			int num;
			if (this._layeredSortingEnabled)
			{
				if (this._nextLayerStack != null)
				{
					uint value = this._nextLayerStack.Value;
					if (texture != this._currentBatchKey.Texture || value != this._currentBatchKey.LayerStack)
					{
						this.SwitchBatch(texture, value);
					}
				}
				else if (texture != this._currentBatchKey.Texture)
				{
					this.SwitchBatch(texture, this._currentBatchKey.LayerStack + 1U);
				}
				this._nextLayerStack = null;
				num = this.GetNextSpriteIndex(ref this._batches[this._currentBatchIndex]);
			}
			else
			{
				if (this._queuedSpriteCount >= this._spriteDataQueue.Length)
				{
					Array.Resize<TileBatch.SpriteData>(ref this._spriteDataQueue, this._spriteDataQueue.Length << 1);
				}
				if (this._queuedSpriteCount >= this._spriteTextures.Length)
				{
					Array.Resize<Texture2D>(ref this._spriteTextures, this._spriteTextures.Length << 1);
				}
				this._spriteTextures[this._queuedSpriteCount] = texture;
				int queuedSpriteCount = this._queuedSpriteCount;
				this._queuedSpriteCount = queuedSpriteCount + 1;
				num = queuedSpriteCount;
			}
			fixed (TileBatch.SpriteData* ptr = &this._spriteDataQueue[num])
			{
				TileBatch.SpriteData* ptr2 = ptr;
				float num2 = destination.Z;
				float num3 = destination.W;
				if (sourceRectangle != null)
				{
					Rectangle value2 = sourceRectangle.Value;
					ptr2->Source.X = (float)value2.X;
					ptr2->Source.Y = (float)value2.Y;
					ptr2->Source.Z = (float)value2.Width;
					ptr2->Source.W = (float)value2.Height;
					if (scaleDestination)
					{
						num2 *= (float)value2.Width;
						num3 *= (float)value2.Height;
					}
				}
				else
				{
					float num4 = (float)texture.Width;
					float num5 = (float)texture.Height;
					ptr2->Source.X = 0f;
					ptr2->Source.Y = 0f;
					ptr2->Source.Z = num4;
					ptr2->Source.W = num5;
					if (scaleDestination)
					{
						num2 *= num4;
						num3 *= num5;
					}
				}
				ptr2->Destination.X = destination.X;
				ptr2->Destination.Y = destination.Y;
				ptr2->Destination.Z = num2;
				ptr2->Destination.W = num3;
				ptr2->Origin.X = origin.X;
				ptr2->Origin.Y = origin.Y;
				ptr2->Effects = effects;
				ptr2->Colors = colors;
				ptr2->Rotation = rotation;
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0051E114 File Offset: 0x0051C314
		private int GetNextSpriteIndex(ref TileBatch.LayerBatch layerBatchState)
		{
			if (layerBatchState.CurrentSliceIsFull)
			{
				int newSpriteBufferSlice = this.GetNewSpriteBufferSlice(layerBatchState.Length);
				this._batchData[layerBatchState.Tail].Next = newSpriteBufferSlice;
				layerBatchState.Tail = newSpriteBufferSlice;
				layerBatchState.NextSprite = this._batchData[newSpriteBufferSlice].Start;
			}
			layerBatchState.Length++;
			int nextSprite = layerBatchState.NextSprite;
			layerBatchState.NextSprite = nextSprite + 1;
			return nextSprite;
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0051E184 File Offset: 0x0051C384
		private int GetNewSpriteBufferSlice(int length)
		{
			if (this._batchDataCount == this._batchData.Length)
			{
				Array.Resize<TileBatch.DataSlice>(ref this._batchData, this._batchData.Length * 2);
			}
			int batchDataCount = this._batchDataCount;
			this._batchDataCount = batchDataCount + 1;
			int num = batchDataCount;
			this._batchData[num] = new TileBatch.DataSlice
			{
				Start = this._queuedSpriteCount,
				Length = length
			};
			this._queuedSpriteCount += length;
			while (this._queuedSpriteCount > this._spriteDataQueue.Length)
			{
				Array.Resize<TileBatch.SpriteData>(ref this._spriteDataQueue, this._spriteDataQueue.Length * 2);
			}
			return num;
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0051E228 File Offset: 0x0051C428
		private void SwitchBatch(Texture2D texture, uint layerStack)
		{
			TileBatch.LayerBatchKey currentBatchKey = this._currentBatchKey;
			int currentBatchIndex = this._currentBatchIndex;
			this._currentBatchKey = new TileBatch.LayerBatchKey
			{
				LayerStack = layerStack,
				Texture = texture
			};
			uint num = (layerStack >> 14) | (layerStack & 65535U);
			if ((ulong)num < (ulong)((long)this._batchLookupCache.Length) && this._batchLookupCache[(int)num].Texture == texture)
			{
				this._currentBatchIndex = this._batchLookupCache[(int)num].BatchIndex;
			}
			else if (!this._batchLookup.TryGetValue(this._currentBatchKey, out this._currentBatchIndex))
			{
				this.CreateBatch();
			}
			uint num2 = (currentBatchKey.LayerStack >> 14) | (currentBatchKey.LayerStack & 65535U);
			if ((ulong)num2 < (ulong)((long)this._batchLookupCache.Length))
			{
				this._batchLookupCache[(int)num2] = new TileBatch.RecentLayerCacheEntry(currentBatchKey.Texture, currentBatchIndex);
			}
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0051E308 File Offset: 0x0051C508
		private void CreateBatch()
		{
			Texture2D texture = this._currentBatchKey.Texture;
			ushort num;
			int num2;
			if (!this._textureIdLookup.TryGetValue(texture, out num))
			{
				num = (this._textureIdLookup[texture] = (ushort)this._passTextureCount);
				if (this._passTextureCount == this._passTextures.Length)
				{
					Array.Resize<Texture2D>(ref this._passTextures, this._passTextures.Length * 2);
				}
				Texture2D[] passTextures = this._passTextures;
				num2 = this._passTextureCount;
				this._passTextureCount = num2 + 1;
				passTextures[num2] = texture;
			}
			if (this._batchCount == this._batches.Length)
			{
				Array.Resize<TileBatch.LayerBatch>(ref this._batches, this._batches.Length * 2);
			}
			int newSpriteBufferSlice = this.GetNewSpriteBufferSlice(2);
			TileBatch.LayerBatch[] batches = this._batches;
			num2 = this._batchCount;
			this._batchCount = num2 + 1;
			batches[this._currentBatchIndex = num2] = new TileBatch.LayerBatch
			{
				LayerStack = this._currentBatchKey.LayerStack,
				Texture = num,
				Head = newSpriteBufferSlice,
				Tail = newSpriteBufferSlice,
				NextSprite = this._batchData[newSpriteBufferSlice].Start
			};
			this._batchLookup[this._currentBatchKey] = this._currentBatchIndex;
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0051E439 File Offset: 0x0051C639
		public int End()
		{
			this._layeredSortingEnabled = false;
			if (this._queuedSpriteCount == 0)
			{
				return 0;
			}
			this.FlushRenderState();
			if (this._passTextureCount > 0)
			{
				this.FlushLayered();
			}
			else
			{
				this.Flush();
			}
			return this._drawCalls;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0051E470 File Offset: 0x0051C670
		private void Flush()
		{
			Texture2D texture2D = null;
			int num = 0;
			for (int i = 0; i < this._queuedSpriteCount; i++)
			{
				if (this._spriteTextures[i] != texture2D)
				{
					if (i > num)
					{
						this.RenderBatch(texture2D, this._spriteDataQueue, num, i - num);
					}
					num = i;
					texture2D = this._spriteTextures[i];
				}
			}
			this.RenderBatch(texture2D, this._spriteDataQueue, num, this._queuedSpriteCount - num);
			Array.Clear(this._spriteTextures, 0, this._queuedSpriteCount);
			this._queuedSpriteCount = 0;
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0051E4EC File Offset: 0x0051C6EC
		private unsafe void RenderBatch(Texture2D texture, TileBatch.SpriteData[] sprites, int offset, int count)
		{
			this._graphicsDevice.Textures[0] = texture;
			while (count > 0)
			{
				SetDataOptions setDataOptions = SetDataOptions.NoOverwrite;
				int num = count;
				if (num > 2048 - this._vertexBufferPosition)
				{
					num = 2048 - this._vertexBufferPosition;
					if (num < 256)
					{
						this._vertexBufferPosition = 0;
						setDataOptions = SetDataOptions.Discard;
						num = count;
						if (num > 2048)
						{
							num = 2048;
						}
					}
				}
				this.FillVertexBuffer(texture, sprites, offset, num, 0);
				int num2 = this._vertexBufferPosition * sizeof(VertexPositionColorTexture) * 4;
				this._vertexBuffer.SetData<VertexPositionColorTexture>(num2, this._vertices, 0, num * 4, sizeof(VertexPositionColorTexture), setDataOptions);
				int num3 = this._vertexBufferPosition * 4;
				int num4 = num * 4;
				int num5 = this._vertexBufferPosition * 6;
				int num6 = num * 2;
				this._graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, num3, num4, num5, num6);
				this._vertexBufferPosition += num;
				offset += num;
				count -= num;
				this._drawCalls++;
			}
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0051E5EC File Offset: 0x0051C7EC
		private unsafe void FillVertexBuffer(Texture2D texture, TileBatch.SpriteData[] sprites, int offset, int count, int vbSpriteOffset)
		{
			float num = 1f / (float)texture.Width;
			float num2 = 1f / (float)texture.Height;
			fixed (TileBatch.SpriteData* ptr = &sprites[offset])
			{
				TileBatch.SpriteData* ptr2 = ptr;
				fixed (VertexPositionColorTexture* ptr3 = &this._vertices[vbSpriteOffset * 4])
				{
					VertexPositionColorTexture* ptr4 = ptr3;
					TileBatch.SpriteData* ptr5 = ptr2;
					VertexPositionColorTexture* ptr6 = ptr4;
					for (int i = 0; i < count; i++)
					{
						float num3;
						float num4;
						if (ptr5->Rotation != 0f)
						{
							num3 = (float)Math.Cos((double)ptr5->Rotation);
							num4 = (float)Math.Sin((double)ptr5->Rotation);
						}
						else
						{
							num3 = 1f;
							num4 = 0f;
						}
						float num5 = ptr5->Origin.X / ptr5->Source.Z;
						float num6 = ptr5->Origin.Y / ptr5->Source.W;
						ptr6->Color = ptr5->Colors.TopLeftColor;
						ptr6[1].Color = ptr5->Colors.TopRightColor;
						ptr6[2].Color = ptr5->Colors.BottomRightColor;
						ptr6[3].Color = ptr5->Colors.BottomLeftColor;
						for (int j = 0; j < 4; j++)
						{
							float num7 = TileBatch.CORNER_OFFSET_X[j];
							float num8 = TileBatch.CORNER_OFFSET_Y[j];
							float num9 = (num7 - num5) * ptr5->Destination.Z;
							float num10 = (num8 - num6) * ptr5->Destination.W;
							float num11 = ptr5->Destination.X + num9 * num3 - num10 * num4;
							float num12 = ptr5->Destination.Y + num9 * num4 + num10 * num3;
							if ((ptr5->Effects & SpriteEffects.FlipVertically) != SpriteEffects.None)
							{
								num8 = 1f - num8;
							}
							if ((ptr5->Effects & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
							{
								num7 = 1f - num7;
							}
							ptr6->Position.X = num11;
							ptr6->Position.Y = num12;
							ptr6->Position.Z = 0f;
							ptr6->TextureCoordinate.X = (ptr5->Source.X + num7 * ptr5->Source.Z) * num;
							ptr6->TextureCoordinate.Y = (ptr5->Source.Y + num8 * ptr5->Source.W) * num2;
							ptr6++;
						}
						ptr5++;
					}
				}
			}
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0051E87C File Offset: 0x0051CA7C
		private void FlushLayered()
		{
			Array.Sort<TileBatch.LayerBatch>(this._batches, 0, this._batchCount);
			int i = 0;
			this._vertexBufferPosition = 0;
			for (int j = 0; j < this._batchCount; j++)
			{
				TileBatch.LayerBatch layerBatch = this._batches[j];
				Texture2D texture2D = this._passTextures[(int)layerBatch.Texture];
				this._graphicsDevice.Textures[0] = texture2D;
				int num = layerBatch.Length;
				int num2 = j;
				int num3 = 0;
				TileBatch.DataSlice dataSlice = default(TileBatch.DataSlice);
				do
				{
					if (this._vertexBufferPosition == i)
					{
						i = 0;
						this._vertexBufferPosition = 0;
						while (i < num)
						{
							if (!this.FillVertexBuffer(this._batches[num2], ref dataSlice, ref num3, ref i))
							{
								break;
							}
							num2++;
							num3 = 0;
						}
						while (i < 2048 && num2 < this._batchCount)
						{
							layerBatch = this._batches[num2];
							if (i + layerBatch.Length > 2048)
							{
								break;
							}
							this.FillVertexBuffer(layerBatch, ref i);
							num2++;
						}
						this._vertexBuffer.SetData<VertexPositionColorTexture>(this._vertices, 0, i * 4, SetDataOptions.Discard);
					}
					int num4 = Math.Min(num, i - this._vertexBufferPosition);
					this._graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, this._vertexBufferPosition * 4, 0, num4 * 4, 0, num4 * 2);
					this._vertexBufferPosition += num4;
					num -= num4;
					this._drawCalls++;
				}
				while (num > 0);
			}
			this._queuedSpriteCount = 0;
			this._batchDataCount = 0;
			this._batchCount = 0;
			this._batchLookup.Clear();
			Array.Clear(this._batchLookupCache, 0, this._batchLookupCache.Length);
			this._passTextureCount = 0;
			this._textureIdLookup.Clear();
			this._currentBatchKey = default(TileBatch.LayerBatchKey);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0051EA44 File Offset: 0x0051CC44
		private void FillVertexBuffer(TileBatch.LayerBatch batch, ref int vbCount)
		{
			TileBatch.DataSlice dataSlice = default(TileBatch.DataSlice);
			int num = 0;
			this.FillVertexBuffer(batch, ref dataSlice, ref num, ref vbCount);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0051EA68 File Offset: 0x0051CC68
		private bool FillVertexBuffer(TileBatch.LayerBatch batch, ref TileBatch.DataSlice currentSlice, ref int batchOffset, ref int vbCount)
		{
			if (batchOffset == 0)
			{
				currentSlice = this._batchData[batch.Head];
			}
			Texture2D texture2D = this._passTextures[(int)batch.Texture];
			while (batchOffset < batch.Length)
			{
				if (currentSlice.Length == 0)
				{
					currentSlice = this._batchData[currentSlice.Next];
				}
				int num = Math.Min(Math.Min(batch.Length - batchOffset, currentSlice.Length), 2048 - vbCount);
				if (num == 0)
				{
					return false;
				}
				this.FillVertexBuffer(texture2D, this._spriteDataQueue, currentSlice.Start, num, vbCount);
				vbCount += num;
				batchOffset += num;
				currentSlice.Start += num;
				currentSlice.Length -= num;
			}
			return true;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0051EB33 File Offset: 0x0051CD33
		// Note: this type is marked as 'beforefieldinit'.
		static TileBatch()
		{
			float[] array = new float[4];
			array[1] = 1f;
			array[2] = 1f;
			TileBatch.CORNER_OFFSET_X = array;
			TileBatch.CORNER_OFFSET_Y = new float[] { 0f, 0f, 1f, 1f };
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0051EB6B File Offset: 0x0051CD6B
		[CompilerGenerated]
		private void <Allocate>b__35_0(object <p0>, EventArgs <p1>)
		{
			this._vertexBufferPosition = 0;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x0051EB74 File Offset: 0x0051CD74
		[CompilerGenerated]
		private void <Allocate>b__35_1(object <p0>, EventArgs <p1>)
		{
			this._indexBuffer.SetData<short>(this._fallbackIndexData);
		}

		// Token: 0x04004A3B RID: 19003
		private const int MinSliceLength = 2;

		// Token: 0x04004A3C RID: 19004
		private static readonly float[] CORNER_OFFSET_X;

		// Token: 0x04004A3D RID: 19005
		private static readonly float[] CORNER_OFFSET_Y;

		// Token: 0x04004A3E RID: 19006
		private GraphicsDevice _graphicsDevice;

		// Token: 0x04004A3F RID: 19007
		private TileBatch.SpriteData[] _spriteDataQueue = new TileBatch.SpriteData[2048];

		// Token: 0x04004A40 RID: 19008
		private Texture2D[] _spriteTextures = new Texture2D[2048];

		// Token: 0x04004A41 RID: 19009
		private int _queuedSpriteCount;

		// Token: 0x04004A42 RID: 19010
		private bool _layeredSortingEnabled;

		// Token: 0x04004A43 RID: 19011
		private TileBatch.DataSlice[] _batchData = new TileBatch.DataSlice[2048];

		// Token: 0x04004A44 RID: 19012
		private int _batchDataCount;

		// Token: 0x04004A45 RID: 19013
		private TileBatch.LayerBatch[] _batches = new TileBatch.LayerBatch[2048];

		// Token: 0x04004A46 RID: 19014
		private int _batchCount;

		// Token: 0x04004A47 RID: 19015
		private uint? _nextLayerStack;

		// Token: 0x04004A48 RID: 19016
		private int _currentBatchIndex;

		// Token: 0x04004A49 RID: 19017
		private TileBatch.LayerBatchKey _currentBatchKey;

		// Token: 0x04004A4A RID: 19018
		private Dictionary<TileBatch.LayerBatchKey, int> _batchLookup = new Dictionary<TileBatch.LayerBatchKey, int>();

		// Token: 0x04004A4B RID: 19019
		private readonly TileBatch.RecentLayerCacheEntry[] _batchLookupCache = new TileBatch.RecentLayerCacheEntry[2048];

		// Token: 0x04004A4C RID: 19020
		private Texture2D[] _passTextures = new Texture2D[512];

		// Token: 0x04004A4D RID: 19021
		private int _passTextureCount;

		// Token: 0x04004A4E RID: 19022
		private Dictionary<Texture2D, ushort> _textureIdLookup = new Dictionary<Texture2D, ushort>();

		// Token: 0x04004A4F RID: 19023
		private SpriteBatch _spriteBatch;

		// Token: 0x04004A50 RID: 19024
		private static Vector2 _vector2Zero;

		// Token: 0x04004A51 RID: 19025
		private static Rectangle? _nullRectangle;

		// Token: 0x04004A52 RID: 19026
		private DynamicVertexBuffer _vertexBuffer;

		// Token: 0x04004A53 RID: 19027
		private DynamicIndexBuffer _indexBuffer;

		// Token: 0x04004A54 RID: 19028
		private short[] _fallbackIndexData;

		// Token: 0x04004A55 RID: 19029
		private VertexPositionColorTexture[] _vertices = new VertexPositionColorTexture[8192];

		// Token: 0x04004A56 RID: 19030
		private int _vertexBufferPosition;

		// Token: 0x04004A57 RID: 19031
		private int _drawCalls;

		// Token: 0x02000790 RID: 1936
		private struct SpriteData
		{
			// Token: 0x04007033 RID: 28723
			public Vector4 Source;

			// Token: 0x04007034 RID: 28724
			public Vector4 Destination;

			// Token: 0x04007035 RID: 28725
			public Vector2 Origin;

			// Token: 0x04007036 RID: 28726
			public SpriteEffects Effects;

			// Token: 0x04007037 RID: 28727
			public VertexColors Colors;

			// Token: 0x04007038 RID: 28728
			public float Rotation;
		}

		// Token: 0x02000791 RID: 1937
		private struct DataSlice
		{
			// Token: 0x04007039 RID: 28729
			public int Start;

			// Token: 0x0400703A RID: 28730
			public int Length;

			// Token: 0x0400703B RID: 28731
			public int Next;
		}

		// Token: 0x02000792 RID: 1938
		private struct LayerBatch : IComparable<TileBatch.LayerBatch>
		{
			// Token: 0x17000529 RID: 1321
			// (get) Token: 0x06004178 RID: 16760 RVA: 0x006BA6C6 File Offset: 0x006B88C6
			public ulong SortKey
			{
				get
				{
					return ((ulong)this.LayerStack << 16) | (ulong)this.Texture;
				}
			}

			// Token: 0x1700052A RID: 1322
			// (get) Token: 0x06004179 RID: 16761 RVA: 0x006BA6DA File Offset: 0x006B88DA
			public bool CurrentSliceIsFull
			{
				get
				{
					return this.Length >= 2 && (this.Length & (this.Length - 1)) == 0;
				}
			}

			// Token: 0x0600417A RID: 16762 RVA: 0x006BA6FC File Offset: 0x006B88FC
			public int CompareTo(TileBatch.LayerBatch other)
			{
				return this.SortKey.CompareTo(other.SortKey);
			}

			// Token: 0x0400703C RID: 28732
			public uint LayerStack;

			// Token: 0x0400703D RID: 28733
			public ushort Texture;

			// Token: 0x0400703E RID: 28734
			public int Head;

			// Token: 0x0400703F RID: 28735
			public int Tail;

			// Token: 0x04007040 RID: 28736
			public int Length;

			// Token: 0x04007041 RID: 28737
			public int NextSprite;
		}

		// Token: 0x02000793 RID: 1939
		private struct LayerBatchKey : IEquatable<TileBatch.LayerBatchKey>
		{
			// Token: 0x0600417B RID: 16763 RVA: 0x006BA71E File Offset: 0x006B891E
			public bool Equals(TileBatch.LayerBatchKey other)
			{
				return this.LayerStack == other.LayerStack;
			}

			// Token: 0x0600417C RID: 16764 RVA: 0x006BA72E File Offset: 0x006B892E
			public override int GetHashCode()
			{
				return (int)(this.LayerStack ^ (uint)this.Texture.GetHashCode());
			}

			// Token: 0x04007042 RID: 28738
			public uint LayerStack;

			// Token: 0x04007043 RID: 28739
			public Texture2D Texture;
		}

		// Token: 0x02000794 RID: 1940
		private struct RecentLayerCacheEntry
		{
			// Token: 0x0600417D RID: 16765 RVA: 0x006BA742 File Offset: 0x006B8942
			public RecentLayerCacheEntry(Texture texture, int batchIndex)
			{
				this.Texture = texture;
				this.BatchIndex = batchIndex;
			}

			// Token: 0x04007044 RID: 28740
			public readonly Texture Texture;

			// Token: 0x04007045 RID: 28741
			public readonly int BatchIndex;
		}
	}
}
