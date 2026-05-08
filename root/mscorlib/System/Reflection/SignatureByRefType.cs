using System;

namespace System.Reflection
{
	// Token: 0x02000892 RID: 2194
	internal sealed class SignatureByRefType : SignatureHasElementType
	{
		// Token: 0x06004985 RID: 18821 RVA: 0x000EF132 File Offset: 0x000ED332
		internal SignatureByRefType(SignatureType elementType)
			: base(elementType)
		{
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected sealed override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x000EF13B File Offset: 0x000ED33B
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x000EF147 File Offset: 0x000ED347
		protected sealed override string Suffix
		{
			get
			{
				return "&";
			}
		}
	}
}
