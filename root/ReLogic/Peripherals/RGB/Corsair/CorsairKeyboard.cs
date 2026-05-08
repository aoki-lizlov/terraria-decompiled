using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000049 RID: 73
	internal class CorsairKeyboard : RgbKeyboard
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00007CC4 File Offset: 0x00005EC4
		private CorsairKeyboard(Fragment fragment, CorsairLedPosition[] ledPositions, DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Corsair, fragment, colorProfile)
		{
			this._ledColors = new CorsairLedColor[base.LedCount];
			for (int i = 0; i < ledPositions.Length; i++)
			{
				this._ledColors[i].LedId = ledPositions[i].LedId;
				Keys keys;
				if (CorsairKeyboard._corsairToXnaKeys.TryGetValue(ledPositions[i].LedId, ref keys))
				{
					this._xnaKeyToIndex[keys] = i;
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007D48 File Offset: 0x00005F48
		public override void Present()
		{
			for (int i = 0; i < base.LedCount; i++)
			{
				Vector4 processedLedColor = base.GetProcessedLedColor(i);
				this._ledColors[i].R = (int)(processedLedColor.X * 255f);
				this._ledColors[i].G = (int)(processedLedColor.Y * 255f);
				this._ledColors[i].B = (int)(processedLedColor.Z * 255f);
			}
			if (this._ledColors.Length != 0)
			{
				NativeMethods.CorsairSetLedsColorsAsync(this._ledColors.Length, this._ledColors, IntPtr.Zero, IntPtr.Zero);
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public static CorsairKeyboard Create(int deviceIndex, DeviceColorProfile colorProfile)
		{
			CorsairLedPosition[] ledPositionsForMouseMatOrKeyboard = CorsairHelper.GetLedPositionsForMouseMatOrKeyboard(deviceIndex);
			return new CorsairKeyboard(CorsairHelper.CreateFragment(ledPositionsForMouseMatOrKeyboard, Vector2.Zero), ledPositionsForMouseMatOrKeyboard, colorProfile);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007E18 File Offset: 0x00006018
		public override void Render(IEnumerable<RgbKey> keys)
		{
			foreach (RgbKey rgbKey in keys)
			{
				int num;
				if (this._xnaKeyToIndex.TryGetValue(rgbKey.Key, ref num))
				{
					base.SetLedColor(num, rgbKey.CurrentColor.ToVector4());
				}
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007E84 File Offset: 0x00006084
		// Note: this type is marked as 'beforefieldinit'.
		static CorsairKeyboard()
		{
			Dictionary<CorsairLedId, Keys> dictionary = new Dictionary<CorsairLedId, Keys>();
			dictionary.Add(CorsairLedId.CLK_Escape, 27);
			dictionary.Add(CorsairLedId.CLK_F1, 112);
			dictionary.Add(CorsairLedId.CLK_F2, 113);
			dictionary.Add(CorsairLedId.CLK_F3, 114);
			dictionary.Add(CorsairLedId.CLK_F4, 115);
			dictionary.Add(CorsairLedId.CLK_F5, 116);
			dictionary.Add(CorsairLedId.CLK_F6, 117);
			dictionary.Add(CorsairLedId.CLK_F7, 118);
			dictionary.Add(CorsairLedId.CLK_F8, 119);
			dictionary.Add(CorsairLedId.CLK_F9, 120);
			dictionary.Add(CorsairLedId.CLK_F10, 121);
			dictionary.Add(CorsairLedId.CLK_F11, 122);
			dictionary.Add(CorsairLedId.CLK_GraveAccentAndTilde, 192);
			dictionary.Add(CorsairLedId.CLK_1, 49);
			dictionary.Add(CorsairLedId.CLK_2, 50);
			dictionary.Add(CorsairLedId.CLK_3, 51);
			dictionary.Add(CorsairLedId.CLK_4, 52);
			dictionary.Add(CorsairLedId.CLK_5, 53);
			dictionary.Add(CorsairLedId.CLK_6, 54);
			dictionary.Add(CorsairLedId.CLK_7, 55);
			dictionary.Add(CorsairLedId.CLK_8, 56);
			dictionary.Add(CorsairLedId.CLK_9, 57);
			dictionary.Add(CorsairLedId.CLK_0, 48);
			dictionary.Add(CorsairLedId.CLK_MinusAndUnderscore, 189);
			dictionary.Add(CorsairLedId.CLK_Tab, 9);
			dictionary.Add(CorsairLedId.CLK_Q, 81);
			dictionary.Add(CorsairLedId.CLK_W, 87);
			dictionary.Add(CorsairLedId.CLK_E, 69);
			dictionary.Add(CorsairLedId.CLK_R, 82);
			dictionary.Add(CorsairLedId.CLK_T, 84);
			dictionary.Add(CorsairLedId.CLK_Y, 89);
			dictionary.Add(CorsairLedId.CLK_U, 85);
			dictionary.Add(CorsairLedId.CLK_I, 73);
			dictionary.Add(CorsairLedId.CLK_O, 79);
			dictionary.Add(CorsairLedId.CLK_P, 80);
			dictionary.Add(CorsairLedId.CLK_BracketLeft, 219);
			dictionary.Add(CorsairLedId.CLK_CapsLock, 20);
			dictionary.Add(CorsairLedId.CLK_A, 65);
			dictionary.Add(CorsairLedId.CLK_S, 83);
			dictionary.Add(CorsairLedId.CLK_D, 68);
			dictionary.Add(CorsairLedId.CLK_F, 70);
			dictionary.Add(CorsairLedId.CLK_G, 71);
			dictionary.Add(CorsairLedId.CLK_H, 72);
			dictionary.Add(CorsairLedId.CLK_J, 74);
			dictionary.Add(CorsairLedId.CLK_K, 75);
			dictionary.Add(CorsairLedId.CLK_L, 76);
			dictionary.Add(CorsairLedId.CLK_SemicolonAndColon, 186);
			dictionary.Add(CorsairLedId.CLK_ApostropheAndDoubleQuote, 222);
			dictionary.Add(CorsairLedId.CLK_LeftShift, 160);
			dictionary.Add(CorsairLedId.CLK_Z, 90);
			dictionary.Add(CorsairLedId.CLK_X, 88);
			dictionary.Add(CorsairLedId.CLK_C, 67);
			dictionary.Add(CorsairLedId.CLK_V, 86);
			dictionary.Add(CorsairLedId.CLK_B, 66);
			dictionary.Add(CorsairLedId.CLK_N, 78);
			dictionary.Add(CorsairLedId.CLK_M, 77);
			dictionary.Add(CorsairLedId.CLK_CommaAndLessThan, 188);
			dictionary.Add(CorsairLedId.CLK_PeriodAndBiggerThan, 190);
			dictionary.Add(CorsairLedId.CLK_SlashAndQuestionMark, 191);
			dictionary.Add(CorsairLedId.CLK_LeftCtrl, 162);
			dictionary.Add(CorsairLedId.CLK_LeftAlt, 164);
			dictionary.Add(CorsairLedId.CLK_Space, 32);
			dictionary.Add(CorsairLedId.CLK_RightAlt, 165);
			dictionary.Add(CorsairLedId.CLK_Application, 93);
			dictionary.Add(CorsairLedId.CLK_F12, 123);
			dictionary.Add(CorsairLedId.CLK_PrintScreen, 44);
			dictionary.Add(CorsairLedId.CLK_ScrollLock, 145);
			dictionary.Add(CorsairLedId.CLK_PauseBreak, 19);
			dictionary.Add(CorsairLedId.CLK_Insert, 45);
			dictionary.Add(CorsairLedId.CLK_Home, 36);
			dictionary.Add(CorsairLedId.CLK_PageUp, 33);
			dictionary.Add(CorsairLedId.CLK_BracketRight, 221);
			dictionary.Add(CorsairLedId.CLK_Backslash, 226);
			dictionary.Add(CorsairLedId.CLK_Enter, 13);
			dictionary.Add(CorsairLedId.CLK_EqualsAndPlus, 187);
			dictionary.Add(CorsairLedId.CLK_Backspace, 8);
			dictionary.Add(CorsairLedId.CLK_Delete, 46);
			dictionary.Add(CorsairLedId.CLK_End, 35);
			dictionary.Add(CorsairLedId.CLK_PageDown, 34);
			dictionary.Add(CorsairLedId.CLK_RightShift, 161);
			dictionary.Add(CorsairLedId.CLK_RightCtrl, 163);
			dictionary.Add(CorsairLedId.CLK_UpArrow, 38);
			dictionary.Add(CorsairLedId.CLK_LeftArrow, 37);
			dictionary.Add(CorsairLedId.CLK_DownArrow, 40);
			dictionary.Add(CorsairLedId.CLK_RightArrow, 39);
			dictionary.Add(CorsairLedId.CLK_Mute, 173);
			dictionary.Add(CorsairLedId.CLK_Stop, 178);
			dictionary.Add(CorsairLedId.CLK_ScanPreviousTrack, 177);
			dictionary.Add(CorsairLedId.CLK_PlayPause, 179);
			dictionary.Add(CorsairLedId.CLK_ScanNextTrack, 176);
			dictionary.Add(CorsairLedId.CLK_NumLock, 144);
			dictionary.Add(CorsairLedId.CLK_KeypadSlash, 111);
			dictionary.Add(CorsairLedId.CLK_KeypadAsterisk, 106);
			dictionary.Add(CorsairLedId.CLK_KeypadMinus, 109);
			dictionary.Add(CorsairLedId.CLK_KeypadPlus, 107);
			dictionary.Add(CorsairLedId.CLK_Keypad7, 103);
			dictionary.Add(CorsairLedId.CLK_Keypad8, 104);
			dictionary.Add(CorsairLedId.CLK_Keypad9, 105);
			dictionary.Add(CorsairLedId.CLK_Keypad4, 100);
			dictionary.Add(CorsairLedId.CLK_Keypad5, 101);
			dictionary.Add(CorsairLedId.CLK_Keypad6, 102);
			dictionary.Add(CorsairLedId.CLK_Keypad1, 97);
			dictionary.Add(CorsairLedId.CLK_Keypad2, 98);
			dictionary.Add(CorsairLedId.CLK_Keypad3, 99);
			dictionary.Add(CorsairLedId.CLK_Keypad0, 96);
			dictionary.Add(CorsairLedId.CLK_KeypadPeriodAndDelete, 46);
			dictionary.Add(CorsairLedId.CLK_VolumeUp, 175);
			dictionary.Add(CorsairLedId.CLK_VolumeDown, 174);
			CorsairKeyboard._corsairToXnaKeys = dictionary;
		}

		// Token: 0x040001B8 RID: 440
		private readonly CorsairLedColor[] _ledColors;

		// Token: 0x040001B9 RID: 441
		private readonly Dictionary<Keys, int> _xnaKeyToIndex = new Dictionary<Keys, int>();

		// Token: 0x040001BA RID: 442
		private static readonly Dictionary<CorsairLedId, Keys> _corsairToXnaKeys;
	}
}
