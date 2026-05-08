using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003DF RID: 991
	public class UISearchBar : UIElement
	{
		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002E0C RID: 11788 RVA: 0x005A7E28 File Offset: 0x005A6028
		// (remove) Token: 0x06002E0D RID: 11789 RVA: 0x005A7E60 File Offset: 0x005A6060
		public event Action<string> OnContentsChanged
		{
			[CompilerGenerated]
			add
			{
				Action<string> action = this.OnContentsChanged;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> action3 = (Action<string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref this.OnContentsChanged, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string> action = this.OnContentsChanged;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> action3 = (Action<string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref this.OnContentsChanged, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002E0E RID: 11790 RVA: 0x005A7E98 File Offset: 0x005A6098
		// (remove) Token: 0x06002E0F RID: 11791 RVA: 0x005A7ED0 File Offset: 0x005A60D0
		public event Action OnStartTakingInput
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnStartTakingInput;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnStartTakingInput, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnStartTakingInput;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnStartTakingInput, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06002E10 RID: 11792 RVA: 0x005A7F08 File Offset: 0x005A6108
		// (remove) Token: 0x06002E11 RID: 11793 RVA: 0x005A7F40 File Offset: 0x005A6140
		public event Action OnEndTakingInput
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnEndTakingInput;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnEndTakingInput, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnEndTakingInput;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnEndTakingInput, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000055 RID: 85
		// (add) Token: 0x06002E12 RID: 11794 RVA: 0x005A7F78 File Offset: 0x005A6178
		// (remove) Token: 0x06002E13 RID: 11795 RVA: 0x005A7FB0 File Offset: 0x005A61B0
		public event Action OnNeedingVirtualKeyboard
		{
			[CompilerGenerated]
			add
			{
				Action action = this.OnNeedingVirtualKeyboard;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnNeedingVirtualKeyboard, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.OnNeedingVirtualKeyboard;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.OnNeedingVirtualKeyboard, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x005A7FE5 File Offset: 0x005A61E5
		public bool HasContents
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.actualContents);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x005A7FF5 File Offset: 0x005A61F5
		// (set) Token: 0x06002E16 RID: 11798 RVA: 0x005A7FFD File Offset: 0x005A61FD
		public bool IsWritingText
		{
			[CompilerGenerated]
			get
			{
				return this.<IsWritingText>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsWritingText>k__BackingField = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x005A8006 File Offset: 0x005A6206
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x005A800E File Offset: 0x005A620E
		public int MaxInputLength
		{
			get
			{
				return this._maxInputLength;
			}
			set
			{
				this._maxInputLength = value;
				this._text.SetTextMaxLength(this._maxInputLength);
			}
		}

		// Token: 0x06002E19 RID: 11801 RVA: 0x005A8028 File Offset: 0x005A6228
		public UISearchBar(LocalizedText emptyContentText, float scale)
		{
			this._textToShowWhenEmpty = emptyContentText;
			this._textScale = scale;
			this._text = new UITextBox("", scale, false)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				BackgroundColor = Color.Transparent,
				BorderColor = Color.Transparent,
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				TextHAlign = 0f,
				ShowInputTicker = false
			};
			this.MaxInputLength = 50;
			base.Append(this._text);
		}

		// Token: 0x06002E1A RID: 11802 RVA: 0x005A80D8 File Offset: 0x005A62D8
		public void SetContents(string contents, bool forced = false)
		{
			if (this.actualContents == contents && !forced)
			{
				return;
			}
			this.actualContents = contents;
			if (string.IsNullOrEmpty(this.actualContents))
			{
				this._text.TextColor = Color.Gray;
				this._text.SetText(this._textToShowWhenEmpty.Value, this._textScale, false);
			}
			else
			{
				this._text.TextColor = Color.White;
				this._text.SetText(this.actualContents);
				this.actualContents = this._text.Text;
			}
			this.TrimDisplayIfOverElementDimensions(0);
			if (this.OnContentsChanged != null)
			{
				this.OnContentsChanged(contents);
			}
		}

		// Token: 0x06002E1B RID: 11803 RVA: 0x005A8188 File Offset: 0x005A6388
		public void TrimDisplayIfOverElementDimensions(int padding)
		{
			CalculatedStyle dimensions = base.GetDimensions();
			if (dimensions.Width == 0f && dimensions.Height == 0f)
			{
				return;
			}
			Point point = new Point((int)dimensions.X, (int)dimensions.Y);
			Point point2 = new Point(point.X + (int)dimensions.Width, point.Y + (int)dimensions.Height);
			Rectangle rectangle = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			CalculatedStyle calculatedStyle = this._text.GetDimensions();
			Point point3 = new Point((int)calculatedStyle.X, (int)calculatedStyle.Y);
			Point point4 = new Point(point3.X + (int)this._text.MinWidth.Pixels, point3.Y + (int)this._text.MinHeight.Pixels);
			Rectangle rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
			while (rectangle2.Right > rectangle.Right - padding && this._text.Text.Length > 0)
			{
				this._text.SetText(Utils.TrimLastCharacter(this._text.Text));
				this.RecalculateChildren();
				calculatedStyle = this._text.GetDimensions();
				point3 = new Point((int)calculatedStyle.X, (int)calculatedStyle.Y);
				point4 = new Point(point3.X + (int)this._text.MinWidth.Pixels, point3.Y + (int)this._text.MinHeight.Pixels);
				rectangle2 = new Rectangle(point3.X, point3.Y, point4.X - point3.X, point4.Y - point3.Y);
				this.actualContents = this._text.Text;
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x005A839B File Offset: 0x005A659B
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002E1D RID: 11805 RVA: 0x005A83B9 File Offset: 0x005A65B9
		public override void Update(GameTime gameTime)
		{
			if (this.IsWritingText)
			{
				if (this.NeedsVirtualkeyboard())
				{
					if (this.OnNeedingVirtualKeyboard != null)
					{
						this.OnNeedingVirtualKeyboard();
					}
					return;
				}
				PlayerInput.WritingText = true;
				Main.CurrentInputTextTakerOverride = this;
			}
			base.Update(gameTime);
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x0059CE40 File Offset: 0x0059B040
		private bool NeedsVirtualkeyboard()
		{
			return PlayerInput.SettingsForUI.ShowGamepadHints;
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x005A83F4 File Offset: 0x005A65F4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (!this.IsWritingText)
			{
				return;
			}
			PlayerInput.WritingText = true;
			Main.instance.HandleIME();
			Rectangle rectangle = this._text.GetDimensions().ToRectangle();
			Vector2 vector = new Vector2((float)rectangle.Left, (float)(rectangle.Bottom + 32));
			Main.instance.SetIMEPanelAnchor(vector, 0f);
			string inputText = Main.GetInputText(this.actualContents, false);
			if (Main.inputTextEnter)
			{
				this.ToggleTakingText();
			}
			else if (Main.inputTextEscape)
			{
				Main.inputTextEscape = false;
				this.ToggleTakingText();
			}
			this.SetContents(inputText, false);
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x005A8498 File Offset: 0x005A6698
		public void ToggleTakingText()
		{
			this.IsWritingText = !this.IsWritingText;
			this._text.ShowInputTicker = this.IsWritingText;
			Main.clrInput();
			if (this.IsWritingText)
			{
				if (this.OnStartTakingInput != null)
				{
					this.OnStartTakingInput();
					return;
				}
			}
			else
			{
				if (this.OnEndTakingInput != null)
				{
					this.OnEndTakingInput();
				}
				PlayerInput.WritingText = false;
				Main.instance.HandleIME();
			}
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x005A8509 File Offset: 0x005A6709
		public override void OnDeactivate()
		{
			if (this.IsWritingText)
			{
				this.ToggleTakingText();
			}
		}

		// Token: 0x04005522 RID: 21794
		private readonly LocalizedText _textToShowWhenEmpty;

		// Token: 0x04005523 RID: 21795
		private UITextBox _text;

		// Token: 0x04005524 RID: 21796
		private string actualContents;

		// Token: 0x04005525 RID: 21797
		private float _textScale;

		// Token: 0x04005526 RID: 21798
		[CompilerGenerated]
		private Action<string> OnContentsChanged;

		// Token: 0x04005527 RID: 21799
		[CompilerGenerated]
		private Action OnStartTakingInput;

		// Token: 0x04005528 RID: 21800
		[CompilerGenerated]
		private Action OnEndTakingInput;

		// Token: 0x04005529 RID: 21801
		[CompilerGenerated]
		private Action OnNeedingVirtualKeyboard;

		// Token: 0x0400552A RID: 21802
		[CompilerGenerated]
		private bool <IsWritingText>k__BackingField;

		// Token: 0x0400552B RID: 21803
		private int _maxInputLength;
	}
}
