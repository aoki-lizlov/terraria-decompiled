using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.UI.Elements;
using Terraria.Graphics.Renderers;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x0200039C RID: 924
	public class UIWorldCreationAdvancedSecretSeedsList : UIState, IHaveBackButtonCommand
	{
		// Token: 0x06002A17 RID: 10775 RVA: 0x00580902 File Offset: 0x0057EB02
		public UIWorldCreationAdvancedSecretSeedsList(UIWorldCreationAdvanced state, UIWorldCreation state2)
		{
			this._creationState = state;
			this._creationState2 = state2;
			this.BuildPage();
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x00580934 File Offset: 0x0057EB34
		private void BuildPage()
		{
			this.SeedDust.Clear();
			this.SeedParticleSystem.Clear();
			base.RemoveAllChildren();
			UIElement uielement = new UIElement
			{
				Width = StyleDimension.FromPixels(500f),
				Height = StyleDimension.FromPixelsAndPercent(-200f, 1f),
				Top = StyleDimension.FromPixels(202f),
				HAlign = 0.5f,
				VAlign = 0f
			};
			uielement.MaxHeight = StyleDimension.FromPixels(400f);
			uielement.SetPadding(0f);
			base.Append(uielement);
			UIPanel uipanel = new UIPanel
			{
				Width = StyleDimension.FromPercent(1f),
				Height = StyleDimension.FromPixelsAndPercent(-102f, 1f),
				BackgroundColor = new Color(33, 43, 79) * 0.8f
			};
			uipanel.SetPadding(0f);
			uielement.Append(uipanel);
			this.MakeBackAndCreatebuttons(uielement);
			int num = 56;
			int num2 = 4;
			UIElement uielement2 = new UIElement
			{
				Top = StyleDimension.FromPixelsAndPercent((float)num2, 0f),
				Width = StyleDimension.FromPixelsAndPercent(-20f, 1f),
				Left = StyleDimension.FromPixelsAndPercent(2f, 0f),
				Height = StyleDimension.FromPixelsAndPercent((float)(-(float)num2 - num), 1f),
				HAlign = 0.5f
			};
			uielement2.SetPadding(0f);
			uielement2.PaddingTop = 8f;
			uielement2.PaddingBottom = 12f;
			uipanel.Append(uielement2);
			this._worldList = new UIList();
			this._worldList.Width.Set(0f, 1f);
			this._worldList.Height.Set(0f, 1f);
			this._worldList.ListPadding = 5f;
			uielement2.Append(this._worldList);
			this._containerPanel = uielement2;
			this._scrollbar = new UIScrollbar(UIScrollbar.ColorTheme.Blue);
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._worldList.SetScrollbar(this._scrollbar);
			List<WorldGen.SecretSeed> seedsForInterface = SecretSeedsTracker.SeedsForInterface;
			this._worldList.ManualSortMethod = new Action<List<UIElement>>(this.CustomSort);
			int num3 = 0;
			foreach (WorldGen.SecretSeed secretSeed in seedsForInterface)
			{
				GroupOptionButton<WorldGen.SecretSeed> groupOptionButton = new GroupOptionButton<WorldGen.SecretSeed>(secretSeed, null, Language.GetText(secretSeed.Localization), Color.White, null, 1f, 0.5f, 10f)
				{
					Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
					Height = new StyleDimension(40f, 0f),
					HAlign = 0f
				};
				groupOptionButton.SetSnapPoint("Seed", num3++, null, null);
				UIElement uielement3 = new UIElement();
				groupOptionButton.Append(uielement3);
				groupOptionButton.SetTextWithoutLocalization(secretSeed.TextThatWasUsedToUnlock, 1f, Color.White, 0f, 10f);
				groupOptionButton.OnLeftMouseDown += this.ClickSecretSeed;
				groupOptionButton.OnMouseOver += this.MouseOverSeed;
				groupOptionButton.OnMouseOut += this.MouseOutSeed;
				groupOptionButton.SetCurrentOption(secretSeed.Enabled ? secretSeed : null);
				uielement3.OnDraw += this.DrawGlowRing;
				this._worldList.Add(groupOptionButton);
			}
			UIElement uielement4 = new UIElement
			{
				Width = StyleDimension.FromPixelsAndPercent(-20f, 1f),
				Height = StyleDimension.FromPixelsAndPercent((float)(num + num2), 0f),
				HAlign = 0.5f,
				VAlign = 1f
			};
			uielement4.SetPadding(0f);
			uielement4.PaddingBottom = 12f;
			uipanel.Append(uielement4);
			this.AddDescriptionPanel(uielement4, (float)num, "desc");
		}

		// Token: 0x06002A19 RID: 10777 RVA: 0x00580D8C File Offset: 0x0057EF8C
		private void AddDescriptionPanel(UIElement container, float accumulatedHeight, string tagGroup)
		{
			float num = 0f;
			UISlicedImage uislicedImage = new UISlicedImage(Main.Assets.Request<Texture2D>("Images/UI/CharCreation/CategoryPanelHighlight", 1))
			{
				HAlign = 0.5f,
				VAlign = 1f,
				Width = StyleDimension.FromPixelsAndPercent(-num * 2f, 1f),
				Left = StyleDimension.FromPixels(-num),
				Height = StyleDimension.FromPixelsAndPercent(accumulatedHeight, 0f),
				Top = StyleDimension.FromPixels(2f)
			};
			uislicedImage.SetSliceDepths(10);
			uislicedImage.Color = Color.LightGray * 0.7f;
			container.Append(uislicedImage);
			UIText uitext = new UIText(Language.GetText("UI.WorldDescriptionDefault"), 0.7f, false)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = StyleDimension.FromPixelsAndPercent(2f, 0f)
			};
			uitext.IsWrapped = true;
			uitext.PaddingLeft = 20f;
			uitext.PaddingRight = 20f;
			uitext.PaddingTop = 4f;
			uislicedImage.Append(uitext);
			this._descriptionText = uitext;
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x00580ED6 File Offset: 0x0057F0D6
		public void MouseOutSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			this.ClearOptionDescription(evt, listeningElement);
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x00580EE0 File Offset: 0x0057F0E0
		public void MouseOverSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<WorldGen.SecretSeed> groupOptionButton = evt.Target as GroupOptionButton<WorldGen.SecretSeed>;
			if (groupOptionButton == null)
			{
				return;
			}
			bool isSelected = groupOptionButton.IsSelected;
			if (Main.mouseLeft)
			{
				listeningElement.LeftMouseDown(evt);
			}
			this.ShowOptionDescription(evt, listeningElement);
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x00580F1C File Offset: 0x0057F11C
		public void DrawGlowRing(UIElement listeningElement, SpriteBatch spriteBatch)
		{
			GroupOptionButton<WorldGen.SecretSeed> groupOptionButton = (GroupOptionButton<WorldGen.SecretSeed>)listeningElement.Parent;
			if (groupOptionButton.OptionValue.Enabled)
			{
				Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/WorldCreation/IconRandomSeed", 1);
				CalculatedStyle dimensions = groupOptionButton.GetDimensions();
				Vector2 vector = dimensions.ToRectangle().TopRight() + new Vector2(-22f, 22f);
				Texture2D value = asset.Value;
				Rectangle rectangle = new Rectangle(0, 0, 4, 4);
				Vector2 vector2 = new Vector2((float)value.Width * 0.45f, (float)value.Height * 0.95f);
				float num = 0.25f * (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 1.3f + dimensions.Position().Y));
				vector2 = rectangle.Size() / 2f;
				float num2 = 1.5f;
				float num3 = num2 + 1f;
				Math.Sin((double)(Main.GlobalTimeWrappedHourly * 1.3f + dimensions.Position().Y * 0.00153178f));
				num2 = 1f;
				num = 0f;
				rectangle = value.Frame(1, 1, 0, 0, 0, 0);
				vector2 = rectangle.Size() / 2f;
				spriteBatch.Draw(value, vector, new Rectangle?(rectangle), Color.White, num, vector2, num2, SpriteEffects.None, 0f);
			}
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x00581076 File Offset: 0x0057F276
		private void CustomSort(List<UIElement> items)
		{
			items.Sort(delegate(UIElement a, UIElement b)
			{
				GroupOptionButton<WorldGen.SecretSeed> groupOptionButton = a as GroupOptionButton<WorldGen.SecretSeed>;
				GroupOptionButton<WorldGen.SecretSeed> groupOptionButton2 = b as GroupOptionButton<WorldGen.SecretSeed>;
				if (groupOptionButton != null && groupOptionButton2 == null)
				{
					return -1;
				}
				if (groupOptionButton == null && groupOptionButton2 != null)
				{
					return 1;
				}
				return groupOptionButton.OptionValue.TextThatWasUsedToUnlock.CompareTo(groupOptionButton2.OptionValue.TextThatWasUsedToUnlock);
			});
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x005810A0 File Offset: 0x0057F2A0
		private void ClickSecretSeed(UIMouseEvent evt, UIElement listeningElement)
		{
			GroupOptionButton<WorldGen.SecretSeed> groupOptionButton = (GroupOptionButton<WorldGen.SecretSeed>)listeningElement;
			WorldGen.SecretSeed optionValue = groupOptionButton.OptionValue;
			if (optionValue.Enabled)
			{
				groupOptionButton.SetCurrentOption(null);
				WorldGen.SecretSeed.Disable(optionValue);
				this._creationState2.RemoveSeedFromSeedMenu(optionValue.TextThatWasUsedToUnlock);
				return;
			}
			groupOptionButton.SetCurrentOption(optionValue);
			WorldGen.SecretSeed.Enable(optionValue, true);
			this._creationState2.AddSeedFromSeedmenu(optionValue.TextThatWasUsedToUnlock);
			this.SpawnParticles(groupOptionButton);
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x00581108 File Offset: 0x0057F308
		private void SpawnParticles(GroupOptionButton<WorldGen.SecretSeed> element)
		{
			CalculatedStyle dimensions = element.GetDimensions();
			dimensions.Center();
			this.Spawn_RainbowRodHit(new ParticleOrchestraSettings
			{
				PositionInWorld = dimensions.Position() + new Vector2(dimensions.Width - 20f, dimensions.Height / 2f),
				MovementVector = new Vector2(0f, 16f) + Main.rand.NextVector2Circular(10f, 2f)
			});
			float num = 8f;
			int num2 = 0;
			while ((float)num2 < num + 1f)
			{
				this.Spawn_BestReforge(new ParticleOrchestraSettings
				{
					PositionInWorld = dimensions.Position() + new Vector2(0f, dimensions.Height / 2f) + new Vector2(dimensions.Width * (1f / num) * (float)num2, 0f)
				});
				num2++;
			}
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00581204 File Offset: 0x0057F404
		private void Spawn_RainbowRodHit(ParticleOrchestraSettings settings)
		{
			float num = Main.rand.NextFloat() * 6.2831855f;
			float num2 = 6f;
			float num3 = Main.rand.NextFloat();
			for (float num4 = 0f; num4 < 1f; num4 += 1f / num2)
			{
				Vector2 vector = settings.MovementVector * Main.rand.NextFloatDirection() * 0.15f;
				Vector2 vector2 = new Vector2(Main.rand.NextFloat() * 0.4f + 0.4f);
				float num5 = num + Main.rand.NextFloat() * 6.2831855f;
				float num6 = 1.5707964f;
				Vector2 vector3 = 1.5f * vector2;
				float num7 = 60f;
				Vector2 vector4 = Main.rand.NextVector2Circular(8f, 8f) * vector2;
				PrettySparkleParticle prettySparkleParticle = new PrettySparkleParticle();
				prettySparkleParticle.Velocity = num5.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = num5.ToRotationVector2() * -(vector3 / num7) - vector * 1f / 60f;
				prettySparkleParticle.ColorTint = Main.hslToRgb((num3 + Main.rand.NextFloat() * 0.33f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				prettySparkleParticle.ColorTint.A = 0;
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + vector4;
				prettySparkleParticle.Rotation = num6;
				prettySparkleParticle.Scale = vector2;
				this.SeedParticleSystem.Add(prettySparkleParticle);
				prettySparkleParticle = new PrettySparkleParticle();
				prettySparkleParticle.Velocity = num5.ToRotationVector2() * vector3 + vector;
				prettySparkleParticle.AccelerationPerFrame = num5.ToRotationVector2() * -(vector3 / num7) - vector * 1f / 60f;
				prettySparkleParticle.ColorTint = new Color(255, 255, 255, 0);
				prettySparkleParticle.LocalPosition = settings.PositionInWorld + vector4;
				prettySparkleParticle.Rotation = num6;
				prettySparkleParticle.Scale = vector2 * 0.6f;
				this.SeedParticleSystem.Add(prettySparkleParticle);
			}
			for (int i = 0; i < 12; i++)
			{
				Color color = Main.hslToRgb((num3 + Main.rand.NextFloat() * 0.12f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				Dust dust = this.SeedDust.NewDust(settings.PositionInWorld, 0, 0, 267, 0f, 0f, 0, color, 1f);
				dust.velocity = Main.rand.NextVector2Circular(1f, 1f);
				dust.velocity += settings.MovementVector * Main.rand.NextFloatDirection() * 0.5f;
				dust.noGravity = true;
				dust.scale = 0.6f + Main.rand.NextFloat() * 0.9f;
				dust.fadeIn = 0.7f + Main.rand.NextFloat() * 0.8f;
				if (dust.dustIndex != 200 && dust.type != 0)
				{
					Dust dust2 = this.SeedDust.CloneDust(dust);
					dust2.scale /= 2f;
					dust2.fadeIn *= 0.75f;
					dust2.color = new Color(255, 255, 255, 255);
				}
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x005815F0 File Offset: 0x0057F7F0
		private void Spawn_BestReforge(ParticleOrchestraSettings settings)
		{
			Vector2 vector = new Vector2(0f, 0.16350001f);
			Asset<Texture2D> asset = Main.Assets.Request<Texture2D>("Images/UI/Creative/Research_Spark", 1);
			for (int i = 0; i < 2; i++)
			{
				Vector2 vector2 = Main.rand.NextVector2Circular(3f, 4f);
				Vector2 vector3 = new Vector2(0f, Main.rand.NextFloatDirection() * 20f);
				this.SeedParticleSystem.Add(new CreativeSacrificeParticle(asset, null, settings.MovementVector + vector2, settings.PositionInWorld + vector3)
				{
					AccelerationPerFrame = vector,
					ScaleOffsetPerFrame = -0.016666668f
				});
			}
			float num = Main.rand.NextFloat();
			for (int j = 0; j < 3; j++)
			{
				Color color = Main.hslToRgb((num + Main.rand.NextFloat() * 0.12f) % 1f, 1f, 0.4f + Main.rand.NextFloat() * 0.25f, byte.MaxValue);
				Dust dust = this.SeedDust.NewDust(settings.PositionInWorld, 0, 0, 267, 0f, 0f, 0, color, 1f);
				dust.velocity = Main.rand.NextVector2Circular(1f, 1f);
				dust.velocity += settings.MovementVector * Main.rand.NextFloatDirection() * 0.5f;
				dust.noGravity = true;
				dust.scale = 0.6f + Main.rand.NextFloat() * 0.9f;
				dust.fadeIn = 0.7f + Main.rand.NextFloat() * 0.8f;
				Vector2 vector4 = new Vector2(0f, Main.rand.NextFloatDirection() * 20f);
				dust.position += vector4;
				if (dust.dustIndex != 200 && dust.type != 0)
				{
					Dust dust2 = this.SeedDust.CloneDust(dust);
					dust2.scale /= 2f;
					dust2.fadeIn *= 0.75f;
					dust2.color = new Color(255, 255, 255, 255);
				}
			}
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x00581858 File Offset: 0x0057FA58
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._containerPanel.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._worldList.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._containerPanel.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._worldList.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x00581908 File Offset: 0x0057FB08
		private void MakeBackAndCreatebuttons(UIElement outerContainer)
		{
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Apply"), 0.65f, true)
			{
				Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
				Height = StyleDimension.FromPixels(50f),
				VAlign = 1f,
				HAlign = 0.5f,
				Top = StyleDimension.FromPixels(-43f)
			};
			uitextPanel.OnMouseOver += this.FadedMouseOver;
			uitextPanel.OnMouseOut += this.FadedMouseOut;
			uitextPanel.OnLeftMouseDown += this.Click_GoBack;
			uitextPanel.SetSnapPoint("Back", 0, null, null);
			outerContainer.Append(uitextPanel);
			this._backButton = uitextPanel;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x005819D8 File Offset: 0x0057FBD8
		private void Click_GoBack(UIMouseEvent evt, UIElement listeningElement)
		{
			this.GoBack();
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x005819E0 File Offset: 0x0057FBE0
		private void GoBack()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.MenuUI.SetState(this._creationState);
			this._creationState.RefreshSecretSeedButton();
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x00581A14 File Offset: 0x0057FC14
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
			this.ShowOptionDescription(evt, listeningElement);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x00581A74 File Offset: 0x0057FC74
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
			this.ClearOptionDescription(evt, listeningElement);
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x005819D8 File Offset: 0x0057FBD8
		public void HandleBackButtonUsage()
		{
			this.GoBack();
		}

		// Token: 0x06002A29 RID: 10793 RVA: 0x00581AC6 File Offset: 0x0057FCC6
		public void ClearOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			this._descriptionText.SetText(Language.GetText("UI.WorldDescriptionDefault"));
		}

		// Token: 0x06002A2A RID: 10794 RVA: 0x00581AE0 File Offset: 0x0057FCE0
		public void ShowOptionDescription(UIMouseEvent evt, UIElement listeningElement)
		{
			LocalizedText localizedText = null;
			GroupOptionButton<WorldGen.SecretSeed> groupOptionButton = listeningElement as GroupOptionButton<WorldGen.SecretSeed>;
			if (groupOptionButton != null)
			{
				localizedText = groupOptionButton.Description;
			}
			if (localizedText != null)
			{
				this._descriptionText.SetText(localizedText);
			}
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x00581B0F File Offset: 0x0057FD0F
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
			this.DrawSeedSystems(spriteBatch);
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x00581B26 File Offset: 0x0057FD26
		public void DrawSeedSystems(SpriteBatch spriteBatch)
		{
			this.SeedDust.UpdateDust();
			this.SeedDust.DrawDust();
			this.SeedParticleSystem.Update();
			this.SeedParticleSystem.Draw(spriteBatch);
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x00581B58 File Offset: 0x0057FD58
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			int num = 3000;
			int num2 = num;
			this.GetSnapPoints();
			UILinkPoint linkPoint = this._helper.GetLinkPoint(num2++, this._backButton);
			List<SnapPoint> snapPoints = this._worldList.GetSnapPoints();
			UILinkPoint[,] array = this._helper.CreateUILinkPointGrid(ref num2, snapPoints, 1, null, null, null, linkPoint);
			UILinkPoint uilinkPoint = array[0, array.GetLength(1) - 1];
			this._helper.PairUpDown(uilinkPoint, linkPoint);
			this._helper.MoveToVisuallyClosestPoint(num, num2);
		}

		// Token: 0x040052E9 RID: 21225
		private UIWorldCreationAdvanced _creationState;

		// Token: 0x040052EA RID: 21226
		private UIElement _backButton;

		// Token: 0x040052EB RID: 21227
		private UIList _worldList;

		// Token: 0x040052EC RID: 21228
		private UIElement _containerPanel;

		// Token: 0x040052ED RID: 21229
		private UIScrollbar _scrollbar;

		// Token: 0x040052EE RID: 21230
		private bool _isScrollbarAttached;

		// Token: 0x040052EF RID: 21231
		private UIWorldCreation _creationState2;

		// Token: 0x040052F0 RID: 21232
		private ParticleRenderer SeedParticleSystem = new ParticleRenderer();

		// Token: 0x040052F1 RID: 21233
		private UIDust SeedDust = new UIDust();

		// Token: 0x040052F2 RID: 21234
		private UIText _descriptionText;

		// Token: 0x040052F3 RID: 21235
		private UIGamepadHelper _helper;

		// Token: 0x020008E0 RID: 2272
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600469B RID: 18075 RVA: 0x006C888E File Offset: 0x006C6A8E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600469C RID: 18076 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x0600469D RID: 18077 RVA: 0x006C889C File Offset: 0x006C6A9C
			internal int <CustomSort>b__16_0(UIElement a, UIElement b)
			{
				GroupOptionButton<WorldGen.SecretSeed> groupOptionButton = a as GroupOptionButton<WorldGen.SecretSeed>;
				GroupOptionButton<WorldGen.SecretSeed> groupOptionButton2 = b as GroupOptionButton<WorldGen.SecretSeed>;
				if (groupOptionButton != null && groupOptionButton2 == null)
				{
					return -1;
				}
				if (groupOptionButton == null && groupOptionButton2 != null)
				{
					return 1;
				}
				return groupOptionButton.OptionValue.TextThatWasUsedToUnlock.CompareTo(groupOptionButton2.OptionValue.TextThatWasUsedToUnlock);
			}

			// Token: 0x04007393 RID: 29587
			public static readonly UIWorldCreationAdvancedSecretSeedsList.<>c <>9 = new UIWorldCreationAdvancedSecretSeedsList.<>c();

			// Token: 0x04007394 RID: 29588
			public static Comparison<UIElement> <>9__16_0;
		}
	}
}
