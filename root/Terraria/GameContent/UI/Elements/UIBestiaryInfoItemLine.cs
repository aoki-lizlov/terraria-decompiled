using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003D8 RID: 984
	public class UIBestiaryInfoItemLine : UIPanel, IManuallyOrderedUIElement
	{
		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06002DE1 RID: 11745 RVA: 0x005A7059 File Offset: 0x005A5259
		// (set) Token: 0x06002DE2 RID: 11746 RVA: 0x005A7061 File Offset: 0x005A5261
		public int OrderInUIList
		{
			[CompilerGenerated]
			get
			{
				return this.<OrderInUIList>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<OrderInUIList>k__BackingField = value;
			}
		}

		// Token: 0x06002DE3 RID: 11747 RVA: 0x005A706C File Offset: 0x005A526C
		public UIBestiaryInfoItemLine(DropRateInfo info, BestiaryUICollectionInfo uiinfo, float textScale = 1f)
		{
			this._infoDisplayItem = new Item();
			this._infoDisplayItem.SetDefaults(info.itemId, null);
			this.SetBestiaryNotesOnItemCache(info);
			base.SetPadding(0f);
			this.PaddingLeft = 10f;
			this.PaddingRight = 10f;
			this.Width.Set(-14f, 1f);
			this.Height.Set(32f, 0f);
			this.Left.Set(5f, 0f);
			base.OnMouseOver += this.MouseOver;
			base.OnMouseOut += this.MouseOut;
			this.BorderColor = new Color(89, 116, 213, 255);
			string text;
			string text2;
			this.GetDropInfo(info, uiinfo, out text, out text2);
			if (uiinfo.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				this._hideMouseOver = true;
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Icon_Locked", 1);
				UIElement uielement = new UIElement
				{
					Height = new StyleDimension(0f, 1f),
					Width = new StyleDimension(0f, 1f),
					HAlign = 0.5f,
					VAlign = 0.5f
				};
				uielement.SetPadding(0f);
				UIImage uiimage = new UIImage(asset)
				{
					ImageScale = 0.55f,
					HAlign = 0.5f,
					VAlign = 0.5f
				};
				uielement.Append(uiimage);
				base.Append(uielement);
				return;
			}
			UIItemIcon uiitemIcon = new UIItemIcon(this._infoDisplayItem, uiinfo.UnlockState < BestiaryEntryUnlockState.CanShowDropsWithoutDropRates_3)
			{
				IgnoresMouseInteraction = true,
				HAlign = 0f,
				Left = new StyleDimension(4f, 0f)
			};
			base.Append(uiitemIcon);
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text + " " + text2;
			}
			UITextPanel<string> uitextPanel = new UITextPanel<string>(text2, textScale, false)
			{
				IgnoresMouseInteraction = true,
				DrawPanel = false,
				HAlign = 1f,
				Top = new StyleDimension(-4f, 0f)
			};
			base.Append(uitextPanel);
		}

		// Token: 0x06002DE4 RID: 11748 RVA: 0x005A7290 File Offset: 0x005A5490
		protected void GetDropInfo(DropRateInfo dropRateInfo, BestiaryUICollectionInfo uiinfo, out string stackRange, out string droprate)
		{
			if (dropRateInfo.stackMin != dropRateInfo.stackMax)
			{
				stackRange = string.Format(" ({0}-{1})", dropRateInfo.stackMin, dropRateInfo.stackMax);
			}
			else if (dropRateInfo.stackMin == 1)
			{
				stackRange = "";
			}
			else
			{
				stackRange = " (" + dropRateInfo.stackMin + ")";
			}
			string text = "P";
			if ((double)dropRateInfo.dropRate < 0.001)
			{
				text = "P4";
			}
			if (dropRateInfo.dropRate != 1f)
			{
				droprate = Utils.PrettifyPercentDisplay(dropRateInfo.dropRate, text);
			}
			else
			{
				droprate = "100%";
			}
			if (uiinfo.UnlockState != BestiaryEntryUnlockState.CanShowDropsWithDropRates_4)
			{
				droprate = "???";
				stackRange = "";
			}
		}

		// Token: 0x06002DE5 RID: 11749 RVA: 0x005A7359 File Offset: 0x005A5559
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			if (base.IsMouseHovering && !this._hideMouseOver)
			{
				this.DrawMouseOver();
			}
		}

		// Token: 0x06002DE6 RID: 11750 RVA: 0x005A7378 File Offset: 0x005A5578
		private void DrawMouseOver()
		{
			Main.HoverItem = this._infoDisplayItem;
			Main.instance.MouseText("", 0, 0, -1, -1, -1, -1, 0);
			Main.mouseText = true;
		}

		// Token: 0x06002DE7 RID: 11751 RVA: 0x005A73AC File Offset: 0x005A55AC
		public override int CompareTo(object obj)
		{
			IManuallyOrderedUIElement manuallyOrderedUIElement = obj as IManuallyOrderedUIElement;
			if (manuallyOrderedUIElement != null)
			{
				return this.OrderInUIList.CompareTo(manuallyOrderedUIElement.OrderInUIList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06002DE8 RID: 11752 RVA: 0x005A73E0 File Offset: 0x005A55E0
		private void SetBestiaryNotesOnItemCache(DropRateInfo info)
		{
			List<string> list = new List<string>();
			if (info.conditions == null)
			{
				return;
			}
			foreach (IProvideItemConditionDescription provideItemConditionDescription in info.conditions)
			{
				if (provideItemConditionDescription != null)
				{
					string conditionDescription = provideItemConditionDescription.GetConditionDescription();
					if (!string.IsNullOrWhiteSpace(conditionDescription))
					{
						list.Add(conditionDescription);
					}
				}
			}
			this._infoDisplayItem.BestiaryNotes = string.Join("\n", list);
		}

		// Token: 0x06002DE9 RID: 11753 RVA: 0x005A746C File Offset: 0x005A566C
		private void MouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			this.BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x005A748E File Offset: 0x005A568E
		private void MouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this.BorderColor = new Color(89, 116, 213, 255);
		}

		// Token: 0x0400550F RID: 21775
		[CompilerGenerated]
		private int <OrderInUIList>k__BackingField;

		// Token: 0x04005510 RID: 21776
		private Item _infoDisplayItem;

		// Token: 0x04005511 RID: 21777
		private bool _hideMouseOver;
	}
}
