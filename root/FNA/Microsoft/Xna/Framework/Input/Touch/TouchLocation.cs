using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Input.Touch
{
	// Token: 0x02000072 RID: 114
	public struct TouchLocation : IEquatable<TouchLocation>
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00023D92 File Offset: 0x00021F92
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x00023D9A File Offset: 0x00021F9A
		public int Id
		{
			[CompilerGenerated]
			get
			{
				return this.<Id>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Id>k__BackingField = value;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00023DA3 File Offset: 0x00021FA3
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x00023DAB File Offset: 0x00021FAB
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00023DB4 File Offset: 0x00021FB4
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00023DBC File Offset: 0x00021FBC
		public TouchLocationState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<State>k__BackingField = value;
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x00023DC5 File Offset: 0x00021FC5
		public TouchLocation(int id, TouchLocationState state, Vector2 position)
		{
			this = default(TouchLocation);
			this.Id = id;
			this.State = state;
			this.Position = position;
			this.prevState = TouchLocationState.Invalid;
			this.prevPosition = Vector2.Zero;
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00023DF5 File Offset: 0x00021FF5
		public TouchLocation(int id, TouchLocationState state, Vector2 position, TouchLocationState previousState, Vector2 previousPosition)
		{
			this = default(TouchLocation);
			this.Id = id;
			this.State = state;
			this.Position = position;
			this.prevState = previousState;
			this.prevPosition = previousPosition;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00023E24 File Offset: 0x00022024
		public bool Equals(TouchLocation other)
		{
			return this.Id == other.Id && this.Position == other.Position && this.State == other.State && this.prevPosition == other.prevPosition && this.prevState == other.prevState;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00023E86 File Offset: 0x00022086
		public override bool Equals(object obj)
		{
			return obj is TouchLocation && this.Equals((TouchLocation)obj);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00023EA0 File Offset: 0x000220A0
		public override int GetHashCode()
		{
			return this.Id.GetHashCode() + this.Position.GetHashCode();
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00023ED0 File Offset: 0x000220D0
		public override string ToString()
		{
			return "{Position:" + this.Position.ToString() + "}";
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00023F00 File Offset: 0x00022100
		public bool TryGetPreviousLocation(out TouchLocation previousLocation)
		{
			previousLocation = new TouchLocation(this.Id, this.prevState, this.prevPosition);
			return previousLocation.State > TouchLocationState.Invalid;
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00023F28 File Offset: 0x00022128
		public static bool operator ==(TouchLocation value1, TouchLocation value2)
		{
			return value1.Equals(value2);
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00023F32 File Offset: 0x00022132
		public static bool operator !=(TouchLocation value1, TouchLocation value2)
		{
			return !value1.Equals(value2);
		}

		// Token: 0x04000793 RID: 1939
		[CompilerGenerated]
		private int <Id>k__BackingField;

		// Token: 0x04000794 RID: 1940
		[CompilerGenerated]
		private Vector2 <Position>k__BackingField;

		// Token: 0x04000795 RID: 1941
		[CompilerGenerated]
		private TouchLocationState <State>k__BackingField;

		// Token: 0x04000796 RID: 1942
		private Vector2 prevPosition;

		// Token: 0x04000797 RID: 1943
		private TouchLocationState prevState;
	}
}
