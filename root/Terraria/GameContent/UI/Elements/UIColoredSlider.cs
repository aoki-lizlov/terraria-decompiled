using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003F0 RID: 1008
	public class UIColoredSlider : UISliderBase
	{
		// Token: 0x06002E9F RID: 11935 RVA: 0x005ABDBC File Offset: 0x005A9FBC
		public UIColoredSlider(LocalizedText textKey, Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, Func<float, Color> blipColorFunction, Color color)
		{
			this._color = color;
			this._textKey = textKey;
			Func<float> func;
			if (getStatus == null)
			{
				func = () => 0f;
			}
			else
			{
				func = getStatus;
			}
			this._getStatusTextAct = func;
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
			this._slideKeyboardAction = action;
			Func<float, Color> func2;
			if (blipColorFunction == null)
			{
				func2 = (float s) => Color.Lerp(Color.Black, Color.White, s);
			}
			else
			{
				func2 = blipColorFunction;
			}
			this._blipFunc = func2;
			this._slideGamepadAction = setStatusGamepad;
			this._isReallyMouseOvered = false;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x005ABE70 File Offset: 0x005AA070
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			UISliderBase.CurrentAimedSlider = null;
			if (!Main.mouseLeft)
			{
				UISliderBase.CurrentLockedSlider = null;
			}
			int usageLevel = base.GetUsageLevel();
			float num = 8f;
			base.DrawSelf(spriteBatch);
			CalculatedStyle dimensions = base.GetDimensions();
			float num2 = dimensions.Width + 1f;
			Vector2 vector = new Vector2(dimensions.X, dimensions.Y);
			bool flag = false;
			bool flag2 = base.IsMouseHovering;
			if (usageLevel == 2)
			{
				flag2 = false;
			}
			if (usageLevel == 1)
			{
				flag2 = true;
			}
			Vector2 vector2 = vector + new Vector2(0f, 2f);
			Color color = (flag ? Color.Gold : (flag2 ? Color.White : Color.Silver));
			color = Color.Lerp(color, Color.White, flag2 ? 0.5f : 0f);
			Vector2 vector3 = new Vector2(0.8f);
			vector2.X += 8f;
			vector2.Y += num;
			vector2.X -= 17f;
			TextureAssets.ColorBar.Frame(1, 1, 0, 0, 0, 0);
			vector2 = new Vector2(dimensions.X + dimensions.Width - 10f, dimensions.Y + 10f + num);
			bool flag3;
			float num3 = this.DrawValueBar(spriteBatch, vector2, 1f, this._getStatusTextAct(), usageLevel, out flag3, this._blipFunc);
			if (UISliderBase.CurrentLockedSlider == this || flag3)
			{
				UISliderBase.CurrentAimedSlider = this;
				if (PlayerInput.Triggers.Current.MouseLeft && !PlayerInput.UsingGamepad && UISliderBase.CurrentLockedSlider == this)
				{
					this._slideKeyboardAction(num3);
					if (!this._soundedUsage)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					this._soundedUsage = true;
				}
				else
				{
					this._soundedUsage = false;
				}
			}
			if (UISliderBase.CurrentAimedSlider != null && UISliderBase.CurrentLockedSlider == null)
			{
				UISliderBase.CurrentLockedSlider = UISliderBase.CurrentAimedSlider;
			}
			if (this._isReallyMouseOvered)
			{
				this._slideGamepadAction();
			}
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x005AC060 File Offset: 0x005AA260
		private float DrawValueBar(SpriteBatch sb, Vector2 drawPosition, float drawScale, float sliderPosition, int lockMode, out bool wasInBar, Func<float, Color> blipColorFunc)
		{
			Texture2D value = TextureAssets.ColorBar.Value;
			Vector2 vector = new Vector2((float)value.Width, (float)value.Height) * drawScale;
			drawPosition.X -= (float)((int)vector.X);
			Rectangle rectangle = new Rectangle((int)drawPosition.X, (int)drawPosition.Y - (int)vector.Y / 2, (int)vector.X, (int)vector.Y);
			Rectangle rectangle2 = rectangle;
			sb.Draw(value, rectangle, Color.White);
			float num = (float)rectangle.X + 5f * drawScale;
			float num2 = (float)rectangle.Y + 4f * drawScale;
			for (float num3 = 0f; num3 < 167f; num3 += 1f)
			{
				float num4 = num3 / 167f;
				Color color = blipColorFunc(num4);
				sb.Draw(TextureAssets.ColorBlip.Value, new Vector2(num + num3 * drawScale, num2), null, color, 0f, Vector2.Zero, drawScale, SpriteEffects.None, 0f);
			}
			rectangle.X = (int)num - 2;
			rectangle.Y = (int)num2;
			rectangle.Width -= 4;
			rectangle.Height -= 8;
			bool flag = rectangle.Contains(new Point(Main.mouseX, Main.mouseY));
			this._isReallyMouseOvered = flag;
			if (this.IgnoresMouseInteraction)
			{
				flag = false;
			}
			if (lockMode == 2)
			{
				flag = false;
			}
			if (flag || lockMode == 1)
			{
				sb.Draw(TextureAssets.ColorHighlight.Value, rectangle2, Main.OurFavoriteColor);
				if (!this._alreadyHovered)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
				this._alreadyHovered = true;
			}
			else
			{
				this._alreadyHovered = false;
			}
			wasInBar = false;
			if (!this.IgnoresMouseInteraction)
			{
				sb.Draw(TextureAssets.ColorSlider.Value, new Vector2(num + 167f * drawScale * sliderPosition, num2 + 4f * drawScale), null, Color.White, 0f, new Vector2(0.5f * (float)TextureAssets.ColorSlider.Value.Width, 0.5f * (float)TextureAssets.ColorSlider.Value.Height), drawScale, SpriteEffects.None, 0f);
				if (Main.mouseX >= rectangle.X && Main.mouseX <= rectangle.X + rectangle.Width)
				{
					wasInBar = flag;
					return (float)(Main.mouseX - rectangle.X) / (float)rectangle.Width;
				}
			}
			if (rectangle.X >= Main.mouseX)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x040055BE RID: 21950
		private Color _color;

		// Token: 0x040055BF RID: 21951
		private LocalizedText _textKey;

		// Token: 0x040055C0 RID: 21952
		private Func<float> _getStatusTextAct;

		// Token: 0x040055C1 RID: 21953
		private Action<float> _slideKeyboardAction;

		// Token: 0x040055C2 RID: 21954
		private Func<float, Color> _blipFunc;

		// Token: 0x040055C3 RID: 21955
		private Action _slideGamepadAction;

		// Token: 0x040055C4 RID: 21956
		private const bool BOTHER_WITH_TEXT = false;

		// Token: 0x040055C5 RID: 21957
		private bool _isReallyMouseOvered;

		// Token: 0x040055C6 RID: 21958
		private bool _alreadyHovered;

		// Token: 0x040055C7 RID: 21959
		private bool _soundedUsage;

		// Token: 0x02000931 RID: 2353
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600480B RID: 18443 RVA: 0x006CC8A7 File Offset: 0x006CAAA7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600480C RID: 18444 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600480D RID: 18445 RVA: 0x006CC8A0 File Offset: 0x006CAAA0
			internal float <.ctor>b__10_0()
			{
				return 0f;
			}

			// Token: 0x0600480E RID: 18446 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <.ctor>b__10_1(float s)
			{
			}

			// Token: 0x0600480F RID: 18447 RVA: 0x006CC8B3 File Offset: 0x006CAAB3
			internal Color <.ctor>b__10_2(float s)
			{
				return Color.Lerp(Color.Black, Color.White, s);
			}

			// Token: 0x04007515 RID: 29973
			public static readonly UIColoredSlider.<>c <>9 = new UIColoredSlider.<>c();

			// Token: 0x04007516 RID: 29974
			public static Func<float> <>9__10_0;

			// Token: 0x04007517 RID: 29975
			public static Action<float> <>9__10_1;

			// Token: 0x04007518 RID: 29976
			public static Func<float, Color> <>9__10_2;
		}
	}
}
