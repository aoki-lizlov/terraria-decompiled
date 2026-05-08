using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Chat;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameInput
{
	// Token: 0x02000095 RID: 149
	public class PlayerInput
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x0600168A RID: 5770 RVA: 0x004D69E0 File Offset: 0x004D4BE0
		// (remove) Token: 0x0600168B RID: 5771 RVA: 0x004D6A14 File Offset: 0x004D4C14
		public static event Action OnBindingChange
		{
			[CompilerGenerated]
			add
			{
				Action action = PlayerInput.OnBindingChange;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref PlayerInput.OnBindingChange, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = PlayerInput.OnBindingChange;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref PlayerInput.OnBindingChange, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600168C RID: 5772 RVA: 0x004D6A48 File Offset: 0x004D4C48
		// (remove) Token: 0x0600168D RID: 5773 RVA: 0x004D6A7C File Offset: 0x004D4C7C
		public static event Action OnActionableInput
		{
			[CompilerGenerated]
			add
			{
				Action action = PlayerInput.OnActionableInput;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref PlayerInput.OnActionableInput, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = PlayerInput.OnActionableInput;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref PlayerInput.OnActionableInput, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x004D6AAF File Offset: 0x004D4CAF
		public static void ListenFor(string triggerName, InputMode inputmode)
		{
			PlayerInput._listeningTrigger = triggerName;
			PlayerInput._listeningInputMode = inputmode;
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x004D6ABD File Offset: 0x004D4CBD
		public static string ListeningTrigger
		{
			get
			{
				return PlayerInput._listeningTrigger;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x004D6AC4 File Offset: 0x004D4CC4
		public static bool CurrentlyRebinding
		{
			get
			{
				return PlayerInput._listeningTrigger != null;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x004D6AD0 File Offset: 0x004D4CD0
		public static bool InvisibleGamepadInMenus
		{
			get
			{
				return !PlayerInput.ControllerHousingCursorActive && (((Main.gameMenu || Main.ingameOptionsWindow || Main.playerInventory || Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1 || Main.InGameUI.CurrentState != null) && !PlayerInput._InBuildingMode && Main.InvisibleCursorForGamepad) || (PlayerInput.CursorIsBusy && !PlayerInput._InBuildingMode));
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x004D6B57 File Offset: 0x004D4D57
		public static PlayerInputProfile CurrentProfile
		{
			get
			{
				return PlayerInput._currentProfile;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x004D6B5E File Offset: 0x004D4D5E
		public static KeyConfiguration ProfileGamepadUI
		{
			get
			{
				return PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI];
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x004D6B70 File Offset: 0x004D4D70
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x004D6B78 File Offset: 0x004D4D78
		public static InputMode CurrentInputMode
		{
			get
			{
				return PlayerInput._inputMode;
			}
			set
			{
				bool usingGamepad = PlayerInput.UsingGamepad;
				PlayerInput._inputMode = value;
				if (PlayerInput.UsingGamepad != usingGamepad && PlayerInput.OnBindingChange != null)
				{
					PlayerInput.OnBindingChange();
				}
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x004D6BAA File Offset: 0x004D4DAA
		public static bool UsingGamepad
		{
			get
			{
				return PlayerInput.CurrentInputMode == InputMode.XBoxGamepad || PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x004D6BBE File Offset: 0x004D4DBE
		public static bool UsingGamepadUI
		{
			get
			{
				return PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x004D6BC8 File Offset: 0x004D4DC8
		public static bool IgnoreMouseInterface
		{
			get
			{
				if (Main.LocalPlayer.UsingOrReusingItem && !PlayerInput.UsingGamepad && !Main.gamePaused)
				{
					return true;
				}
				bool flag = PlayerInput.UsingGamepad && !UILinkPointNavigator.Available;
				return (!flag || !PlayerInput.SteamDeckIsUsed || PlayerInput.SettingsForUI.CurrentCursorMode != CursorMode.Mouse || Main.mouseRight) && flag;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x004D6C1F File Offset: 0x004D4E1F
		public static bool SteamDeckIsUsed
		{
			get
			{
				return PlayerInput.UseSteamDeckIfPossible;
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x004D6C28 File Offset: 0x004D4E28
		private static bool InvalidateKeyboardSwap()
		{
			if (PlayerInput._invalidatorCheck.Length == 0)
			{
				return false;
			}
			string text = "";
			List<Keys> pressedKeys = PlayerInput.GetPressedKeys();
			for (int i = 0; i < pressedKeys.Count; i++)
			{
				text = text + pressedKeys[i] + ", ";
			}
			if (text == PlayerInput._invalidatorCheck)
			{
				return true;
			}
			PlayerInput._invalidatorCheck = "";
			return false;
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x004D6C94 File Offset: 0x004D4E94
		public static void ResetInputsOnActiveStateChange()
		{
			bool allowInputProcessing = FocusHelper.AllowInputProcessing;
			if (PlayerInput._lastActivityState != allowInputProcessing)
			{
				PlayerInput.MouseInfo = default(MouseState);
				PlayerInput.MouseInfoOld = default(MouseState);
				Main.keyState = Keyboard.GetState();
				Main.inputText = Keyboard.GetState();
				Main.oldInputText = Keyboard.GetState();
				Main.keyCount = 0;
				PlayerInput.Triggers.Reset();
				PlayerInput.Triggers.Reset();
				string text = "";
				List<Keys> pressedKeys = PlayerInput.GetPressedKeys();
				for (int i = 0; i < pressedKeys.Count; i++)
				{
					text = text + pressedKeys[i] + ", ";
				}
				PlayerInput._invalidatorCheck = text;
			}
			PlayerInput._lastActivityState = allowInputProcessing;
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x004D6D44 File Offset: 0x004D4F44
		public static List<Keys> GetPressedKeys()
		{
			List<Keys> list = Main.keyState.GetPressedKeys().ToList<Keys>();
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i] == Keys.None || list[i] == Keys.Kanji)
				{
					list.RemoveAt(i);
				}
			}
			return list;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x004D6D90 File Offset: 0x004D4F90
		public static void TryEnteringFastUseModeForInventorySlot(int inventorySlot)
		{
			PlayerInput._fastUseMemory.TryStartForItemSlot(Main.LocalPlayer, inventorySlot);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x004D6DA3 File Offset: 0x004D4FA3
		public static void TryEnteringFastUseModeForMouseItem()
		{
			PlayerInput._fastUseMemory.TryStartForMouse(Main.LocalPlayer);
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x004D6DB5 File Offset: 0x004D4FB5
		public static bool ShouldFastUseItem
		{
			get
			{
				return PlayerInput._fastUseMemory.CanFastUse();
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x004D6DC1 File Offset: 0x004D4FC1
		public static void TryEndingFastUse()
		{
			PlayerInput._fastUseMemory.EndFastUse();
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x004D6DCD File Offset: 0x004D4FCD
		public static bool InBuildingMode
		{
			get
			{
				return PlayerInput._InBuildingMode;
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x004D6DD4 File Offset: 0x004D4FD4
		public static void EnterBuildingMode()
		{
			if (Main.LocalPlayer.UsingOrReusingItem)
			{
				return;
			}
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			PlayerInput._InBuildingMode = true;
			PlayerInput._UIPointForBuildingMode = UILinkPointNavigator.CurrentPoint;
			if (Main.mouseItem.IsAir)
			{
				int uipointForBuildingMode = PlayerInput._UIPointForBuildingMode;
				if (uipointForBuildingMode < 50 && uipointForBuildingMode >= 0 && Main.player[Main.myPlayer].inventory[uipointForBuildingMode].stack > 0)
				{
					Utils.Swap<Item>(ref Main.mouseItem, ref Main.player[Main.myPlayer].inventory[uipointForBuildingMode]);
				}
			}
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x004D6E68 File Offset: 0x004D5068
		public static void ExitBuildingMode(bool quiet = false)
		{
			if (!quiet)
			{
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			}
			PlayerInput._InBuildingMode = false;
			UILinkPointNavigator.ChangePoint(PlayerInput._UIPointForBuildingMode);
			if (Main.mouseItem.stack > 0 && Main.player[Main.myPlayer].itemAnimation == 0)
			{
				int uipointForBuildingMode = PlayerInput._UIPointForBuildingMode;
				if (uipointForBuildingMode < 50 && uipointForBuildingMode >= 0 && Main.player[Main.myPlayer].inventory[uipointForBuildingMode].stack <= 0)
				{
					Utils.Swap<Item>(ref Main.mouseItem, ref Main.player[Main.myPlayer].inventory[uipointForBuildingMode]);
				}
			}
			PlayerInput._UIPointForBuildingMode = -1;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x004D6F0C File Offset: 0x004D510C
		public static void VerifyBuildingMode()
		{
			if (PlayerInput._InBuildingMode)
			{
				Player player = Main.player[Main.myPlayer];
				bool flag = false;
				if (Main.mouseItem.stack <= 0)
				{
					flag = true;
				}
				if (player.dead)
				{
					flag = true;
				}
				if (flag)
				{
					PlayerInput.ExitBuildingMode(false);
				}
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x004D6F4E File Offset: 0x004D514E
		public static int RealScreenWidth
		{
			get
			{
				return PlayerInput._originalScreenWidth;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x004D6F55 File Offset: 0x004D5155
		public static int RealScreenHeight
		{
			get
			{
				return PlayerInput._originalScreenHeight;
			}
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x004D6F5C File Offset: 0x004D515C
		public static void SetSelectedProfile(string name)
		{
			if (PlayerInput.Profiles.ContainsKey(name))
			{
				PlayerInput._selectedProfile = name;
				PlayerInput._currentProfile = PlayerInput.Profiles[PlayerInput._selectedProfile];
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x004D6F88 File Offset: 0x004D5188
		public static void Initialize()
		{
			Main.InputProfiles.OnProcessText += PlayerInput.PrettyPrintProfiles;
			Player.Hooks.OnEnterWorld += PlayerInput.Hook_OnEnterWorld;
			PlayerInputProfile playerInputProfile = new PlayerInputProfile("Redigit's Pick");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Yoraiz0r's Pick");
			playerInputProfile.Initialize(PresetProfiles.Yoraiz0r);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Playstation)");
			playerInputProfile.Initialize(PresetProfiles.ConsolePS);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Xbox)");
			playerInputProfile.Initialize(PresetProfiles.ConsoleXBox);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Custom");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			PlayerInput.Profiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Redigit's Pick");
			playerInputProfile.Initialize(PresetProfiles.Redigit);
			PlayerInput.OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Yoraiz0r's Pick");
			playerInputProfile.Initialize(PresetProfiles.Yoraiz0r);
			PlayerInput.OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Playstation)");
			playerInputProfile.Initialize(PresetProfiles.ConsolePS);
			PlayerInput.OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			playerInputProfile = new PlayerInputProfile("Console (Xbox)");
			playerInputProfile.Initialize(PresetProfiles.ConsoleXBox);
			PlayerInput.OriginalProfiles.Add(playerInputProfile.Name, playerInputProfile);
			PlayerInput.SetSelectedProfile("Custom");
			PlayerInput.Triggers.Initialize();
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x004D710B File Offset: 0x004D530B
		public static void Hook_OnEnterWorld(Player player)
		{
			if (player.whoAmI == Main.myPlayer)
			{
				Main.SmartCursorWanted_GamePad = true;
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x004D7120 File Offset: 0x004D5320
		public static bool Save()
		{
			Main.InputProfiles.Clear();
			Main.InputProfiles.Put("Selected Profile", PlayerInput._selectedProfile);
			foreach (KeyValuePair<string, PlayerInputProfile> keyValuePair in PlayerInput.Profiles)
			{
				Main.InputProfiles.Put(keyValuePair.Value.Name, keyValuePair.Value.Save());
			}
			return Main.InputProfiles.Save(true);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x004D71B8 File Offset: 0x004D53B8
		public static void Load()
		{
			Main.InputProfiles.Load();
			Dictionary<string, PlayerInputProfile> dictionary = new Dictionary<string, PlayerInputProfile>();
			string text = null;
			Main.InputProfiles.Get<string>("Selected Profile", ref text);
			List<string> allKeys = Main.InputProfiles.GetAllKeys();
			for (int i = 0; i < allKeys.Count; i++)
			{
				string text2 = allKeys[i];
				if (!(text2 == "Selected Profile") && !string.IsNullOrEmpty(text2))
				{
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					Main.InputProfiles.Get<Dictionary<string, object>>(text2, ref dictionary2);
					if (dictionary2.Count > 0)
					{
						PlayerInputProfile playerInputProfile = new PlayerInputProfile(text2);
						playerInputProfile.Initialize(PresetProfiles.None);
						if (playerInputProfile.Load(dictionary2))
						{
							dictionary.Add(text2, playerInputProfile);
						}
					}
				}
			}
			if (dictionary.Count > 0)
			{
				PlayerInput.Profiles = dictionary;
				if (!string.IsNullOrEmpty(text) && PlayerInput.Profiles.ContainsKey(text))
				{
					PlayerInput.SetSelectedProfile(text);
					return;
				}
				PlayerInput.SetSelectedProfile(PlayerInput.Profiles.Keys.First<string>());
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x004D72AC File Offset: 0x004D54AC
		public static void ManageVersion_1_3()
		{
			PlayerInputProfile playerInputProfile = PlayerInput.Profiles["Custom"];
			string[,] array = new string[20, 2];
			array[0, 0] = "KeyUp";
			array[0, 1] = "Up";
			array[1, 0] = "KeyDown";
			array[1, 1] = "Down";
			array[2, 0] = "KeyLeft";
			array[2, 1] = "Left";
			array[3, 0] = "KeyRight";
			array[3, 1] = "Right";
			array[4, 0] = "KeyJump";
			array[4, 1] = "Jump";
			array[5, 0] = "KeyThrowItem";
			array[5, 1] = "Throw";
			array[6, 0] = "KeyInventory";
			array[6, 1] = "Inventory";
			array[7, 0] = "KeyQuickHeal";
			array[7, 1] = "QuickHeal";
			array[8, 0] = "KeyQuickMana";
			array[8, 1] = "QuickMana";
			array[9, 0] = "KeyQuickBuff";
			array[9, 1] = "QuickBuff";
			array[10, 0] = "KeyUseHook";
			array[10, 1] = "Grapple";
			array[11, 0] = "KeyAutoSelect";
			array[11, 1] = "SmartSelect";
			array[12, 0] = "KeySmartCursor";
			array[12, 1] = "SmartCursor";
			array[13, 0] = "KeyMount";
			array[13, 1] = "QuickMount";
			array[14, 0] = "KeyMapStyle";
			array[14, 1] = "MapStyle";
			array[15, 0] = "KeyFullscreenMap";
			array[15, 1] = "MapFull";
			array[16, 0] = "KeyMapZoomIn";
			array[16, 1] = "MapZoomIn";
			array[17, 0] = "KeyMapZoomOut";
			array[17, 1] = "MapZoomOut";
			array[18, 0] = "KeyMapAlphaUp";
			array[18, 1] = "MapAlphaUp";
			array[19, 0] = "KeyMapAlphaDown";
			array[19, 1] = "MapAlphaDown";
			string[,] array2 = array;
			for (int i = 0; i < array2.GetLength(0); i++)
			{
				string text = null;
				Main.Configuration.Get<string>(array2[i, 0], ref text);
				if (text != null)
				{
					playerInputProfile.InputModes[InputMode.Keyboard].KeyStatus[array2[i, 1]] = new List<string> { text };
					playerInputProfile.InputModes[InputMode.KeyboardUI].KeyStatus[array2[i, 1]] = new List<string> { text };
				}
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x004D756F File Offset: 0x004D576F
		public static bool CursorIsBusy
		{
			get
			{
				return ItemSlot.CircularRadialOpacity > 0f || ItemSlot.QuicksRadialOpacity > 0f;
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x004D758C File Offset: 0x004D578C
		public static void LockGamepadButtons(string TriggerName)
		{
			List<string> list = null;
			KeyConfiguration keyConfiguration = null;
			if (PlayerInput.CurrentProfile.InputModes.TryGetValue(PlayerInput.CurrentInputMode, out keyConfiguration) && keyConfiguration.KeyStatus.TryGetValue(TriggerName, out list))
			{
				PlayerInput._buttonsLocked.AddRange(list);
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x004D75D0 File Offset: 0x004D57D0
		public static bool IsGamepadButtonLockedFromUse(string keyName)
		{
			return PlayerInput._buttonsLocked.Contains(keyName);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x004D75E0 File Offset: 0x004D57E0
		public static void UpdateInput()
		{
			PlayerInput.SettingsForUI.UpdateCounters();
			PlayerInput.Triggers.Reset();
			PlayerInput.ScrollWheelValueOld = PlayerInput.ScrollWheelValue;
			PlayerInput.ScrollWheelValue = 0;
			PlayerInput.GamepadThumbstickLeft = Vector2.Zero;
			PlayerInput.GamepadThumbstickRight = Vector2.Zero;
			PlayerInput.GrappleAndInteractAreShared = (PlayerInput.UsingGamepad || PlayerInput.SteamDeckIsUsed) && PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].DoGrappleAndInteractShareTheSameKey;
			if (PlayerInput.InBuildingMode && !PlayerInput.UsingGamepad)
			{
				PlayerInput.ExitBuildingMode(false);
			}
			if (PlayerInput._canReleaseRebindingLock && PlayerInput.NavigatorRebindingLock > 0)
			{
				PlayerInput.NavigatorRebindingLock--;
				PlayerInput.Triggers.Current.UsedMovementKey = false;
				if (PlayerInput.NavigatorRebindingLock == 0 && PlayerInput._memoOfLastPoint != -1)
				{
					UIManageControls.ForceMoveTo = PlayerInput._memoOfLastPoint;
					PlayerInput._memoOfLastPoint = -1;
				}
			}
			PlayerInput._canReleaseRebindingLock = true;
			PlayerInput.VerifyBuildingMode();
			bool flag = false;
			PlayerInput.MouseInput();
			bool flag2 = flag | PlayerInput.KeyboardInput() | PlayerInput.GamePadInput();
			PlayerInput.Triggers.Update();
			PlayerInput.PostInput();
			PlayerInput.ScrollWheelDelta = PlayerInput.ScrollWheelValue - PlayerInput.ScrollWheelValueOld;
			PlayerInput.ScrollWheelDeltaForUI = PlayerInput.ScrollWheelDelta;
			PlayerInput.WritingText = false;
			PlayerInput.UpdateMainMouse();
			Main.mouseLeft = PlayerInput.Triggers.Current.MouseLeft;
			Main.mouseRight = PlayerInput.Triggers.Current.MouseRight;
			PlayerInput.CacheZoomableValues();
			if (flag2 && PlayerInput.OnActionableInput != null)
			{
				PlayerInput.OnActionableInput();
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x004D773A File Offset: 0x004D593A
		public static void UpdateMainMouse()
		{
			Main.lastMouseX = Main.mouseX;
			Main.lastMouseY = Main.mouseY;
			Main.mouseX = PlayerInput.MouseX;
			Main.mouseY = PlayerInput.MouseY;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x004D7764 File Offset: 0x004D5964
		public static void CacheZoomableValues()
		{
			PlayerInput.CacheOriginalInput();
			PlayerInput.CacheOriginalScreenDimensions();
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x004D7770 File Offset: 0x004D5970
		public static void CacheMousePositionForZoom()
		{
			float num = 1f;
			PlayerInput._originalMouseX = (int)((float)Main.mouseX * num);
			PlayerInput._originalMouseY = (int)((float)Main.mouseY * num);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x004D779F File Offset: 0x004D599F
		private static void CacheOriginalInput()
		{
			PlayerInput._originalMouseX = Main.mouseX;
			PlayerInput._originalMouseY = Main.mouseY;
			PlayerInput._originalLastMouseX = Main.lastMouseX;
			PlayerInput._originalLastMouseY = Main.lastMouseY;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x004D77C9 File Offset: 0x004D59C9
		public static void CacheOriginalScreenDimensions()
		{
			PlayerInput._originalScreenWidth = Main.screenWidth;
			PlayerInput._originalScreenHeight = Main.screenHeight;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x004D77DF File Offset: 0x004D59DF
		public static Vector2 OriginalScreenSize
		{
			get
			{
				return new Vector2((float)PlayerInput._originalScreenWidth, (float)PlayerInput._originalScreenHeight);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x004D77F2 File Offset: 0x004D59F2
		public static bool ControllerHousingCursorActive
		{
			get
			{
				return PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI && UILinkPointNavigator.CurrentPoint >= 600 && UILinkPointNavigator.CurrentPoint < 650;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060016B8 RID: 5816 RVA: 0x004D7816 File Offset: 0x004D5A16
		// (set) Token: 0x060016B9 RID: 5817 RVA: 0x004D782C File Offset: 0x004D5A2C
		public static Vector2 HousingWorldPosition
		{
			get
			{
				return Main.LocalPlayer.Center + PlayerInput.HousingMouseOffset;
			}
			set
			{
				PlayerInput.HousingMouseOffset = value - Main.LocalPlayer.Center;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060016BA RID: 5818 RVA: 0x004D7843 File Offset: 0x004D5A43
		public static Vector2 HousingScreenPosition
		{
			get
			{
				return PlayerInput.HousingWorldPosition.ToScreenPosition();
			}
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x004D7850 File Offset: 0x004D5A50
		public static void UpdateHousingCursor()
		{
			if (PlayerInput.ControllerHousingCursorActive)
			{
				Vector2 gamepadThumbstickRight = PlayerInput.GamepadThumbstickRight;
				Vector2 vector = gamepadThumbstickRight;
				if (vector != Vector2.Zero)
				{
					vector.Normalize();
				}
				Vector2 center = Main.LocalPlayer.Center;
				float m = Main.GameViewMatrix.ZoomMatrix.M11;
				if (gamepadThumbstickRight != Vector2.Zero)
				{
					Vector2 vector2 = new Vector2(10f);
					Vector2 vector3 = gamepadThumbstickRight * vector2 * m;
					PlayerInput.HousingMouseOffset += vector3;
				}
				Vector2 vector4 = new Vector2(20f, 20f);
				Vector2 vector5 = new Vector2((float)Main.screenWidth, (float)Main.screenHeight);
				PlayerInput.HousingWorldPosition = Vector2.Clamp(PlayerInput.HousingWorldPosition, vector4.ScreenToWorldPosition(), (vector5 - vector4).ScreenToWorldPosition());
				return;
			}
			PlayerInput.HousingMouseOffset = Vector2.Zero;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x004D792C File Offset: 0x004D5B2C
		private static bool GamePadInput()
		{
			bool flag = false;
			PlayerInput.ScrollWheelValue += PlayerInput.GamepadScrollValue;
			GamePadState gamePadState = default(GamePadState);
			bool flag2 = false;
			for (int i = 0; i < 4; i++)
			{
				GamePadState state = GamePad.GetState((PlayerIndex)i);
				if (state.IsConnected)
				{
					flag2 = true;
					gamePadState = state;
					break;
				}
			}
			if (Main.SettingBlockGamepadsEntirely)
			{
				return false;
			}
			if (!flag2)
			{
				return false;
			}
			if (!FocusHelper.AllowInputProcessingForGamepad)
			{
				return false;
			}
			Player player = Main.player[Main.myPlayer];
			bool flag3 = UILinkPointNavigator.Available && !PlayerInput.InBuildingMode;
			InputMode inputMode = InputMode.XBoxGamepad;
			if (Main.gameMenu || flag3 || player.talkNPC != -1 || player.sign != -1 || IngameFancyUI.CanCover())
			{
				inputMode = InputMode.XBoxGamepadUI;
			}
			if (!Main.gameMenu && PlayerInput.InBuildingMode)
			{
				inputMode = InputMode.XBoxGamepad;
			}
			if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepad && inputMode == InputMode.XBoxGamepadUI)
			{
				flag = true;
			}
			if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI && inputMode == InputMode.XBoxGamepad)
			{
				flag = true;
			}
			if (flag)
			{
				PlayerInput.CurrentInputMode = inputMode;
			}
			KeyConfiguration keyConfiguration = PlayerInput.CurrentProfile.InputModes[inputMode];
			int num = 2145386496;
			for (int j = 0; j < PlayerInput.ButtonsGamepad.Length; j++)
			{
				if ((num & (int)PlayerInput.ButtonsGamepad[j]) <= 0)
				{
					string text = PlayerInput.ButtonsGamepad[j].ToString();
					bool flag4 = PlayerInput._buttonsLocked.Contains(text);
					if (gamePadState.IsButtonDown(PlayerInput.ButtonsGamepad[j]))
					{
						if (!flag4)
						{
							if (PlayerInput.CheckRebindingProcessGamepad(text))
							{
								return false;
							}
							keyConfiguration.Processkey(PlayerInput.Triggers.Current, text, inputMode);
							flag = true;
						}
					}
					else
					{
						PlayerInput._buttonsLocked.Remove(text);
					}
				}
			}
			PlayerInput.GamepadThumbstickLeft = gamePadState.ThumbSticks.Left * new Vector2(1f, -1f) * new Vector2((float)(PlayerInput.CurrentProfile.LeftThumbstickInvertX.ToDirectionInt() * -1), (float)(PlayerInput.CurrentProfile.LeftThumbstickInvertY.ToDirectionInt() * -1));
			PlayerInput.GamepadThumbstickRight = gamePadState.ThumbSticks.Right * new Vector2(1f, -1f) * new Vector2((float)(PlayerInput.CurrentProfile.RightThumbstickInvertX.ToDirectionInt() * -1), (float)(PlayerInput.CurrentProfile.RightThumbstickInvertY.ToDirectionInt() * -1));
			Vector2 gamepadThumbstickRight = PlayerInput.GamepadThumbstickRight;
			Vector2 gamepadThumbstickLeft = PlayerInput.GamepadThumbstickLeft;
			Vector2 vector = gamepadThumbstickRight;
			if (vector != Vector2.Zero)
			{
				vector.Normalize();
			}
			Vector2 vector2 = gamepadThumbstickLeft;
			if (vector2 != Vector2.Zero)
			{
				vector2.Normalize();
			}
			float num2 = 0.6f;
			float triggersDeadzone = PlayerInput.CurrentProfile.TriggersDeadzone;
			if (inputMode == InputMode.XBoxGamepadUI)
			{
				num2 = 0.4f;
				if (PlayerInput.GamepadAllowScrolling)
				{
					PlayerInput.GamepadScrollValue -= (int)(gamepadThumbstickRight.Y * 16f);
				}
				PlayerInput.GamepadAllowScrolling = false;
			}
			if (Vector2.Dot(-Vector2.UnitX, vector2) >= num2 && gamepadThumbstickLeft.X < -PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftThumbstickLeft.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftThumbstickLeft.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitX, vector2) >= num2 && gamepadThumbstickLeft.X > PlayerInput.CurrentProfile.LeftThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftThumbstickRight.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftThumbstickRight.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitY, vector2) >= num2 && gamepadThumbstickLeft.Y < -PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftThumbstickUp.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftThumbstickUp.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitY, vector2) >= num2 && gamepadThumbstickLeft.Y > PlayerInput.CurrentProfile.LeftThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftThumbstickDown.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftThumbstickDown.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitX, vector) >= num2 && gamepadThumbstickRight.X < -PlayerInput.CurrentProfile.RightThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.RightThumbstickLeft.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.RightThumbstickLeft.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitX, vector) >= num2 && gamepadThumbstickRight.X > PlayerInput.CurrentProfile.RightThumbstickDeadzoneX)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.RightThumbstickRight.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.RightThumbstickRight.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(-Vector2.UnitY, vector) >= num2 && gamepadThumbstickRight.Y < -PlayerInput.CurrentProfile.RightThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.RightThumbstickUp.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.RightThumbstickUp.ToString(), inputMode);
				flag = true;
			}
			if (Vector2.Dot(Vector2.UnitY, vector) >= num2 && gamepadThumbstickRight.Y > PlayerInput.CurrentProfile.RightThumbstickDeadzoneY)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.RightThumbstickDown.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.RightThumbstickDown.ToString(), inputMode);
				flag = true;
			}
			if (gamePadState.Triggers.Left > triggersDeadzone)
			{
				if (PlayerInput.CheckRebindingProcessGamepad(Buttons.LeftTrigger.ToString()))
				{
					return false;
				}
				keyConfiguration.Processkey(PlayerInput.Triggers.Current, Buttons.LeftTrigger.ToString(), inputMode);
				flag = true;
			}
			if (gamePadState.Triggers.Right > triggersDeadzone)
			{
				string text2 = Buttons.RightTrigger.ToString();
				if (PlayerInput.CheckRebindingProcessGamepad(text2))
				{
					return false;
				}
				if (inputMode == InputMode.XBoxGamepadUI && PlayerInput.SteamDeckIsUsed && PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Mouse)
				{
					PlayerInput.Triggers.Current.MouseLeft = true;
				}
				else
				{
					keyConfiguration.Processkey(PlayerInput.Triggers.Current, text2, inputMode);
					flag = true;
				}
			}
			bool flag5 = ItemID.Sets.GamepadWholeScreenUseRange[player.inventory[player.selectedItem].type] || player.scope;
			Item item = player.inventory[player.selectedItem];
			bool flag6 = false;
			bool flag7 = !Main.gameMenu && !flag3 && Main.SmartCursorWanted;
			bool flag8 = player.rulerGrid && player.builderAccStatus[1] == 0;
			bool flag9 = player.rulerLine && player.builderAccStatus[0] == 0;
			bool flag10 = flag8 || flag9;
			if (!flag7 && flag10)
			{
				flag5 = true;
			}
			int num3 = item.tileBoost + ItemID.Sets.GamepadExtraRange[item.type];
			if (player.yoyoString && ItemID.Sets.Yoyo[item.type])
			{
				num3 += 5;
			}
			else if (item.createTile < 0 && item.createWall <= 0 && item.shoot > 0)
			{
				num3 += 10;
			}
			else if (player.controlTorch)
			{
				num3++;
			}
			if (item.createWall > 0 || item.createTile > 0 || item.tileWand > 0)
			{
				num3 += player.blockRange;
			}
			if (flag5)
			{
				num3 += 30;
			}
			if (player.mount.Active && player.mount.Type == 8)
			{
				num3 = 10;
			}
			if (!PlayerInput.CursorIsBusy)
			{
				bool flag11 = Main.mapFullscreen || (!Main.gameMenu && !flag3);
				int num4 = Main.screenWidth / 2;
				int num5 = Main.screenHeight / 2;
				if (!Main.mapFullscreen && flag11 && !flag5)
				{
					Point point = Main.ReverseGravitySupport(player.Center - Main.screenPosition, 0f).ToPoint();
					num4 = point.X;
					num5 = point.Y;
				}
				if (player.velocity == Vector2.Zero && gamepadThumbstickLeft == Vector2.Zero && gamepadThumbstickRight == Vector2.Zero && flag7)
				{
					num4 += player.direction * 10;
				}
				float m = Main.GameViewMatrix.ZoomMatrix.M11;
				PlayerInput.smartSelectPointer.UpdateSize(new Vector2((float)(Player.tileRangeX * 16 + num3 * 16), (float)(Player.tileRangeY * 16 + num3 * 16)) * m);
				if (flag5)
				{
					PlayerInput.smartSelectPointer.UpdateSize(new Vector2((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2)));
				}
				PlayerInput.smartSelectPointer.UpdateCenter(new Vector2((float)num4, (float)num5));
				if (gamepadThumbstickRight != Vector2.Zero && flag11)
				{
					Vector2 vector3 = new Vector2(8f);
					if (!Main.gameMenu && Main.mapFullscreen)
					{
						vector3 = new Vector2(16f);
					}
					if (flag7)
					{
						vector3 = new Vector2((float)((Player.tileRangeX + num3) * 16), (float)((Player.tileRangeY + num3) * 16));
						if (flag5)
						{
							vector3 = new Vector2((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2));
						}
					}
					else if (!Main.mapFullscreen)
					{
						if (player.inventory[player.selectedItem].mech)
						{
							vector3 += Vector2.Zero;
						}
						else
						{
							vector3 += new Vector2((float)num3) / 4f;
						}
					}
					float m2 = Main.GameViewMatrix.ZoomMatrix.M11;
					Vector2 vector4 = gamepadThumbstickRight * vector3 * m2;
					int num6 = PlayerInput.MouseX - num4;
					int num7 = PlayerInput.MouseY - num5;
					if (flag7)
					{
						num6 = 0;
						num7 = 0;
					}
					num6 += (int)vector4.X;
					num7 += (int)vector4.Y;
					PlayerInput.MouseX = num6 + num4;
					PlayerInput.MouseY = num7 + num5;
					flag = true;
					flag6 = true;
					PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
				}
				bool allowSecondaryGamepadAim = PlayerInput.SettingsForUI.AllowSecondaryGamepadAim;
				if (gamepadThumbstickLeft != Vector2.Zero && flag11)
				{
					float num8 = 8f;
					if (!Main.gameMenu && Main.mapFullscreen)
					{
						num8 = 3f;
					}
					if (Main.mapFullscreen)
					{
						Vector2 vector5 = gamepadThumbstickLeft * num8;
						Main.mapFullscreenPos += vector5 * num8 * (1f / Main.mapFullscreenScale);
					}
					else if (!flag6 && Main.SmartCursorWanted && allowSecondaryGamepadAim)
					{
						float m3 = Main.GameViewMatrix.ZoomMatrix.M11;
						Vector2 vector6 = gamepadThumbstickLeft * new Vector2((float)((Player.tileRangeX + num3) * 16), (float)((Player.tileRangeY + num3) * 16)) * m3;
						if (flag5)
						{
							vector6 = new Vector2((float)(Math.Max(Main.screenWidth, Main.screenHeight) / 2)) * gamepadThumbstickLeft;
						}
						int num9 = (int)vector6.X;
						int num10 = (int)vector6.Y;
						PlayerInput.MouseX = num9 + num4;
						PlayerInput.MouseY = num10 + num5;
					}
					flag = true;
				}
				if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepad)
				{
					PlayerInput.HandleDpadSnap();
					if (PlayerInput.SettingsForUI.AllowSecondaryGamepadAim)
					{
						int num11 = PlayerInput.MouseX - num4;
						int num12 = PlayerInput.MouseY - num5;
						if (!Main.gameMenu && !flag3)
						{
							if (flag5 && !Main.mapFullscreen)
							{
								float num13 = 1f;
								int num14 = Main.screenWidth / 2;
								int num15 = Main.screenHeight / 2;
								num11 = (int)Utils.Clamp<float>((float)num11, (float)(-(float)num14) * num13, (float)num14 * num13);
								num12 = (int)Utils.Clamp<float>((float)num12, (float)(-(float)num15) * num13, (float)num15 * num13);
							}
							else
							{
								float num16 = 0f;
								if (player.HeldItem.createTile >= 0 || player.HeldItem.createWall > 0 || player.HeldItem.tileWand >= 0)
								{
									num16 = 0.5f;
								}
								float m4 = Main.GameViewMatrix.ZoomMatrix.M11;
								float num17 = -((float)(Player.tileRangeY + num3) - num16) * 16f * m4;
								float num18 = ((float)(Player.tileRangeY + num3) - num16) * 16f * m4;
								num17 -= (float)(player.height / 16 / 2 * 16);
								num11 = (int)Utils.Clamp<float>((float)num11, -((float)(Player.tileRangeX + num3) - num16) * 16f * m4, ((float)(Player.tileRangeX + num3) - num16) * 16f * m4);
								num12 = (int)Utils.Clamp<float>((float)num12, num17, num18);
							}
							if (flag7 && (!flag || flag5))
							{
								float num19 = 0.81f;
								if (flag5)
								{
									num19 = 0.95f;
								}
								num11 = (int)((float)num11 * num19);
								num12 = (int)((float)num12 * num19);
							}
						}
						else
						{
							num11 = Utils.Clamp<int>(num11, -num4 + 10, num4 - 10);
							num12 = Utils.Clamp<int>(num12, -num5 + 10, num5 - 10);
						}
						PlayerInput.MouseX = num11 + num4;
						PlayerInput.MouseY = num12 + num5;
					}
				}
			}
			if (flag)
			{
				PlayerInput.CurrentInputMode = inputMode;
			}
			if (PlayerInput.CurrentInputMode != InputMode.XBoxGamepadUI && flag)
			{
				PlayerInput.PreventCursorModeSwappingToGamepad = true;
			}
			if (!flag)
			{
				PlayerInput.PreventCursorModeSwappingToGamepad = false;
			}
			if (PlayerInput.CurrentInputMode == InputMode.XBoxGamepadUI && flag && !PlayerInput.PreventCursorModeSwappingToGamepad)
			{
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
			}
			return flag;
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x004D871C File Offset: 0x004D691C
		private static void MouseInput()
		{
			bool flag = false;
			PlayerInput.MouseInfoOld = PlayerInput.MouseInfo;
			PlayerInput.MouseInfo = Mouse.GetState();
			PlayerInput.ScrollWheelValue += PlayerInput.MouseInfo.ScrollWheelValue;
			if (PlayerInput.MouseInfo.X != PlayerInput.MouseInfoOld.X || PlayerInput.MouseInfo.Y != PlayerInput.MouseInfoOld.Y || PlayerInput.MouseInfo.ScrollWheelValue != PlayerInput.MouseInfoOld.ScrollWheelValue)
			{
				PlayerInput.MouseX = (int)((float)PlayerInput.MouseInfo.X * PlayerInput.RawMouseScale.X);
				PlayerInput.MouseY = (int)((float)PlayerInput.MouseInfo.Y * PlayerInput.RawMouseScale.Y);
				if (!PlayerInput.PreventFirstMousePositionGrab)
				{
					flag = true;
					PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Mouse);
				}
				PlayerInput.PreventFirstMousePositionGrab = false;
			}
			PlayerInput.MouseKeys.Clear();
			if (FocusHelper.AllowInputProcessing)
			{
				if (PlayerInput.MouseInfo.LeftButton == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse1");
					flag = true;
				}
				if (PlayerInput.MouseInfo.RightButton == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse2");
					flag = true;
				}
				if (PlayerInput.MouseInfo.MiddleButton == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse3");
					flag = true;
				}
				if (PlayerInput.MouseInfo.XButton1 == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse4");
					flag = true;
				}
				if (PlayerInput.MouseInfo.XButton2 == ButtonState.Pressed)
				{
					PlayerInput.MouseKeys.Add("Mouse5");
					flag = true;
				}
			}
			if (flag)
			{
				PlayerInput.CurrentInputMode = InputMode.Mouse;
				PlayerInput.Triggers.Current.UsedMovementKey = false;
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x004D88A8 File Offset: 0x004D6AA8
		private static bool KeyboardInput()
		{
			bool flag = false;
			bool flag2 = false;
			List<Keys> pressedKeys = PlayerInput.GetPressedKeys();
			PlayerInput.DebugKeys(pressedKeys);
			if (pressedKeys.Count == 0 && PlayerInput.MouseKeys.Count == 0)
			{
				Main.blockKey = Keys.None.ToString();
				return false;
			}
			for (int i = 0; i < pressedKeys.Count; i++)
			{
				if (pressedKeys[i] == Keys.LeftShift || pressedKeys[i] == Keys.RightShift)
				{
					flag = true;
				}
				else if (pressedKeys[i] == Keys.LeftAlt || pressedKeys[i] == Keys.RightAlt)
				{
					flag2 = true;
				}
				Main.ChromaPainter.PressKey(pressedKeys[i]);
			}
			if (Main.blockKey != Keys.None.ToString())
			{
				bool flag3 = false;
				for (int j = 0; j < pressedKeys.Count; j++)
				{
					if (pressedKeys[j].ToString() == Main.blockKey)
					{
						pressedKeys[j] = Keys.None;
						flag3 = true;
					}
				}
				if (!flag3)
				{
					Main.blockKey = Keys.None.ToString();
				}
			}
			KeyConfiguration keyConfiguration = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard];
			if (Main.gameMenu && !PlayerInput.WritingText)
			{
				keyConfiguration = PlayerInput.CurrentProfile.InputModes[InputMode.KeyboardUI];
			}
			List<string> list = new List<string>(pressedKeys.Count);
			for (int k = 0; k < pressedKeys.Count; k++)
			{
				list.Add(pressedKeys[k].ToString());
			}
			if (PlayerInput.WritingText)
			{
				list.Clear();
			}
			int count = list.Count;
			list.AddRange(PlayerInput.MouseKeys);
			bool flag4 = false;
			for (int l = 0; l < list.Count; l++)
			{
				if (l >= count || pressedKeys[l] != Keys.None)
				{
					string text = list[l];
					if (!(list[l] == Keys.Tab.ToString()) || ((!flag || SocialAPI.Mode != SocialMode.Steam) && !flag2))
					{
						if (PlayerInput.CheckRebindingProcessKeyboard(text))
						{
							return false;
						}
						KeyboardState oldKeyState = Main.oldKeyState;
						if (l >= count || !Main.oldKeyState.IsKeyDown(pressedKeys[l]))
						{
							keyConfiguration.Processkey(PlayerInput.Triggers.Current, text, InputMode.Keyboard);
						}
						else
						{
							keyConfiguration.CopyKeyState(PlayerInput.Triggers.Old, PlayerInput.Triggers.Current, text);
						}
						if (l >= count || pressedKeys[l] != Keys.None)
						{
							flag4 = true;
						}
					}
				}
			}
			if (flag4)
			{
				PlayerInput.CurrentInputMode = InputMode.Keyboard;
			}
			return flag4;
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00009E46 File Offset: 0x00008046
		private static void DebugKeys(List<Keys> keys)
		{
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x004D8B54 File Offset: 0x004D6D54
		private static void FixDerpedRebinds()
		{
			List<string> list = new List<string> { "MouseLeft", "MouseRight", "Inventory" };
			foreach (object obj in Enum.GetValues(typeof(InputMode)))
			{
				InputMode inputMode = (InputMode)obj;
				if (inputMode != InputMode.Mouse)
				{
					PlayerInput.FixKeysConflict(inputMode, list);
					foreach (string text in list)
					{
						if (PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[text].Count < 1)
						{
							PlayerInput.ResetKeyBinding(inputMode, text);
						}
					}
				}
			}
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x004D8C48 File Offset: 0x004D6E48
		private static void FixKeysConflict(InputMode inputMode, List<string> triggers)
		{
			for (int i = 0; i < triggers.Count; i++)
			{
				for (int j = i + 1; j < triggers.Count; j++)
				{
					List<string> list = PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[triggers[i]];
					List<string> list2 = PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[triggers[j]];
					foreach (string text in list.Intersect(list2).ToList<string>())
					{
						list.Remove(text);
						list2.Remove(text);
					}
				}
			}
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x004D8D24 File Offset: 0x004D6F24
		private static void ResetKeyBinding(InputMode inputMode, string trigger)
		{
			string text = "Redigit's Pick";
			if (PlayerInput.OriginalProfiles.ContainsKey(PlayerInput._selectedProfile))
			{
				text = PlayerInput._selectedProfile;
			}
			PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[trigger].Clear();
			PlayerInput.CurrentProfile.InputModes[inputMode].KeyStatus[trigger].AddRange(PlayerInput.OriginalProfiles[text].InputModes[inputMode].KeyStatus[trigger]);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x004D8DB0 File Offset: 0x004D6FB0
		private static bool CheckRebindingProcessGamepad(string newKey)
		{
			PlayerInput._canReleaseRebindingLock = false;
			if (!PlayerInput.CurrentlyRebinding)
			{
				return PlayerInput.NavigatorRebindingLock > 0;
			}
			if (PlayerInput.CurrentlyRebinding && PlayerInput._listeningInputMode == InputMode.XBoxGamepad)
			{
				PlayerInput.NavigatorRebindingLock = 3;
				PlayerInput._memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[PlayerInput.ListeningTrigger].Contains(newKey))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[PlayerInput.ListeningTrigger].Remove(newKey);
				}
				else
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepad].KeyStatus[PlayerInput.ListeningTrigger] = new List<string> { newKey };
				}
				PlayerInput.ListenFor(null, InputMode.XBoxGamepad);
			}
			if (PlayerInput.CurrentlyRebinding && PlayerInput._listeningInputMode == InputMode.XBoxGamepadUI)
			{
				PlayerInput.NavigatorRebindingLock = 3;
				PlayerInput._memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[PlayerInput.ListeningTrigger].Contains(newKey))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[PlayerInput.ListeningTrigger].Remove(newKey);
				}
				else
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.XBoxGamepadUI].KeyStatus[PlayerInput.ListeningTrigger] = new List<string> { newKey };
				}
				PlayerInput.ListenFor(null, InputMode.XBoxGamepadUI);
			}
			PlayerInput.FixDerpedRebinds();
			if (PlayerInput.OnBindingChange != null)
			{
				PlayerInput.OnBindingChange();
			}
			return PlayerInput.NavigatorRebindingLock > 0;
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x004D8F68 File Offset: 0x004D7168
		private static bool CheckRebindingProcessKeyboard(string newKey)
		{
			PlayerInput._canReleaseRebindingLock = false;
			if (!PlayerInput.CurrentlyRebinding)
			{
				return PlayerInput.NavigatorRebindingLock > 0;
			}
			if (PlayerInput.CurrentlyRebinding && PlayerInput._listeningInputMode == InputMode.Keyboard)
			{
				PlayerInput.NavigatorRebindingLock = 3;
				PlayerInput._memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[PlayerInput.ListeningTrigger].Contains(newKey))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[PlayerInput.ListeningTrigger].Remove(newKey);
				}
				else
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[PlayerInput.ListeningTrigger] = new List<string> { newKey };
				}
				PlayerInput.ListenFor(null, InputMode.Keyboard);
				Main.blockKey = newKey;
				Main.blockInput = false;
				Main.ChromaPainter.CollectBoundKeys();
			}
			if (PlayerInput.CurrentlyRebinding && PlayerInput._listeningInputMode == InputMode.KeyboardUI)
			{
				PlayerInput.NavigatorRebindingLock = 3;
				PlayerInput._memoOfLastPoint = UILinkPointNavigator.CurrentPoint;
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (PlayerInput.CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[PlayerInput.ListeningTrigger].Contains(newKey))
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[PlayerInput.ListeningTrigger].Remove(newKey);
				}
				else
				{
					PlayerInput.CurrentProfile.InputModes[InputMode.KeyboardUI].KeyStatus[PlayerInput.ListeningTrigger] = new List<string> { newKey };
				}
				PlayerInput.ListenFor(null, InputMode.KeyboardUI);
				Main.blockKey = newKey;
				Main.blockInput = false;
				Main.ChromaPainter.CollectBoundKeys();
			}
			PlayerInput.FixDerpedRebinds();
			if (PlayerInput.OnBindingChange != null)
			{
				PlayerInput.OnBindingChange();
			}
			return PlayerInput.NavigatorRebindingLock > 0;
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x004D914C File Offset: 0x004D734C
		private static void PostInput()
		{
			Main.GamepadCursorAlpha = MathHelper.Clamp(Main.GamepadCursorAlpha + ((Main.SmartCursorIsUsed && !UILinkPointNavigator.Available && PlayerInput.GamepadThumbstickLeft == Vector2.Zero && PlayerInput.GamepadThumbstickRight == Vector2.Zero) ? (-0.05f) : 0.05f), 0f, 1f);
			if (PlayerInput.CurrentProfile.HotbarAllowsRadial)
			{
				int num = PlayerInput.Triggers.Current.HotbarPlus.ToInt() - PlayerInput.Triggers.Current.HotbarMinus.ToInt();
				if (PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed)
				{
					if (num == 1)
					{
						PlayerInput.Triggers.Current.RadialHotbar = true;
						PlayerInput.Triggers.JustReleased.RadialHotbar = false;
					}
					else if (num == -1)
					{
						PlayerInput.Triggers.Current.RadialQuickbar = true;
						PlayerInput.Triggers.JustReleased.RadialQuickbar = false;
					}
				}
			}
			PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed = false;
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x004D923C File Offset: 0x004D743C
		private static void HandleDpadSnap()
		{
			Vector2 vector = Vector2.Zero;
			Player player = Main.player[Main.myPlayer];
			for (int i = 0; i < 4; i++)
			{
				bool flag = false;
				Vector2 vector2 = Vector2.Zero;
				if (Main.gameMenu || (UILinkPointNavigator.Available && !PlayerInput.InBuildingMode))
				{
					return;
				}
				switch (i)
				{
				case 0:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap1;
					vector2 = -Vector2.UnitY;
					break;
				case 1:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap2;
					vector2 = Vector2.UnitX;
					break;
				case 2:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap3;
					vector2 = Vector2.UnitY;
					break;
				case 3:
					flag = PlayerInput.Triggers.Current.DpadMouseSnap4;
					vector2 = -Vector2.UnitX;
					break;
				}
				if (PlayerInput.DpadSnapCooldown[i] > 0)
				{
					PlayerInput.DpadSnapCooldown[i]--;
				}
				if (flag)
				{
					if (PlayerInput.DpadSnapCooldown[i] == 0)
					{
						int num = 6;
						if (ItemSlot.IsABuildingItem(player.inventory[player.selectedItem]))
						{
							num = player.inventory[player.selectedItem].useTime;
						}
						PlayerInput.DpadSnapCooldown[i] = num;
						vector += vector2;
					}
				}
				else
				{
					PlayerInput.DpadSnapCooldown[i] = 0;
				}
			}
			if (vector != Vector2.Zero)
			{
				Main.SmartCursorWanted_GamePad = false;
				Matrix matrix = Main.GameViewMatrix.ZoomMatrix * Main.GameViewMatrix.EffectMatrix;
				Matrix matrix2 = Matrix.Invert(matrix);
				Vector2 vector3 = Vector2.Transform(Main.MouseScreen, matrix2);
				vector.Y *= Main.LocalPlayer.gravDir;
				Vector2 vector4 = Vector2.Transform((vector3 + vector * new Vector2(16f) + Main.screenPosition).ToTileCoordinates().ToWorldCoordinates(8f, 8f) - Main.screenPosition, matrix);
				PlayerInput.MouseX = (int)vector4.X;
				PlayerInput.MouseY = (int)vector4.Y;
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
			}
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x004D9439 File Offset: 0x004D7639
		private static bool ShouldShowInstructionsForGamepad()
		{
			return PlayerInput.UsingGamepad || PlayerInput.SteamDeckIsUsed;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x004D944C File Offset: 0x004D764C
		public static string ComposeInstructionsForGamepad()
		{
			string text = string.Empty;
			InputMode inputMode = InputMode.XBoxGamepad;
			if (Main.gameMenu || UILinkPointNavigator.Available)
			{
				inputMode = InputMode.XBoxGamepadUI;
			}
			if (PlayerInput.InBuildingMode && !Main.gameMenu)
			{
				inputMode = InputMode.XBoxGamepad;
			}
			KeyConfiguration keyConfiguration = PlayerInput.CurrentProfile.InputModes[inputMode];
			Player localPlayer = Main.LocalPlayer;
			if (Main.mapFullscreen && !Main.gameMenu)
			{
				text += "          ";
				text += PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"] });
				text += PlayerInput.BuildCommand(Lang.inter[118].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"] });
				text += PlayerInput.BuildCommand(Lang.inter[119].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"] });
				if (Main.netMode == 1 && Main.player[Main.myPlayer].HasItem(2997))
				{
					text += PlayerInput.BuildCommand(Lang.inter[120].Value, new List<string>[] { PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"] });
				}
			}
			else if (inputMode == InputMode.XBoxGamepadUI && !PlayerInput.InBuildingMode)
			{
				text = UILinkPointNavigator.GetInstructions();
			}
			else if ((localPlayer.dead && localPlayer.CanDeathSpectate && localPlayer.AnyoneToSpectate()) || localPlayer.spectating >= 0)
			{
				string textValue = Language.GetTextValue((localPlayer.spectating >= 0) ? "Game.GamepadSpectateChangeTarget" : "Game.GamepadSpectate");
				text += PlayerInput.BuildCommand(textValue, new List<string>[]
				{
					keyConfiguration.KeyStatus["Left"],
					keyConfiguration.KeyStatus["Right"]
				});
				if (localPlayer.spectating >= 0)
				{
					text += PlayerInput.BuildCommand(Language.GetTextValue("Game.GamepadSpectateCancel"), new List<string>[] { keyConfiguration.KeyStatus["Jump"] });
				}
				if (localPlayer.CanWormholeToSpectating())
				{
					text += PlayerInput.BuildCommand(Language.GetTextValue("Game.GamepadSpectateWormhole"), new List<string>[] { keyConfiguration.KeyStatus["QuickBuff"] });
				}
			}
			else
			{
				if (localPlayer.dead)
				{
					return text;
				}
				text += PlayerInput.BuildCommand(Lang.misc[58].Value, new List<string>[] { keyConfiguration.KeyStatus["Jump"] });
				text += PlayerInput.BuildCommand(Lang.misc[59].Value, new List<string>[]
				{
					keyConfiguration.KeyStatus["HotbarMinus"],
					keyConfiguration.KeyStatus["HotbarPlus"]
				});
				if (PlayerInput.InBuildingMode)
				{
					text += PlayerInput.BuildCommand(Lang.menu[6].Value, new List<string>[]
					{
						keyConfiguration.KeyStatus["Inventory"],
						keyConfiguration.KeyStatus["MouseRight"]
					});
				}
				if (WiresUI.Open)
				{
					text += PlayerInput.BuildCommand(Lang.misc[53].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseLeft"] });
					text += PlayerInput.BuildCommand(Lang.misc[56].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseRight"] });
				}
				else
				{
					Item item = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem];
					if (item.damage > 0 && item.ammo == 0)
					{
						text += PlayerInput.BuildCommand(Lang.misc[60].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseLeft"] });
					}
					else if (item.createTile >= 0 || item.createWall > 0)
					{
						text += PlayerInput.BuildCommand(Lang.misc[61].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseLeft"] });
					}
					else
					{
						text += PlayerInput.BuildCommand(Lang.misc[63].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseLeft"] });
					}
					bool flag = true;
					bool flag2 = Main.SmartInteractProj != -1 || Main.HasInteractableObjectThatIsNotATile;
					bool flag3 = !Main.SmartInteractShowingGenuine && Main.SmartInteractShowingFake;
					if (Main.SmartInteractShowingGenuine || Main.SmartInteractShowingFake || flag2)
					{
						if (Main.SmartInteractNPC != -1)
						{
							if (flag3)
							{
								flag = false;
							}
							text += PlayerInput.BuildCommand(Lang.misc[80].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseRight"] });
						}
						else if (flag2)
						{
							if (flag3)
							{
								flag = false;
							}
							text += PlayerInput.BuildCommand(Lang.misc[79].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseRight"] });
						}
						else if (Main.SmartInteractX != -1 && Main.SmartInteractY != -1)
						{
							if (flag3)
							{
								flag = false;
							}
							Tile tile = Main.tile[Main.SmartInteractX, Main.SmartInteractY];
							if (TileID.Sets.TileInteractRead[(int)tile.type])
							{
								text += PlayerInput.BuildCommand(Lang.misc[81].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseRight"] });
							}
							else
							{
								text += PlayerInput.BuildCommand(Lang.misc[79].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseRight"] });
							}
						}
					}
					else if (WiresUI.Settings.DrawToolModeUI)
					{
						text += PlayerInput.BuildCommand(Lang.misc[89].Value, new List<string>[] { keyConfiguration.KeyStatus["MouseRight"] });
					}
					else if (ItemID.Sets.HasRightFire[item.type])
					{
						text += PlayerInput.BuildCommand(Language.GetTextValue("UI.ActionAltAction"), new List<string>[] { keyConfiguration.KeyStatus["MouseRight"] });
					}
					if ((!PlayerInput.GrappleAndInteractAreShared || (!WiresUI.Settings.DrawToolModeUI && (!Main.SmartInteractShowingGenuine || !Main.HasSmartInteractTarget) && (!Main.SmartInteractShowingFake || flag) && !ItemID.Sets.HasRightFire[item.type])) && Main.LocalPlayer.QuickGrapple_GetItemToUse() != null)
					{
						text += PlayerInput.BuildCommand(Lang.misc[57].Value, new List<string>[] { keyConfiguration.KeyStatus["Grapple"] });
					}
				}
			}
			return text;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x004D9B44 File Offset: 0x004D7D44
		public static string BuildCommand(string CommandText, params List<string>[] Bindings)
		{
			string text = "";
			if (Bindings.Length == 0)
			{
				return text;
			}
			text += PlayerInput.GenerateGlyphList(Bindings[0]);
			for (int i = 1; i < Bindings.Length; i++)
			{
				string text2 = PlayerInput.GenerateGlyphList(Bindings[i]);
				if (text2.Length > 0)
				{
					text = text + "/" + text2;
				}
			}
			if (text.Length > 0)
			{
				text = text + ": " + CommandText + "   ";
			}
			return text;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x004D9BB8 File Offset: 0x004D7DB8
		public static string GenerateInputTag_ForCurrentGamemode(bool tagForGameplay, string triggerName)
		{
			if (PlayerInput.UsingGamepad)
			{
				return PlayerInput.GenerateGlyphList(PlayerInput.CurrentProfile.InputModes[tagForGameplay ? InputMode.XBoxGamepad : InputMode.XBoxGamepadUI].KeyStatus[triggerName]);
			}
			return PlayerInput.GenerateRawInputList(PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[triggerName]);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x004D9C13 File Offset: 0x004D7E13
		private static string LocalizeKey(string keyName)
		{
			if (keyName == "Mouse1")
			{
				return Language.GetTextValue("Controls.LeftClick");
			}
			if (keyName == "Mouse2")
			{
				return Language.GetTextValue("Controls.RightClick");
			}
			return keyName;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x004D9C48 File Offset: 0x004D7E48
		private static string GenerateGlyphList(List<string> list)
		{
			if (list.Count == 0)
			{
				return "";
			}
			string text = GlyphTagHandler.GenerateTag(list[0]);
			for (int i = 1; i < list.Count; i++)
			{
				text = text + "/" + GlyphTagHandler.GenerateTag(list[i]);
			}
			return text;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x004D9C9C File Offset: 0x004D7E9C
		private static string GenerateRawInputList(List<string> list)
		{
			if (list.Count == 0)
			{
				return "";
			}
			string text = PlayerInput.LocalizeKey(list[0]);
			for (int i = 1; i < list.Count; i++)
			{
				text = text + "/" + PlayerInput.LocalizeKey(list[i]);
			}
			return text;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x004D9CEE File Offset: 0x004D7EEE
		public static void NavigatorCachePosition()
		{
			PlayerInput.PreUIX = PlayerInput.MouseX;
			PlayerInput.PreUIY = PlayerInput.MouseY;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x004D9D04 File Offset: 0x004D7F04
		public static void NavigatorUnCachePosition()
		{
			PlayerInput.MouseX = PlayerInput.PreUIX;
			PlayerInput.MouseY = PlayerInput.PreUIY;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x004D9D1A File Offset: 0x004D7F1A
		public static void LockOnCachePosition()
		{
			PlayerInput.PreLockOnX = PlayerInput.MouseX;
			PlayerInput.PreLockOnY = PlayerInput.MouseY;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x004D9D30 File Offset: 0x004D7F30
		public static void LockOnUnCachePosition()
		{
			PlayerInput.MouseX = PlayerInput.PreLockOnX;
			PlayerInput.MouseY = PlayerInput.PreLockOnY;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x004D9D48 File Offset: 0x004D7F48
		public static void PrettyPrintProfiles(ref string text)
		{
			foreach (string text2 in text.Split(new string[] { "\r\n" }, StringSplitOptions.None))
			{
				if (text2.Contains(": {"))
				{
					string text3 = text2.Substring(0, text2.IndexOf('"'));
					string text4 = text2 + "\r\n  ";
					string text5 = text4.Replace(": {\r\n  ", ": \r\n" + text3 + "{\r\n  ");
					text = text.Replace(text4, text5);
				}
			}
			text = text.Replace("[\r\n        ", "[");
			text = text.Replace("[\r\n      ", "[");
			text = text.Replace("\"\r\n      ", "\"");
			text = text.Replace("\",\r\n        ", "\", ");
			text = text.Replace("\",\r\n      ", "\", ");
			text = text.Replace("\r\n    ]", "]");
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x004D9E48 File Offset: 0x004D8048
		public static void PrettyPrintProfilesOld(ref string text)
		{
			text = text.Replace(": {\r\n  ", ": \r\n  {\r\n  ");
			text = text.Replace("[\r\n      ", "[");
			text = text.Replace("\"\r\n      ", "\"");
			text = text.Replace("\",\r\n      ", "\", ");
			text = text.Replace("\r\n    ]", "]");
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x004D9EB4 File Offset: 0x004D80B4
		public static void Reset(KeyConfiguration c, PresetProfiles style, InputMode mode)
		{
			switch (style)
			{
			case PresetProfiles.Redigit:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					c.KeyStatus["Loadout1"].Add("F1");
					c.KeyStatus["Loadout2"].Add("F2");
					c.KeyStatus["Loadout3"].Add("F3");
					c.KeyStatus["ToggleCameraMode"].Add("F4");
					return;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.B));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.A));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MapStyle"].Add(string.Concat(Buttons.Back));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					return;
				default:
					return;
				}
				break;
			case PresetProfiles.Yoraiz0r:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					c.KeyStatus["Loadout1"].Add("F1");
					c.KeyStatus["Loadout2"].Add("F2");
					c.KeyStatus["Loadout3"].Add("F3");
					c.KeyStatus["ToggleCameraMode"].Add("F4");
					return;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.X));
					c.KeyStatus["QuickHeal"].Add(string.Concat(Buttons.A));
					c.KeyStatus["RadialHotbar"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MapStyle"].Add(string.Concat(Buttons.Back));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadSnap1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadSnap3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadSnap4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadSnap2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					return;
				default:
					return;
				}
				break;
			case PresetProfiles.ConsolePS:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					c.KeyStatus["Loadout1"].Add("F1");
					c.KeyStatus["Loadout2"].Add("F2");
					c.KeyStatus["Loadout3"].Add("F3");
					c.KeyStatus["ToggleCameraMode"].Add("F4");
					return;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.A));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.Back));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					return;
				default:
					return;
				}
				break;
			case PresetProfiles.ConsoleXBox:
				switch (mode)
				{
				case InputMode.Keyboard:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Jump"].Add("Space");
					c.KeyStatus["Inventory"].Add("Escape");
					c.KeyStatus["Grapple"].Add("E");
					c.KeyStatus["SmartSelect"].Add("LeftShift");
					c.KeyStatus["SmartCursor"].Add("LeftControl");
					c.KeyStatus["QuickMount"].Add("R");
					c.KeyStatus["QuickHeal"].Add("H");
					c.KeyStatus["QuickMana"].Add("J");
					c.KeyStatus["QuickBuff"].Add("B");
					c.KeyStatus["MapStyle"].Add("Tab");
					c.KeyStatus["MapFull"].Add("M");
					c.KeyStatus["MapZoomIn"].Add("Add");
					c.KeyStatus["MapZoomOut"].Add("Subtract");
					c.KeyStatus["MapAlphaUp"].Add("PageUp");
					c.KeyStatus["MapAlphaDown"].Add("PageDown");
					c.KeyStatus["Hotbar1"].Add("D1");
					c.KeyStatus["Hotbar2"].Add("D2");
					c.KeyStatus["Hotbar3"].Add("D3");
					c.KeyStatus["Hotbar4"].Add("D4");
					c.KeyStatus["Hotbar5"].Add("D5");
					c.KeyStatus["Hotbar6"].Add("D6");
					c.KeyStatus["Hotbar7"].Add("D7");
					c.KeyStatus["Hotbar8"].Add("D8");
					c.KeyStatus["Hotbar9"].Add("D9");
					c.KeyStatus["Hotbar10"].Add("D0");
					c.KeyStatus["ViewZoomOut"].Add("OemMinus");
					c.KeyStatus["ViewZoomIn"].Add("OemPlus");
					c.KeyStatus["ToggleCreativeMenu"].Add("C");
					c.KeyStatus["Loadout1"].Add("F1");
					c.KeyStatus["Loadout2"].Add("F2");
					c.KeyStatus["Loadout3"].Add("F3");
					c.KeyStatus["ToggleCameraMode"].Add("F4");
					return;
				case InputMode.KeyboardUI:
					c.KeyStatus["MouseLeft"].Add("Mouse1");
					c.KeyStatus["MouseLeft"].Add("Space");
					c.KeyStatus["MouseRight"].Add("Mouse2");
					c.KeyStatus["Up"].Add("W");
					c.KeyStatus["Up"].Add("Up");
					c.KeyStatus["Down"].Add("S");
					c.KeyStatus["Down"].Add("Down");
					c.KeyStatus["Left"].Add("A");
					c.KeyStatus["Left"].Add("Left");
					c.KeyStatus["Right"].Add("D");
					c.KeyStatus["Right"].Add("Right");
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["Inventory"].Add(Keys.Escape.ToString());
					return;
				case InputMode.Mouse:
					break;
				case InputMode.XBoxGamepad:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Jump"].Add(string.Concat(Buttons.A));
					c.KeyStatus["LockOn"].Add(string.Concat(Buttons.X));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.LeftStick));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.Back));
					return;
				case InputMode.XBoxGamepadUI:
					c.KeyStatus["MouseLeft"].Add(string.Concat(Buttons.A));
					c.KeyStatus["MouseRight"].Add(string.Concat(Buttons.LeftShoulder));
					c.KeyStatus["SmartCursor"].Add(string.Concat(Buttons.RightShoulder));
					c.KeyStatus["Up"].Add(string.Concat(Buttons.LeftThumbstickUp));
					c.KeyStatus["Down"].Add(string.Concat(Buttons.LeftThumbstickDown));
					c.KeyStatus["Left"].Add(string.Concat(Buttons.LeftThumbstickLeft));
					c.KeyStatus["Right"].Add(string.Concat(Buttons.LeftThumbstickRight));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.B));
					c.KeyStatus["Inventory"].Add(string.Concat(Buttons.Y));
					c.KeyStatus["HotbarMinus"].Add(string.Concat(Buttons.LeftTrigger));
					c.KeyStatus["HotbarPlus"].Add(string.Concat(Buttons.RightTrigger));
					c.KeyStatus["Grapple"].Add(string.Concat(Buttons.X));
					c.KeyStatus["MapFull"].Add(string.Concat(Buttons.Start));
					c.KeyStatus["SmartSelect"].Add(string.Concat(Buttons.Back));
					c.KeyStatus["QuickMount"].Add(string.Concat(Buttons.RightStick));
					c.KeyStatus["DpadRadial1"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["DpadRadial3"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["DpadRadial4"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["DpadRadial2"].Add(string.Concat(Buttons.DPadRight));
					c.KeyStatus["MenuUp"].Add(string.Concat(Buttons.DPadUp));
					c.KeyStatus["MenuDown"].Add(string.Concat(Buttons.DPadDown));
					c.KeyStatus["MenuLeft"].Add(string.Concat(Buttons.DPadLeft));
					c.KeyStatus["MenuRight"].Add(string.Concat(Buttons.DPadRight));
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x004DCD29 File Offset: 0x004DAF29
		public static void SetZoom_UI()
		{
			PlayerInput.SetZoom_Scaled(1f / Main.UIScale);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x004DCD3B File Offset: 0x004DAF3B
		public static void SetZoom_Background()
		{
			PlayerInput.SetZoom_Scaled(1f / Main.BackgroundViewMatrix.RenderZoom.X);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x004DCD57 File Offset: 0x004DAF57
		public static void SetZoom_World()
		{
			PlayerInput.SetZoom_Unscaled();
			PlayerInput.SetZoom_MouseInWorld();
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x004DCD63 File Offset: 0x004DAF63
		public static void SetZoom_Unscaled()
		{
			Main.lastMouseX = PlayerInput._originalLastMouseX;
			Main.lastMouseY = PlayerInput._originalLastMouseY;
			Main.mouseX = PlayerInput._originalMouseX;
			Main.mouseY = PlayerInput._originalMouseY;
			Main.screenWidth = PlayerInput._originalScreenWidth;
			Main.screenHeight = PlayerInput._originalScreenHeight;
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x004DCDA4 File Offset: 0x004DAFA4
		public static void SetZoom_MouseInWorld()
		{
			Vector2 vector = Main.screenPosition + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f;
			Vector2 vector2 = Main.screenPosition + new Vector2((float)PlayerInput._originalMouseX, (float)PlayerInput._originalMouseY);
			Vector2 vector3 = Main.screenPosition + new Vector2((float)PlayerInput._originalLastMouseX, (float)PlayerInput._originalLastMouseY);
			Vector2 vector4 = vector2 - vector;
			Vector2 vector5 = vector3 - vector;
			float num = 1f / Main.GameViewMatrix.RenderZoom.X;
			Vector2 vector6 = vector - Main.screenPosition + vector4 * num;
			Main.mouseX = (int)vector6.X;
			Main.mouseY = (int)vector6.Y;
			Vector2 vector7 = vector - Main.screenPosition + vector5 * num;
			Main.lastMouseX = (int)vector7.X;
			Main.lastMouseY = (int)vector7.Y;
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x004DCE94 File Offset: 0x004DB094
		private static void SetZoom_Scaled(float scale)
		{
			Main.lastMouseX = (int)((float)PlayerInput._originalLastMouseX * scale);
			Main.lastMouseY = (int)((float)PlayerInput._originalLastMouseY * scale);
			Main.mouseX = (int)((float)PlayerInput._originalMouseX * scale);
			Main.mouseY = (int)((float)PlayerInput._originalMouseY * scale);
			Main.screenWidth = (int)((float)PlayerInput._originalScreenWidth * scale);
			Main.screenHeight = (int)((float)PlayerInput._originalScreenHeight * scale);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x0000357B File Offset: 0x0000177B
		public PlayerInput()
		{
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x004DCEF8 File Offset: 0x004DB0F8
		// Note: this type is marked as 'beforefieldinit'.
		static PlayerInput()
		{
		}

		// Token: 0x0400118A RID: 4490
		[CompilerGenerated]
		private static Action OnBindingChange;

		// Token: 0x0400118B RID: 4491
		[CompilerGenerated]
		private static Action OnActionableInput;

		// Token: 0x0400118C RID: 4492
		public static Vector2 RawMouseScale = Vector2.One;

		// Token: 0x0400118D RID: 4493
		public static TriggersPack Triggers = new TriggersPack();

		// Token: 0x0400118E RID: 4494
		public static List<string> KnownTriggers = new List<string>
		{
			"MouseLeft", "MouseRight", "Up", "Down", "Left", "Right", "Jump", "Throw", "Inventory", "Grapple",
			"SmartSelect", "SmartCursor", "QuickMount", "QuickHeal", "QuickMana", "QuickBuff", "MapZoomIn", "MapZoomOut", "MapAlphaUp", "MapAlphaDown",
			"MapFull", "MapStyle", "Hotbar1", "Hotbar2", "Hotbar3", "Hotbar4", "Hotbar5", "Hotbar6", "Hotbar7", "Hotbar8",
			"Hotbar9", "Hotbar10", "HotbarMinus", "HotbarPlus", "DpadRadial1", "DpadRadial2", "DpadRadial3", "DpadRadial4", "RadialHotbar", "RadialQuickbar",
			"DpadSnap1", "DpadSnap2", "DpadSnap3", "DpadSnap4", "MenuUp", "MenuDown", "MenuLeft", "MenuRight", "LockOn", "ViewZoomIn",
			"ViewZoomOut", "Loadout1", "Loadout2", "Loadout3", "PreviousLoadout", "NextLoadout", "ToggleCameraMode", "ToggleCreativeMenu", "ArmorSetAbility", "Dash"
		};

		// Token: 0x0400118F RID: 4495
		private static bool _canReleaseRebindingLock = true;

		// Token: 0x04001190 RID: 4496
		private static int _memoOfLastPoint = -1;

		// Token: 0x04001191 RID: 4497
		public static int NavigatorRebindingLock;

		// Token: 0x04001192 RID: 4498
		public static string BlockedKey = "";

		// Token: 0x04001193 RID: 4499
		private static string _listeningTrigger;

		// Token: 0x04001194 RID: 4500
		private static InputMode _listeningInputMode;

		// Token: 0x04001195 RID: 4501
		public static Dictionary<string, PlayerInputProfile> Profiles = new Dictionary<string, PlayerInputProfile>();

		// Token: 0x04001196 RID: 4502
		public static Dictionary<string, PlayerInputProfile> OriginalProfiles = new Dictionary<string, PlayerInputProfile>();

		// Token: 0x04001197 RID: 4503
		private static string _selectedProfile;

		// Token: 0x04001198 RID: 4504
		private static PlayerInputProfile _currentProfile;

		// Token: 0x04001199 RID: 4505
		private static InputMode _inputMode = InputMode.Keyboard;

		// Token: 0x0400119A RID: 4506
		private static Buttons[] ButtonsGamepad = (Buttons[])Enum.GetValues(typeof(Buttons));

		// Token: 0x0400119B RID: 4507
		public static bool GrappleAndInteractAreShared;

		// Token: 0x0400119C RID: 4508
		public static SmartSelectGamepadPointer smartSelectPointer = new SmartSelectGamepadPointer();

		// Token: 0x0400119D RID: 4509
		public static bool UseSteamDeckIfPossible;

		// Token: 0x0400119E RID: 4510
		private static string _invalidatorCheck = "";

		// Token: 0x0400119F RID: 4511
		private static bool _lastActivityState;

		// Token: 0x040011A0 RID: 4512
		public static MouseState MouseInfo;

		// Token: 0x040011A1 RID: 4513
		public static MouseState MouseInfoOld;

		// Token: 0x040011A2 RID: 4514
		public static int MouseX;

		// Token: 0x040011A3 RID: 4515
		public static int MouseY;

		// Token: 0x040011A4 RID: 4516
		public static bool LockGamepadTileUseButton = false;

		// Token: 0x040011A5 RID: 4517
		public static List<string> MouseKeys = new List<string>();

		// Token: 0x040011A6 RID: 4518
		public static int PreUIX;

		// Token: 0x040011A7 RID: 4519
		public static int PreUIY;

		// Token: 0x040011A8 RID: 4520
		public static int PreLockOnX;

		// Token: 0x040011A9 RID: 4521
		public static int PreLockOnY;

		// Token: 0x040011AA RID: 4522
		public static int ScrollWheelValue;

		// Token: 0x040011AB RID: 4523
		public static int ScrollWheelValueOld;

		// Token: 0x040011AC RID: 4524
		public static int ScrollWheelDelta;

		// Token: 0x040011AD RID: 4525
		public static int ScrollWheelDeltaForUI;

		// Token: 0x040011AE RID: 4526
		public static bool GamepadAllowScrolling;

		// Token: 0x040011AF RID: 4527
		public static int GamepadScrollValue;

		// Token: 0x040011B0 RID: 4528
		public static Vector2 GamepadThumbstickLeft = Vector2.Zero;

		// Token: 0x040011B1 RID: 4529
		public static Vector2 GamepadThumbstickRight = Vector2.Zero;

		// Token: 0x040011B2 RID: 4530
		private static PlayerInput.FastUseItemMemory _fastUseMemory;

		// Token: 0x040011B3 RID: 4531
		private static bool _InBuildingMode;

		// Token: 0x040011B4 RID: 4532
		private static int _UIPointForBuildingMode = -1;

		// Token: 0x040011B5 RID: 4533
		public static bool WritingText;

		// Token: 0x040011B6 RID: 4534
		private static int _originalMouseX;

		// Token: 0x040011B7 RID: 4535
		private static int _originalMouseY;

		// Token: 0x040011B8 RID: 4536
		private static int _originalLastMouseX;

		// Token: 0x040011B9 RID: 4537
		private static int _originalLastMouseY;

		// Token: 0x040011BA RID: 4538
		private static int _originalScreenWidth;

		// Token: 0x040011BB RID: 4539
		private static int _originalScreenHeight;

		// Token: 0x040011BC RID: 4540
		private static List<string> _buttonsLocked = new List<string>();

		// Token: 0x040011BD RID: 4541
		public static Vector2 HousingMouseOffset;

		// Token: 0x040011BE RID: 4542
		public static bool PreventCursorModeSwappingToGamepad = false;

		// Token: 0x040011BF RID: 4543
		public static bool PreventFirstMousePositionGrab = false;

		// Token: 0x040011C0 RID: 4544
		private static int[] DpadSnapCooldown = new int[4];

		// Token: 0x040011C1 RID: 4545
		public static bool AllowExecutionOfGamepadInstructions = true;

		// Token: 0x040011C2 RID: 4546
		private static readonly List<string> _keysToLocalize = new List<string>();

		// Token: 0x02000689 RID: 1673
		public class MiscSettingsTEMP
		{
			// Token: 0x06003E81 RID: 16001 RVA: 0x0000357B File Offset: 0x0000177B
			public MiscSettingsTEMP()
			{
			}

			// Token: 0x06003E82 RID: 16002 RVA: 0x00697F0B File Offset: 0x0069610B
			// Note: this type is marked as 'beforefieldinit'.
			static MiscSettingsTEMP()
			{
			}

			// Token: 0x0400675B RID: 26459
			public static bool HotbarRadialShouldBeUsed = true;
		}

		// Token: 0x0200068A RID: 1674
		public static class SettingsForUI
		{
			// Token: 0x170004E8 RID: 1256
			// (get) Token: 0x06003E83 RID: 16003 RVA: 0x00697F13 File Offset: 0x00696113
			// (set) Token: 0x06003E84 RID: 16004 RVA: 0x00697F1A File Offset: 0x0069611A
			public static CursorMode CurrentCursorMode
			{
				[CompilerGenerated]
				get
				{
					return PlayerInput.SettingsForUI.<CurrentCursorMode>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					PlayerInput.SettingsForUI.<CurrentCursorMode>k__BackingField = value;
				}
			}

			// Token: 0x06003E85 RID: 16005 RVA: 0x00697F22 File Offset: 0x00696122
			public static void SetCursorMode(CursorMode cursorMode)
			{
				PlayerInput.SettingsForUI.CurrentCursorMode = cursorMode;
				if (PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Mouse)
				{
					PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode = 0;
				}
			}

			// Token: 0x170004E9 RID: 1257
			// (get) Token: 0x06003E86 RID: 16006 RVA: 0x004D9439 File Offset: 0x004D7639
			public static bool ShowGamepadHints
			{
				get
				{
					return PlayerInput.UsingGamepad || PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x170004EA RID: 1258
			// (get) Token: 0x06003E87 RID: 16007 RVA: 0x00697F37 File Offset: 0x00696137
			public static bool AllowSecondaryGamepadAim
			{
				get
				{
					return PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Gamepad || !PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x170004EB RID: 1259
			// (get) Token: 0x06003E88 RID: 16008 RVA: 0x00697F4B File Offset: 0x0069614B
			public static bool PushEquipmentAreaUp
			{
				get
				{
					return PlayerInput.UsingGamepad && !PlayerInput.SteamDeckIsUsed;
				}
			}

			// Token: 0x170004EC RID: 1260
			// (get) Token: 0x06003E89 RID: 16009 RVA: 0x00697F5E File Offset: 0x0069615E
			public static bool ShowGamepadCursor
			{
				get
				{
					if (PlayerInput.ControllerHousingCursorActive)
					{
						return true;
					}
					if (!PlayerInput.SteamDeckIsUsed)
					{
						return PlayerInput.UsingGamepad;
					}
					return PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Gamepad;
				}
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x06003E8A RID: 16010 RVA: 0x00697F7E File Offset: 0x0069617E
			public static bool HighlightThingsForMouse
			{
				get
				{
					return !PlayerInput.UsingGamepadUI || PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Mouse;
				}
			}

			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x06003E8B RID: 16011 RVA: 0x00697F91 File Offset: 0x00696191
			// (set) Token: 0x06003E8C RID: 16012 RVA: 0x00697F98 File Offset: 0x00696198
			public static int FramesSinceLastTimeInMouseMode
			{
				[CompilerGenerated]
				get
				{
					return PlayerInput.SettingsForUI.<FramesSinceLastTimeInMouseMode>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					PlayerInput.SettingsForUI.<FramesSinceLastTimeInMouseMode>k__BackingField = value;
				}
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x06003E8D RID: 16013 RVA: 0x00697FA0 File Offset: 0x006961A0
			public static bool PreventHighlightsForGamepad
			{
				get
				{
					return PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode == 0;
				}
			}

			// Token: 0x06003E8E RID: 16014 RVA: 0x00697FAA File Offset: 0x006961AA
			public static void UpdateCounters()
			{
				if (PlayerInput.SettingsForUI.CurrentCursorMode != CursorMode.Mouse)
				{
					PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode++;
				}
			}

			// Token: 0x06003E8F RID: 16015 RVA: 0x00697FBF File Offset: 0x006961BF
			public static void TryRevertingToMouseMode()
			{
				if (PlayerInput.SettingsForUI.FramesSinceLastTimeInMouseMode > 0)
				{
					return;
				}
				PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Mouse);
				PlayerInput.CurrentInputMode = InputMode.Mouse;
				PlayerInput.Triggers.Current.UsedMovementKey = false;
				PlayerInput.NavigatorUnCachePosition();
			}

			// Token: 0x0400675C RID: 26460
			[CompilerGenerated]
			private static CursorMode <CurrentCursorMode>k__BackingField;

			// Token: 0x0400675D RID: 26461
			[CompilerGenerated]
			private static int <FramesSinceLastTimeInMouseMode>k__BackingField;
		}

		// Token: 0x0200068B RID: 1675
		private struct FastUseItemMemory
		{
			// Token: 0x06003E90 RID: 16016 RVA: 0x00697FEC File Offset: 0x006961EC
			public bool TryStartForItemSlot(Player player, int itemSlot)
			{
				if (player.UsingOrReusingItem)
				{
					return false;
				}
				if (itemSlot < 0 || itemSlot >= 50)
				{
					this.Clear();
					return false;
				}
				this._player = player;
				this._slot = itemSlot;
				this._itemType = this._player.inventory[itemSlot].type;
				this._shouldFastUse = true;
				this._isMouseItem = false;
				ItemSlot.PickupItemIntoMouse(player.inventory, 0, itemSlot, player);
				return true;
			}

			// Token: 0x06003E91 RID: 16017 RVA: 0x00698057 File Offset: 0x00696257
			public bool TryStartForMouse(Player player)
			{
				if (player.UsingOrReusingItem)
				{
					return false;
				}
				this._player = player;
				this._slot = -1;
				this._itemType = Main.mouseItem.type;
				this._shouldFastUse = true;
				this._isMouseItem = true;
				return true;
			}

			// Token: 0x06003E92 RID: 16018 RVA: 0x00698090 File Offset: 0x00696290
			public void Clear()
			{
				this._shouldFastUse = false;
			}

			// Token: 0x06003E93 RID: 16019 RVA: 0x0069809C File Offset: 0x0069629C
			public bool CanFastUse()
			{
				if (!this._shouldFastUse)
				{
					return false;
				}
				if (this._isMouseItem)
				{
					return Main.mouseItem.type == this._itemType;
				}
				return this._player.inventory[this._slot].type == this._itemType;
			}

			// Token: 0x06003E94 RID: 16020 RVA: 0x006980F0 File Offset: 0x006962F0
			public void EndFastUse()
			{
				if (!this._shouldFastUse)
				{
					return;
				}
				if (!this._isMouseItem && this._player.inventory[this._slot].IsAir)
				{
					Utils.Swap<Item>(ref Main.mouseItem, ref this._player.inventory[this._slot]);
				}
				this.Clear();
			}

			// Token: 0x0400675E RID: 26462
			private int _slot;

			// Token: 0x0400675F RID: 26463
			private int _itemType;

			// Token: 0x04006760 RID: 26464
			private bool _shouldFastUse;

			// Token: 0x04006761 RID: 26465
			private bool _isMouseItem;

			// Token: 0x04006762 RID: 26466
			private Player _player;
		}
	}
}
