using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Testing.ChatCommands;
using Terraria.UI.Chat;

namespace Terraria.Testing
{
	// Token: 0x02000116 RID: 278
	public static class DebugUtils
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x004F93C0 File Offset: 0x004F75C0
		internal static string GetTileDescription(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			if (tile == null)
			{
				return "";
			}
			Point point = (Main.LocalPlayer.Bottom + new Vector2(-8f, 8f)).ToTileCoordinates();
			string text;
			if (!TileID.Search.TryGetName((int)tile.type, ref text))
			{
				text = "Unknown";
			}
			string text2;
			if (!WallID.Search.TryGetName((int)tile.wall, ref text2))
			{
				text2 = "Unknown";
			}
			string text3 = "   ";
			return string.Concat(new object[]
			{
				text3,
				"Pos: ",
				x,
				", ",
				y,
				"\n",
				text3,
				"Type: ",
				tile.type,
				(tile.blockType() == 0) ? "" : (" " + DebugUtils._slopeIcons[tile.blockType() - 1].ToString()),
				" (",
				text,
				")\n",
				text3,
				"Frame: ",
				tile.frameX,
				", ",
				tile.frameY,
				"\n",
				text3,
				"FrameImportant: ",
				Main.tileFrameImportant[(int)tile.type].ToString(),
				"\n",
				text3,
				"Liquid: ",
				tile.liquid,
				" (",
				tile.liquidType(),
				")\n",
				text3,
				"Wall: ",
				tile.wall,
				" (",
				text2,
				")\n",
				text3,
				"Compare Spot: ",
				point.X,
				", ",
				point.Y,
				"\n",
				text3,
				"Chunk: ",
				x / 200,
				", ",
				y / 150,
				"\n",
				text3,
				"Paints: ",
				tile.color(),
				tile.fullbrightBlock() ? " fullbright" : "",
				tile.invisibleBlock() ? " echo" : "",
				", ",
				tile.wallColor(),
				tile.fullbrightWall() ? " fullbright" : "",
				tile.invisibleWall() ? " echo" : "",
				"\n",
				text3,
				"Light: ",
				Lighting.GetColor(x, y)
			});
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x004F9708 File Offset: 0x004F7908
		internal static bool PracticeModeReset(Player player, PlayerDeathReason damageSource)
		{
			if (!DebugOptions.PracticeMode)
			{
				return false;
			}
			if (!NPC.AnyDanger(false, true))
			{
				return false;
			}
			player.statLife = player.statLifeMax2;
			for (int i = 0; i < Player.maxBuffs; i++)
			{
				if (player.buffTime[i] > 0)
				{
					int num = player.buffType[i];
					if (Main.debuff[num] && (num == 21 || !BuffID.Sets.NurseCannotRemoveDebuff[num]))
					{
						player.DelBuff(i);
						i = -1;
					}
				}
			}
			for (int j = 0; j < BuffID.Count; j++)
			{
				if (Main.debuff[j])
				{
					player.buffImmune[j] = true;
				}
			}
			string text = "unknown source";
			Entity entity;
			damageSource.TryGetCausingEntity(out entity);
			if (entity is NPC)
			{
				text = ((NPC)entity).TypeName;
			}
			else if (entity is Projectile)
			{
				text = ((Projectile)entity).Name;
			}
			else if (entity is Player)
			{
				text = ((Player)entity).name;
			}
			Main.NewText("Lethal damage dealt by " + text, byte.MaxValue, byte.MaxValue, 0);
			if (Main.netMode != 0)
			{
				return true;
			}
			for (int k = 0; k < 1000; k++)
			{
				Projectile projectile = Main.projectile[k];
				if (projectile.active && projectile.hostile)
				{
					projectile.active = false;
				}
			}
			for (int l = 0; l < Main.maxNPCs; l++)
			{
				NPC npc = Main.npc[l];
				if (npc.active && !npc.friendly && !npc.isLikeATownNPC)
				{
					npc.active = false;
				}
			}
			return true;
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x004F988E File Offset: 0x004F7A8E
		public static void QuickSPMessage(string message)
		{
			ChatManager.DebugCommands.Process(new DebugMessage((byte)Main.myPlayer, message));
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x004F98A7 File Offset: 0x004F7AA7
		// Note: this type is marked as 'beforefieldinit'.
		static DebugUtils()
		{
		}

		// Token: 0x04001553 RID: 5459
		private static char[] _slopeIcons = new char[] { '⬓', '◣', '◢', '◤', '◥' };
	}
}
