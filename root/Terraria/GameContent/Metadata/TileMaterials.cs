using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Terraria.ID;

namespace Terraria.GameContent.Metadata
{
	// Token: 0x0200028E RID: 654
	public static class TileMaterials
	{
		// Token: 0x06002529 RID: 9513 RVA: 0x00553868 File Offset: 0x00551A68
		static TileMaterials()
		{
			TileMaterial tileMaterial = TileMaterials._materialsByName["Default"];
			for (int i = 0; i < TileMaterials.MaterialsByTileId.Length; i++)
			{
				TileMaterials.MaterialsByTileId[i] = tileMaterial;
			}
			foreach (KeyValuePair<string, string> keyValuePair in TileMaterials.DeserializeEmbeddedResource<Dictionary<string, string>>("Terraria.GameContent.Metadata.MaterialData.Tiles.json"))
			{
				string key = keyValuePair.Key;
				string value = keyValuePair.Value;
				TileMaterials.SetForTileId((ushort)TileID.Search.GetId(key), TileMaterials._materialsByName[value]);
			}
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00553930 File Offset: 0x00551B30
		private static T DeserializeEmbeddedResource<T>(string path)
		{
			T t;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					t = JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
				}
			}
			return t;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00553990 File Offset: 0x00551B90
		public static void SetForTileId(ushort tileId, TileMaterial material)
		{
			TileMaterials.MaterialsByTileId[(int)tileId] = material;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0055399A File Offset: 0x00551B9A
		public static TileMaterial GetByTileId(ushort tileId)
		{
			return TileMaterials.MaterialsByTileId[(int)tileId];
		}

		// Token: 0x04004F8D RID: 20365
		private static Dictionary<string, TileMaterial> _materialsByName = TileMaterials.DeserializeEmbeddedResource<Dictionary<string, TileMaterial>>("Terraria.GameContent.Metadata.MaterialData.Materials.json");

		// Token: 0x04004F8E RID: 20366
		private static readonly TileMaterial[] MaterialsByTileId = new TileMaterial[(int)TileID.Count];
	}
}
