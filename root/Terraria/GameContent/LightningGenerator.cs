using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Utilities;
using Terraria.Utilities.Terraria.Utilities;

namespace Terraria.GameContent
{
	// Token: 0x02000239 RID: 569
	public class LightningGenerator
	{
		// Token: 0x06002279 RID: 8825 RVA: 0x00538944 File Offset: 0x00536B44
		public LightningGenerator.Bolt Generate(List<LightningGenerator.Bolt> bolts, uint seed, Vector2 sourcePosition, Vector2 targetPosition, bool calcPositions, bool calcRotations)
		{
			LightningGenerator.Bolt bolt = this.GenerateBolt(bolts, seed, 0, calcPositions, sourcePosition, targetPosition, this.RotationStrength, (float)this.StepSize, new FloatRange(0f, 1f));
			if (calcRotations)
			{
				foreach (LightningGenerator.Bolt bolt2 in bolts)
				{
					bolt2.rotations = LightningGenerator.CalcRotations(bolt2.positions);
					LightningGenerator.SmoothRotations(bolt2.rotations);
				}
			}
			return bolt;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x005389D8 File Offset: 0x00536BD8
		private LightningGenerator.Bolt GenerateBolt(List<LightningGenerator.Bolt> bolts, uint seed, int depth, bool calcPositions, Vector2 startPos, Vector2 targetPos, float rotationStrength, float stepSize, FloatRange progressRange)
		{
			LCG32Random lcg32Random = new LCG32Random(seed);
			float num = 0f;
			float[] array = new float[this.Layers];
			Point point = targetPos.ToTileCoordinates();
			Vector2 vector = startPos;
			Vector2 vector2 = targetPos - startPos;
			float num2 = vector2.Length();
			vector2 /= num2;
			Vector2 vector3 = new Vector2(vector2.Y, -vector2.X);
			int num3 = (int)(num2 * 2f / stepSize);
			int num4 = 0;
			Vector2[] array2 = (calcPositions ? new Vector2[num3] : null);
			LightningGenerator.Bolt bolt = new LightningGenerator.Bolt
			{
				positions = array2,
				forkDepth = depth,
				progressRange = progressRange
			};
			int i;
			for (i = 0; i < num3; i++)
			{
				if (calcPositions)
				{
					array2[i] = vector;
				}
				Vector2 vector4 = targetPos - vector;
				float num5 = Vector2.Dot(vector4, vector2);
				if (num5 < stepSize)
				{
					break;
				}
				float num6 = MathHelper.Clamp(1f - num5 / num2, 0f, 1f);
				if (this.SolidTileCollision && vector.ToTileCoordinates() != point && this.TileCollision(vector))
				{
					bolt.progressRange = new FloatRange(progressRange.Minimum, progressRange.Lerp(num6));
					bolt.collidedWithTile = true;
					break;
				}
				vector4 /= vector4.Length();
				float num7 = -Vector2.Dot(vector4, vector3);
				float num8 = Math.Max(0.01f, Math.Min(num6, 1f - num6) * this.PerpendicularDeviationFactor * 2f);
				float num9 = MathHelper.Clamp(num7 / num8, -1f, 1f);
				int num10;
				if (this.PickLayerToReroll(lcg32Random.NextDouble(), 0.5f, out num10))
				{
					float num11 = rotationStrength;
					for (int j = this.Layers - 1; j > num10; j--)
					{
						num11 /= this.LayerStrengthFactor;
					}
					float num12 = (float)lcg32Random.NextDouble() * 2f - 1f;
					num12 += (num9 - num12 * Math.Abs(num9)) / 2f;
					float num13 = num12 * num11;
					float num14 = array[num10];
					float num15 = num13 - num14;
					num += num15;
					array[num10] = num13;
					if (num10 == this.Layers - 1)
					{
						float num16 = lcg32Random.NextFloat();
						float num17 = Utils.Remap((float)num4, 0f, (float)this.MaxForksPerBolt, 1f, 0f, true);
						float num18 = num - num15 * (1f + this.ForkReflectAngleMultiplier);
						if (bolts != null && Math.Abs(num15) >= rotationStrength * this.ForkGenerationThresholdAngleFraction && this.ForkProgressRange.Contains(num6) && depth < this.MaxForkDepth && num16 < num17 && Math.Abs(num18) < 1.3962635f)
						{
							num4++;
							float num19 = (1f - num6) * this.ForkLengthMultiplier;
							Vector2 vector5 = vector + vector4.RotatedBy((double)num18, default(Vector2)) * num2 * num19;
							this.GenerateBolt(bolts, lcg32Random.state + 1U, depth + 1, calcPositions, vector, vector5, rotationStrength * this.ForkRotationStrengthMultiplier, stepSize * this.ForkStepSizeMultiplier, new FloatRange(progressRange.Lerp(num6), progressRange.Lerp(num6 + num19)));
						}
					}
				}
				float num20 = Utils.Remap(num6, this.ReduceRandomnessAfter, 1f, 0f, 1f, true);
				num20 += Utils.Remap(Math.Abs(num9), 0.5f, 1f, 0f, 1f, true);
				if (this.PickHighLayerToReroll(lcg32Random.NextDouble(), num20, out num10))
				{
					num -= array[num10];
					array[num10] = 0f;
				}
				vector += vector4.RotatedBy((double)num, default(Vector2)) * stepSize;
			}
			if (calcPositions && i < num3)
			{
				Array.Resize<Vector2>(ref array2, i + 1);
				bolt.positions = array2;
			}
			if (bolts != null && i > 2)
			{
				bolts.Add(bolt);
			}
			return bolt;
		}

		// Token: 0x0600227B RID: 8827 RVA: 0x00538DF0 File Offset: 0x00536FF0
		private bool TileCollision(Vector2 pos)
		{
			Point point = pos.ToTileCoordinates();
			if (!WorldGen.InWorld(point, 0) || Main.tile[point.X, point.Y] == null)
			{
				return false;
			}
			if (WorldGen.SolidOrSlopedTile(point.X, point.Y))
			{
				return true;
			}
			int liquid = (int)Main.tile[point.X, point.Y].liquid;
			return liquid > 0 && (int)pos.Y % 16 > 16 * (255 - liquid) / 255;
		}

		// Token: 0x0600227C RID: 8828 RVA: 0x00538E7B File Offset: 0x0053707B
		private bool PickLayerToReroll(double r, float chance, out int layer)
		{
			for (layer = 0; layer < this.Layers; layer++)
			{
				if (r >= (double)(1f - chance))
				{
					return true;
				}
				r /= (double)chance;
			}
			return false;
		}

		// Token: 0x0600227D RID: 8829 RVA: 0x00538EA6 File Offset: 0x005370A6
		private bool PickHighLayerToReroll(double r, float chance, out int layer)
		{
			if (!this.PickLayerToReroll(r, chance, out layer))
			{
				return false;
			}
			layer = this.Layers - 1 - layer;
			return true;
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x00538EC4 File Offset: 0x005370C4
		private static float[] CalcRotations(Vector2[] positions)
		{
			float[] array = new float[positions.Length];
			if (array.Length < 2)
			{
				return array;
			}
			int i = 0;
			float num = (positions[0] - positions[1]).ToRotation();
			array[i++] = num;
			while (i < array.Length - 1)
			{
				float num2 = (positions[i] - positions[i + 1]).ToRotation();
				array[i++] = num + MathHelper.WrapAngle(num2 - num) / 2f;
				num = num2;
			}
			array[i] = num;
			return array;
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x00538F48 File Offset: 0x00537148
		private static void SmoothRotations(float[] rotations)
		{
			float num = rotations[0];
			for (int i = 1; i < rotations.Length - 1; i++)
			{
				float num2 = rotations[i];
				float num3 = rotations[i + 1];
				rotations[i] = num2 + (MathHelper.WrapAngle(num - num2) + MathHelper.WrapAngle(num3 - num2)) / 2f;
				num = num2;
			}
		}

		// Token: 0x06002280 RID: 8832 RVA: 0x0000357B File Offset: 0x0000177B
		public LightningGenerator()
		{
		}

		// Token: 0x04004CF1 RID: 19697
		public bool SolidTileCollision;

		// Token: 0x04004CF2 RID: 19698
		public float RotationStrength;

		// Token: 0x04004CF3 RID: 19699
		public int StepSize;

		// Token: 0x04004CF4 RID: 19700
		public int Layers;

		// Token: 0x04004CF5 RID: 19701
		public float LayerStrengthFactor;

		// Token: 0x04004CF6 RID: 19702
		public float PerpendicularDeviationFactor;

		// Token: 0x04004CF7 RID: 19703
		public float ReduceRandomnessAfter;

		// Token: 0x04004CF8 RID: 19704
		public float ForkGenerationThresholdAngleFraction;

		// Token: 0x04004CF9 RID: 19705
		public float ForkReflectAngleMultiplier;

		// Token: 0x04004CFA RID: 19706
		public float ForkRotationStrengthMultiplier;

		// Token: 0x04004CFB RID: 19707
		public float ForkStepSizeMultiplier;

		// Token: 0x04004CFC RID: 19708
		public float ForkLengthMultiplier;

		// Token: 0x04004CFD RID: 19709
		public int MaxForksPerBolt;

		// Token: 0x04004CFE RID: 19710
		public int MaxForkDepth;

		// Token: 0x04004CFF RID: 19711
		public FloatRange ForkProgressRange;

		// Token: 0x020007C3 RID: 1987
		public class Bolt
		{
			// Token: 0x17000531 RID: 1329
			// (get) Token: 0x0600420C RID: 16908 RVA: 0x006BD993 File Offset: 0x006BBB93
			public bool IsMainBolt
			{
				get
				{
					return this.forkDepth == 0;
				}
			}

			// Token: 0x0600420D RID: 16909 RVA: 0x0000357B File Offset: 0x0000177B
			public Bolt()
			{
			}

			// Token: 0x040070DF RID: 28895
			public Vector2[] positions;

			// Token: 0x040070E0 RID: 28896
			public float[] rotations;

			// Token: 0x040070E1 RID: 28897
			public FloatRange progressRange;

			// Token: 0x040070E2 RID: 28898
			public int forkDepth;

			// Token: 0x040070E3 RID: 28899
			public bool collidedWithTile;
		}

		// Token: 0x020007C4 RID: 1988
		public static class StormLightning
		{
			// Token: 0x0600420E RID: 16910 RVA: 0x006BD99E File Offset: 0x006BBB9E
			public static bool CanHitTarget(uint seed, Vector2 targetPosition)
			{
				return !LightningGenerator.StormLightning.Generate(null, seed, targetPosition, false, false).collidedWithTile;
			}

			// Token: 0x0600420F RID: 16911 RVA: 0x006BD9B2 File Offset: 0x006BBBB2
			public static LightningGenerator.Bolt GenerateMainBoltPath(uint seed, Vector2 targetPosition)
			{
				return LightningGenerator.StormLightning.Generate(null, seed, targetPosition, true, false);
			}

			// Token: 0x06004210 RID: 16912 RVA: 0x006BD9C0 File Offset: 0x006BBBC0
			public static LightningGenerator.Bolt Generate(List<LightningGenerator.Bolt> bolts, uint seed, Vector2 targetPosition, bool calcPositions = true, bool calcRotations = true)
			{
				LCG32Random lcg32Random = new LCG32Random(seed);
				Vector2 vector = -Vector2.UnitY.RotatedBy((lcg32Random.NextDouble() * 2.0 - 1.0) * (double)LightningGenerator.StormLightning.SourceRotationLimit, default(Vector2));
				return LightningGenerator.StormLightning.Generator.Generate(bolts, seed, targetPosition + vector * LightningGenerator.StormLightning.Length, targetPosition, calcPositions, calcRotations);
			}

			// Token: 0x06004211 RID: 16913 RVA: 0x006BDA34 File Offset: 0x006BBC34
			// Note: this type is marked as 'beforefieldinit'.
			static StormLightning()
			{
			}

			// Token: 0x040070E4 RID: 28900
			public static LightningGenerator Generator = new LightningGenerator
			{
				RotationStrength = 0.9f,
				StepSize = 8,
				Layers = 4,
				LayerStrengthFactor = 1.5f,
				PerpendicularDeviationFactor = 5f,
				ReduceRandomnessAfter = 0.8f,
				ForkGenerationThresholdAngleFraction = 0.65f,
				ForkReflectAngleMultiplier = 0.4f,
				ForkRotationStrengthMultiplier = 0.9f,
				ForkStepSizeMultiplier = 0.8f,
				ForkLengthMultiplier = 0.8f,
				MaxForksPerBolt = 2,
				MaxForkDepth = 2,
				ForkProgressRange = new FloatRange(0.3f, 0.8f),
				SolidTileCollision = true
			};

			// Token: 0x040070E5 RID: 28901
			private static float SourceRotationLimit = 0.34906587f;

			// Token: 0x040070E6 RID: 28902
			private static float Length = 1000f;
		}
	}
}
