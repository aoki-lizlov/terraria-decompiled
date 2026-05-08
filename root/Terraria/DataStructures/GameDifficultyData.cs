using System;

namespace Terraria.DataStructures
{
	// Token: 0x02000547 RID: 1351
	public static class GameDifficultyData
	{
		// Token: 0x0600377D RID: 14205 RVA: 0x0062F2B0 File Offset: 0x0062D4B0
		// Note: this type is marked as 'beforefieldinit'.
		static GameDifficultyData()
		{
		}

		// Token: 0x04005BAA RID: 23466
		public static readonly GameDifficultyData.LinearCurve EnemyMaxLifeMultiplier = new GameDifficultyData.LinearCurve(new GameDifficultyData.LinearCurve.Key[]
		{
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Journey, 0.5f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Legendary, 4f)
		});

		// Token: 0x04005BAB RID: 23467
		public static readonly GameDifficultyData.LinearCurve EnemyDamageMultiplier = new GameDifficultyData.LinearCurve(new GameDifficultyData.LinearCurve.Key[]
		{
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Journey, 0.5f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Master, 3f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Legendary, 5.3333335f)
		});

		// Token: 0x04005BAC RID: 23468
		public static readonly GameDifficultyData.LinearCurve HostileProjectileDamageMultiplier = new GameDifficultyData.LinearCurve(new GameDifficultyData.LinearCurve.Key[]
		{
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Journey, 0.5f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Master, 3f)
		});

		// Token: 0x04005BAD RID: 23469
		public static readonly GameDifficultyData.LinearCurve KnockbackToEnemiesMultiplier = new GameDifficultyData.LinearCurve(new GameDifficultyData.LinearCurve.Key[]
		{
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Classic, 1f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Master, 0.8f)
		});

		// Token: 0x04005BAE RID: 23470
		public static readonly GameDifficultyData.LinearCurve EnemyMoneyDropMultiplier = new GameDifficultyData.LinearCurve(new GameDifficultyData.LinearCurve.Key[]
		{
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Classic, 1f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Expert, 2.5f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Master, 2.5f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Legendary, 3.5f)
		});

		// Token: 0x04005BAF RID: 23471
		public static readonly GameDifficultyData.LinearCurve TownNPCDamageMultiplier = new GameDifficultyData.LinearCurve(new GameDifficultyData.LinearCurve.Key[]
		{
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Journey, 2f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Classic, 1f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Expert, 1.5f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Legendary, 2f)
		});

		// Token: 0x04005BB0 RID: 23472
		public static readonly GameDifficultyData.LinearCurve DebuffTimeMultiplier = new GameDifficultyData.LinearCurve(new GameDifficultyData.LinearCurve.Key[]
		{
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Classic, 1f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Expert, 2f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Master, 2.5f)
		});

		// Token: 0x04005BB1 RID: 23473
		public static readonly GameDifficultyData.LinearCurve LightningPlayerDamageScaling = new GameDifficultyData.LinearCurve(new GameDifficultyData.LinearCurve.Key[]
		{
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Journey, 0.04f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Classic, 0.08f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Master, 0.24f),
			new GameDifficultyData.LinearCurve.Key(GameDifficultyLevel.Legendary, 0.4f)
		});

		// Token: 0x020009B8 RID: 2488
		public struct LinearCurve
		{
			// Token: 0x06004A2F RID: 18991 RVA: 0x006D3E0C File Offset: 0x006D200C
			public LinearCurve(params GameDifficultyData.LinearCurve.Key[] keys)
			{
				this.keys = keys;
				for (int i = 1; i < keys.Length; i++)
				{
					float input = keys[i].input;
				}
			}

			// Token: 0x06004A30 RID: 18992 RVA: 0x006D3E44 File Offset: 0x006D2044
			public float Sample(float value)
			{
				GameDifficultyData.LinearCurve.Key key = this.keys[0];
				GameDifficultyData.LinearCurve.Key key2 = key;
				for (int i = 0; i < this.keys.Length; i++)
				{
					key2 = this.keys[i];
					if (value <= key2.input)
					{
						break;
					}
					key = key2;
				}
				float num = key2.input - key.input;
				float num2 = key2.output - key.output;
				if (num == 0f)
				{
					return key.output;
				}
				return (value - key.input) * num2 / num + key.output;
			}

			// Token: 0x06004A31 RID: 18993 RVA: 0x006D3ECE File Offset: 0x006D20CE
			public override string ToString()
			{
				return string.Join<GameDifficultyData.LinearCurve.Key>(", ", this.keys);
			}

			// Token: 0x040076C8 RID: 30408
			public readonly GameDifficultyData.LinearCurve.Key[] keys;

			// Token: 0x02000B13 RID: 2835
			public struct Key
			{
				// Token: 0x06004DB2 RID: 19890 RVA: 0x006DC8CD File Offset: 0x006DAACD
				public Key(float input, float output)
				{
					this.input = input;
					this.output = output;
				}

				// Token: 0x06004DB3 RID: 19891 RVA: 0x006DC8DD File Offset: 0x006DAADD
				public override string ToString()
				{
					return this.input + " -> " + this.output;
				}

				// Token: 0x04007919 RID: 31001
				public readonly float input;

				// Token: 0x0400791A RID: 31002
				public readonly float output;
			}
		}
	}
}
