using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.IO;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000408 RID: 1032
	public abstract class AWorldListItem : UIPanel
	{
		// Token: 0x06002F6A RID: 12138 RVA: 0x005B2754 File Offset: 0x005B0954
		private void UpdateGlitchAnimation(UIElement affectedElement)
		{
			int glitchFrame = this._glitchFrame;
			int num = 3;
			int num2 = 3;
			if (this._glitchFrame == 0)
			{
				num = 15;
				num2 = 120;
			}
			int num3 = this._glitchFrameCounter + 1;
			this._glitchFrameCounter = num3;
			if (num3 >= Main.rand.Next(num, num2 + 1))
			{
				this._glitchFrameCounter = 0;
				this._glitchFrame = (this._glitchFrame + 1) % 16;
				if ((this._glitchFrame == 4 || this._glitchFrame == 8 || this._glitchFrame == 12) && Main.rand.Next(3) == 0)
				{
					this._glitchVariation = Main.rand.Next(7);
				}
			}
			(affectedElement as UIImageFramed).SetFrame(7, 16, this._glitchVariation, this._glitchFrame, 0, 0);
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x005B2810 File Offset: 0x005B0A10
		protected void GetDifficulty(out string expertText, out Color gameModeColor)
		{
			expertText = "";
			gameModeColor = Color.White;
			if (this._data.GameMode == 3)
			{
				expertText = Language.GetTextValue("UI.Creative");
				gameModeColor = Main.creativeModeColor;
				return;
			}
			int num = 1;
			int gameMode = this._data.GameMode;
			if (gameMode != 1)
			{
				if (gameMode == 2)
				{
					num = 3;
				}
			}
			else
			{
				num = 2;
			}
			if (this._data.ForTheWorthy)
			{
				num++;
			}
			switch (num)
			{
			case 2:
				expertText = Language.GetTextValue("UI.Expert");
				gameModeColor = Main.mcColor;
				return;
			case 3:
				expertText = Language.GetTextValue("UI.Master");
				gameModeColor = Main.hcColor;
				return;
			case 4:
				expertText = Language.GetTextValue("UI.Legendary");
				gameModeColor = Main.legendaryModeColor;
				return;
			default:
				expertText = Language.GetTextValue("UI.Normal");
				return;
			}
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x005B28EC File Offset: 0x005B0AEC
		protected Asset<Texture2D> GetIcon()
		{
			if (this._data.ZenithWorld)
			{
				return this.GetSeedIcon("Everything", true, false);
			}
			if (this._data.DrunkWorld)
			{
				return this.GetSeedIcon("CorruptionCrimson", true, false);
			}
			if (this._data.ForTheWorthy)
			{
				return this.GetSeedIcon("FTW", true, true);
			}
			if (this._data.NotTheBees)
			{
				return this.GetSeedIcon("NotTheBees", true, true);
			}
			if (this._data.Anniversary)
			{
				return this.GetSeedIcon("Anniversary", true, true);
			}
			if (this._data.DontStarve)
			{
				return this.GetSeedIcon("DontStarve", true, true);
			}
			if (this._data.RemixWorld)
			{
				return this.GetSeedIcon("Remix", true, true);
			}
			if (this._data.NoTrapsWorld)
			{
				return this.GetSeedIcon("Traps", true, true);
			}
			if (this._data.SkyblockWorld)
			{
				return this.GetSeedIcon("Skyblock", false, false);
			}
			return this.GetSeedIcon("", true, true);
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x005B29FC File Offset: 0x005B0BFC
		protected List<Asset<Texture2D>> GetIcons()
		{
			List<Asset<Texture2D>> list = new List<Asset<Texture2D>>();
			if (this._data.DrunkWorld)
			{
				list.Add(this.GetSeedIcon("CorruptionCrimson", true, false));
			}
			if (this._data.ForTheWorthy)
			{
				list.Add(this.GetSeedIcon("FTW", true, true));
			}
			if (this._data.NotTheBees)
			{
				list.Add(this.GetSeedIcon("NotTheBees", true, true));
			}
			if (this._data.Anniversary)
			{
				list.Add(this.GetSeedIcon("Anniversary", true, true));
			}
			if (this._data.DontStarve)
			{
				list.Add(this.GetSeedIcon("DontStarve", true, true));
			}
			if (this._data.RemixWorld)
			{
				list.Add(this.GetSeedIcon("Remix", true, true));
			}
			if (this._data.NoTrapsWorld)
			{
				list.Add(this.GetSeedIcon("Traps", true, true));
			}
			if (this._data.SkyblockWorld)
			{
				list.Add(this.GetSeedIcon("Skyblock", false, false));
			}
			if (list.Count > 0)
			{
				return list;
			}
			list.Add(this.GetSeedIcon("", true, true));
			return list;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x005B2B30 File Offset: 0x005B0D30
		protected UIElement GetIconElement()
		{
			if (this._data.ZenithWorld)
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/IconEverythingAnimated", 1);
				UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(7, 16, 0, 0, 0, 0));
				uiimageFramed.Left = new StyleDimension(4f, 0f);
				uiimageFramed.OnUpdate += this.UpdateGlitchAnimation;
				return uiimageFramed;
			}
			return new UICyclingImage(this.GetIcons())
			{
				Left = new StyleDimension(4f, 0f)
			};
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x005B2BB4 File Offset: 0x005B0DB4
		private Asset<Texture2D> GetSeedIcon(string seed, bool hardmodeVariants = true, bool evilVariants = true)
		{
			string text = "Images/UI/Icon";
			if (hardmodeVariants)
			{
				text += (this._data.IsHardMode ? "Hallow" : "");
			}
			if (evilVariants)
			{
				text += (this._data.HasCorruption ? "Corruption" : "Crimson");
			}
			text += seed;
			return Main.Assets.Request<Texture2D>(text, 1);
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x005B2C21 File Offset: 0x005B0E21
		protected AWorldListItem()
		{
		}

		// Token: 0x0400566C RID: 22124
		protected WorldFileData _data;

		// Token: 0x0400566D RID: 22125
		protected int _glitchFrameCounter;

		// Token: 0x0400566E RID: 22126
		protected int _glitchFrame;

		// Token: 0x0400566F RID: 22127
		protected int _glitchVariation;
	}
}
