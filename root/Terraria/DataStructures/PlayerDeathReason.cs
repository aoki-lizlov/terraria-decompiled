using System;
using System.IO;
using Terraria.Localization;

namespace Terraria.DataStructures
{
	// Token: 0x02000599 RID: 1433
	public class PlayerDeathReason
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x006326D4 File Offset: 0x006308D4
		public int? SourceProjectileType
		{
			get
			{
				if (this._sourceProjectileLocalIndex == -1)
				{
					return null;
				}
				return new int?(this._sourceProjectileType);
			}
		}

		// Token: 0x06003872 RID: 14450 RVA: 0x00632700 File Offset: 0x00630900
		public bool TryGetCausingEntity(out Entity entity)
		{
			entity = null;
			if (Main.npc.IndexInRange(this._sourceNPCIndex))
			{
				entity = Main.npc[this._sourceNPCIndex];
				return true;
			}
			if (Main.projectile.IndexInRange(this._sourceProjectileLocalIndex))
			{
				entity = Main.projectile[this._sourceProjectileLocalIndex];
				return true;
			}
			if (Main.player.IndexInRange(this._sourcePlayerIndex))
			{
				entity = Main.player[this._sourcePlayerIndex];
				return true;
			}
			return false;
		}

		// Token: 0x06003873 RID: 14451 RVA: 0x00632777 File Offset: 0x00630977
		public static PlayerDeathReason LegacyDefault()
		{
			return new PlayerDeathReason
			{
				_sourceOtherIndex = 255
			};
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x00632789 File Offset: 0x00630989
		public static PlayerDeathReason ByNPC(int index)
		{
			return new PlayerDeathReason
			{
				_sourceNPCIndex = index
			};
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x00632797 File Offset: 0x00630997
		public static PlayerDeathReason ByCustomReason(string reasonInEnglish)
		{
			return new PlayerDeathReason
			{
				_sourceCustomReason = reasonInEnglish
			};
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x006327A8 File Offset: 0x006309A8
		public static PlayerDeathReason ByPlayer(int index)
		{
			return new PlayerDeathReason
			{
				_sourcePlayerIndex = index,
				_sourceItemType = Main.player[index].inventory[Main.player[index].selectedItem].type,
				_sourceItemPrefix = (int)Main.player[index].inventory[Main.player[index].selectedItem].prefix
			};
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x00632809 File Offset: 0x00630A09
		public static PlayerDeathReason ByOther(int type)
		{
			return new PlayerDeathReason
			{
				_sourceOtherIndex = type
			};
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x00632818 File Offset: 0x00630A18
		public static PlayerDeathReason ByProjectile(int playerIndex, int projectileIndex)
		{
			PlayerDeathReason playerDeathReason = new PlayerDeathReason
			{
				_sourcePlayerIndex = playerIndex,
				_sourceProjectileLocalIndex = projectileIndex,
				_sourceProjectileType = Main.projectile[projectileIndex].type
			};
			if (playerIndex >= 0 && playerIndex <= 255)
			{
				playerDeathReason._sourceItemType = Main.player[playerIndex].inventory[Main.player[playerIndex].selectedItem].type;
				playerDeathReason._sourceItemPrefix = (int)Main.player[playerIndex].inventory[Main.player[playerIndex].selectedItem].prefix;
			}
			return playerDeathReason;
		}

		// Token: 0x06003879 RID: 14457 RVA: 0x006328A0 File Offset: 0x00630AA0
		public NetworkText GetDeathText(string deadPlayerName)
		{
			if (this._sourceCustomReason != null)
			{
				return NetworkText.FromLiteral(this._sourceCustomReason);
			}
			return Lang.CreateDeathMessage(deadPlayerName, this._sourcePlayerIndex, this._sourceNPCIndex, this._sourceProjectileLocalIndex, this._sourceOtherIndex, this._sourceProjectileType, this._sourceItemType);
		}

		// Token: 0x0600387A RID: 14458 RVA: 0x006328E0 File Offset: 0x00630AE0
		public void WriteSelfTo(BinaryWriter writer)
		{
			BitsByte bitsByte = 0;
			bitsByte[0] = this._sourcePlayerIndex != -1;
			bitsByte[1] = this._sourceNPCIndex != -1;
			bitsByte[2] = this._sourceProjectileLocalIndex != -1;
			bitsByte[3] = this._sourceOtherIndex != -1;
			bitsByte[4] = this._sourceProjectileType != 0;
			bitsByte[5] = this._sourceItemType != 0;
			bitsByte[6] = this._sourceItemPrefix != 0;
			bitsByte[7] = this._sourceCustomReason != null;
			writer.Write(bitsByte);
			if (bitsByte[0])
			{
				writer.Write((short)this._sourcePlayerIndex);
			}
			if (bitsByte[1])
			{
				writer.Write((short)this._sourceNPCIndex);
			}
			if (bitsByte[2])
			{
				writer.Write((short)this._sourceProjectileLocalIndex);
			}
			if (bitsByte[3])
			{
				writer.Write((byte)this._sourceOtherIndex);
			}
			if (bitsByte[4])
			{
				writer.Write((short)this._sourceProjectileType);
			}
			if (bitsByte[5])
			{
				writer.Write((short)this._sourceItemType);
			}
			if (bitsByte[6])
			{
				writer.Write((byte)this._sourceItemPrefix);
			}
			if (bitsByte[7])
			{
				writer.Write(this._sourceCustomReason);
			}
		}

		// Token: 0x0600387B RID: 14459 RVA: 0x00632A4C File Offset: 0x00630C4C
		public static PlayerDeathReason FromReader(BinaryReader reader)
		{
			PlayerDeathReason playerDeathReason = new PlayerDeathReason();
			BitsByte bitsByte = reader.ReadByte();
			if (bitsByte[0])
			{
				playerDeathReason._sourcePlayerIndex = (int)reader.ReadInt16();
			}
			if (bitsByte[1])
			{
				playerDeathReason._sourceNPCIndex = (int)reader.ReadInt16();
			}
			if (bitsByte[2])
			{
				playerDeathReason._sourceProjectileLocalIndex = (int)reader.ReadInt16();
			}
			if (bitsByte[3])
			{
				playerDeathReason._sourceOtherIndex = (int)reader.ReadByte();
			}
			if (bitsByte[4])
			{
				playerDeathReason._sourceProjectileType = (int)reader.ReadInt16();
			}
			if (bitsByte[5])
			{
				playerDeathReason._sourceItemType = (int)reader.ReadInt16();
			}
			if (bitsByte[6])
			{
				playerDeathReason._sourceItemPrefix = (int)reader.ReadByte();
			}
			if (bitsByte[7])
			{
				playerDeathReason._sourceCustomReason = reader.ReadString();
			}
			return playerDeathReason;
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x00632B1C File Offset: 0x00630D1C
		public PlayerDeathReason()
		{
		}

		// Token: 0x04005C9B RID: 23707
		private int _sourcePlayerIndex = -1;

		// Token: 0x04005C9C RID: 23708
		private int _sourceNPCIndex = -1;

		// Token: 0x04005C9D RID: 23709
		private int _sourceProjectileLocalIndex = -1;

		// Token: 0x04005C9E RID: 23710
		private int _sourceOtherIndex = -1;

		// Token: 0x04005C9F RID: 23711
		private int _sourceProjectileType;

		// Token: 0x04005CA0 RID: 23712
		private int _sourceItemType;

		// Token: 0x04005CA1 RID: 23713
		private int _sourceItemPrefix;

		// Token: 0x04005CA2 RID: 23714
		private string _sourceCustomReason;
	}
}
