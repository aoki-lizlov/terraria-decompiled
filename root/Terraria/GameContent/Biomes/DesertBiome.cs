using System;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.GameContent.Biomes.Desert;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200050F RID: 1295
	public class DesertBiome : MicroBiome
	{
		// Token: 0x0600365A RID: 13914 RVA: 0x00625058 File Offset: 0x00623258
		public override bool Place(Point origin, StructureMap structures, GenerationProgress progress)
		{
			DesertDescription desertDescription = DesertDescription.CreateFromPlacement(origin);
			if (!desertDescription.IsValid)
			{
				return false;
			}
			DesertBiome.ExportDescriptionToEngine(desertDescription);
			SandMound.Place(desertDescription, progress, 0f, 0.1f);
			desertDescription.UpdateSurfaceMap();
			if (!Main.tenthAnniversaryWorld && GenBase._random.NextDouble() <= this.ChanceOfEntrance && !WorldGen.SecretSeed.extraLiquid.Enabled)
			{
				switch (GenBase._random.Next(4))
				{
				case 0:
					ChambersEntrance.Place(desertDescription, progress, 0.1f, 0.2f);
					break;
				case 1:
					AnthillEntrance.Place(desertDescription, progress, 0.1f, 0.2f);
					break;
				case 2:
					LarvaHoleEntrance.Place(desertDescription, progress, 0.1f, 0.2f);
					break;
				case 3:
					PitEntrance.Place(desertDescription, progress, 0.1f, 0.2f);
					break;
				}
			}
			DesertHive.Place(desertDescription, progress, 0.2f, 0.75f);
			DesertBiome.CleanupArea(desertDescription.Hive, progress, 0.75f, 1f);
			Rectangle rectangle = new Rectangle(desertDescription.CombinedArea.X, 50, desertDescription.CombinedArea.Width, desertDescription.CombinedArea.Bottom - 20);
			structures.AddStructure(rectangle, 10);
			return true;
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x0062518A File Offset: 0x0062338A
		private static void ExportDescriptionToEngine(DesertDescription description)
		{
			GenVars.UndergroundDesertLocation = description.CombinedArea;
			GenVars.UndergroundDesertLocation.Inflate(10, 10);
			GenVars.UndergroundDesertHiveLocation = description.Hive;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x006251B0 File Offset: 0x006233B0
		private static void CleanupArea(Rectangle area, GenerationProgress progress, float progressMin, float progressMax)
		{
			int num = 20 - area.Left;
			int num2 = num + area.Right + 20;
			for (int i = -20 + area.Left; i < area.Right + 20; i++)
			{
				progress.Set((double)((float)(i + num) / (float)num2), (double)progressMin, (double)progressMax);
				for (int j = -20 + area.Top; j < area.Bottom + 20; j++)
				{
					if (i > 0 && i < Main.maxTilesX - 1 && j > 0 && j < Main.maxTilesY - 1)
					{
						WorldGen.SquareWallFrame(i, j, true);
						WorldUtils.TileFrame(i, j, true);
					}
				}
			}
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x0062524F File Offset: 0x0062344F
		public DesertBiome()
		{
		}

		// Token: 0x04005B25 RID: 23333
		[JsonProperty("ChanceOfEntrance")]
		public double ChanceOfEntrance = 0.3333;
	}
}
