using System;
using System.Numerics.Hashing;

namespace System
{
	// Token: 0x0200019B RID: 411
	public readonly struct SequencePosition : IEquatable<SequencePosition>
	{
		// Token: 0x06001333 RID: 4915 RVA: 0x0004E5AD File Offset: 0x0004C7AD
		public SequencePosition(object @object, int integer)
		{
			this._object = @object;
			this._integer = integer;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0004E5BD File Offset: 0x0004C7BD
		public object GetObject()
		{
			return this._object;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004E5C5 File Offset: 0x0004C7C5
		public int GetInteger()
		{
			return this._integer;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004E5CD File Offset: 0x0004C7CD
		public bool Equals(SequencePosition other)
		{
			return this._integer == other._integer && object.Equals(this._object, other._object);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004E5F0 File Offset: 0x0004C7F0
		public override bool Equals(object obj)
		{
			if (obj is SequencePosition)
			{
				SequencePosition sequencePosition = (SequencePosition)obj;
				return this.Equals(sequencePosition);
			}
			return false;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0004E615 File Offset: 0x0004C815
		public override int GetHashCode()
		{
			object @object = this._object;
			return HashHelpers.Combine((@object != null) ? @object.GetHashCode() : 0, this._integer);
		}

		// Token: 0x0400132C RID: 4908
		private readonly object _object;

		// Token: 0x0400132D RID: 4909
		private readonly int _integer;
	}
}
