using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameInput;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F6 RID: 1014
	public class UIKeybindingSliderItem : UIElement
	{
		// Token: 0x06002EB9 RID: 11961 RVA: 0x005ADAAC File Offset: 0x005ABCAC
		public UIKeybindingSliderItem(Func<string> getText, Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, int sliderIDInPage, Color color)
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
			Func<float> func2;
			if (getStatus == null)
			{
				func2 = () => 0f;
			}
			else
			{
				func2 = getStatus;
			}
			this._GetStatusFunction = func2;
			Action<float> action;
			if (setStatusKeyboard == null)
			{
				action = delegate(float s)
				{
				};
			}
			else
			{
				action = setStatusKeyboard;
			}
			this._SlideKeyboardAction = action;
			Action action2;
			if (setStatusGamepad == null)
			{
				action2 = delegate
				{
				};
			}
			else
			{
				action2 = setStatusGamepad;
			}
			this._SlideGamepadAction = action2;
			this._sliderIDInPage = sliderIDInPage;
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x005ADB94 File Offset: 0x005ABD94
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float num = 6f;
			base.DrawSelf(spriteBatch);
			int num2 = 0;
			IngameOptions.rightHover = -1;
			if (!Main.mouseLeft)
			{
				IngameOptions.rightLock = -1;
			}
			if (IngameOptions.rightLock == this._sliderIDInPage)
			{
				num2 = 1;
			}
			else if (IngameOptions.rightLock != -1)
			{
				num2 = 2;
			}
			CalculatedStyle dimensions = base.GetDimensions();
			float num3 = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			bool flag = false;
			bool flag2 = base.IsMouseHovering;
			if (num2 == 1)
			{
				flag2 = true;
			}
			if (num2 == 2)
			{
				flag2 = false;
			}
			Vector2 vector2 = new Vector2(0.8f);
			Color color = (flag ? Color.Gold : (flag2 ? Color.White : Color.Silver));
			color = Color.Lerp(color, Color.White, flag2 ? 0.5f : 0f);
			Color color2 = (flag2 ? this._color : this._color.MultiplyRGBA(new Color(180, 180, 180)));
			Vector2 vector3 = vector;
			Utils.DrawSettingsPanel(spriteBatch, vector3, num3, color2);
			vector3.X += 8f;
			vector3.Y += 2f + num;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, this._TextDisplayFunction(), vector3, color, 0f, Vector2.Zero, vector2, num3, 2f);
			vector3.X -= 17f;
			TextureAssets.ColorBar.Frame(1, 1, 0, 0, 0, 0);
			vector3 = new Vector2(dimensions.X + dimensions.Width - 10f, dimensions.Y + 10f + num);
			IngameOptions.valuePosition = vector3;
			float num4 = IngameOptions.DrawValueBar(spriteBatch, 1f, this._GetStatusFunction(), num2, null);
			if (IngameOptions.inBar || IngameOptions.rightLock == this._sliderIDInPage)
			{
				IngameOptions.rightHover = this._sliderIDInPage;
				if (PlayerInput.Triggers.Current.MouseLeft && PlayerInput.CurrentProfile.AllowEditing && !PlayerInput.UsingGamepad && IngameOptions.rightLock == this._sliderIDInPage)
				{
					this._SlideKeyboardAction(num4);
				}
			}
			if (IngameOptions.rightHover != -1 && IngameOptions.rightLock == -1)
			{
				IngameOptions.rightLock = IngameOptions.rightHover;
			}
			if (base.IsMouseHovering && PlayerInput.CurrentProfile.AllowEditing)
			{
				this._SlideGamepadAction();
			}
		}

		// Token: 0x040055D9 RID: 21977
		private Color _color;

		// Token: 0x040055DA RID: 21978
		private Func<string> _TextDisplayFunction;

		// Token: 0x040055DB RID: 21979
		private Func<float> _GetStatusFunction;

		// Token: 0x040055DC RID: 21980
		private Action<float> _SlideKeyboardAction;

		// Token: 0x040055DD RID: 21981
		private Action _SlideGamepadAction;

		// Token: 0x040055DE RID: 21982
		private int _sliderIDInPage;

		// Token: 0x040055DF RID: 21983
		private Asset<Texture2D> _toggleTexture;

		// Token: 0x02000933 RID: 2355
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004813 RID: 18451 RVA: 0x006CC8D8 File Offset: 0x006CAAD8
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004814 RID: 18452 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004815 RID: 18453 RVA: 0x006CC8D1 File Offset: 0x006CAAD1
			internal string <.ctor>b__7_0()
			{
				return "???";
			}

			// Token: 0x06004816 RID: 18454 RVA: 0x006CC8A0 File Offset: 0x006CAAA0
			internal float <.ctor>b__7_1()
			{
				return 0f;
			}

			// Token: 0x06004817 RID: 18455 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <.ctor>b__7_2(float s)
			{
			}

			// Token: 0x06004818 RID: 18456 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <.ctor>b__7_3()
			{
			}

			// Token: 0x0400751B RID: 29979
			public static readonly UIKeybindingSliderItem.<>c <>9 = new UIKeybindingSliderItem.<>c();

			// Token: 0x0400751C RID: 29980
			public static Func<string> <>9__7_0;

			// Token: 0x0400751D RID: 29981
			public static Func<float> <>9__7_1;

			// Token: 0x0400751E RID: 29982
			public static Action<float> <>9__7_2;

			// Token: 0x0400751F RID: 29983
			public static Action <>9__7_3;
		}
	}
}
