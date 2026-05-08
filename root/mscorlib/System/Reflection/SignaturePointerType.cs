using System;

namespace System.Reflection
{
	// Token: 0x02000897 RID: 2199
	internal sealed class SignaturePointerType : SignatureHasElementType
	{
		// Token: 0x060049D9 RID: 18905 RVA: 0x000EF132 File Offset: 0x000ED332
		internal SignaturePointerType(SignatureType elementType)
			: base(elementType)
		{
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected sealed override bool IsPointerImpl()
		{
			return true;
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060049DE RID: 18910 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060049DF RID: 18911 RVA: 0x000EF13B File Offset: 0x000ED33B
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060049E0 RID: 18912 RVA: 0x000EF369 File Offset: 0x000ED569
		protected sealed override string Suffix
		{
			get
			{
				return "*";
			}
		}
	}
}
