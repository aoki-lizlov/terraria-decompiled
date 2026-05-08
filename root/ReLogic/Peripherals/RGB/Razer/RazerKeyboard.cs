using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ReLogic.Peripherals.RGB.Razer
{
	// Token: 0x0200003D RID: 61
	internal class RazerKeyboard : RgbKeyboard
	{
		// Token: 0x0600018F RID: 399 RVA: 0x00006593 File Offset: 0x00004793
		public RazerKeyboard(DeviceColorProfile colorProfile)
			: base(RgbDeviceVendor.Razer, Fragment.FromGrid(new Rectangle(0, 0, 22, 6)), colorProfile)
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000065D4 File Offset: 0x000047D4
		public override void Render(IEnumerable<RgbKey> keys)
		{
			foreach (RgbKey rgbKey in keys)
			{
				RazerKey razerKey;
				if (RazerKeyboard.XnaKeyToChromaKey.TryGetValue(rgbKey.Key, ref razerKey))
				{
					uint num = RazerHelper.XnaColorToDeviceColor(base.ProcessLedColor(rgbKey.CurrentColor));
					this._pendingKeys.Add(Tuple.Create<RazerKey, uint>(razerKey, num));
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006650 File Offset: 0x00004850
		public override void Present()
		{
			for (int i = 0; i < this._effect.Color.Length; i++)
			{
				this._effect.Color[i] = 0U;
				this._effect.Key[i] = 0U;
			}
			for (int j = 0; j < base.LedCount; j++)
			{
				this._effect.Color[j] = RazerHelper.Vector4ToDeviceColor(base.GetProcessedLedColor(j));
				this._effect.Key[j] = this._effect.Color[j] & 16777215U;
			}
			for (int k = 0; k < this._pendingKeys.Count; k++)
			{
				int num = (int)this._pendingKeys[k].Item1;
				num = (num >> 8) * 22 + (num & 255);
				this._effect.Key[num] = this._pendingKeys[k].Item2 | 16777216U;
			}
			this._pendingKeys.Clear();
			this._handle.SetAsKeyboardEffect(ref this._effect);
			this._handle.Apply();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006760 File Offset: 0x00004960
		// Note: this type is marked as 'beforefieldinit'.
		static RazerKeyboard()
		{
			Dictionary<Keys, RazerKey> dictionary = new Dictionary<Keys, RazerKey>();
			dictionary.Add(27, RazerKey.Esc);
			dictionary.Add(112, RazerKey.F1);
			dictionary.Add(113, RazerKey.F2);
			dictionary.Add(114, RazerKey.F3);
			dictionary.Add(115, RazerKey.F4);
			dictionary.Add(116, RazerKey.F5);
			dictionary.Add(117, RazerKey.F6);
			dictionary.Add(118, RazerKey.F7);
			dictionary.Add(119, RazerKey.F8);
			dictionary.Add(120, RazerKey.F9);
			dictionary.Add(121, RazerKey.F10);
			dictionary.Add(122, RazerKey.F11);
			dictionary.Add(123, RazerKey.F12);
			dictionary.Add(49, RazerKey.D1);
			dictionary.Add(50, RazerKey.D2);
			dictionary.Add(51, RazerKey.D3);
			dictionary.Add(52, RazerKey.D4);
			dictionary.Add(53, RazerKey.D5);
			dictionary.Add(54, RazerKey.D6);
			dictionary.Add(55, RazerKey.D7);
			dictionary.Add(56, RazerKey.D8);
			dictionary.Add(57, RazerKey.D9);
			dictionary.Add(48, RazerKey.D0);
			dictionary.Add(65, RazerKey.A);
			dictionary.Add(66, RazerKey.B);
			dictionary.Add(67, RazerKey.C);
			dictionary.Add(68, RazerKey.D);
			dictionary.Add(69, RazerKey.E);
			dictionary.Add(70, RazerKey.F);
			dictionary.Add(71, RazerKey.G);
			dictionary.Add(72, RazerKey.H);
			dictionary.Add(73, RazerKey.I);
			dictionary.Add(74, RazerKey.J);
			dictionary.Add(75, RazerKey.K);
			dictionary.Add(76, RazerKey.L);
			dictionary.Add(77, RazerKey.M);
			dictionary.Add(78, RazerKey.N);
			dictionary.Add(79, RazerKey.O);
			dictionary.Add(80, RazerKey.P);
			dictionary.Add(81, RazerKey.Q);
			dictionary.Add(82, RazerKey.R);
			dictionary.Add(83, RazerKey.S);
			dictionary.Add(84, RazerKey.T);
			dictionary.Add(85, RazerKey.U);
			dictionary.Add(86, RazerKey.V);
			dictionary.Add(87, RazerKey.W);
			dictionary.Add(88, RazerKey.X);
			dictionary.Add(89, RazerKey.Y);
			dictionary.Add(90, RazerKey.Z);
			dictionary.Add(144, RazerKey.NumLock);
			dictionary.Add(96, RazerKey.Numpad0);
			dictionary.Add(97, RazerKey.Numpad1);
			dictionary.Add(98, RazerKey.Numpad2);
			dictionary.Add(99, RazerKey.Numpad3);
			dictionary.Add(100, RazerKey.Numpad4);
			dictionary.Add(101, RazerKey.Numpad5);
			dictionary.Add(102, RazerKey.Numpad6);
			dictionary.Add(103, RazerKey.Numpad7);
			dictionary.Add(104, RazerKey.Numpad8);
			dictionary.Add(105, RazerKey.Numpad9);
			dictionary.Add(111, RazerKey.NumpadDivide);
			dictionary.Add(106, RazerKey.NumpadMultiply);
			dictionary.Add(109, RazerKey.NumpadSubtract);
			dictionary.Add(107, RazerKey.NumpadAdd);
			dictionary.Add(13, RazerKey.NumpadEnter);
			dictionary.Add(110, RazerKey.NumpadDecimal);
			dictionary.Add(44, RazerKey.PrintScreen);
			dictionary.Add(145, RazerKey.Scroll);
			dictionary.Add(19, RazerKey.Pause);
			dictionary.Add(45, RazerKey.Insert);
			dictionary.Add(36, RazerKey.Home);
			dictionary.Add(33, RazerKey.PageUp);
			dictionary.Add(46, RazerKey.Delete);
			dictionary.Add(35, RazerKey.End);
			dictionary.Add(34, RazerKey.PageDown);
			dictionary.Add(38, RazerKey.Up);
			dictionary.Add(37, RazerKey.Left);
			dictionary.Add(40, RazerKey.Down);
			dictionary.Add(39, RazerKey.Right);
			dictionary.Add(9, RazerKey.Tab);
			dictionary.Add(20, RazerKey.CapsLock);
			dictionary.Add(8, RazerKey.Backspace);
			dictionary.Add(162, RazerKey.LeftCtrl);
			dictionary.Add(91, RazerKey.LeftWindows);
			dictionary.Add(164, RazerKey.LeftAlt);
			dictionary.Add(32, RazerKey.Space);
			dictionary.Add(165, RazerKey.RightAlt);
			dictionary.Add(93, RazerKey.RightMenu);
			dictionary.Add(163, RazerKey.RightCtrl);
			dictionary.Add(160, RazerKey.LeftShift);
			dictionary.Add(161, RazerKey.RightShift);
			dictionary.Add(192, RazerKey.OemTilde);
			dictionary.Add(189, RazerKey.OemMinus);
			dictionary.Add(219, RazerKey.OemLeftBracket);
			dictionary.Add(221, RazerKey.OemRightBracket);
			dictionary.Add(226, RazerKey.OemBackslash);
			dictionary.Add(186, RazerKey.OemSemicolon);
			dictionary.Add(222, RazerKey.OemApostrophe);
			dictionary.Add(188, RazerKey.OemComma);
			dictionary.Add(190, RazerKey.OemPeriod);
			RazerKeyboard.XnaKeyToChromaKey = dictionary;
		}

		// Token: 0x04000136 RID: 310
		private NativeMethods.CustomKeyboardEffect _effect = NativeMethods.CustomKeyboardEffect.Create();

		// Token: 0x04000137 RID: 311
		private readonly EffectHandle _handle = new EffectHandle();

		// Token: 0x04000138 RID: 312
		private readonly List<Tuple<RazerKey, uint>> _pendingKeys = new List<Tuple<RazerKey, uint>>(132);

		// Token: 0x04000139 RID: 313
		private static readonly Dictionary<Keys, RazerKey> XnaKeyToChromaKey;
	}
}
