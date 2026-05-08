using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E6 RID: 998
	public class EmotesGroupListItem : UIElement
	{
		// Token: 0x06002E57 RID: 11863 RVA: 0x005AA454 File Offset: 0x005A8654
		public EmotesGroupListItem(LocalizedText groupTitle, int groupIndex, int maxEmotesPerRow, params int[] emotes)
		{
			maxEmotesPerRow = 14;
			base.SetPadding(0f);
			this._groupIndex = groupIndex;
			this._maxEmotesPerRow = maxEmotesPerRow;
			this._tempTex = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive", 1);
			int num = emotes.Length / this._maxEmotesPerRow;
			if (emotes.Length % this._maxEmotesPerRow != 0)
			{
				num++;
			}
			this.Height.Set((float)(30 + 36 * num), 0f);
			this.Width.Set(0f, 1f);
			UIElement uielement = new UIElement
			{
				Height = StyleDimension.FromPixels(30f),
				Width = StyleDimension.FromPixelsAndPercent(-20f, 1f),
				HAlign = 0.5f
			};
			uielement.SetPadding(0f);
			base.Append(uielement);
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				VAlign = 1f,
				HAlign = 0.5f,
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			uielement.Append(uihorizontalSeparator);
			UIText uitext = new UIText(groupTitle, 1f, false)
			{
				VAlign = 1f,
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(-6f)
			};
			uielement.Append(uitext);
			float num2 = 6f;
			for (int i = 0; i < emotes.Length; i++)
			{
				int num3 = emotes[i];
				int num4 = i / this._maxEmotesPerRow;
				int num5 = i % this._maxEmotesPerRow;
				int num6 = emotes.Length % this._maxEmotesPerRow;
				if (emotes.Length / this._maxEmotesPerRow != num4)
				{
					num6 = this._maxEmotesPerRow;
				}
				if (num6 == 0)
				{
					num6 = this._maxEmotesPerRow;
				}
				float num7 = 36f * ((float)num6 / 2f);
				num7 -= 16f;
				num7 = -16f;
				EmoteButton emoteButton = new EmoteButton(num3)
				{
					HAlign = 0f,
					VAlign = 0f,
					Top = StyleDimension.FromPixels((float)(30 + num4 * 36) + num2),
					Left = StyleDimension.FromPixels((float)(36 * num5) - num7)
				};
				base.Append(emoteButton);
				emoteButton.SetSnapPoint("Group " + groupIndex, i, null, null);
			}
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x005AA6E4 File Offset: 0x005A88E4
		public override int CompareTo(object obj)
		{
			EmotesGroupListItem emotesGroupListItem = obj as EmotesGroupListItem;
			if (emotesGroupListItem != null)
			{
				return this._groupIndex.CompareTo(emotesGroupListItem._groupIndex);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x04005565 RID: 21861
		private const int TITLE_HEIGHT = 20;

		// Token: 0x04005566 RID: 21862
		private const int SEPARATOR_HEIGHT = 10;

		// Token: 0x04005567 RID: 21863
		private const int SIZE_PER_EMOTE = 36;

		// Token: 0x04005568 RID: 21864
		private Asset<Texture2D> _tempTex;

		// Token: 0x04005569 RID: 21865
		private int _groupIndex;

		// Token: 0x0400556A RID: 21866
		private int _maxEmotesPerRow = 10;
	}
}
