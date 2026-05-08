using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x0200026E RID: 622
	public class TreeTopsInfo
	{
		// Token: 0x0600240C RID: 9228 RVA: 0x0054A2DC File Offset: 0x005484DC
		public void Save(BinaryWriter writer)
		{
			writer.Write(this._variations.Length);
			for (int i = 0; i < this._variations.Length; i++)
			{
				writer.Write(this._variations[i]);
			}
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0054A318 File Offset: 0x00548518
		public void Load(BinaryReader reader, int loadVersion)
		{
			if (loadVersion < 211)
			{
				this.CopyExistingWorldInfo();
				return;
			}
			int num = reader.ReadInt32();
			int num2 = 0;
			while (num2 < num && num2 < this._variations.Length)
			{
				this._variations[num2] = reader.ReadInt32();
				num2++;
			}
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0054A360 File Offset: 0x00548560
		public void SyncSend(BinaryWriter writer)
		{
			for (int i = 0; i < this._variations.Length; i++)
			{
				writer.Write((byte)this._variations[i]);
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0054A390 File Offset: 0x00548590
		public void SyncReceive(BinaryReader reader)
		{
			for (int i = 0; i < this._variations.Length; i++)
			{
				int num = this._variations[i];
				this._variations[i] = (int)reader.ReadByte();
				if (this._variations[i] != num)
				{
					this.DoTreeFX(i);
				}
			}
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x0054A3D9 File Offset: 0x005485D9
		public int GetTreeStyle(int areaId)
		{
			return this._variations[areaId];
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x0054A3E4 File Offset: 0x005485E4
		public void RandomizeTreeStyleBasedOnWorldPosition(UnifiedRandom rand, Vector2 worldPosition)
		{
			Point point = new Point((int)(worldPosition.X / 16f), (int)(worldPosition.Y / 16f) + 1);
			Tile tileSafely = Framing.GetTileSafely(point);
			if (!tileSafely.active())
			{
				return;
			}
			int num = -1;
			if (tileSafely.type == 70)
			{
				num = 11;
			}
			else if (tileSafely.type == 53 && WorldGen.oceanDepths(point.X, point.Y))
			{
				num = 10;
			}
			else if (tileSafely.type == 23)
			{
				num = 4;
			}
			else if (tileSafely.type == 199)
			{
				num = 8;
			}
			else if (tileSafely.type == 109 || tileSafely.type == 492)
			{
				num = 7;
			}
			else if (tileSafely.type == 53)
			{
				num = 9;
			}
			else if (tileSafely.type == 147)
			{
				num = 6;
			}
			else if (tileSafely.type == 60)
			{
				num = 5;
			}
			else if (tileSafely.type == 633)
			{
				num = 12;
			}
			else if (tileSafely.type == 2 || tileSafely.type == 477)
			{
				if (point.X < Main.treeX[0])
				{
					num = 0;
				}
				else if (point.X < Main.treeX[1])
				{
					num = 1;
				}
				else if (point.X < Main.treeX[2])
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			if (num > -1)
			{
				this.RandomizeTreeStyle(rand, num);
			}
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x0054A540 File Offset: 0x00548740
		public void RandomizeTreeStyle(UnifiedRandom rand, int areaId)
		{
			int num = this._variations[areaId];
			bool flag = false;
			while (this._variations[areaId] == num)
			{
				switch (areaId)
				{
				case 0:
				case 1:
				case 2:
				case 3:
					this._variations[areaId] = rand.Next(6);
					break;
				case 4:
					this._variations[areaId] = rand.Next(5);
					break;
				case 5:
					this._variations[areaId] = rand.Next(6);
					break;
				case 6:
					this._variations[areaId] = rand.NextFromList(new int[]
					{
						0, 1, 2, 21, 22, 3, 31, 32, 4, 41,
						42, 5, 6, 7
					});
					break;
				case 7:
					this._variations[areaId] = rand.Next(5);
					break;
				case 8:
					this._variations[areaId] = rand.Next(6);
					break;
				case 9:
					this._variations[areaId] = rand.Next(5);
					break;
				case 10:
					this._variations[areaId] = rand.Next(6);
					break;
				case 11:
					this._variations[areaId] = rand.Next(4);
					break;
				case 12:
					this._variations[areaId] = rand.Next(6);
					break;
				default:
					flag = true;
					break;
				}
				if (flag)
				{
					break;
				}
			}
			if (num != this._variations[areaId])
			{
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
					return;
				}
				this.DoTreeFX(areaId);
			}
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00009E46 File Offset: 0x00008046
		private void DoTreeFX(int areaID)
		{
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0054A6AA File Offset: 0x005488AA
		public void CopyExistingWorldInfoForWorldGeneration()
		{
			this.CopyExistingWorldInfo();
		}

		// Token: 0x06002415 RID: 9237 RVA: 0x0054A6B4 File Offset: 0x005488B4
		private void CopyExistingWorldInfo()
		{
			this._variations[0] = Main.treeStyle[0];
			this._variations[1] = Main.treeStyle[1];
			this._variations[2] = Main.treeStyle[2];
			this._variations[3] = Main.treeStyle[3];
			this._variations[4] = WorldGen.corruptBG;
			this._variations[5] = WorldGen.jungleBG;
			this._variations[6] = WorldGen.snowBG;
			this._variations[7] = WorldGen.hallowBG;
			this._variations[8] = WorldGen.crimsonBG;
			this._variations[9] = WorldGen.desertBG;
			this._variations[10] = WorldGen.oceanBG;
			this._variations[11] = WorldGen.mushroomBG;
			this._variations[12] = WorldGen.underworldBG;
		}

		// Token: 0x06002416 RID: 9238 RVA: 0x0054A776 File Offset: 0x00548976
		public TreeTopsInfo()
		{
		}

		// Token: 0x04004DD2 RID: 19922
		private int[] _variations = new int[TreeTopsInfo.AreaId.Count];

		// Token: 0x020007F1 RID: 2033
		public class AreaId
		{
			// Token: 0x06004290 RID: 17040 RVA: 0x0000357B File Offset: 0x0000177B
			public AreaId()
			{
			}

			// Token: 0x06004291 RID: 17041 RVA: 0x006BF183 File Offset: 0x006BD383
			// Note: this type is marked as 'beforefieldinit'.
			static AreaId()
			{
			}

			// Token: 0x04007167 RID: 29031
			public const int Forest1 = 0;

			// Token: 0x04007168 RID: 29032
			public const int Forest2 = 1;

			// Token: 0x04007169 RID: 29033
			public const int Forest3 = 2;

			// Token: 0x0400716A RID: 29034
			public const int Forest4 = 3;

			// Token: 0x0400716B RID: 29035
			public const int Corruption = 4;

			// Token: 0x0400716C RID: 29036
			public const int Jungle = 5;

			// Token: 0x0400716D RID: 29037
			public const int Snow = 6;

			// Token: 0x0400716E RID: 29038
			public const int Hallow = 7;

			// Token: 0x0400716F RID: 29039
			public const int Crimson = 8;

			// Token: 0x04007170 RID: 29040
			public const int Desert = 9;

			// Token: 0x04007171 RID: 29041
			public const int Ocean = 10;

			// Token: 0x04007172 RID: 29042
			public const int GlowingMushroom = 11;

			// Token: 0x04007173 RID: 29043
			public const int Underworld = 12;

			// Token: 0x04007174 RID: 29044
			public static readonly int Count = 13;
		}
	}
}
