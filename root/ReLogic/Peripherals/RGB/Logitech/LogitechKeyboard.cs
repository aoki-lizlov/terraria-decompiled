using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ReLogic.Peripherals.RGB.Logitech
{
	// Token: 0x0200003E RID: 62
	internal class LogitechKeyboard : RgbKeyboard
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00006C86 File Offset: 0x00004E86
		public LogitechKeyboard(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Logitech, Fragment.FromGrid(new Rectangle(0, 0, 21, 6)), colorProfile)
		{
			this._colors = new byte[base.LedCount * 4];
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public override void Present()
		{
			if (NativeMethods.LogiLedSetTargetDevice(4))
			{
				for (int i = 0; i < base.LedCount; i++)
				{
					Vector4 processedLedColor = base.GetProcessedLedColor(i);
					this._colors[i * 4 + 2] = (byte)(processedLedColor.X * 255f);
					this._colors[i * 4 + 1] = (byte)(processedLedColor.Y * 255f);
					this._colors[i * 4] = (byte)(processedLedColor.Z * 255f);
					this._colors[i * 4 + 3] = byte.MaxValue;
				}
				NativeMethods.LogiLedSetLightingFromBitmap(this._colors);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006D54 File Offset: 0x00004F54
		public override void Render(IEnumerable<RgbKey> keys)
		{
			int num = 0;
			foreach (RgbKey rgbKey in keys)
			{
				KeyName keyName;
				if (LogitechKeyboard.XnaToLogitechKeys.TryGetValue(rgbKey.Key, ref keyName))
				{
					Color color = base.ProcessLedColor(rgbKey.CurrentColor);
					this._excludedKeys[num++] = keyName;
					NativeMethods.LogiLedSetLightingForKeyWithKeyName(keyName, (int)(color.R * 100 / byte.MaxValue), (int)(color.G * 100 / byte.MaxValue), (int)(color.B * 100 / byte.MaxValue));
				}
			}
			NativeMethods.LogiLedExcludeKeysFromBitmap(this._excludedKeys, num);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006E0C File Offset: 0x0000500C
		// Note: this type is marked as 'beforefieldinit'.
		static LogitechKeyboard()
		{
			Dictionary<Keys, KeyName> dictionary = new Dictionary<Keys, KeyName>();
			dictionary.Add(27, KeyName.ESC);
			dictionary.Add(112, KeyName.F1);
			dictionary.Add(113, KeyName.F2);
			dictionary.Add(114, KeyName.F3);
			dictionary.Add(115, KeyName.F4);
			dictionary.Add(116, KeyName.F5);
			dictionary.Add(117, KeyName.F6);
			dictionary.Add(118, KeyName.F7);
			dictionary.Add(119, KeyName.F8);
			dictionary.Add(120, KeyName.F9);
			dictionary.Add(121, KeyName.F10);
			dictionary.Add(122, KeyName.F11);
			dictionary.Add(123, KeyName.F12);
			dictionary.Add(44, KeyName.PRINT_SCREEN);
			dictionary.Add(145, KeyName.SCROLL_LOCK);
			dictionary.Add(19, KeyName.PAUSE_BREAK);
			dictionary.Add(192, KeyName.TILDE);
			dictionary.Add(49, KeyName.ONE);
			dictionary.Add(50, KeyName.TWO);
			dictionary.Add(51, KeyName.THREE);
			dictionary.Add(52, KeyName.FOUR);
			dictionary.Add(53, KeyName.FIVE);
			dictionary.Add(54, KeyName.SIX);
			dictionary.Add(55, KeyName.SEVEN);
			dictionary.Add(56, KeyName.EIGHT);
			dictionary.Add(57, KeyName.NINE);
			dictionary.Add(48, KeyName.ZERO);
			dictionary.Add(189, KeyName.MINUS);
			dictionary.Add(187, KeyName.EQUALS);
			dictionary.Add(8, KeyName.BACKSPACE);
			dictionary.Add(45, KeyName.INSERT);
			dictionary.Add(36, KeyName.HOME);
			dictionary.Add(33, KeyName.PAGE_UP);
			dictionary.Add(144, KeyName.NUM_LOCK);
			dictionary.Add(111, KeyName.NUM_SLASH);
			dictionary.Add(106, KeyName.NUM_ASTERISK);
			dictionary.Add(109, KeyName.NUM_MINUS);
			dictionary.Add(9, KeyName.TAB);
			dictionary.Add(81, KeyName.Q);
			dictionary.Add(87, KeyName.W);
			dictionary.Add(69, KeyName.E);
			dictionary.Add(82, KeyName.R);
			dictionary.Add(84, KeyName.T);
			dictionary.Add(89, KeyName.Y);
			dictionary.Add(85, KeyName.U);
			dictionary.Add(73, KeyName.I);
			dictionary.Add(79, KeyName.O);
			dictionary.Add(80, KeyName.P);
			dictionary.Add(219, KeyName.OPEN_BRACKET);
			dictionary.Add(221, KeyName.CLOSE_BRACKET);
			dictionary.Add(226, KeyName.BACKSLASH);
			dictionary.Add(46, KeyName.KEYBOARD_DELETE);
			dictionary.Add(35, KeyName.END);
			dictionary.Add(34, KeyName.PAGE_DOWN);
			dictionary.Add(103, KeyName.NUM_SEVEN);
			dictionary.Add(104, KeyName.NUM_EIGHT);
			dictionary.Add(105, KeyName.NUM_NINE);
			dictionary.Add(107, KeyName.NUM_PLUS);
			dictionary.Add(20, KeyName.CAPS_LOCK);
			dictionary.Add(65, KeyName.A);
			dictionary.Add(83, KeyName.S);
			dictionary.Add(68, KeyName.D);
			dictionary.Add(70, KeyName.F);
			dictionary.Add(71, KeyName.G);
			dictionary.Add(72, KeyName.H);
			dictionary.Add(74, KeyName.J);
			dictionary.Add(75, KeyName.K);
			dictionary.Add(76, KeyName.L);
			dictionary.Add(186, KeyName.SEMICOLON);
			dictionary.Add(222, KeyName.APOSTROPHE);
			dictionary.Add(13, KeyName.ENTER);
			dictionary.Add(100, KeyName.NUM_FOUR);
			dictionary.Add(101, KeyName.NUM_FIVE);
			dictionary.Add(102, KeyName.NUM_SIX);
			dictionary.Add(160, KeyName.LEFT_SHIFT);
			dictionary.Add(90, KeyName.Z);
			dictionary.Add(88, KeyName.X);
			dictionary.Add(67, KeyName.C);
			dictionary.Add(86, KeyName.V);
			dictionary.Add(66, KeyName.B);
			dictionary.Add(78, KeyName.N);
			dictionary.Add(77, KeyName.M);
			dictionary.Add(188, KeyName.COMMA);
			dictionary.Add(190, KeyName.PERIOD);
			dictionary.Add(191, KeyName.FORWARD_SLASH);
			dictionary.Add(161, KeyName.RIGHT_SHIFT);
			dictionary.Add(38, KeyName.ARROW_UP);
			dictionary.Add(97, KeyName.NUM_ONE);
			dictionary.Add(98, KeyName.NUM_TWO);
			dictionary.Add(99, KeyName.NUM_THREE);
			dictionary.Add(162, KeyName.LEFT_CONTROL);
			dictionary.Add(91, KeyName.LEFT_WINDOWS);
			dictionary.Add(164, KeyName.LEFT_ALT);
			dictionary.Add(32, KeyName.SPACE);
			dictionary.Add(165, KeyName.RIGHT_ALT);
			dictionary.Add(92, KeyName.RIGHT_WINDOWS);
			dictionary.Add(93, KeyName.APPLICATION_SELECT);
			dictionary.Add(163, KeyName.RIGHT_CONTROL);
			dictionary.Add(37, KeyName.ARROW_LEFT);
			dictionary.Add(40, KeyName.ARROW_DOWN);
			dictionary.Add(39, KeyName.ARROW_RIGHT);
			dictionary.Add(96, KeyName.NUM_ZERO);
			LogitechKeyboard.XnaToLogitechKeys = dictionary;
		}

		// Token: 0x0400013A RID: 314
		private readonly byte[] _colors;

		// Token: 0x0400013B RID: 315
		private readonly KeyName[] _excludedKeys = new KeyName[126];

		// Token: 0x0400013C RID: 316
		private static readonly Dictionary<Keys, KeyName> XnaToLogitechKeys;
	}
}
