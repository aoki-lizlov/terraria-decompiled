using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x02000625 RID: 1573
	[Serializable]
	internal sealed class MemberHolder
	{
		// Token: 0x06003C36 RID: 15414 RVA: 0x000D14C4 File Offset: 0x000CF6C4
		internal MemberHolder(Type type, StreamingContext ctx)
		{
			this._memberType = type;
			this._context = ctx;
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x000D14DA File Offset: 0x000CF6DA
		public override int GetHashCode()
		{
			return this._memberType.GetHashCode();
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x000D14E8 File Offset: 0x000CF6E8
		public override bool Equals(object obj)
		{
			MemberHolder memberHolder = obj as MemberHolder;
			return memberHolder != null && memberHolder._memberType == this._memberType && memberHolder._context.State == this._context.State;
		}

		// Token: 0x040026A5 RID: 9893
		internal readonly MemberInfo[] _members;

		// Token: 0x040026A6 RID: 9894
		internal readonly Type _memberType;

		// Token: 0x040026A7 RID: 9895
		internal readonly StreamingContext _context;
	}
}
