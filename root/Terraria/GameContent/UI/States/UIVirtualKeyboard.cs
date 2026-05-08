using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003B3 RID: 947
	public class UIVirtualKeyboard : UIState
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x0059BD51 File Offset: 0x00599F51
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x0059BD5E File Offset: 0x00599F5E
		public string Text
		{
			get
			{
				return this._textBox.Text;
			}
			set
			{
				this._textBox.SetText(value);
				this.ValidateText();
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x0059BD72 File Offset: 0x00599F72
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x0059BD7F File Offset: 0x00599F7F
		public bool HideContents
		{
			get
			{
				return this._textBox.HideContents;
			}
			set
			{
				this._textBox.HideContents = value;
			}
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0059BD90 File Offset: 0x00599F90
		public UIVirtualKeyboard(string labelText, string startingText, UIVirtualKeyboard.KeyboardSubmitEvent submitAction, Action cancelAction, int inputMode = 0, bool allowEmpty = false, int maxLength = 20)
		{
			this._keyboardContext = inputMode;
			this._allowEmpty = allowEmpty;
			UIVirtualKeyboard.OffsetDown = 0;
			UIVirtualKeyboard.ShouldHideText = false;
			this._lastOffsetDown = 0;
			this._editingSign = this._keyboardContext == 1;
			this._editingChest = this._keyboardContext == 2;
			UIVirtualKeyboard._currentInstance = this;
			this._submitAction = submitAction;
			this._cancelAction = cancelAction;
			this._textureShift = Main.Assets.Request<Texture2D>("Images/UI/VK_Shift", 1);
			this._textureBackspace = Main.Assets.Request<Texture2D>("Images/UI/VK_Backspace", 1);
			this.Top.Pixels = (float)this._lastOffsetDown;
			float num = (float)(-5000 * this._editingSign.ToInt());
			float num2 = 270f;
			float num3 = 0f;
			float num4 = 516f;
			UIElement uielement = new UIElement();
			uielement.Width.Pixels = num4 + 8f + 16f;
			uielement.Top.Precent = num3;
			uielement.Top.Pixels = num2;
			uielement.Height.Pixels = 266f;
			uielement.HAlign = 0.5f;
			uielement.SetPadding(0f);
			this.outerLayer1 = uielement;
			UIElement uielement2 = new UIElement();
			uielement2.Width.Pixels = num4 + 8f + 16f;
			uielement2.Top.Precent = num3;
			uielement2.Top.Pixels = num2;
			uielement2.Height.Pixels = 266f;
			uielement2.HAlign = 0.5f;
			uielement2.SetPadding(0f);
			this.outerLayer2 = uielement2;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Precent = 1f;
			uipanel.Height.Pixels = 225f;
			uipanel.BackgroundColor = new Color(23, 33, 69) * 0.7f;
			uielement.Append(uipanel);
			float num5 = -50f;
			this._textBox = new UITextBox("", 0.78f, true);
			this._textBox.BackgroundColor = Color.Transparent;
			this._textBox.BorderColor = Color.Transparent;
			this._textBox.HAlign = 0.5f;
			this._textBox.Width.Pixels = num4;
			this._textBox.Top.Pixels = num5 + num2 - 10f + num;
			this._textBox.Top.Precent = num3;
			this._textBox.Height.Pixels = 37f;
			base.Append(this._textBox);
			for (int i = 0; i < 10; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int num6 = j * 10 + i;
					UITextPanel<object> uitextPanel = this.CreateKeyboardButton("1234567890qwertyuiopasdfghjkl'zxcvbnm,.?"[num6].ToString(), i, j, 1, true);
					uitextPanel.OnLeftClick += this.TypeText;
					uipanel.Append(uitextPanel);
				}
			}
			this._shiftButton = this.CreateKeyboardButton("", 0, 4, 1, false);
			this._shiftButton.PaddingLeft = 0f;
			this._shiftButton.PaddingRight = 0f;
			this._shiftButton.PaddingBottom = (this._shiftButton.PaddingTop = 0f);
			this._shiftButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this._shiftButton.BorderColor = this._internalBorderColor * 0.7f;
			this._shiftButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this._shiftButton.BorderColor = this._internalBorderColorSelected;
				if (this._keyState != UIVirtualKeyboard.KeyState.Shift)
				{
					this._shiftButton.BackgroundColor = new Color(73, 94, 171);
				}
			};
			this._shiftButton.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this._shiftButton.BorderColor = this._internalBorderColor * 0.7f;
				if (this._keyState != UIVirtualKeyboard.KeyState.Shift)
				{
					this._shiftButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			};
			this._shiftButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.SetKeyState((this._keyState == UIVirtualKeyboard.KeyState.Shift) ? UIVirtualKeyboard.KeyState.Default : UIVirtualKeyboard.KeyState.Shift);
			};
			UIImage uiimage = new UIImage(this._textureShift);
			uiimage.HAlign = 0.5f;
			uiimage.VAlign = 0.5f;
			uiimage.ImageScale = 0.85f;
			this._shiftButton.Append(uiimage);
			uipanel.Append(this._shiftButton);
			this._symbolButton = this.CreateKeyboardButton("@%", 1, 4, 1, false);
			this._symbolButton.PaddingLeft = 0f;
			this._symbolButton.PaddingRight = 0f;
			this._symbolButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this._symbolButton.BorderColor = this._internalBorderColor * 0.7f;
			this._symbolButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this._symbolButton.BorderColor = this._internalBorderColorSelected;
				if (this._keyState != UIVirtualKeyboard.KeyState.Symbol)
				{
					this._symbolButton.BackgroundColor = new Color(73, 94, 171);
				}
			};
			this._symbolButton.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this._symbolButton.BorderColor = this._internalBorderColor * 0.7f;
				if (this._keyState != UIVirtualKeyboard.KeyState.Symbol)
				{
					this._symbolButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			};
			this._symbolButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.SetKeyState((this._keyState == UIVirtualKeyboard.KeyState.Symbol) ? UIVirtualKeyboard.KeyState.Default : UIVirtualKeyboard.KeyState.Symbol);
			};
			uipanel.Append(this._symbolButton);
			this.BuildSpaceBarArea(uipanel);
			this._submitButton = new UITextPanel<LocalizedText>((this._editingSign || this._editingChest) ? Language.GetText("UI.Save") : Language.GetText("UI.Submit"), 0.4f, true);
			this._submitButton.Height.Pixels = 37f;
			this._submitButton.Width.Precent = 0.4f;
			this._submitButton.HAlign = 1f;
			this._submitButton.VAlign = 1f;
			this._submitButton.PaddingLeft = 0f;
			this._submitButton.PaddingRight = 0f;
			this.ValidateText();
			this._submitButton.OnMouseOver += this.FadedMouseOver;
			this._submitButton.OnMouseOut += this.FadedMouseOut;
			this._submitButton.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this.ValidateText();
			};
			this._submitButton.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this.ValidateText();
			};
			this._submitButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				UIVirtualKeyboard.Submit();
			};
			uielement.Append(this._submitButton);
			this._cancelButton = new UITextPanel<LocalizedText>(Language.GetText("UI.Cancel"), 0.4f, true);
			this.StyleKey<LocalizedText>(this._cancelButton, true);
			this._cancelButton.Height.Pixels = 37f;
			this._cancelButton.Width.Precent = 0.4f;
			this._cancelButton.VAlign = 1f;
			this._cancelButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				this._cancelAction();
			};
			this._cancelButton.OnMouseOver += this.FadedMouseOver;
			this._cancelButton.OnMouseOut += this.FadedMouseOut;
			uielement.Append(this._cancelButton);
			this._submitButton2 = new UITextPanel<LocalizedText>((this._editingSign || this._editingChest) ? Language.GetText("UI.Save") : Language.GetText("UI.Submit"), 0.72f, true);
			this._submitButton2.TextColor = Color.Silver;
			this._submitButton2.DrawPanel = false;
			this._submitButton2.Height.Pixels = 60f;
			this._submitButton2.Width.Precent = 0.4f;
			this._submitButton2.HAlign = 0.5f;
			this._submitButton2.VAlign = 0f;
			this._submitButton2.OnMouseOver += delegate(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.85f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.White;
			};
			this._submitButton2.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.72f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.Silver;
			};
			this._submitButton2.Top.Pixels = 50f;
			this._submitButton2.PaddingLeft = 0f;
			this._submitButton2.PaddingRight = 0f;
			this.ValidateText();
			this._submitButton2.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this.ValidateText();
			};
			this._submitButton2.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				this.ValidateText();
			};
			this._submitButton2.OnMouseOver += this.FadedMouseOver;
			this._submitButton2.OnMouseOut += this.FadedMouseOut;
			this._submitButton2.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				if (this.TextIsValidForSubmit())
				{
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					this._submitAction(this.Text.Trim());
				}
			};
			this.outerLayer2.Append(this._submitButton2);
			this._cancelButton2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Cancel"), 0.72f, true);
			this._cancelButton2.TextColor = Color.Silver;
			this._cancelButton2.DrawPanel = false;
			this._cancelButton2.OnMouseOver += delegate(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.85f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.White;
			};
			this._cancelButton2.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.72f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.Silver;
			};
			this._cancelButton2.Height.Pixels = 60f;
			this._cancelButton2.Width.Precent = 0.4f;
			this._cancelButton2.Top.Pixels = 114f;
			this._cancelButton2.VAlign = 0f;
			this._cancelButton2.HAlign = 0.5f;
			this._cancelButton2.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				this._cancelAction();
			};
			this.outerLayer2.Append(this._cancelButton2);
			UITextPanel<object> uitextPanel2 = this.CreateKeyboardButton("", 8, 4, 2, true);
			uitextPanel2.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this._textBox.Backspace();
				this.ValidateText();
			};
			uitextPanel2.PaddingLeft = 0f;
			uitextPanel2.PaddingRight = 0f;
			uitextPanel2.PaddingBottom = (uitextPanel2.PaddingTop = 0f);
			uitextPanel2.Append(new UIImage(this._textureBackspace)
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				ImageScale = 0.92f
			});
			uipanel.Append(uitextPanel2);
			UIText uitext = new UIText(labelText, 0.75f, true);
			uitext.HAlign = 0.5f;
			uitext.Width.Pixels = num4;
			uitext.Top.Pixels = num5 - 37f - 4f + num2 + num;
			uitext.Top.Precent = num3;
			uitext.Height.Pixels = 37f;
			base.Append(uitext);
			this._label = uitext;
			base.Append(uielement);
			this._textBox.SetTextMaxLength(maxLength);
			this.Text = startingText;
			if (this.Text.Length == 0)
			{
				this.SetKeyState(UIVirtualKeyboard.KeyState.Shift);
			}
			UIVirtualKeyboard.ShouldHideText = true;
			UIVirtualKeyboard.OffsetDown = 9999;
			this.UpdateOffsetDown();
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x0059C8BE File Offset: 0x0059AABE
		public void SetMaxInputLength(int length)
		{
			this._textBox.SetTextMaxLength(length);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x0059C8CC File Offset: 0x0059AACC
		private void BuildSpaceBarArea(UIPanel mainPanel)
		{
			UIElement.MouseEvent <>9__1;
			UIElement.MouseEvent <>9__2;
			Action createTheseTwo = delegate
			{
				bool flag = this.CanRestore();
				int num = (flag ? 4 : 5);
				bool editingSign = this._editingSign;
				int num2 = ((flag && editingSign) ? 2 : 3);
				UITextPanel<object> uitextPanel = this.CreateKeyboardButton(Language.GetText("UI.SpaceButton"), 2, 4, (this._editingSign || (this._editingChest && flag)) ? num2 : 6, true);
				UIElement uielement = uitextPanel;
				UIElement.MouseEvent mouseEvent;
				if ((mouseEvent = <>9__1) == null)
				{
					mouseEvent = (<>9__1 = delegate(UIMouseEvent evt, UIElement listeningElement)
					{
						this.PressSpace();
					});
				}
				uielement.OnLeftClick += mouseEvent;
				mainPanel.Append(uitextPanel);
				this._spacebarButton = uitextPanel;
				if (editingSign)
				{
					UITextPanel<object> uitextPanel2 = this.CreateKeyboardButton(Language.GetText("UI.EnterButton"), num, 4, num2, true);
					UIElement uielement2 = uitextPanel2;
					UIElement.MouseEvent mouseEvent2;
					if ((mouseEvent2 = <>9__2) == null)
					{
						mouseEvent2 = (<>9__2 = delegate(UIMouseEvent evt, UIElement listeningElement)
						{
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							this._textBox.Write("\n");
							this.ValidateText();
						});
					}
					uielement2.OnLeftClick += mouseEvent2;
					mainPanel.Append(uitextPanel2);
					this._enterButton = uitextPanel2;
				}
			};
			createTheseTwo();
			if (this.CanRestore())
			{
				UITextPanel<object> restoreBar = this.CreateKeyboardButton(Language.GetText("UI.RestoreButton"), 6, 4, 2, true);
				restoreBar.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					this.RestoreCanceledInput(this._keyboardContext);
					this.ValidateText();
					restoreBar.Remove();
					this._enterButton.Remove();
					this._spacebarButton.Remove();
					createTheseTwo();
				};
				mainPanel.Append(restoreBar);
				this._restoreButton = restoreBar;
			}
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x0059C974 File Offset: 0x0059AB74
		private void PressSpace()
		{
			string text = " ";
			if (this.CustomTextValidationForUpdate != null && !this.CustomTextValidationForUpdate(this.Text + text))
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._textBox.Write(text);
			this.ValidateText();
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x0059C9E5 File Offset: 0x0059ABE5
		private bool CanRestore()
		{
			if (this._editingSign)
			{
				return UIVirtualKeyboard._cancelCacheSign.Length > 0;
			}
			return this._editingChest && UIVirtualKeyboard._cancelCacheChest.Length > 0;
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x0059CA14 File Offset: 0x0059AC14
		private void TypeText(UIMouseEvent evt, UIElement listeningElement)
		{
			string text = ((UITextPanel<object>)listeningElement).Text;
			if (this.CustomTextValidationForUpdate != null && !this.CustomTextValidationForUpdate(this.Text + text))
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			bool flag = this.Text.Length == 0;
			this._textBox.Write(text);
			this.ValidateText();
			if (flag && this.Text.Length > 0 && this._keyState == UIVirtualKeyboard.KeyState.Shift)
			{
				this.SetKeyState(UIVirtualKeyboard.KeyState.Default);
			}
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x0059CABC File Offset: 0x0059ACBC
		public void SetKeyState(UIVirtualKeyboard.KeyState keyState)
		{
			UITextPanel<object> uitextPanel = null;
			UIVirtualKeyboard.KeyState keyState2 = this._keyState;
			if (keyState2 != UIVirtualKeyboard.KeyState.Symbol)
			{
				if (keyState2 == UIVirtualKeyboard.KeyState.Shift)
				{
					uitextPanel = this._shiftButton;
				}
			}
			else
			{
				uitextPanel = this._symbolButton;
			}
			if (uitextPanel != null)
			{
				if (uitextPanel.IsMouseHovering)
				{
					uitextPanel.BackgroundColor = new Color(73, 94, 171);
				}
				else
				{
					uitextPanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				}
			}
			string text = null;
			UITextPanel<object> uitextPanel2 = null;
			switch (keyState)
			{
			case UIVirtualKeyboard.KeyState.Default:
				text = "1234567890qwertyuiopasdfghjkl'zxcvbnm,.?";
				break;
			case UIVirtualKeyboard.KeyState.Symbol:
				text = "1234567890!@#$%^&*()-_+=/\\{}[]<>;:\"`|~£¥";
				uitextPanel2 = this._symbolButton;
				break;
			case UIVirtualKeyboard.KeyState.Shift:
				text = "1234567890QWERTYUIOPASDFGHJKL'ZXCVBNM,.?";
				uitextPanel2 = this._shiftButton;
				break;
			}
			for (int i = 0; i < text.Length; i++)
			{
				this._keyList[i].SetText(text[i].ToString());
			}
			this._keyState = keyState;
			if (uitextPanel2 != null)
			{
				uitextPanel2.BackgroundColor = new Color(93, 114, 191);
			}
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x0059CBB8 File Offset: 0x0059ADB8
		private void ValidateText()
		{
			if (this.TextIsValidForSubmit())
			{
				this._canSubmit = true;
				this._submitButton.TextColor = Color.White;
				if (this._submitButton.IsMouseHovering)
				{
					this._submitButton.BackgroundColor = new Color(73, 94, 171);
					return;
				}
				this._submitButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				return;
			}
			else
			{
				this._canSubmit = false;
				this._submitButton.TextColor = Color.Gray;
				if (this._submitButton.IsMouseHovering)
				{
					this._submitButton.BackgroundColor = new Color(180, 60, 60) * 0.85f;
					return;
				}
				this._submitButton.BackgroundColor = new Color(150, 40, 40) * 0.85f;
				return;
			}
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x0059CC9C File Offset: 0x0059AE9C
		private bool TextIsValidForSubmit()
		{
			if (this.CustomTextValidationForUpdate != null)
			{
				return this.CustomTextValidationForUpdate(this.Text);
			}
			return this.Text.Trim().Length > 0 || this._editingSign || this._editingChest || this._allowEmpty;
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x0059CCF0 File Offset: 0x0059AEF0
		private void StyleKey<T>(UITextPanel<T> button, bool external = false)
		{
			button.PaddingLeft = 0f;
			button.PaddingRight = 0f;
			button.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			if (!external)
			{
				button.BorderColor = this._internalBorderColor * 0.7f;
			}
			button.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				((UITextPanel<T>)listeningElement).BackgroundColor = new Color(73, 94, 171) * 0.85f;
				if (!external)
				{
					((UITextPanel<T>)listeningElement).BorderColor = this._internalBorderColorSelected * 0.85f;
				}
			};
			button.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				((UITextPanel<T>)listeningElement).BackgroundColor = new Color(63, 82, 151) * 0.7f;
				if (!external)
				{
					((UITextPanel<T>)listeningElement).BorderColor = this._internalBorderColor * 0.7f;
				}
			};
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x0059CD88 File Offset: 0x0059AF88
		private UITextPanel<object> CreateKeyboardButton(object text, int x, int y, int width = 1, bool style = true)
		{
			float num = 516f;
			UITextPanel<object> uitextPanel = new UITextPanel<object>(text, 0.4f, true);
			uitextPanel.Width.Pixels = 48f * (float)width + 4f * (float)(width - 1);
			uitextPanel.Height.Pixels = 37f;
			uitextPanel.Left.Precent = 0.5f;
			uitextPanel.Left.Pixels = 52f * (float)x - num * 0.5f;
			uitextPanel.Top.Pixels = 41f * (float)y;
			if (style)
			{
				this.StyleKey<object>(uitextPanel, false);
			}
			for (int i = 0; i < width; i++)
			{
				this._keyList[y * 10 + x + i] = uitextPanel;
			}
			return uitextPanel;
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0059CE40 File Offset: 0x0059B040
		private bool ShouldShowKeyboard()
		{
			return PlayerInput.SettingsForUI.ShowGamepadHints;
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x0059CE48 File Offset: 0x0059B048
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			if (Main.gameMenu)
			{
				if (this.ShouldShowKeyboard())
				{
					this.outerLayer2.Remove();
					if (!this.Elements.Contains(this.outerLayer1))
					{
						base.Append(this.outerLayer1);
					}
					this.outerLayer1.Activate();
					this.outerLayer2.Deactivate();
					this.Recalculate();
					this.RecalculateChildren();
					if (this._labelHeight != 0f)
					{
						this._textBox.Top.Pixels = this._textBoxHeight;
						this._label.Top.Pixels = this._labelHeight;
						this._textBox.Recalculate();
						this._label.Recalculate();
						this._labelHeight = (this._textBoxHeight = 0f);
						UserInterface.ActiveInstance.ResetLasts();
					}
				}
				else
				{
					this.outerLayer1.Remove();
					if (!this.Elements.Contains(this.outerLayer2))
					{
						base.Append(this.outerLayer2);
					}
					this.outerLayer2.Activate();
					this.outerLayer1.Deactivate();
					this.Recalculate();
					this.RecalculateChildren();
					if (this._textBoxHeight == 0f)
					{
						this._textBoxHeight = this._textBox.Top.Pixels;
						this._labelHeight = this._label.Top.Pixels;
						UITextBox textBox = this._textBox;
						textBox.Top.Pixels = textBox.Top.Pixels + 50f;
						UIText label = this._label;
						label.Top.Pixels = label.Top.Pixels + 50f;
						this._textBox.Recalculate();
						this._label.Recalculate();
						UserInterface.ActiveInstance.ResetLasts();
					}
				}
			}
			if (!Main.editSign && this._editingSign)
			{
				IngameFancyUI.Close(false);
				return;
			}
			if (!Main.editChest && this._editingChest)
			{
				IngameFancyUI.Close(false);
				return;
			}
			bool flag = this._textBox.GetOuterDimensions().Width > this._textBox.Parent.GetInnerDimensions().Width;
			this._textBox.HAlign = (flag ? 1f : 0.5f);
			this._textBox.Recalculate();
			base.DrawSelf(spriteBatch);
			this.UpdateOffsetDown();
			UIVirtualKeyboard.OffsetDown = 0;
			UIVirtualKeyboard.ShouldHideText = false;
			this.SetupGamepadPoints(spriteBatch);
			PlayerInput.WritingText = true;
			Main.instance.HandleIME();
			Vector2 vector = new Vector2((float)(Main.screenWidth / 2), (float)(this._textBox.GetDimensions().ToRectangle().Bottom + 32));
			Main.instance.SetIMEPanelAnchor(vector, 0.5f);
			string text = Main.GetInputText(this.Text, this._editingSign);
			if (this._editingSign && Main.inputTextEnter)
			{
				text += "\n";
			}
			else
			{
				if (this._editingChest && Main.inputTextEnter)
				{
					ChestUI.RenameChestSubmit(Main.player[Main.myPlayer]);
					IngameFancyUI.Close(false);
					return;
				}
				if (Main.inputTextEnter && UIVirtualKeyboard.CanSubmit)
				{
					UIVirtualKeyboard.Submit();
				}
				else if (this._editingChest && Main.player[Main.myPlayer].chest < 0)
				{
					ChestUI.RenameChestCancel();
				}
				else if (Main.inputTextEscape && this.TryEscapingMenu())
				{
					return;
				}
			}
			if (IngameFancyUI.CanShowVirtualKeyboard(this._keyboardContext))
			{
				if (text != this.Text)
				{
					if (this.CustomTextValidationForUpdate == null || this.CustomTextValidationForUpdate(text))
					{
						this.Text = text;
					}
					else
					{
						SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					}
				}
				if (this._editingSign)
				{
					this.CopyTextToSign();
				}
				if (this._editingChest)
				{
					this.CopyTextToChest();
				}
			}
			byte b = (byte.MaxValue + Main.tileColor.R * 2) / 3;
			Color color = new Color((int)b, (int)b, (int)b, 255);
			this._textBox.TextColor = Color.Lerp(Color.White, color, 0.2f);
			this._label.TextColor = Color.Lerp(Color.White, color, 0.2f);
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x0059D251 File Offset: 0x0059B451
		private bool TryEscapingMenu()
		{
			if (this._cancelAction != null)
			{
				UIVirtualKeyboard.Cancel();
				return true;
			}
			if (this._editingSign)
			{
				Main.InputTextSignCancel();
			}
			if (this._editingChest)
			{
				ChestUI.RenameChestCancel();
			}
			if (!Main.gameMenu)
			{
				IngameFancyUI.Close(false);
			}
			return true;
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x0059D28C File Offset: 0x0059B48C
		private void UpdateOffsetDown()
		{
			this._textBox.HideSelf = UIVirtualKeyboard.ShouldHideText;
			int num = UIVirtualKeyboard.OffsetDown - this._lastOffsetDown;
			int num2 = num;
			if (Math.Abs(num) < 10)
			{
				num2 = num;
			}
			this._lastOffsetDown += num2;
			if (num2 == 0)
			{
				return;
			}
			this.Top.Pixels = this.Top.Pixels + (float)num2;
			this.Recalculate();
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x0059D2ED File Offset: 0x0059B4ED
		public override void OnActivate()
		{
			this.SetupGamepadPoints(null);
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3002);
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x0059D307 File Offset: 0x0059B507
		public override void OnDeactivate()
		{
			base.OnDeactivate();
			PlayerInput.WritingText = false;
			Main.instance.HandleIME();
			UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 0;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0059D328 File Offset: 0x0059B528
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 6;
			UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS = 1;
			int num = 3002;
			int num2 = 5;
			int num3 = 10;
			int num4 = num3 * num2 - 1;
			int num5 = num3 * (num2 - 1);
			UILinkPointNavigator.SetPosition(3000, this._cancelButton.GetDimensions().Center());
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[3000];
			uilinkPoint.Unlink();
			uilinkPoint.Right = 3001;
			uilinkPoint.Up = num + num5;
			UILinkPointNavigator.SetPosition(3001, this._submitButton.GetDimensions().Center());
			uilinkPoint = UILinkPointNavigator.Points[3001];
			uilinkPoint.Unlink();
			uilinkPoint.Left = 3000;
			uilinkPoint.Up = num + num4;
			for (int i = 0; i < num2; i++)
			{
				for (int j = 0; j < num3; j++)
				{
					int num6 = i * num3 + j;
					int num7 = num + num6;
					if (this._keyList[num6] != null)
					{
						UILinkPointNavigator.SetPosition(num7, this._keyList[num6].GetDimensions().Center());
						uilinkPoint = UILinkPointNavigator.Points[num7];
						uilinkPoint.Unlink();
						int num8 = j - 1;
						while (num8 >= 0 && this._keyList[i * num3 + num8] == this._keyList[num6])
						{
							num8--;
						}
						if (num8 != -1)
						{
							uilinkPoint.Left = i * num3 + num8 + num;
						}
						else
						{
							uilinkPoint.Left = i * num3 + (num3 - 1) + num;
						}
						int num9 = j + 1;
						while (num9 <= num3 - 1 && this._keyList[i * num3 + num9] == this._keyList[num6])
						{
							num9++;
						}
						if (num9 != num3 && this._keyList[num6] != this._keyList[num9])
						{
							uilinkPoint.Right = i * num3 + num9 + num;
						}
						else
						{
							uilinkPoint.Right = i * num3 + num;
						}
						if (i != 0)
						{
							uilinkPoint.Up = num7 - num3;
						}
						if (i != num2 - 1)
						{
							uilinkPoint.Down = num7 + num3;
						}
						else
						{
							uilinkPoint.Down = ((j < num2) ? 3000 : 3001);
						}
					}
				}
			}
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0059D564 File Offset: 0x0059B764
		public static void CycleSymbols()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			switch (UIVirtualKeyboard._currentInstance._keyState)
			{
			case UIVirtualKeyboard.KeyState.Default:
				UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Shift);
				return;
			case UIVirtualKeyboard.KeyState.Symbol:
				UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Default);
				return;
			case UIVirtualKeyboard.KeyState.Shift:
				UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Symbol);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x0059D5BA File Offset: 0x0059B7BA
		public static void BackSpace()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			UIVirtualKeyboard._currentInstance._textBox.Backspace();
			UIVirtualKeyboard._currentInstance.ValidateText();
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06002C94 RID: 11412 RVA: 0x0059D5F2 File Offset: 0x0059B7F2
		public static bool CanSubmit
		{
			get
			{
				return UIVirtualKeyboard._currentInstance != null && UIVirtualKeyboard._currentInstance._canSubmit;
			}
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x0059D607 File Offset: 0x0059B807
		public static void Submit()
		{
			if (UIVirtualKeyboard._currentInstance != null)
			{
				UIVirtualKeyboard._currentInstance.InternalSubmit();
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x0059D61C File Offset: 0x0059B81C
		private void InternalSubmit()
		{
			string text = this.Text.Trim();
			if (this.TextIsValidForSubmit())
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				this._submitAction(text);
			}
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x0059D65E File Offset: 0x0059B85E
		public static void Cancel()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			UIVirtualKeyboard._currentInstance._cancelAction();
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x0059D68C File Offset: 0x0059B88C
		public static void Write(string text)
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			bool flag = UIVirtualKeyboard._currentInstance.Text.Length == 0;
			UIVirtualKeyboard._currentInstance._textBox.Write(text);
			UIVirtualKeyboard._currentInstance.ValidateText();
			if (flag && UIVirtualKeyboard._currentInstance.Text.Length > 0 && UIVirtualKeyboard._currentInstance._keyState == UIVirtualKeyboard.KeyState.Shift)
			{
				UIVirtualKeyboard._currentInstance.SetKeyState(UIVirtualKeyboard.KeyState.Default);
			}
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x0059D70E File Offset: 0x0059B90E
		public static void CursorLeft()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			UIVirtualKeyboard._currentInstance._textBox.CursorLeft();
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x0059D73C File Offset: 0x0059B93C
		public static void CursorRight()
		{
			if (UIVirtualKeyboard._currentInstance == null)
			{
				return;
			}
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			UIVirtualKeyboard._currentInstance._textBox.CursorRight();
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x0059D76A File Offset: 0x0059B96A
		public static bool CanDisplay(int keyboardContext)
		{
			return keyboardContext != 1 || Main.screenHeight > 700;
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x0059D77E File Offset: 0x0059B97E
		public static int KeyboardContext
		{
			get
			{
				if (UIVirtualKeyboard._currentInstance == null)
				{
					return -1;
				}
				return UIVirtualKeyboard._currentInstance._keyboardContext;
			}
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x0059D793 File Offset: 0x0059B993
		public static void CacheCanceledInput(int cacheMode)
		{
			if (cacheMode == 1)
			{
				UIVirtualKeyboard._cancelCacheSign = Main.npcChatText;
			}
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x0059D7A3 File Offset: 0x0059B9A3
		private void RestoreCanceledInput(int cacheMode)
		{
			if (cacheMode == 1)
			{
				Main.npcChatText = UIVirtualKeyboard._cancelCacheSign;
				this.Text = Main.npcChatText;
				UIVirtualKeyboard._cancelCacheSign = "";
			}
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x0059D7C8 File Offset: 0x0059B9C8
		private void CopyTextToSign()
		{
			if (!this._editingSign)
			{
				return;
			}
			int sign = Main.player[Main.myPlayer].sign;
			if (sign < 0 || Main.sign[sign] == null)
			{
				return;
			}
			Main.npcChatText = this.Text;
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x0059D808 File Offset: 0x0059BA08
		private void CopyTextToChest()
		{
			if (!this._editingChest)
			{
				return;
			}
			Main.npcChatText = this.Text;
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x0059D820 File Offset: 0x0059BA20
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x00588AB1 File Offset: 0x00586CB1
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x0059D875 File Offset: 0x0059BA75
		// Note: this type is marked as 'beforefieldinit'.
		static UIVirtualKeyboard()
		{
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x0059D88B File Offset: 0x0059BA8B
		[CompilerGenerated]
		private void <.ctor>b__49_0(UIMouseEvent evt, UIElement listeningElement)
		{
			this._shiftButton.BorderColor = this._internalBorderColorSelected;
			if (this._keyState != UIVirtualKeyboard.KeyState.Shift)
			{
				this._shiftButton.BackgroundColor = new Color(73, 94, 171);
			}
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x0059D8C0 File Offset: 0x0059BAC0
		[CompilerGenerated]
		private void <.ctor>b__49_1(UIMouseEvent evt, UIElement listeningElement)
		{
			this._shiftButton.BorderColor = this._internalBorderColor * 0.7f;
			if (this._keyState != UIVirtualKeyboard.KeyState.Shift)
			{
				this._shiftButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			}
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x0059D914 File Offset: 0x0059BB14
		[CompilerGenerated]
		private void <.ctor>b__49_2(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.SetKeyState((this._keyState == UIVirtualKeyboard.KeyState.Shift) ? UIVirtualKeyboard.KeyState.Default : UIVirtualKeyboard.KeyState.Shift);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x0059D93E File Offset: 0x0059BB3E
		[CompilerGenerated]
		private void <.ctor>b__49_3(UIMouseEvent evt, UIElement listeningElement)
		{
			this._symbolButton.BorderColor = this._internalBorderColorSelected;
			if (this._keyState != UIVirtualKeyboard.KeyState.Symbol)
			{
				this._symbolButton.BackgroundColor = new Color(73, 94, 171);
			}
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x0059D974 File Offset: 0x0059BB74
		[CompilerGenerated]
		private void <.ctor>b__49_4(UIMouseEvent evt, UIElement listeningElement)
		{
			this._symbolButton.BorderColor = this._internalBorderColor * 0.7f;
			if (this._keyState != UIVirtualKeyboard.KeyState.Symbol)
			{
				this._symbolButton.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			}
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x0059D9C8 File Offset: 0x0059BBC8
		[CompilerGenerated]
		private void <.ctor>b__49_5(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.SetKeyState((this._keyState == UIVirtualKeyboard.KeyState.Symbol) ? UIVirtualKeyboard.KeyState.Default : UIVirtualKeyboard.KeyState.Symbol);
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x0059D9F2 File Offset: 0x0059BBF2
		[CompilerGenerated]
		private void <.ctor>b__49_6(UIMouseEvent evt, UIElement listeningElement)
		{
			this.ValidateText();
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x0059D9F2 File Offset: 0x0059BBF2
		[CompilerGenerated]
		private void <.ctor>b__49_7(UIMouseEvent evt, UIElement listeningElement)
		{
			this.ValidateText();
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x0059D9FA File Offset: 0x0059BBFA
		[CompilerGenerated]
		private void <.ctor>b__49_9(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			this._cancelAction();
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x0059D9F2 File Offset: 0x0059BBF2
		[CompilerGenerated]
		private void <.ctor>b__49_12(UIMouseEvent evt, UIElement listeningElement)
		{
			this.ValidateText();
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x0059D9F2 File Offset: 0x0059BBF2
		[CompilerGenerated]
		private void <.ctor>b__49_13(UIMouseEvent evt, UIElement listeningElement)
		{
			this.ValidateText();
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x0059DA1C File Offset: 0x0059BC1C
		[CompilerGenerated]
		private void <.ctor>b__49_14(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this.TextIsValidForSubmit())
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				this._submitAction(this.Text.Trim());
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x0059D9FA File Offset: 0x0059BBFA
		[CompilerGenerated]
		private void <.ctor>b__49_17(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			this._cancelAction();
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x0059DA51 File Offset: 0x0059BC51
		[CompilerGenerated]
		private void <.ctor>b__49_18(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this._textBox.Backspace();
			this.ValidateText();
		}

		// Token: 0x0400540F RID: 21519
		private static UIVirtualKeyboard _currentInstance;

		// Token: 0x04005410 RID: 21520
		private static string _cancelCacheSign = "";

		// Token: 0x04005411 RID: 21521
		private static string _cancelCacheChest = "";

		// Token: 0x04005412 RID: 21522
		private const string DEFAULT_KEYS = "1234567890qwertyuiopasdfghjkl'zxcvbnm,.?";

		// Token: 0x04005413 RID: 21523
		private const string SHIFT_KEYS = "1234567890QWERTYUIOPASDFGHJKL'ZXCVBNM,.?";

		// Token: 0x04005414 RID: 21524
		private const string SYMBOL_KEYS = "1234567890!@#$%^&*()-_+=/\\{}[]<>;:\"`|~£¥";

		// Token: 0x04005415 RID: 21525
		private const float KEY_SPACING = 4f;

		// Token: 0x04005416 RID: 21526
		private const float KEY_WIDTH = 48f;

		// Token: 0x04005417 RID: 21527
		private const float KEY_HEIGHT = 37f;

		// Token: 0x04005418 RID: 21528
		private UITextPanel<object>[] _keyList = new UITextPanel<object>[50];

		// Token: 0x04005419 RID: 21529
		private UITextPanel<object> _shiftButton;

		// Token: 0x0400541A RID: 21530
		private UITextPanel<object> _symbolButton;

		// Token: 0x0400541B RID: 21531
		private UITextBox _textBox;

		// Token: 0x0400541C RID: 21532
		private UITextPanel<LocalizedText> _submitButton;

		// Token: 0x0400541D RID: 21533
		private UITextPanel<LocalizedText> _cancelButton;

		// Token: 0x0400541E RID: 21534
		private UIText _label;

		// Token: 0x0400541F RID: 21535
		private UITextPanel<object> _enterButton;

		// Token: 0x04005420 RID: 21536
		private UITextPanel<object> _spacebarButton;

		// Token: 0x04005421 RID: 21537
		private UITextPanel<object> _restoreButton;

		// Token: 0x04005422 RID: 21538
		private Asset<Texture2D> _textureShift;

		// Token: 0x04005423 RID: 21539
		private Asset<Texture2D> _textureBackspace;

		// Token: 0x04005424 RID: 21540
		private Color _internalBorderColor = new Color(89, 116, 213);

		// Token: 0x04005425 RID: 21541
		private Color _internalBorderColorSelected = Main.OurFavoriteColor;

		// Token: 0x04005426 RID: 21542
		private UITextPanel<LocalizedText> _submitButton2;

		// Token: 0x04005427 RID: 21543
		private UITextPanel<LocalizedText> _cancelButton2;

		// Token: 0x04005428 RID: 21544
		private UIElement outerLayer1;

		// Token: 0x04005429 RID: 21545
		private UIElement outerLayer2;

		// Token: 0x0400542A RID: 21546
		private bool _allowEmpty;

		// Token: 0x0400542B RID: 21547
		private UIVirtualKeyboard.KeyState _keyState;

		// Token: 0x0400542C RID: 21548
		private UIVirtualKeyboard.KeyboardSubmitEvent _submitAction;

		// Token: 0x0400542D RID: 21549
		private Action _cancelAction;

		// Token: 0x0400542E RID: 21550
		private int _lastOffsetDown;

		// Token: 0x0400542F RID: 21551
		public static int OffsetDown;

		// Token: 0x04005430 RID: 21552
		public static bool ShouldHideText;

		// Token: 0x04005431 RID: 21553
		private int _keyboardContext;

		// Token: 0x04005432 RID: 21554
		private bool _editingSign;

		// Token: 0x04005433 RID: 21555
		private bool _editingChest;

		// Token: 0x04005434 RID: 21556
		private float _textBoxHeight;

		// Token: 0x04005435 RID: 21557
		private float _labelHeight;

		// Token: 0x04005436 RID: 21558
		public Func<string, bool> CustomTextValidationForUpdate;

		// Token: 0x04005437 RID: 21559
		public Func<string, bool> CustomTextValidationForSubmit;

		// Token: 0x04005438 RID: 21560
		private bool _canSubmit;

		// Token: 0x02000917 RID: 2327
		// (Invoke) Token: 0x060047B6 RID: 18358
		public delegate void KeyboardSubmitEvent(string text);

		// Token: 0x02000918 RID: 2328
		public enum KeyState
		{
			// Token: 0x040074BE RID: 29886
			Default,
			// Token: 0x040074BF RID: 29887
			Symbol,
			// Token: 0x040074C0 RID: 29888
			Shift
		}

		// Token: 0x02000919 RID: 2329
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060047B9 RID: 18361 RVA: 0x006CC1E6 File Offset: 0x006CA3E6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060047BA RID: 18362 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060047BB RID: 18363 RVA: 0x006CC1F2 File Offset: 0x006CA3F2
			internal void <.ctor>b__49_8(UIMouseEvent evt, UIElement listeningElement)
			{
				UIVirtualKeyboard.Submit();
			}

			// Token: 0x060047BC RID: 18364 RVA: 0x006CC1F9 File Offset: 0x006CA3F9
			internal void <.ctor>b__49_10(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.85f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.White;
			}

			// Token: 0x060047BD RID: 18365 RVA: 0x006CC21B File Offset: 0x006CA41B
			internal void <.ctor>b__49_11(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.72f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.Silver;
			}

			// Token: 0x060047BE RID: 18366 RVA: 0x006CC1F9 File Offset: 0x006CA3F9
			internal void <.ctor>b__49_15(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.85f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.White;
			}

			// Token: 0x060047BF RID: 18367 RVA: 0x006CC21B File Offset: 0x006CA41B
			internal void <.ctor>b__49_16(UIMouseEvent a, UIElement b)
			{
				((UITextPanel<LocalizedText>)b).TextScale = 0.72f;
				((UITextPanel<LocalizedText>)b).TextColor = Color.Silver;
			}

			// Token: 0x040074C1 RID: 29889
			public static readonly UIVirtualKeyboard.<>c <>9 = new UIVirtualKeyboard.<>c();

			// Token: 0x040074C2 RID: 29890
			public static UIElement.MouseEvent <>9__49_8;

			// Token: 0x040074C3 RID: 29891
			public static UIElement.MouseEvent <>9__49_10;

			// Token: 0x040074C4 RID: 29892
			public static UIElement.MouseEvent <>9__49_11;

			// Token: 0x040074C5 RID: 29893
			public static UIElement.MouseEvent <>9__49_15;

			// Token: 0x040074C6 RID: 29894
			public static UIElement.MouseEvent <>9__49_16;
		}

		// Token: 0x0200091A RID: 2330
		[CompilerGenerated]
		private sealed class <>c__DisplayClass51_0
		{
			// Token: 0x060047C0 RID: 18368 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass51_0()
			{
			}

			// Token: 0x060047C1 RID: 18369 RVA: 0x006CC240 File Offset: 0x006CA440
			internal void <BuildSpaceBarArea>b__0()
			{
				bool flag = this.<>4__this.CanRestore();
				int num = (flag ? 4 : 5);
				bool editingSign = this.<>4__this._editingSign;
				int num2 = ((flag && editingSign) ? 2 : 3);
				UITextPanel<object> uitextPanel = this.<>4__this.CreateKeyboardButton(Language.GetText("UI.SpaceButton"), 2, 4, (this.<>4__this._editingSign || (this.<>4__this._editingChest && flag)) ? num2 : 6, true);
				UIElement uielement = uitextPanel;
				UIElement.MouseEvent mouseEvent;
				if ((mouseEvent = this.<>9__1) == null)
				{
					mouseEvent = (this.<>9__1 = delegate(UIMouseEvent evt, UIElement listeningElement)
					{
						this.<>4__this.PressSpace();
					});
				}
				uielement.OnLeftClick += mouseEvent;
				this.mainPanel.Append(uitextPanel);
				this.<>4__this._spacebarButton = uitextPanel;
				if (editingSign)
				{
					UITextPanel<object> uitextPanel2 = this.<>4__this.CreateKeyboardButton(Language.GetText("UI.EnterButton"), num, 4, num2, true);
					UIElement uielement2 = uitextPanel2;
					UIElement.MouseEvent mouseEvent2;
					if ((mouseEvent2 = this.<>9__2) == null)
					{
						mouseEvent2 = (this.<>9__2 = delegate(UIMouseEvent evt, UIElement listeningElement)
						{
							SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
							this.<>4__this._textBox.Write("\n");
							this.<>4__this.ValidateText();
						});
					}
					uielement2.OnLeftClick += mouseEvent2;
					this.mainPanel.Append(uitextPanel2);
					this.<>4__this._enterButton = uitextPanel2;
				}
			}

			// Token: 0x060047C2 RID: 18370 RVA: 0x006CC353 File Offset: 0x006CA553
			internal void <BuildSpaceBarArea>b__1(UIMouseEvent evt, UIElement listeningElement)
			{
				this.<>4__this.PressSpace();
			}

			// Token: 0x060047C3 RID: 18371 RVA: 0x006CC360 File Offset: 0x006CA560
			internal void <BuildSpaceBarArea>b__2(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.<>4__this._textBox.Write("\n");
				this.<>4__this.ValidateText();
			}

			// Token: 0x040074C7 RID: 29895
			public UIVirtualKeyboard <>4__this;

			// Token: 0x040074C8 RID: 29896
			public UIPanel mainPanel;

			// Token: 0x040074C9 RID: 29897
			public Action createTheseTwo;

			// Token: 0x040074CA RID: 29898
			public UIElement.MouseEvent <>9__1;

			// Token: 0x040074CB RID: 29899
			public UIElement.MouseEvent <>9__2;
		}

		// Token: 0x0200091B RID: 2331
		[CompilerGenerated]
		private sealed class <>c__DisplayClass51_1
		{
			// Token: 0x060047C4 RID: 18372 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass51_1()
			{
			}

			// Token: 0x060047C5 RID: 18373 RVA: 0x006CC398 File Offset: 0x006CA598
			internal void <BuildSpaceBarArea>b__3(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				this.CS$<>8__locals1.<>4__this.RestoreCanceledInput(this.CS$<>8__locals1.<>4__this._keyboardContext);
				this.CS$<>8__locals1.<>4__this.ValidateText();
				this.restoreBar.Remove();
				this.CS$<>8__locals1.<>4__this._enterButton.Remove();
				this.CS$<>8__locals1.<>4__this._spacebarButton.Remove();
				this.CS$<>8__locals1.createTheseTwo();
			}

			// Token: 0x040074CC RID: 29900
			public UITextPanel<object> restoreBar;

			// Token: 0x040074CD RID: 29901
			public UIVirtualKeyboard.<>c__DisplayClass51_0 CS$<>8__locals1;
		}

		// Token: 0x0200091C RID: 2332
		[CompilerGenerated]
		private sealed class <>c__DisplayClass59_0<T>
		{
			// Token: 0x060047C6 RID: 18374 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass59_0()
			{
			}

			// Token: 0x060047C7 RID: 18375 RVA: 0x006CC430 File Offset: 0x006CA630
			internal void <StyleKey>b__0(UIMouseEvent evt, UIElement listeningElement)
			{
				((UITextPanel<T>)listeningElement).BackgroundColor = new Color(73, 94, 171) * 0.85f;
				if (!this.external)
				{
					((UITextPanel<T>)listeningElement).BorderColor = this.<>4__this._internalBorderColorSelected * 0.85f;
				}
			}

			// Token: 0x060047C8 RID: 18376 RVA: 0x006CC488 File Offset: 0x006CA688
			internal void <StyleKey>b__1(UIMouseEvent evt, UIElement listeningElement)
			{
				((UITextPanel<T>)listeningElement).BackgroundColor = new Color(63, 82, 151) * 0.7f;
				if (!this.external)
				{
					((UITextPanel<T>)listeningElement).BorderColor = this.<>4__this._internalBorderColor * 0.7f;
				}
			}

			// Token: 0x040074CE RID: 29902
			public bool external;

			// Token: 0x040074CF RID: 29903
			public UIVirtualKeyboard <>4__this;
		}
	}
}
