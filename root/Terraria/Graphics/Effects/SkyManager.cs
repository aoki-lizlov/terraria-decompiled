using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics.Effects
{
	// Token: 0x020001EA RID: 490
	public class SkyManager : EffectManager<CustomSky>
	{
		// Token: 0x0600208C RID: 8332 RVA: 0x00522D54 File Offset: 0x00520F54
		public void Reset()
		{
			foreach (CustomSky customSky in this._effects.Values)
			{
				customSky.Reset();
			}
			this._activeSkies.Clear();
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00522DB4 File Offset: 0x00520FB4
		public void Update(GameTime gameTime)
		{
			int num = Main.dayRate;
			if (num < 1)
			{
				num = 1;
			}
			for (int i = 0; i < num; i++)
			{
				LinkedListNode<CustomSky> next;
				for (LinkedListNode<CustomSky> linkedListNode = this._activeSkies.First; linkedListNode != null; linkedListNode = next)
				{
					CustomSky value = linkedListNode.Value;
					next = linkedListNode.Next;
					value.Update(gameTime);
					if (!value.IsActive())
					{
						this._activeSkies.Remove(linkedListNode);
					}
				}
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00522E13 File Offset: 0x00521013
		public void Draw(SpriteBatch spriteBatch)
		{
			this.DrawDepthRange(spriteBatch, float.MinValue, float.MaxValue);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00522E26 File Offset: 0x00521026
		public void DrawToDepth(SpriteBatch spriteBatch, float minDepth)
		{
			if (this._lastDepth <= minDepth)
			{
				return;
			}
			this.DrawDepthRange(spriteBatch, minDepth, this._lastDepth);
			this._lastDepth = minDepth;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00522E48 File Offset: 0x00521048
		public void DrawDepthRange(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			foreach (CustomSky customSky in this._activeSkies)
			{
				customSky.Draw(spriteBatch, minDepth, maxDepth);
			}
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00522E9C File Offset: 0x0052109C
		public void DrawRemainingDepth(SpriteBatch spriteBatch)
		{
			this.DrawDepthRange(spriteBatch, float.MinValue, this._lastDepth);
			this._lastDepth = float.MinValue;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00522EBB File Offset: 0x005210BB
		public void ResetDepthTracker()
		{
			this._lastDepth = float.MaxValue;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x00522EC8 File Offset: 0x005210C8
		public void SetStartingDepth(float depth)
		{
			this._lastDepth = depth;
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00522ED1 File Offset: 0x005210D1
		public override void OnActivate(CustomSky effect, Vector2 position)
		{
			this._activeSkies.Remove(effect);
			this._activeSkies.AddLast(effect);
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00522EF0 File Offset: 0x005210F0
		public Color ProcessTileColor(Color color)
		{
			foreach (CustomSky customSky in this._activeSkies)
			{
				color = customSky.OnTileColor(color);
			}
			return color;
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x00522F44 File Offset: 0x00521144
		public float ProcessCloudAlpha()
		{
			float num = 1f;
			foreach (CustomSky customSky in this._activeSkies)
			{
				num *= customSky.GetCloudAlpha();
			}
			return MathHelper.Clamp(num, 0f, 1f);
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00522FB0 File Offset: 0x005211B0
		public SkyManager()
		{
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00522FC3 File Offset: 0x005211C3
		// Note: this type is marked as 'beforefieldinit'.
		static SkyManager()
		{
		}

		// Token: 0x04004B11 RID: 19217
		public static SkyManager Instance = new SkyManager();

		// Token: 0x04004B12 RID: 19218
		private float _lastDepth;

		// Token: 0x04004B13 RID: 19219
		private LinkedList<CustomSky> _activeSkies = new LinkedList<CustomSky>();
	}
}
