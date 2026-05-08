using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.Net;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.Creative
{
	// Token: 0x0200031F RID: 799
	public class CreativeUI
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00568EB1 File Offset: 0x005670B1
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x00568EB9 File Offset: 0x005670B9
		public bool Enabled
		{
			[CompilerGenerated]
			get
			{
				return this.<Enabled>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Enabled>k__BackingField = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x00568EC2 File Offset: 0x005670C2
		public bool Blocked
		{
			get
			{
				return Main.LocalPlayer.talkNPC != -1 || (!NewCraftingUI.Visible && (Main.LocalPlayer.chest != -1 || Main.LocalPlayer.tileEntityAnchor.IsInValidUseTileEntity()));
			}
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x00568EFC File Offset: 0x005670FC
		public CreativeUI()
		{
			for (int i = 0; i < this._itemSlotsForUI.Length; i++)
			{
				this._itemSlotsForUI[i] = new Item();
			}
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x00568F48 File Offset: 0x00567148
		public void Initialize()
		{
			this._buttonTexture = Main.Assets.Request<Texture2D>("Images/UI/Creative/Journey_Toggle", 1);
			this._buttonBorderTexture = Main.Assets.Request<Texture2D>("Images/UI/Creative/Journey_Toggle_MouseOver", 1);
			this._uiState = new UICreativePowersMenu();
			this._powersUI.SetState(this._uiState);
			this._initialized = true;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x00568FA4 File Offset: 0x005671A4
		public void Update(GameTime gameTime)
		{
			if (!this.Enabled)
			{
				return;
			}
			if (!Main.playerInventory)
			{
				return;
			}
			this._powersUI.Update(gameTime);
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x00568FC4 File Offset: 0x005671C4
		public void Draw(SpriteBatch spriteBatch)
		{
			if (!this._initialized)
			{
				this.Initialize();
			}
			if (Main.LocalPlayer.difficulty != 3)
			{
				this.Enabled = false;
				return;
			}
			if (this.Blocked)
			{
				return;
			}
			Vector2 vector = new Vector2(28f, 267f);
			Vector2 vector2 = new Vector2(353f, 258f);
			new Vector2(40f, 267f);
			vector2 + new Vector2(50f, 50f);
			if (Main.screenHeight < 650 && this.Enabled)
			{
				vector.X += 52f * Main.inventoryScale;
			}
			this.DrawToggleButton(spriteBatch, vector);
			if (!this.Enabled)
			{
				return;
			}
			this._powersUI.Draw(spriteBatch, Main.gameTimeCache);
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0056908E File Offset: 0x0056728E
		public UIElement ProvideItemSlotElement(int itemSlotContext)
		{
			if (itemSlotContext != 0)
			{
				return null;
			}
			return new UIItemSlot(this._itemSlotsForUI, itemSlotContext, 30);
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x005690A3 File Offset: 0x005672A3
		public Item GetItemByIndex(int itemSlotContext)
		{
			if (itemSlotContext != 0)
			{
				return null;
			}
			return this._itemSlotsForUI[itemSlotContext];
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x005690B2 File Offset: 0x005672B2
		public void SetItembyIndex(Item item, int itemSlotContext)
		{
			if (itemSlotContext == 0)
			{
				this._itemSlotsForUI[itemSlotContext] = item;
			}
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x005690C0 File Offset: 0x005672C0
		private void DrawToggleButton(SpriteBatch spritebatch, Vector2 location)
		{
			Vector2 vector = this._buttonTexture.Size();
			Rectangle rectangle = Utils.CenteredRectangle(location + vector / 2f, vector);
			UILinkPointNavigator.SetPosition(311, rectangle.Center.ToVector2());
			spritebatch.Draw(this._buttonTexture.Value, location, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Main.LocalPlayer.creativeInterface = false;
			if (rectangle.Contains(Main.MouseScreen.ToPoint()) && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.creativeInterface = true;
				Main.LocalPlayer.mouseInterface = true;
				if (this.Enabled)
				{
					Main.instance.MouseTextNoOverride(Language.GetTextValue("CreativePowers.PowersMenuOpen"), 0, 0, -1, -1, -1, -1, 0);
				}
				else
				{
					Main.instance.MouseTextNoOverride(Language.GetTextValue("CreativePowers.PowersMenuClosed"), 0, 0, -1, -1, -1, -1, 0);
				}
				spritebatch.Draw(this._buttonBorderTexture.Value, location, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					this.ToggleMenu();
				}
			}
			Main.DoStatefulTickSound(ref Main.CreativeMenuMouseOver, Main.LocalPlayer.creativeInterface);
			if (Main.LocalPlayerCreativeTracker.ItemSacrifices.AnyNewUnlocksFromTeammates)
			{
				Utils.DrawNotificationIcon(spritebatch, rectangle, 1f, false);
			}
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x00569238 File Offset: 0x00567438
		public void SwapItem(ref Item item)
		{
			Utils.Swap<Item>(ref item, ref this._itemSlotsForUI[0]);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x0056924C File Offset: 0x0056744C
		public void CloseMenu()
		{
			if (!this.Enabled)
			{
				return;
			}
			this.Enabled = false;
			this.StopPlayingSacrificeAnimations();
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00569264 File Offset: 0x00567464
		public void ResumeMenuFromGamepadSearch()
		{
			this.Enabled = true;
			this.GamepadMoveToSearchButtonHack = true;
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00569274 File Offset: 0x00567474
		public void ToggleMenu()
		{
			this.Enabled = !this.Enabled;
			this._powersUI.EscapeElements();
			UISliderBase.EscapeElements();
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			if (this.Enabled)
			{
				NewCraftingUI.Close(true, true);
				Main.LocalPlayer.chest = -1;
				Main.LocalPlayer.tileEntityAnchor.Clear();
				UILinkPointNavigator.ChangePoint(10000);
				return;
			}
			if (this._itemSlotsForUI[0].stack > 0)
			{
				Main.LocalPlayer.GetOrDropItem(this._itemSlotsForUI[0], GetItemSettings.ReturnItemFromSlot);
				this._itemSlotsForUI[0] = new Item();
				this.StopPlayingSacrificeAnimations();
			}
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00569323 File Offset: 0x00567523
		public bool IsShowingResearchMenu()
		{
			return this.Enabled && this._uiState != null && this._uiState.IsShowingResearchMenu;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00569342 File Offset: 0x00567542
		public void SacrificeItemInSacrificeSlot()
		{
			if (this._uiState == null)
			{
				return;
			}
			this._uiState.SacrificeWhatsInResearchMenu();
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00569358 File Offset: 0x00567558
		public void StopPlayingSacrificeAnimations()
		{
			if (this._uiState == null)
			{
				return;
			}
			this._uiState.StopPlayingResearchAnimations();
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00569370 File Offset: 0x00567570
		public bool ShouldDrawSacrificeArea()
		{
			if (!this._itemSlotsForUI[0].IsAir)
			{
				return true;
			}
			Item mouseItem = Main.mouseItem;
			int num;
			return !mouseItem.IsAir && CreativeItemSacrificesCatalog.Instance.TryGetSacrificeCountCapToUnlockInfiniteItems(mouseItem.type, out num) && Main.LocalPlayerCreativeTracker.ItemSacrifices.GetSacrificeCount(mouseItem.type) < num;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x005693D0 File Offset: 0x005675D0
		public bool GetSacrificeNumbers(out int itemIdChecked, out int amountWeHave, out int amountNeededTotal)
		{
			amountWeHave = 0;
			amountNeededTotal = 0;
			itemIdChecked = 0;
			Item item = this._itemSlotsForUI[0];
			if (!item.IsAir)
			{
				itemIdChecked = item.type;
			}
			return Main.LocalPlayerCreativeTracker.ItemSacrifices.TryGetSacrificeNumbers(item.type, out amountWeHave, out amountNeededTotal);
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0056941B File Offset: 0x0056761B
		public CreativeUI.ItemSacrificeResult SacrificeItem(out int amountWeSacrificed)
		{
			return this.SacrificeItem(ref this._itemSlotsForUI[0], out amountWeSacrificed, true, false);
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x00569434 File Offset: 0x00567634
		public CreativeUI.ItemSacrificeResult SacrificeItem(ref Item item, out int amountWeSacrificed, bool spawnExcessItem = true, bool onlySacrificeIfItWouldFinishResearch = false)
		{
			int num = 0;
			int num2 = 0;
			amountWeSacrificed = 0;
			if (!Main.LocalPlayerCreativeTracker.ItemSacrifices.TryGetSacrificeNumbers(item.type, out num2, out num))
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			int num3 = Utils.Clamp<int>(num - num2, 0, num);
			if (num3 == 0)
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			int num4 = Math.Min(num3, item.stack);
			bool flag = num4 == num3;
			if (onlySacrificeIfItWouldFinishResearch && !flag)
			{
				return CreativeUI.ItemSacrificeResult.CannotSacrifice;
			}
			NetPacket netPacket = NetCreativeUnlocksPlayerReportModule.SerializeSacrificeRequest(Main.myPlayer, item.type, num4);
			NetManager.Instance.SendToServer(netPacket);
			if (!Main.ServerSideCharacter)
			{
				Main.LocalPlayerCreativeTracker.ItemSacrifices.RegisterItemSacrifice(item.type, num4, null);
			}
			item.stack -= num4;
			if (item.stack <= 0)
			{
				item.TurnToAir(false);
			}
			amountWeSacrificed = num4;
			if (item.stack > 0 && spawnExcessItem)
			{
				item = Main.LocalPlayer.GetItem(item, GetItemSettings.ReturnItemFromSlot);
			}
			if (!flag)
			{
				return CreativeUI.ItemSacrificeResult.SacrificedButNotDone;
			}
			return CreativeUI.ItemSacrificeResult.SacrificedAndDone;
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x00569520 File Offset: 0x00567720
		public void Reset()
		{
			for (int i = 0; i < this._itemSlotsForUI.Length; i++)
			{
				this._itemSlotsForUI[i].TurnToAir(false);
			}
			this._initialized = false;
			this.Enabled = false;
		}

		// Token: 0x040050F7 RID: 20727
		public const int ItemSlotIndexes_SacrificeItem = 0;

		// Token: 0x040050F8 RID: 20728
		public const int ItemSlotIndexes_Count = 1;

		// Token: 0x040050F9 RID: 20729
		[CompilerGenerated]
		private bool <Enabled>k__BackingField;

		// Token: 0x040050FA RID: 20730
		private bool _initialized;

		// Token: 0x040050FB RID: 20731
		private Asset<Texture2D> _buttonTexture;

		// Token: 0x040050FC RID: 20732
		private Asset<Texture2D> _buttonBorderTexture;

		// Token: 0x040050FD RID: 20733
		private Item[] _itemSlotsForUI = new Item[1];

		// Token: 0x040050FE RID: 20734
		private UserInterface _powersUI = new UserInterface();

		// Token: 0x040050FF RID: 20735
		public bool GamepadMoveToSearchButtonHack;

		// Token: 0x04005100 RID: 20736
		private UICreativePowersMenu _uiState;

		// Token: 0x0200087F RID: 2175
		public enum ItemSacrificeResult
		{
			// Token: 0x040072AB RID: 29355
			CannotSacrifice,
			// Token: 0x040072AC RID: 29356
			SacrificedButNotDone,
			// Token: 0x040072AD RID: 29357
			SacrificedAndDone
		}
	}
}
