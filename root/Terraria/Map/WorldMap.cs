using System;
using System.IO;
using Terraria.IO;
using Terraria.Social;
using Terraria.Testing;
using Terraria.Utilities;

namespace Terraria.Map
{
	// Token: 0x02000183 RID: 387
	public class WorldMap
	{
		// Token: 0x170002FA RID: 762
		public MapTile this[int x, int y]
		{
			get
			{
				return this._tiles[x, y];
			}
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0050FC40 File Offset: 0x0050DE40
		public WorldMap(int maxWidth, int maxHeight)
		{
			this.MaxWidth = maxWidth;
			this.MaxHeight = maxHeight;
			this._tiles = new MapTile[this.MaxWidth, this.MaxHeight];
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0050FC6D File Offset: 0x0050DE6D
		public void ConsumeUpdate(int x, int y)
		{
			this._tiles[x, y].IsChanged = false;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0050FC82 File Offset: 0x0050DE82
		public void Update(int x, int y, byte light)
		{
			this._tiles[x, y] = MapHelper.CreateMapTile(x, y, light, 0);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0050FC9A File Offset: 0x0050DE9A
		public void SetTile(int x, int y, ref MapTile tile)
		{
			this._tiles[x, y] = tile;
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x0050FCAF File Offset: 0x0050DEAF
		public bool IsRevealed(int x, int y)
		{
			return this._tiles[x, y].Light > 0;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x0050FCC8 File Offset: 0x0050DEC8
		public bool UpdateLighting(int x, int y, byte light)
		{
			MapTile mapTile = this._tiles[x, y];
			if (light == 0 && mapTile.Light == 0)
			{
				return false;
			}
			MapTile mapTile2 = MapHelper.CreateMapTile(x, y, Math.Max(mapTile.Light, light), 0);
			if (mapTile2.Equals(mapTile))
			{
				return false;
			}
			this._tiles[x, y] = mapTile2;
			return true;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x0050FD20 File Offset: 0x0050DF20
		public bool UpdateType(int x, int y)
		{
			return this.UpdateType(x, y, ref this._tiles[x, y]);
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x0050FD38 File Offset: 0x0050DF38
		private bool UpdateType(int x, int y, ref MapTile mapTile)
		{
			if (!mapTile.UpdateQueued)
			{
				return false;
			}
			mapTile.UpdateQueued = false;
			if (mapTile.Light == 0)
			{
				return false;
			}
			if (!Main.sectionManager.TileLoaded(x, y))
			{
				return false;
			}
			bool flag = MapHelper.IsBackground((int)mapTile.Type);
			MapTile mapTile2 = MapHelper.CreateMapTile(x, y, mapTile.Light, (int)(flag ? mapTile.Type : 0));
			if (mapTile2.Equals(mapTile))
			{
				return false;
			}
			mapTile = mapTile2;
			return true;
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0050FDAF File Offset: 0x0050DFAF
		internal bool QueueUpdate(int x, int y)
		{
			return this.QueueUpdate(ref this._tiles[x, y]);
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0050FDC4 File Offset: 0x0050DFC4
		private bool QueueUpdate(ref MapTile mapTile)
		{
			if (mapTile.Light == 0 || mapTile.UpdateQueued)
			{
				return false;
			}
			mapTile.UpdateQueued = true;
			return true;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0050FDE0 File Offset: 0x0050DFE0
		public void UnlockMapSection(int sectionX, int sectionY)
		{
			int num = sectionX * 200;
			int num2 = num + 200;
			int num3 = sectionY * 150;
			int num4 = num3 + 150;
			int num5 = 40;
			num = Utils.Clamp<int>(num, num5, Main.maxTilesX - num5);
			num2 = Utils.Clamp<int>(num2, num5, Main.maxTilesX - num5);
			num3 = Utils.Clamp<int>(num3, num5, Main.maxTilesY - num5);
			num4 = Utils.Clamp<int>(num4, num5, Main.maxTilesY - num5);
			if (DebugOptions.unlockMap == 2)
			{
				for (int i = num; i < num2; i++)
				{
					for (int j = num3; j < num4; j++)
					{
						this.UnlockMapTilePretty(i, j);
					}
				}
				return;
			}
			for (int k = num; k < num2; k++)
			{
				for (int l = num3; l < num4; l++)
				{
					this.UpdateLighting(k, l, byte.MaxValue);
				}
			}
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0050FEB8 File Offset: 0x0050E0B8
		public void UnlockMapTilePretty(int x, int y)
		{
			if (!WorldGen.InWorld(x, y, 12))
			{
				return;
			}
			if (WorldGen.SolidTile(x, y, false))
			{
				return;
			}
			int num = 5;
			float num2 = 255f;
			Tile tileSafely = Framing.GetTileSafely(x, y);
			if (tileSafely.liquid > 0 && !tileSafely.lava())
			{
				return;
			}
			if (tileSafely.wall > 0)
			{
				num2 *= 0.8f;
			}
			if ((double)y >= Main.worldSurface)
			{
				num2 *= 0.7f;
			}
			for (int i = -num; i <= num; i++)
			{
				for (int j = -num; j <= num; j++)
				{
					int num3 = x + i;
					int num4 = y + j;
					float num5 = (float)(num - Math.Abs(i) - Math.Abs(j));
					if (num5 >= 0f)
					{
						this.UpdateLighting(num3, num4, (byte)(num2 * (num5 / (float)num)));
					}
				}
			}
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0050FF80 File Offset: 0x0050E180
		public void Load()
		{
			Lighting.Clear();
			bool isCloudSave = Main.ActivePlayerFileData.IsCloudSave;
			if (isCloudSave && SocialAPI.Cloud == null)
			{
				return;
			}
			if (!Main.mapEnabled)
			{
				return;
			}
			string text;
			if (!WorldMap.TryGetMapPath(Main.ActivePlayerFileData, Main.ActiveWorldFileData, out text))
			{
				Main.MapFileMetadata = FileMetadata.FromCurrentSettings(FileType.Map);
				return;
			}
			using (MemoryStream memoryStream = new MemoryStream(FileUtilities.ReadAllBytes(text, isCloudSave)))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					try
					{
						int num = binaryReader.ReadInt32();
						bool flag = (num & 32768) == 32768;
						num &= -32769;
						if (num <= 319)
						{
							if (flag)
							{
								MapHelper.LoadMapVersionCompressed(binaryReader, num);
							}
							else if (num <= 91)
							{
								MapHelper.LoadMapVersion1(binaryReader, num);
							}
							else
							{
								MapHelper.LoadMapVersion2(binaryReader, num);
							}
							this.ClearEdges();
							Main.clearMap = true;
							Main.loadMap = true;
							Main.loadMapLock = true;
							Main.refreshMap = false;
						}
					}
					catch (Exception ex)
					{
						using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
						{
							streamWriter.WriteLine(DateTime.Now);
							streamWriter.WriteLine(ex);
							streamWriter.WriteLine("");
						}
						if (!isCloudSave)
						{
							File.Copy(text, text + ".bad", true);
						}
						this.Clear();
					}
				}
			}
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x00510104 File Offset: 0x0050E304
		public static bool TryGetMapPath(PlayerFileData playerFileData, WorldFileData worldFileData, out string mapPath)
		{
			string text = playerFileData.Path.Substring(0, playerFileData.Path.Length - 4);
			mapPath = text + Path.DirectorySeparatorChar.ToString() + worldFileData.MapFileName + ".map";
			if (worldFileData.UseGuidAsMapName && !FileUtilities.Exists(mapPath, playerFileData.IsCloudSave))
			{
				mapPath = string.Concat(new object[]
				{
					text,
					Path.DirectorySeparatorChar.ToString(),
					worldFileData.WorldId,
					".map"
				});
			}
			return FileUtilities.Exists(mapPath, playerFileData.IsCloudSave);
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x005101A6 File Offset: 0x0050E3A6
		public void Save()
		{
			MapHelper.SaveMap();
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x005101B0 File Offset: 0x0050E3B0
		public void Clear()
		{
			for (int i = 0; i < this.MaxWidth; i++)
			{
				for (int j = 0; j < this.MaxHeight; j++)
				{
					this._tiles[i, j].Clear();
				}
			}
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x005101F4 File Offset: 0x0050E3F4
		public void ClearEdges()
		{
			for (int i = 0; i < this.MaxWidth; i++)
			{
				for (int j = 0; j < 40; j++)
				{
					this._tiles[i, j].Clear();
				}
			}
			for (int k = 0; k < this.MaxWidth; k++)
			{
				for (int l = this.MaxHeight - 40; l < this.MaxHeight; l++)
				{
					this._tiles[k, l].Clear();
				}
			}
			for (int m = 0; m < 40; m++)
			{
				for (int n = 40; n < this.MaxHeight - 40; n++)
				{
					this._tiles[m, n].Clear();
				}
			}
			for (int num = this.MaxWidth - 40; num < this.MaxWidth; num++)
			{
				for (int num2 = 40; num2 < this.MaxHeight - 40; num2++)
				{
					this._tiles[num, num2].Clear();
				}
			}
		}

		// Token: 0x040016DB RID: 5851
		public readonly int MaxWidth;

		// Token: 0x040016DC RID: 5852
		public readonly int MaxHeight;

		// Token: 0x040016DD RID: 5853
		public const int BlackEdgeWidth = 40;

		// Token: 0x040016DE RID: 5854
		private MapTile[,] _tiles;
	}
}
