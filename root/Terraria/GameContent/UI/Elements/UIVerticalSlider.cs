using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003E0 RID: 992
	public class UIVerticalSlider : UISliderBase
	{
		// Token: 0x06002E22 RID: 11810 RVA: 0x005A851C File Offset: 0x005A671C
		public UIVerticalSlider(Func<float> getStatus, Action<float> setStatusKeyboard, Action setStatusGamepad, Color color)
		{
			Func<float> func;
			if (getStatus == null)
			{
				func = () => 0f;
			}
			else
			{
				func = getStatus;
			}
			this._getSliderValue = func;
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
			this._slideGamepadAction = setStatusGamepad;
			this._isReallyMouseOvered = false;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x005A85AC File Offset: 0x005A67AC
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			UISliderBase.CurrentAimedSlider = null;
			if (!Main.mouseLeft || PlayerInput.IgnoreMouseInterface)
			{
				UISliderBase.CurrentLockedSlider = null;
			}
			base.GetUsageLevel();
			this.FillPercent = this._getSliderValue();
			float fillPercent = this.FillPercent;
			bool flag = false;
			if (this.DrawValueBarDynamicWidth(spriteBatch, out fillPercent))
			{
				flag = true;
			}
			if (UISliderBase.CurrentLockedSlider == this || flag)
			{
				UISliderBase.CurrentAimedSlider = this;
				if (PlayerInput.Triggers.Current.MouseLeft && !PlayerInput.UsingGamepad && UISliderBase.CurrentLockedSlider == this)
				{
					this._slideKeyboardAction(fillPercent);
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

		// Token: 0x06002E24 RID: 11812 RVA: 0x005A8698 File Offset: 0x005A6898
		private bool DrawValueBarDynamicWidth(SpriteBatch spriteBatch, out float sliderValueThatWasSet)
		{
			sliderValueThatWasSet = 0f;
			Texture2D value = TextureAssets.ColorBar.Value;
			Rectangle rectangle = base.GetDimensions().ToRectangle();
			Rectangle rectangle2 = new Rectangle(5, 4, 4, 4);
			Utils.DrawSplicedPanel(spriteBatch, value, rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, rectangle2.X, rectangle2.Width, rectangle2.Y, rectangle2.Height, Color.White);
			Rectangle rectangle3 = rectangle;
			rectangle3.X += rectangle2.Left;
			rectangle3.Width -= rectangle2.Right;
			rectangle3.Y += rectangle2.Top;
			rectangle3.Height -= rectangle2.Bottom;
			Texture2D value2 = TextureAssets.MagicPixel.Value;
			Rectangle rectangle4 = new Rectangle(0, 0, 1, 1);
			spriteBatch.Draw(value2, rectangle3, new Rectangle?(rectangle4), this.EmptyColor);
			Rectangle rectangle5 = rectangle3;
			rectangle5.Height = (int)((float)rectangle5.Height * this.FillPercent);
			rectangle5.Y += rectangle3.Height - rectangle5.Height;
			spriteBatch.Draw(value2, rectangle5, new Rectangle?(rectangle4), this.FilledColor);
			Vector2 vector = new Vector2((float)(rectangle5.Center.X + 1), (float)rectangle5.Top);
			Vector2 vector2 = new Vector2((float)(rectangle5.Width + 16), 4f);
			Rectangle rectangle6 = Utils.CenteredRectangle(vector, vector2);
			Rectangle rectangle7 = rectangle6;
			rectangle7.Inflate(2, 2);
			spriteBatch.Draw(value2, rectangle7, new Rectangle?(rectangle4), Color.Black);
			spriteBatch.Draw(value2, rectangle6, new Rectangle?(rectangle4), Color.White);
			Rectangle rectangle8 = rectangle3;
			rectangle8.Inflate(4, 0);
			bool flag = rectangle8.Contains(Main.MouseScreen.ToPoint()) && !PlayerInput.IgnoreMouseInterface;
			this._isReallyMouseOvered = flag;
			bool flag2 = flag;
			if (this.IgnoresMouseInteraction)
			{
				flag2 = false;
			}
			int usageLevel = base.GetUsageLevel();
			if (usageLevel == 2)
			{
				flag2 = false;
			}
			if (usageLevel == 1)
			{
				flag2 = true;
			}
			if (flag2 || usageLevel == 1)
			{
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
			if (flag2)
			{
				sliderValueThatWasSet = Utils.GetLerpValue((float)rectangle3.Bottom, (float)rectangle3.Top, (float)Main.mouseY, true);
				return true;
			}
			return false;
		}

		// Token: 0x0400552C RID: 21804
		public float FillPercent;

		// Token: 0x0400552D RID: 21805
		public Color FilledColor = Main.OurFavoriteColor;

		// Token: 0x0400552E RID: 21806
		public Color EmptyColor = Color.Black;

		// Token: 0x0400552F RID: 21807
		private Func<float> _getSliderValue;

		// Token: 0x04005530 RID: 21808
		private Action<float> _slideKeyboardAction;

		// Token: 0x04005531 RID: 21809
		private Action _slideGamepadAction;

		// Token: 0x04005532 RID: 21810
		private bool _isReallyMouseOvered;

		// Token: 0x04005533 RID: 21811
		private bool _soundedUsage;

		// Token: 0x04005534 RID: 21812
		private bool _alreadyHovered;

		// Token: 0x02000930 RID: 2352
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004807 RID: 18439 RVA: 0x006CC894 File Offset: 0x006CAA94
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004808 RID: 18440 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004809 RID: 18441 RVA: 0x006CC8A0 File Offset: 0x006CAAA0
			internal float <.ctor>b__9_0()
			{
				return 0f;
			}

			// Token: 0x0600480A RID: 18442 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <.ctor>b__9_1(float s)
			{
			}

			// Token: 0x04007512 RID: 29970
			public static readonly UIVerticalSlider.<>c <>9 = new UIVerticalSlider.<>c();

			// Token: 0x04007513 RID: 29971
			public static Func<float> <>9__9_0;

			// Token: 0x04007514 RID: 29972
			public static Action<float> <>9__9_1;
		}
	}
}
