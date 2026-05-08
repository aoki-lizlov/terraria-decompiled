using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.IO;

namespace Terraria.Utilities
{
	// Token: 0x020000D4 RID: 212
	public static class TileSnapshot
	{
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x004E1A0C File Offset: 0x004DFC0C
		// (set) Token: 0x0600183D RID: 6205 RVA: 0x004E1A13 File Offset: 0x004DFC13
		public static object Context
		{
			[CompilerGenerated]
			get
			{
				return TileSnapshot.<Context>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				TileSnapshot.<Context>k__BackingField = value;
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x004E1A1B File Offset: 0x004DFC1B
		public static void Create(object context = null)
		{
			TileSnapshot.Context = context;
			TileSnapshot._worldFile = Main.ActiveWorldFileData;
			TileSnapshot.SaveTiles();
			TileSnapshot.SaveTileEntities(true);
			TileSnapshot.SaveChests(true);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x004E1A40 File Offset: 0x004DFC40
		private static void SaveTiles()
		{
			Array.Resize<TileSnapshot.TileStruct>(ref TileSnapshot._tiles, Main.maxTilesX * Main.maxTilesY);
			FastParallel.For(0, Main.maxTilesX, delegate(int x0, int x1, object _)
			{
				Tile[,] tile = Main.tile;
				TileSnapshot.TileStruct[] tiles = TileSnapshot._tiles;
				int maxTilesY = Main.maxTilesY;
				for (int i = x0; i < x1; i++)
				{
					for (int j = 0; j < maxTilesY; j++)
					{
						tiles[i * maxTilesY + j] = TileSnapshot.TileStruct.From(tile[i, j]);
					}
				}
			}, null);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x004E1A90 File Offset: 0x004DFC90
		private static void SaveTileEntities(bool clone)
		{
			if (TileSnapshot._tileEntities == null)
			{
				TileSnapshot._tileEntities = new List<TileEntity>(TileEntity.ByID.Count);
			}
			TileSnapshot._tileEntities.Clear();
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				foreach (TileEntity tileEntity in TileEntity.ByID.Values)
				{
					TileSnapshot._tileEntities.Add(clone ? TileSnapshot.Clone(tileEntity) : tileEntity);
				}
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x004E1B44 File Offset: 0x004DFD44
		private static void SaveChests(bool clone)
		{
			if (TileSnapshot._chests == null)
			{
				TileSnapshot._chests = new List<Chest>(8000);
			}
			TileSnapshot._chests.Clear();
			foreach (Chest chest2 in Main.chest)
			{
				if (chest2 != null)
				{
					TileSnapshot._chests.Add(clone ? chest2.CloneWithSeparateItems() : chest2);
				}
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x004E1BA2 File Offset: 0x004DFDA2
		public static bool IsCreated
		{
			get
			{
				return TileSnapshot._tiles != null;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x004E1BAC File Offset: 0x004DFDAC
		public static bool SizeMatches
		{
			get
			{
				return TileSnapshot._worldFile.WorldSizeX == Main.ActiveWorldFileData.WorldSizeX && TileSnapshot._worldFile.WorldSizeY == Main.ActiveWorldFileData.WorldSizeY;
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x004E1BDC File Offset: 0x004DFDDC
		public static IEnumerable<Point> Compare()
		{
			bool any = false;
			int num;
			for (int x = 0; x < Main.maxTilesX; x = num + 1)
			{
				for (int y = 0; y < Main.maxTilesY; y = num + 1)
				{
					TileSnapshot.TileStruct tileStruct = TileSnapshot.TileStruct.From(Main.tile[x, y]);
					TileSnapshot.TileStruct tileStruct2 = TileSnapshot._tiles[x * Main.maxTilesY + y];
					if (!(tileStruct == tileStruct2))
					{
						any = true;
						Main.NewText(string.Concat(new object[] { "Mismatch at ", x, ", ", y, " world vs snap" }), byte.MaxValue, byte.MaxValue, byte.MaxValue);
						Main.NewText(tileStruct.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
						Main.NewText(tileStruct2.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
						yield return new Point(x, y);
					}
					num = y;
				}
				num = x;
			}
			Main.NewText(any ? "No more differences" : "Snapshot matches identically", byte.MaxValue, byte.MaxValue, byte.MaxValue);
			yield break;
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x004E1BE5 File Offset: 0x004DFDE5
		private static TileEntity Clone(TileEntity ent)
		{
			TileSnapshot._tempStream.Position = 0L;
			TileEntity.Write(TileSnapshot._tempWriter, ent, false);
			TileSnapshot._tempStream.Position = 0L;
			return TileEntity.Read(TileSnapshot._tempReader, 319, false);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x004E1C1C File Offset: 0x004DFE1C
		public static void Restore()
		{
			TileSnapshot.RestoreTiles();
			TileSnapshot.RestoreTileEntities(TileSnapshot._tileEntities, true);
			TileSnapshot.RestoreChests(TileSnapshot._chests, true);
			if (Main.dedServ)
			{
				NetMessage.ResyncTiles(new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY));
				return;
			}
			Main.sectionManager.SetAllSectionsLoaded();
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x004E1C6C File Offset: 0x004DFE6C
		private static void RestoreTiles()
		{
			FastParallel.For(0, Main.maxTilesX, delegate(int x0, int x1, object _)
			{
				TileSnapshot.TileStruct[] tiles = TileSnapshot._tiles;
				Tile[,] tile = Main.tile;
				int maxTilesY = Main.maxTilesY;
				for (int i = x0; i < x1; i++)
				{
					for (int j = 0; j < maxTilesY; j++)
					{
						tiles[i * maxTilesY + j].Apply(tile[i, j]);
					}
				}
				Liquid.numLiquid = 0;
				LiquidBuffer.numLiquidBuffer = 0;
			}, null);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x004E1C9C File Offset: 0x004DFE9C
		private static void RestoreTileEntities(List<TileEntity> entities, bool clone)
		{
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				LeashedEntity.Clear(true);
				TileEntity.Clear();
				foreach (TileEntity tileEntity in entities)
				{
					TileSnapshot.Restore(clone ? TileSnapshot.Clone(tileEntity) : tileEntity);
				}
			}
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x004E1D28 File Offset: 0x004DFF28
		private static void Restore(TileEntity ent)
		{
			ent.ID = TileEntity.TileEntitiesNextID++;
			TileEntity.Add(ent);
			ent.OnWorldLoaded();
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x004E1D4C File Offset: 0x004DFF4C
		private static void RestoreChests(List<Chest> chests, bool clone)
		{
			Chest.Clear();
			foreach (Chest chest in chests)
			{
				Chest.Assign(clone ? chest.CloneWithSeparateItems() : chest);
			}
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x004E1DAC File Offset: 0x004DFFAC
		public static void Swap()
		{
			TileSnapshot._worldFile = Main.ActiveWorldFileData;
			TileSnapshot.SwapTiles();
			List<TileEntity> tileEntities = TileSnapshot._tileEntities;
			TileSnapshot._tileEntities = null;
			TileSnapshot.SaveTileEntities(false);
			TileSnapshot.RestoreTileEntities(tileEntities, false);
			List<Chest> chests = TileSnapshot._chests;
			TileSnapshot._chests = null;
			TileSnapshot.SaveChests(false);
			TileSnapshot.RestoreChests(chests, false);
			if (Main.dedServ)
			{
				NetMessage.ResyncTiles(new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY));
				return;
			}
			Main.sectionManager.SetAllSectionsLoaded();
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x004E1E20 File Offset: 0x004E0020
		private static void SwapTiles()
		{
			Array.Resize<TileSnapshot.TileStruct>(ref TileSnapshot._tiles, Main.maxTilesX * Main.maxTilesY);
			FastParallel.For(0, Main.maxTilesX, delegate(int x0, int x1, object _)
			{
				Tile[,] tile = Main.tile;
				TileSnapshot.TileStruct[] tiles = TileSnapshot._tiles;
				int maxTilesY = Main.maxTilesY;
				for (int i = x0; i < x1; i++)
				{
					for (int j = 0; j < maxTilesY; j++)
					{
						Tile tile2 = tile[i, j];
						TileSnapshot.TileStruct tileStruct = TileSnapshot.TileStruct.From(tile2);
						Utils.Swap<TileSnapshot.TileStruct>(ref tiles[i * maxTilesY + j], ref tileStruct);
						tileStruct.Apply(tile2);
					}
				}
			}, null);
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x004E1E6D File Offset: 0x004E006D
		public static void Clear()
		{
			TileSnapshot._tiles = null;
			TileSnapshot._tileEntities = null;
			TileSnapshot._chests = null;
			GC.Collect();
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x004E1E88 File Offset: 0x004E0088
		public static void Save(BinaryWriter writer)
		{
			writer.Write(Marshal.SizeOf(typeof(TileSnapshot.TileStruct)));
			foreach (TileSnapshot.TileStruct tileStruct in TileSnapshot._tiles)
			{
				tileStruct.Write(writer);
			}
			writer.Write(TileSnapshot._tileEntities.Count);
			foreach (TileEntity tileEntity in TileSnapshot._tileEntities)
			{
				TileEntity.Write(writer, tileEntity, false);
			}
			writer.Write(TileSnapshot._chests.Count);
			foreach (Chest chest in TileSnapshot._chests)
			{
				writer.Write(chest.index);
				writer.Write(chest.x);
				writer.Write(chest.y);
				writer.Write(chest.maxItems);
				writer.Write(chest.name);
				for (int j = 0; j < chest.maxItems; j++)
				{
					Item item = chest.item[j];
					if (item.IsAir)
					{
						writer.Write(0);
					}
					else
					{
						writer.Write((ushort)item.type);
						writer.Write((ushort)item.stack);
						writer.Write(item.prefix);
					}
				}
			}
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x004E2018 File Offset: 0x004E0218
		public static void Load(BinaryReader reader, object context = null)
		{
			if (reader.ReadInt32() != Marshal.SizeOf(typeof(TileSnapshot.TileStruct)))
			{
				throw new Exception("TileSnapshot was saved with a different value of #define SNAPSHOT_RUNTIME_DATA");
			}
			TileSnapshot.Context = context;
			TileSnapshot._worldFile = Main.ActiveWorldFileData;
			Array.Resize<TileSnapshot.TileStruct>(ref TileSnapshot._tiles, Main.maxTilesX * Main.maxTilesY);
			for (int i = 0; i < TileSnapshot._tiles.Length; i++)
			{
				TileSnapshot._tiles[i] = TileSnapshot.TileStruct.Read(reader);
			}
			if (TileSnapshot._tileEntities == null)
			{
				TileSnapshot._tileEntities = new List<TileEntity>();
			}
			TileSnapshot._tileEntities.Clear();
			int num = reader.ReadInt32();
			for (int j = 0; j < num; j++)
			{
				TileSnapshot._tileEntities.Add(TileEntity.Read(reader, 319, false));
			}
			if (TileSnapshot._chests == null)
			{
				TileSnapshot._chests = new List<Chest>();
			}
			TileSnapshot._chests.Clear();
			num = reader.ReadInt32();
			for (int k = 0; k < num; k++)
			{
				Chest chest = Chest.CreateOutOfArray(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
				chest.name = reader.ReadString();
				chest.FillWithEmptyInstances();
				for (int l = 0; l < chest.maxItems; l++)
				{
					int num2 = (int)reader.ReadUInt16();
					if (num2 != 0)
					{
						Item item = chest.item[l];
						item.SetDefaults(num2, null);
						item.stack = (int)reader.ReadUInt16();
						item.Prefix((int)reader.ReadByte());
					}
				}
				TileSnapshot._chests.Add(chest);
			}
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x004E2194 File Offset: 0x004E0394
		public static void Save(string path)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Create(path)))
			{
				TileSnapshot.Save(binaryWriter);
			}
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x004E21D0 File Offset: 0x004E03D0
		public static void Load(string path, object context = null)
		{
			using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(path)))
			{
				TileSnapshot.Load(binaryReader, context);
			}
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x004E220C File Offset: 0x004E040C
		// Note: this type is marked as 'beforefieldinit'.
		static TileSnapshot()
		{
		}

		// Token: 0x040012CB RID: 4811
		[CompilerGenerated]
		private static object <Context>k__BackingField;

		// Token: 0x040012CC RID: 4812
		private static WorldFileData _worldFile;

		// Token: 0x040012CD RID: 4813
		private static TileSnapshot.TileStruct[] _tiles;

		// Token: 0x040012CE RID: 4814
		private static List<TileEntity> _tileEntities;

		// Token: 0x040012CF RID: 4815
		private static List<Chest> _chests;

		// Token: 0x040012D0 RID: 4816
		private static MemoryStream _tempStream = new MemoryStream();

		// Token: 0x040012D1 RID: 4817
		private static BinaryWriter _tempWriter = new BinaryWriter(TileSnapshot._tempStream);

		// Token: 0x040012D2 RID: 4818
		private static BinaryReader _tempReader = new BinaryReader(TileSnapshot._tempStream);

		// Token: 0x020006F7 RID: 1783
		[StructLayout(LayoutKind.Explicit)]
		public struct TileStruct
		{
			// Token: 0x06003FC3 RID: 16323 RVA: 0x0069B4C8 File Offset: 0x006996C8
			public static TileSnapshot.TileStruct From(Tile tile)
			{
				TileSnapshot.TileStruct tileStruct = default(TileSnapshot.TileStruct);
				tileStruct._type = tile.type;
				ushort wall = tile.wall;
				tileStruct._sTileHeader = tile.sTileHeader;
				tileStruct._liquid = tile.liquid;
				tileStruct._bTileHeader = tile.bTileHeader;
				tileStruct._wall_bTileHeader3_packed = (ushort)((int)wall | ((int)(tile.bTileHeader3 & 224) << 8));
				if ((tileStruct._sTileHeader & 32) == 0)
				{
					tileStruct._type = 0;
					tileStruct._sTileHeader &= 35808;
				}
				else
				{
					if (Main.tileFrameImportant[(int)tileStruct._type])
					{
						tileStruct._frameX = tile.frameX;
						tileStruct._frameY = tile.frameY;
					}
					if ((tileStruct._sTileHeader & 29696) != 0 && !TileID.Sets.SaveSlopes[(int)tileStruct._type])
					{
						tileStruct._sTileHeader &= 35839;
					}
				}
				if (wall == 0)
				{
					tileStruct._bTileHeader &= 224;
				}
				if (tileStruct._liquid == 0)
				{
					tileStruct._bTileHeader &= 159;
				}
				return tileStruct;
			}

			// Token: 0x06003FC4 RID: 16324 RVA: 0x0069B5DC File Offset: 0x006997DC
			public void Apply(Tile tile)
			{
				tile.type = this._type;
				tile.wall = this._wall_bTileHeader3_packed & 8191;
				tile.sTileHeader = this._sTileHeader;
				tile.frameX = this._frameX;
				tile.frameY = this._frameY;
				tile.liquid = this._liquid;
				tile.bTileHeader = this._bTileHeader;
				tile.bTileHeader2 = 0;
				tile.bTileHeader3 = (byte)((this._wall_bTileHeader3_packed >> 8) & 224);
			}

			// Token: 0x06003FC5 RID: 16325 RVA: 0x0069B660 File Offset: 0x00699860
			public static bool operator ==(TileSnapshot.TileStruct lhs, TileSnapshot.TileStruct rhs)
			{
				return lhs._i0 == rhs._i0 && lhs._i1 == rhs._i1 && lhs._i2 == rhs._i2;
			}

			// Token: 0x06003FC6 RID: 16326 RVA: 0x0069B68E File Offset: 0x0069988E
			public static bool operator !=(TileSnapshot.TileStruct lhs, TileSnapshot.TileStruct rhs)
			{
				return !(lhs == rhs);
			}

			// Token: 0x06003FC7 RID: 16327 RVA: 0x0069B69A File Offset: 0x0069989A
			public override bool Equals(object obj)
			{
				return obj is TileSnapshot.TileStruct && (TileSnapshot.TileStruct)obj == this;
			}

			// Token: 0x06003FC8 RID: 16328 RVA: 0x0069B6B7 File Offset: 0x006998B7
			public override int GetHashCode()
			{
				return this._i0 ^ this._i1 ^ this._i2;
			}

			// Token: 0x06003FC9 RID: 16329 RVA: 0x0069B6D0 File Offset: 0x006998D0
			public override string ToString()
			{
				bool flag = (this._sTileHeader & 32) > 0;
				int num = (int)(this._sTileHeader & 31);
				bool flag2 = (this._sTileHeader & 1024) > 0;
				int num2 = (this._sTileHeader & 28672) >> 12;
				int num3 = (int)(this._wall_bTileHeader3_packed & 8191);
				int num4 = (int)(this._bTileHeader & 31);
				int num5 = (this._bTileHeader & 96) >> 5;
				bool flag3 = (this._sTileHeader & 128) > 0;
				bool flag4 = (this._sTileHeader & 256) > 0;
				bool flag5 = (this._sTileHeader & 512) > 0;
				bool flag6 = (this._bTileHeader & 128) > 0;
				bool flag7 = (this._sTileHeader & 2048) > 0;
				bool flag8 = (this._sTileHeader & 64) > 0;
				bool flag9 = (this._wall_bTileHeader3_packed & 8192) > 0;
				bool flag10 = (this._wall_bTileHeader3_packed & 16384) > 0;
				bool flag11 = (this._sTileHeader & 32768) > 0;
				bool flag12 = (this._wall_bTileHeader3_packed & 32768) > 0;
				string text = "!tile";
				if (flag)
				{
					text = "tile:" + this._type;
					if (num > 0)
					{
						text = text + "c" + num;
					}
					if (flag2)
					{
						text += "h";
					}
					if (num2 != 0)
					{
						text = text + "s" + num2;
					}
					if (Main.tileFrameImportant[(int)this._type])
					{
						text = string.Concat(new object[] { text, " f", this._frameX, ",", this._frameY });
					}
				}
				string text2 = "!wall";
				if (num3 > 0)
				{
					text2 = "wall:" + num3;
					if (num4 > 0)
					{
						text2 = text2 + "c" + num4;
					}
				}
				string text3 = "!liquid";
				if (this._liquid > 0)
				{
					text3 = TileSnapshot.TileStruct._liquidNames[num5] + ":" + this._liquid;
				}
				return string.Format("{0} {1} {2} flags:{3}{4} {5}{6} {7}{8} {9}{10}{11}{12}", new object[]
				{
					text,
					text2,
					text3,
					flag7 ? 'a' : '0',
					flag8 ? 'x' : '0',
					flag9 ? 'E' : '0',
					flag10 ? 'e' : '0',
					flag12 ? 'F' : '0',
					flag11 ? 'f' : '0',
					flag3 ? 'r' : '0',
					flag4 ? 'b' : '0',
					flag5 ? 'g' : '0',
					flag6 ? 'y' : '0'
				});
			}

			// Token: 0x06003FCA RID: 16330 RVA: 0x0069B9CC File Offset: 0x00699BCC
			public void Write(BinaryWriter writer)
			{
				writer.Write(this._i0);
				writer.Write(this._i1);
				writer.Write(this._i2);
			}

			// Token: 0x06003FCB RID: 16331 RVA: 0x0069B9F4 File Offset: 0x00699BF4
			public static TileSnapshot.TileStruct Read(BinaryReader reader)
			{
				return new TileSnapshot.TileStruct
				{
					_i0 = reader.ReadInt32(),
					_i1 = reader.ReadInt32(),
					_i2 = reader.ReadInt32()
				};
			}

			// Token: 0x06003FCC RID: 16332 RVA: 0x0069BA31 File Offset: 0x00699C31
			// Note: this type is marked as 'beforefieldinit'.
			static TileStruct()
			{
			}

			// Token: 0x0400681C RID: 26652
			[FieldOffset(0)]
			private ushort _type;

			// Token: 0x0400681D RID: 26653
			[FieldOffset(2)]
			private ushort _wall_bTileHeader3_packed;

			// Token: 0x0400681E RID: 26654
			[FieldOffset(4)]
			private ushort _sTileHeader;

			// Token: 0x0400681F RID: 26655
			[FieldOffset(6)]
			private short _frameX;

			// Token: 0x04006820 RID: 26656
			[FieldOffset(8)]
			private short _frameY;

			// Token: 0x04006821 RID: 26657
			[FieldOffset(10)]
			private byte _liquid;

			// Token: 0x04006822 RID: 26658
			[FieldOffset(11)]
			private byte _bTileHeader;

			// Token: 0x04006823 RID: 26659
			[FieldOffset(0)]
			private int _i0;

			// Token: 0x04006824 RID: 26660
			[FieldOffset(4)]
			private int _i1;

			// Token: 0x04006825 RID: 26661
			[FieldOffset(8)]
			private int _i2;

			// Token: 0x04006826 RID: 26662
			private static string[] _liquidNames = new string[] { "water", "lava", "honey", "shimmer" };
		}

		// Token: 0x020006F8 RID: 1784
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06003FCD RID: 16333 RVA: 0x0069BA5E File Offset: 0x00699C5E
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06003FCE RID: 16334 RVA: 0x0000357B File Offset: 0x0000177B
			public <>c()
			{
			}

			// Token: 0x06003FCF RID: 16335 RVA: 0x0069BA6C File Offset: 0x00699C6C
			internal void <SaveTiles>b__10_0(int x0, int x1, object _)
			{
				Tile[,] tile = Main.tile;
				TileSnapshot.TileStruct[] tiles = TileSnapshot._tiles;
				int maxTilesY = Main.maxTilesY;
				for (int i = x0; i < x1; i++)
				{
					for (int j = 0; j < maxTilesY; j++)
					{
						tiles[i * maxTilesY + j] = TileSnapshot.TileStruct.From(tile[i, j]);
					}
				}
			}

			// Token: 0x06003FD0 RID: 16336 RVA: 0x0069BAC4 File Offset: 0x00699CC4
			internal void <RestoreTiles>b__23_0(int x0, int x1, object _)
			{
				TileSnapshot.TileStruct[] tiles = TileSnapshot._tiles;
				Tile[,] tile = Main.tile;
				int maxTilesY = Main.maxTilesY;
				for (int i = x0; i < x1; i++)
				{
					for (int j = 0; j < maxTilesY; j++)
					{
						tiles[i * maxTilesY + j].Apply(tile[i, j]);
					}
				}
				Liquid.numLiquid = 0;
				LiquidBuffer.numLiquidBuffer = 0;
			}

			// Token: 0x06003FD1 RID: 16337 RVA: 0x0069BB28 File Offset: 0x00699D28
			internal void <SwapTiles>b__28_0(int x0, int x1, object _)
			{
				Tile[,] tile = Main.tile;
				TileSnapshot.TileStruct[] tiles = TileSnapshot._tiles;
				int maxTilesY = Main.maxTilesY;
				for (int i = x0; i < x1; i++)
				{
					for (int j = 0; j < maxTilesY; j++)
					{
						Tile tile2 = tile[i, j];
						TileSnapshot.TileStruct tileStruct = TileSnapshot.TileStruct.From(tile2);
						Utils.Swap<TileSnapshot.TileStruct>(ref tiles[i * maxTilesY + j], ref tileStruct);
						tileStruct.Apply(tile2);
					}
				}
			}

			// Token: 0x04006827 RID: 26663
			public static readonly TileSnapshot.<>c <>9 = new TileSnapshot.<>c();

			// Token: 0x04006828 RID: 26664
			public static ParallelForAction <>9__10_0;

			// Token: 0x04006829 RID: 26665
			public static ParallelForAction <>9__23_0;

			// Token: 0x0400682A RID: 26666
			public static ParallelForAction <>9__28_0;
		}

		// Token: 0x020006F9 RID: 1785
		[CompilerGenerated]
		private sealed class <Compare>d__17 : IEnumerable<Point>, IEnumerable, IEnumerator<Point>, IDisposable, IEnumerator
		{
			// Token: 0x06003FD2 RID: 16338 RVA: 0x0069BB93 File Offset: 0x00699D93
			[DebuggerHidden]
			public <Compare>d__17(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x06003FD3 RID: 16339 RVA: 0x00009E46 File Offset: 0x00008046
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06003FD4 RID: 16340 RVA: 0x0069BBB4 File Offset: 0x00699DB4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num == 0)
				{
					this.<>1__state = -1;
					any = false;
					x = 0;
					goto IL_0172;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_0142:
				int num2 = y;
				y = num2 + 1;
				IL_0152:
				if (y >= Main.maxTilesY)
				{
					num2 = x;
					x = num2 + 1;
				}
				else
				{
					TileSnapshot.TileStruct tileStruct = TileSnapshot.TileStruct.From(Main.tile[x, y]);
					TileSnapshot.TileStruct tileStruct2 = TileSnapshot._tiles[x * Main.maxTilesY + y];
					if (!(tileStruct == tileStruct2))
					{
						any = true;
						Main.NewText(string.Concat(new object[] { "Mismatch at ", x, ", ", y, " world vs snap" }), byte.MaxValue, byte.MaxValue, byte.MaxValue);
						Main.NewText(tileStruct.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
						Main.NewText(tileStruct2.ToString(), byte.MaxValue, byte.MaxValue, byte.MaxValue);
						this.<>2__current = new Point(x, y);
						this.<>1__state = 1;
						return true;
					}
					goto IL_0142;
				}
				IL_0172:
				if (x >= Main.maxTilesX)
				{
					Main.NewText(any ? "No more differences" : "Snapshot matches identically", byte.MaxValue, byte.MaxValue, byte.MaxValue);
					return false;
				}
				y = 0;
				goto IL_0152;
			}

			// Token: 0x17000507 RID: 1287
			// (get) Token: 0x06003FD5 RID: 16341 RVA: 0x0069BD6C File Offset: 0x00699F6C
			Point IEnumerator<Point>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003FD6 RID: 16342 RVA: 0x0066E2F4 File Offset: 0x0066C4F4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000508 RID: 1288
			// (get) Token: 0x06003FD7 RID: 16343 RVA: 0x0069BD74 File Offset: 0x00699F74
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06003FD8 RID: 16344 RVA: 0x0069BD84 File Offset: 0x00699F84
			[DebuggerHidden]
			IEnumerator<Point> IEnumerable<Point>.GetEnumerator()
			{
				TileSnapshot.<Compare>d__17 <Compare>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<Compare>d__ = this;
				}
				else
				{
					<Compare>d__ = new TileSnapshot.<Compare>d__17(0);
				}
				return <Compare>d__;
			}

			// Token: 0x06003FD9 RID: 16345 RVA: 0x0069BDC0 File Offset: 0x00699FC0
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Microsoft.Xna.Framework.Point>.GetEnumerator();
			}

			// Token: 0x0400682B RID: 26667
			private int <>1__state;

			// Token: 0x0400682C RID: 26668
			private Point <>2__current;

			// Token: 0x0400682D RID: 26669
			private int <>l__initialThreadId;

			// Token: 0x0400682E RID: 26670
			private bool <any>5__2;

			// Token: 0x0400682F RID: 26671
			private int <x>5__3;

			// Token: 0x04006830 RID: 26672
			private int <y>5__4;
		}
	}
}
