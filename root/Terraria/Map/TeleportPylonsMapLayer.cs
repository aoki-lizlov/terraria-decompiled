using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.Map
{
	// Token: 0x02000180 RID: 384
	public class TeleportPylonsMapLayer : IMapLayer
	{
		// Token: 0x06001E48 RID: 7752 RVA: 0x005044E4 File Offset: 0x005026E4
		public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color col, float width)
		{
			float num = Vector2.Distance(start, end);
			float num2 = (end - start).ToRotation();
			int num3 = Math.Min(5, (int)num);
			Rectangle rectangle = TextureAssets.BlackTile.Value.Frame(1, 1, 0, 0, 0, 0);
			for (int i = 0; i < num3; i++)
			{
				spriteBatch.Draw(TextureAssets.BlackTile.Value, Vector2.Lerp(start, end, (float)i / (float)num3), new Rectangle?(rectangle), col, num2, new Vector2(0f, (float)rectangle.Width * 0.5f), new Vector2(num / (float)num3 / 16f, width / 16f), SpriteEffects.None, 0f);
			}
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00504590 File Offset: 0x00502790
		public void Draw(ref MapOverlayDrawContext context, ref string text)
		{
			List<TeleportPylonInfo> pylons = Main.PylonSystem.Pylons;
			float num = 1f;
			float num2 = num * 2f;
			float num3 = num * 0.5f;
			Texture2D value = TextureAssets.Extra[182].Value;
			Texture2D value2 = TextureAssets.Extra[299].Value;
			Color color = Color.White;
			if (!TeleportPylonsSystem.IsPlayerNearAPylon(Main.LocalPlayer))
			{
				color = Color.Gray * 0.5f;
			}
			bool flag = false;
			int num4 = -1;
			if (Main.mapFullscreen && Main.MapPylonTile.X != -1 && Main.MapPylonTile.Y != -1)
			{
				Texture2D texture2D = value;
				Vector2 vector = Main.MapPylonTile.ToVector2() + new Vector2(1.5f, 2f);
				SpriteFrame spriteFrame = new SpriteFrame(11, 1, 0, 0)
				{
					PaddingY = 0
				};
				Point center = context.GetUnclampedDrawRegion(texture2D, vector, spriteFrame, num, Alignment.Center).Center;
				for (int i = 0; i < pylons.Count; i++)
				{
					TeleportPylonInfo teleportPylonInfo = pylons[i];
					if (TeleportPylonsMapLayer.IsRevealed(teleportPylonInfo) && !(teleportPylonInfo.PositionInTiles == Main.MapPylonTile))
					{
						Texture2D texture2D2 = value;
						Vector2 vector2 = teleportPylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f);
						spriteFrame = new SpriteFrame(11, 1, 0, 0)
						{
							PaddingY = 0
						};
						Point center2 = context.GetUnclampedDrawRegion(texture2D2, vector2, spriteFrame, num, Alignment.Center).Center;
						TeleportPylonsMapLayer.DrawLine(Main.spriteBatch, center.ToVector2(), center2.ToVector2(), Color.Black, 6f);
						TeleportPylonsMapLayer.DrawLine(Main.spriteBatch, center.ToVector2(), center2.ToVector2(), Color.White, 2f);
					}
				}
			}
			for (int j = 0; j < pylons.Count; j++)
			{
				TeleportPylonInfo teleportPylonInfo2 = pylons[j];
				if (TeleportPylonsMapLayer.IsRevealed(teleportPylonInfo2))
				{
					bool flag2 = true;
					MapOverlayDrawContext.DrawResult drawResult;
					if (Main.mapFullscreen)
					{
						Texture2D texture2D3 = value;
						Texture2D texture2D4 = value2;
						Vector2 vector3 = teleportPylonInfo2.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f);
						Color color2 = color;
						SpriteFrame spriteFrame = new SpriteFrame(11, 1, (byte)teleportPylonInfo2.TypeOfPylon, 0)
						{
							PaddingY = 0
						};
						drawResult = context.DrawClamped(texture2D3, texture2D4, vector3, color2, spriteFrame, num, num2, num3, Alignment.Center, 10, out flag2);
					}
					else
					{
						Texture2D texture2D5 = value;
						Vector2 vector4 = teleportPylonInfo2.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f);
						Color color3 = color;
						SpriteFrame spriteFrame = new SpriteFrame(11, 1, (byte)teleportPylonInfo2.TypeOfPylon, 0)
						{
							PaddingY = 0
						};
						drawResult = context.Draw(texture2D5, vector4, color3, spriteFrame, num, num2, Alignment.Center);
					}
					if (drawResult.IsMouseOver)
					{
						Main.cancelWormHole = true;
						string itemNameValue = Lang.GetItemNameValue(TETeleportationPylon.GetPylonItemTypeFromTileStyle((int)teleportPylonInfo2.TypeOfPylon));
						text = itemNameValue;
						if (Main.mouseLeft && Main.mouseLeftRelease)
						{
							flag = flag2;
							num4 = j;
						}
					}
				}
			}
			if (num4 != -1 && Main.mouseLeft && Main.mouseLeftRelease)
			{
				TeleportPylonInfo teleportPylonInfo3 = pylons[num4];
				if (flag)
				{
					Main.mouseLeftRelease = false;
					Main.mapFullscreen = false;
					PlayerInput.LockGamepadButtons("MouseLeft");
					Main.PylonSystem.RequestTeleportation(teleportPylonInfo3, Main.LocalPlayer);
					SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
					return;
				}
				Main.mouseLeftRelease = false;
				PlayerInput.LockGamepadButtons("MouseLeft");
				Main.PanTargetMapFullscreen = true;
				Main.PanTargetMapFullscreenEnd.X = (float)teleportPylonInfo3.PositionInTiles.X;
				Main.PanTargetMapFullscreenEnd.Y = (float)teleportPylonInfo3.PositionInTiles.Y;
			}
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00504926 File Offset: 0x00502B26
		public static bool IsRevealed(TeleportPylonInfo info)
		{
			return !Main.teamBasedSpawnsSeed || Main.Map.IsRevealed((int)info.PositionInTiles.X, (int)info.PositionInTiles.Y);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0000357B File Offset: 0x0000177B
		public TeleportPylonsMapLayer()
		{
		}

		// Token: 0x040016A3 RID: 5795
		public const int BorderSize = 10;
	}
}
