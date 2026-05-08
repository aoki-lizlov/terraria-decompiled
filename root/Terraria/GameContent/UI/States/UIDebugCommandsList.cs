using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Testing.ChatCommands;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003AF RID: 943
	public class UIDebugCommandsList : UIState
	{
		// Token: 0x06002C34 RID: 11316 RVA: 0x005968C1 File Offset: 0x00594AC1
		public UIDebugCommandsList()
		{
			this.BuildPage();
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00009E46 File Offset: 0x00008046
		public override void OnDeactivate()
		{
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x005968E8 File Offset: 0x00594AE8
		private void BuildPage()
		{
			base.RemoveAllChildren();
			UIElement uielement = new UIElement();
			uielement.Width.Set(0f, 0.8f);
			uielement.MaxWidth.Set(800f, 0f);
			uielement.MinWidth.Set(600f, 0f);
			uielement.Top.Set(220f, 0f);
			uielement.Height.Set(-220f, 1f);
			uielement.HAlign = 0.5f;
			base.Append(uielement);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Set(0f, 1f);
			uipanel.Height.Set(-110f, 1f);
			uipanel.BackgroundColor = new Color(33, 43, 79) * 0.95f;
			uielement.Append(uipanel);
			UIWrappedSearchBar uiwrappedSearchBar = new UIWrappedSearchBar(delegate
			{
				UserInterface.ActiveInstance.SetState(this);
			}, null, UIWrappedSearchBar.ColorTheme.Blue)
			{
				Width = new StyleDimension(200f, 0f),
				Top = new StyleDimension(20f, 0f)
			};
			uiwrappedSearchBar.OnSearchContentsChanged += this.searchbar_OnSearchContentsChanged;
			uipanel.Append(uiwrappedSearchBar);
			this._commandsList.Width.Set(-25f, 1f);
			this._commandsList.Height.Set(-60f, 1f);
			this._commandsList.VAlign = 1f;
			this._commandsList.ListPadding = 5f;
			uipanel.Append(this._commandsList);
			UITextPanel<string> uitextPanel = new UITextPanel<string>("Debug Commands", 1f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Set(-33f, 0f);
			uitextPanel.SetPadding(13f);
			uitextPanel.BackgroundColor = new Color(73, 94, 171);
			uielement.Append(uitextPanel);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel2.Width.Set(-10f, 0.5f);
			uitextPanel2.Height.Set(50f, 0f);
			uitextPanel2.VAlign = 1f;
			uitextPanel2.HAlign = 0.5f;
			uitextPanel2.Top.Set(-45f, 0f);
			uitextPanel2.OnMouseOver += UIDebugCommandsList.FadedMouseOver;
			uitextPanel2.OnMouseOut += UIDebugCommandsList.FadedMouseOut;
			uitextPanel2.OnLeftClick += this.GoBackClick;
			uielement.Append(uitextPanel2);
			UIScrollbar uiscrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			uiscrollbar.SetView(100f, 1000f);
			uiscrollbar.Height.Set(0f, 1f);
			uiscrollbar.HAlign = 1f;
			uipanel.Append(uiscrollbar);
			this._commandsList.SetScrollbar(uiscrollbar);
			this.PopulateCommandsList();
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x00596BE4 File Offset: 0x00594DE4
		private void searchbar_OnSearchContentsChanged(string searchContents)
		{
			if (searchContents == null)
			{
				searchContents = string.Empty;
			}
			string text = searchContents.ToLowerInvariant().Trim();
			bool flag = string.IsNullOrWhiteSpace(text);
			this._commandsList.Clear();
			foreach (UIDebugCommandItem uidebugCommandItem in this._commands)
			{
				if (flag || UIDebugCommandsList.DoesCommandMatchSearch(text, uidebugCommandItem))
				{
					this._commandsList.Add(uidebugCommandItem);
				}
			}
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x00596C70 File Offset: 0x00594E70
		private static bool DoesCommandMatchSearch(string lowerContents, UIDebugCommandItem command)
		{
			IDebugCommand command2 = command.Command;
			return command2.Name.ToLowerInvariant().Contains(lowerContents) || (command2.Description != null && command2.Description.ToLowerInvariant().Contains(lowerContents)) || (command2.HelpText != null && command2.HelpText.ToLowerInvariant().Contains(lowerContents));
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x00596CD4 File Offset: 0x00594ED4
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			IngameFancyUI.Close(false);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x00596CDC File Offset: 0x00594EDC
		private static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x00587919 File Offset: 0x00585B19
		private static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x00596D34 File Offset: 0x00594F34
		private void PopulateCommandsList()
		{
			List<IDebugCommand> list = ChatManager.DebugCommands.Commands.ToList<IDebugCommand>();
			list.Sort((IDebugCommand x, IDebugCommand y) => StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name));
			int num = 0;
			foreach (IDebugCommand debugCommand in list)
			{
				UIDebugCommandItem uidebugCommandItem = new UIDebugCommandItem(debugCommand, num++);
				this._commands.Add(uidebugCommandItem);
				this._commandsList.Add(uidebugCommandItem);
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x00596DD4 File Offset: 0x00594FD4
		private static void DrawMouseOver()
		{
			Item item = new Item();
			item.SetDefaults(0, null);
			item.SetNameOverride("Dev Commands");
			item.type = 1;
			item.scale = 0f;
			item.rare = 10;
			Main.HoverItem = item;
			Main.instance.MouseText("", 0, 0, -1, -1, -1, -1, 0);
			Main.mouseText = true;
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x005839BA File Offset: 0x00581BBA
		[CompilerGenerated]
		private void <BuildPage>b__4_0()
		{
			UserInterface.ActiveInstance.SetState(this);
		}

		// Token: 0x040053E0 RID: 21472
		private readonly UIList _commandsList = new UIList();

		// Token: 0x040053E1 RID: 21473
		private readonly List<UIDebugCommandItem> _commands = new List<UIDebugCommandItem>();

		// Token: 0x02000910 RID: 2320
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004763 RID: 18275 RVA: 0x006CB555 File Offset: 0x006C9755
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004764 RID: 18276 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06004765 RID: 18277 RVA: 0x006CB561 File Offset: 0x006C9761
			internal int <PopulateCommandsList>b__10_0(IDebugCommand x, IDebugCommand y)
			{
				return StringComparer.OrdinalIgnoreCase.Compare(x.Name, y.Name);
			}

			// Token: 0x0400745F RID: 29791
			public static readonly UIDebugCommandsList.<>c <>9 = new UIDebugCommandsList.<>c();

			// Token: 0x04007460 RID: 29792
			public static Comparison<IDebugCommand> <>9__10_0;
		}
	}
}
