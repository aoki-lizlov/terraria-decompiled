using System;

namespace System.Reflection
{
	// Token: 0x02000891 RID: 2193
	internal sealed class SignatureArrayType : SignatureHasElementType
	{
		// Token: 0x0600497D RID: 18813 RVA: 0x000EF0C3 File Offset: 0x000ED2C3
		internal SignatureArrayType(SignatureType elementType, int rank, bool isMultiDim)
			: base(elementType)
		{
			this._rank = rank;
			this._isMultiDim = isMultiDim;
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected sealed override bool IsArrayImpl()
		{
			return true;
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x000EF0DA File Offset: 0x000ED2DA
		public sealed override bool IsSZArray
		{
			get
			{
				return !this._isMultiDim;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004982 RID: 18818 RVA: 0x000EF0E5 File Offset: 0x000ED2E5
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return this._isMultiDim;
			}
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x000EF0ED File Offset: 0x000ED2ED
		public sealed override int GetArrayRank()
		{
			return this._rank;
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x000EF0F5 File Offset: 0x000ED2F5
		protected sealed override string Suffix
		{
			get
			{
				if (!this._isMultiDim)
				{
					return "[]";
				}
				if (this._rank == 1)
				{
					return "[*]";
				}
				return "[" + new string(',', this._rank - 1) + "]";
			}
		}

		// Token: 0x04002EAE RID: 11950
		private readonly int _rank;

		// Token: 0x04002EAF RID: 11951
		private readonly bool _isMultiDim;
	}
}
