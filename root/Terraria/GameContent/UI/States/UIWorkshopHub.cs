using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003A4 RID: 932
	public class UIWorkshopHub : UIState, IHaveBackButtonCommand
	{
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06002AC5 RID: 10949 RVA: 0x005879DC File Offset: 0x00585BDC
		// (remove) Token: 0x06002AC6 RID: 10950 RVA: 0x00587A10 File Offset: 0x00585C10
		public static event Action OnWorkshopHubMenuOpened
		{
			[CompilerGenerated]
			add
			{
				Action action = UIWorkshopHub.OnWorkshopHubMenuOpened;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref UIWorkshopHub.OnWorkshopHubMenuOpened, action3, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = UIWorkshopHub.OnWorkshopHubMenuOpened;
				Action action2;
				do
				{
					action2 = action;
					Action action3 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref UIWorkshopHub.OnWorkshopHubMenuOpened, action3, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x00587A43 File Offset: 0x00585C43
		public UIWorkshopHub(UIState stateToGoBackTo)
		{
			this._previousUIState = stateToGoBackTo;
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x00587A52 File Offset: 0x00585C52
		public void EnterHub()
		{
			UIWorkshopHub.OnWorkshopHubMenuOpened();
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00587A60 File Offset: 0x00585C60
		public override void OnInitialize()
		{
			base.OnInitialize();
			int num = 20;
			int num2 = 250;
			int num3 = 50 + num * 2;
			int num4 = 600;
			int num5 = num4 - num2 - num3;
			UIElement uielement = new UIElement();
			uielement.Width.Set(600f, 0f);
			uielement.Top.Set((float)num2, 0f);
			uielement.Height.Set((float)(num4 - num2), 0f);
			uielement.HAlign = 0.5f;
			int num6 = 154;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set((float)num5, 0f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			UIElement uielement2 = new UIElement();
			uielement2.Width.Set(0f, 1f);
			uielement2.Height.Set((float)num6, 0f);
			uielement2.SetPadding(0f);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.WorkshopHub"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-46f, 0f);
			uitextPanel.SetPadding(15f);
			uitextPanel.BackgroundColor = new Color(73, 94, 171);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel2.Width.Set(-10f, 0.5f);
			uitextPanel2.Height.Set(50f, 0f);
			uitextPanel2.VAlign = 1f;
			uitextPanel2.HAlign = 0f;
			uitextPanel2.Top.Set((float)(-(float)num), 0f);
			uitextPanel2.OnMouseOver += this.FadedMouseOver;
			uitextPanel2.OnMouseOut += this.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.GoBackClick;
			uitextPanel2.SetSnapPoint("Back", 0, null, null);
			uielement.Append(uitextPanel2);
			this._buttonBack = uitextPanel2;
			UITextPanel<LocalizedText> uitextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("Workshop.ReportLogsButton"), 0.7f, true);
			uitextPanel3.Width.Set(-10f, 0.5f);
			uitextPanel3.Height.Set(50f, 0f);
			uitextPanel3.VAlign = 1f;
			uitextPanel3.HAlign = 1f;
			uitextPanel3.Top.Set((float)(-(float)num), 0f);
			uitextPanel3.OnMouseOver += this.FadedMouseOver;
			uitextPanel3.OnMouseOut += this.FadedMouseOut;
			uitextPanel3.OnLeftClick += this.GoLogsClick;
			uitextPanel3.SetSnapPoint("Logs", 0, null, null);
			uielement.Append(uitextPanel3);
			this._buttonLogs = uitextPanel3;
			UIElement uielement3 = this.MakeButton_OpenWorkshopWorldsImportMenu();
			uielement3.HAlign = 0f;
			uielement3.VAlign = 0f;
			uielement2.Append(uielement3);
			uielement3 = this.MakeButton_OpenUseResourcePacksMenu();
			uielement3.HAlign = 1f;
			uielement3.VAlign = 0f;
			uielement2.Append(uielement3);
			uielement3 = this.MakeButton_OpenPublishWorldsMenu();
			uielement3.HAlign = 0f;
			uielement3.VAlign = 1f;
			uielement2.Append(uielement3);
			uielement3 = this.MakeButton_OpenPublishResourcePacksMenu();
			uielement3.HAlign = 1f;
			uielement3.VAlign = 1f;
			uielement2.Append(uielement3);
			UIWorkshopHub.AddHorizontalSeparator(uipanel, (float)(num6 + 6 + 6));
			this.AddDescriptionPanel(uipanel, (float)(num6 + 8 + 6 + 6), (float)(num5 - num6 - 12 - 12 - 8), "desc");
			uipanel.Append(uielement2);
			uielement.Append(uipanel);
			uielement.Append(uitextPanel);
			base.Append(uielement);
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x00587E7C File Offset: 0x0058607C
		private UIElement MakeButton_OpenUseResourcePacksMenu()
		{
			UIElement uielement = this.MakeFancyButton("Images/UI/Workshop/HubResourcepacks", "Workshop.HubResourcePacks");
			uielement.OnLeftClick += this.Click_OpenResourcePacksMenu;
			this._buttonUseResourcePacks = uielement;
			return uielement;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x00587EB4 File Offset: 0x005860B4
		private void Click_OpenResourcePacksMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.OpenResourcePacksMenu(this);
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x00587ED4 File Offset: 0x005860D4
		private UIElement MakeButton_OpenWorkshopWorldsImportMenu()
		{
			UIElement uielement = this.MakeFancyButton("Images/UI/Workshop/HubWorlds", "Workshop.HubWorlds");
			uielement.OnLeftClick += this.Click_OpenWorldImportMenu;
			this._buttonImportWorlds = uielement;
			return uielement;
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x00587F0C File Offset: 0x0058610C
		private void Click_OpenWorldImportMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(new UIWorkshopWorldImport(this));
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x00587F34 File Offset: 0x00586134
		private UIElement MakeButton_OpenPublishResourcePacksMenu()
		{
			UIElement uielement = this.MakeFancyButton("Images/UI/Workshop/HubPublishResourcepacks", "Workshop.HubPublishResourcePacks");
			uielement.OnLeftClick += this.Click_OpenResourcePackPublishMenu;
			this._buttonPublishResourcePacks = uielement;
			return uielement;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x00587F6C File Offset: 0x0058616C
		private void Click_OpenResourcePackPublishMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(new UIWorkshopSelectResourcePackToPublish(this));
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x00587F94 File Offset: 0x00586194
		private UIElement MakeButton_OpenPublishWorldsMenu()
		{
			UIElement uielement = this.MakeFancyButton("Images/UI/Workshop/HubPublishWorlds", "Workshop.HubPublishWorlds");
			uielement.OnLeftClick += this.Click_OpenWorldPublishMenu;
			this._buttonPublishWorlds = uielement;
			return uielement;
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x00587FCC File Offset: 0x005861CC
		private void Click_OpenWorldPublishMenu(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(new UIWorkshopSelectWorldToPublish(this));
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x00587FF4 File Offset: 0x005861F4
		private static void AddHorizontalSeparator(UIElement Container, float accumualtedHeight)
		{
			UIHorizontalSeparator uihorizontalSeparator = new UIHorizontalSeparator(2, true)
			{
				Width = StyleDimension.FromPercent(1f),
				Top = StyleDimension.FromPixels(accumualtedHeight - 8f),
				Color = Color.Lerp(Color.White, new Color(63, 65, 151, 255), 0.85f) * 0.9f
			};
			Container.Append(uihorizontalSeparator);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x00588064 File Offset: 0x00586264
		public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			if (listeningElement == this._buttonUseResourcePacks)
			{
				localizedText = Language.GetText("Workshop.HubDescriptionUseResourcePacks");
			}
			if (listeningElement == this._buttonPublishResourcePacks)
			{
				localizedText = Language.GetText("Workshop.HubDescriptionPublishResourcePacks");
			}
			if (listeningElement == this._buttonImportWorlds)
			{
				localizedText = Language.GetText("Workshop.HubDescriptionImportWorlds");
			}
			if (listeningElement == this._buttonPublishWorlds)
			{
				localizedText = Language.GetText("Workshop.HubDescriptionPublishWorlds");
			}
			if (localizedText != null)
			{
				this._descriptionText.SetText(localizedText);
			}
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x005880D2 File Offset: 0x005862D2
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("Workshop.HubDescriptionDefault"));
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x005880EC File Offset: 0x005862EC
		private void AddDescriptionPanel(UIElement container, float accumulatedHeight, float height, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(height, 0f),
				Top = StyleDimension.FromPixels(2f)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			container.Append(uislicedImage);
			UIText uitext = new UIText(Language.GetText("Workshop.HubDescriptionDefault"), 1f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f)
			};
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 6f;
			uitext.IsWrapped = true;
			uislicedImage.Append(uitext);
			this._descriptionText = uitext;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00588238 File Offset: 0x00586438
		private UIElement MakeFancyButton(string iconImagePath, string textKey)
		{
			UIPanel uipanel = new UIPanel();
			int num = -3;
			int num2 = -3;
			uipanel.Width = StyleDimension.FromPixelsAndPercent((float)num, 0.5f);
			uipanel.Height = StyleDimension.FromPixelsAndPercent((float)num2, 0.5f);
			uipanel.OnMouseOver += this.SetColorsToHovered;
			uipanel.OnMouseOut += this.SetColorsToNotHovered;
			uipanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			uipanel.BorderColor = new Color(89, 116, 213) * 0.7f;
			uipanel.SetPadding(6f);
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>(iconImagePath, 1))
			{
				IgnoresMouseInteraction = true,
				VAlign = 0.5f
			};
			uiimage.Left.Set(2f, 0f);
			uipanel.Append(uiimage);
			uipanel.OnMouseOver += this.ShowOptionDescription;
			uipanel.OnMouseOut += this.ClearOptionDescription;
			UIText uitext = new UIText(Language.GetText(textKey), 0.45f, true)
			{
				HAlign = 0f,
				VAlign = 0.5f,
				Width = StyleDimension.FromPixelsAndPercent(-80f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(5f, 0f),
				Left = StyleDimension.FromPixels(80f),
				IgnoresMouseInteraction = true,
				TextOriginX = 0f,
				TextOriginY = 0f
			};
			uitext.PaddingLeft = 0f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 10f;
			uitext.IsWrapped = true;
			uipanel.Append(uitext);
			uipanel.SetSnapPoint("Button", 0, null, null);
			return uipanel;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x00588426 File Offset: 0x00586626
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00588443 File Offset: 0x00586643
		private void GoLogsClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Main.IssueReporterIndicator.Hide();
			Main.OpenReportsMenu();
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0058846C File Offset: 0x0058666C
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0058539D File Offset: 0x0058359D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x005884C1 File Offset: 0x005866C1
		private void SetColorsToHovered(UIMouseEvent evt, UIElement listeningElement)
		{
			UIPanel uipanel = (UIPanel)evt.Target;
			uipanel.BackgroundColor = new Color(73, 94, 171);
			uipanel.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x005884F8 File Offset: 0x005866F8
		private void SetColorsToNotHovered(UIMouseEvent evt, UIElement listeningElement)
		{
			UIPanel uipanel = (UIPanel)evt.Target;
			uipanel.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			uipanel.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0058854B File Offset: 0x0058674B
		public void HandleBackButtonUsage()
		{
			if (this._previousUIState == null)
			{
				Main.menuMode = 0;
				return;
			}
			Main.menuMode = 888;
			Main.MenuUI.SetState(this._previousUIState);
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0058858B File Offset: 0x0058678B
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0058859C File Offset: 0x0058679C
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int num2 = num;
			UILinkPoint linkPoint = this._helper.GetLinkPoint(num2++, this._buttonUseResourcePacks);
			UILinkPoint linkPoint2 = this._helper.GetLinkPoint(num2++, this._buttonPublishResourcePacks);
			UILinkPoint linkPoint3 = this._helper.GetLinkPoint(num2++, this._buttonImportWorlds);
			UILinkPoint linkPoint4 = this._helper.GetLinkPoint(num2++, this._buttonPublishWorlds);
			UILinkPoint linkPoint5 = this._helper.GetLinkPoint(num2++, this._buttonBack);
			UILinkPoint linkPoint6 = this._helper.GetLinkPoint(num2++, this._buttonLogs);
			this._helper.PairLeftRight(linkPoint3, linkPoint);
			this._helper.PairLeftRight(linkPoint4, linkPoint2);
			this._helper.PairLeftRight(linkPoint5, linkPoint6);
			this._helper.PairUpDown(linkPoint3, linkPoint4);
			this._helper.PairUpDown(linkPoint, linkPoint2);
			this._helper.PairUpDown(linkPoint4, linkPoint5);
			this._helper.PairUpDown(linkPoint2, linkPoint6);
			this._helper.MoveToVisuallyClosestPoint(num, num2);
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x005886B6 File Offset: 0x005868B6
		// Note: this type is marked as 'beforefieldinit'.
		static UIWorkshopHub()
		{
		}

		// Token: 0x04005340 RID: 21312
		[CompilerGenerated]
		private static Action OnWorkshopHubMenuOpened = delegate
		{
		};

		// Token: 0x04005341 RID: 21313
		private UIState _previousUIState;

		// Token: 0x04005342 RID: 21314
		private UIText _descriptionText;

		// Token: 0x04005343 RID: 21315
		private UIElement _buttonUseResourcePacks;

		// Token: 0x04005344 RID: 21316
		private UIElement _buttonPublishResourcePacks;

		// Token: 0x04005345 RID: 21317
		private UIElement _buttonImportWorlds;

		// Token: 0x04005346 RID: 21318
		private UIElement _buttonPublishWorlds;

		// Token: 0x04005347 RID: 21319
		private UIElement _buttonBack;

		// Token: 0x04005348 RID: 21320
		private UIElement _buttonLogs;

		// Token: 0x04005349 RID: 21321
		private UIGamepadHelper _helper;

		// Token: 0x020008F9 RID: 2297
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004732 RID: 18226 RVA: 0x006CB3B4 File Offset: 0x006C95B4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004733 RID: 18227 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004734 RID: 18228 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <.cctor>b__37_0()
			{
			}

			// Token: 0x04007409 RID: 29705
			public static readonly UIWorkshopHub.<>c <>9 = new UIWorkshopHub.<>c();
		}
	}
}
