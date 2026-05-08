using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class CurveKey : IEquatable<CurveKey>, IComparable<CurveKey>
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00009899 File Offset: 0x00007A99
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x000098A1 File Offset: 0x00007AA1
		public CurveContinuity Continuity
		{
			[CompilerGenerated]
			get
			{
				return this.<Continuity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Continuity>k__BackingField = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x000098AA File Offset: 0x00007AAA
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x000098B2 File Offset: 0x00007AB2
		public float Position
		{
			[CompilerGenerated]
			get
			{
				return this.<Position>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Position>k__BackingField = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000098BB File Offset: 0x00007ABB
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x000098C3 File Offset: 0x00007AC3
		public float TangentIn
		{
			[CompilerGenerated]
			get
			{
				return this.<TangentIn>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TangentIn>k__BackingField = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000098CC File Offset: 0x00007ACC
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x000098D4 File Offset: 0x00007AD4
		public float TangentOut
		{
			[CompilerGenerated]
			get
			{
				return this.<TangentOut>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TangentOut>k__BackingField = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x000098DD File Offset: 0x00007ADD
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x000098E5 File Offset: 0x00007AE5
		public float Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000098EE File Offset: 0x00007AEE
		public CurveKey(float position, float value)
			: this(position, value, 0f, 0f, CurveContinuity.Smooth)
		{
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00009903 File Offset: 0x00007B03
		public CurveKey(float position, float value, float tangentIn, float tangentOut)
			: this(position, value, tangentIn, tangentOut, CurveContinuity.Smooth)
		{
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00009911 File Offset: 0x00007B11
		public CurveKey(float position, float value, float tangentIn, float tangentOut, CurveContinuity continuity)
		{
			this.Position = position;
			this.Value = value;
			this.TangentIn = tangentIn;
			this.TangentOut = tangentOut;
			this.Continuity = continuity;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000993E File Offset: 0x00007B3E
		public CurveKey Clone()
		{
			return new CurveKey(this.Position, this.Value, this.TangentIn, this.TangentOut, this.Continuity);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00009964 File Offset: 0x00007B64
		public int CompareTo(CurveKey other)
		{
			return this.Position.CompareTo(other.Position);
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00009985 File Offset: 0x00007B85
		public bool Equals(CurveKey other)
		{
			return this == other;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000998E File Offset: 0x00007B8E
		public static bool operator !=(CurveKey a, CurveKey b)
		{
			return !(a == b);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0000999C File Offset: 0x00007B9C
		public static bool operator ==(CurveKey a, CurveKey b)
		{
			if (object.Equals(a, null))
			{
				return object.Equals(b, null);
			}
			if (object.Equals(b, null))
			{
				return object.Equals(a, null);
			}
			return a.Position == b.Position && a.Value == b.Value && a.TangentIn == b.TangentIn && a.TangentOut == b.TangentOut && a.Continuity == b.Continuity;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00009A13 File Offset: 0x00007C13
		public override bool Equals(object obj)
		{
			return obj as CurveKey == this;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00009A24 File Offset: 0x00007C24
		public override int GetHashCode()
		{
			return this.Position.GetHashCode() ^ this.Value.GetHashCode() ^ this.TangentIn.GetHashCode() ^ this.TangentOut.GetHashCode() ^ this.Continuity.GetHashCode();
		}

		// Token: 0x040004AB RID: 1195
		[CompilerGenerated]
		private CurveContinuity <Continuity>k__BackingField;

		// Token: 0x040004AC RID: 1196
		[CompilerGenerated]
		private float <Position>k__BackingField;

		// Token: 0x040004AD RID: 1197
		[CompilerGenerated]
		private float <TangentIn>k__BackingField;

		// Token: 0x040004AE RID: 1198
		[CompilerGenerated]
		private float <TangentOut>k__BackingField;

		// Token: 0x040004AF RID: 1199
		[CompilerGenerated]
		private float <Value>k__BackingField;
	}
}
