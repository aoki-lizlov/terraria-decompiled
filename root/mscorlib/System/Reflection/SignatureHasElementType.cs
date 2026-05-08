using System;

namespace System.Reflection
{
	// Token: 0x02000896 RID: 2198
	internal abstract class SignatureHasElementType : SignatureType
	{
		// Token: 0x060049C0 RID: 18880 RVA: 0x000EF308 File Offset: 0x000ED508
		protected SignatureHasElementType(SignatureType elementType)
		{
			this._elementType = elementType;
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060049C1 RID: 18881 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060049C2 RID: 18882 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x00003FB7 File Offset: 0x000021B7
		protected sealed override bool HasElementTypeImpl()
		{
			return true;
		}

		// Token: 0x060049C4 RID: 18884
		protected abstract override bool IsArrayImpl();

		// Token: 0x060049C5 RID: 18885
		protected abstract override bool IsByRefImpl();

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x060049C6 RID: 18886 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060049C7 RID: 18887
		protected abstract override bool IsPointerImpl();

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x060049C8 RID: 18888
		public abstract override bool IsSZArray { get; }

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x060049C9 RID: 18889
		public abstract override bool IsVariableBoundArray { get; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x060049CA RID: 18890 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x060049CB RID: 18891 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x000EF317 File Offset: 0x000ED517
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				return this._elementType.ContainsGenericParameters;
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x000EF324 File Offset: 0x000ED524
		internal sealed override SignatureType ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x060049D0 RID: 18896
		public abstract override int GetArrayRank();

		// Token: 0x060049D1 RID: 18897 RVA: 0x000EF2F4 File Offset: 0x000ED4F4
		public sealed override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException("This operation is only valid on generic types.");
		}

		// Token: 0x060049D2 RID: 18898 RVA: 0x000EECEA File Offset: 0x000ECEEA
		public sealed override Type[] GetGenericArguments()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x060049D3 RID: 18899 RVA: 0x000EECEA File Offset: 0x000ECEEA
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return Array.Empty<Type>();
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x00047DF4 File Offset: 0x00045FF4
		public sealed override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x060049D5 RID: 18901 RVA: 0x000EF32C File Offset: 0x000ED52C
		public sealed override string Name
		{
			get
			{
				return this._elementType.Name + this.Suffix;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x060049D6 RID: 18902 RVA: 0x000EF344 File Offset: 0x000ED544
		public sealed override string Namespace
		{
			get
			{
				return this._elementType.Namespace;
			}
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x000EF351 File Offset: 0x000ED551
		public sealed override string ToString()
		{
			return this._elementType.ToString() + this.Suffix;
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x060049D8 RID: 18904
		protected abstract string Suffix { get; }

		// Token: 0x04002EB3 RID: 11955
		private readonly SignatureType _elementType;
	}
}
