using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using ReLogic.Content;
using ReLogic.Graphics;
using ReLogic.Threading;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Testing;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200039D RID: 925
	public class UIWorldGenDebug : UIState
	{
		// Token: 0x06002A2E RID: 10798 RVA: 0x00581BE1 File Offset: 0x0057FDE1
		private static void SetButtonState(UIWorldGenDebug.UIImageButtonWithExtraIcon button, UIWorldGenDebug.ButtonState state)
		{
			if (state == UIWorldGenDebug.ButtonState.Enabled)
			{
				button.SetVisibility(1f, 0.4f);
			}
			else if (state == UIWorldGenDebug.ButtonState.NotVisible)
			{
				button.SetVisibility(0f, 0f);
			}
			button.IgnoresMouseInteraction = state > UIWorldGenDebug.ButtonState.Enabled;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x00581C16 File Offset: 0x0057FE16
		public static UIWorldGenDebug ActiveInstance
		{
			get
			{
				return UserInterface.ActiveInstance.CurrentState as UIWorldGenDebug;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x00581C27 File Offset: 0x0057FE27
		public static bool IsActive
		{
			get
			{
				return (Main.gameMenu ? Main.MenuUI.CurrentState : Main.InGameUI.CurrentState) is UIWorldGenDebug;
			}
		}

		// Token: 0x06002A31 RID: 10801 RVA: 0x00581C4E File Offset: 0x0057FE4E
		public static void Open()
		{
			if (Main.gameMenu)
			{
				Main.MenuUI.SetState(new UIWorldGenDebug());
				return;
			}
			IngameFancyUI.OpenUIState(new UIWorldGenDebug());
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x00581C71 File Offset: 0x0057FE71
		public static void Close()
		{
			if (UIWorldGenDebug.ActiveInstance == null)
			{
				return;
			}
			if (Main.gameMenu)
			{
				Main.MenuUI.SetState(new UIWorldLoad());
				return;
			}
			IngameFancyUI.Close(false);
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06002A33 RID: 10803 RVA: 0x00581C98 File Offset: 0x0057FE98
		private static WorldGenerator.Controller Controller
		{
			get
			{
				return WorldGenerator.CurrentController;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x00581C9F File Offset: 0x0057FE9F
		private static bool CanSubmitActions
		{
			get
			{
				return UIWorldGenDebug.Controller.Paused && UIWorldGenDebug.Controller.CurrentPass == null;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06002A35 RID: 10805 RVA: 0x00581CBC File Offset: 0x0057FEBC
		public static GenPass CurrentTargetOrLatestPass
		{
			get
			{
				GenPass genPass = UIWorldGenDebug.Controller.PauseAfterPass;
				if (genPass == null)
				{
					genPass = UIWorldGenDebug.Controller.CurrentPass;
				}
				if (genPass == null)
				{
					genPass = UIWorldGenDebug.Controller.LastCompletedPass;
				}
				return genPass;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x00581CF4 File Offset: 0x0057FEF4
		public static GenPass CurrentTargetPass
		{
			get
			{
				GenPass genPass = UIWorldGenDebug.Controller.PauseAfterPass;
				if (genPass == UIWorldGenDebug.Controller.LastCompletedPass)
				{
					genPass = null;
				}
				return genPass;
			}
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00581D1C File Offset: 0x0057FF1C
		public UIWorldGenDebug()
		{
			this.NoGamepadSupport = true;
			this.IgnoresMouseInteraction = true;
			UIGenProgressBar progressBar = new UIGenProgressBar
			{
				VAlign = 0f,
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(20f),
				IgnoresMouseInteraction = true
			};
			base.Append(progressBar);
			UIHeader progressMessage = new UIHeader
			{
				VAlign = 0f,
				HAlign = 0.5f,
				IgnoresMouseInteraction = true
			};
			base.Append(progressMessage);
			base.OnUpdate += delegate(UIElement e)
			{
				progressBar.SetProgress((float)WorldGenerator.CurrentGenerationProgress.TotalProgress, (float)WorldGenerator.CurrentGenerationProgress.Value);
				progressMessage.Text = WorldGenerator.CurrentGenerationProgress.Message;
				if (WorldGenerator.CurrentController.QueuedAbort)
				{
					progressMessage.Text = Language.GetTextValue("UI.Canceling");
				}
				if (WorldGen.Manifest.GenPassResults.Count != this.LassPassIndex)
				{
					this.LassPassIndex = WorldGen.Manifest.GenPassResults.Count;
					this.EnsurePassVisible(this.LassPassIndex);
				}
			};
			this.controlListArea = new UIElement
			{
				Width = StyleDimension.FromPixels(450f),
				Height = StyleDimension.FromPixelsAndPercent(-60f, 1f),
				Top = StyleDimension.FromPixels(30f),
				Left = StyleDimension.FromPixels(10f)
			};
			base.Append(this.controlListArea);
			this.controlPanel = new UIPanel
			{
				Height = StyleDimension.FromPixels(50f)
			};
			this.controlPanel.SetPadding(8f);
			this.controlPanel.BackgroundColor = new Color(73, 94, 171) * 0.9f;
			UIElement uielement = this.AddButton(this.controlPanel, "Images/UI/Camera_0", delegate
			{
				UIWorldGenDebug.Controller.DeleteAllSnapshots();
			}, () => "Delete all snapshots", () => string.Concat(new object[]
			{
				"Click to clear all snapshots\nEstimated Disk Usage: ",
				WorldGenSnapshot.EstimatedDiskUsage / 1024L / 1024L,
				"MB",
				UIWorldGenDebug.CanSubmitActions ? "" : "\n[c/FFA500:Must be paused to manipulate snapshots]"
			}));
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>("Images/CoolDown", 1))
			{
				ScaleToFit = true,
				Width = new StyleDimension(28f, 0f),
				Height = new StyleDimension(28f, 0f),
				Left = new StyleDimension(3f, 0f),
				Top = new StyleDimension(3f, 0f)
			};
			uielement.Append(uiimage);
			GroupOptionButton<bool> groupOptionButton = this.AddButton(this.controlPanel, "Images/UI/IconReset", delegate
			{
				UIWorldGenDebug.Controller.TryReset();
			}, () => "Reset", delegate
			{
				if (!UIWorldGenDebug.CanSubmitActions)
				{
					return "[c/FFA500:Must be paused to reset]";
				}
				return null;
			});
			groupOptionButton.IconScale = 28f / (float)groupOptionButton.Icon.Width;
			groupOptionButton.IconOffset = new Vector2(2f, 3f);
			GroupOptionButton<bool> groupOptionButton2 = this.AddButton(this.controlPanel, "Images/UI/IconPrev", new Action(this.StepBack), () => "Step Back", () => "Hotkey: Up/Left");
			groupOptionButton2.IconScale = 28f / (float)groupOptionButton2.Icon.Width;
			groupOptionButton2.IconOffset = new Vector2(2f, 3f);
			GroupOptionButton<bool> playPauseButton = this.AddButton(this.controlPanel, "Images/UI/IconPlayPause", delegate
			{
				WorldGenerator.Controller controller = UIWorldGenDebug.Controller;
				controller.Paused = !controller.Paused;
			}, delegate
			{
				if (!WorldGenerator.CurrentController.Paused)
				{
					return "Pause";
				}
				return "Play";
			}, () => "Hotkey: Space");
			playPauseButton.IconScale = 28f / (float)playPauseButton.Icon.Width;
			playPauseButton.IconOffset = new Vector2(3f, 3f);
			playPauseButton.OnUpdate += delegate(UIElement e)
			{
				playPauseButton.SetIconFrame(playPauseButton.Icon.Frame(1, 2, 0, UIWorldGenDebug.Controller.Paused ? 0 : 1, 0, 0));
			};
			GroupOptionButton<bool> groupOptionButton3 = this.AddButton(this.controlPanel, "Images/UI/IconNext", new Action(this.StepForward), () => "Step Forward", () => "Hotkey: Down/Right");
			groupOptionButton3.IconScale = 28f / (float)groupOptionButton3.Icon.Width;
			groupOptionButton3.IconOffset = new Vector2(2f, 3f);
			this.AddButton(this.controlPanel, "Images/Map_0", delegate
			{
				this.ToggleMap();
			}, () => "Toggle Map", () => "Left click to toggle the map display").IconOffset = new Vector2(4f, 5f);
			GroupOptionButton<bool> groupOptionButton4 = this.AddButton(this.controlPanel, "Images/Extra_" + 48, delegate
			{
				this.hideChat = !this.hideChat;
			}, () => "Toggle Chat", () => "Left click to toggle the chat log");
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/Extra_" + 48, 1);
			Rectangle rectangle = asset.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, 1, 0, 0, 0);
			groupOptionButton4.IconScale = 28f / (float)rectangle.Width;
			groupOptionButton4.IconOffset = new Vector2(3f, 5f);
			groupOptionButton4.SetIconFrame(rectangle);
			rectangle = asset.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, 4, 3, 0, 0);
			UIImage uiimage2 = new UIImage(asset)
			{
				Frame = new Rectangle?(rectangle),
				ScaleToFit = true,
				Width = new StyleDimension(28f, 0f),
				Height = new StyleDimension(28f, 0f),
				Left = new StyleDimension(2f, 0f),
				Top = new StyleDimension(6f, 0f)
			};
			groupOptionButton4.Append(uiimage2);
			GroupOptionButton<bool> snapshotFrequencyButton = this.AddButton(this.controlPanel, "Images/UI/IconSnapshotFrequency", new Action(this.CycleSnapshotMode), new Func<string>(this.GetSnapshotModeButtonTitle), null);
			snapshotFrequencyButton.OnUpdate += delegate(UIElement e)
			{
				snapshotFrequencyButton.SetIconFrame(snapshotFrequencyButton.Icon.Frame(1, 3, 0, (int)WorldGenerator.CurrentController.SnapshotFrequency, 0, 0));
			};
			GroupOptionButton<bool> mismatchPauseButton = this.AddButton(this.controlPanel, "Images/UI/IconMismatchPause", delegate
			{
				WorldGenerator.Controller controller2 = UIWorldGenDebug.Controller;
				controller2.PauseOnHashMismatch = !controller2.PauseOnHashMismatch;
			}, () => "Pause on gen pass change: " + (WorldGenerator.CurrentController.PauseOnHashMismatch ? "On" : "Off"), () => "Stop the generator when the output of a pass is different\nto the last time it was run in the save, or current session");
			mismatchPauseButton.SetColorsBasedOnSelectionState(new Color(152, 175, 235), Colors.InventoryDefaultColor, 1f, 0.7f);
			mismatchPauseButton.OnUpdate += delegate(UIElement e)
			{
				mismatchPauseButton.SetCurrentOption(WorldGenerator.CurrentController.PauseOnHashMismatch);
			};
			string quickLoadCommand = (Main.gameMenu ? "/quickload-regen" : "/quickload");
			this.AddButton(this.controlPanel, "Images/UI/IconQuickload", delegate
			{
				DebugUtils.QuickSPMessage(quickLoadCommand);
			}, () => "Save current settings to " + quickLoadCommand, () => "Future launches of the game will automatically load the world\nfrom the most recent snapshot, and run to the current pass");
			UIImage uiimage3 = this.AddImage(this.controlPanel, "Images/UI/Bestiary/Icon_Locked", delegate
			{
			}, () => "Controls", () => this.GetControls());
			uiimage3.ImageScale = 24f / (float)uiimage3.Texture.Value.Height;
			uiimage3.NormalizedOrigin.X = 0.75f;
			this.AddButton(this.controlPanel, "Images/UI/Camera_5", delegate
			{
				UIWorldGenDebug.Controller.QueuedAbort = true;
			}, () => "Cancel", null).IconOffset = new Vector2(4f, 4f);
			this.controlListArea.Append(this.controlPanel);
			float num = this.controlPanel.Height.Pixels + 2f;
			this.scrollPanel = new UIPanel
			{
				Width = StyleDimension.FromPixelsAndPercent(300f, 0f),
				Height = StyleDimension.FromPixelsAndPercent(-num, 1f),
				Top = StyleDimension.FromPixels(num),
				Left = this.controlPanel.Left,
				HAlign = 0f,
				VAlign = 0f
			};
			this.scrollPanel.PaddingTop = 8f;
			this.scrollPanel.PaddingBottom = 8f;
			this.scrollPanel.PaddingLeft = 4f;
			this.scrollPanel.PaddingRight = 4f;
			this.controlListArea.Append(this.scrollPanel);
			this.searchBar = new UIWrappedSearchBar(delegate
			{
				UserInterface.ActiveInstance.SetState(this);
			}, null, UIWrappedSearchBar.ColorTheme.Blue)
			{
				Left = StyleDimension.FromPixels(-2f),
				Top = StyleDimension.FromPixels(-2f),
				Height = StyleDimension.FromPixels(28f),
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				HAlign = 0f
			};
			this.searchBar.OnSearchContentsChanged += delegate(string s)
			{
				this.searchText = s;
			};
			this.searchBar.HideSearchButton();
			this.scrollPanel.Append(this.searchBar);
			num = 30f;
			UIList uilist = new UIList();
			uilist.Top = StyleDimension.FromPixels(num);
			uilist.Width = StyleDimension.FromPixelsAndPercent(-20f, 1f);
			uilist.Height = StyleDimension.FromPixelsAndPercent(-num, 1f);
			uilist.ListPadding = 0f;
			uilist.ManualSortMethod = delegate(List<UIElement> _)
			{
			};
			this.GenPassList = uilist;
			this.scrollPanel.Append(this.GenPassList);
			foreach (GenPass genPass in UIWorldGenDebug.Controller.Passes)
			{
				UIWorldGenDebug.GenPassElement genPassElement = new UIWorldGenDebug.GenPassElement(this, genPass)
				{
					Width = new StyleDimension(-4f, 1f),
					Height = StyleDimension.FromPixels(32f),
					PaddingLeft = 7f
				};
				this.allPasses.Add(genPassElement);
				this.GenPassList.Add(genPassElement);
			}
			this.scrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue)
			{
				Top = StyleDimension.FromPixels(34f),
				Height = StyleDimension.FromPixelsAndPercent(-38f, 1f),
				Left = StyleDimension.FromPixels(-1f),
				HAlign = 1f
			};
			this.scrollbar.SetView(100f, 1000f);
			this.GenPassList.SetScrollbar(this.scrollbar);
			this.scrollPanel.Append(this.scrollbar);
			this.RefreshControlsPosition();
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x00582938 File Offset: 0x00580B38
		private void EnsurePassVisible(int passIndex)
		{
			if (passIndex < this.allPasses.Count)
			{
				UIWorldGenDebug.GenPassElement genPassElement = this.allPasses[passIndex];
				if (!this.searchVisible || string.IsNullOrEmpty(this.searchText) || this.MatchesSearch(genPassElement.Pass))
				{
					float height = this.scrollPanel.GetDimensions().Height;
					if (genPassElement.Height.Pixels + genPassElement.Top.Pixels > this.scrollbar.ViewPosition + height - 8f)
					{
						this.scrollbar.ViewPosition = genPassElement.Top.Pixels - (height - 8f) + genPassElement.Height.Pixels;
						return;
					}
					if (genPassElement.Top.Pixels < this.scrollbar.ViewPosition)
					{
						this.scrollbar.ViewPosition = genPassElement.Top.Pixels;
					}
				}
			}
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00009E46 File Offset: 0x00008046
		private void RefreshControlsPosition()
		{
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x00582A1F File Offset: 0x00580C1F
		private string GetControls()
		{
			return "[c/FFF014:Space] to pause/resume\n[c/FFF014:R] to rerun current step\n[c/FFF014:Up]/[c/FFF014:Down] or [c/FFF014:Left]/[c/FFF014:Right] to step back/forward\n[c/FFF014:H] to hide UI\n[c/FFF014:M] to toggle map\n[c/FFF014:C] to hide chat log\n";
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x00582A28 File Offset: 0x00580C28
		private GroupOptionButton<bool> AddButton(UIPanel controlPanel, string assetPath, Action onClick, Func<string> getTitle, Func<string> getDescription = null)
		{
			GroupOptionButton<bool> groupOptionButton = new GroupOptionButton<bool>(true, null, null, Color.White, assetPath, 1f, 0.5f, 10f)
			{
				Width = new StyleDimension(34f, 0f),
				Height = new StyleDimension(34f, 0f),
				Left = StyleDimension.FromPixelsAndPercent((float)(36 * controlPanel.Children.Count<UIElement>()), 0f),
				ShowHighlightWhenSelected = false
			};
			groupOptionButton.IconScale = 24f / (float)groupOptionButton.Icon.Width;
			groupOptionButton.IconOffset = new Vector2(3f, 3f);
			groupOptionButton.OnLeftClick += delegate(UIMouseEvent evt, UIElement e)
			{
				onClick();
			};
			groupOptionButton.Append(new UIWorldGenDebug.TooltipElement(getTitle, getDescription));
			controlPanel.Append(groupOptionButton);
			controlPanel.Width = StyleDimension.FromPixelsAndPercent((float)(36 * controlPanel.Children.Count<UIElement>() - 2) + controlPanel.PaddingLeft + controlPanel.PaddingRight, 0f);
			return groupOptionButton;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x00582B38 File Offset: 0x00580D38
		private UIImage AddImage(UIPanel controlPanel, string assetPath, Action onClick, Func<string> getTitle, Func<string> getDescription = null)
		{
			UIImage uiimage = new UIImage(Main.Assets.Request<Texture2D>(assetPath, 1))
			{
				Width = new StyleDimension(34f, 0f),
				Height = new StyleDimension(34f, 0f),
				Left = StyleDimension.FromPixelsAndPercent((float)(36 * controlPanel.Children.Count<UIElement>()), 0f)
			};
			uiimage.OnLeftClick += delegate(UIMouseEvent evt, UIElement e)
			{
				onClick();
			};
			uiimage.Append(new UIWorldGenDebug.TooltipElement(getTitle, getDescription));
			controlPanel.Append(uiimage);
			controlPanel.Width = StyleDimension.FromPixelsAndPercent((float)(36 * controlPanel.Children.Count<UIElement>() - 2) + controlPanel.PaddingLeft + controlPanel.PaddingRight, 0f);
			return uiimage;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00582C08 File Offset: 0x00580E08
		private void RangePassClickEvent(UIWorldGenDebug.GenPassElement target, Action<UIWorldGenDebug.GenPassElement> evt)
		{
			if (this._previousRangePassClickEvent != null && this._previousRangePassClickEvent.Item2.Method == evt.Method && this._previousRangePassClickEvent.Item1 != target && this._previousRangePassClickEvent.Item1.Parent == target.Parent && Main.keyState.PressingShift())
			{
				IEnumerable<UIWorldGenDebug.GenPassElement> enumerable = ((UIList)target.Parent.Parent).Cast<UIWorldGenDebug.GenPassElement>();
				UIWorldGenDebug.GenPassElement item = this._previousRangePassClickEvent.Item1;
				int num = 0;
				using (IEnumerator<UIWorldGenDebug.GenPassElement> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						UIWorldGenDebug.GenPassElement genPassElement = enumerator.Current;
						if (genPassElement == item || genPassElement == target)
						{
							num++;
						}
						if (num > 0)
						{
							evt(genPassElement);
						}
						if (num == 2)
						{
							break;
						}
					}
					goto IL_00CA;
				}
			}
			evt(target);
			IL_00CA:
			this._previousRangePassClickEvent = new Tuple<UIWorldGenDebug.GenPassElement, Action<UIWorldGenDebug.GenPassElement>>(target, evt);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x00582CFC File Offset: 0x00580EFC
		private void RangePassClickEventCheckHistory_OnElementClicked(UIElement clicked)
		{
			if (this._previousRangePassClickEvent != null && clicked != this._previousRangePassClickEvent.Item1)
			{
				this._previousRangePassClickEvent = null;
			}
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x00582D1C File Offset: 0x00580F1C
		public override void OnActivate()
		{
			UIWorldGenDebug.Config.Load();
			if (UIWorldGenDebug.Controller.SnapshotFrequency == WorldGenerator.SnapshotFrequency.None)
			{
				UIWorldGenDebug.Controller.SnapshotFrequency = WorldGenerator.SnapshotFrequency.Automatic;
			}
			Main.menuChat = true;
			if (Main.gameMenu)
			{
				PlayerInput.SetZoom_World();
				Main.mapFullscreenPos = new Vector2((float)(Main.maxTilesX / 2), (float)(Main.maxTilesY / 2));
				Main.mapFullscreenScale = (float)Main.screenWidth / (float)Main.maxTilesX;
			}
			else
			{
				Main.mapFullscreenScale = 2.5f;
				Main.mapFullscreenPos = Main.Camera.Center / 16f;
			}
			this.ToggleMap();
			if (!Main.gameMenu && !DebugOptions.devLightTilesCheat)
			{
				DebugOptions.devLightTilesCheat = true;
				this.disableLightOnClose = true;
			}
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x00582DCA File Offset: 0x00580FCA
		public override void OnDeactivate()
		{
			Main.menuChat = false;
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x00582DD4 File Offset: 0x00580FD4
		public override void Update(GameTime gameTime)
		{
			Main.starGame = false;
			Main.LocalPlayer.dead = true;
			if (UIWorldGenDebug.Controller.Paused && this.TestEnumerator != null)
			{
				for (;;)
				{
					if (UIWorldGenDebug.Controller.TryOperateInControlLock(delegate
					{
					}))
					{
						break;
					}
					Thread.Yield();
				}
				if (!this.TestEnumerator.MoveNext())
				{
					this.TestEnumerator = null;
				}
			}
			base.Update(gameTime);
			if (Main.drawingPlayerChat || this.searchBar.IsWritingText)
			{
				this.ignoreEscapeAttempt = 3;
				return;
			}
			int num = this.ignoreEscapeAttempt;
			this.ignoreEscapeAttempt = num - 1;
			if (num <= 0 && PlayerInput.Triggers.JustPressed.Inventory)
			{
				UIWorldGenDebug.Controller.QueuedAbort = true;
			}
			if (UIWorldGenDebug.KeyPressed(Keys.Space))
			{
				WorldGenerator.Controller controller = UIWorldGenDebug.Controller;
				controller.Paused = !controller.Paused;
			}
			if (UIWorldGenDebug.KeyPressed(Keys.R) && UIWorldGenDebug.CanSubmitActions && UIWorldGenDebug.Controller.LastCompletedPass != null)
			{
				UIWorldGenDebug.Controller.TryRunToEndOfPass(UIWorldGenDebug.Controller.LastCompletedPass, !Main.keyState.PressingShift(), true);
			}
			if (UIWorldGenDebug.KeyPressed(Keys.Up) || UIWorldGenDebug.KeyPressed(Keys.Left))
			{
				this.StepBack();
			}
			if (UIWorldGenDebug.KeyPressed(Keys.Down) || UIWorldGenDebug.KeyPressed(Keys.Right))
			{
				this.StepForward();
			}
			if (UIWorldGenDebug.KeyPressed(Keys.C))
			{
				this.hideChat = !this.hideChat;
			}
			if (UIWorldGenDebug.KeyPressed(Keys.H))
			{
				this.ToggleUI();
			}
			if (UIWorldGenDebug.KeyPressed(Keys.M))
			{
				this.ToggleMap();
			}
			PlayerInput.SetZoom_World();
			if (this.showMap)
			{
				if (PlayerInput.Triggers.Current.Up && !Main.oldKeyState.IsKeyDown(Keys.Up))
				{
					Main.mapFullscreenPos.Y = Main.mapFullscreenPos.Y - 1f * (16f / Main.mapFullscreenScale);
				}
				if (PlayerInput.Triggers.Current.Down && !Main.oldKeyState.IsKeyDown(Keys.Down))
				{
					Main.mapFullscreenPos.Y = Main.mapFullscreenPos.Y + 1f * (16f / Main.mapFullscreenScale);
				}
				if (PlayerInput.Triggers.Current.Left && !Main.oldKeyState.IsKeyDown(Keys.Left))
				{
					Main.mapFullscreenPos.X = Main.mapFullscreenPos.X - 1f * (16f / Main.mapFullscreenScale);
				}
				if (PlayerInput.Triggers.Current.Right && !Main.oldKeyState.IsKeyDown(Keys.Right))
				{
					Main.mapFullscreenPos.X = Main.mapFullscreenPos.X + 1f * (16f / Main.mapFullscreenScale);
				}
				if (!UserInterface.ActiveInstance.IsElementUnderMouse())
				{
					Main.mapFullscreenScale *= 1f + (float)(PlayerInput.ScrollWheelDelta / 120) * 0.3f;
				}
				Main.screenPosition = Main.mapFullscreenPos * 16f - Main.Camera.UnscaledSize / 2f;
			}
			else if (!Main.gameMenu)
			{
				Main.DebugCameraPan(PlayerInput.Triggers.Current.Left, PlayerInput.Triggers.Current.Right, PlayerInput.Triggers.Current.Up, PlayerInput.Triggers.Current.Down);
			}
			if (!Main.gameMenu)
			{
				Main.ClampScreenPositionToWorld();
				Main.LocalPlayer.position += Main.screenPosition - Main.PlayerFocusedScreenPosition();
				Main.mapFullscreenPos = Main.Camera.Center / 16f;
			}
			PlayerInput.SetZoom_UI();
			UIWorldGenDebug.spaceWasPressed = Main.keyState.IsKeyDown(Keys.Space) || Main.oldKeyState.IsKeyDown(Keys.Space);
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x0058317C File Offset: 0x0058137C
		private void ToggleUI()
		{
			this.hideUI = !this.hideUI;
			foreach (UIElement uielement in this.Elements)
			{
				uielement.IgnoresMouseInteraction = this.hideUI;
			}
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x005831E4 File Offset: 0x005813E4
		public void UnhideChat()
		{
			this.hideChat = false;
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x005831ED File Offset: 0x005813ED
		private void StepBack()
		{
			if (UIWorldGenDebug.CurrentTargetOrLatestPass != null)
			{
				UIWorldGenDebug.Controller.TryResetToPreviousPass(UIWorldGenDebug.CurrentTargetOrLatestPass);
			}
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x00583208 File Offset: 0x00581408
		private void StepForward()
		{
			int num = UIWorldGenDebug.Controller.Passes.IndexOf(UIWorldGenDebug.CurrentTargetOrLatestPass);
			GenPass genPass = UIWorldGenDebug.Controller.Passes.Skip(num + 1).FirstOrDefault((GenPass p) => p.Enabled);
			if (genPass != null)
			{
				UIWorldGenDebug.Controller.TryRunToEndOfPass(genPass, true, true);
			}
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x00583272 File Offset: 0x00581472
		private void CycleSnapshotMode()
		{
			UIWorldGenDebug.Controller.SnapshotFrequency = (UIWorldGenDebug.Controller.SnapshotFrequency + 1) % (WorldGenerator.SnapshotFrequency)3;
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0058328C File Offset: 0x0058148C
		private string GetSnapshotModeButtonTitle()
		{
			switch (WorldGenerator.CurrentController.SnapshotFrequency)
			{
			case WorldGenerator.SnapshotFrequency.Manual:
				return "Create snaphots: Manually";
			case WorldGenerator.SnapshotFrequency.Automatic:
				return "Create snaphots: Automatically";
			case WorldGenerator.SnapshotFrequency.Always:
				return "Create snaphots: After every pass";
			default:
				return "";
			}
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x005832CF File Offset: 0x005814CF
		private static bool KeyPressed(Keys key)
		{
			return Main.keyState.IsKeyDown(key) && !Main.oldKeyState.IsKeyDown(key);
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x005832EE File Offset: 0x005814EE
		private bool MatchesSearch(GenPass pass)
		{
			return string.IsNullOrWhiteSpace(this.searchText) || pass.Name.ToLowerInvariant().Contains(this.searchText.Trim().ToLowerInvariant());
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x00583320 File Offset: 0x00581520
		private void UpdateFilter()
		{
			if (this.searchVisible && !string.IsNullOrEmpty(this.searchText))
			{
				if (!(this.lastSearchText != this.searchText))
				{
					return;
				}
				this.GenPassList.Clear();
				this.lastSearchText = this.searchText;
				using (List<UIWorldGenDebug.GenPassElement>.Enumerator enumerator = this.allPasses.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						UIWorldGenDebug.GenPassElement genPassElement = enumerator.Current;
						if (this.MatchesSearch(genPassElement.Pass))
						{
							this.GenPassList.Add(genPassElement);
						}
					}
					return;
				}
			}
			if (this.allPasses.Count != this.GenPassList.Count)
			{
				this.lastSearchText = null;
				this.GenPassList.Clear();
				foreach (UIWorldGenDebug.GenPassElement genPassElement2 in this.allPasses)
				{
					this.GenPassList.Add(genPassElement2);
				}
			}
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x0058343C File Offset: 0x0058163C
		public override void Recalculate()
		{
			if (Main.gameMenu)
			{
				Main.UIScale = Main.UIScaleWanted;
				PlayerInput.SetZoom_UI();
			}
			base.Recalculate();
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x0058345A File Offset: 0x0058165A
		protected override void DrawChildren(SpriteBatch spriteBatch)
		{
			if (!this.hideUI)
			{
				base.DrawChildren(spriteBatch);
			}
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0058346C File Offset: 0x0058166C
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			Main.starGame = false;
			Main.onlyDrawFancyUI = this.showMap;
			if (this.showMap)
			{
				Main.alreadyGrabbingSunOrMoon = false;
				this.UpdateAndDrawMap();
				Main.instance.DrawFPS();
			}
			if (!this.hideChat || Main.drawingPlayerChat)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateTranslation(250f, 0f, 0f) * Main.UIScaleMatrix);
				Main.instance.DrawPlayerChat();
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
			}
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x00583508 File Offset: 0x00581708
		public override void Draw(SpriteBatch spriteBatch)
		{
			this.UpdateFilter();
			if (Main.gameMenu)
			{
				Main.UIScale = Main.UIScaleWanted;
				PlayerInput.SetZoom_UI();
				Main.spriteBatch.End();
				Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
			}
			base.Draw(spriteBatch);
			if (!this.showMap)
			{
				Main.DrawInterface_37_DebugStuff();
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00583564 File Offset: 0x00581764
		private void ToggleMap()
		{
			this.showMap = !this.showMap;
			Main.onlyDrawFancyUI = this.showMap;
			if (this.showMap)
			{
				this.nextMapSection = Point.Zero;
				this.fullMapScanTimer = Stopwatch.StartNew();
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x005835A0 File Offset: 0x005817A0
		private void UpdateAndDrawMap()
		{
			Main.spriteBatch.End();
			if (Main.clearMap)
			{
				Main.Map.Clear();
				MapRenderer.DrawToMap(default(Rectangle));
			}
			PlayerInput.SetZoom_Unscaled();
			Rectangle rectangle = Utils.CenteredRectangle(Main.mapFullscreenPos.ToPoint(), (Main.ScreenSize.ToVector2() / Main.mapFullscreenScale).ToPoint());
			rectangle.Inflate(2, 2);
			rectangle = WorldUtils.ClampToWorld(rectangle, 40);
			Stopwatch stopwatch = Stopwatch.StartNew();
			Point point = this.nextMapSection;
			while (stopwatch.ElapsedMilliseconds < 10L)
			{
				Rectangle sectionStripRect = new Rectangle(this.nextMapSection.X * 200, 0, 200, Main.maxTilesY);
				sectionStripRect = Rectangle.Intersect(sectionStripRect, rectangle);
				bool mapUpdate = false;
				FastParallel.For(sectionStripRect.Left, sectionStripRect.Right, delegate(int x1, int x2, object _)
				{
					bool flag4 = false;
					int top = sectionStripRect.Top;
					int bottom = sectionStripRect.Bottom;
					for (int i = x1; i < x2; i++)
					{
						for (int j = top; j < bottom; j++)
						{
							flag4 |= Main.Map.UpdateLighting(i, j, byte.MaxValue);
						}
					}
					if (flag4)
					{
						mapUpdate = true;
					}
				}, null);
				this.nextMapSection.Y = 0;
				while ((this.nextMapSection.Y < Main.maxSectionsY) & mapUpdate)
				{
					Rectangle rectangle2 = new Rectangle(this.nextMapSection.X * 200, this.nextMapSection.Y * 150, 200, 150);
					if (rectangle2.Intersects(rectangle))
					{
						MapRenderer.DrawToMap_Section(this.nextMapSection.X, this.nextMapSection.Y);
					}
					this.nextMapSection.Y = this.nextMapSection.Y + 1;
				}
				this.nextMapSection.X = this.nextMapSection.X + 1;
				if (this.nextMapSection.X >= Main.maxSectionsX)
				{
					if (this.lastScanRateUpdate.Elapsed > TimeSpan.FromMilliseconds(200.0))
					{
						this.lastScanRateUpdate.Restart();
						this.fullMapScanPeriod = this.fullMapScanTimer.Elapsed;
					}
					this.fullMapScanTimer.Restart();
					this.nextMapSection.X = 0;
				}
				if (this.nextMapSection.X == point.X)
				{
					break;
				}
			}
			Main.instance.GraphicsDevice.Clear(new Color(100, 100, 255));
			Main.spriteBatch.Begin();
			Main.mapReady = true;
			Main.MapPylonTile = new Point16(-1, -1);
			Main.mapFullscreen = true;
			bool flag = UserInterface.ActiveInstance.MouseCaptured() || UserInterface.ActiveInstance.IsElementUnderMouse();
			bool flag2 = Main.mouseLeft && !flag;
			bool flag3 = Main.mouseRight && !flag;
			Utils.Swap<bool>(ref Main.mouseLeft, ref flag2);
			Utils.Swap<bool>(ref Main.mouseRight, ref flag3);
			Main.instance.DrawMap(new GameTime());
			Utils.Swap<bool>(ref Main.mouseLeft, ref flag2);
			Utils.Swap<bool>(ref Main.mouseRight, ref flag3);
			Main.mapFullscreen = false;
			PlayerInput.SetZoom_UI();
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
			if (Main.showFrameRate)
			{
				double num = Math.Min(1.0 / this.fullMapScanPeriod.TotalSeconds, 60.0);
				string text = string.Format((num >= 10.0) ? "{0:0}" : "{0:0.0}", num);
				text += " map scans/s";
				DynamicSpriteFontExtensionMethods.DrawString(Main.spriteBatch, FontAssets.MouseText.Value, text, new Vector2((float)(Main.screenWidth - (int)FontAssets.MouseText.Value.MeasureString(text).X), 4f), new Color((int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor, (int)Main.mouseTextColor));
			}
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x00583967 File Offset: 0x00581B67
		public void RunTest(IEnumerable<object> test)
		{
			this.TestEnumerator = test.GetEnumerator();
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x00583975 File Offset: 0x00581B75
		private static IEnumerable<bool> TestSetupResetAndCreateSnapshots()
		{
			UIWorldGenDebug.Controller.TryReset();
			UIWorldGenDebug.Controller.SnapshotFrequency = WorldGenerator.SnapshotFrequency.Always;
			UIWorldGenDebug.Controller.PauseOnHashMismatch = true;
			Main.NewText("Creating Snapshots", byte.MaxValue, 100, 0);
			GenPass lastPass = UIWorldGenDebug.Controller.Passes.Last<GenPass>();
			UIWorldGenDebug.Controller.TryRunToEndOfPass(lastPass, false, true);
			yield return true;
			if (UIWorldGenDebug.Controller.LastCompletedPass != lastPass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
			{
				Main.NewText("Test aborted", byte.MaxValue, 0, 0);
				yield return false;
			}
			UIWorldGenDebug.Controller.SnapshotFrequency = WorldGenerator.SnapshotFrequency.Manual;
			yield break;
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x0058397E File Offset: 0x00581B7E
		public static IEnumerable<object> TestResetFromPassesAndRegen()
		{
			using (IEnumerator<bool> enumerator = UIWorldGenDebug.TestSetupResetAndCreateSnapshots().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current)
					{
						yield break;
					}
					yield return null;
				}
			}
			IEnumerator<bool> enumerator = null;
			GenPass lastPass = UIWorldGenDebug.Controller.Passes.Last<GenPass>();
			List<GenPass> passes = UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled).ToList<GenPass>();
			int num;
			for (int i = 0; i < passes.Count; i = num + 1)
			{
				GenPass pass = passes[i];
				UIWorldGenDebug.Controller.TryReset();
				Main.NewText(string.Format("[{0}/{1}] Running to {2}", i + 1, passes.Count, pass.Name), byte.MaxValue, 100, 0);
				UIWorldGenDebug.Controller.TryRunToEndOfPass(pass, false, true);
				yield return null;
				if (UIWorldGenDebug.Controller.LastCompletedPass != pass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
				{
					Main.NewText("Test aborted", byte.MaxValue, 0, 0);
					yield break;
				}
				UIWorldGenDebug.Controller.TryReset();
				UIWorldGenDebug.Controller.TryRunToEndOfPass(lastPass, false, true);
				yield return null;
				if (UIWorldGenDebug.Controller.LastCompletedPass != lastPass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
				{
					Main.NewText("Test aborted", byte.MaxValue, 0, 0);
					yield break;
				}
				pass = null;
				num = i;
			}
			Main.NewText("Test Completed Successfully", 0, byte.MaxValue, 0);
			yield break;
			yield break;
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x00583987 File Offset: 0x00581B87
		public static IEnumerable<object> TestHiddenTileData()
		{
			using (IEnumerator<bool> enumerator = UIWorldGenDebug.TestSetupResetAndCreateSnapshots().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current)
					{
						yield break;
					}
					yield return null;
				}
			}
			IEnumerator<bool> enumerator = null;
			UIWorldGenDebug.Controller.TryReset();
			foreach (GenPass pass in UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled))
			{
				TileSnapshot.Create(null);
				TileSnapshot.Restore();
				UIWorldGenDebug.Controller.TryRunToEndOfPass(pass, false, true);
				yield return null;
				if (UIWorldGenDebug.Controller.LastCompletedPass != pass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
				{
					Main.NewText("Test aborted", byte.MaxValue, 0, 0);
					yield break;
				}
				pass = null;
			}
			IEnumerator<GenPass> enumerator2 = null;
			Main.NewText("Test Completed Successfully", 0, byte.MaxValue, 0);
			yield break;
			yield break;
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00583990 File Offset: 0x00581B90
		public static IEnumerable<object> TestResumeFromSnapshots()
		{
			using (IEnumerator<bool> enumerator = UIWorldGenDebug.TestSetupResetAndCreateSnapshots().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current)
					{
						yield break;
					}
					yield return null;
				}
			}
			IEnumerator<bool> enumerator = null;
			GenPass lastPass = UIWorldGenDebug.Controller.Passes.Last<GenPass>();
			foreach (GenPass pass in UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled).Reverse<GenPass>())
			{
				UIWorldGenDebug.Controller.TryRunToEndOfPass(pass, true, true);
				yield return null;
				if (UIWorldGenDebug.Controller.LastCompletedPass != pass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
				{
					Main.NewText("Test aborted", byte.MaxValue, 0, 0);
					yield break;
				}
				pass = null;
			}
			IEnumerator<GenPass> enumerator2 = null;
			Main.NewText("Single pass rerun test completed successfully", 0, byte.MaxValue, 0);
			foreach (GenPass genPass in UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled))
			{
				UIWorldGenDebug.Controller.TryResetToSnapshot(genPass);
				UIWorldGenDebug.Controller.TryRunToEndOfPass(lastPass, false, true);
				yield return null;
				if (UIWorldGenDebug.Controller.LastCompletedPass != lastPass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
				{
					Main.NewText("Test aborted", byte.MaxValue, 0, 0);
					yield break;
				}
			}
			enumerator2 = null;
			Main.NewText("Load snapshot and run to end test completed successfully", 0, byte.MaxValue, 0);
			foreach (GenPass genPass2 in UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled))
			{
				UIWorldGenDebug.Controller.TryReset();
				UIWorldGenDebug.Controller.TryResetToSnapshot(genPass2);
				UIWorldGenDebug.Controller.TryRunToEndOfPass(lastPass, false, true);
				yield return null;
				if (UIWorldGenDebug.Controller.LastCompletedPass != lastPass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
				{
					Main.NewText("Test aborted", byte.MaxValue, 0, 0);
					yield break;
				}
			}
			enumerator2 = null;
			Main.NewText("Clean load snapshot and run to end test completed successfully", 0, byte.MaxValue, 0);
			yield break;
			yield break;
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static UIWorldGenDebug()
		{
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00583999 File Offset: 0x00581B99
		[CompilerGenerated]
		private void <.ctor>b__38_15()
		{
			this.ToggleMap();
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x005839A1 File Offset: 0x00581BA1
		[CompilerGenerated]
		private void <.ctor>b__38_18()
		{
			this.hideChat = !this.hideChat;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x005839B2 File Offset: 0x00581BB2
		[CompilerGenerated]
		private string <.ctor>b__38_31()
		{
			return this.GetControls();
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x005839BA File Offset: 0x00581BBA
		[CompilerGenerated]
		private void <.ctor>b__38_35()
		{
			UserInterface.ActiveInstance.SetState(this);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x005839C7 File Offset: 0x00581BC7
		[CompilerGenerated]
		private void <.ctor>b__38_34(string s)
		{
			this.searchText = s;
		}

		// Token: 0x040052F4 RID: 21236
		private UIWrappedSearchBar searchBar;

		// Token: 0x040052F5 RID: 21237
		private string lastSearchText;

		// Token: 0x040052F6 RID: 21238
		private string searchText;

		// Token: 0x040052F7 RID: 21239
		private bool showMap;

		// Token: 0x040052F8 RID: 21240
		private bool hideChat;

		// Token: 0x040052F9 RID: 21241
		private bool hideUI;

		// Token: 0x040052FA RID: 21242
		private bool disableDebugOnClose;

		// Token: 0x040052FB RID: 21243
		private bool disableLightOnClose;

		// Token: 0x040052FC RID: 21244
		private IEnumerator<object> TestEnumerator;

		// Token: 0x040052FD RID: 21245
		private UIElement controlListArea;

		// Token: 0x040052FE RID: 21246
		private UIPanel controlPanel;

		// Token: 0x040052FF RID: 21247
		private UIPanel scrollPanel;

		// Token: 0x04005300 RID: 21248
		private UIScrollbar scrollbar;

		// Token: 0x04005301 RID: 21249
		private UIList GenPassList;

		// Token: 0x04005302 RID: 21250
		private GroupOptionButton<bool> SearchButton;

		// Token: 0x04005303 RID: 21251
		private List<UIWorldGenDebug.GenPassElement> allPasses = new List<UIWorldGenDebug.GenPassElement>();

		// Token: 0x04005304 RID: 21252
		private bool searchVisible = true;

		// Token: 0x04005305 RID: 21253
		private int LassPassIndex;

		// Token: 0x04005306 RID: 21254
		private Tuple<UIWorldGenDebug.GenPassElement, Action<UIWorldGenDebug.GenPassElement>> _previousRangePassClickEvent;

		// Token: 0x04005307 RID: 21255
		private int ignoreEscapeAttempt;

		// Token: 0x04005308 RID: 21256
		private static bool spaceWasPressed;

		// Token: 0x04005309 RID: 21257
		private Point nextMapSection;

		// Token: 0x0400530A RID: 21258
		private TimeSpan fullMapScanPeriod;

		// Token: 0x0400530B RID: 21259
		private Stopwatch fullMapScanTimer;

		// Token: 0x0400530C RID: 21260
		private Stopwatch lastScanRateUpdate = Stopwatch.StartNew();

		// Token: 0x020008E1 RID: 2273
		private class TooltipElement : UIElement
		{
			// Token: 0x0600469E RID: 18078 RVA: 0x006C88E2 File Offset: 0x006C6AE2
			public TooltipElement(Func<string> getTitle, Func<string> getDescription = null)
			{
				this._getTitle = getTitle;
				this._getDescription = getDescription;
				this.IgnoresMouseInteraction = true;
			}

			// Token: 0x0600469F RID: 18079 RVA: 0x006C8900 File Offset: 0x006C6B00
			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				if (!base.Parent.IsMouseHovering)
				{
					return;
				}
				string text = this._getTitle();
				string text2 = ((this._getDescription == null) ? null : this._getDescription());
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				Item item = Main.DisplayAndGetFakeItem(ItemRarityColor.StrongRed10);
				item.SetNameOverride(text);
				item.ToolTip = ItemTooltip.FromHardcodedText(new string[] { text2 });
			}

			// Token: 0x04007395 RID: 29589
			private Func<string> _getTitle;

			// Token: 0x04007396 RID: 29590
			private Func<string> _getDescription;
		}

		// Token: 0x020008E2 RID: 2274
		private class Config
		{
			// Token: 0x060046A0 RID: 18080 RVA: 0x006C8969 File Offset: 0x006C6B69
			public static void Save()
			{
				File.WriteAllText(UIWorldGenDebug.Config.FilePath, JsonConvert.SerializeObject(UIWorldGenDebug.Config.Instance));
			}

			// Token: 0x060046A1 RID: 18081 RVA: 0x006C8980 File Offset: 0x006C6B80
			public static void Load()
			{
				try
				{
					if (File.Exists(UIWorldGenDebug.Config.FilePath))
					{
						UIWorldGenDebug.Config.Instance = JsonConvert.DeserializeObject<UIWorldGenDebug.Config>(File.ReadAllText(UIWorldGenDebug.Config.FilePath));
					}
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x060046A2 RID: 18082 RVA: 0x006C89C4 File Offset: 0x006C6BC4
			public Config()
			{
			}

			// Token: 0x060046A3 RID: 18083 RVA: 0x006C89D7 File Offset: 0x006C6BD7
			// Note: this type is marked as 'beforefieldinit'.
			static Config()
			{
			}

			// Token: 0x04007397 RID: 29591
			private static readonly string FilePath = Path.Combine(Main.SavePath, "dev-worldgen.json");

			// Token: 0x04007398 RID: 29592
			public static UIWorldGenDebug.Config Instance = new UIWorldGenDebug.Config();

			// Token: 0x04007399 RID: 29593
			public HashSet<string> HighlightedPassNames = new HashSet<string>();
		}

		// Token: 0x020008E3 RID: 2275
		private class UIImageButtonWithExtraIcon : UIImageButton
		{
			// Token: 0x060046A4 RID: 18084 RVA: 0x006C89F7 File Offset: 0x006C6BF7
			public UIImageButtonWithExtraIcon(Asset<Texture2D> texture, Rectangle? frame = null)
				: base(texture, frame)
			{
			}

			// Token: 0x060046A5 RID: 18085 RVA: 0x006C8A0C File Offset: 0x006C6C0C
			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				base.DrawSelf(spriteBatch);
				if (this._iconTexture != null)
				{
					Rectangle rectangle = base.GetDimensions().ToRectangle();
					rectangle.Inflate(-2, -2);
					int num;
					int num2;
					if (this._iconFrame != null)
					{
						num = this._iconFrame.Value.Width;
						num2 = this._iconFrame.Value.Height;
					}
					else
					{
						num = this._iconTexture.Value.Width;
						num2 = this._iconTexture.Value.Height;
					}
					if (num != num2)
					{
						if (num < num2)
						{
							float num3 = (float)num / (float)num2;
							int num4 = rectangle.Width - (int)((float)rectangle.Width * num3);
							rectangle.Width -= num4;
							rectangle.X += num4 / 2;
						}
						else
						{
							float num5 = (float)num2 / (float)num;
							int num6 = rectangle.Height - (int)((float)rectangle.Height * num5);
							rectangle.Height -= num6;
							rectangle.Y += num6 / 2;
						}
					}
					spriteBatch.Draw(this._iconTexture.Value, rectangle, this._iconFrame, this.IconColor * (base.IsMouseHovering ? this._visibilityActive : this._visibilityInactive));
				}
			}

			// Token: 0x060046A6 RID: 18086 RVA: 0x006C8B49 File Offset: 0x006C6D49
			public void SetIcon(string iconTexturePath)
			{
				if (iconTexturePath != null)
				{
					this._iconTexture = Main.Assets.Request<Texture2D>(iconTexturePath, 1);
					return;
				}
				this._iconTexture = null;
			}

			// Token: 0x060046A7 RID: 18087 RVA: 0x006C8B68 File Offset: 0x006C6D68
			public void SetIconFrame(Rectangle region)
			{
				this._iconFrame = new Rectangle?(region);
			}

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x060046A8 RID: 18088 RVA: 0x006C8B76 File Offset: 0x006C6D76
			public Texture2D Icon
			{
				get
				{
					if (this._iconTexture == null)
					{
						return null;
					}
					return this._iconTexture.Value;
				}
			}

			// Token: 0x0400739A RID: 29594
			private Rectangle? _iconFrame;

			// Token: 0x0400739B RID: 29595
			private Asset<Texture2D> _iconTexture;

			// Token: 0x0400739C RID: 29596
			public Color IconColor = Color.White;
		}

		// Token: 0x020008E4 RID: 2276
		private class GenPassElement : UIPanel
		{
			// Token: 0x060046A9 RID: 18089 RVA: 0x006C8B90 File Offset: 0x006C6D90
			private UIWorldGenDebug.UIImageButtonWithExtraIcon AddButton(string assetPath, string iconAsset, float x, float y, Action onClick, Func<string> getTitle, Func<string> getDescription = null)
			{
				UIWorldGenDebug.UIImageButtonWithExtraIcon uiimageButtonWithExtraIcon = new UIWorldGenDebug.UIImageButtonWithExtraIcon(Main.Assets.Request<Texture2D>(assetPath, 1), null)
				{
					Left = StyleDimension.FromPixelsAndPercent(x, 0f),
					Top = StyleDimension.FromPixelsAndPercent(y, 0f)
				};
				if (!string.IsNullOrEmpty(iconAsset))
				{
					uiimageButtonWithExtraIcon.SetIcon(iconAsset);
				}
				uiimageButtonWithExtraIcon.OnLeftClick += delegate(UIMouseEvent evt, UIElement e)
				{
					onClick();
				};
				if (getTitle != null)
				{
					uiimageButtonWithExtraIcon.Append(new UIWorldGenDebug.TooltipElement(getTitle, getDescription));
				}
				base.Append(uiimageButtonWithExtraIcon);
				return uiimageButtonWithExtraIcon;
			}

			// Token: 0x060046AA RID: 18090 RVA: 0x006C8C25 File Offset: 0x006C6E25
			protected override void DrawSelf(SpriteBatch spriteBatch)
			{
				this.RefreshColors();
				base.DrawSelf(spriteBatch);
			}

			// Token: 0x060046AB RID: 18091 RVA: 0x006C8C34 File Offset: 0x006C6E34
			private static void InitPassIcons()
			{
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Terrain, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(1));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Skyblock, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(26));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DunesAndPyramidLocations, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(4));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.OceanSand, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(28));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SandPatches, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(169));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Tunnels, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4501));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.MountainCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4510));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DirtWallBackgrounds, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(30));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.RocksInDirt, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(3));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DirtInRocks, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(2));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Clay, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(133));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SmallHoles, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4538));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DirtLayerCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4510));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.RockLayerCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4512));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SurfaceCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4501));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.WavyCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4537));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.IceBiome, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(6));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Grass, UIWorldGenDebug.GenPassElement.PassIconEntry.FromImageFrame("Images/Tiles_3", 5, 45, 1));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Jungle, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(22));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.MudCavesToJungleGrass, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(745));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DesertBiome, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(3));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.GlowingMushroomPatches, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(24));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Marble, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(29));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Granite, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(30));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.FloatingIslands, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(26));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DirtToMud, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(176));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Silt, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(424));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.OresAndShinies, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(19));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Webs, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(150));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Underworld, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(33));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.CorruptionAndCrimson, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(7));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Lakes, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(28));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.StoneToIceAndSiltPlusMudIntoSlush, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(1103));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DualDungeonsDitherSnake, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(32));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Dungeon, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(32));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.MountainCaveOpenings, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(2));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.BeachesAndOceanCleanup, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(27));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Gems, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(178));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.GravitatingSandCleanup, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(169));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.OceanCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(28));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Shimmer, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(5340));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DirtWallCleanup, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(2));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Pyramids, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(607));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DirtRockWallRunner, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4501));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.LivingTrees, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(0));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.LivingTreeWalls, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(1723));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DemonAndCrimsonAltars, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(5467));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SurfaceWaterInJungle, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(22));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.LihzahrdTemple, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(31));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Beehives, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(1126));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.JungleShrines, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(680));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SettleLiquids, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(28));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.RemoveSurfaceWaterAboveSand, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(169));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Oasis, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(27));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.ShellPilesMarblePilesAndSpikePits, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4090));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SmoothWorld, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(1));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Waterfalls, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(2169));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.FragileIceOverIceBiomeWater, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(664));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.CaveWallVariety, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4540));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.LifeCrystals, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(29));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Statues, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(52));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.UndergroundHousesAndBuriedChests, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(306));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SurfaceChests, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(48));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.ChestsInJungleShrines, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(680));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.UnderwaterChests, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(1298));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SpiderCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(34));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.GemCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4644));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.MossAndMossCaves, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4496));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.LihzahrdTemplePart2, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(31));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.CaveWallsInEnclosedSpaces, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4510));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.UndergroundJungleTrees, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(23));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.FloatingIslandHouses, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(26));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.QuickCleanup, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(41));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.PotsGraveyardsAndBoulderPiles, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(222));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Hellforges, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(221));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SpreadingGrassOnSurfaceSunflowersEvilsOnSurfaceAndLavaCleanup, UIWorldGenDebug.GenPassElement.PassIconEntry.FromImageFrame("Images/Tiles_3", 5, 45, 1));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SurfaceOreAndStone, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(19));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.FallenLogsAndWaterFeatures, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(0));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Traps, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(580));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Piles, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(1));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SpawnPoint, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(224));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SurfaceDirtWallsToGrassWalls, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(745));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SpawnStarterNPCs, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(867));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SunflowersPart2, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(63));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Trees, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(0));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.AlchemyHerbs, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(3093));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DyePlants, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(1109));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.WebsInSpiderCavesAndHoneyPlusSpeleothemsInBeehives, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(150));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.GrassPlantsEvilPlantsAndPumpkinsOnSurface, UIWorldGenDebug.GenPassElement.PassIconEntry.FromImageFrame("Images/Tiles_3", 5, 45, 1));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.GlowingMushroomPlantsUndergroundAndJunglePlants, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(25));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.JunglePlantsPart2, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(23));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Vines, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(3005));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Flowers, UIWorldGenDebug.GenPassElement.PassIconEntry.FromImageFrame("Images/Tiles_3", 33, 45, 1));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.Mushrooms, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(5));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.ExposedGemsInIceBiome, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(182));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.ExposedGemsUnderground, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4400));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.LongMoss, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4496));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.DirtWallsIntoMudWallsInJungleAndJungleMinMax, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4487));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.BeeLarvaInBeehives, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(2108));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SettleLiquidsPart2AndNotTheBees, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(28));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.CactusPalmTreesAndCoral, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(3));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.TileCleanup, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(41));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.LihzahrdAltar, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(31));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.MicroBiomes, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(0));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.LilypadsCattailsBambooAndSeaweed, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(4564));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.SpeleothemsAndGemTrees, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(6));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.BrokenTrapCleanup, UIWorldGenDebug.GenPassElement.PassIconEntry.FromItem(580));
				UIWorldGenDebug.GenPassElement.passIcons.Add(GenPassNameID.FinalCleanup, UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(41));
			}

			// Token: 0x060046AC RID: 18092 RVA: 0x006C9630 File Offset: 0x006C7830
			private UIWorldGenDebug.GenPassElement.PassIconEntry GetPassIcon(GenPass pass)
			{
				if (UIWorldGenDebug.GenPassElement.passIcons.Count == 0)
				{
					UIWorldGenDebug.GenPassElement.InitPassIcons();
				}
				UIWorldGenDebug.GenPassElement.PassIconEntry passIconEntry;
				if (!UIWorldGenDebug.GenPassElement.passIcons.TryGetValue(pass.Name, out passIconEntry))
				{
					passIconEntry = UIWorldGenDebug.GenPassElement.PassIconEntry.FromBestiaryIcon(64);
				}
				return passIconEntry;
			}

			// Token: 0x060046AD RID: 18093 RVA: 0x006C966C File Offset: 0x006C786C
			private UIImage AddIcon()
			{
				UIWorldGenDebug.GenPassElement.PassIconEntry passIcon = this.GetPassIcon(this.Pass);
				return new UIImage(Main.Assets.Request<Texture2D>(passIcon.Icon, 1))
				{
					Width = new StyleDimension((float)passIcon.Width, 0f),
					Height = new StyleDimension((float)passIcon.Height, 0f),
					Top = new StyleDimension((float)((26 - passIcon.Height) / 2), 0f),
					Left = new StyleDimension((float)((26 - passIcon.Width) / 2), 0f),
					Frame = new Rectangle?(passIcon.Region),
					ScaleToFit = true
				};
			}

			// Token: 0x060046AE RID: 18094 RVA: 0x006C971C File Offset: 0x006C791C
			public GenPassElement(UIWorldGenDebug parent, GenPass pass)
			{
				UIWorldGenDebug.GenPassElement.<>c__DisplayClass9_0 CS$<>8__locals1 = new UIWorldGenDebug.GenPassElement.<>c__DisplayClass9_0();
				CS$<>8__locals1.pass = pass;
				CS$<>8__locals1.parent = parent;
				base..ctor();
				CS$<>8__locals1.<>4__this = this;
				this.Pass = CS$<>8__locals1.pass;
				base.SetPadding(2f);
				this.Height.Set(96f, 0f);
				base.Append(new UIWorldGenDebug.TooltipElement(new Func<string>(this.GetTitle), new Func<string>(this.GetDescription)));
				UIImage uiimage = this.AddIcon();
				uiimage.IgnoresMouseInteraction = true;
				base.Append(uiimage);
				UIText indexText = new UIText(this.Index.ToString(), 0.5f, false)
				{
					Left = StyleDimension.FromPixels(2f),
					Top = StyleDimension.FromPixels(2f),
					IgnoresMouseInteraction = true
				};
				base.Append(indexText);
				UIText text = new UIText(CS$<>8__locals1.pass.Name, 1f, false)
				{
					Left = StyleDimension.FromPixels(32f),
					Top = StyleDimension.FromPixels(4f),
					IgnoresMouseInteraction = true
				};
				text.OnUpdate += delegate(UIElement e)
				{
					text.TextColor = (CS$<>8__locals1.<>4__this.IsRunning ? Color.Yellow : (CS$<>8__locals1.<>4__this.Skipped ? Color.DarkGreen : (CS$<>8__locals1.<>4__this.HasCompleted ? new Color(0, 230, 0) : ((!CS$<>8__locals1.pass.Enabled) ? Color.DarkGray : Color.White))));
					text.TextColor *= (CS$<>8__locals1.parent.MatchesSearch(CS$<>8__locals1.pass) ? 1f : 0.6f);
					indexText.TextColor = text.TextColor;
				};
				base.Append(text);
				this.SetColorsToNotHovered();
				UIWorldGenDebug.UIImageButtonWithExtraIcon snapshotIcon = this.AddButton("Images/UI/ButtonBacking", "Images/UI/Camera_4", 72f, 3f, delegate
				{
					if (Main.keyState.PressingAlt())
					{
						return;
					}
					if (CS$<>8__locals1.<>4__this.Snapshot == null)
					{
						UIWorldGenDebug.Controller.TryCreateSnapshot();
						return;
					}
					if (!CS$<>8__locals1.<>4__this.Snapshot.Outdated)
					{
						UIWorldGenDebug.Controller.TryResetToSnapshot(CS$<>8__locals1.pass);
					}
				}, new Func<string>(this.GetSnapshotButtonTitle), new Func<string>(this.GetSnapshotButtonDescription));
				snapshotIcon.OnUpdate += delegate(UIElement e)
				{
					if (CS$<>8__locals1.<>4__this.Snapshot != null)
					{
						snapshotIcon.SetIcon("Images/UI/Camera_4");
					}
					else if (UIWorldGenDebug.Controller.LastCompletedPass == CS$<>8__locals1.<>4__this.Pass)
					{
						snapshotIcon.SetIcon("Images/UI/Camera_7");
					}
					UIWorldGenDebug.SetButtonState(snapshotIcon, (CS$<>8__locals1.<>4__this.Pass.Enabled && (CS$<>8__locals1.<>4__this.Snapshot != null || UIWorldGenDebug.Controller.LastCompletedPass == CS$<>8__locals1.<>4__this.Pass)) ? UIWorldGenDebug.ButtonState.Enabled : UIWorldGenDebug.ButtonState.NotVisible);
					if (CS$<>8__locals1.<>4__this.Snapshot != null && CS$<>8__locals1.<>4__this.Snapshot.Outdated)
					{
						snapshotIcon.IconColor = Color.PaleVioletRed;
						return;
					}
					snapshotIcon.IconColor = Color.White;
				};
				snapshotIcon.OnRightClick += delegate(UIMouseEvent evt, UIElement e)
				{
					if (CS$<>8__locals1.<>4__this.Snapshot != null)
					{
						UIWorldGenDebug.Controller.DeleteSnapshot(CS$<>8__locals1.pass);
						UserInterface.ActiveInstance.ClearPointers();
					}
				};
				snapshotIcon.Left = new StyleDimension(-28f, 1f);
				base.OnLeftClick += delegate(UIMouseEvent evt, UIElement e)
				{
					if (CS$<>8__locals1.<>4__this != evt.Target)
					{
						return;
					}
					if (UIWorldGenDebug.spaceWasPressed)
					{
						return;
					}
					if (Main.keyState.PressingAlt())
					{
						CS$<>8__locals1.<>4__this.ToggleHighlight();
						CS$<>8__locals1.<>4__this.SetColorsToHovered();
						return;
					}
					if (!CS$<>8__locals1.pass.Enabled)
					{
						CS$<>8__locals1.parent.RangePassClickEvent(CS$<>8__locals1.<>4__this, delegate(UIWorldGenDebug.GenPassElement x)
						{
							x.Enable();
							x.RefreshColors();
						});
						return;
					}
					if (CS$<>8__locals1.pass.Enabled)
					{
						UIWorldGenDebug.Controller.TryRunToEndOfPass(CS$<>8__locals1.pass, !Main.keyState.PressingShift(), true);
					}
				};
				base.OnRightClick += delegate(UIMouseEvent evt, UIElement e)
				{
					if (CS$<>8__locals1.pass.Enabled)
					{
						CS$<>8__locals1.parent.RangePassClickEvent(CS$<>8__locals1.<>4__this, delegate(UIWorldGenDebug.GenPassElement x)
						{
							x.Disable();
							x.RefreshColors();
						});
					}
				};
			}

			// Token: 0x060046AF RID: 18095 RVA: 0x006C9947 File Offset: 0x006C7B47
			private void RefreshColors()
			{
				if (this.Hovered)
				{
					this.SetColorsToHovered();
					return;
				}
				this.SetColorsToNotHovered();
			}

			// Token: 0x060046B0 RID: 18096 RVA: 0x006C9960 File Offset: 0x006C7B60
			private void SetColorsToHovered()
			{
				this.BackgroundColor = new Color(73, 94, 171);
				this.BorderColor = new Color(89, 116, 213);
				if (this.IsHighlighted)
				{
					this.BackgroundColor = new Color(110, 30, 150);
					this.BorderColor = new Color(171, 53, 255);
				}
				if (UIWorldGenDebug.CurrentTargetPass == this.Pass)
				{
					this.BorderColor = new Color(255, 231, 69);
				}
				if (!this.Pass.Enabled)
				{
					this.BorderColor = new Color(150, 150, 150) * 1f;
					this.BackgroundColor = Color.Lerp(this.BackgroundColor, new Color(120, 120, 120), 0.5f) * 1f;
				}
			}

			// Token: 0x060046B1 RID: 18097 RVA: 0x006C9A48 File Offset: 0x006C7C48
			private void SetColorsToNotHovered()
			{
				this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
				this.BorderColor = new Color(89, 116, 213) * 0.7f;
				if (this.IsHighlighted)
				{
					this.BackgroundColor = new Color(110, 30, 150) * 0.7f;
					this.BorderColor = new Color(171, 53, 255) * 0.7f;
				}
				if (UIWorldGenDebug.CurrentTargetPass == this.Pass)
				{
					this.BorderColor = new Color(255, 231, 69);
				}
				if (!this.Pass.Enabled)
				{
					this.BorderColor = new Color(127, 127, 127) * 0.7f;
					this.BackgroundColor = Color.Lerp(this.BackgroundColor, new Color(80, 80, 80), 0.5f) * 0.7f;
				}
			}

			// Token: 0x060046B2 RID: 18098 RVA: 0x006C9B4F File Offset: 0x006C7D4F
			public override void MouseOver(UIMouseEvent evt)
			{
				this.Hovered = true;
				base.MouseOver(evt);
				this.SetColorsToHovered();
			}

			// Token: 0x060046B3 RID: 18099 RVA: 0x006C9B65 File Offset: 0x006C7D65
			public override void MouseOut(UIMouseEvent evt)
			{
				this.Hovered = false;
				base.MouseOut(evt);
				this.SetColorsToNotHovered();
			}

			// Token: 0x060046B4 RID: 18100 RVA: 0x006C9B7C File Offset: 0x006C7D7C
			private string GetTitle()
			{
				if (this.Skipped)
				{
					return "Skipped: " + this.Pass.Name;
				}
				if (!this.Pass.Enabled)
				{
					return "Disabled: " + this.Pass.Name;
				}
				return ((!this.HasCompleted) ? "Run" : "Rerun") + " to " + this.Pass.Name;
			}

			// Token: 0x060046B5 RID: 18101 RVA: 0x006C9BF4 File Offset: 0x006C7DF4
			private string GetDescription()
			{
				string text = string.Empty;
				if (this.Pass.Enabled)
				{
					text += "Hold shift to ignore snapshots\n";
					if (!UIWorldGenDebug.CanSubmitActions && (this.HasCompleted || Main.keyState.PressingShift()))
					{
						text += "[c/FFA500:Must be paused to rerun or load snapshots]\n";
					}
				}
				if (!this.HasCompleted && !this.Skipped)
				{
					text += "Left click to enable\n";
					text += "Right click to disable\n";
					text += "Shift to edit ranges\n";
				}
				return text + "Alt click to toggle highlight\n";
			}

			// Token: 0x060046B6 RID: 18102 RVA: 0x006C9C88 File Offset: 0x006C7E88
			private string GetSnapshotButtonTitle()
			{
				if (this.Snapshot != null && this.Snapshot.Outdated)
				{
					return "Snapshot is outdated and will only be used for comparison when the pass is run again";
				}
				if (this.Snapshot != null)
				{
					return "Reset to snapshot";
				}
				if (UIWorldGenDebug.Controller.LastCompletedPass == this.Pass)
				{
					return "Take snapshot";
				}
				return null;
			}

			// Token: 0x060046B7 RID: 18103 RVA: 0x006C9CD8 File Offset: 0x006C7ED8
			private string GetSnapshotButtonDescription()
			{
				if (this.Snapshot != null)
				{
					string text = "Left click to load snapshot\n";
					text += "Right click to delete snapshot\n";
					if (!UIWorldGenDebug.CanSubmitActions)
					{
						text += "[c/FFA500:Must be paused to load a snapshot]";
					}
					return text;
				}
				if (UIWorldGenDebug.Controller.LastCompletedPass == this.Pass)
				{
					return "Left click to take snapshot\n";
				}
				return null;
			}

			// Token: 0x060046B8 RID: 18104 RVA: 0x006C9D2D File Offset: 0x006C7F2D
			private void Enable()
			{
				Utils.TryOperateInLock(this.Pass, delegate
				{
					if (!this.HasCompleted)
					{
						this.Pass.Enable();
						UIWorldGenDebug.Controller.ForceUpdateProgress();
					}
				});
			}

			// Token: 0x060046B9 RID: 18105 RVA: 0x006C9D47 File Offset: 0x006C7F47
			private void Disable()
			{
				Utils.TryOperateInLock(this.Pass, delegate
				{
					if (!this.HasCompleted)
					{
						this.Pass.Disable();
						UIWorldGenDebug.Controller.ForceUpdateProgress();
						UIWorldGenDebug.Controller.DeleteSnapshot(this.Pass);
					}
				});
			}

			// Token: 0x060046BA RID: 18106 RVA: 0x006C9D64 File Offset: 0x006C7F64
			private void ToggleHighlight()
			{
				if (this.IsHighlighted)
				{
					UIWorldGenDebug.Config.Instance.HighlightedPassNames.Remove(this.Pass.Name);
				}
				else
				{
					UIWorldGenDebug.Config.Instance.HighlightedPassNames.Add(this.Pass.Name);
				}
				UIWorldGenDebug.Config.Save();
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x060046BB RID: 18107 RVA: 0x006C9DB6 File Offset: 0x006C7FB6
			public int Index
			{
				get
				{
					return UIWorldGenDebug.Controller.Passes.IndexOf(this.Pass);
				}
			}

			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x060046BC RID: 18108 RVA: 0x006C9DCD File Offset: 0x006C7FCD
			public bool IsRunning
			{
				get
				{
					return UIWorldGenDebug.Controller.CurrentPass == this.Pass;
				}
			}

			// Token: 0x1700056F RID: 1391
			// (get) Token: 0x060046BD RID: 18109 RVA: 0x006C9DE1 File Offset: 0x006C7FE1
			public bool HasCompleted
			{
				get
				{
					return WorldGen.Manifest.GenPassResults.Count > this.Index;
				}
			}

			// Token: 0x17000570 RID: 1392
			// (get) Token: 0x060046BE RID: 18110 RVA: 0x006C9DFA File Offset: 0x006C7FFA
			public bool Skipped
			{
				get
				{
					return this.HasCompleted && WorldGen.Manifest.GenPassResults[this.Index].Skipped;
				}
			}

			// Token: 0x17000571 RID: 1393
			// (get) Token: 0x060046BF RID: 18111 RVA: 0x006C9E20 File Offset: 0x006C8020
			public WorldGenSnapshot Snapshot
			{
				get
				{
					return UIWorldGenDebug.Controller.GetSnapshot(this.Pass);
				}
			}

			// Token: 0x17000572 RID: 1394
			// (get) Token: 0x060046C0 RID: 18112 RVA: 0x006C9E32 File Offset: 0x006C8032
			public bool IsPausedAfterThisPass
			{
				get
				{
					return UIWorldGenDebug.CanSubmitActions && this.HasCompleted && !this.Skipped && WorldGen.Manifest.GenPassResults.Count == this.Index + 1;
				}
			}

			// Token: 0x17000573 RID: 1395
			// (get) Token: 0x060046C1 RID: 18113 RVA: 0x006C9E66 File Offset: 0x006C8066
			public bool IsHighlighted
			{
				get
				{
					return UIWorldGenDebug.Config.Instance.HighlightedPassNames.Contains(this.Pass.Name);
				}
			}

			// Token: 0x060046C2 RID: 18114 RVA: 0x006C9E82 File Offset: 0x006C8082
			// Note: this type is marked as 'beforefieldinit'.
			static GenPassElement()
			{
			}

			// Token: 0x060046C3 RID: 18115 RVA: 0x006C9E8E File Offset: 0x006C808E
			[CompilerGenerated]
			private void <Enable>b__19_0()
			{
				if (!this.HasCompleted)
				{
					this.Pass.Enable();
					UIWorldGenDebug.Controller.ForceUpdateProgress();
				}
			}

			// Token: 0x060046C4 RID: 18116 RVA: 0x006C9EAD File Offset: 0x006C80AD
			[CompilerGenerated]
			private void <Disable>b__20_0()
			{
				if (!this.HasCompleted)
				{
					this.Pass.Disable();
					UIWorldGenDebug.Controller.ForceUpdateProgress();
					UIWorldGenDebug.Controller.DeleteSnapshot(this.Pass);
				}
			}

			// Token: 0x0400739D RID: 29597
			public readonly GenPass Pass;

			// Token: 0x0400739E RID: 29598
			private bool Hovered;

			// Token: 0x0400739F RID: 29599
			private static Dictionary<string, UIWorldGenDebug.GenPassElement.PassIconEntry> passIcons = new Dictionary<string, UIWorldGenDebug.GenPassElement.PassIconEntry>();

			// Token: 0x02000AE3 RID: 2787
			internal struct PassIconEntry
			{
				// Token: 0x06004CEF RID: 19695 RVA: 0x006DB2B4 File Offset: 0x006D94B4
				internal static UIWorldGenDebug.GenPassElement.PassIconEntry FromBestiaryIcon(int index)
				{
					string text = "Images/UI/Bestiary/Icon_Tags_Shadow";
					Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(text, 1);
					return new UIWorldGenDebug.GenPassElement.PassIconEntry
					{
						Icon = text,
						Region = asset.Frame(16, 5, index % 16, index / 16, 0, 0),
						Width = 26,
						Height = 26
					};
				}

				// Token: 0x06004CF0 RID: 19696 RVA: 0x006DB314 File Offset: 0x006D9514
				internal static UIWorldGenDebug.GenPassElement.PassIconEntry FromItem(int index)
				{
					string text = "Images/Item_" + index.ToString();
					Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(text, 1);
					Rectangle rectangle = asset.Frame(1, 1, 0, 0, 0, 0);
					int num = ((rectangle.Width > rectangle.Height) ? rectangle.Width : asset.Height());
					float num2 = 20f / (float)num;
					if (num2 > 1.2f)
					{
						num2 = 1.2f;
					}
					return new UIWorldGenDebug.GenPassElement.PassIconEntry
					{
						Icon = text,
						Region = rectangle,
						Width = (int)((float)rectangle.Width * num2),
						Height = (int)((float)rectangle.Height * num2)
					};
				}

				// Token: 0x06004CF1 RID: 19697 RVA: 0x006DB3C4 File Offset: 0x006D95C4
				internal static UIWorldGenDebug.GenPassElement.PassIconEntry FromImageFrame(string image, int index, int rowCount, int lineCount)
				{
					Asset<Texture2D> asset = Main.Assets.Request<Texture2D>(image, 1);
					Rectangle rectangle = asset.Frame(rowCount, lineCount, index % rowCount, index / rowCount, 0, 0);
					int num = ((rectangle.Width > rectangle.Height) ? rectangle.Width : asset.Height());
					float num2 = 20f / (float)num;
					if (num2 > 1.2f)
					{
						num2 = 1.2f;
					}
					return new UIWorldGenDebug.GenPassElement.PassIconEntry
					{
						Icon = image,
						Region = rectangle,
						Width = (int)((float)rectangle.Width * num2),
						Height = (int)((float)rectangle.Height * num2)
					};
				}

				// Token: 0x040078A3 RID: 30883
				internal string Icon;

				// Token: 0x040078A4 RID: 30884
				internal Rectangle Region;

				// Token: 0x040078A5 RID: 30885
				internal int Width;

				// Token: 0x040078A6 RID: 30886
				internal int Height;
			}

			// Token: 0x02000AE4 RID: 2788
			[CompilerGenerated]
			private sealed class <>c__DisplayClass2_0
			{
				// Token: 0x06004CF2 RID: 19698 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass2_0()
				{
				}

				// Token: 0x06004CF3 RID: 19699 RVA: 0x006DB45F File Offset: 0x006D965F
				internal void <AddButton>b__0(UIMouseEvent evt, UIElement e)
				{
					this.onClick();
				}

				// Token: 0x040078A7 RID: 30887
				public Action onClick;
			}

			// Token: 0x02000AE5 RID: 2789
			[CompilerGenerated]
			private sealed class <>c__DisplayClass9_0
			{
				// Token: 0x06004CF4 RID: 19700 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass9_0()
				{
				}

				// Token: 0x06004CF5 RID: 19701 RVA: 0x006DB46C File Offset: 0x006D966C
				internal void <.ctor>b__1()
				{
					if (Main.keyState.PressingAlt())
					{
						return;
					}
					if (this.<>4__this.Snapshot == null)
					{
						UIWorldGenDebug.Controller.TryCreateSnapshot();
						return;
					}
					if (!this.<>4__this.Snapshot.Outdated)
					{
						UIWorldGenDebug.Controller.TryResetToSnapshot(this.pass);
					}
				}

				// Token: 0x06004CF6 RID: 19702 RVA: 0x006DB4C2 File Offset: 0x006D96C2
				internal void <.ctor>b__3(UIMouseEvent evt, UIElement e)
				{
					if (this.<>4__this.Snapshot != null)
					{
						UIWorldGenDebug.Controller.DeleteSnapshot(this.pass);
						UserInterface.ActiveInstance.ClearPointers();
					}
				}

				// Token: 0x06004CF7 RID: 19703 RVA: 0x006DB4EC File Offset: 0x006D96EC
				internal void <.ctor>b__4(UIMouseEvent evt, UIElement e)
				{
					if (this.<>4__this != evt.Target)
					{
						return;
					}
					if (UIWorldGenDebug.spaceWasPressed)
					{
						return;
					}
					if (Main.keyState.PressingAlt())
					{
						this.<>4__this.ToggleHighlight();
						this.<>4__this.SetColorsToHovered();
						return;
					}
					if (!this.pass.Enabled)
					{
						this.parent.RangePassClickEvent(this.<>4__this, delegate(UIWorldGenDebug.GenPassElement x)
						{
							x.Enable();
							x.RefreshColors();
						});
						return;
					}
					if (this.pass.Enabled)
					{
						UIWorldGenDebug.Controller.TryRunToEndOfPass(this.pass, !Main.keyState.PressingShift(), true);
					}
				}

				// Token: 0x06004CF8 RID: 19704 RVA: 0x006DB59D File Offset: 0x006D979D
				internal void <.ctor>b__5(UIMouseEvent evt, UIElement e)
				{
					if (this.pass.Enabled)
					{
						this.parent.RangePassClickEvent(this.<>4__this, delegate(UIWorldGenDebug.GenPassElement x)
						{
							x.Disable();
							x.RefreshColors();
						});
					}
				}

				// Token: 0x040078A8 RID: 30888
				public UIWorldGenDebug.GenPassElement <>4__this;

				// Token: 0x040078A9 RID: 30889
				public GenPass pass;

				// Token: 0x040078AA RID: 30890
				public UIWorldGenDebug parent;
			}

			// Token: 0x02000AE6 RID: 2790
			[CompilerGenerated]
			private sealed class <>c__DisplayClass9_1
			{
				// Token: 0x06004CF9 RID: 19705 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c__DisplayClass9_1()
				{
				}

				// Token: 0x06004CFA RID: 19706 RVA: 0x006DB5DC File Offset: 0x006D97DC
				internal void <.ctor>b__0(UIElement e)
				{
					this.text.TextColor = (this.CS$<>8__locals1.<>4__this.IsRunning ? Color.Yellow : (this.CS$<>8__locals1.<>4__this.Skipped ? Color.DarkGreen : (this.CS$<>8__locals1.<>4__this.HasCompleted ? new Color(0, 230, 0) : ((!this.CS$<>8__locals1.pass.Enabled) ? Color.DarkGray : Color.White))));
					this.text.TextColor *= (this.CS$<>8__locals1.parent.MatchesSearch(this.CS$<>8__locals1.pass) ? 1f : 0.6f);
					this.indexText.TextColor = this.text.TextColor;
				}

				// Token: 0x06004CFB RID: 19707 RVA: 0x006DB6BC File Offset: 0x006D98BC
				internal void <.ctor>b__2(UIElement e)
				{
					if (this.CS$<>8__locals1.<>4__this.Snapshot != null)
					{
						this.snapshotIcon.SetIcon("Images/UI/Camera_4");
					}
					else if (UIWorldGenDebug.Controller.LastCompletedPass == this.CS$<>8__locals1.<>4__this.Pass)
					{
						this.snapshotIcon.SetIcon("Images/UI/Camera_7");
					}
					UIWorldGenDebug.SetButtonState(this.snapshotIcon, (this.CS$<>8__locals1.<>4__this.Pass.Enabled && (this.CS$<>8__locals1.<>4__this.Snapshot != null || UIWorldGenDebug.Controller.LastCompletedPass == this.CS$<>8__locals1.<>4__this.Pass)) ? UIWorldGenDebug.ButtonState.Enabled : UIWorldGenDebug.ButtonState.NotVisible);
					if (this.CS$<>8__locals1.<>4__this.Snapshot != null && this.CS$<>8__locals1.<>4__this.Snapshot.Outdated)
					{
						this.snapshotIcon.IconColor = Color.PaleVioletRed;
						return;
					}
					this.snapshotIcon.IconColor = Color.White;
				}

				// Token: 0x040078AB RID: 30891
				public UIText text;

				// Token: 0x040078AC RID: 30892
				public UIText indexText;

				// Token: 0x040078AD RID: 30893
				public UIWorldGenDebug.UIImageButtonWithExtraIcon snapshotIcon;

				// Token: 0x040078AE RID: 30894
				public UIWorldGenDebug.GenPassElement.<>c__DisplayClass9_0 CS$<>8__locals1;
			}

			// Token: 0x02000AE7 RID: 2791
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06004CFC RID: 19708 RVA: 0x006DB7B7 File Offset: 0x006D99B7
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06004CFD RID: 19709 RVA: 0x0000357B File Offset: 0x0000177B
				public <>c()
				{
				}

				// Token: 0x06004CFE RID: 19710 RVA: 0x006DB7C3 File Offset: 0x006D99C3
				internal void <.ctor>b__9_6(UIWorldGenDebug.GenPassElement x)
				{
					x.Enable();
					x.RefreshColors();
				}

				// Token: 0x06004CFF RID: 19711 RVA: 0x006DB7D1 File Offset: 0x006D99D1
				internal void <.ctor>b__9_7(UIWorldGenDebug.GenPassElement x)
				{
					x.Disable();
					x.RefreshColors();
				}

				// Token: 0x040078AF RID: 30895
				public static readonly UIWorldGenDebug.GenPassElement.<>c <>9 = new UIWorldGenDebug.GenPassElement.<>c();

				// Token: 0x040078B0 RID: 30896
				public static Action<UIWorldGenDebug.GenPassElement> <>9__9_6;

				// Token: 0x040078B1 RID: 30897
				public static Action<UIWorldGenDebug.GenPassElement> <>9__9_7;
			}
		}

		// Token: 0x020008E5 RID: 2277
		private enum ButtonState
		{
			// Token: 0x040073A1 RID: 29601
			Enabled,
			// Token: 0x040073A2 RID: 29602
			NotVisible
		}

		// Token: 0x020008E6 RID: 2278
		[CompilerGenerated]
		private sealed class <>c__DisplayClass38_0
		{
			// Token: 0x060046C5 RID: 18117 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass38_0()
			{
			}

			// Token: 0x060046C6 RID: 18118 RVA: 0x006C9EDC File Offset: 0x006C80DC
			internal void <.ctor>b__0(UIElement e)
			{
				this.progressBar.SetProgress((float)WorldGenerator.CurrentGenerationProgress.TotalProgress, (float)WorldGenerator.CurrentGenerationProgress.Value);
				this.progressMessage.Text = WorldGenerator.CurrentGenerationProgress.Message;
				if (WorldGenerator.CurrentController.QueuedAbort)
				{
					this.progressMessage.Text = Language.GetTextValue("UI.Canceling");
				}
				if (WorldGen.Manifest.GenPassResults.Count != this.<>4__this.LassPassIndex)
				{
					this.<>4__this.LassPassIndex = WorldGen.Manifest.GenPassResults.Count;
					this.<>4__this.EnsurePassVisible(this.<>4__this.LassPassIndex);
				}
			}

			// Token: 0x060046C7 RID: 18119 RVA: 0x006C9F8C File Offset: 0x006C818C
			internal void <.ctor>b__12(UIElement e)
			{
				this.playPauseButton.SetIconFrame(this.playPauseButton.Icon.Frame(1, 2, 0, UIWorldGenDebug.Controller.Paused ? 0 : 1, 0, 0));
			}

			// Token: 0x060046C8 RID: 18120 RVA: 0x006C9FBE File Offset: 0x006C81BE
			internal void <.ctor>b__21(UIElement e)
			{
				this.snapshotFrequencyButton.SetIconFrame(this.snapshotFrequencyButton.Icon.Frame(1, 3, 0, (int)WorldGenerator.CurrentController.SnapshotFrequency, 0, 0));
			}

			// Token: 0x060046C9 RID: 18121 RVA: 0x006C9FEA File Offset: 0x006C81EA
			internal void <.ctor>b__25(UIElement e)
			{
				this.mismatchPauseButton.SetCurrentOption(WorldGenerator.CurrentController.PauseOnHashMismatch);
			}

			// Token: 0x060046CA RID: 18122 RVA: 0x006CA001 File Offset: 0x006C8201
			internal void <.ctor>b__26()
			{
				DebugUtils.QuickSPMessage(this.quickLoadCommand);
			}

			// Token: 0x060046CB RID: 18123 RVA: 0x006CA00E File Offset: 0x006C820E
			internal string <.ctor>b__27()
			{
				return "Save current settings to " + this.quickLoadCommand;
			}

			// Token: 0x040073A3 RID: 29603
			public UIGenProgressBar progressBar;

			// Token: 0x040073A4 RID: 29604
			public UIHeader progressMessage;

			// Token: 0x040073A5 RID: 29605
			public GroupOptionButton<bool> playPauseButton;

			// Token: 0x040073A6 RID: 29606
			public GroupOptionButton<bool> snapshotFrequencyButton;

			// Token: 0x040073A7 RID: 29607
			public GroupOptionButton<bool> mismatchPauseButton;

			// Token: 0x040073A8 RID: 29608
			public string quickLoadCommand;

			// Token: 0x040073A9 RID: 29609
			public UIWorldGenDebug <>4__this;
		}

		// Token: 0x020008E7 RID: 2279
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060046CC RID: 18124 RVA: 0x006CA020 File Offset: 0x006C8220
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060046CD RID: 18125 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x060046CE RID: 18126 RVA: 0x006CA02C File Offset: 0x006C822C
			internal void <.ctor>b__38_1()
			{
				UIWorldGenDebug.Controller.DeleteAllSnapshots();
			}

			// Token: 0x060046CF RID: 18127 RVA: 0x006CA038 File Offset: 0x006C8238
			internal string <.ctor>b__38_2()
			{
				return "Delete all snapshots";
			}

			// Token: 0x060046D0 RID: 18128 RVA: 0x006CA040 File Offset: 0x006C8240
			internal string <.ctor>b__38_3()
			{
				return string.Concat(new object[]
				{
					"Click to clear all snapshots\nEstimated Disk Usage: ",
					WorldGenSnapshot.EstimatedDiskUsage / 1024L / 1024L,
					"MB",
					UIWorldGenDebug.CanSubmitActions ? "" : "\n[c/FFA500:Must be paused to manipulate snapshots]"
				});
			}

			// Token: 0x060046D1 RID: 18129 RVA: 0x006CA099 File Offset: 0x006C8299
			internal void <.ctor>b__38_4()
			{
				UIWorldGenDebug.Controller.TryReset();
			}

			// Token: 0x060046D2 RID: 18130 RVA: 0x006CA0A6 File Offset: 0x006C82A6
			internal string <.ctor>b__38_5()
			{
				return "Reset";
			}

			// Token: 0x060046D3 RID: 18131 RVA: 0x006CA0AD File Offset: 0x006C82AD
			internal string <.ctor>b__38_6()
			{
				if (!UIWorldGenDebug.CanSubmitActions)
				{
					return "[c/FFA500:Must be paused to reset]";
				}
				return null;
			}

			// Token: 0x060046D4 RID: 18132 RVA: 0x006CA0BD File Offset: 0x006C82BD
			internal string <.ctor>b__38_7()
			{
				return "Step Back";
			}

			// Token: 0x060046D5 RID: 18133 RVA: 0x006CA0C4 File Offset: 0x006C82C4
			internal string <.ctor>b__38_8()
			{
				return "Hotkey: Up/Left";
			}

			// Token: 0x060046D6 RID: 18134 RVA: 0x006CA0CB File Offset: 0x006C82CB
			internal void <.ctor>b__38_9()
			{
				WorldGenerator.Controller controller = UIWorldGenDebug.Controller;
				controller.Paused = !controller.Paused;
			}

			// Token: 0x060046D7 RID: 18135 RVA: 0x006CA0E0 File Offset: 0x006C82E0
			internal string <.ctor>b__38_10()
			{
				if (!WorldGenerator.CurrentController.Paused)
				{
					return "Pause";
				}
				return "Play";
			}

			// Token: 0x060046D8 RID: 18136 RVA: 0x006CA0F9 File Offset: 0x006C82F9
			internal string <.ctor>b__38_11()
			{
				return "Hotkey: Space";
			}

			// Token: 0x060046D9 RID: 18137 RVA: 0x006CA100 File Offset: 0x006C8300
			internal string <.ctor>b__38_13()
			{
				return "Step Forward";
			}

			// Token: 0x060046DA RID: 18138 RVA: 0x006CA107 File Offset: 0x006C8307
			internal string <.ctor>b__38_14()
			{
				return "Hotkey: Down/Right";
			}

			// Token: 0x060046DB RID: 18139 RVA: 0x006CA10E File Offset: 0x006C830E
			internal string <.ctor>b__38_16()
			{
				return "Toggle Map";
			}

			// Token: 0x060046DC RID: 18140 RVA: 0x006CA115 File Offset: 0x006C8315
			internal string <.ctor>b__38_17()
			{
				return "Left click to toggle the map display";
			}

			// Token: 0x060046DD RID: 18141 RVA: 0x006CA11C File Offset: 0x006C831C
			internal string <.ctor>b__38_19()
			{
				return "Toggle Chat";
			}

			// Token: 0x060046DE RID: 18142 RVA: 0x006CA123 File Offset: 0x006C8323
			internal string <.ctor>b__38_20()
			{
				return "Left click to toggle the chat log";
			}

			// Token: 0x060046DF RID: 18143 RVA: 0x006CA12A File Offset: 0x006C832A
			internal void <.ctor>b__38_22()
			{
				WorldGenerator.Controller controller = UIWorldGenDebug.Controller;
				controller.PauseOnHashMismatch = !controller.PauseOnHashMismatch;
			}

			// Token: 0x060046E0 RID: 18144 RVA: 0x006CA13F File Offset: 0x006C833F
			internal string <.ctor>b__38_23()
			{
				return "Pause on gen pass change: " + (WorldGenerator.CurrentController.PauseOnHashMismatch ? "On" : "Off");
			}

			// Token: 0x060046E1 RID: 18145 RVA: 0x006CA163 File Offset: 0x006C8363
			internal string <.ctor>b__38_24()
			{
				return "Stop the generator when the output of a pass is different\nto the last time it was run in the save, or current session";
			}

			// Token: 0x060046E2 RID: 18146 RVA: 0x006CA16A File Offset: 0x006C836A
			internal string <.ctor>b__38_28()
			{
				return "Future launches of the game will automatically load the world\nfrom the most recent snapshot, and run to the current pass";
			}

			// Token: 0x060046E3 RID: 18147 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <.ctor>b__38_29()
			{
			}

			// Token: 0x060046E4 RID: 18148 RVA: 0x006CA171 File Offset: 0x006C8371
			internal string <.ctor>b__38_30()
			{
				return "Controls";
			}

			// Token: 0x060046E5 RID: 18149 RVA: 0x006CA178 File Offset: 0x006C8378
			internal void <.ctor>b__38_32()
			{
				UIWorldGenDebug.Controller.QueuedAbort = true;
			}

			// Token: 0x060046E6 RID: 18150 RVA: 0x006CA185 File Offset: 0x006C8385
			internal string <.ctor>b__38_33()
			{
				return "Cancel";
			}

			// Token: 0x060046E7 RID: 18151 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <.ctor>b__38_36(List<UIElement> _)
			{
			}

			// Token: 0x060046E8 RID: 18152 RVA: 0x00009E46 File Offset: 0x00008046
			internal void <Update>b__51_0()
			{
			}

			// Token: 0x060046E9 RID: 18153 RVA: 0x0069AD09 File Offset: 0x00698F09
			internal bool <StepForward>b__55_0(GenPass p)
			{
				return p.Enabled;
			}

			// Token: 0x060046EA RID: 18154 RVA: 0x0069AD09 File Offset: 0x00698F09
			internal bool <TestResetFromPassesAndRegen>b__73_0(GenPass p)
			{
				return p.Enabled;
			}

			// Token: 0x060046EB RID: 18155 RVA: 0x0069AD09 File Offset: 0x00698F09
			internal bool <TestHiddenTileData>b__74_0(GenPass p)
			{
				return p.Enabled;
			}

			// Token: 0x060046EC RID: 18156 RVA: 0x0069AD09 File Offset: 0x00698F09
			internal bool <TestResumeFromSnapshots>b__75_0(GenPass p)
			{
				return p.Enabled;
			}

			// Token: 0x060046ED RID: 18157 RVA: 0x0069AD09 File Offset: 0x00698F09
			internal bool <TestResumeFromSnapshots>b__75_1(GenPass p)
			{
				return p.Enabled;
			}

			// Token: 0x060046EE RID: 18158 RVA: 0x0069AD09 File Offset: 0x00698F09
			internal bool <TestResumeFromSnapshots>b__75_2(GenPass p)
			{
				return p.Enabled;
			}

			// Token: 0x040073AA RID: 29610
			public static readonly UIWorldGenDebug.<>c <>9 = new UIWorldGenDebug.<>c();

			// Token: 0x040073AB RID: 29611
			public static Action <>9__38_1;

			// Token: 0x040073AC RID: 29612
			public static Func<string> <>9__38_2;

			// Token: 0x040073AD RID: 29613
			public static Func<string> <>9__38_3;

			// Token: 0x040073AE RID: 29614
			public static Action <>9__38_4;

			// Token: 0x040073AF RID: 29615
			public static Func<string> <>9__38_5;

			// Token: 0x040073B0 RID: 29616
			public static Func<string> <>9__38_6;

			// Token: 0x040073B1 RID: 29617
			public static Func<string> <>9__38_7;

			// Token: 0x040073B2 RID: 29618
			public static Func<string> <>9__38_8;

			// Token: 0x040073B3 RID: 29619
			public static Action <>9__38_9;

			// Token: 0x040073B4 RID: 29620
			public static Func<string> <>9__38_10;

			// Token: 0x040073B5 RID: 29621
			public static Func<string> <>9__38_11;

			// Token: 0x040073B6 RID: 29622
			public static Func<string> <>9__38_13;

			// Token: 0x040073B7 RID: 29623
			public static Func<string> <>9__38_14;

			// Token: 0x040073B8 RID: 29624
			public static Func<string> <>9__38_16;

			// Token: 0x040073B9 RID: 29625
			public static Func<string> <>9__38_17;

			// Token: 0x040073BA RID: 29626
			public static Func<string> <>9__38_19;

			// Token: 0x040073BB RID: 29627
			public static Func<string> <>9__38_20;

			// Token: 0x040073BC RID: 29628
			public static Action <>9__38_22;

			// Token: 0x040073BD RID: 29629
			public static Func<string> <>9__38_23;

			// Token: 0x040073BE RID: 29630
			public static Func<string> <>9__38_24;

			// Token: 0x040073BF RID: 29631
			public static Func<string> <>9__38_28;

			// Token: 0x040073C0 RID: 29632
			public static Action <>9__38_29;

			// Token: 0x040073C1 RID: 29633
			public static Func<string> <>9__38_30;

			// Token: 0x040073C2 RID: 29634
			public static Action <>9__38_32;

			// Token: 0x040073C3 RID: 29635
			public static Func<string> <>9__38_33;

			// Token: 0x040073C4 RID: 29636
			public static Action<List<UIElement>> <>9__38_36;

			// Token: 0x040073C5 RID: 29637
			public static Action <>9__51_0;

			// Token: 0x040073C6 RID: 29638
			public static Func<GenPass, bool> <>9__55_0;

			// Token: 0x040073C7 RID: 29639
			public static Func<GenPass, bool> <>9__73_0;

			// Token: 0x040073C8 RID: 29640
			public static Func<GenPass, bool> <>9__74_0;

			// Token: 0x040073C9 RID: 29641
			public static Func<GenPass, bool> <>9__75_0;

			// Token: 0x040073CA RID: 29642
			public static Func<GenPass, bool> <>9__75_1;

			// Token: 0x040073CB RID: 29643
			public static Func<GenPass, bool> <>9__75_2;
		}

		// Token: 0x020008E8 RID: 2280
		[CompilerGenerated]
		private sealed class <>c__DisplayClass42_0
		{
			// Token: 0x060046EF RID: 18159 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass42_0()
			{
			}

			// Token: 0x060046F0 RID: 18160 RVA: 0x006CA18C File Offset: 0x006C838C
			internal void <AddButton>b__0(UIMouseEvent evt, UIElement e)
			{
				this.onClick();
			}

			// Token: 0x040073CC RID: 29644
			public Action onClick;
		}

		// Token: 0x020008E9 RID: 2281
		[CompilerGenerated]
		private sealed class <>c__DisplayClass43_0
		{
			// Token: 0x060046F1 RID: 18161 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass43_0()
			{
			}

			// Token: 0x060046F2 RID: 18162 RVA: 0x006CA199 File Offset: 0x006C8399
			internal void <AddImage>b__0(UIMouseEvent evt, UIElement e)
			{
				this.onClick();
			}

			// Token: 0x040073CD RID: 29645
			public Action onClick;
		}

		// Token: 0x020008EA RID: 2282
		[CompilerGenerated]
		private sealed class <>c__DisplayClass70_0
		{
			// Token: 0x060046F3 RID: 18163 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c__DisplayClass70_0()
			{
			}

			// Token: 0x060046F4 RID: 18164 RVA: 0x006CA1A8 File Offset: 0x006C83A8
			internal void <UpdateAndDrawMap>b__0(int x1, int x2, object _)
			{
				bool flag = false;
				int top = this.sectionStripRect.Top;
				int bottom = this.sectionStripRect.Bottom;
				for (int i = x1; i < x2; i++)
				{
					for (int j = top; j < bottom; j++)
					{
						flag |= Main.Map.UpdateLighting(i, j, byte.MaxValue);
					}
				}
				if (flag)
				{
					this.mapUpdate = true;
				}
			}

			// Token: 0x040073CE RID: 29646
			public Rectangle sectionStripRect;

			// Token: 0x040073CF RID: 29647
			public bool mapUpdate;
		}

		// Token: 0x020008EB RID: 2283
		[CompilerGenerated]
		private sealed class <TestSetupResetAndCreateSnapshots>d__72 : IEnumerable<bool>, IEnumerable, IEnumerator<bool>, IDisposable, IEnumerator
		{
			// Token: 0x060046F5 RID: 18165 RVA: 0x006CA20A File Offset: 0x006C840A
			[DebuggerHidden]
			public <TestSetupResetAndCreateSnapshots>d__72(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060046F6 RID: 18166 RVA: 0x00009E46 File Offset: 0x00008046
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060046F7 RID: 18167 RVA: 0x006CA22C File Offset: 0x006C842C
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					UIWorldGenDebug.Controller.TryReset();
					UIWorldGenDebug.Controller.SnapshotFrequency = WorldGenerator.SnapshotFrequency.Always;
					UIWorldGenDebug.Controller.PauseOnHashMismatch = true;
					Main.NewText("Creating Snapshots", byte.MaxValue, 100, 0);
					lastPass = UIWorldGenDebug.Controller.Passes.Last<GenPass>();
					UIWorldGenDebug.Controller.TryRunToEndOfPass(lastPass, false, true);
					this.<>2__current = true;
					this.<>1__state = 1;
					return true;
				case 1:
					this.<>1__state = -1;
					if (UIWorldGenDebug.Controller.LastCompletedPass != lastPass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
					{
						Main.NewText("Test aborted", byte.MaxValue, 0, 0);
						this.<>2__current = false;
						this.<>1__state = 2;
						return true;
					}
					break;
				case 2:
					this.<>1__state = -1;
					break;
				default:
					return false;
				}
				UIWorldGenDebug.Controller.SnapshotFrequency = WorldGenerator.SnapshotFrequency.Manual;
				return false;
			}

			// Token: 0x17000574 RID: 1396
			// (get) Token: 0x060046F8 RID: 18168 RVA: 0x006CA31F File Offset: 0x006C851F
			bool IEnumerator<bool>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060046F9 RID: 18169 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000575 RID: 1397
			// (get) Token: 0x060046FA RID: 18170 RVA: 0x006CA327 File Offset: 0x006C8527
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060046FB RID: 18171 RVA: 0x006CA334 File Offset: 0x006C8534
			[DebuggerHidden]
			IEnumerator<bool> IEnumerable<bool>.GetEnumerator()
			{
				UIWorldGenDebug.<TestSetupResetAndCreateSnapshots>d__72 <TestSetupResetAndCreateSnapshots>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<TestSetupResetAndCreateSnapshots>d__ = this;
				}
				else
				{
					<TestSetupResetAndCreateSnapshots>d__ = new UIWorldGenDebug.<TestSetupResetAndCreateSnapshots>d__72(0);
				}
				return <TestSetupResetAndCreateSnapshots>d__;
			}

			// Token: 0x060046FC RID: 18172 RVA: 0x006CA370 File Offset: 0x006C8570
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Boolean>.GetEnumerator();
			}

			// Token: 0x040073D0 RID: 29648
			private int <>1__state;

			// Token: 0x040073D1 RID: 29649
			private bool <>2__current;

			// Token: 0x040073D2 RID: 29650
			private int <>l__initialThreadId;

			// Token: 0x040073D3 RID: 29651
			private GenPass <lastPass>5__2;
		}

		// Token: 0x020008EC RID: 2284
		[CompilerGenerated]
		private sealed class <TestResetFromPassesAndRegen>d__73 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x060046FD RID: 18173 RVA: 0x006CA378 File Offset: 0x006C8578
			[DebuggerHidden]
			public <TestResetFromPassesAndRegen>d__73(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060046FE RID: 18174 RVA: 0x006CA398 File Offset: 0x006C8598
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060046FF RID: 18175 RVA: 0x006CA3D0 File Offset: 0x006C85D0
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = UIWorldGenDebug.TestSetupResetAndCreateSnapshots().GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -1;
						if (UIWorldGenDebug.Controller.LastCompletedPass != pass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
						{
							Main.NewText("Test aborted", byte.MaxValue, 0, 0);
							return false;
						}
						UIWorldGenDebug.Controller.TryReset();
						UIWorldGenDebug.Controller.TryRunToEndOfPass(lastPass, false, true);
						this.<>2__current = null;
						this.<>1__state = 3;
						return true;
					case 3:
					{
						this.<>1__state = -1;
						if (UIWorldGenDebug.Controller.LastCompletedPass != lastPass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
						{
							Main.NewText("Test aborted", byte.MaxValue, 0, 0);
							return false;
						}
						pass = null;
						int num = i;
						i = num + 1;
						goto IL_0240;
					}
					default:
						return false;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						lastPass = UIWorldGenDebug.Controller.Passes.Last<GenPass>();
						passes = UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled).ToList<GenPass>();
						i = 0;
					}
					else
					{
						if (enumerator.Current)
						{
							this.<>2__current = null;
							this.<>1__state = 1;
							return true;
						}
						flag = false;
						this.<>m__Finally1();
						return flag;
					}
					IL_0240:
					if (i >= passes.Count)
					{
						Main.NewText("Test Completed Successfully", 0, byte.MaxValue, 0);
						flag = false;
					}
					else
					{
						pass = passes[i];
						UIWorldGenDebug.Controller.TryReset();
						Main.NewText(string.Format("[{0}/{1}] Running to {2}", i + 1, passes.Count, pass.Name), byte.MaxValue, 100, 0);
						UIWorldGenDebug.Controller.TryRunToEndOfPass(pass, false, true);
						this.<>2__current = null;
						this.<>1__state = 2;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06004700 RID: 18176 RVA: 0x006CA66C File Offset: 0x006C886C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000576 RID: 1398
			// (get) Token: 0x06004701 RID: 18177 RVA: 0x006CA688 File Offset: 0x006C8888
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004702 RID: 18178 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000577 RID: 1399
			// (get) Token: 0x06004703 RID: 18179 RVA: 0x006CA688 File Offset: 0x006C8888
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004704 RID: 18180 RVA: 0x006CA690 File Offset: 0x006C8890
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				UIWorldGenDebug.<TestResetFromPassesAndRegen>d__73 <TestResetFromPassesAndRegen>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<TestResetFromPassesAndRegen>d__ = this;
				}
				else
				{
					<TestResetFromPassesAndRegen>d__ = new UIWorldGenDebug.<TestResetFromPassesAndRegen>d__73(0);
				}
				return <TestResetFromPassesAndRegen>d__;
			}

			// Token: 0x06004705 RID: 18181 RVA: 0x006CA6CC File Offset: 0x006C88CC
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x040073D4 RID: 29652
			private int <>1__state;

			// Token: 0x040073D5 RID: 29653
			private object <>2__current;

			// Token: 0x040073D6 RID: 29654
			private int <>l__initialThreadId;

			// Token: 0x040073D7 RID: 29655
			private GenPass <lastPass>5__2;

			// Token: 0x040073D8 RID: 29656
			private List<GenPass> <passes>5__3;

			// Token: 0x040073D9 RID: 29657
			private IEnumerator<bool> <>7__wrap3;

			// Token: 0x040073DA RID: 29658
			private int <i>5__5;

			// Token: 0x040073DB RID: 29659
			private GenPass <pass>5__6;
		}

		// Token: 0x020008ED RID: 2285
		[CompilerGenerated]
		private sealed class <TestHiddenTileData>d__74 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06004706 RID: 18182 RVA: 0x006CA6D4 File Offset: 0x006C88D4
			[DebuggerHidden]
			public <TestHiddenTileData>d__74(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06004707 RID: 18183 RVA: 0x006CA6F4 File Offset: 0x006C88F4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						return;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					return;
				default:
					return;
				}
				try
				{
				}
				finally
				{
					this.<>m__Finally2();
				}
			}

			// Token: 0x06004708 RID: 18184 RVA: 0x006CA760 File Offset: 0x006C8960
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = UIWorldGenDebug.TestSetupResetAndCreateSnapshots().GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						if (UIWorldGenDebug.Controller.LastCompletedPass != pass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
						{
							Main.NewText("Test aborted", byte.MaxValue, 0, 0);
							flag = false;
							this.<>m__Finally2();
							return flag;
						}
						pass = null;
						goto IL_016F;
					default:
						return false;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						UIWorldGenDebug.Controller.TryReset();
						enumerator2 = UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled).GetEnumerator();
						this.<>1__state = -4;
					}
					else
					{
						if (enumerator.Current)
						{
							this.<>2__current = null;
							this.<>1__state = 1;
							return true;
						}
						flag = false;
						this.<>m__Finally1();
						return flag;
					}
					IL_016F:
					if (!enumerator2.MoveNext())
					{
						this.<>m__Finally2();
						enumerator2 = null;
						Main.NewText("Test Completed Successfully", 0, byte.MaxValue, 0);
						flag = false;
					}
					else
					{
						pass = enumerator2.Current;
						TileSnapshot.Create(null);
						TileSnapshot.Restore();
						UIWorldGenDebug.Controller.TryRunToEndOfPass(pass, false, true);
						this.<>2__current = null;
						this.<>1__state = 2;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06004709 RID: 18185 RVA: 0x006CA93C File Offset: 0x006C8B3C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x0600470A RID: 18186 RVA: 0x006CA958 File Offset: 0x006C8B58
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x17000578 RID: 1400
			// (get) Token: 0x0600470B RID: 18187 RVA: 0x006CA974 File Offset: 0x006C8B74
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600470C RID: 18188 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000579 RID: 1401
			// (get) Token: 0x0600470D RID: 18189 RVA: 0x006CA974 File Offset: 0x006C8B74
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600470E RID: 18190 RVA: 0x006CA97C File Offset: 0x006C8B7C
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				UIWorldGenDebug.<TestHiddenTileData>d__74 <TestHiddenTileData>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<TestHiddenTileData>d__ = this;
				}
				else
				{
					<TestHiddenTileData>d__ = new UIWorldGenDebug.<TestHiddenTileData>d__74(0);
				}
				return <TestHiddenTileData>d__;
			}

			// Token: 0x0600470F RID: 18191 RVA: 0x006CA9B8 File Offset: 0x006C8BB8
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x040073DC RID: 29660
			private int <>1__state;

			// Token: 0x040073DD RID: 29661
			private object <>2__current;

			// Token: 0x040073DE RID: 29662
			private int <>l__initialThreadId;

			// Token: 0x040073DF RID: 29663
			private IEnumerator<bool> <>7__wrap1;

			// Token: 0x040073E0 RID: 29664
			private IEnumerator<GenPass> <>7__wrap2;

			// Token: 0x040073E1 RID: 29665
			private GenPass <pass>5__4;
		}

		// Token: 0x020008EE RID: 2286
		[CompilerGenerated]
		private sealed class <TestResumeFromSnapshots>d__75 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IDisposable, IEnumerator
		{
			// Token: 0x06004710 RID: 18192 RVA: 0x006CA9C0 File Offset: 0x006C8BC0
			[DebuggerHidden]
			public <TestResumeFromSnapshots>d__75(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06004711 RID: 18193 RVA: 0x006CA9E0 File Offset: 0x006C8BE0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -6:
				case 4:
					goto IL_005B;
				case -5:
				case 3:
					goto IL_0051;
				case -4:
				case 2:
					break;
				case -3:
				case 1:
					try
					{
						return;
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				case -2:
				case -1:
				case 0:
					return;
				default:
					return;
				}
				try
				{
					return;
				}
				finally
				{
					this.<>m__Finally2();
				}
				IL_0051:
				try
				{
					return;
				}
				finally
				{
					this.<>m__Finally3();
				}
				IL_005B:
				try
				{
				}
				finally
				{
					this.<>m__Finally4();
				}
			}

			// Token: 0x06004712 RID: 18194 RVA: 0x006CAA88 File Offset: 0x006C8C88
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
						this.<>1__state = -1;
						enumerator = UIWorldGenDebug.TestSetupResetAndCreateSnapshots().GetEnumerator();
						this.<>1__state = -3;
						break;
					case 1:
						this.<>1__state = -3;
						break;
					case 2:
						this.<>1__state = -4;
						if (UIWorldGenDebug.Controller.LastCompletedPass != pass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
						{
							Main.NewText("Test aborted", byte.MaxValue, 0, 0);
							flag = false;
							this.<>m__Finally2();
							return flag;
						}
						pass = null;
						goto IL_0178;
					case 3:
						this.<>1__state = -5;
						if (UIWorldGenDebug.Controller.LastCompletedPass != lastPass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
						{
							Main.NewText("Test aborted", byte.MaxValue, 0, 0);
							flag = false;
							this.<>m__Finally3();
							return flag;
						}
						goto IL_0271;
					case 4:
						this.<>1__state = -6;
						if (UIWorldGenDebug.Controller.LastCompletedPass != lastPass || UIWorldGenDebug.Controller.PausedDueToHashMismatch)
						{
							Main.NewText("Test aborted", byte.MaxValue, 0, 0);
							flag = false;
							this.<>m__Finally4();
							return flag;
						}
						goto IL_0375;
					default:
						return false;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						lastPass = UIWorldGenDebug.Controller.Passes.Last<GenPass>();
						enumerator2 = UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled).Reverse<GenPass>().GetEnumerator();
						this.<>1__state = -4;
					}
					else
					{
						if (enumerator.Current)
						{
							this.<>2__current = null;
							this.<>1__state = 1;
							return true;
						}
						flag = false;
						this.<>m__Finally1();
						return flag;
					}
					IL_0178:
					if (enumerator2.MoveNext())
					{
						pass = enumerator2.Current;
						UIWorldGenDebug.Controller.TryRunToEndOfPass(pass, true, true);
						this.<>2__current = null;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					Main.NewText("Single pass rerun test completed successfully", 0, byte.MaxValue, 0);
					enumerator2 = UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled).GetEnumerator();
					this.<>1__state = -5;
					IL_0271:
					if (enumerator2.MoveNext())
					{
						GenPass genPass = enumerator2.Current;
						UIWorldGenDebug.Controller.TryResetToSnapshot(genPass);
						UIWorldGenDebug.Controller.TryRunToEndOfPass(lastPass, false, true);
						this.<>2__current = null;
						this.<>1__state = 3;
						return true;
					}
					this.<>m__Finally3();
					enumerator2 = null;
					Main.NewText("Load snapshot and run to end test completed successfully", 0, byte.MaxValue, 0);
					enumerator2 = UIWorldGenDebug.Controller.Passes.Where((GenPass p) => p.Enabled).GetEnumerator();
					this.<>1__state = -6;
					IL_0375:
					if (!enumerator2.MoveNext())
					{
						this.<>m__Finally4();
						enumerator2 = null;
						Main.NewText("Clean load snapshot and run to end test completed successfully", 0, byte.MaxValue, 0);
						flag = false;
					}
					else
					{
						GenPass genPass2 = enumerator2.Current;
						UIWorldGenDebug.Controller.TryReset();
						UIWorldGenDebug.Controller.TryResetToSnapshot(genPass2);
						UIWorldGenDebug.Controller.TryRunToEndOfPass(lastPass, false, true);
						this.<>2__current = null;
						this.<>1__state = 4;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06004713 RID: 18195 RVA: 0x006CAE6C File Offset: 0x006C906C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06004714 RID: 18196 RVA: 0x006CAE88 File Offset: 0x006C9088
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x06004715 RID: 18197 RVA: 0x006CAE88 File Offset: 0x006C9088
			private void <>m__Finally3()
			{
				this.<>1__state = -1;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x06004716 RID: 18198 RVA: 0x006CAE88 File Offset: 0x006C9088
			private void <>m__Finally4()
			{
				this.<>1__state = -1;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x1700057A RID: 1402
			// (get) Token: 0x06004717 RID: 18199 RVA: 0x006CAEA4 File Offset: 0x006C90A4
			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06004718 RID: 18200 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700057B RID: 1403
			// (get) Token: 0x06004719 RID: 18201 RVA: 0x006CAEA4 File Offset: 0x006C90A4
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600471A RID: 18202 RVA: 0x006CAEAC File Offset: 0x006C90AC
			[DebuggerHidden]
			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				UIWorldGenDebug.<TestResumeFromSnapshots>d__75 <TestResumeFromSnapshots>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<TestResumeFromSnapshots>d__ = this;
				}
				else
				{
					<TestResumeFromSnapshots>d__ = new UIWorldGenDebug.<TestResumeFromSnapshots>d__75(0);
				}
				return <TestResumeFromSnapshots>d__;
			}

			// Token: 0x0600471B RID: 18203 RVA: 0x006CAEE8 File Offset: 0x006C90E8
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Object>.GetEnumerator();
			}

			// Token: 0x040073E2 RID: 29666
			private int <>1__state;

			// Token: 0x040073E3 RID: 29667
			private object <>2__current;

			// Token: 0x040073E4 RID: 29668
			private int <>l__initialThreadId;

			// Token: 0x040073E5 RID: 29669
			private GenPass <lastPass>5__2;

			// Token: 0x040073E6 RID: 29670
			private IEnumerator<bool> <>7__wrap2;

			// Token: 0x040073E7 RID: 29671
			private IEnumerator<GenPass> <>7__wrap3;

			// Token: 0x040073E8 RID: 29672
			private GenPass <pass>5__5;
		}
	}
}
