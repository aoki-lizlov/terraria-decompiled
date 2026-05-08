using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x0200062B RID: 1579
	internal sealed class ValueTypeFixupInfo
	{
		// Token: 0x06003C4F RID: 15439 RVA: 0x000D17F4 File Offset: 0x000CF9F4
		public ValueTypeFixupInfo(long containerID, FieldInfo member, int[] parentIndex)
		{
			if (member == null && parentIndex == null)
			{
				throw new ArgumentException("When supplying the ID of a containing object, the FieldInfo that identifies the current field within that object must also be supplied.");
			}
			if (containerID == 0L && member == null)
			{
				this._containerID = containerID;
				this._parentField = member;
				this._parentIndex = parentIndex;
			}
			if (member != null)
			{
				if (parentIndex != null)
				{
					throw new ArgumentException("Cannot supply both a MemberInfo and an Array to indicate the parent of a value type.");
				}
				if (member.FieldType.IsValueType && containerID == 0L)
				{
					throw new ArgumentException("When supplying a FieldInfo for fixing up a nested type, a valid ID for that containing object must also be supplied.");
				}
			}
			this._containerID = containerID;
			this._parentField = member;
			this._parentIndex = parentIndex;
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003C50 RID: 15440 RVA: 0x000D1886 File Offset: 0x000CFA86
		public long ContainerID
		{
			get
			{
				return this._containerID;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003C51 RID: 15441 RVA: 0x000D188E File Offset: 0x000CFA8E
		public FieldInfo ParentField
		{
			get
			{
				return this._parentField;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003C52 RID: 15442 RVA: 0x000D1896 File Offset: 0x000CFA96
		public int[] ParentIndex
		{
			get
			{
				return this._parentIndex;
			}
		}

		// Token: 0x040026B2 RID: 9906
		private readonly long _containerID;

		// Token: 0x040026B3 RID: 9907
		private readonly FieldInfo _parentField;

		// Token: 0x040026B4 RID: 9908
		private readonly int[] _parentIndex;
	}
}
