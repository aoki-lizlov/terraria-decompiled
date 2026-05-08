using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Terraria.GameContent.Biomes.CaveHouse;
using Terraria.ID;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes
{
	// Token: 0x0200050D RID: 1293
	public class CaveHouseBiome : MicroBiome
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x00624B7C File Offset: 0x00622D7C
		// (set) Token: 0x06003647 RID: 13895 RVA: 0x00624B84 File Offset: 0x00622D84
		[JsonProperty]
		public double IceChestChance
		{
			[CompilerGenerated]
			get
			{
				return this.<IceChestChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IceChestChance>k__BackingField = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x00624B8D File Offset: 0x00622D8D
		// (set) Token: 0x06003649 RID: 13897 RVA: 0x00624B95 File Offset: 0x00622D95
		[JsonProperty]
		public double JungleChestChance
		{
			[CompilerGenerated]
			get
			{
				return this.<JungleChestChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<JungleChestChance>k__BackingField = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x00624B9E File Offset: 0x00622D9E
		// (set) Token: 0x0600364B RID: 13899 RVA: 0x00624BA6 File Offset: 0x00622DA6
		[JsonProperty]
		public double GoldChestChance
		{
			[CompilerGenerated]
			get
			{
				return this.<GoldChestChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GoldChestChance>k__BackingField = value;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600364C RID: 13900 RVA: 0x00624BAF File Offset: 0x00622DAF
		// (set) Token: 0x0600364D RID: 13901 RVA: 0x00624BB7 File Offset: 0x00622DB7
		[JsonProperty]
		public double GraniteChestChance
		{
			[CompilerGenerated]
			get
			{
				return this.<GraniteChestChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GraniteChestChance>k__BackingField = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x00624BC0 File Offset: 0x00622DC0
		// (set) Token: 0x0600364F RID: 13903 RVA: 0x00624BC8 File Offset: 0x00622DC8
		[JsonProperty]
		public double MarbleChestChance
		{
			[CompilerGenerated]
			get
			{
				return this.<MarbleChestChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MarbleChestChance>k__BackingField = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x00624BD1 File Offset: 0x00622DD1
		// (set) Token: 0x06003651 RID: 13905 RVA: 0x00624BD9 File Offset: 0x00622DD9
		[JsonProperty]
		public double MushroomChestChance
		{
			[CompilerGenerated]
			get
			{
				return this.<MushroomChestChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MushroomChestChance>k__BackingField = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x00624BE2 File Offset: 0x00622DE2
		// (set) Token: 0x06003653 RID: 13907 RVA: 0x00624BEA File Offset: 0x00622DEA
		[JsonProperty]
		public double DesertChestChance
		{
			[CompilerGenerated]
			get
			{
				return this.<DesertChestChance>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DesertChestChance>k__BackingField = value;
			}
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x00624BF4 File Offset: 0x00622DF4
		public override bool Place(Point origin, StructureMap structures, GenerationProgress progress)
		{
			if (!WorldGen.InWorld(origin.X, origin.Y, 30))
			{
				return false;
			}
			int num = 25;
			for (int i = origin.X - num; i <= origin.X + num; i++)
			{
				for (int j = origin.Y - num; j <= origin.Y + num; j++)
				{
					if (Main.tile[i, j].wire())
					{
						return false;
					}
					if (TileID.Sets.BasicChest[(int)Main.tile[i, j].type])
					{
						return false;
					}
				}
			}
			HouseBuilder houseBuilder = HouseUtils.CreateBuilder(origin, structures);
			if (!houseBuilder.IsValid)
			{
				return false;
			}
			this.ApplyConfigurationToBuilder(houseBuilder);
			houseBuilder.Place(this._builderContext, structures);
			return true;
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x00624CA8 File Offset: 0x00622EA8
		private void ApplyConfigurationToBuilder(HouseBuilder builder)
		{
			switch (builder.Type)
			{
			case HouseType.Wood:
				builder.ChestChance = this.GoldChestChance;
				return;
			case HouseType.Ice:
				builder.ChestChance = this.IceChestChance;
				return;
			case HouseType.Desert:
				builder.ChestChance = this.DesertChestChance;
				return;
			case HouseType.Jungle:
				builder.ChestChance = this.JungleChestChance;
				return;
			case HouseType.Mushroom:
				builder.ChestChance = this.MushroomChestChance;
				return;
			case HouseType.Granite:
				builder.ChestChance = this.GraniteChestChance;
				return;
			case HouseType.Marble:
				builder.ChestChance = this.MarbleChestChance;
				return;
			default:
				return;
			}
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x00624D39 File Offset: 0x00622F39
		public CaveHouseBiome()
		{
		}

		// Token: 0x04005B1C RID: 23324
		[CompilerGenerated]
		private double <IceChestChance>k__BackingField;

		// Token: 0x04005B1D RID: 23325
		[CompilerGenerated]
		private double <JungleChestChance>k__BackingField;

		// Token: 0x04005B1E RID: 23326
		[CompilerGenerated]
		private double <GoldChestChance>k__BackingField;

		// Token: 0x04005B1F RID: 23327
		[CompilerGenerated]
		private double <GraniteChestChance>k__BackingField;

		// Token: 0x04005B20 RID: 23328
		[CompilerGenerated]
		private double <MarbleChestChance>k__BackingField;

		// Token: 0x04005B21 RID: 23329
		[CompilerGenerated]
		private double <MushroomChestChance>k__BackingField;

		// Token: 0x04005B22 RID: 23330
		[CompilerGenerated]
		private double <DesertChestChance>k__BackingField;

		// Token: 0x04005B23 RID: 23331
		private readonly HouseBuilderContext _builderContext = new HouseBuilderContext();
	}
}
