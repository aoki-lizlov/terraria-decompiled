using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F7 RID: 1015
	public class UIKeybindingToggleListItem : UIElement
	{
		// Token: 0x06002EBB RID: 11963 RVA: 0x005ADDEC File Offset: 0x005ABFEC
		public UIKeybindingToggleListItem(Func<string> getText, Func<bool> getStatus, Color color)
		{
			this._color = color;
			this._toggleTexture = Main.Assets.Request<Texture2D>("Images/UI/Settings_Toggle", 1);
			Func<string> func;
			if (getText == null)
			{
				func = () => "???";
			}
			else
			{
				func = getText;
			}
			this._TextDisplayFunction = func;
			Func<bool> func2;
			if (getStatus == null)
			{
				func2 = () => false;
			}
			else
			{
				func2 = getStatus;
			}
			this._IsOnFunction = func2;
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x005ADE74 File Offset: 0x005AC074
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float num = 6f;
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float num2 = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			bool flag = false;
			Vector2 vector2 = new Vector2(0.8f);
			Color color = (flag ? Color.Gold : (base.IsMouseHovering ? Color.White : Color.Silver));
			color = Color.Lerp(color, Color.White, base.IsMouseHovering ? 0.5f : 0f);
			Color color2 = (base.IsMouseHovering ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180)));
			Vector2 vector3 = vector;
			Utils.DrawSettingsPanel(spriteBatch, vector3, num2, color2);
			vector3.X += 8f;
			vector3.Y += 2f + num;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this._TextDisplayFunction(), vector3, color, 0f, Vector2.Zero, vector2, num2, 2f);
			vector3.X -= 17f;
			Rectangle rectangle = new Rectangle(this._IsOnFunction() ? ((this._toggleTexture.Width() - 2) / 2 + 2) : 0, 0, (this._toggleTexture.Width() - 2) / 2, this._toggleTexture.Height());
			Vector2 vector4 = new Vector2((float)rectangle.Width, 0f);
			vector3 = new Vector2(dimensions.X + dimensions.Width - vector4.X - 10f, dimensions.Y + 2f + num);
			spriteBatch.Draw(this._toggleTexture.Value, vector3, new Rectangle?(rectangle), Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
		}

		// Token: 0x040055E0 RID: 21984
		private Color _color;

		// Token: 0x040055E1 RID: 21985
		private Func<string> _TextDisplayFunction;

		// Token: 0x040055E2 RID: 21986
		private Func<bool> _IsOnFunction;

		// Token: 0x040055E3 RID: 21987
		private Asset<Texture2D> _toggleTexture;

		// Token: 0x02000934 RID: 2356
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004819 RID: 18457 RVA: 0x006CC8E4 File Offset: 0x006CAAE4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600481A RID: 18458 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600481B RID: 18459 RVA: 0x006CC8D1 File Offset: 0x006CAAD1
			internal string <.ctor>b__4_0()
			{
				return "???";
			}

			// Token: 0x0600481C RID: 18460 RVA: 0x001DAC3B File Offset: 0x001D8E3B
			internal bool <.ctor>b__4_1()
			{
				return false;
			}

			// Token: 0x04007520 RID: 29984
			public static readonly UIKeybindingToggleListItem.<>c <>9 = new UIKeybindingToggleListItem.<>c();

			// Token: 0x04007521 RID: 29985
			public static Func<string> <>9__4_0;

			// Token: 0x04007522 RID: 29986
			public static Func<bool> <>9__4_1;
		}
	}
}
