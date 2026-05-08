using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;

namespace Terraria.GameContent.UI
{
	// Token: 0x0200037A RID: 890
	public class WiresUI
	{
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06002970 RID: 10608 RVA: 0x0057BDB4 File Offset: 0x00579FB4
		public static bool Open
		{
			get
			{
				return WiresUI.radial.active;
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x0057BDC0 File Offset: 0x00579FC0
		public static void HandleWiresUI(SpriteBatch spriteBatch)
		{
			WiresUI.radial.Update();
			WiresUI.radial.Draw(spriteBatch);
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x0000357B File Offset: 0x0000177B
		public WiresUI()
		{
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x0057BDD7 File Offset: 0x00579FD7
		// Note: this type is marked as 'beforefieldinit'.
		static WiresUI()
		{
		}

		// Token: 0x040051F4 RID: 20980
		private static WiresUI.WiresRadial radial = new WiresUI.WiresRadial();

		// Token: 0x020008D6 RID: 2262
		public static class Settings
		{
			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x0600467D RID: 18045 RVA: 0x006C71E8 File Offset: 0x006C53E8
			public static bool DrawWires
			{
				get
				{
					return (!Main.noTrapsWorld || NPC.downedBoss3) && (Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].mech || (Main.player[Main.myPlayer].InfoAccMechShowWires && Main.player[Main.myPlayer].builderAccStatus[8] == 0));
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x0600467E RID: 18046 RVA: 0x006C7258 File Offset: 0x006C5458
			public static bool HideWires
			{
				get
				{
					return Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type == 3620;
				}
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x0600467F RID: 18047 RVA: 0x006C7288 File Offset: 0x006C5488
			public static bool DrawToolModeUI
			{
				get
				{
					int type = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
					return type == 3611 || type == 3625;
				}
			}

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x06004680 RID: 18048 RVA: 0x006C72D0 File Offset: 0x006C54D0
			public static bool DrawToolAllowActuators
			{
				get
				{
					int type = Main.player[Main.myPlayer].inventory[Main.player[Main.myPlayer].selectedItem].type;
					if (type == 3611)
					{
						WiresUI.Settings._lastActuatorEnabled = 2;
					}
					if (type == 3625)
					{
						WiresUI.Settings._lastActuatorEnabled = 1;
					}
					return WiresUI.Settings._lastActuatorEnabled == 2;
				}
			}

			// Token: 0x06004681 RID: 18049 RVA: 0x006C7326 File Offset: 0x006C5526
			// Note: this type is marked as 'beforefieldinit'.
			static Settings()
			{
			}

			// Token: 0x04007384 RID: 29572
			public static WiresUI.Settings.MultiToolMode ToolMode = WiresUI.Settings.MultiToolMode.Red;

			// Token: 0x04007385 RID: 29573
			private static int _lastActuatorEnabled;

			// Token: 0x02000AE2 RID: 2786
			[Flags]
			public enum MultiToolMode
			{
				// Token: 0x0400789D RID: 30877
				Red = 1,
				// Token: 0x0400789E RID: 30878
				Green = 2,
				// Token: 0x0400789F RID: 30879
				Blue = 4,
				// Token: 0x040078A0 RID: 30880
				Yellow = 8,
				// Token: 0x040078A1 RID: 30881
				Actuator = 16,
				// Token: 0x040078A2 RID: 30882
				Cutter = 32
			}
		}

		// Token: 0x020008D7 RID: 2263
		public class WiresRadial
		{
			// Token: 0x06004682 RID: 18050 RVA: 0x006C732E File Offset: 0x006C552E
			public void Update()
			{
				this.FlowerUpdate();
				this.LineUpdate();
			}

			// Token: 0x06004683 RID: 18051 RVA: 0x006C733C File Offset: 0x006C553C
			private void LineUpdate()
			{
				bool flag = true;
				float num = 0.75f;
				Player player = Main.player[Main.myPlayer];
				if (!WiresUI.Settings.DrawToolModeUI || Main.drawingPlayerChat)
				{
					flag = false;
					num = 0f;
				}
				if (player.dead || Main.mouseItem.type > 0)
				{
					this._lineOpacity = 0f;
					return;
				}
				if (player.cursorItemIconEnabled && player.cursorItemIconID != 0 && player.cursorItemIconID != 3625)
				{
					this._lineOpacity = 0f;
					return;
				}
				if ((!player.cursorItemIconEnabled && ((!PlayerInput.UsingGamepad && !WiresUI.Settings.DrawToolAllowActuators) || player.mouseInterface || player.lastMouseInterface)) || Main.ingameOptionsWindow || Main.InGameUI.IsVisible)
				{
					this._lineOpacity = 0f;
					return;
				}
				float num2 = Utils.Clamp<float>(this._lineOpacity + 0.05f * (float)flag.ToDirectionInt(), num, 1f);
				this._lineOpacity += 0.05f * (float)Math.Sign(num2 - this._lineOpacity);
				if (Math.Abs(this._lineOpacity - num2) < 0.05f)
				{
					this._lineOpacity = num2;
				}
			}

			// Token: 0x06004684 RID: 18052 RVA: 0x006C7464 File Offset: 0x006C5664
			private void FlowerUpdate()
			{
				Player player = Main.player[Main.myPlayer];
				if (!WiresUI.Settings.DrawToolModeUI)
				{
					this.active = false;
					return;
				}
				if ((player.mouseInterface || player.lastMouseInterface) && !this.OnWiresMenu)
				{
					this.active = false;
					return;
				}
				if (player.dead || Main.mouseItem.type > 0)
				{
					this.active = false;
					this.OnWiresMenu = false;
					return;
				}
				this.OnWiresMenu = false;
				if (Main.mouseRight && Main.mouseRightRelease && !PlayerInput.LockGamepadTileUseButton && player.noThrow == 0 && !Main.HoveringOverAnNPC && player.talkNPC == -1)
				{
					if (this.active)
					{
						this.active = false;
						return;
					}
					if (!Main.SmartInteractShowingGenuine)
					{
						this.active = true;
						this.position = Main.MouseScreen;
						if (PlayerInput.UsingGamepad && Main.SmartCursorWanted)
						{
							this.position = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f;
						}
					}
				}
			}

			// Token: 0x06004685 RID: 18053 RVA: 0x006C755C File Offset: 0x006C575C
			public void Draw(SpriteBatch spriteBatch)
			{
				this.DrawFlower(spriteBatch);
				this.DrawCursorArea(spriteBatch);
			}

			// Token: 0x06004686 RID: 18054 RVA: 0x006C756C File Offset: 0x006C576C
			private void DrawLine(SpriteBatch spriteBatch)
			{
				if (this.active || this._lineOpacity == 0f)
				{
					return;
				}
				Vector2 vector = Main.MouseScreen;
				Vector2 vector2 = new Vector2((float)(Main.screenWidth / 2), (float)(Main.screenHeight - 70));
				if (PlayerInput.UsingGamepad)
				{
					vector = Vector2.Zero;
				}
				Vector2 vector3 = vector - vector2;
				Vector2.Dot(Vector2.Normalize(vector3), Vector2.UnitX);
				Vector2.Dot(Vector2.Normalize(vector3), Vector2.UnitY);
				vector3.ToRotation();
				vector3.Length();
				bool flag = false;
				bool drawToolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
				for (int i = 0; i < 6; i++)
				{
					if (drawToolAllowActuators || i != 5)
					{
						bool flag2 = (WiresUI.Settings.ToolMode & (WiresUI.Settings.MultiToolMode)(1 << i)) > (WiresUI.Settings.MultiToolMode)0;
						if (i == 5)
						{
							flag2 = (WiresUI.Settings.ToolMode & WiresUI.Settings.MultiToolMode.Actuator) > (WiresUI.Settings.MultiToolMode)0;
						}
						Vector2 vector4 = vector2 + Vector2.UnitX * (45f * ((float)i - 1.5f));
						int num = i ?? 3;
						if (i == 3)
						{
							num = 0;
						}
						switch (num)
						{
						case 0:
						case 1:
							vector4 = vector2 + new Vector2((45f + (float)(drawToolAllowActuators ? 15 : 0)) * (float)(2 - num), 0f) * this._lineOpacity;
							break;
						case 2:
						case 3:
							vector4 = vector2 + new Vector2(-(45f + (float)(drawToolAllowActuators ? 15 : 0)) * (float)(num - 1), 0f) * this._lineOpacity;
							break;
						case 4:
							flag2 = false;
							vector4 = vector2 - new Vector2(0f, drawToolAllowActuators ? 22f : 0f) * this._lineOpacity;
							break;
						case 5:
							vector4 = vector2 + new Vector2(0f, 22f) * this._lineOpacity;
							break;
						}
						bool flag3 = false;
						if (!PlayerInput.UsingGamepad)
						{
							flag3 = Vector2.Distance(vector4, vector) < 19f * this._lineOpacity;
						}
						if (flag)
						{
							flag3 = false;
						}
						if (flag3)
						{
							flag = true;
						}
						Texture2D value = TextureAssets.WireUi[(((WiresUI.Settings.ToolMode & WiresUI.Settings.MultiToolMode.Cutter) != (WiresUI.Settings.MultiToolMode)0) ? 8 : 0) + (flag3 ? 1 : 0)].Value;
						Texture2D texture2D = null;
						switch (i)
						{
						case 0:
						case 1:
						case 2:
						case 3:
							texture2D = TextureAssets.WireUi[2 + i].Value;
							break;
						case 4:
							texture2D = TextureAssets.WireUi[((WiresUI.Settings.ToolMode & WiresUI.Settings.MultiToolMode.Cutter) != (WiresUI.Settings.MultiToolMode)0) ? 7 : 6].Value;
							break;
						case 5:
							texture2D = TextureAssets.WireUi[10].Value;
							break;
						}
						Color white = Color.White;
						Color white2 = Color.White;
						if (!flag2 && i != 4)
						{
							if (flag3)
							{
								white2 = new Color(100, 100, 100);
								white2 = new Color(120, 120, 120);
								white = new Color(200, 200, 200);
							}
							else
							{
								white2 = new Color(150, 150, 150);
								white2 = new Color(80, 80, 80);
								white = new Color(100, 100, 100);
							}
						}
						Utils.CenteredRectangle(vector4, new Vector2(40f));
						if (flag3)
						{
							if (Main.mouseLeft && Main.mouseLeftRelease)
							{
								switch (i)
								{
								case 0:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Red;
									break;
								case 1:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Green;
									break;
								case 2:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Blue;
									break;
								case 3:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Yellow;
									break;
								case 4:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Cutter;
									break;
								case 5:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Actuator;
									break;
								}
							}
							if (!Main.mouseLeft || Main.player[Main.myPlayer].mouseInterface)
							{
								Main.player[Main.myPlayer].mouseInterface = true;
							}
							this.OnWiresMenu = true;
						}
						spriteBatch.Draw(value, vector4, null, white * this._lineOpacity, 0f, value.Size() / 2f, this._lineOpacity, SpriteEffects.None, 0f);
						spriteBatch.Draw(texture2D, vector4, null, white2 * this._lineOpacity, 0f, texture2D.Size() / 2f, this._lineOpacity, SpriteEffects.None, 0f);
					}
				}
				if (Main.mouseLeft && Main.mouseLeftRelease && !flag)
				{
					this.active = false;
				}
			}

			// Token: 0x06004687 RID: 18055 RVA: 0x006C79FC File Offset: 0x006C5BFC
			private void DrawFlower(SpriteBatch spriteBatch)
			{
				if (!this.active)
				{
					return;
				}
				Vector2 vector = Main.MouseScreen;
				Vector2 vector2 = this.position;
				if (PlayerInput.UsingGamepad && Main.SmartCursorWanted)
				{
					if (PlayerInput.GamepadThumbstickRight != Vector2.Zero)
					{
						vector = this.position + PlayerInput.GamepadThumbstickRight * 40f;
					}
					else if (PlayerInput.GamepadThumbstickLeft != Vector2.Zero)
					{
						vector = this.position + PlayerInput.GamepadThumbstickLeft * 40f;
					}
					else
					{
						vector = this.position;
					}
				}
				Vector2 vector3 = vector - vector2;
				Vector2.Dot(Vector2.Normalize(vector3), Vector2.UnitX);
				Vector2.Dot(Vector2.Normalize(vector3), Vector2.UnitY);
				float num = vector3.ToRotation();
				float num2 = vector3.Length();
				bool flag = false;
				bool drawToolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
				float num3 = (float)(4 + drawToolAllowActuators.ToInt());
				float num4 = (drawToolAllowActuators ? 11f : (-0.5f));
				for (int i = 0; i < 6; i++)
				{
					if (drawToolAllowActuators || i != 5)
					{
						bool flag2 = (WiresUI.Settings.ToolMode & (WiresUI.Settings.MultiToolMode)(1 << i)) > (WiresUI.Settings.MultiToolMode)0;
						if (i == 5)
						{
							flag2 = (WiresUI.Settings.ToolMode & WiresUI.Settings.MultiToolMode.Actuator) > (WiresUI.Settings.MultiToolMode)0;
						}
						Vector2 vector4 = vector2 + Vector2.UnitX * (45f * ((float)i - 1.5f));
						switch (i)
						{
						case 0:
						case 1:
						case 2:
						case 3:
						{
							float num5 = (float)i;
							if (i == 0)
							{
								num5 = 3f;
							}
							if (i == 3)
							{
								num5 = 0f;
							}
							vector4 = vector2 + Vector2.UnitX.RotatedBy((double)(num5 * 6.2831855f / num3 - 3.1415927f / num4), default(Vector2)) * 45f;
							break;
						}
						case 4:
							flag2 = false;
							vector4 = vector2;
							break;
						case 5:
							vector4 = vector2 + Vector2.UnitX.RotatedBy((double)((float)(i - 1) * 6.2831855f / num3 - 3.1415927f / num4), default(Vector2)) * 45f;
							break;
						}
						bool flag3 = false;
						if (i == 4)
						{
							flag3 = num2 < 20f;
						}
						switch (i)
						{
						case 0:
						case 1:
						case 2:
						case 3:
						case 5:
						{
							float num6 = (vector4 - vector2).ToRotation().AngleTowards(num, 6.2831855f / (num3 * 2f)) - num;
							if (num2 >= 20f && Math.Abs(num6) < 0.01f)
							{
								flag3 = true;
							}
							break;
						}
						case 4:
							flag3 = num2 < 20f;
							break;
						}
						if (!PlayerInput.UsingGamepad)
						{
							flag3 = Vector2.Distance(vector4, vector) < 19f;
						}
						if (flag)
						{
							flag3 = false;
						}
						if (flag3)
						{
							flag = true;
						}
						Texture2D value = TextureAssets.WireUi[(((WiresUI.Settings.ToolMode & WiresUI.Settings.MultiToolMode.Cutter) != (WiresUI.Settings.MultiToolMode)0) ? 8 : 0) + (flag3 ? 1 : 0)].Value;
						Texture2D texture2D = null;
						switch (i)
						{
						case 0:
						case 1:
						case 2:
						case 3:
							texture2D = TextureAssets.WireUi[2 + i].Value;
							break;
						case 4:
							texture2D = TextureAssets.WireUi[((WiresUI.Settings.ToolMode & WiresUI.Settings.MultiToolMode.Cutter) != (WiresUI.Settings.MultiToolMode)0) ? 7 : 6].Value;
							break;
						case 5:
							texture2D = TextureAssets.WireUi[10].Value;
							break;
						}
						Color white = Color.White;
						Color white2 = Color.White;
						if (!flag2 && i != 4)
						{
							if (flag3)
							{
								white2 = new Color(100, 100, 100);
								white2 = new Color(120, 120, 120);
								white = new Color(200, 200, 200);
							}
							else
							{
								white2 = new Color(150, 150, 150);
								white2 = new Color(80, 80, 80);
								white = new Color(100, 100, 100);
							}
						}
						Utils.CenteredRectangle(vector4, new Vector2(40f));
						if (flag3)
						{
							if (Main.mouseLeft && Main.mouseLeftRelease)
							{
								switch (i)
								{
								case 0:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Red;
									break;
								case 1:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Green;
									break;
								case 2:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Blue;
									break;
								case 3:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Yellow;
									break;
								case 4:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Cutter;
									break;
								case 5:
									WiresUI.Settings.ToolMode ^= WiresUI.Settings.MultiToolMode.Actuator;
									break;
								}
							}
							Main.player[Main.myPlayer].mouseInterface = true;
							this.OnWiresMenu = true;
						}
						spriteBatch.Draw(value, vector4, null, white, 0f, value.Size() / 2f, 1f, SpriteEffects.None, 0f);
						spriteBatch.Draw(texture2D, vector4, null, white2, 0f, texture2D.Size() / 2f, 1f, SpriteEffects.None, 0f);
					}
				}
				if (Main.mouseLeft && Main.mouseLeftRelease && !flag)
				{
					this.active = false;
				}
			}

			// Token: 0x06004688 RID: 18056 RVA: 0x006C7F04 File Offset: 0x006C6104
			private void DrawCursorArea(SpriteBatch spriteBatch)
			{
				if (this.active || this._lineOpacity == 0f)
				{
					return;
				}
				Vector2 vector = Main.MouseScreen + new Vector2((float)(10 - 9 * PlayerInput.UsingGamepad.ToInt()), 25f);
				Color color = new Color(50, 50, 50);
				bool drawToolAllowActuators = WiresUI.Settings.DrawToolAllowActuators;
				if (!drawToolAllowActuators)
				{
					if (!PlayerInput.UsingGamepad)
					{
						vector += new Vector2(-20f, 10f);
					}
					else
					{
						vector += new Vector2(0f, 10f);
					}
				}
				Texture2D value = TextureAssets.BuilderAcc.Value;
				Texture2D texture2D = value;
				Rectangle rectangle = new Rectangle(140, 2, 6, 6);
				Rectangle rectangle2 = new Rectangle(148, 2, 6, 6);
				Rectangle rectangle3 = new Rectangle(128, 0, 10, 10);
				float num = 1f;
				float num2 = 1f;
				bool flag = false;
				if (flag && !drawToolAllowActuators)
				{
					num *= Main.cursorScale;
				}
				float num3 = this._lineOpacity;
				if (PlayerInput.UsingGamepad)
				{
					num3 *= Main.GamepadCursorAlpha;
				}
				for (int i = 0; i < 5; i++)
				{
					if (drawToolAllowActuators || i != 4)
					{
						float num4 = num3;
						Vector2 vector2 = vector + Vector2.UnitX * (45f * ((float)i - 1.5f));
						int num5 = i ?? 3;
						if (i == 1)
						{
							num5 = 2;
						}
						if (i == 2)
						{
							num5 = 1;
						}
						if (i == 3)
						{
							num5 = 0;
						}
						if (i == 4)
						{
							num5 = 5;
						}
						int num6 = num5;
						if (num6 == 2)
						{
							num6 = 1;
						}
						else if (num6 == 1)
						{
							num6 = 2;
						}
						bool flag2 = (WiresUI.Settings.ToolMode & (WiresUI.Settings.MultiToolMode)(1 << num6)) > (WiresUI.Settings.MultiToolMode)0;
						if (num6 == 5)
						{
							flag2 = (WiresUI.Settings.ToolMode & WiresUI.Settings.MultiToolMode.Actuator) > (WiresUI.Settings.MultiToolMode)0;
						}
						Color color2 = Color.HotPink;
						switch (num5)
						{
						case 0:
							color2 = new Color(253, 58, 61);
							break;
						case 1:
							color2 = new Color(83, 180, 253);
							break;
						case 2:
							color2 = new Color(83, 253, 153);
							break;
						case 3:
							color2 = new Color(253, 254, 83);
							break;
						case 5:
							color2 = Color.WhiteSmoke;
							break;
						}
						if (!flag2)
						{
							color2 = Color.Lerp(color2, Color.Black, 0.65f);
						}
						if (flag)
						{
							if (drawToolAllowActuators)
							{
								switch (num5)
								{
								case 0:
									vector2 = vector + new Vector2(-12f, 0f) * num;
									break;
								case 1:
									vector2 = vector + new Vector2(-6f, 12f) * num;
									break;
								case 2:
									vector2 = vector + new Vector2(6f, 12f) * num;
									break;
								case 3:
									vector2 = vector + new Vector2(12f, 0f) * num;
									break;
								case 5:
									vector2 = vector + new Vector2(0f, 0f) * num;
									break;
								}
							}
							else
							{
								vector2 = vector + new Vector2((float)(12 * (num5 + 1)), (float)(12 * (3 - num5))) * num;
							}
						}
						else if (drawToolAllowActuators)
						{
							switch (num5)
							{
							case 0:
								vector2 = vector + new Vector2(-12f, 0f) * num;
								break;
							case 1:
								vector2 = vector + new Vector2(-6f, 12f) * num;
								break;
							case 2:
								vector2 = vector + new Vector2(6f, 12f) * num;
								break;
							case 3:
								vector2 = vector + new Vector2(12f, 0f) * num;
								break;
							case 5:
								vector2 = vector + new Vector2(0f, 0f) * num;
								break;
							}
						}
						else
						{
							float num7 = 0.7f;
							switch (num5)
							{
							case 0:
								vector2 = vector + new Vector2(0f, -12f) * num * num7;
								break;
							case 1:
								vector2 = vector + new Vector2(-12f, 0f) * num * num7;
								break;
							case 2:
								vector2 = vector + new Vector2(0f, 12f) * num * num7;
								break;
							case 3:
								vector2 = vector + new Vector2(12f, 0f) * num * num7;
								break;
							}
						}
						vector2 = vector2.Floor();
						spriteBatch.Draw(texture2D, vector2, new Rectangle?(rectangle3), color * num4, 0f, rectangle3.Size() / 2f, num2, SpriteEffects.None, 0f);
						spriteBatch.Draw(value, vector2, new Rectangle?(rectangle), color2 * num4, 0f, rectangle.Size() / 2f, num2, SpriteEffects.None, 0f);
						if ((WiresUI.Settings.ToolMode & WiresUI.Settings.MultiToolMode.Cutter) != (WiresUI.Settings.MultiToolMode)0)
						{
							spriteBatch.Draw(value, vector2, new Rectangle?(rectangle2), color * num4, 0f, rectangle2.Size() / 2f, num2, SpriteEffects.None, 0f);
						}
					}
				}
			}

			// Token: 0x06004689 RID: 18057 RVA: 0x0000357B File Offset: 0x0000177B
			public WiresRadial()
			{
			}

			// Token: 0x04007386 RID: 29574
			public Vector2 position;

			// Token: 0x04007387 RID: 29575
			public bool active;

			// Token: 0x04007388 RID: 29576
			public bool OnWiresMenu;

			// Token: 0x04007389 RID: 29577
			private float _lineOpacity;
		}
	}
}
