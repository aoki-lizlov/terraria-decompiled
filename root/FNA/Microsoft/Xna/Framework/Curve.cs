using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class Curve
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00009289 File Offset: 0x00007489
		public bool IsConstant
		{
			get
			{
				return this.Keys.Count <= 1;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0000929C File Offset: 0x0000749C
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x000092A4 File Offset: 0x000074A4
		public CurveKeyCollection Keys
		{
			[CompilerGenerated]
			get
			{
				return this.<Keys>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Keys>k__BackingField = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x000092AD File Offset: 0x000074AD
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x000092B5 File Offset: 0x000074B5
		public CurveLoopType PostLoop
		{
			[CompilerGenerated]
			get
			{
				return this.<PostLoop>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PostLoop>k__BackingField = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x000092BE File Offset: 0x000074BE
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x000092C6 File Offset: 0x000074C6
		public CurveLoopType PreLoop
		{
			[CompilerGenerated]
			get
			{
				return this.<PreLoop>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PreLoop>k__BackingField = value;
			}
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000092CF File Offset: 0x000074CF
		public Curve()
		{
			this.Keys = new CurveKeyCollection();
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000092E2 File Offset: 0x000074E2
		private Curve(CurveKeyCollection keys)
		{
			this.Keys = keys;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000092F1 File Offset: 0x000074F1
		public Curve Clone()
		{
			return new Curve(this.Keys.Clone())
			{
				PreLoop = this.PreLoop,
				PostLoop = this.PostLoop
			};
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0000931C File Offset: 0x0000751C
		public float Evaluate(float position)
		{
			if (this.Keys.Count == 0)
			{
				return 0f;
			}
			if (this.Keys.Count == 1)
			{
				return this.Keys[0].Value;
			}
			CurveKey curveKey = this.Keys[0];
			CurveKey curveKey2 = this.Keys[this.Keys.Count - 1];
			if (position < curveKey.Position)
			{
				switch (this.PreLoop)
				{
				case CurveLoopType.Constant:
					return curveKey.Value;
				case CurveLoopType.Cycle:
				{
					int num = this.GetNumberOfCycle(position);
					float num2 = position - (float)num * (curveKey2.Position - curveKey.Position);
					return this.GetCurvePosition(num2);
				}
				case CurveLoopType.CycleOffset:
				{
					int num = this.GetNumberOfCycle(position);
					float num2 = position - (float)num * (curveKey2.Position - curveKey.Position);
					return this.GetCurvePosition(num2) + (float)num * (curveKey2.Value - curveKey.Value);
				}
				case CurveLoopType.Oscillate:
				{
					int num = this.GetNumberOfCycle(position);
					float num2;
					if (0f == (float)num % 2f)
					{
						num2 = position - (float)num * (curveKey2.Position - curveKey.Position);
					}
					else
					{
						num2 = curveKey2.Position - position + curveKey.Position + (float)num * (curveKey2.Position - curveKey.Position);
					}
					return this.GetCurvePosition(num2);
				}
				case CurveLoopType.Linear:
					return curveKey.Value - curveKey.TangentIn * (curveKey.Position - position);
				}
			}
			else if (position > curveKey2.Position)
			{
				switch (this.PostLoop)
				{
				case CurveLoopType.Constant:
					return curveKey2.Value;
				case CurveLoopType.Cycle:
				{
					int num3 = this.GetNumberOfCycle(position);
					float num4 = position - (float)num3 * (curveKey2.Position - curveKey.Position);
					return this.GetCurvePosition(num4);
				}
				case CurveLoopType.CycleOffset:
				{
					int num3 = this.GetNumberOfCycle(position);
					float num4 = position - (float)num3 * (curveKey2.Position - curveKey.Position);
					return this.GetCurvePosition(num4) + (float)num3 * (curveKey2.Value - curveKey.Value);
				}
				case CurveLoopType.Oscillate:
				{
					int num3 = this.GetNumberOfCycle(position);
					float num4 = position - (float)num3 * (curveKey2.Position - curveKey.Position);
					if (0f == (float)num3 % 2f)
					{
						num4 = position - (float)num3 * (curveKey2.Position - curveKey.Position);
					}
					else
					{
						num4 = curveKey2.Position - position + curveKey.Position + (float)num3 * (curveKey2.Position - curveKey.Position);
					}
					return this.GetCurvePosition(num4);
				}
				case CurveLoopType.Linear:
					return curveKey2.Value + curveKey.TangentOut * (position - curveKey2.Position);
				}
			}
			return this.GetCurvePosition(position);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000095AA File Offset: 0x000077AA
		public void ComputeTangents(CurveTangent tangentType)
		{
			this.ComputeTangents(tangentType, tangentType);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000095B4 File Offset: 0x000077B4
		public void ComputeTangents(CurveTangent tangentInType, CurveTangent tangentOutType)
		{
			for (int i = 0; i < this.Keys.Count; i++)
			{
				this.ComputeTangent(i, tangentInType, tangentOutType);
			}
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000095E0 File Offset: 0x000077E0
		public void ComputeTangent(int keyIndex, CurveTangent tangentType)
		{
			this.ComputeTangent(keyIndex, tangentType, tangentType);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000095EC File Offset: 0x000077EC
		public void ComputeTangent(int keyIndex, CurveTangent tangentInType, CurveTangent tangentOutType)
		{
			CurveKey curveKey = this.Keys[keyIndex];
			float num3;
			float num2;
			float num = (num2 = (num3 = curveKey.Position));
			float num6;
			float num5;
			float num4 = (num5 = (num6 = curveKey.Value));
			if (keyIndex > 0)
			{
				num2 = this.Keys[keyIndex - 1].Position;
				num5 = this.Keys[keyIndex - 1].Value;
			}
			if (keyIndex < this.Keys.Count - 1)
			{
				num3 = this.Keys[keyIndex + 1].Position;
				num6 = this.Keys[keyIndex + 1].Value;
			}
			switch (tangentInType)
			{
			case CurveTangent.Flat:
				curveKey.TangentIn = 0f;
				break;
			case CurveTangent.Linear:
				curveKey.TangentIn = num4 - num5;
				break;
			case CurveTangent.Smooth:
			{
				float num7 = num3 - num2;
				if (MathHelper.WithinEpsilon(num7, 0f))
				{
					curveKey.TangentIn = 0f;
				}
				else
				{
					curveKey.TangentIn = (num6 - num5) * ((num - num2) / num7);
				}
				break;
			}
			}
			switch (tangentOutType)
			{
			case CurveTangent.Flat:
				curveKey.TangentOut = 0f;
				return;
			case CurveTangent.Linear:
				curveKey.TangentOut = num6 - num4;
				return;
			case CurveTangent.Smooth:
			{
				float num8 = num3 - num2;
				if (Math.Abs(num8) < 1E-45f)
				{
					curveKey.TangentOut = 0f;
					return;
				}
				curveKey.TangentOut = (num6 - num5) * ((num3 - num) / num8);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00009744 File Offset: 0x00007944
		private int GetNumberOfCycle(float position)
		{
			float num = (position - this.Keys[0].Position) / (this.Keys[this.Keys.Count - 1].Position - this.Keys[0].Position);
			if (num < 0f)
			{
				num -= 1f;
			}
			return (int)num;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000097A8 File Offset: 0x000079A8
		private float GetCurvePosition(float position)
		{
			CurveKey curveKey = this.Keys[0];
			int i = 1;
			while (i < this.Keys.Count)
			{
				CurveKey curveKey2 = this.Keys[i];
				if (curveKey2.Position >= position)
				{
					if (curveKey.Continuity != CurveContinuity.Step)
					{
						float num = (position - curveKey.Position) / (curveKey2.Position - curveKey.Position);
						float num2 = num * num;
						float num3 = num2 * num;
						return (2f * num3 - 3f * num2 + 1f) * curveKey.Value + (num3 - 2f * num2 + num) * curveKey.TangentOut + (3f * num2 - 2f * num3) * curveKey2.Value + (num3 - num2) * curveKey2.TangentIn;
					}
					if (position >= 1f)
					{
						return curveKey2.Value;
					}
					return curveKey.Value;
				}
				else
				{
					curveKey = curveKey2;
					i++;
				}
			}
			return 0f;
		}

		// Token: 0x040004A5 RID: 1189
		[CompilerGenerated]
		private CurveKeyCollection <Keys>k__BackingField;

		// Token: 0x040004A6 RID: 1190
		[CompilerGenerated]
		private CurveLoopType <PostLoop>k__BackingField;

		// Token: 0x040004A7 RID: 1191
		[CompilerGenerated]
		private CurveLoopType <PreLoop>k__BackingField;
	}
}
