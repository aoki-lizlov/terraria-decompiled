using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Input.Touch
{
	// Token: 0x0200006F RID: 111
	public struct GestureSample
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x00023AE3 File Offset: 0x00021CE3
		// (set) Token: 0x060010B4 RID: 4276 RVA: 0x00023AEB File Offset: 0x00021CEB
		public Vector2 Delta
		{
			[CompilerGenerated]
			get
			{
				return this.<Delta>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Delta>k__BackingField = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00023AF4 File Offset: 0x00021CF4
		// (set) Token: 0x060010B6 RID: 4278 RVA: 0x00023AFC File Offset: 0x00021CFC
		public Vector2 Delta2
		{
			[CompilerGenerated]
			get
			{
				return this.<Delta2>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Delta2>k__BackingField = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060010B7 RID: 4279 RVA: 0x00023B05 File Offset: 0x00021D05
		// (set) Token: 0x060010B8 RID: 4280 RVA: 0x00023B0D File Offset: 0x00021D0D
		public GestureType GestureType
		{
			[CompilerGenerated]
			get
			{
				return this.<GestureType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GestureType>k__BackingField = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x00023B16 File Offset: 0x00021D16
		// (set) Token: 0x060010BA RID: 4282 RVA: 0x00023B1E File Offset: 0x00021D1E
		public Vector2 Position
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

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00023B27 File Offset: 0x00021D27
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x00023B2F File Offset: 0x00021D2F
		public Vector2 Position2
		{
			[CompilerGenerated]
			get
			{
				return this.<Position2>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Position2>k__BackingField = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060010BD RID: 4285 RVA: 0x00023B38 File Offset: 0x00021D38
		// (set) Token: 0x060010BE RID: 4286 RVA: 0x00023B40 File Offset: 0x00021D40
		public TimeSpan Timestamp
		{
			[CompilerGenerated]
			get
			{
				return this.<Timestamp>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Timestamp>k__BackingField = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00023B49 File Offset: 0x00021D49
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x00023B51 File Offset: 0x00021D51
		public int FingerIdEXT
		{
			[CompilerGenerated]
			get
			{
				return this.<FingerIdEXT>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FingerIdEXT>k__BackingField = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060010C1 RID: 4289 RVA: 0x00023B5A File Offset: 0x00021D5A
		// (set) Token: 0x060010C2 RID: 4290 RVA: 0x00023B62 File Offset: 0x00021D62
		public int FingerId2EXT
		{
			[CompilerGenerated]
			get
			{
				return this.<FingerId2EXT>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FingerId2EXT>k__BackingField = value;
			}
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x00023B6C File Offset: 0x00021D6C
		public GestureSample(GestureType gestureType, TimeSpan timestamp, Vector2 position, Vector2 position2, Vector2 delta, Vector2 delta2)
		{
			this = default(GestureSample);
			this.GestureType = gestureType;
			this.Timestamp = timestamp;
			this.Position = position;
			this.Position2 = position2;
			this.Delta = delta;
			this.Delta2 = delta2;
			this.FingerIdEXT = -1;
			this.FingerId2EXT = -1;
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00023BBC File Offset: 0x00021DBC
		internal GestureSample(GestureType gestureType, TimeSpan timestamp, Vector2 position, Vector2 position2, Vector2 delta, Vector2 delta2, int fingerId, int fingerId2)
		{
			this = default(GestureSample);
			this.GestureType = gestureType;
			this.Timestamp = timestamp;
			this.Position = position;
			this.Position2 = position2;
			this.Delta = delta;
			this.Delta2 = delta2;
			this.FingerIdEXT = fingerId;
			this.FingerId2EXT = fingerId2;
		}

		// Token: 0x0400077E RID: 1918
		[CompilerGenerated]
		private Vector2 <Delta>k__BackingField;

		// Token: 0x0400077F RID: 1919
		[CompilerGenerated]
		private Vector2 <Delta2>k__BackingField;

		// Token: 0x04000780 RID: 1920
		[CompilerGenerated]
		private GestureType <GestureType>k__BackingField;

		// Token: 0x04000781 RID: 1921
		[CompilerGenerated]
		private Vector2 <Position>k__BackingField;

		// Token: 0x04000782 RID: 1922
		[CompilerGenerated]
		private Vector2 <Position2>k__BackingField;

		// Token: 0x04000783 RID: 1923
		[CompilerGenerated]
		private TimeSpan <Timestamp>k__BackingField;

		// Token: 0x04000784 RID: 1924
		[CompilerGenerated]
		private int <FingerIdEXT>k__BackingField;

		// Token: 0x04000785 RID: 1925
		[CompilerGenerated]
		private int <FingerId2EXT>k__BackingField;
	}
}
