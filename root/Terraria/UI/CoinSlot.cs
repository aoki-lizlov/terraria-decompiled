using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.UI
{
	// Token: 0x020000DE RID: 222
	public class CoinSlot
	{
		// Token: 0x06001894 RID: 6292 RVA: 0x004E2EDB File Offset: 0x004E10DB
		public static void UpdateSavings(int slot, int count, out CoinSlot.CoinDrawState drawState)
		{
			CoinSlot.Savings[slot].UpdateState(71 + slot, count, CoinSlot.SavingsCoinJumpScale, out drawState);
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x004E2EF8 File Offset: 0x004E10F8
		public static void UpdateCustom(int customCurrencyID, int count, out CoinSlot.CoinDrawState drawState)
		{
			CoinSlot.CoinEntryRef coinEntryRef;
			if (!CoinSlot.Custom.TryGetValue(customCurrencyID, out coinEntryRef))
			{
				coinEntryRef = new CoinSlot.CoinEntryRef();
				CoinSlot.Custom[customCurrencyID] = coinEntryRef;
			}
			coinEntryRef.val.UpdateState(customCurrencyID, count, CoinSlot.SavingsCoinJumpScale, out drawState);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x004E2F3C File Offset: 0x004E113C
		public static float DrawItemCoin(SpriteBatch spriteBatch, Vector2 screenPositionForItemCenter, int coinType, int coinFrame, float scale, float sizeLimit, Color itemColor, float itemFade = 1f)
		{
			int num = coinType - 71;
			Texture2D value = TextureAssets.Coin[num].Value;
			Rectangle rectangle = value.Frame(1, 8, 0, coinFrame, 0, 0);
			Color white = Color.White;
			Color white2 = Color.White;
			float num2 = 1f;
			if ((float)rectangle.Width > sizeLimit || (float)rectangle.Height > sizeLimit)
			{
				if (rectangle.Width > rectangle.Height)
				{
					num2 = sizeLimit / (float)rectangle.Width;
				}
				else
				{
					num2 = sizeLimit / (float)rectangle.Height;
				}
			}
			float num3 = scale * num2;
			SpriteEffects spriteEffects = SpriteEffects.None;
			Vector2 vector = rectangle.Size() / 2f;
			Color color = ContentSamples.ItemsByType[coinType].GetAlpha(itemColor).MultiplyRGBA(white);
			spriteBatch.Draw(value, screenPositionForItemCenter, new Rectangle?(rectangle), color * itemFade, 0f, vector, num3, spriteEffects, 0f);
			return num3;
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x004E3018 File Offset: 0x004E1218
		public static void UpdateSlotAnims()
		{
			for (int i = 0; i < CoinSlot.Savings.Length; i++)
			{
				CoinSlot.Savings[i].UpdateAnim();
			}
			for (int j = 0; j < CoinSlot.ChestEntries.Length; j++)
			{
				CoinSlot.ChestEntries[j].UpdateAnim();
			}
			for (int k = 0; k < CoinSlot.InventoryEntries.Length; k++)
			{
				CoinSlot.InventoryEntries[k].UpdateAnim();
			}
			foreach (KeyValuePair<int, CoinSlot.CoinEntryRef> keyValuePair in CoinSlot.Custom)
			{
				keyValuePair.Value.val.UpdateAnim();
			}
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x004E30DC File Offset: 0x004E12DC
		public static void ForceSlotState(int slot, int context, Item item)
		{
			if (context <= 2)
			{
				CoinSlot.InventoryEntries[slot].ForceState(item.type, item.stack);
				return;
			}
			if (context - 3 > 1)
			{
				return;
			}
			CoinSlot.ChestEntries[slot].ForceState(item.type, item.stack);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x004E3130 File Offset: 0x004E1330
		public static void UpdateDrawState(int slot, int context, Item item, out CoinSlot.CoinDrawState drawState)
		{
			if (context <= 2)
			{
				CoinSlot.InventoryEntries[slot].UpdateState(item.type, item.stack, CoinSlot.ItemSlotCoinJumpScale, out drawState);
				return;
			}
			if (context - 3 > 1)
			{
				drawState.fadeItem = 0;
				drawState.fadeScale = 1f;
				drawState.coinAnimFrame = 0;
				drawState.coinYOffset = 0f;
				drawState.stackTextScale = 1f;
				drawState.stackTextDrawFadeOverload = -1f;
				return;
			}
			CoinSlot.ChestEntries[slot].UpdateState(item.type, item.stack, CoinSlot.ItemSlotCoinJumpScale, out drawState);
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x0000357B File Offset: 0x0000177B
		public CoinSlot()
		{
		}

		// Token: 0x0600189B RID: 6299 RVA: 0x004E31CC File Offset: 0x004E13CC
		// Note: this type is marked as 'beforefieldinit'.
		static CoinSlot()
		{
		}

		// Token: 0x040012E2 RID: 4834
		private static float[] FadeAnimKeys = new float[]
		{
			0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1f, 1f, 1f,
			1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f,
			1f
		};

		// Token: 0x040012E3 RID: 4835
		private static float[] TextAnimKeys = new float[]
		{
			1f, 1.0107f, 1.0391f, 1.0791f, 1.125f, 1.1709f, 1.2109f, 1.2393f, 1.25f, 1.2393f,
			1.2109f, 1.1709f, 1.125f, 1.0791f, 1.0391f, 1.0107f, 1f
		};

		// Token: 0x040012E4 RID: 4836
		private static float[] JumpAnimKeys = new float[]
		{
			0f, 0.23748f, 0.43408f, 0.59366f, 0.72007f, 0.81717f, 0.88881f, 0.93885f, 0.97115f, 0.98955f,
			0.99793f, 1f, 1f, 1f, 0.99793f, 0.98955f, 0.97115f, 0.93885f, 0.88881f, 0.81717f,
			0.72007f, 0.59366f, 0.43408f, 0.23748f, 0f
		};

		// Token: 0x040012E5 RID: 4837
		private static int JumpApex = 12;

		// Token: 0x040012E6 RID: 4838
		private static int JumpTrigger_TextAnimRangeStart = 9;

		// Token: 0x040012E7 RID: 4839
		private static int JumpTrigger_TextAnimRangeEnd = 13;

		// Token: 0x040012E8 RID: 4840
		private static float ItemSlotCoinJumpScale = 10f;

		// Token: 0x040012E9 RID: 4841
		private static float SavingsCoinJumpScale = 10f;

		// Token: 0x040012EA RID: 4842
		private static int JumpAnimHoldTime = 12;

		// Token: 0x040012EB RID: 4843
		private static int SpinAnimRangeStart = 9;

		// Token: 0x040012EC RID: 4844
		private static int SpinAnimRangeEnd = 13;

		// Token: 0x040012ED RID: 4845
		private static CoinSlot.CoinEntry[] Savings = new CoinSlot.CoinEntry[4];

		// Token: 0x040012EE RID: 4846
		private static CoinSlot.CoinEntry[] ChestEntries = new CoinSlot.CoinEntry[200];

		// Token: 0x040012EF RID: 4847
		private static CoinSlot.CoinEntry[] InventoryEntries = new CoinSlot.CoinEntry[59];

		// Token: 0x040012F0 RID: 4848
		private static Dictionary<int, CoinSlot.CoinEntryRef> Custom = new Dictionary<int, CoinSlot.CoinEntryRef>();

		// Token: 0x020006FE RID: 1790
		private struct CoinEntry
		{
			// Token: 0x06003FDD RID: 16349 RVA: 0x0069BDDC File Offset: 0x00699FDC
			public void ForceState(int itemType, int itemStack)
			{
				this.Type = itemType;
				this.Stack = itemStack;
				this.TextAnimFrame = 0;
				this.JumpAnimFrame = 0;
				this.JumpAnimHold = 0;
				this.SpinAnimFrame = 0;
				this.FadeItemType = 0;
			}

			// Token: 0x06003FDE RID: 16350 RVA: 0x0069BE10 File Offset: 0x0069A010
			public void UpdateState(int itemType, int itemStack, float jumpScale, out CoinSlot.CoinDrawState drawState)
			{
				if (this.Type != itemType || this.DrawActive == 0)
				{
					bool flag = true;
					if (itemType != 0 && this.FadeItemType == itemType && this.DrawActive != 0)
					{
						flag = false;
					}
					if (itemType == 0 && this.DrawActive != 0 && ItemID.Sets.CommonCoin[this.Type])
					{
						this.FadeItemType = this.Type;
					}
					else
					{
						this.FadeItemType = 0;
					}
					if (this.FadeItemType != 0)
					{
						flag = false;
					}
					this.Type = itemType;
					if (this.DrawActive == 0)
					{
						this.Stack = itemStack;
					}
					if (flag)
					{
						this.TextAnimFrame = 0;
						this.JumpAnimFrame = 0;
						this.JumpAnimHold = 0;
						this.SpinAnimFrame = 0;
					}
				}
				this.DrawActive = 2;
				if (ItemID.Sets.CommonCoin[this.Type] || this.Type == 3817 || this.FadeItemType != 0)
				{
					if (this.Stack != itemStack)
					{
						this.Stack = itemStack;
						if (this.TextAnimFrame == 0)
						{
							this.TextAnimFrame = CoinSlot.TextAnimKeys.Length - 1;
						}
					}
					if (this.TextAnimFrame >= CoinSlot.JumpTrigger_TextAnimRangeStart && this.TextAnimFrame <= CoinSlot.JumpTrigger_TextAnimRangeEnd)
					{
						this.JumpAnimHold = CoinSlot.JumpAnimHoldTime;
						if (this.JumpAnimFrame == 0)
						{
							this.JumpAnimFrame = CoinSlot.JumpAnimKeys.Length - 1;
						}
					}
				}
				drawState.stackTextScale = CoinSlot.TextAnimKeys[this.TextAnimFrame];
				drawState.coinYOffset = CoinSlot.JumpAnimKeys[this.JumpAnimFrame] * jumpScale;
				drawState.coinAnimFrame = this.SpinAnimFrame / 2;
				drawState.fadeItem = this.FadeItemType;
				drawState.fadeScale = 1f;
				if (this.FadeItemType != 0)
				{
					if (this.TextAnimFrame > 0 || this.JumpAnimFrame >= CoinSlot.JumpApex || this.JumpAnimFrame >= CoinSlot.FadeAnimKeys.Length)
					{
						drawState.stackTextDrawFadeOverload = 1f;
					}
					else
					{
						drawState.stackTextDrawFadeOverload = CoinSlot.FadeAnimKeys[this.JumpAnimFrame];
					}
					drawState.fadeScale = drawState.stackTextDrawFadeOverload;
					return;
				}
				if (this.Stack != 1 || (this.TextAnimFrame <= 0 && this.JumpAnimFrame == 0))
				{
					drawState.stackTextDrawFadeOverload = -1f;
					return;
				}
				if (this.TextAnimFrame > 0 || this.JumpAnimFrame >= CoinSlot.JumpApex || this.JumpAnimFrame >= CoinSlot.FadeAnimKeys.Length)
				{
					drawState.stackTextDrawFadeOverload = 1f;
					return;
				}
				drawState.stackTextDrawFadeOverload = CoinSlot.FadeAnimKeys[this.JumpAnimFrame];
			}

			// Token: 0x06003FDF RID: 16351 RVA: 0x0069C05C File Offset: 0x0069A25C
			public void UpdateAnim()
			{
				if (this.DrawActive > 0)
				{
					this.DrawActive--;
				}
				if (this.FadeItemType > 0 && this.JumpAnimFrame == 0 && this.TextAnimFrame == 0)
				{
					this.FadeItemType = 0;
				}
				if (this.TextAnimFrame > 0)
				{
					this.TextAnimFrame--;
				}
				if (this.JumpAnimHold > 0)
				{
					this.JumpAnimHold--;
				}
				if (this.JumpAnimFrame > 0)
				{
					if (this.JumpAnimHold > 0)
					{
						if (this.JumpAnimFrame != CoinSlot.JumpApex)
						{
							if (this.JumpAnimFrame < CoinSlot.JumpApex)
							{
								this.JumpAnimFrame = CoinSlot.JumpApex + CoinSlot.JumpApex - this.JumpAnimFrame;
							}
							this.JumpAnimFrame--;
						}
					}
					else
					{
						this.JumpAnimFrame--;
					}
				}
				if (this.JumpAnimFrame >= CoinSlot.SpinAnimRangeStart && this.JumpAnimFrame <= CoinSlot.SpinAnimRangeEnd)
				{
					this.SpinAnimFrame = (this.SpinAnimFrame + 1) % 14;
					return;
				}
				if (this.SpinAnimFrame != 0)
				{
					this.SpinAnimFrame = (this.SpinAnimFrame + 1) % 14;
				}
			}

			// Token: 0x0400684E RID: 26702
			public int Type;

			// Token: 0x0400684F RID: 26703
			public int Stack;

			// Token: 0x04006850 RID: 26704
			public int TextAnimFrame;

			// Token: 0x04006851 RID: 26705
			public int JumpAnimFrame;

			// Token: 0x04006852 RID: 26706
			public int SpinAnimFrame;

			// Token: 0x04006853 RID: 26707
			public int DrawActive;

			// Token: 0x04006854 RID: 26708
			public int JumpAnimHold;

			// Token: 0x04006855 RID: 26709
			public int FadeItemType;
		}

		// Token: 0x020006FF RID: 1791
		private class CoinEntryRef
		{
			// Token: 0x06003FE0 RID: 16352 RVA: 0x0000357B File Offset: 0x0000177B
			public CoinEntryRef()
			{
			}

			// Token: 0x04006856 RID: 26710
			public CoinSlot.CoinEntry val;
		}

		// Token: 0x02000700 RID: 1792
		public struct CoinDrawState
		{
			// Token: 0x04006857 RID: 26711
			public int coinAnimFrame;

			// Token: 0x04006858 RID: 26712
			public float coinYOffset;

			// Token: 0x04006859 RID: 26713
			public float stackTextScale;

			// Token: 0x0400685A RID: 26714
			public float stackTextDrawFadeOverload;

			// Token: 0x0400685B RID: 26715
			public int fadeItem;

			// Token: 0x0400685C RID: 26716
			public float fadeScale;
		}
	}
}
