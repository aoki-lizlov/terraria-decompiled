using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Terraria.GameInput
{
	// Token: 0x0200008E RID: 142
	public class PlayerInputProfile
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x004D49F6 File Offset: 0x004D2BF6
		public string ShowName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x004D49FE File Offset: 0x004D2BFE
		public bool HotbarAllowsRadial
		{
			get
			{
				return this.HotbarRadialHoldTimeRequired != -1;
			}
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x004D4A0C File Offset: 0x004D2C0C
		public PlayerInputProfile(string name)
		{
			this.Name = name;
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x004D4AB0 File Offset: 0x004D2CB0
		public void Initialize(PresetProfiles style)
		{
			foreach (KeyValuePair<InputMode, KeyConfiguration> keyValuePair in this.InputModes)
			{
				keyValuePair.Value.SetupKeys();
				PlayerInput.Reset(keyValuePair.Value, style, keyValuePair.Key);
			}
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x004D4B1C File Offset: 0x004D2D1C
		public bool Load(Dictionary<string, object> dict)
		{
			int num = 0;
			object obj;
			if (dict.TryGetValue("Last Launched Version", out obj))
			{
				num = (int)((long)obj);
			}
			if (dict.TryGetValue("Mouse And Keyboard", out obj))
			{
				this.InputModes[InputMode.Keyboard].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)obj).ToString()));
			}
			if (dict.TryGetValue("Gamepad", out obj))
			{
				this.InputModes[InputMode.XBoxGamepad].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)obj).ToString()));
			}
			if (dict.TryGetValue("Mouse And Keyboard UI", out obj))
			{
				this.InputModes[InputMode.KeyboardUI].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)obj).ToString()));
			}
			if (dict.TryGetValue("Gamepad UI", out obj))
			{
				this.InputModes[InputMode.XBoxGamepadUI].ReadPreferences(JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(((JObject)obj).ToString()));
			}
			if (num < 190)
			{
				this.InputModes[InputMode.Keyboard].KeyStatus["ViewZoomIn"] = new List<string>();
				this.InputModes[InputMode.Keyboard].KeyStatus["ViewZoomIn"].AddRange(PlayerInput.OriginalProfiles["Redigit's Pick"].InputModes[InputMode.Keyboard].KeyStatus["ViewZoomIn"]);
				this.InputModes[InputMode.Keyboard].KeyStatus["ViewZoomOut"] = new List<string>();
				this.InputModes[InputMode.Keyboard].KeyStatus["ViewZoomOut"].AddRange(PlayerInput.OriginalProfiles["Redigit's Pick"].InputModes[InputMode.Keyboard].KeyStatus["ViewZoomOut"]);
			}
			if (num < 218)
			{
				this.InputModes[InputMode.Keyboard].KeyStatus["ToggleCreativeMenu"] = new List<string>();
				this.InputModes[InputMode.Keyboard].KeyStatus["ToggleCreativeMenu"].AddRange(PlayerInput.OriginalProfiles["Redigit's Pick"].InputModes[InputMode.Keyboard].KeyStatus["ToggleCreativeMenu"]);
			}
			if (num < 227)
			{
				List<string> list = this.InputModes[InputMode.KeyboardUI].KeyStatus["MouseLeft"];
				string text = "Mouse1";
				if (!list.Contains(text))
				{
					list.Add(text);
				}
			}
			if (num < 265)
			{
				foreach (string text2 in new string[] { "Loadout1", "Loadout2", "Loadout3", "ToggleCameraMode" })
				{
					this.InputModes[InputMode.Keyboard].KeyStatus[text2] = new List<string>(PlayerInput.OriginalProfiles["Redigit's Pick"].InputModes[InputMode.Keyboard].KeyStatus[text2]);
				}
			}
			if (dict.TryGetValue("Settings", out obj))
			{
				Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(((JObject)obj).ToString());
				if (dictionary.TryGetValue("Edittable", out obj))
				{
					this.AllowEditing = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - HotbarRadialHoldTime", out obj))
				{
					this.HotbarRadialHoldTimeRequired = (int)((long)obj);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickDeadzoneX", out obj))
				{
					this.LeftThumbstickDeadzoneX = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickDeadzoneY", out obj))
				{
					this.LeftThumbstickDeadzoneY = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickDeadzoneX", out obj))
				{
					this.RightThumbstickDeadzoneX = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickDeadzoneY", out obj))
				{
					this.RightThumbstickDeadzoneY = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickInvertX", out obj))
				{
					this.LeftThumbstickInvertX = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - LeftThumbstickInvertY", out obj))
				{
					this.LeftThumbstickInvertY = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickInvertX", out obj))
				{
					this.RightThumbstickInvertX = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - RightThumbstickInvertY", out obj))
				{
					this.RightThumbstickInvertY = (bool)obj;
				}
				if (dictionary.TryGetValue("Gamepad - TriggersDeadzone", out obj))
				{
					this.TriggersDeadzone = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - InterfaceDeadzoneX", out obj))
				{
					this.InterfaceDeadzoneX = (float)((double)obj);
				}
				if (dictionary.TryGetValue("Gamepad - InventoryMoveCD", out obj))
				{
					this.InventoryMoveCD = (int)((long)obj);
				}
			}
			return true;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x004D4FA4 File Offset: 0x004D31A4
		public Dictionary<string, object> Save()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			dictionary.Add("Last Launched Version", 319);
			dictionary2.Add("Edittable", this.AllowEditing);
			dictionary2.Add("Gamepad - HotbarRadialHoldTime", this.HotbarRadialHoldTimeRequired);
			dictionary2.Add("Gamepad - LeftThumbstickDeadzoneX", this.LeftThumbstickDeadzoneX);
			dictionary2.Add("Gamepad - LeftThumbstickDeadzoneY", this.LeftThumbstickDeadzoneY);
			dictionary2.Add("Gamepad - RightThumbstickDeadzoneX", this.RightThumbstickDeadzoneX);
			dictionary2.Add("Gamepad - RightThumbstickDeadzoneY", this.RightThumbstickDeadzoneY);
			dictionary2.Add("Gamepad - LeftThumbstickInvertX", this.LeftThumbstickInvertX);
			dictionary2.Add("Gamepad - LeftThumbstickInvertY", this.LeftThumbstickInvertY);
			dictionary2.Add("Gamepad - RightThumbstickInvertX", this.RightThumbstickInvertX);
			dictionary2.Add("Gamepad - RightThumbstickInvertY", this.RightThumbstickInvertY);
			dictionary2.Add("Gamepad - TriggersDeadzone", this.TriggersDeadzone);
			dictionary2.Add("Gamepad - InterfaceDeadzoneX", this.InterfaceDeadzoneX);
			dictionary2.Add("Gamepad - InventoryMoveCD", this.InventoryMoveCD);
			dictionary.Add("Settings", dictionary2);
			dictionary.Add("Mouse And Keyboard", this.InputModes[InputMode.Keyboard].WritePreferences());
			dictionary.Add("Gamepad", this.InputModes[InputMode.XBoxGamepad].WritePreferences());
			dictionary.Add("Mouse And Keyboard UI", this.InputModes[InputMode.KeyboardUI].WritePreferences());
			dictionary.Add("Gamepad UI", this.InputModes[InputMode.XBoxGamepadUI].WritePreferences());
			return dictionary;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x004D516C File Offset: 0x004D336C
		public void ConditionalAddProfile(Dictionary<string, object> dicttouse, string k, InputMode nm, Dictionary<string, List<string>> dict)
		{
			if (PlayerInput.OriginalProfiles.ContainsKey(this.Name))
			{
				foreach (KeyValuePair<string, List<string>> keyValuePair in PlayerInput.OriginalProfiles[this.Name].InputModes[nm].WritePreferences())
				{
					bool flag = true;
					List<string> list;
					if (dict.TryGetValue(keyValuePair.Key, out list))
					{
						if (list.Count != keyValuePair.Value.Count)
						{
							flag = false;
						}
						if (!flag)
						{
							for (int i = 0; i < list.Count; i++)
							{
								if (list[i] != keyValuePair.Value[i])
								{
									flag = false;
									break;
								}
							}
						}
					}
					else
					{
						flag = false;
					}
					if (flag)
					{
						dict.Remove(keyValuePair.Key);
					}
				}
			}
			if (dict.Count > 0)
			{
				dicttouse.Add(k, dict);
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x004D527C File Offset: 0x004D347C
		public void ConditionalAdd(Dictionary<string, object> dicttouse, string a, object b, Func<PlayerInputProfile, bool> check)
		{
			if (PlayerInput.OriginalProfiles.ContainsKey(this.Name) && check(PlayerInput.OriginalProfiles[this.Name]))
			{
				return;
			}
			dicttouse.Add(a, b);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x004D52B4 File Offset: 0x004D34B4
		public void CopyGameplaySettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] array = new string[]
			{
				"MouseLeft", "MouseRight", "Up", "Down", "Left", "Right", "Jump", "Grapple", "SmartSelect", "SmartCursor",
				"QuickMount", "QuickHeal", "QuickMana", "QuickBuff", "Throw", "Inventory", "ViewZoomIn", "ViewZoomOut", "Loadout1", "Loadout2",
				"Loadout3", "NextLoadout", "PreviousLoadout", "ToggleCreativeMenu", "ToggleCameraMode", "ArmorSetAbility", "Dash"
			};
			this.CopyKeysFrom(profile, mode, array);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x004D53BC File Offset: 0x004D35BC
		public void CopyHotbarSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] array = new string[]
			{
				"HotbarMinus", "HotbarPlus", "Hotbar1", "Hotbar2", "Hotbar3", "Hotbar4", "Hotbar5", "Hotbar6", "Hotbar7", "Hotbar8",
				"Hotbar9", "Hotbar10"
			};
			this.CopyKeysFrom(profile, mode, array);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x004D5440 File Offset: 0x004D3640
		public void CopyMapSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] array = new string[] { "MapZoomIn", "MapZoomOut", "MapAlphaUp", "MapAlphaDown", "MapFull", "MapStyle" };
			this.CopyKeysFrom(profile, mode, array);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x004D5490 File Offset: 0x004D3690
		public void CopyGamepadSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			string[] array = new string[] { "RadialHotbar", "RadialQuickbar", "DpadSnap1", "DpadSnap2", "DpadSnap3", "DpadSnap4", "DpadRadial1", "DpadRadial2", "DpadRadial3", "DpadRadial4" };
			this.CopyKeysFrom(profile, InputMode.XBoxGamepad, array);
			this.CopyKeysFrom(profile, InputMode.XBoxGamepadUI, array);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x004D5508 File Offset: 0x004D3708
		public void CopyGamepadAdvancedSettingsFrom(PlayerInputProfile profile, InputMode mode)
		{
			this.TriggersDeadzone = profile.TriggersDeadzone;
			this.InterfaceDeadzoneX = profile.InterfaceDeadzoneX;
			this.LeftThumbstickDeadzoneX = profile.LeftThumbstickDeadzoneX;
			this.LeftThumbstickDeadzoneY = profile.LeftThumbstickDeadzoneY;
			this.RightThumbstickDeadzoneX = profile.RightThumbstickDeadzoneX;
			this.RightThumbstickDeadzoneY = profile.RightThumbstickDeadzoneY;
			this.LeftThumbstickInvertX = profile.LeftThumbstickInvertX;
			this.LeftThumbstickInvertY = profile.LeftThumbstickInvertY;
			this.RightThumbstickInvertX = profile.RightThumbstickInvertX;
			this.RightThumbstickInvertY = profile.RightThumbstickInvertY;
			this.InventoryMoveCD = profile.InventoryMoveCD;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x004D559C File Offset: 0x004D379C
		private void CopyKeysFrom(PlayerInputProfile profile, InputMode mode, string[] keysToCopy)
		{
			for (int i = 0; i < keysToCopy.Length; i++)
			{
				List<string> list;
				if (profile.InputModes[mode].KeyStatus.TryGetValue(keysToCopy[i], out list))
				{
					this.InputModes[mode].KeyStatus[keysToCopy[i]].Clear();
					this.InputModes[mode].KeyStatus[keysToCopy[i]].AddRange(list);
				}
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x004D5614 File Offset: 0x004D3814
		public bool UsingDpadHotbar()
		{
			return this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial1"].Contains(Buttons.DPadUp.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial2"].Contains(Buttons.DPadRight.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial3"].Contains(Buttons.DPadDown.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadRadial4"].Contains(Buttons.DPadLeft.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial1"].Contains(Buttons.DPadUp.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial2"].Contains(Buttons.DPadRight.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial3"].Contains(Buttons.DPadDown.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadRadial4"].Contains(Buttons.DPadLeft.ToString());
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x004D57B8 File Offset: 0x004D39B8
		public bool UsingDpadMovekeys()
		{
			return this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap1"].Contains(Buttons.DPadUp.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap2"].Contains(Buttons.DPadRight.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap3"].Contains(Buttons.DPadDown.ToString()) && this.InputModes[InputMode.XBoxGamepad].KeyStatus["DpadSnap4"].Contains(Buttons.DPadLeft.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap1"].Contains(Buttons.DPadUp.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap2"].Contains(Buttons.DPadRight.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap3"].Contains(Buttons.DPadDown.ToString()) && this.InputModes[InputMode.XBoxGamepadUI].KeyStatus["DpadSnap4"].Contains(Buttons.DPadLeft.ToString());
		}

		// Token: 0x04001127 RID: 4391
		public Dictionary<InputMode, KeyConfiguration> InputModes = new Dictionary<InputMode, KeyConfiguration>
		{
			{
				InputMode.Keyboard,
				new KeyConfiguration()
			},
			{
				InputMode.KeyboardUI,
				new KeyConfiguration()
			},
			{
				InputMode.XBoxGamepad,
				new KeyConfiguration()
			},
			{
				InputMode.XBoxGamepadUI,
				new KeyConfiguration()
			}
		};

		// Token: 0x04001128 RID: 4392
		public string Name = "";

		// Token: 0x04001129 RID: 4393
		public bool AllowEditing = true;

		// Token: 0x0400112A RID: 4394
		public int HotbarRadialHoldTimeRequired = 16;

		// Token: 0x0400112B RID: 4395
		public float TriggersDeadzone = 0.3f;

		// Token: 0x0400112C RID: 4396
		public float InterfaceDeadzoneX = 0.2f;

		// Token: 0x0400112D RID: 4397
		public float LeftThumbstickDeadzoneX = 0.25f;

		// Token: 0x0400112E RID: 4398
		public float LeftThumbstickDeadzoneY = 0.4f;

		// Token: 0x0400112F RID: 4399
		public float RightThumbstickDeadzoneX;

		// Token: 0x04001130 RID: 4400
		public float RightThumbstickDeadzoneY;

		// Token: 0x04001131 RID: 4401
		public bool LeftThumbstickInvertX;

		// Token: 0x04001132 RID: 4402
		public bool LeftThumbstickInvertY;

		// Token: 0x04001133 RID: 4403
		public bool RightThumbstickInvertX;

		// Token: 0x04001134 RID: 4404
		public bool RightThumbstickInvertY;

		// Token: 0x04001135 RID: 4405
		public int InventoryMoveCD = 6;
	}
}
