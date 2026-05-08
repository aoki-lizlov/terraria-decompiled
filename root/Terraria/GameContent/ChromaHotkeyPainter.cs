using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ReLogic.Peripherals.RGB;
using Terraria.GameInput;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x02000279 RID: 633
	public class ChromaHotkeyPainter
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x0054C8BE File Offset: 0x0054AABE
		public bool PotionAlert
		{
			get
			{
				return this._quickHealAlert != 0;
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x0054C8CC File Offset: 0x0054AACC
		public void CollectBoundKeys()
		{
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair in this._keys)
			{
				keyValuePair.Value.Unbind();
			}
			this._keys.Clear();
			foreach (KeyValuePair<string, List<string>> keyValuePair2 in PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus)
			{
				this._keys.Add(keyValuePair2.Key, new ChromaHotkeyPainter.PaintKey(keyValuePair2.Key, keyValuePair2.Value));
			}
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair3 in this._keys)
			{
				keyValuePair3.Value.Bind();
			}
			this._wasdKeys = new List<ChromaHotkeyPainter.PaintKey>
			{
				this._keys["Up"],
				this._keys["Down"],
				this._keys["Left"],
				this._keys["Right"]
			};
			this._healKey = this._keys["QuickHeal"];
			this._mountKey = this._keys["QuickMount"];
			this._jumpKey = this._keys["Jump"];
			this._grappleKey = this._keys["Grapple"];
			this._throwKey = this._keys["Throw"];
			this._manaKey = this._keys["QuickMana"];
			this._buffKey = this._keys["QuickBuff"];
			this._smartCursorKey = this._keys["SmartCursor"];
			this._smartSelectKey = this._keys["SmartSelect"];
			this._reactiveKeys.Clear();
			this._xnaKeysInUse.Clear();
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair4 in this._keys)
			{
				this._xnaKeysInUse.AddRange(keyValuePair4.Value.GetXNAKeysInUse());
			}
			this._xnaKeysInUse = this._xnaKeysInUse.Distinct<Keys>().ToList<Keys>();
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00009E46 File Offset: 0x00008046
		[Old("Reactive keys are no longer used so this catch-all method isn't used")]
		public void PressKey(Keys key)
		{
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x0054CB90 File Offset: 0x0054AD90
		private ChromaHotkeyPainter.ReactiveRGBKey FindReactiveKey(Keys keyTarget)
		{
			return this._reactiveKeys.FirstOrDefault((ChromaHotkeyPainter.ReactiveRGBKey x) => x.XNAKey == keyTarget);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0054CBC4 File Offset: 0x0054ADC4
		public void Update()
		{
			this._player = Main.LocalPlayer;
			if (!FocusHelper.AllowChroma)
			{
				this.Step_ClearAll();
				return;
			}
			if (this.PotionAlert)
			{
				foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair in this._keys)
				{
					if (keyValuePair.Key != "QuickHeal")
					{
						keyValuePair.Value.SetClear();
					}
				}
				this.Step_QuickHeal();
			}
			else
			{
				this.Step_Movement();
				this.Step_QuickHeal();
			}
			if (Main.InGameUI.CurrentState == Main.ManageControlsMenu)
			{
				this.Step_ClearAll();
				this.Step_KeybindsMenu();
			}
			this.Step_UpdateReactiveKeys();
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0054CC88 File Offset: 0x0054AE88
		private void SetGroupColorBase(List<ChromaHotkeyPainter.PaintKey> keys, Color color)
		{
			foreach (ChromaHotkeyPainter.PaintKey paintKey in keys)
			{
				paintKey.SetSolid(color);
			}
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0054CCD4 File Offset: 0x0054AED4
		private void SetGroupClear(List<ChromaHotkeyPainter.PaintKey> keys)
		{
			foreach (ChromaHotkeyPainter.PaintKey paintKey in keys)
			{
				paintKey.SetClear();
			}
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x0054CD20 File Offset: 0x0054AF20
		private void Step_KeybindsMenu()
		{
			this.SetGroupColorBase(this._wasdKeys, ChromaHotkeyPainter.PainterColors.MovementKeys);
			this._jumpKey.SetSolid(ChromaHotkeyPainter.PainterColors.MovementKeys);
			this._grappleKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickGrapple);
			this._mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMount);
			this._quickHealAlert = 0;
			this._healKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickHealReady);
			this._manaKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMana);
			this._throwKey.SetSolid(ChromaHotkeyPainter.PainterColors.Throw);
			this._smartCursorKey.SetSolid(ChromaHotkeyPainter.PainterColors.SmartCursor);
			this._smartSelectKey.SetSolid(ChromaHotkeyPainter.PainterColors.SmartSelect);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x0054CDC8 File Offset: 0x0054AFC8
		private void Step_UpdateReactiveKeys()
		{
			using (List<ChromaHotkeyPainter.ReactiveRGBKey>.Enumerator enumerator = this._reactiveKeys.FindAll((ChromaHotkeyPainter.ReactiveRGBKey x) => x.Expired).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ChromaHotkeyPainter.ReactiveRGBKey key = enumerator.Current;
					key.Clear();
					if (!this._keys.Any((KeyValuePair<string, ChromaHotkeyPainter.PaintKey> x) => x.Value.UsesKey(key.XNAKey)))
					{
						key.Unbind();
					}
				}
			}
			this._reactiveKeys.RemoveAll((ChromaHotkeyPainter.ReactiveRGBKey x) => x.Expired);
			foreach (ChromaHotkeyPainter.ReactiveRGBKey reactiveRGBKey in this._reactiveKeys)
			{
				reactiveRGBKey.Update();
			}
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x0054CEDC File Offset: 0x0054B0DC
		private void Step_ClearAll()
		{
			foreach (KeyValuePair<string, ChromaHotkeyPainter.PaintKey> keyValuePair in this._keys)
			{
				keyValuePair.Value.SetClear();
			}
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x0054CF34 File Offset: 0x0054B134
		private void Step_SmartKeys()
		{
			ChromaHotkeyPainter.PaintKey smartCursorKey = this._smartCursorKey;
			ChromaHotkeyPainter.PaintKey smartSelectKey = this._smartSelectKey;
			if (this._player.dead || this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned || this._player.noItems)
			{
				smartCursorKey.SetClear();
				smartSelectKey.SetClear();
				return;
			}
			if (Main.SmartCursorWanted)
			{
				smartCursorKey.SetSolid(ChromaHotkeyPainter.PainterColors.SmartCursor);
			}
			else
			{
				smartCursorKey.SetClear();
			}
			if (this._player.controlTorch)
			{
				smartSelectKey.SetSolid(ChromaHotkeyPainter.PainterColors.SmartSelect);
				return;
			}
			smartSelectKey.SetClear();
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x0054CFE4 File Offset: 0x0054B1E4
		private void Step_Movement()
		{
			List<ChromaHotkeyPainter.PaintKey> wasdKeys = this._wasdKeys;
			bool flag = this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned;
			if (this._player.dead)
			{
				this.SetGroupClear(wasdKeys);
				return;
			}
			if (flag)
			{
				this.SetGroupColorBase(wasdKeys, ChromaHotkeyPainter.PainterColors.DangerKeyBlocked);
				return;
			}
			this.SetGroupColorBase(wasdKeys, ChromaHotkeyPainter.PainterColors.MovementKeys);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x0054D060 File Offset: 0x0054B260
		private void Step_Mount()
		{
			ChromaHotkeyPainter.PaintKey mountKey = this._mountKey;
			if (this._player.QuickMount_GetItemToUse() == null || this._player.dead)
			{
				mountKey.SetClear();
				return;
			}
			if (this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned || this._player.gravDir == -1f || this._player.noItems)
			{
				mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked);
				if (this._player.gravDir == -1f)
				{
					mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked * 0.6f);
				}
				return;
			}
			mountKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMount);
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x0054D128 File Offset: 0x0054B328
		private void Step_Grapple()
		{
			ChromaHotkeyPainter.PaintKey grappleKey = this._grappleKey;
			if (this._player.QuickGrapple_GetItemToUse() == null || this._player.dead)
			{
				grappleKey.SetClear();
				return;
			}
			if (this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned || this._player.noItems)
			{
				grappleKey.SetSolid(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked);
				return;
			}
			grappleKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickGrapple);
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x0054D1B8 File Offset: 0x0054B3B8
		private void Step_Jump()
		{
			ChromaHotkeyPainter.PaintKey jumpKey = this._jumpKey;
			if (this._player.dead)
			{
				jumpKey.SetClear();
				return;
			}
			if (this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned)
			{
				jumpKey.SetSolid(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked);
				return;
			}
			jumpKey.SetSolid(ChromaHotkeyPainter.PainterColors.MovementKeys);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x0054D22C File Offset: 0x0054B42C
		private void Step_QuickHeal()
		{
			ChromaHotkeyPainter.PaintKey healKey = this._healKey;
			if (this._player.QuickHeal_GetItemToUse() == null || this._player.dead)
			{
				healKey.SetClear();
				this._quickHealAlert = 0;
				return;
			}
			if (this._player.potionDelay > 0)
			{
				float lerpValue = Utils.GetLerpValue((float)this._player.potionDelayTime, 0f, (float)this._player.potionDelay, true);
				Color color = Color.Lerp(ChromaHotkeyPainter.PainterColors.DangerKeyBlocked, ChromaHotkeyPainter.PainterColors.QuickHealCooldown, lerpValue) * lerpValue * lerpValue * lerpValue;
				healKey.SetSolid(color);
				this._quickHealAlert = 0;
				return;
			}
			if (this._player.statLife == this._player.statLifeMax2)
			{
				healKey.SetClear();
				this._quickHealAlert = 0;
				return;
			}
			if ((float)this._player.statLife <= (float)this._player.statLifeMax2 / 4f)
			{
				if (this._quickHealAlert != 1)
				{
					this._quickHealAlert = 1;
					healKey.SetAlert(Color.Black, ChromaHotkeyPainter.PainterColors.QuickHealReadyUrgent, -1f, 2f);
				}
				return;
			}
			if ((float)this._player.statLife <= (float)this._player.statLifeMax2 / 2f)
			{
				if (this._quickHealAlert != 2)
				{
					this._quickHealAlert = 2;
					healKey.SetAlert(Color.Black, ChromaHotkeyPainter.PainterColors.QuickHealReadyUrgent, -1f, 2f);
				}
				return;
			}
			healKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickHealReady);
			this._quickHealAlert = 0;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x0054D39C File Offset: 0x0054B59C
		private void Step_QuickMana()
		{
			ChromaHotkeyPainter.PaintKey manaKey = this._manaKey;
			if (this._player.QuickMana_GetItemToUse() == null || this._player.dead || this._player.statMana == this._player.statManaMax2)
			{
				manaKey.SetClear();
				return;
			}
			manaKey.SetSolid(ChromaHotkeyPainter.PainterColors.QuickMana);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x0054D3F4 File Offset: 0x0054B5F4
		private void Step_Throw()
		{
			ChromaHotkeyPainter.PaintKey throwKey = this._throwKey;
			Item heldItem = this._player.HeldItem;
			if (this._player.dead || this._player.HeldItem.favorited || this._player.noThrow > 0)
			{
				throwKey.SetClear();
				return;
			}
			if (this._player.frozen || this._player.tongued || this._player.webbed || this._player.stoned || this._player.noItems)
			{
				throwKey.SetClear();
				return;
			}
			throwKey.SetSolid(ChromaHotkeyPainter.PainterColors.Throw);
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x0054D49B File Offset: 0x0054B69B
		public ChromaHotkeyPainter()
		{
		}

		// Token: 0x04004DFC RID: 19964
		private readonly Dictionary<string, ChromaHotkeyPainter.PaintKey> _keys = new Dictionary<string, ChromaHotkeyPainter.PaintKey>();

		// Token: 0x04004DFD RID: 19965
		private readonly List<ChromaHotkeyPainter.ReactiveRGBKey> _reactiveKeys = new List<ChromaHotkeyPainter.ReactiveRGBKey>();

		// Token: 0x04004DFE RID: 19966
		private List<Keys> _xnaKeysInUse = new List<Keys>();

		// Token: 0x04004DFF RID: 19967
		private Player _player;

		// Token: 0x04004E00 RID: 19968
		private int _quickHealAlert;

		// Token: 0x04004E01 RID: 19969
		private List<ChromaHotkeyPainter.PaintKey> _wasdKeys = new List<ChromaHotkeyPainter.PaintKey>();

		// Token: 0x04004E02 RID: 19970
		private ChromaHotkeyPainter.PaintKey _healKey;

		// Token: 0x04004E03 RID: 19971
		private ChromaHotkeyPainter.PaintKey _mountKey;

		// Token: 0x04004E04 RID: 19972
		private ChromaHotkeyPainter.PaintKey _jumpKey;

		// Token: 0x04004E05 RID: 19973
		private ChromaHotkeyPainter.PaintKey _grappleKey;

		// Token: 0x04004E06 RID: 19974
		private ChromaHotkeyPainter.PaintKey _throwKey;

		// Token: 0x04004E07 RID: 19975
		private ChromaHotkeyPainter.PaintKey _manaKey;

		// Token: 0x04004E08 RID: 19976
		private ChromaHotkeyPainter.PaintKey _buffKey;

		// Token: 0x04004E09 RID: 19977
		private ChromaHotkeyPainter.PaintKey _smartCursorKey;

		// Token: 0x04004E0A RID: 19978
		private ChromaHotkeyPainter.PaintKey _smartSelectKey;

		// Token: 0x020007F9 RID: 2041
		private class ReactiveRGBKey
		{
			// Token: 0x17000539 RID: 1337
			// (get) Token: 0x060042AF RID: 17071 RVA: 0x006BF9EB File Offset: 0x006BDBEB
			public bool Expired
			{
				get
				{
					return this._expireTime < Main.gameTimeCache.TotalGameTime;
				}
			}

			// Token: 0x060042B0 RID: 17072 RVA: 0x006BFA02 File Offset: 0x006BDC02
			public ReactiveRGBKey(Keys key, Color color, TimeSpan duration, string whatIsThisKeyFor)
			{
				this._color = color;
				this.XNAKey = key;
				this.WhatIsThisKeyFor = whatIsThisKeyFor;
				this._duration = duration;
				this._startTime = Main.gameTimeCache.TotalGameTime;
			}

			// Token: 0x060042B1 RID: 17073 RVA: 0x006BFA38 File Offset: 0x006BDC38
			public void Update()
			{
				float num = (float)Utils.GetLerpValue(this._startTime.TotalSeconds, this._expireTime.TotalSeconds, Main.gameTimeCache.TotalGameTime.TotalSeconds, true);
				this._rgbKey.SetSolid(Color.Lerp(this._color, Color.Black, num));
			}

			// Token: 0x060042B2 RID: 17074 RVA: 0x006BFA91 File Offset: 0x006BDC91
			public void Clear()
			{
				this._rgbKey.Clear();
			}

			// Token: 0x060042B3 RID: 17075 RVA: 0x006BFA9E File Offset: 0x006BDC9E
			public void Unbind()
			{
				Main.Chroma.UnbindKey(this.XNAKey);
			}

			// Token: 0x060042B4 RID: 17076 RVA: 0x006BFAB0 File Offset: 0x006BDCB0
			public void Bind()
			{
				this._rgbKey = Main.Chroma.BindKey(this.XNAKey, this.WhatIsThisKeyFor);
			}

			// Token: 0x060042B5 RID: 17077 RVA: 0x006BFACE File Offset: 0x006BDCCE
			public void Refresh()
			{
				this._startTime = Main.gameTimeCache.TotalGameTime;
				this._expireTime = this._startTime;
				this._expireTime.Add(this._duration);
			}

			// Token: 0x04007199 RID: 29081
			public readonly Keys XNAKey;

			// Token: 0x0400719A RID: 29082
			public readonly string WhatIsThisKeyFor;

			// Token: 0x0400719B RID: 29083
			private readonly Color _color;

			// Token: 0x0400719C RID: 29084
			private readonly TimeSpan _duration;

			// Token: 0x0400719D RID: 29085
			private TimeSpan _startTime;

			// Token: 0x0400719E RID: 29086
			private TimeSpan _expireTime;

			// Token: 0x0400719F RID: 29087
			private RgbKey _rgbKey;
		}

		// Token: 0x020007FA RID: 2042
		private class PaintKey
		{
			// Token: 0x060042B6 RID: 17078 RVA: 0x006BFB00 File Offset: 0x006BDD00
			public PaintKey(string triggerName, List<string> keys)
			{
				this._triggerName = triggerName;
				this._xnaKeys = new List<Keys>();
				using (List<string>.Enumerator enumerator = keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Keys keys2;
						if (Enum.TryParse<Keys>(enumerator.Current, true, out keys2))
						{
							this._xnaKeys.Add(keys2);
						}
					}
				}
				this._rgbKeys = new List<RgbKey>();
			}

			// Token: 0x060042B7 RID: 17079 RVA: 0x006BFB80 File Offset: 0x006BDD80
			public void Unbind()
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					Main.Chroma.UnbindKey(rgbKey.Key);
				}
			}

			// Token: 0x060042B8 RID: 17080 RVA: 0x006BFBDC File Offset: 0x006BDDDC
			public void Bind()
			{
				foreach (Keys keys in this._xnaKeys)
				{
					this._rgbKeys.Add(Main.Chroma.BindKey(keys, this._triggerName));
				}
				this._rgbKeys = this._rgbKeys.Distinct<RgbKey>().ToList<RgbKey>();
			}

			// Token: 0x060042B9 RID: 17081 RVA: 0x006BFC5C File Offset: 0x006BDE5C
			public void SetSolid(Color color)
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					rgbKey.SetSolid(color);
				}
			}

			// Token: 0x060042BA RID: 17082 RVA: 0x006BFCB0 File Offset: 0x006BDEB0
			public void SetClear()
			{
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					rgbKey.Clear();
				}
			}

			// Token: 0x060042BB RID: 17083 RVA: 0x006BFD00 File Offset: 0x006BDF00
			public bool UsesKey(Keys key)
			{
				return this._xnaKeys.Contains(key);
			}

			// Token: 0x060042BC RID: 17084 RVA: 0x006BFD10 File Offset: 0x006BDF10
			public void SetAlert(Color colorBase, Color colorFlash, float time, float flashesPerSecond)
			{
				if (time == -1f)
				{
					time = 10000f;
				}
				foreach (RgbKey rgbKey in this._rgbKeys)
				{
					rgbKey.SetFlashing(colorBase, colorFlash, time, flashesPerSecond);
				}
			}

			// Token: 0x060042BD RID: 17085 RVA: 0x006BFD74 File Offset: 0x006BDF74
			public List<Keys> GetXNAKeysInUse()
			{
				return new List<Keys>(this._xnaKeys);
			}

			// Token: 0x040071A0 RID: 29088
			private string _triggerName;

			// Token: 0x040071A1 RID: 29089
			private List<Keys> _xnaKeys;

			// Token: 0x040071A2 RID: 29090
			private List<RgbKey> _rgbKeys;
		}

		// Token: 0x020007FB RID: 2043
		private static class PainterColors
		{
			// Token: 0x060042BE RID: 17086 RVA: 0x006BFD84 File Offset: 0x006BDF84
			// Note: this type is marked as 'beforefieldinit'.
			static PainterColors()
			{
			}

			// Token: 0x040071A3 RID: 29091
			private const float HOTKEY_COLOR_MULTIPLIER = 1f;

			// Token: 0x040071A4 RID: 29092
			public static readonly Color MovementKeys = Color.Gray * 1f;

			// Token: 0x040071A5 RID: 29093
			public static readonly Color QuickMount = Color.RoyalBlue * 1f;

			// Token: 0x040071A6 RID: 29094
			public static readonly Color QuickGrapple = Color.Lerp(Color.RoyalBlue, Color.Blue, 0.5f) * 1f;

			// Token: 0x040071A7 RID: 29095
			public static readonly Color QuickHealReady = Color.Pink * 1f;

			// Token: 0x040071A8 RID: 29096
			public static readonly Color QuickHealReadyUrgent = Color.DeepPink * 1f;

			// Token: 0x040071A9 RID: 29097
			public static readonly Color QuickHealCooldown = Color.HotPink * 0.5f * 1f;

			// Token: 0x040071AA RID: 29098
			public static readonly Color QuickMana = new Color(40, 0, 230) * 1f;

			// Token: 0x040071AB RID: 29099
			public static readonly Color Throw = Color.Red * 0.2f * 1f;

			// Token: 0x040071AC RID: 29100
			public static readonly Color SmartCursor = Color.Gold;

			// Token: 0x040071AD RID: 29101
			public static readonly Color SmartSelect = Color.Goldenrod;

			// Token: 0x040071AE RID: 29102
			public static readonly Color DangerKeyBlocked = Color.Red * 1f;
		}

		// Token: 0x020007FC RID: 2044
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x060042BF RID: 17087 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x060042C0 RID: 17088 RVA: 0x006BFE84 File Offset: 0x006BE084
			internal bool <FindReactiveKey>b__0(ChromaHotkeyPainter.ReactiveRGBKey x)
			{
				return x.XNAKey == this.keyTarget;
			}

			// Token: 0x040071AF RID: 29103
			public Keys keyTarget;
		}

		// Token: 0x020007FD RID: 2045
		[CompilerGenerated]
		private sealed class <>c__DisplayClass24_0
		{
			// Token: 0x060042C1 RID: 17089 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass24_0()
			{
			}

			// Token: 0x060042C2 RID: 17090 RVA: 0x006BFE94 File Offset: 0x006BE094
			internal bool <Step_UpdateReactiveKeys>b__2(KeyValuePair<string, ChromaHotkeyPainter.PaintKey> x)
			{
				return x.Value.UsesKey(this.key.XNAKey);
			}

			// Token: 0x040071B0 RID: 29104
			public ChromaHotkeyPainter.ReactiveRGBKey key;
		}

		// Token: 0x020007FE RID: 2046
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060042C3 RID: 17091 RVA: 0x006BFEAD File Offset: 0x006BE0AD
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060042C4 RID: 17092 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060042C5 RID: 17093 RVA: 0x006BFEB9 File Offset: 0x006BE0B9
			internal bool <Step_UpdateReactiveKeys>b__24_0(ChromaHotkeyPainter.ReactiveRGBKey x)
			{
				return x.Expired;
			}

			// Token: 0x060042C6 RID: 17094 RVA: 0x006BFEB9 File Offset: 0x006BE0B9
			internal bool <Step_UpdateReactiveKeys>b__24_1(ChromaHotkeyPainter.ReactiveRGBKey x)
			{
				return x.Expired;
			}

			// Token: 0x040071B1 RID: 29105
			public static readonly ChromaHotkeyPainter.<>c <>9 = new ChromaHotkeyPainter.<>c();

			// Token: 0x040071B2 RID: 29106
			public static Predicate<ChromaHotkeyPainter.ReactiveRGBKey> <>9__24_0;

			// Token: 0x040071B3 RID: 29107
			public static Predicate<ChromaHotkeyPainter.ReactiveRGBKey> <>9__24_1;
		}
	}
}
