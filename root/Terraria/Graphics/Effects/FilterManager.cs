using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.IO;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001EF RID: 495
	public class FilterManager : EffectManager<Filter>
	{
		// Token: 0x060020AD RID: 8365 RVA: 0x00523272 File Offset: 0x00521472
		public FilterManager()
		{
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0052328D File Offset: 0x0052148D
		public void BindTo(Preferences preferences)
		{
			preferences.OnSave += this.Configuration_OnSave;
			preferences.OnLoad += this.Configuration_OnLoad;
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x005232B3 File Offset: 0x005214B3
		private void Configuration_OnSave(Preferences preferences)
		{
			preferences.Put("FilterLimit", this._filterLimit);
			preferences.Put("FilterPriorityThreshold", Enum.GetName(typeof(EffectPriority), this._priorityThreshold));
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x005232F0 File Offset: 0x005214F0
		private void Configuration_OnLoad(Preferences preferences)
		{
			this._filterLimit = preferences.Get<int>("FilterLimit", 16);
			EffectPriority effectPriority;
			if (Enum.TryParse<EffectPriority>(preferences.Get<string>("FilterPriorityThreshold", "VeryLow"), out effectPriority))
			{
				this._priorityThreshold = effectPriority;
			}
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00523330 File Offset: 0x00521530
		public override void OnActivate(Filter effect, Vector2 position)
		{
			if (this._activeFilters.Contains(effect))
			{
				if (effect.Active)
				{
					return;
				}
				if (effect.Priority >= this._priorityThreshold)
				{
					this._activeFilterCount--;
				}
				this._activeFilters.Remove(effect);
			}
			else
			{
				effect.Opacity = 0f;
			}
			if (effect.Priority >= this._priorityThreshold)
			{
				this._activeFilterCount++;
			}
			if (this._activeFilters.Count == 0)
			{
				this._activeFilters.AddLast(effect);
				return;
			}
			for (LinkedListNode<Filter> linkedListNode = this._activeFilters.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				Filter value = linkedListNode.Value;
				if (effect.Priority <= value.Priority)
				{
					this._activeFilters.AddAfter(linkedListNode, effect);
					return;
				}
			}
			this._activeFilters.AddLast(effect);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0052340A File Offset: 0x0052160A
		public void BeginCapture(RenderTarget2D screenTarget1)
		{
			this._captureThisFrame = true;
			Main.instance.GraphicsDevice.SetRenderTarget(screenTarget1);
			Main.instance.GraphicsDevice.Clear(Color.Transparent);
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00523438 File Offset: 0x00521638
		public void Update(GameTime gameTime)
		{
			LinkedListNode<Filter> linkedListNode = this._activeFilters.First;
			int count = this._activeFilters.Count;
			int num = 0;
			while (linkedListNode != null)
			{
				Filter value = linkedListNode.Value;
				LinkedListNode<Filter> next = linkedListNode.Next;
				bool flag = false;
				if (value.Priority >= this._priorityThreshold)
				{
					num++;
					if (num > this._activeFilterCount - this._filterLimit)
					{
						value.Update(gameTime);
						flag = true;
					}
				}
				if (value.Active && flag)
				{
					value.Opacity = Math.Min(value.Opacity + (float)gameTime.ElapsedGameTime.TotalSeconds * 1f, 1f);
				}
				else
				{
					value.Opacity = Math.Max(value.Opacity - (float)gameTime.ElapsedGameTime.TotalSeconds * 1f, 0f);
				}
				if (!value.Active && value.Opacity == 0f)
				{
					if (value.Priority >= this._priorityThreshold)
					{
						this._activeFilterCount--;
					}
					this._activeFilters.Remove(linkedListNode);
				}
				linkedListNode = next;
			}
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x00523548 File Offset: 0x00521748
		public void EndCapture(RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2)
		{
			this.EndCapture(finalTexture, screenTarget1, screenTarget2, screenTarget1.Size(), screenTarget1.Size(), Vector2.Zero);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00523564 File Offset: 0x00521764
		public void EndCapture(RenderTarget2D finalTexture, RenderTarget2D screenTarget1, RenderTarget2D screenTarget2, Vector2 screenSize, Vector2 sceneSize, Vector2 sceneOffset)
		{
			if (!this._captureThisFrame)
			{
				return;
			}
			this._captureThisFrame = false;
			TimeLogger.StartTimestamp startTimestamp = TimeLogger.Start();
			Rectangle rectangle = new Rectangle(0, 0, (int)screenSize.X, (int)screenSize.Y);
			RenderTarget2D renderTarget2D = screenTarget1;
			RenderTarget2D renderTarget2D2 = screenTarget2;
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			graphicsDevice.SetRenderTarget(renderTarget2D2);
			graphicsDevice.Clear(Color.Transparent);
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			SpriteEffects effects = Main.GameViewMatrix.Effects;
			Main.spriteBatch.Draw(Main.skyTarget, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
			Main.spriteBatch.Draw(renderTarget2D, Vector2.Zero, new Rectangle?(rectangle), Color.White, 0f, Vector2.Zero, 1f, effects, 0f);
			Main.spriteBatch.End();
			Utils.Swap<RenderTarget2D>(ref renderTarget2D2, ref renderTarget2D);
			int num = 0;
			LinkedListNode<Filter> linkedListNode = this._activeFilters.First;
			Filter filter = null;
			while (linkedListNode != null)
			{
				Filter value = linkedListNode.Value;
				LinkedListNode<Filter> next = linkedListNode.Next;
				if (value.Priority >= this._priorityThreshold)
				{
					num++;
					if (num > this._activeFilterCount - this._filterLimit && value.IsVisible())
					{
						if (filter != null)
						{
							graphicsDevice.SetRenderTarget(renderTarget2D2);
							graphicsDevice.Clear(Color.Transparent);
							Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
							filter.Apply(renderTarget2D.Size(), sceneSize, sceneOffset);
							Main.spriteBatch.Draw(renderTarget2D, Vector2.Zero, new Rectangle?(rectangle), Main.ColorOfTheSkies);
							Main.spriteBatch.End();
							Utils.Swap<RenderTarget2D>(ref renderTarget2D2, ref renderTarget2D);
						}
						filter = value;
					}
				}
				linkedListNode = next;
			}
			graphicsDevice.SetRenderTarget(finalTexture);
			graphicsDevice.Clear(Color.Transparent);
			if (Main.player[Main.myPlayer].gravDir == -1f)
			{
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.EffectMatrix);
			}
			else
			{
				Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
			}
			if (filter != null)
			{
				filter.Apply(renderTarget2D.Size(), sceneSize, sceneOffset);
				Main.spriteBatch.Draw(renderTarget2D, Vector2.Zero, new Rectangle?(rectangle), Main.ColorOfTheSkies);
			}
			else
			{
				Main.spriteBatch.Draw(renderTarget2D, Vector2.Zero, new Rectangle?(rectangle), Color.White);
			}
			Main.spriteBatch.End();
			for (int i = 0; i < 8; i++)
			{
				graphicsDevice.Textures[i] = null;
			}
			TimeLogger.Filters.AddTime(startTimestamp);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x0052380F File Offset: 0x00521A0F
		public bool HasActiveFilter()
		{
			return this._activeFilters.Count != 0;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0052381F File Offset: 0x00521A1F
		public bool CanCapture()
		{
			return this.HasActiveFilter();
		}

		// Token: 0x04004B20 RID: 19232
		private const float OPACITY_RATE = 1f;

		// Token: 0x04004B21 RID: 19233
		private LinkedList<Filter> _activeFilters = new LinkedList<Filter>();

		// Token: 0x04004B22 RID: 19234
		private int _filterLimit = 16;

		// Token: 0x04004B23 RID: 19235
		private EffectPriority _priorityThreshold;

		// Token: 0x04004B24 RID: 19236
		private int _activeFilterCount;

		// Token: 0x04004B25 RID: 19237
		private bool _captureThisFrame;
	}
}
