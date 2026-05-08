using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020001DB RID: 475
	public class CaptureInterface
	{
		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x0051EE64 File Offset: 0x0051D064
		private static Matrix SelectionZoomMatrix
		{
			get
			{
				CaptureInterface.SelectionContext selectionContext = CaptureInterface._selectionContext;
				if (selectionContext != CaptureInterface.SelectionContext.World)
				{
					return Matrix.Identity;
				}
				return Main.GameViewMatrix.ZoomMatrix;
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0051EE90 File Offset: 0x0051D090
		private static void SetZoom_Context()
		{
			CaptureInterface.SelectionContext selectionContext = CaptureInterface._selectionContext;
			if (selectionContext != CaptureInterface.SelectionContext.World)
			{
				if (selectionContext == CaptureInterface.SelectionContext.Map)
				{
					PlayerInput.SetZoom_Unscaled();
					return;
				}
			}
			else
			{
				PlayerInput.SetZoom_World();
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x0051EEB5 File Offset: 0x0051D0B5
		private static Dictionary<int, CaptureInterface.CaptureInterfaceMode> FillModes()
		{
			return new Dictionary<int, CaptureInterface.CaptureInterfaceMode>
			{
				{
					0,
					new CaptureInterface.ModeEdgeSelection()
				},
				{
					1,
					new CaptureInterface.ModeDragBounds()
				},
				{
					2,
					new CaptureInterface.ModeChangeSettings()
				}
			};
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0051EEE0 File Offset: 0x0051D0E0
		public static Rectangle GetArea()
		{
			int num = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
			int num2 = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
			int num3 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
			int num4 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
			return new Rectangle(num, num2, num3 + 1, num4 + 1);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0051EF64 File Offset: 0x0051D164
		public void Update(CaptureInterface.SelectionContext context)
		{
			CaptureInterface._selectionContext = context;
			if (CaptureInterface.CameraLock)
			{
				return;
			}
			PlayerInput.SetZoom_UI();
			bool toggleCameraMode = PlayerInput.Triggers.Current.ToggleCameraMode;
			if (toggleCameraMode && !this.KeyToggleActiveHeld && (Main.mouseItem.type == 0 || this.Active) && !Main.CaptureModeDisabled && !Main.player[Main.myPlayer].dead && !Main.player[Main.myPlayer].ghost)
			{
				this.ToggleCamera(!this.Active);
			}
			this.KeyToggleActiveHeld = toggleCameraMode;
			if (!this.Active)
			{
				return;
			}
			Main.blockMouse = true;
			if (CaptureInterface.JustActivated && Main.mouseLeftRelease && !Main.mouseLeft)
			{
				CaptureInterface.JustActivated = false;
			}
			Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY);
			if (this.UpdateButtons(vector) && Main.mouseLeft)
			{
				return;
			}
			foreach (KeyValuePair<int, CaptureInterface.CaptureInterfaceMode> keyValuePair in CaptureInterface.Modes)
			{
				keyValuePair.Value.Selected = keyValuePair.Key == this.SelectedMode;
				keyValuePair.Value.Update();
			}
			PlayerInput.SetZoom_Unscaled();
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0051F0AC File Offset: 0x0051D2AC
		public void Draw(SpriteBatch sb)
		{
			if (!this.Active)
			{
				return;
			}
			sb.End();
			sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			PlayerInput.SetZoom_UI();
			foreach (CaptureInterface.CaptureInterfaceMode captureInterfaceMode in CaptureInterface.Modes.Values)
			{
				captureInterfaceMode.Draw(sb);
			}
			sb.End();
			sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			PlayerInput.SetZoom_UI();
			Main.mouseText = false;
			Main.instance.GUIBarsDraw();
			this.DrawButtons(sb);
			Main.instance.DrawMouseOver();
			sb.End();
			sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
			Utils.DrawBorderStringBig(sb, Lang.inter[81].Value, new Vector2((float)Main.screenWidth * 0.5f, 100f), Color.White, 1f, 0.5f, 0.5f, -1);
			Utils.DrawCursorSingle(sb, Main.cursorColor, float.NaN, Main.cursorScale, default(Vector2), 0, 0);
			this.DrawCameraLock(sb);
			sb.End();
			sb.Begin();
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x0051F21C File Offset: 0x0051D41C
		public void ToggleCamera(bool On = true)
		{
			if (CaptureInterface.CameraLock)
			{
				return;
			}
			bool active = this.Active;
			this.Active = CaptureInterface.Modes.ContainsKey(this.SelectedMode) && On;
			if (active != this.Active)
			{
				SoundEngine.PlaySound(On ? 10 : 11, -1, -1, 1, 1f, 0f);
			}
			foreach (KeyValuePair<int, CaptureInterface.CaptureInterfaceMode> keyValuePair in CaptureInterface.Modes)
			{
				keyValuePair.Value.ToggleActive(this.Active && keyValuePair.Key == this.SelectedMode);
			}
			if (On && !active)
			{
				CaptureInterface.JustActivated = true;
			}
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0051F2E8 File Offset: 0x0051D4E8
		private bool UpdateButtons(Vector2 mouse)
		{
			this.HoveredMode = -1;
			bool flag = !Main.graphics.IsFullScreen;
			int num = 9;
			for (int i = 0; i < num; i++)
			{
				Rectangle rectangle = new Rectangle(24 + 46 * i, 24, 42, 42);
				if (rectangle.Contains(mouse.ToPoint()))
				{
					this.HoveredMode = i;
					bool flag2 = Main.mouseLeft && Main.mouseLeftRelease;
					int num2 = 0;
					if (i == num2++ && flag2)
					{
						CaptureInterface.QuickScreenshot();
					}
					if (i == num2++ && flag2 && CaptureInterface.EdgeAPinned && CaptureInterface.EdgeBPinned)
					{
						CaptureSettings captureSettings = new CaptureSettings();
						captureSettings.Area = CaptureInterface.GetArea();
						captureSettings.Biome = CaptureBiome.GetCaptureBiome(CaptureInterface.Settings.BiomeChoiceIndex);
						captureSettings.CaptureBackground = !CaptureInterface.Settings.TransparentBackground;
						captureSettings.CaptureEntities = CaptureInterface.Settings.IncludeEntities;
						captureSettings.UseScaling = CaptureInterface.Settings.PackImage;
						captureSettings.CaptureMech = WiresUI.Settings.DrawWires;
						if (captureSettings.Biome.WaterStyle != 13)
						{
							Main.liquidAlpha[13] = 0f;
						}
						CaptureInterface.StartCamera(captureSettings);
					}
					if (i == num2++ && flag2 && this.SelectedMode != 0)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						this.SelectedMode = 0;
						this.ToggleCamera(true);
					}
					if (i == num2++ && flag2 && this.SelectedMode != 1)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						this.SelectedMode = 1;
						this.ToggleCamera(true);
					}
					if (i == num2++ && flag2)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						CaptureInterface.ResetFocus();
					}
					if (i == num2++ && flag2 && Main.mapEnabled)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						Main.mapFullscreen = !Main.mapFullscreen;
					}
					if (i == num2++ && flag2 && this.SelectedMode != 2)
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						this.SelectedMode = 2;
						this.ToggleCamera(true);
					}
					if (i == num2++ && (flag2 && flag))
					{
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						Utils.OpenFolder(Path.Combine(Main.SavePath, "Captures"));
					}
					if (i == num2++ && flag2)
					{
						this.ToggleCamera(false);
						Main.blockMouse = true;
						Main.mouseLeftRelease = false;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0051F56C File Offset: 0x0051D76C
		public static Rectangle FullScreenArea()
		{
			int screenWidth = Main.screenWidth;
			int screenHeight = Main.screenHeight;
			Main.screenWidth = PlayerInput.RealScreenWidth;
			Main.screenHeight = PlayerInput.RealScreenHeight;
			Rectangle rectangle;
			try
			{
				Vector2 vector = Main.Camera.ScaledPosition / 16f;
				Vector2 vector2 = (Main.Camera.ScaledPosition + Main.Camera.ScaledSize) / 16f;
				Point point = new Point((int)Math.Ceiling((double)vector.X), (int)Math.Ceiling((double)vector.Y));
				Point point2 = new Point((int)Math.Floor((double)vector2.X), (int)Math.Floor((double)vector2.Y));
				rectangle = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
			}
			finally
			{
				Main.screenWidth = screenWidth;
				Main.screenHeight = screenHeight;
			}
			return rectangle;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0051F668 File Offset: 0x0051D868
		public static void QuickScreenshot()
		{
			CaptureInterface.StartCamera(new CaptureSettings
			{
				Area = CaptureInterface.FullScreenArea(),
				Biome = CaptureBiome.GetCaptureBiome(CaptureInterface.Settings.BiomeChoiceIndex),
				CaptureBackground = !CaptureInterface.Settings.TransparentBackground,
				CaptureEntities = CaptureInterface.Settings.IncludeEntities,
				UseScaling = CaptureInterface.Settings.PackImage,
				CaptureMech = WiresUI.Settings.DrawWires
			});
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0051F6CC File Offset: 0x0051D8CC
		private void DrawButtons(SpriteBatch sb)
		{
			new Vector2((float)Main.mouseX, (float)Main.mouseY);
			int num = 9;
			for (int i = 0; i < num; i++)
			{
				Texture2D texture2D = TextureAssets.InventoryBack.Value;
				float num2 = 0.8f;
				Vector2 vector = new Vector2((float)(24 + 46 * i), 24f);
				Color color = Main.inventoryBack * 0.8f;
				if (this.SelectedMode == 0 && i == 2)
				{
					texture2D = TextureAssets.InventoryBack14.Value;
				}
				else if (this.SelectedMode == 1 && i == 3)
				{
					texture2D = TextureAssets.InventoryBack14.Value;
				}
				else if (this.SelectedMode == 2 && i == 6)
				{
					texture2D = TextureAssets.InventoryBack14.Value;
				}
				else if (i >= 2 && i <= 3)
				{
					texture2D = TextureAssets.InventoryBack2.Value;
				}
				sb.Draw(texture2D, vector, null, color, 0f, default(Vector2), num2, SpriteEffects.None, 0f);
				switch (i)
				{
				case 0:
					texture2D = TextureAssets.Camera[7].Value;
					break;
				case 1:
					texture2D = TextureAssets.Camera[0].Value;
					break;
				case 2:
				case 3:
				case 4:
					texture2D = TextureAssets.Camera[i].Value;
					break;
				case 5:
					texture2D = (Main.mapFullscreen ? TextureAssets.MapIcon[0].Value : TextureAssets.MapIcon[4].Value);
					break;
				case 6:
					texture2D = TextureAssets.Camera[1].Value;
					break;
				case 7:
					texture2D = TextureAssets.Camera[6].Value;
					break;
				case 8:
					texture2D = TextureAssets.Camera[5].Value;
					break;
				}
				sb.Draw(texture2D, vector + new Vector2(26f) * num2, null, Color.White, 0f, texture2D.Size() / 2f, 1f, SpriteEffects.None, 0f);
				bool flag = false;
				if (i != 1)
				{
					if (i != 5)
					{
						if (i == 7)
						{
							if (Main.graphics.IsFullScreen)
							{
								flag = true;
							}
						}
					}
					else if (!Main.mapEnabled)
					{
						flag = true;
					}
				}
				else if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
				{
					flag = true;
				}
				if (flag)
				{
					sb.Draw(TextureAssets.Cd.Value, vector + new Vector2(26f) * num2, null, Color.White * 0.65f, 0f, TextureAssets.Cd.Value.Size() / 2f, 1f, SpriteEffects.None, 0f);
				}
			}
			string text = "";
			switch (this.HoveredMode)
			{
			case -1:
				break;
			case 0:
				text = Lang.inter[111].Value;
				break;
			case 1:
				text = Lang.inter[67].Value;
				break;
			case 2:
				text = Lang.inter[69].Value;
				break;
			case 3:
				text = Lang.inter[70].Value;
				break;
			case 4:
				text = Lang.inter[78].Value;
				break;
			case 5:
				text = (Main.mapFullscreen ? Lang.inter[109].Value : Lang.inter[108].Value);
				break;
			case 6:
				text = Lang.inter[68].Value;
				break;
			case 7:
				text = Lang.inter[110].Value;
				break;
			case 8:
				text = Lang.inter[71].Value;
				break;
			default:
				text = "???";
				break;
			}
			int hoveredMode = this.HoveredMode;
			if (hoveredMode != 1)
			{
				if (hoveredMode != 5)
				{
					if (hoveredMode == 7)
					{
						if (Main.graphics.IsFullScreen)
						{
							text = text + "\n" + Lang.inter[113].Value;
						}
					}
				}
				else if (!Main.mapEnabled)
				{
					text = text + "\n" + Lang.inter[114].Value;
				}
			}
			else if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
			{
				text = text + "\n" + Lang.inter[112].Value;
			}
			if (text != "")
			{
				Main.instance.MouseText(text, 0, 0, -1, -1, -1, -1, 0);
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x0051FB04 File Offset: 0x0051DD04
		private static bool GetMapCoords(int PinX, int PinY, int Goal, out Point result)
		{
			if (!Main.mapFullscreen)
			{
				result = new Point(-1, -1);
				return false;
			}
			int num = Main.maxTilesX / MapRenderer.textureMaxWidth;
			int num2 = Main.maxTilesY / MapRenderer.textureMaxHeight;
			float num3 = 10f;
			float num4 = 10f;
			float num5 = (float)(Main.maxTilesX - 10);
			float num6 = (float)(Main.maxTilesY - 10);
			float num7 = Main.mapFullscreenScale;
			float num8 = (float)Main.screenWidth / (float)Main.maxTilesX * 0.8f;
			if (Main.mapFullscreenScale < num8)
			{
				Main.mapFullscreenScale = num8;
			}
			if (Main.mapFullscreenScale > 16f)
			{
				Main.mapFullscreenScale = 16f;
			}
			num7 = Main.mapFullscreenScale;
			if (Main.mapFullscreenPos.X < num3)
			{
				Main.mapFullscreenPos.X = num3;
			}
			if (Main.mapFullscreenPos.X > num5)
			{
				Main.mapFullscreenPos.X = num5;
			}
			if (Main.mapFullscreenPos.Y < num4)
			{
				Main.mapFullscreenPos.Y = num4;
			}
			if (Main.mapFullscreenPos.Y > num6)
			{
				Main.mapFullscreenPos.Y = num6;
			}
			float x = Main.mapFullscreenPos.X;
			float num9 = Main.mapFullscreenPos.Y;
			float num10 = x * num7;
			num9 *= num7;
			float num11 = -num10 + (float)(Main.screenWidth / 2);
			float num12 = -num9 + (float)(Main.screenHeight / 2);
			num11 += num3 * num7;
			num12 += num4 * num7;
			float num13 = (float)(Main.maxTilesX / 840);
			num13 *= Main.mapFullscreenScale;
			float num14 = num11;
			float num15 = num12;
			float num16 = (float)TextureAssets.Map.Width();
			float num17 = (float)TextureAssets.Map.Height();
			if (Main.maxTilesX == 8400)
			{
				num13 *= 0.999f;
				num14 -= 40.6f * num13;
				num15 = num12 - 5f * num13;
				num16 -= 8.045f;
				num16 *= num13;
				num17 += 0.12f;
				num17 *= num13;
				if ((double)num13 < 1.2)
				{
					num17 += 1f;
				}
			}
			else if (Main.maxTilesX == 6400)
			{
				num13 *= 1.09f;
				num14 -= 38.8f * num13;
				num15 = num12 - 3.85f * num13;
				num16 -= 13.6f;
				num16 *= num13;
				num17 -= 6.92f;
				num17 *= num13;
				if ((double)num13 < 1.2)
				{
					num17 += 2f;
				}
			}
			else if (Main.maxTilesX == 6300)
			{
				num13 *= 1.09f;
				num14 -= 39.8f * num13;
				num15 = num12 - 4.08f * num13;
				num16 -= 26.69f;
				num16 *= num13;
				num17 -= 6.92f;
				num17 *= num13;
				if ((double)num13 < 1.2)
				{
					num17 += 2f;
				}
			}
			else if (Main.maxTilesX == 4200)
			{
				num13 *= 0.998f;
				num14 -= 37.3f * num13;
				num15 -= 1.7f * num13;
				num16 -= 16f;
				num16 *= num13;
				num17 -= 8.31f;
				num17 *= num13;
			}
			if (Goal == 0)
			{
				int num18 = (int)((-num11 + (float)PinX) / num7 + num3);
				int num19 = (int)((-num12 + (float)PinY) / num7 + num4);
				bool flag = false;
				if ((float)num18 < num3)
				{
					flag = true;
				}
				if ((float)num18 >= num5)
				{
					flag = true;
				}
				if ((float)num19 < num4)
				{
					flag = true;
				}
				if ((float)num19 >= num6)
				{
					flag = true;
				}
				if (!flag)
				{
					result = new Point(num18, num19);
					return true;
				}
				result = new Point(-1, -1);
				return false;
			}
			else
			{
				if (Goal == 1)
				{
					Vector2 vector = new Vector2(num11, num12);
					Vector2 vector2 = new Vector2((float)PinX, (float)PinY) * num7 - new Vector2(10f * num7);
					result = (vector + vector2).ToPoint();
					return true;
				}
				result = new Point(-1, -1);
				return false;
			}
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x0051FF04 File Offset: 0x0051E104
		private static void ConstraintPoints()
		{
			int num = 40;
			if (CaptureInterface.EdgeAPinned)
			{
				CaptureInterface.PointWorldClamp(ref CaptureInterface.EdgeA, num);
			}
			if (CaptureInterface.EdgeBPinned)
			{
				CaptureInterface.PointWorldClamp(ref CaptureInterface.EdgeB, num);
			}
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x0051FF38 File Offset: 0x0051E138
		private static void PointWorldClamp(ref Point point, int fluff)
		{
			if (point.X < fluff)
			{
				point.X = fluff;
			}
			if (point.X > Main.maxTilesX - 1 - fluff)
			{
				point.X = Main.maxTilesX - 1 - fluff;
			}
			if (point.Y < fluff)
			{
				point.Y = fluff;
			}
			if (point.Y > Main.maxTilesY - 1 - fluff)
			{
				point.Y = Main.maxTilesY - 1 - fluff;
			}
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x0051FFA5 File Offset: 0x0051E1A5
		public bool UsingMap()
		{
			return CaptureInterface.CameraLock || CaptureInterface.Modes[this.SelectedMode].UsingMap();
		}

		// Token: 0x06001FFA RID: 8186 RVA: 0x0051FFC5 File Offset: 0x0051E1C5
		public static void ResetFocus()
		{
			CaptureInterface.EdgeAPinned = false;
			CaptureInterface.EdgeBPinned = false;
			CaptureInterface.EdgeA = new Point(-1, -1);
			CaptureInterface.EdgeB = new Point(-1, -1);
		}

		// Token: 0x06001FFB RID: 8187 RVA: 0x0051FFEC File Offset: 0x0051E1EC
		public void Scrolling()
		{
			int num = PlayerInput.ScrollWheelDelta / 120;
			num %= 30;
			if (num < 0)
			{
				num += 30;
			}
			int selectedMode = this.SelectedMode;
			this.SelectedMode -= num;
			while (this.SelectedMode < 0)
			{
				this.SelectedMode += 2;
			}
			while (this.SelectedMode > 2)
			{
				this.SelectedMode -= 2;
			}
			if (this.SelectedMode != selectedMode)
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			}
		}

		// Token: 0x06001FFC RID: 8188 RVA: 0x00520078 File Offset: 0x0051E278
		public void UpdateCameraCountdown()
		{
			if (CaptureInterface.CameraLock && CaptureInterface.CameraFrame == 4f)
			{
				CaptureManager.Instance.Capture(CaptureInterface.CameraSettings);
			}
			CaptureInterface.CameraFrame += (float)CaptureInterface.CameraLock.ToDirectionInt();
			if (CaptureInterface.CameraFrame < 0f)
			{
				CaptureInterface.CameraFrame = 0f;
			}
			if (CaptureInterface.CameraFrame > 5f)
			{
				CaptureInterface.CameraFrame = 5f;
			}
			if (CaptureInterface.CameraFrame == 5f)
			{
				CaptureInterface.CameraWaiting += 1f;
			}
			if (CaptureInterface.CameraWaiting > 60f)
			{
				CaptureInterface.CameraWaiting = 60f;
			}
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0052011C File Offset: 0x0051E31C
		private void DrawCameraLock(SpriteBatch sb)
		{
			if (CaptureInterface.CameraFrame == 0f)
			{
				return;
			}
			sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Black * (CaptureInterface.CameraFrame / 5f));
			if (CaptureInterface.CameraFrame != 5f)
			{
				return;
			}
			float num = CaptureInterface.CameraWaiting - 60f + 5f;
			if (num <= 0f)
			{
				return;
			}
			num /= 5f;
			float num2 = CaptureManager.Instance.GetProgress() * 100f;
			if (num2 > 100f)
			{
				num2 = 100f;
			}
			string text = num2.ToString("##") + " ";
			string text2 = "/ 100%";
			Vector2 vector = FontAssets.DeathText.Value.MeasureString(text);
			Vector2 vector2 = FontAssets.DeathText.Value.MeasureString(text2);
			Vector2 vector3 = new Vector2(-vector.X, -vector.Y / 2f);
			Vector2 vector4 = new Vector2(0f, -vector2.Y / 2f);
			ChatManager.DrawColorCodedStringWithShadow(sb, FontAssets.DeathText.Value, text, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector3, Color.White * num, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
			ChatManager.DrawColorCodedStringWithShadow(sb, FontAssets.DeathText.Value, text2, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector4, Color.White * num, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x005202F0 File Offset: 0x0051E4F0
		public static void StartCamera(CaptureSettings settings)
		{
			settings.CameraSpaceEffects = CaptureInterface.FullScreenArea().Contains(settings.Area);
			SoundEngine.PlaySound(40, -1, -1, 1, 1f, 0f);
			CaptureInterface.CameraSettings = settings;
			CaptureInterface.CameraLock = true;
			CaptureInterface.CameraWaiting = 0f;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x00520341 File Offset: 0x0051E541
		public static void EndCamera()
		{
			CaptureInterface.CameraLock = false;
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0000357B File Offset: 0x0000177B
		public CaptureInterface()
		{
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x00520349 File Offset: 0x0051E549
		// Note: this type is marked as 'beforefieldinit'.
		static CaptureInterface()
		{
		}

		// Token: 0x04004A61 RID: 19041
		private static CaptureInterface.SelectionContext _selectionContext;

		// Token: 0x04004A62 RID: 19042
		private static Dictionary<int, CaptureInterface.CaptureInterfaceMode> Modes = CaptureInterface.FillModes();

		// Token: 0x04004A63 RID: 19043
		public bool Active;

		// Token: 0x04004A64 RID: 19044
		public static bool JustActivated;

		// Token: 0x04004A65 RID: 19045
		private bool KeyToggleActiveHeld;

		// Token: 0x04004A66 RID: 19046
		public int SelectedMode;

		// Token: 0x04004A67 RID: 19047
		public int HoveredMode;

		// Token: 0x04004A68 RID: 19048
		public static bool EdgeAPinned;

		// Token: 0x04004A69 RID: 19049
		public static bool EdgeBPinned;

		// Token: 0x04004A6A RID: 19050
		public static Point EdgeA;

		// Token: 0x04004A6B RID: 19051
		public static Point EdgeB;

		// Token: 0x04004A6C RID: 19052
		public static bool CameraLock;

		// Token: 0x04004A6D RID: 19053
		private static float CameraFrame;

		// Token: 0x04004A6E RID: 19054
		private static float CameraWaiting;

		// Token: 0x04004A6F RID: 19055
		private const float CameraMaxFrame = 5f;

		// Token: 0x04004A70 RID: 19056
		private const float CameraMaxWait = 60f;

		// Token: 0x04004A71 RID: 19057
		private static CaptureSettings CameraSettings;

		// Token: 0x02000799 RID: 1945
		public static class Settings
		{
			// Token: 0x06004181 RID: 16769 RVA: 0x006BA83C File Offset: 0x006B8A3C
			// Note: this type is marked as 'beforefieldinit'.
			static Settings()
			{
			}

			// Token: 0x0400706A RID: 28778
			public static bool PackImage = true;

			// Token: 0x0400706B RID: 28779
			public static bool IncludeEntities = true;

			// Token: 0x0400706C RID: 28780
			public static bool TransparentBackground;

			// Token: 0x0400706D RID: 28781
			public static int BiomeChoiceIndex = -1;

			// Token: 0x0400706E RID: 28782
			public static int ScreenAnchor = 0;

			// Token: 0x0400706F RID: 28783
			public static Color MarkedAreaColor = new Color(0.8f, 0.8f, 0.8f, 0f) * 0.3f;
		}

		// Token: 0x0200079A RID: 1946
		private abstract class CaptureInterfaceMode
		{
			// Token: 0x06004182 RID: 16770
			public abstract void Update();

			// Token: 0x06004183 RID: 16771
			public abstract void Draw(SpriteBatch sb);

			// Token: 0x06004184 RID: 16772
			public abstract void ToggleActive(bool tickedOn);

			// Token: 0x06004185 RID: 16773
			public abstract bool UsingMap();

			// Token: 0x06004186 RID: 16774 RVA: 0x006BA889 File Offset: 0x006B8A89
			protected void StartDrawingSelection(SpriteBatch sb)
			{
				sb.End();
				sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, CaptureInterface.SelectionZoomMatrix);
				CaptureInterface.SetZoom_Context();
			}

			// Token: 0x06004187 RID: 16775 RVA: 0x006BA8B7 File Offset: 0x006B8AB7
			protected void EndDrawingSelection(SpriteBatch sb)
			{
				sb.End();
				sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
				PlayerInput.SetZoom_UI();
			}

			// Token: 0x06004188 RID: 16776 RVA: 0x0000357B File Offset: 0x0000177B
			protected CaptureInterfaceMode()
			{
			}

			// Token: 0x04007070 RID: 28784
			public bool Selected;
		}

		// Token: 0x0200079B RID: 1947
		public enum SelectionContext
		{
			// Token: 0x04007072 RID: 28786
			World,
			// Token: 0x04007073 RID: 28787
			Map
		}

		// Token: 0x0200079C RID: 1948
		private class ModeEdgeSelection : CaptureInterface.CaptureInterfaceMode
		{
			// Token: 0x06004189 RID: 16777 RVA: 0x006BA8E8 File Offset: 0x006B8AE8
			public override void Update()
			{
				if (!this.Selected)
				{
					return;
				}
				CaptureInterface.SetZoom_Context();
				Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY);
				this.EdgePlacement(vector);
			}

			// Token: 0x0600418A RID: 16778 RVA: 0x006BA91D File Offset: 0x006B8B1D
			public override void Draw(SpriteBatch sb)
			{
				if (!this.Selected)
				{
					return;
				}
				base.StartDrawingSelection(sb);
				this.DrawMarkedArea(sb);
				this.DrawCursors(sb);
				base.EndDrawingSelection(sb);
			}

			// Token: 0x0600418B RID: 16779 RVA: 0x00009E46 File Offset: 0x00008046
			public override void ToggleActive(bool tickedOn)
			{
			}

			// Token: 0x0600418C RID: 16780 RVA: 0x000379E9 File Offset: 0x00035BE9
			public override bool UsingMap()
			{
				return true;
			}

			// Token: 0x0600418D RID: 16781 RVA: 0x006BA944 File Offset: 0x006B8B44
			private void EdgePlacement(Vector2 mouse)
			{
				if (CaptureInterface.JustActivated)
				{
					return;
				}
				Point point;
				if (!Main.mapFullscreen)
				{
					if (Main.mouseLeft)
					{
						CaptureInterface.EdgeAPinned = true;
						CaptureInterface.EdgeA = Main.MouseWorld.ToTileCoordinates();
					}
					if (Main.mouseRight)
					{
						CaptureInterface.EdgeBPinned = true;
						CaptureInterface.EdgeB = Main.MouseWorld.ToTileCoordinates();
					}
				}
				else if (CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out point))
				{
					if (Main.mouseLeft)
					{
						CaptureInterface.EdgeAPinned = true;
						CaptureInterface.EdgeA = point;
					}
					if (Main.mouseRight)
					{
						CaptureInterface.EdgeBPinned = true;
						CaptureInterface.EdgeB = point;
					}
				}
				CaptureInterface.ConstraintPoints();
			}

			// Token: 0x0600418E RID: 16782 RVA: 0x006BA9E0 File Offset: 0x006B8BE0
			private void DrawMarkedArea(SpriteBatch sb)
			{
				if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
				{
					return;
				}
				int num = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
				int num2 = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
				int num3 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
				int num4 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
				if (!Main.mapFullscreen)
				{
					Rectangle rectangle = Main.ReverseGravitySupport(new Rectangle(num * 16, num2 * 16, (num3 + 1) * 16, (num4 + 1) * 16));
					Rectangle rectangle2 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
					Rectangle rectangle3;
					Rectangle.Intersect(ref rectangle2, ref rectangle, out rectangle3);
					if (rectangle3.Width == 0 || rectangle3.Height == 0)
					{
						return;
					}
					rectangle3.Offset(-rectangle2.X, -rectangle2.Y);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle3, CaptureInterface.Settings.MarkedAreaColor);
					for (int i = 0; i < 2; i++)
					{
						sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rectangle3.X, rectangle3.Y + ((i == 1) ? rectangle3.Height : (-2)), rectangle3.Width, 2), Color.White);
						sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rectangle3.X + ((i == 1) ? rectangle3.Width : (-2)), rectangle3.Y, 2, rectangle3.Height), Color.White);
					}
					return;
				}
				else
				{
					Point point;
					CaptureInterface.GetMapCoords(num, num2, 1, out point);
					Point point2;
					CaptureInterface.GetMapCoords(num + num3 + 1, num2 + num4 + 1, 1, out point2);
					Rectangle rectangle4 = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
					Rectangle rectangle5 = new Rectangle(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
					Rectangle rectangle6;
					Rectangle.Intersect(ref rectangle5, ref rectangle4, out rectangle6);
					if (rectangle6.Width == 0 || rectangle6.Height == 0)
					{
						return;
					}
					rectangle6.Offset(-rectangle5.X, -rectangle5.Y);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle6, CaptureInterface.Settings.MarkedAreaColor);
					for (int j = 0; j < 2; j++)
					{
						sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rectangle6.X, rectangle6.Y + ((j == 1) ? rectangle6.Height : (-2)), rectangle6.Width, 2), Color.White);
						sb.Draw(TextureAssets.MagicPixel.Value, new Rectangle(rectangle6.X + ((j == 1) ? rectangle6.Width : (-2)), rectangle6.Y, 2, rectangle6.Height), Color.White);
					}
					return;
				}
			}

			// Token: 0x0600418F RID: 16783 RVA: 0x006BACF4 File Offset: 0x006B8EF4
			private void DrawCursors(SpriteBatch sb)
			{
				float num = 1f / Main.cursorScale;
				float num2 = 0.8f / num;
				Vector2 vector = Main.screenPosition + new Vector2(30f);
				Vector2 vector2 = vector + new Vector2((float)Main.screenWidth, (float)Main.screenHeight) - new Vector2(60f);
				if (Main.mapFullscreen)
				{
					vector -= Main.screenPosition;
					vector2 -= Main.screenPosition;
				}
				Vector3 vector3 = Main.rgbToHsl(Main.cursorColor);
				Color color = Main.hslToRgb((vector3.X + 0.33f) % 1f, vector3.Y, vector3.Z, byte.MaxValue);
				Color color2 = Main.hslToRgb((vector3.X - 0.33f) % 1f, vector3.Y, vector3.Z, byte.MaxValue);
				color2 = (color = Color.White);
				bool flag = Main.player[Main.myPlayer].gravDir == -1f;
				if (!CaptureInterface.EdgeAPinned)
				{
					Utils.DrawCursorSingle(sb, color, 3.926991f, Main.cursorScale * num * num2, new Vector2((float)Main.mouseX - 5f + 12f, (float)Main.mouseY + 2.5f + 12f), 4, 0);
				}
				else
				{
					int num3 = 0;
					float num4 = 0f;
					Vector2 vector4 = Vector2.Zero;
					if (!Main.mapFullscreen)
					{
						Vector2 vector5 = CaptureInterface.EdgeA.ToVector2() * 16f;
						if (!CaptureInterface.EdgeBPinned)
						{
							num3 = 1;
							vector5 += Vector2.One * 8f;
							num4 = (-vector5 + Main.ReverseGravitySupport(new Vector2((float)Main.mouseX, (float)Main.mouseY), 0f) + Main.screenPosition).ToRotation();
							if (flag)
							{
								num4 = -num4;
							}
							vector4 = Vector2.Clamp(vector5, vector, vector2);
							if (vector4 != vector5)
							{
								num4 = (vector5 - vector4).ToRotation();
							}
						}
						else
						{
							Vector2 vector6 = new Vector2((float)((CaptureInterface.EdgeA.X > CaptureInterface.EdgeB.X).ToInt() * 16), (float)((CaptureInterface.EdgeA.Y > CaptureInterface.EdgeB.Y).ToInt() * 16));
							vector5 += vector6;
							vector4 = Vector2.Clamp(vector5, vector, vector2);
							num4 = (CaptureInterface.EdgeB.ToVector2() * 16f + new Vector2(16f) - vector6 - vector4).ToRotation();
							if (vector4 != vector5)
							{
								num4 = (vector5 - vector4).ToRotation();
								num3 = 1;
							}
							if (flag)
							{
								num4 *= -1f;
							}
						}
						Utils.DrawCursorSingle(sb, color, num4 - 1.5707964f, Main.cursorScale * num, Main.ReverseGravitySupport(vector4 - Main.screenPosition, 0f), 4, num3);
					}
					else
					{
						Point edgeA = CaptureInterface.EdgeA;
						if (CaptureInterface.EdgeBPinned)
						{
							int num5 = (CaptureInterface.EdgeA.X > CaptureInterface.EdgeB.X).ToInt();
							int num6 = (CaptureInterface.EdgeA.Y > CaptureInterface.EdgeB.Y).ToInt();
							edgeA.X += num5;
							edgeA.Y += num6;
							CaptureInterface.GetMapCoords(edgeA.X, edgeA.Y, 1, out edgeA);
							Point edgeB = CaptureInterface.EdgeB;
							edgeB.X += 1 - num5;
							edgeB.Y += 1 - num6;
							CaptureInterface.GetMapCoords(edgeB.X, edgeB.Y, 1, out edgeB);
							vector4 = edgeA.ToVector2();
							vector4 = Vector2.Clamp(vector4, vector, vector2);
							num4 = (edgeB.ToVector2() - vector4).ToRotation();
						}
						else
						{
							CaptureInterface.GetMapCoords(edgeA.X, edgeA.Y, 1, out edgeA);
						}
						Utils.DrawCursorSingle(sb, color, num4 - 1.5707964f, Main.cursorScale * num, edgeA.ToVector2(), 4, 0);
					}
				}
				if (!CaptureInterface.EdgeBPinned)
				{
					Utils.DrawCursorSingle(sb, color2, 0.7853981f, Main.cursorScale * num * num2, new Vector2((float)Main.mouseX + 2.5f + 12f, (float)Main.mouseY - 5f + 12f), 5, 0);
					return;
				}
				int num7 = 0;
				float num8 = 0f;
				Vector2 vector7 = Vector2.Zero;
				if (!Main.mapFullscreen)
				{
					Vector2 vector8 = CaptureInterface.EdgeB.ToVector2() * 16f;
					if (!CaptureInterface.EdgeAPinned)
					{
						num7 = 1;
						vector8 += Vector2.One * 8f;
						num8 = (-vector8 + Main.ReverseGravitySupport(new Vector2((float)Main.mouseX, (float)Main.mouseY), 0f) + Main.screenPosition).ToRotation();
						if (flag)
						{
							num8 = -num8;
						}
						vector7 = Vector2.Clamp(vector8, vector, vector2);
						if (vector7 != vector8)
						{
							num8 = (vector8 - vector7).ToRotation();
						}
					}
					else
					{
						Vector2 vector9 = new Vector2((float)((CaptureInterface.EdgeB.X >= CaptureInterface.EdgeA.X).ToInt() * 16), (float)((CaptureInterface.EdgeB.Y >= CaptureInterface.EdgeA.Y).ToInt() * 16));
						vector8 += vector9;
						vector7 = Vector2.Clamp(vector8, vector, vector2);
						num8 = (CaptureInterface.EdgeA.ToVector2() * 16f + new Vector2(16f) - vector9 - vector7).ToRotation();
						if (vector7 != vector8)
						{
							num8 = (vector8 - vector7).ToRotation();
							num7 = 1;
						}
						if (flag)
						{
							num8 *= -1f;
						}
					}
					Utils.DrawCursorSingle(sb, color2, num8 - 1.5707964f, Main.cursorScale * num, Main.ReverseGravitySupport(vector7 - Main.screenPosition, 0f), 5, num7);
					return;
				}
				Point edgeB2 = CaptureInterface.EdgeB;
				if (CaptureInterface.EdgeAPinned)
				{
					int num9 = (CaptureInterface.EdgeB.X >= CaptureInterface.EdgeA.X).ToInt();
					int num10 = (CaptureInterface.EdgeB.Y >= CaptureInterface.EdgeA.Y).ToInt();
					edgeB2.X += num9;
					edgeB2.Y += num10;
					CaptureInterface.GetMapCoords(edgeB2.X, edgeB2.Y, 1, out edgeB2);
					Point edgeA2 = CaptureInterface.EdgeA;
					edgeA2.X += 1 - num9;
					edgeA2.Y += 1 - num10;
					CaptureInterface.GetMapCoords(edgeA2.X, edgeA2.Y, 1, out edgeA2);
					vector7 = edgeB2.ToVector2();
					vector7 = Vector2.Clamp(vector7, vector, vector2);
					num8 = (edgeA2.ToVector2() - vector7).ToRotation();
				}
				else
				{
					CaptureInterface.GetMapCoords(edgeB2.X, edgeB2.Y, 1, out edgeB2);
				}
				Utils.DrawCursorSingle(sb, color2, num8 - 1.5707964f, Main.cursorScale * num, edgeB2.ToVector2(), 5, 0);
			}

			// Token: 0x06004190 RID: 16784 RVA: 0x006BB445 File Offset: 0x006B9645
			public ModeEdgeSelection()
			{
			}
		}

		// Token: 0x0200079D RID: 1949
		private class ModeDragBounds : CaptureInterface.CaptureInterfaceMode
		{
			// Token: 0x06004191 RID: 16785 RVA: 0x006BB450 File Offset: 0x006B9650
			public override void Update()
			{
				if (!this.Selected)
				{
					return;
				}
				if (CaptureInterface.JustActivated)
				{
					return;
				}
				CaptureInterface.SetZoom_Context();
				Vector2 vector = new Vector2((float)Main.mouseX, (float)Main.mouseY);
				this.DragBounds(vector);
			}

			// Token: 0x06004192 RID: 16786 RVA: 0x006BB48D File Offset: 0x006B968D
			public override void Draw(SpriteBatch sb)
			{
				if (!this.Selected)
				{
					return;
				}
				base.StartDrawingSelection(sb);
				this.DrawMarkedArea(sb);
				base.EndDrawingSelection(sb);
			}

			// Token: 0x06004193 RID: 16787 RVA: 0x006BB4AD File Offset: 0x006B96AD
			public override void ToggleActive(bool tickedOn)
			{
				if (!tickedOn)
				{
					this.currentAim = -1;
				}
			}

			// Token: 0x06004194 RID: 16788 RVA: 0x006BB4B9 File Offset: 0x006B96B9
			public override bool UsingMap()
			{
				return this.caughtEdge != -1;
			}

			// Token: 0x06004195 RID: 16789 RVA: 0x006BB4C8 File Offset: 0x006B96C8
			private void DragBounds(Vector2 mouse)
			{
				if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
				{
					bool flag = false;
					if (Main.mouseLeft)
					{
						flag = true;
					}
					if (flag)
					{
						bool flag2 = true;
						Point point;
						if (!Main.mapFullscreen)
						{
							point = (Main.screenPosition + mouse).ToTileCoordinates();
						}
						else
						{
							flag2 = CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out point);
						}
						if (flag2)
						{
							if (!CaptureInterface.EdgeAPinned)
							{
								CaptureInterface.EdgeAPinned = true;
								CaptureInterface.EdgeA = point;
							}
							if (!CaptureInterface.EdgeBPinned)
							{
								CaptureInterface.EdgeBPinned = true;
								CaptureInterface.EdgeB = point;
							}
						}
						this.currentAim = 3;
						this.caughtEdge = 1;
					}
				}
				int num = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
				int num2 = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
				int num3 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
				int num4 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
				bool flag3 = Main.player[Main.myPlayer].gravDir == -1f;
				int num5 = 1 - flag3.ToInt();
				int num6 = flag3.ToInt();
				Rectangle rectangle;
				Rectangle rectangle2;
				if (!Main.mapFullscreen)
				{
					rectangle = Main.ReverseGravitySupport(new Rectangle(num * 16, num2 * 16, (num3 + 1) * 16, (num4 + 1) * 16));
					rectangle2 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
					Rectangle rectangle3;
					Rectangle.Intersect(ref rectangle2, ref rectangle, out rectangle3);
					if (rectangle3.Width == 0 || rectangle3.Height == 0)
					{
						return;
					}
					rectangle3.Offset(-rectangle2.X, -rectangle2.Y);
				}
				else
				{
					Point point2;
					CaptureInterface.GetMapCoords(num, num2, 1, out point2);
					Point point3;
					CaptureInterface.GetMapCoords(num + num3 + 1, num2 + num4 + 1, 1, out point3);
					rectangle = new Rectangle(point2.X, point2.Y, point3.X - point2.X, point3.Y - point2.Y);
					rectangle2 = new Rectangle(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
					Rectangle rectangle3;
					Rectangle.Intersect(ref rectangle2, ref rectangle, out rectangle3);
					if (rectangle3.Width == 0 || rectangle3.Height == 0)
					{
						return;
					}
					rectangle3.Offset(-rectangle2.X, -rectangle2.Y);
				}
				this.dragging = false;
				if (!Main.mouseLeft)
				{
					this.currentAim = -1;
				}
				if (this.currentAim != -1)
				{
					this.dragging = true;
					Point point4 = default(Point);
					if (!Main.mapFullscreen)
					{
						point4 = Main.MouseWorld.ToTileCoordinates();
					}
					else
					{
						Point point5;
						if (!CaptureInterface.GetMapCoords((int)mouse.X, (int)mouse.Y, 0, out point5))
						{
							return;
						}
						point4 = point5;
					}
					int num7 = this.currentAim;
					if (num7 > 1)
					{
						if (num7 - 2 <= 1)
						{
							if (this.caughtEdge == 0)
							{
								CaptureInterface.EdgeA.X = point4.X;
							}
							if (this.caughtEdge == 1)
							{
								CaptureInterface.EdgeB.X = point4.X;
							}
						}
					}
					else
					{
						if (this.caughtEdge == 0)
						{
							CaptureInterface.EdgeA.Y = point4.Y;
						}
						if (this.caughtEdge == 1)
						{
							CaptureInterface.EdgeB.Y = point4.Y;
						}
					}
				}
				else
				{
					this.caughtEdge = -1;
					Rectangle rectangle4 = rectangle;
					rectangle4.Offset(-rectangle2.X, -rectangle2.Y);
					this.inMap = rectangle4.Contains(mouse.ToPoint());
					int i = 0;
					while (i < 4)
					{
						Rectangle bound = this.GetBound(rectangle4, i);
						bound.Inflate(8, 8);
						if (bound.Contains(mouse.ToPoint()))
						{
							this.currentAim = i;
							if (i == 0)
							{
								if (CaptureInterface.EdgeA.Y < CaptureInterface.EdgeB.Y)
								{
									this.caughtEdge = num6;
									break;
								}
								this.caughtEdge = num5;
								break;
							}
							else if (i == 1)
							{
								if (CaptureInterface.EdgeA.Y >= CaptureInterface.EdgeB.Y)
								{
									this.caughtEdge = num6;
									break;
								}
								this.caughtEdge = num5;
								break;
							}
							else if (i == 2)
							{
								if (CaptureInterface.EdgeA.X < CaptureInterface.EdgeB.X)
								{
									this.caughtEdge = 0;
									break;
								}
								this.caughtEdge = 1;
								break;
							}
							else
							{
								if (i != 3)
								{
									break;
								}
								if (CaptureInterface.EdgeA.X >= CaptureInterface.EdgeB.X)
								{
									this.caughtEdge = 0;
									break;
								}
								this.caughtEdge = 1;
								break;
							}
						}
						else
						{
							i++;
						}
					}
				}
				CaptureInterface.ConstraintPoints();
			}

			// Token: 0x06004196 RID: 16790 RVA: 0x006BB960 File Offset: 0x006B9B60
			private Rectangle GetBound(Rectangle drawbox, int boundIndex)
			{
				if (boundIndex == 0)
				{
					return new Rectangle(drawbox.X, drawbox.Y - 2, drawbox.Width, 2);
				}
				if (boundIndex == 1)
				{
					return new Rectangle(drawbox.X, drawbox.Y + drawbox.Height, drawbox.Width, 2);
				}
				if (boundIndex == 2)
				{
					return new Rectangle(drawbox.X - 2, drawbox.Y, 2, drawbox.Height);
				}
				if (boundIndex == 3)
				{
					return new Rectangle(drawbox.X + drawbox.Width, drawbox.Y, 2, drawbox.Height);
				}
				return Rectangle.Empty;
			}

			// Token: 0x06004197 RID: 16791 RVA: 0x006BB9F8 File Offset: 0x006B9BF8
			public void DrawMarkedArea(SpriteBatch sb)
			{
				if (!CaptureInterface.EdgeAPinned || !CaptureInterface.EdgeBPinned)
				{
					return;
				}
				int num = Math.Min(CaptureInterface.EdgeA.X, CaptureInterface.EdgeB.X);
				int num2 = Math.Min(CaptureInterface.EdgeA.Y, CaptureInterface.EdgeB.Y);
				int num3 = Math.Abs(CaptureInterface.EdgeA.X - CaptureInterface.EdgeB.X);
				int num4 = Math.Abs(CaptureInterface.EdgeA.Y - CaptureInterface.EdgeB.Y);
				Rectangle rectangle3;
				if (!Main.mapFullscreen)
				{
					Rectangle rectangle = Main.ReverseGravitySupport(new Rectangle(num * 16, num2 * 16, (num3 + 1) * 16, (num4 + 1) * 16));
					Rectangle rectangle2 = Main.ReverseGravitySupport(new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y, Main.screenWidth + 1, Main.screenHeight + 1));
					Rectangle.Intersect(ref rectangle2, ref rectangle, out rectangle3);
					if (rectangle3.Width == 0 || rectangle3.Height == 0)
					{
						return;
					}
					rectangle3.Offset(-rectangle2.X, -rectangle2.Y);
				}
				else
				{
					Point point;
					CaptureInterface.GetMapCoords(num, num2, 1, out point);
					Point point2;
					CaptureInterface.GetMapCoords(num + num3 + 1, num2 + num4 + 1, 1, out point2);
					Rectangle rectangle = new Rectangle(point.X, point.Y, point2.X - point.X, point2.Y - point.Y);
					Rectangle rectangle2 = new Rectangle(0, 0, Main.screenWidth + 1, Main.screenHeight + 1);
					Rectangle.Intersect(ref rectangle2, ref rectangle, out rectangle3);
					if (rectangle3.Width == 0 || rectangle3.Height == 0)
					{
						return;
					}
					rectangle3.Offset(-rectangle2.X, -rectangle2.Y);
				}
				sb.Draw(TextureAssets.MagicPixel.Value, rectangle3, CaptureInterface.Settings.MarkedAreaColor);
				Rectangle empty = Rectangle.Empty;
				for (int i = 0; i < 2; i++)
				{
					if (this.currentAim != i)
					{
						this.DrawBound(sb, new Rectangle(rectangle3.X, rectangle3.Y + ((i == 1) ? rectangle3.Height : (-2)), rectangle3.Width, 2), 0);
					}
					else
					{
						empty = new Rectangle(rectangle3.X, rectangle3.Y + ((i == 1) ? rectangle3.Height : (-2)), rectangle3.Width, 2);
					}
					if (this.currentAim != i + 2)
					{
						this.DrawBound(sb, new Rectangle(rectangle3.X + ((i == 1) ? rectangle3.Width : (-2)), rectangle3.Y, 2, rectangle3.Height), 0);
					}
					else
					{
						empty = new Rectangle(rectangle3.X + ((i == 1) ? rectangle3.Width : (-2)), rectangle3.Y, 2, rectangle3.Height);
					}
				}
				if (empty != Rectangle.Empty)
				{
					this.DrawBound(sb, empty, 1 + this.dragging.ToInt());
				}
			}

			// Token: 0x06004198 RID: 16792 RVA: 0x006BBCE8 File Offset: 0x006B9EE8
			private void DrawBound(SpriteBatch sb, Rectangle r, int mode)
			{
				if (mode == 0)
				{
					sb.Draw(TextureAssets.MagicPixel.Value, r, Color.Silver);
					return;
				}
				if (mode == 1)
				{
					Rectangle rectangle = new Rectangle(r.X - 2, r.Y, r.Width + 4, r.Height);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle, Color.White);
					rectangle = new Rectangle(r.X, r.Y - 2, r.Width, r.Height + 4);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle, Color.White);
					sb.Draw(TextureAssets.MagicPixel.Value, r, Color.White);
					return;
				}
				if (mode == 2)
				{
					Rectangle rectangle2 = new Rectangle(r.X - 2, r.Y, r.Width + 4, r.Height);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle2, Color.Gold);
					rectangle2 = new Rectangle(r.X, r.Y - 2, r.Width, r.Height + 4);
					sb.Draw(TextureAssets.MagicPixel.Value, rectangle2, Color.Gold);
					sb.Draw(TextureAssets.MagicPixel.Value, r, Color.Gold);
				}
			}

			// Token: 0x06004199 RID: 16793 RVA: 0x006BBE2E File Offset: 0x006BA02E
			public ModeDragBounds()
			{
			}

			// Token: 0x04007074 RID: 28788
			public int currentAim = -1;

			// Token: 0x04007075 RID: 28789
			private bool dragging;

			// Token: 0x04007076 RID: 28790
			private int caughtEdge = -1;

			// Token: 0x04007077 RID: 28791
			private bool inMap;
		}

		// Token: 0x0200079E RID: 1950
		private class ModeChangeSettings : CaptureInterface.CaptureInterfaceMode
		{
			// Token: 0x0600419A RID: 16794 RVA: 0x006BBE44 File Offset: 0x006BA044
			private Rectangle GetRect()
			{
				Rectangle rectangle = new Rectangle(0, 0, 224, 170);
				if (CaptureInterface.Settings.ScreenAnchor == 0)
				{
					rectangle.X = 227 - rectangle.Width / 2;
					rectangle.Y = 80;
				}
				return rectangle;
			}

			// Token: 0x0600419B RID: 16795 RVA: 0x006BBE8C File Offset: 0x006BA08C
			private void ButtonDraw(int button, ref string key, ref string value)
			{
				switch (button)
				{
				case 0:
					key = Lang.inter[74].Value;
					value = Lang.inter[73 - CaptureInterface.Settings.PackImage.ToInt()].Value;
					return;
				case 1:
					key = Lang.inter[75].Value;
					value = Lang.inter[73 - CaptureInterface.Settings.IncludeEntities.ToInt()].Value;
					return;
				case 2:
					key = Lang.inter[76].Value;
					value = Lang.inter[73 - (!CaptureInterface.Settings.TransparentBackground).ToInt()].Value;
					return;
				case 3:
				case 4:
				case 5:
					break;
				case 6:
					key = "      " + Lang.menu[86].Value;
					value = "";
					break;
				default:
					return;
				}
			}

			// Token: 0x0600419C RID: 16796 RVA: 0x006BBF60 File Offset: 0x006BA160
			private void PressButton(int button)
			{
				bool flag = false;
				switch (button)
				{
				case 0:
					CaptureInterface.Settings.PackImage = !CaptureInterface.Settings.PackImage;
					flag = true;
					break;
				case 1:
					CaptureInterface.Settings.IncludeEntities = !CaptureInterface.Settings.IncludeEntities;
					flag = true;
					break;
				case 2:
					CaptureInterface.Settings.TransparentBackground = !CaptureInterface.Settings.TransparentBackground;
					flag = true;
					break;
				case 6:
					CaptureInterface.Settings.PackImage = true;
					CaptureInterface.Settings.IncludeEntities = true;
					CaptureInterface.Settings.TransparentBackground = false;
					CaptureInterface.Settings.BiomeChoiceIndex = -1;
					flag = true;
					break;
				}
				if (flag)
				{
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			}

			// Token: 0x0600419D RID: 16797 RVA: 0x006BBFF8 File Offset: 0x006BA1F8
			private void DrawWaterChoices(SpriteBatch spritebatch, Point start, Point mouse)
			{
				Rectangle rectangle = new Rectangle(0, 0, 20, 20);
				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < 7; j++)
					{
						if (i != 1 || j != 6)
						{
							int num = j + i * 7;
							rectangle.X = start.X + 24 * j + 12 * i;
							rectangle.Y = start.Y + 24 * i;
							int num2 = num;
							int num3 = 0;
							if (rectangle.Contains(mouse))
							{
								if (Main.mouseLeft && Main.mouseLeftRelease)
								{
									SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
									CaptureInterface.Settings.BiomeChoiceIndex = num2;
								}
								Main.instance.MouseText(Language.GetTextValue("CaptureBiomeChoice." + num2), 0, 0, -1, -1, -1, -1, 0);
								num3++;
							}
							if (CaptureInterface.Settings.BiomeChoiceIndex == num2)
							{
								num3 += 2;
							}
							Texture2D value = TextureAssets.Extra[130].Value;
							int num4 = num * 18;
							Color white = Color.White;
							float num5 = 1f;
							if (num3 < 2)
							{
								num5 *= 0.5f;
							}
							if (num3 % 2 == 1)
							{
								spritebatch.Draw(TextureAssets.MagicPixel.Value, rectangle.TopLeft(), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.Gold, 0f, Vector2.Zero, new Vector2(20f), SpriteEffects.None, 0f);
							}
							else
							{
								spritebatch.Draw(TextureAssets.MagicPixel.Value, rectangle.TopLeft(), new Rectangle?(new Rectangle(0, 0, 1, 1)), Color.White * num5, 0f, Vector2.Zero, new Vector2(20f), SpriteEffects.None, 0f);
							}
							spritebatch.Draw(value, rectangle.TopLeft() + new Vector2(2f), new Rectangle?(new Rectangle(num4, 0, 16, 16)), Color.White * num5);
						}
					}
				}
			}

			// Token: 0x0600419E RID: 16798 RVA: 0x006BC1E4 File Offset: 0x006BA3E4
			private int UnnecessaryBiomeSelectionTypeConversion(int index)
			{
				switch (index)
				{
				case 0:
					return -1;
				case 1:
					return 0;
				case 2:
					return 2;
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
					return index;
				case 9:
					return 10;
				case 10:
					return 12;
				case 11:
					return 13;
				case 12:
					return 14;
				default:
					return 0;
				}
			}

			// Token: 0x0600419F RID: 16799 RVA: 0x006BC244 File Offset: 0x006BA444
			public override void Update()
			{
				if (!this.Selected)
				{
					return;
				}
				if (CaptureInterface.JustActivated)
				{
					return;
				}
				PlayerInput.SetZoom_UI();
				Point point = new Point(Main.mouseX, Main.mouseY);
				this.hoveredButton = -1;
				Rectangle rect = this.GetRect();
				this.inUI = rect.Contains(point);
				rect.Inflate(-20, -20);
				rect.Height = 16;
				int y = rect.Y;
				for (int i = 0; i < 7; i++)
				{
					rect.Y = y + i * 20;
					if (rect.Contains(point))
					{
						this.hoveredButton = i;
						break;
					}
				}
				if (Main.mouseLeft && Main.mouseLeftRelease && this.hoveredButton != -1)
				{
					this.PressButton(this.hoveredButton);
				}
			}

			// Token: 0x060041A0 RID: 16800 RVA: 0x006BC300 File Offset: 0x006BA500
			public override void Draw(SpriteBatch sb)
			{
				if (!this.Selected)
				{
					return;
				}
				base.StartDrawingSelection(sb);
				((CaptureInterface.ModeDragBounds)CaptureInterface.Modes[1]).currentAim = -1;
				((CaptureInterface.ModeDragBounds)CaptureInterface.Modes[1]).DrawMarkedArea(sb);
				base.EndDrawingSelection(sb);
				Rectangle rect = this.GetRect();
				Utils.DrawInvBG(sb, rect, new Color(63, 65, 151, 255) * 0.485f);
				for (int i = 0; i < 7; i++)
				{
					string text = "";
					string text2 = "";
					this.ButtonDraw(i, ref text, ref text2);
					Color color = Color.White;
					if (i == this.hoveredButton)
					{
						color = Color.Gold;
					}
					ChatManager.DrawColorCodedStringWithShadow(sb, FontAssets.ItemStack.Value, text, rect.TopLeft() + new Vector2(20f, (float)(20 + 20 * i)), color, 0f, Vector2.Zero, Vector2.One, -1f, 2f);
					ChatManager.DrawColorCodedStringWithShadow(sb, FontAssets.ItemStack.Value, text2, rect.TopRight() + new Vector2(-20f, (float)(20 + 20 * i)), color, 0f, FontAssets.ItemStack.Value.MeasureString(text2) * Vector2.UnitX, Vector2.One, -1f, 2f);
				}
				this.DrawWaterChoices(sb, (rect.TopLeft() + new Vector2((float)(rect.Width / 2 - 84), 90f)).ToPoint(), Main.MouseScreen.ToPoint());
			}

			// Token: 0x060041A1 RID: 16801 RVA: 0x006BC499 File Offset: 0x006BA699
			public override void ToggleActive(bool tickedOn)
			{
				if (tickedOn)
				{
					this.hoveredButton = -1;
				}
			}

			// Token: 0x060041A2 RID: 16802 RVA: 0x006BC4A5 File Offset: 0x006BA6A5
			public override bool UsingMap()
			{
				return this.inUI;
			}

			// Token: 0x060041A3 RID: 16803 RVA: 0x006BC4AD File Offset: 0x006BA6AD
			public ModeChangeSettings()
			{
			}

			// Token: 0x04007078 RID: 28792
			private const int ButtonsCount = 7;

			// Token: 0x04007079 RID: 28793
			private int hoveredButton = -1;

			// Token: 0x0400707A RID: 28794
			private bool inUI;
		}
	}
}
