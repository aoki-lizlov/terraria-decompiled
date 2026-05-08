using System;

namespace System.Reflection
{
	// Token: 0x02000895 RID: 2197
	internal abstract class SignatureGenericParameterType : SignatureType
	{
		// Token: 0x060049A9 RID: 18857 RVA: 0x000EF2E5 File Offset: 0x000ED4E5
		protected SignatureGenericParameterType(int position)
		{
			this._position = position;
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsByRefLike
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060049B1 RID: 18865 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060049B2 RID: 18866 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x060049B3 RID: 18867 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x060049B4 RID: 18868 RVA: 0x00003FB7 File Offset: 0x000021B7
		public sealed override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x060049B5 RID: 18869
		public abstract override bool IsGenericMethodParameter { get; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x060049B6 RID: 18870 RVA: 0x00003FB7 File Offset: 0x000021B7
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060049B7 RID: 18871 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		internal sealed override SignatureType ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x000EF13B File Offset: 0x000ED33B
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x000EF2F4 File Offset: 0x000ED4F4
		public sealed override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException("This operation is only valid on generic types.");
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x000EECEA File Offset: 0x000ECEEA
		public sealed override Type[] GetGenericArguments()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x000EECEA File Offset: 0x000ECEEA
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return Array.Empty<Type>();
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x000EF300 File Offset: 0x000ED500
		public sealed override int GenericParameterPosition
		{
			get
			{
				return this._position;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060049BD RID: 18877
		public abstract override string Name { get; }

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060049BE RID: 18878 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public sealed override string Namespace
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x000486E4 File Offset: 0x000468E4
		public sealed override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04002EB2 RID: 11954
		private readonly int _position;
	}
}
