using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003CC RID: 972
	public class UICreativeInfiniteItemsDisplay : UIElement
	{
		// Token: 0x06002D7A RID: 11642 RVA: 0x005A3FC4 File Offset: 0x005A21C4
		public UICreativeInfiniteItemsDisplay()
		{
			this._filterer = new EntryFilterer<Item, IItemEntryFilter>();
			List<IItemEntryFilter> list = new List<IItemEntryFilter>
			{
				new ItemFilters.Weapon(),
				new ItemFilters.Armor(),
				new ItemFilters.Vanity(),
				new ItemFilters.BuildingBlock(),
				new ItemFilters.Furniture(),
				new ItemFilters.Accessories(),
				new ItemFilters.MiscAccessories(),
				new ItemFilters.Consumables(),
				new ItemFilters.Tools(),
				new ItemFilters.Materials()
			};
			List<IItemEntryFilter> list2 = new List<IItemEntryFilter>();
			list2.AddRange(list);
			list2.Add(new ItemFilters.MiscFallback(list));
			this._filterer.AddFilters(list2);
			this._filterer.SetSearchFilterObject<ItemFilters.BySearch>(new ItemFilters.BySearch());
			this._sorter = new EntrySorter<Item, ICreativeItemSortStep>();
			this._sorter.AddSortSteps(new List<ICreativeItemSortStep>
			{
				new SortingSteps.ByUnlockStatus(),
				new SortingSteps.ByCreativeSortingId(),
				new SortingSteps.Alphabetical()
			});
			this.BuildPage();
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x005A4100 File Offset: 0x005A2300
		private void BuildPage()
		{
			this._lastCheckedVersionForEdits = -1;
			base.RemoveAllChildren();
			base.SetPadding(0f);
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill
			};
			uielement.SetPadding(0f);
			this._containerInfinites = uielement;
			UIElement uielement2 = new UIElement
			{
				Width = StyleDimension.Fill,
				Height = StyleDimension.Fill
			};
			uielement2.SetPadding(0f);
			this._containerSacrifice = uielement2;
			this.BuildInfinitesMenuContents(uielement);
			this.BuildSacrificeMenuContents(uielement2);
			this.UpdateContents();
			base.OnUpdate += this.UICreativeInfiniteItemsDisplay_OnUpdate;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x005A41A7 File Offset: 0x005A23A7
		private void Hover_OnUpdate(UIElement affectedElement)
		{
			if (this._hovered)
			{
				Main.LocalPlayer.mouseInterface = true;
			}
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x005A41BC File Offset: 0x005A23BC
		private void Hover_OnMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = false;
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x005A41C5 File Offset: 0x005A23C5
		private void Hover_OnMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._hovered = true;
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x005A41CE File Offset: 0x005A23CE
		private static UIPanel CreateBasicPanel()
		{
			UIPanel uipanel = new UIPanel();
			UICreativeInfiniteItemsDisplay.SetBasicSizesForCreativeSacrificeOrInfinitesPanel(uipanel);
			uipanel.BackgroundColor *= 0.8f;
			uipanel.BorderColor *= 0.8f;
			return uipanel;
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x005A4208 File Offset: 0x005A2408
		private static void SetBasicSizesForCreativeSacrificeOrInfinitesPanel(UIElement element)
		{
			element.Width = new StyleDimension(0f, 1f);
			element.Height = new StyleDimension(-38f, 1f);
			element.Top = new StyleDimension(38f, 0f);
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x005A4254 File Offset: 0x005A2454
		private void BuildInfinitesMenuContents(UIElement totalContainer)
		{
			UIPanel uipanel = UICreativeInfiniteItemsDisplay.CreateBasicPanel();
			totalContainer.Append(uipanel);
			uipanel.OnUpdate += this.Hover_OnUpdate;
			uipanel.OnMouseOver += this.Hover_OnMouseOver;
			uipanel.OnMouseOut += this.Hover_OnMouseOut;
			this._itemGrid = new UICreativeItemGrid();
			UIWrappedSearchBar uiwrappedSearchBar = new UIWrappedSearchBar(new Action(this.GoBackFromVirtualKeyboard), null, UIWrappedSearchBar.ColorTheme.Blue);
			uiwrappedSearchBar.CustomOpenVirtualKeyboard = new Action<UIState>(IngameFancyUI.OpenUIState);
			uiwrappedSearchBar.OnSearchContentsChanged += this.OnSearchContentsChanged;
			uiwrappedSearchBar.SetSearchSnapPoint("CreativeInfinitesSearch", 0, null, null);
			uipanel.Append(uiwrappedSearchBar);
			UIList uilist = new UIList
			{
				Width = new StyleDimension(-25f, 1f),
				Height = new StyleDimension(-28f, 1f),
				VAlign = 1f,
				HAlign = 0f
			};
			uipanel.Append(uilist);
			float num = 4f;
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue)
			{
				Height = new StyleDimension(-28f - num * 2f, 1f),
				Top = new StyleDimension(-num, 0f),
				VAlign = 1f,
				HAlign = 1f
			};
			uipanel.Append(uiscrollbar);
			uilist.SetScrollbar(uiscrollbar);
			uilist.Add(this._itemGrid);
			UICreativeItemsInfiniteFilteringOptions uicreativeItemsInfiniteFilteringOptions = new UICreativeItemsInfiniteFilteringOptions(this._filterer, "CreativeInfinitesFilter", UICreativeItemsInfiniteFilteringOptions.ColorTheme.Blue);
			uicreativeItemsInfiniteFilteringOptions.OnClickingOption += this.filtersHelper_OnClickingOption;
			uicreativeItemsInfiniteFilteringOptions.Left = new StyleDimension(20f, 0f);
			totalContainer.Append(uicreativeItemsInfiniteFilteringOptions);
			uicreativeItemsInfiniteFilteringOptions.OnUpdate += this.Hover_OnUpdate;
			uicreativeItemsInfiniteFilteringOptions.OnMouseOver += this.Hover_OnMouseOver;
			uicreativeItemsInfiniteFilteringOptions.OnMouseOut += this.Hover_OnMouseOut;
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x005A4448 File Offset: 0x005A2648
		private void BuildSacrificeMenuContents(UIElement totalContainer)
		{
			UIPanel uipanel = UICreativeInfiniteItemsDisplay.CreateBasicPanel();
			uipanel.VAlign = 0.5f;
			uipanel.Height = new StyleDimension(170f, 0f);
			uipanel.Width = new StyleDimension(170f, 0f);
			uipanel.Top = default(StyleDimension);
			totalContainer.Append(uipanel);
			uipanel.OnUpdate += this.Hover_OnUpdate;
			uipanel.OnMouseOver += this.Hover_OnMouseOver;
			uipanel.OnMouseOut += this.Hover_OnMouseOut;
			this.AddCogsForSacrificeMenu(uipanel);
			this._pistonParticleAsset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Spark", 1);
			float num = 0f;
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Slots", 1))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = new StyleDimension(-20f, 0f),
				Left = new StyleDimension(num, 0f)
			};
			uipanel.Append(uiimage);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_FramedPistons", 1);
			UIImageFramed uiimageFramed = new UIImageFramed(asset, asset.Frame(1, 9, 0, 0, 0, 0))
			{
				HAlign = 0.5f,
				VAlign = 0.5f,
				Top = new StyleDimension(-20f, 0f),
				Left = new StyleDimension(num, 0f),
				IgnoresMouseInteraction = true
			};
			uipanel.Append(uiimageFramed);
			this._sacrificePistons = uiimageFramed;
			UIParticleLayer uiparticleLayer = new UIParticleLayer
			{
				Width = new StyleDimension(0f, 1f),
				Height = new StyleDimension(0f, 1f),
				AnchorPositionOffsetByPercents = Vector2.One / 2f,
				AnchorPositionOffsetByPixels = Vector2.Zero
			};
			this._pistonParticleSystem = uiparticleLayer;
			uiimageFramed.Append(this._pistonParticleSystem);
			UIElement uielement = Main.CreativeMenu.ProvideItemSlotElement(0);
			uielement.HAlign = 0.5f;
			uielement.VAlign = 0.5f;
			uielement.Top = new StyleDimension(-15f, 0f);
			uielement.Left = new StyleDimension(num, 0f);
			uielement.SetSnapPoint("CreativeSacrificeSlot", 0, null, null);
			uiimage.Append(uielement);
			UIText uitext = new UIText("(0/50)", 0.8f, false)
			{
				Top = new StyleDimension(10f, 0f),
				Left = new StyleDimension(num, 0f),
				HAlign = 0.5f,
				VAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			uitext.OnUpdate += this.descriptionText_OnUpdate;
			uipanel.Append(uitext);
			UIPanel uipanel2 = new UIPanel
			{
				Top = new StyleDimension(0f, 0f),
				Left = new StyleDimension(num, 0f),
				HAlign = 0.5f,
				VAlign = 1f,
				Width = new StyleDimension(124f, 0f),
				Height = new StyleDimension(30f, 0f)
			};
			UIText uitext2 = new UIText(Language.GetText("CreativePowers.ConfirmInfiniteItemSacrifice"), 0.8f, false)
			{
				IgnoresMouseInteraction = true,
				HAlign = 0.5f,
				VAlign = 0.5f
			};
			uipanel2.Append(uitext2);
			uipanel2.SetSnapPoint("CreativeSacrificeConfirm", 0, null, null);
			uipanel2.OnLeftClick += this.sacrificeButton_OnClick;
			uipanel2.OnMouseOver += this.FadedMouseOver;
			uipanel2.OnMouseOut += this.FadedMouseOut;
			uipanel2.OnUpdate += this.research_OnUpdate;
			uipanel.Append(uipanel2);
			uipanel.OnUpdate += this.sacrificeWindow_OnUpdate;
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x005A4848 File Offset: 0x005A2A48
		private void research_OnUpdate(UIElement affectedElement)
		{
			if (affectedElement.IsMouseHovering)
			{
				Main.instance.MouseTextNoOverride(Language.GetTextValue("CreativePowers.ResearchButtonTooltip"), 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x005A4878 File Offset: 0x005A2A78
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x005A48A4 File Offset: 0x005A2AA4
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x005A48BC File Offset: 0x005A2ABC
		private void AddCogsForSacrificeMenu(UIElement sacrificesContainer)
		{
			UIElement uielement = new UIElement();
			uielement.IgnoresMouseInteraction = true;
			UICreativeInfiniteItemsDisplay.SetBasicSizesForCreativeSacrificeOrInfinitesPanel(uielement);
			uielement.VAlign = 0.5f;
			uielement.Height = new StyleDimension(170f, 0f);
			uielement.Width = new StyleDimension(280f, 0f);
			uielement.Top = default(StyleDimension);
			uielement.SetPadding(0f);
			sacrificesContainer.Append(uielement);
			Vector2 vector = new Vector2(-10f, -10f);
			this.AddSymetricalCogsPair(uielement, new Vector2(22f, 1f) + vector, "Images/UI/Creative/Research_GearC", this._sacrificeCogsSmall);
			this.AddSymetricalCogsPair(uielement, new Vector2(1f, 28f) + vector, "Images/UI/Creative/Research_GearB", this._sacrificeCogsMedium);
			this.AddSymetricalCogsPair(uielement, new Vector2(5f, 5f) + vector, "Images/UI/Creative/Research_GearA", this._sacrificeCogsBig);
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x005A49B5 File Offset: 0x005A2BB5
		private void sacrificeWindow_OnUpdate(UIElement affectedElement)
		{
			this.UpdateVisualFrame();
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x005A49C0 File Offset: 0x005A2BC0
		private void UpdateVisualFrame()
		{
			float num = 0.05f;
			float sacrificeAnimationProgress = this.GetSacrificeAnimationProgress();
			float lerpValue = Utils.GetLerpValue(1f, 0.7f, sacrificeAnimationProgress, true);
			float num2 = lerpValue * lerpValue;
			num2 *= 2f;
			float num3 = 1f + num2;
			num *= num3;
			float num4 = 2f;
			float num5 = 1.1428572f;
			float num6 = 1f;
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs(num4 * num, this._sacrificeCogsSmall);
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs(num5 * num, this._sacrificeCogsMedium);
			UICreativeInfiniteItemsDisplay.OffsetRotationsForCogs(-num6 * num, this._sacrificeCogsBig);
			int num7 = 0;
			if (this._sacrificeAnimationTimeLeft != 0)
			{
				float num8 = 0.1f;
				float num9 = 0.06666667f;
				if (sacrificeAnimationProgress >= 1f - num8)
				{
					num7 = 8;
				}
				else if (sacrificeAnimationProgress >= 1f - num8 * 2f)
				{
					num7 = 7;
				}
				else if (sacrificeAnimationProgress >= 1f - num8 * 3f)
				{
					num7 = 6;
				}
				else if (sacrificeAnimationProgress >= num9 * 4f)
				{
					num7 = 5;
				}
				else if (sacrificeAnimationProgress >= num9 * 3f)
				{
					num7 = 4;
				}
				else if (sacrificeAnimationProgress >= num9 * 2f)
				{
					num7 = 3;
				}
				else if (sacrificeAnimationProgress >= num9)
				{
					num7 = 2;
				}
				else
				{
					num7 = 1;
				}
				if (this._sacrificeAnimationTimeLeft == 56)
				{
					SoundEngine.PlaySound(63, -1, -1, 1, 1f, 0f);
					Vector2 vector = new Vector2(0f, 0.16350001f);
					for (int i = 0; i < 15; i++)
					{
						Vector2 vector2 = Main.rand.NextVector2Circular(4f, 3f);
						if (vector2.Y > 0f)
						{
							vector2.Y = -vector2.Y;
						}
						vector2.Y -= 2f;
						this._pistonParticleSystem.AddParticle(new CreativeSacrificeParticle(this._pistonParticleAsset, null, vector2, Vector2.Zero)
						{
							AccelerationPerFrame = vector,
							ScaleOffsetPerFrame = -0.016666668f
						});
					}
				}
				if (this._sacrificeAnimationTimeLeft == 40 && this._researchComplete)
				{
					this._researchComplete = false;
					SoundEngine.PlaySound(64, -1, -1, 1, 1f, 0f);
				}
			}
			this._sacrificePistons.SetFrame(1, 9, 0, num7, 0, 0);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x005A4BE0 File Offset: 0x005A2DE0
		private static void OffsetRotationsForCogs(float rotationOffset, List<UIImage> cogsList)
		{
			cogsList[0].Rotation += rotationOffset;
			cogsList[1].Rotation -= rotationOffset;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x005A4C0C File Offset: 0x005A2E0C
		private void AddSymetricalCogsPair(UIElement sacrificesContainer, Vector2 cogOFfsetsInPixels, string assetPath, List<UIImage> imagesList)
		{
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(assetPath, 1);
			cogOFfsetsInPixels += -asset.Size() / 2f;
			UIImage uiimage = new UIImage(asset)
			{
				NormalizedOrigin = Vector2.One / 2f,
				Left = new StyleDimension(cogOFfsetsInPixels.X, 0f),
				Top = new StyleDimension(cogOFfsetsInPixels.Y, 0f)
			};
			imagesList.Add(uiimage);
			sacrificesContainer.Append(uiimage);
			uiimage = new UIImage(asset)
			{
				NormalizedOrigin = Vector2.One / 2f,
				HAlign = 1f,
				Left = new StyleDimension(-cogOFfsetsInPixels.X, 0f),
				Top = new StyleDimension(cogOFfsetsInPixels.Y, 0f)
			};
			imagesList.Add(uiimage);
			sacrificesContainer.Append(uiimage);
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x005A4D00 File Offset: 0x005A2F00
		private void descriptionText_OnUpdate(UIElement affectedElement)
		{
			UIText uitext = affectedElement as UIText;
			int num;
			int num2;
			int num3;
			bool sacrificeNumbers = Main.CreativeMenu.GetSacrificeNumbers(out num, out num2, out num3);
			Main.CreativeMenu.ShouldDrawSacrificeArea();
			if (!Main.mouseItem.IsAir)
			{
				this.ForgetItemSacrifice();
			}
			if (num == 0)
			{
				if (this._lastItemIdSacrificed != 0 && this._lastItemAmountWeNeededTotal != this._lastItemAmountWeHad)
				{
					uitext.SetText(string.Format("({0}/{1})", this._lastItemAmountWeHad, this._lastItemAmountWeNeededTotal));
					return;
				}
				uitext.SetText("???");
				return;
			}
			else
			{
				this.ForgetItemSacrifice();
				if (!sacrificeNumbers)
				{
					uitext.SetText("X");
					return;
				}
				uitext.SetText(string.Format("({0}/{1})", num2, num3));
				return;
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x005A4DC1 File Offset: 0x005A2FC1
		private void sacrificeButton_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.SacrificeWhatYouCan();
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x005A4DCC File Offset: 0x005A2FCC
		public void SacrificeWhatYouCan()
		{
			int num;
			int num2;
			int num3;
			Main.CreativeMenu.GetSacrificeNumbers(out num, out num2, out num3);
			int num4;
			CreativeUI.ItemSacrificeResult itemSacrificeResult = Main.CreativeMenu.SacrificeItem(out num4);
			if (itemSacrificeResult != CreativeUI.ItemSacrificeResult.SacrificedButNotDone)
			{
				if (itemSacrificeResult == CreativeUI.ItemSacrificeResult.SacrificedAndDone)
				{
					this._researchComplete = true;
					this.BeginSacrificeAnimation();
					this.RememberItemSacrifice(num, num2 + num4, num3);
					return;
				}
			}
			else
			{
				this._researchComplete = false;
				this.BeginSacrificeAnimation();
				this.RememberItemSacrifice(num, num2 + num4, num3);
			}
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x005A4E33 File Offset: 0x005A3033
		public void StopPlayingAnimation()
		{
			this.ForgetItemSacrifice();
			this._sacrificeAnimationTimeLeft = 0;
			this._pistonParticleSystem.ClearParticles();
			this.UpdateVisualFrame();
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x005A4E53 File Offset: 0x005A3053
		private void RememberItemSacrifice(int itemId, int amountWeHave, int amountWeNeedTotal)
		{
			this._lastItemIdSacrificed = itemId;
			this._lastItemAmountWeHad = amountWeHave;
			this._lastItemAmountWeNeededTotal = amountWeNeedTotal;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x005A4E6A File Offset: 0x005A306A
		private void ForgetItemSacrifice()
		{
			this._lastItemIdSacrificed = 0;
			this._lastItemAmountWeHad = 0;
			this._lastItemAmountWeNeededTotal = 0;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x005A4E81 File Offset: 0x005A3081
		private void BeginSacrificeAnimation()
		{
			this._sacrificeAnimationTimeLeft = 60;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x005A4E8B File Offset: 0x005A308B
		private void UpdateSacrificeAnimation()
		{
			if (this._sacrificeAnimationTimeLeft > 0)
			{
				this._sacrificeAnimationTimeLeft--;
			}
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x005A4EA4 File Offset: 0x005A30A4
		private float GetSacrificeAnimationProgress()
		{
			return Utils.GetLerpValue(60f, 0f, (float)this._sacrificeAnimationTimeLeft, true);
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x005A4EBD File Offset: 0x005A30BD
		public void SetPageTypeToShow(UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage page)
		{
			this._showSacrificesInsteadOfInfinites = page == UICreativeInfiniteItemsDisplay.InfiniteItemsDisplayPage.InfiniteItemsResearch;
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x005A4ECC File Offset: 0x005A30CC
		private void UICreativeInfiniteItemsDisplay_OnUpdate(UIElement affectedElement)
		{
			base.RemoveAllChildren();
			CreativeUnlocksTracker localPlayerCreativeTracker = Main.LocalPlayerCreativeTracker;
			if (this._lastTrackerCheckedForEdits != localPlayerCreativeTracker)
			{
				this._lastTrackerCheckedForEdits = localPlayerCreativeTracker;
				this._lastCheckedVersionForEdits = -1;
			}
			int lastEditId = localPlayerCreativeTracker.ItemSacrifices.LastEditId;
			if (this._lastCheckedVersionForEdits != lastEditId)
			{
				this._lastCheckedVersionForEdits = lastEditId;
				this.UpdateContents();
			}
			if (this._showSacrificesInsteadOfInfinites)
			{
				base.Append(this._containerSacrifice);
			}
			else
			{
				base.Append(this._containerInfinites);
			}
			this.UpdateSacrificeAnimation();
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x005A4F46 File Offset: 0x005A3146
		private void filtersHelper_OnClickingOption()
		{
			this.UpdateContents();
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x005A4F50 File Offset: 0x005A3150
		private void UpdateContents()
		{
			this._itemList.Clear();
			Main.LocalPlayerCreativeTracker.ItemSacrifices.ForEachItemWithResearchProgress(delegate(int type)
			{
				Item item = ContentSamples.ItemsByType[type];
				if (this._filterer.FitsFilter(item))
				{
					this._itemList.Add(item);
				}
			});
			this._itemList.Sort(this._sorter);
			this._itemGrid.SetContentsToShow(this._itemList);
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x005A4FA5 File Offset: 0x005A31A5
		private void OnSearchContentsChanged(string contents)
		{
			this._filterer.SetSearchFilter(contents);
			this.UpdateContents();
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x005A4FBC File Offset: 0x005A31BC
		private static UserInterface GetCurrentInterface()
		{
			UserInterface userInterface = UserInterface.ActiveInstance;
			if (Main.gameMenu)
			{
				userInterface = Main.MenuUI;
			}
			else
			{
				userInterface = Main.InGameUI;
			}
			return userInterface;
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x005A4FE5 File Offset: 0x005A31E5
		private void GoBackFromVirtualKeyboard()
		{
			IngameFancyUI.Close(true);
			Main.playerInventory = true;
			Main.CreativeMenu.ResumeMenuFromGamepadSearch();
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x005A4FFD File Offset: 0x005A31FD
		public int GetItemsPerLine()
		{
			return this._itemGrid.GetItemsPerLine();
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x005A500C File Offset: 0x005A320C
		[CompilerGenerated]
		private void <UpdateContents>b__55_0(int type)
		{
			Item item = ContentSamples.ItemsByType[type];
			if (this._filterer.FitsFilter(item))
			{
				this._itemList.Add(item);
			}
		}

		// Token: 0x040054CF RID: 21711
		private CreativeUnlocksTracker _lastTrackerCheckedForEdits;

		// Token: 0x040054D0 RID: 21712
		private int _lastCheckedVersionForEdits = -1;

		// Token: 0x040054D1 RID: 21713
		private UICreativeItemGrid _itemGrid;

		// Token: 0x040054D2 RID: 21714
		private EntryFilterer<Item, IItemEntryFilter> _filterer;

		// Token: 0x040054D3 RID: 21715
		private EntrySorter<Item, ICreativeItemSortStep> _sorter;

		// Token: 0x040054D4 RID: 21716
		private UIElement _containerInfinites;

		// Token: 0x040054D5 RID: 21717
		private UIElement _containerSacrifice;

		// Token: 0x040054D6 RID: 21718
		private bool _showSacrificesInsteadOfInfinites;

		// Token: 0x040054D7 RID: 21719
		public const string SnapPointName_SacrificeSlot = "CreativeSacrificeSlot";

		// Token: 0x040054D8 RID: 21720
		public const string SnapPointName_SacrificeConfirmButton = "CreativeSacrificeConfirm";

		// Token: 0x040054D9 RID: 21721
		public const string SnapPointName_InfinitesFilter = "CreativeInfinitesFilter";

		// Token: 0x040054DA RID: 21722
		public const string SnapPointName_InfinitesSearch = "CreativeInfinitesSearch";

		// Token: 0x040054DB RID: 21723
		private List<UIImage> _sacrificeCogsSmall = new List<UIImage>();

		// Token: 0x040054DC RID: 21724
		private List<UIImage> _sacrificeCogsMedium = new List<UIImage>();

		// Token: 0x040054DD RID: 21725
		private List<UIImage> _sacrificeCogsBig = new List<UIImage>();

		// Token: 0x040054DE RID: 21726
		private UIImageFramed _sacrificePistons;

		// Token: 0x040054DF RID: 21727
		private UIParticleLayer _pistonParticleSystem;

		// Token: 0x040054E0 RID: 21728
		private Asset<Texture2D> _pistonParticleAsset;

		// Token: 0x040054E1 RID: 21729
		private int _sacrificeAnimationTimeLeft;

		// Token: 0x040054E2 RID: 21730
		private bool _researchComplete;

		// Token: 0x040054E3 RID: 21731
		private bool _hovered;

		// Token: 0x040054E4 RID: 21732
		private int _lastItemIdSacrificed;

		// Token: 0x040054E5 RID: 21733
		private int _lastItemAmountWeHad;

		// Token: 0x040054E6 RID: 21734
		private int _lastItemAmountWeNeededTotal;

		// Token: 0x040054E7 RID: 21735
		private List<Item> _itemList = new List<Item>();

		// Token: 0x02000926 RID: 2342
		public enum InfiniteItemsDisplayPage
		{
			// Token: 0x040074F5 RID: 29941
			InfiniteItemsPickup,
			// Token: 0x040074F6 RID: 29942
			InfiniteItemsResearch
		}
	}
}
