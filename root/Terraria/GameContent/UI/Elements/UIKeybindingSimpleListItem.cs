using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F5 RID: 1013
	public class UIKeybindingSimpleListItem : UIElement
	{
		// Token: 0x06002EB7 RID: 11959 RVA: 0x005AD91A File Offset: 0x005ABB1A
		public UIKeybindingSimpleListItem(Func<string> getText, Color color)
		{
			this._color = color;
			Func<string> func;
			if (getText == null)
			{
				func = () => "???";
			}
			else
			{
				func = getText;
			}
			this._GetTextFunction = func;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x005AD954 File Offset: 0x005ABB54
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float num = 6f;
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float num2 = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			Vector2 vector2 = new Vector2(0.8f);
			Color color = (base.IsMouseHovering ? Color.White : Color.Silver);
			color = Color.Lerp(color, Color.White, base.IsMouseHovering ? 0.5f : 0f);
			Color color2 = (base.IsMouseHovering ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180)));
			Vector2 vector3 = vector;
			Utils.DrawSettings2Panel(spriteBatch, vector3, num2, color2);
			vector3.X += 8f;
			vector3.Y += 2f + num;
			string text = this._GetTextFunction();
			Vector2 stringSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, text, vector2, -1f);
			vector3.X = dimensions.X + dimensions.Width / 2f - stringSize.X / 2f;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, text, vector3, color, 0f, Vector2.Zero, vector2, num2, 2f);
		}

		// Token: 0x040055D7 RID: 21975
		private Color _color;

		// Token: 0x040055D8 RID: 21976
		private Func<string> _GetTextFunction;

		// Token: 0x02000932 RID: 2354
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004810 RID: 18448 RVA: 0x006CC8C5 File Offset: 0x006CAAC5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004811 RID: 18449 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004812 RID: 18450 RVA: 0x006CC8D1 File Offset: 0x006CAAD1
			internal string <.ctor>b__2_0()
			{
				return "???";
			}

			// Token: 0x04007519 RID: 29977
			public static readonly UIKeybindingSimpleListItem.<>c <>9 = new UIKeybindingSimpleListItem.<>c();

			// Token: 0x0400751A RID: 29978
			public static Func<string> <>9__2_0;
		}
	}
}
