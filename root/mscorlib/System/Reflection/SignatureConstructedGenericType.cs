using System;
using System.Text;

namespace System.Reflection
{
	// Token: 0x02000893 RID: 2195
	internal sealed class SignatureConstructedGenericType : SignatureType
	{
		// Token: 0x0600498D RID: 18829 RVA: 0x000EF150 File Offset: 0x000ED350
		internal SignatureConstructedGenericType(Type genericTypeDefinition, Type[] typeArguments)
		{
			if (genericTypeDefinition == null)
			{
				throw new ArgumentNullException("genericTypeDefinition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			typeArguments = (Type[])typeArguments.Clone();
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			this._genericTypeDefinition = genericTypeDefinition;
			this._genericTypeArguments = typeArguments;
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool HasElementTypeImpl()
		{
			return false;
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06004993 RID: 18835 RVA: 0x000EF1C4 File Offset: 0x000ED3C4
		public sealed override bool IsByRefLike
		{
			get
			{
				return this._genericTypeDefinition.IsByRefLike;
			}
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x0000408A File Offset: 0x0000228A
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06004996 RID: 18838 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004997 RID: 18839 RVA: 0x00003FB7 File Offset: 0x000021B7
		public sealed override bool IsConstructedGenericType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004998 RID: 18840 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004999 RID: 18841 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericTypeParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600499A RID: 18842 RVA: 0x0000408A File Offset: 0x0000228A
		public sealed override bool IsGenericMethodParameter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600499B RID: 18843 RVA: 0x000EF1D4 File Offset: 0x000ED3D4
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				for (int i = 0; i < this._genericTypeArguments.Length; i++)
				{
					if (this._genericTypeArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600499C RID: 18844 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		internal sealed override SignatureType ElementType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x000EF13B File Offset: 0x000ED33B
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x000EF206 File Offset: 0x000ED406
		public sealed override Type GetGenericTypeDefinition()
		{
			return this._genericTypeDefinition;
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x000EF20E File Offset: 0x000ED40E
		public sealed override Type[] GetGenericArguments()
		{
			return this.GenericTypeArguments;
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x000EF216 File Offset: 0x000ED416
		public sealed override Type[] GenericTypeArguments
		{
			get
			{
				return (Type[])this._genericTypeArguments.Clone();
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x00047DF4 File Offset: 0x00045FF4
		public sealed override int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException("Method may only be called on a Type for which Type.IsGenericParameter is true.");
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060049A2 RID: 18850 RVA: 0x000EF228 File Offset: 0x000ED428
		public sealed override string Name
		{
			get
			{
				return this._genericTypeDefinition.Name;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060049A3 RID: 18851 RVA: 0x000EF235 File Offset: 0x000ED435
		public sealed override string Namespace
		{
			get
			{
				return this._genericTypeDefinition.Namespace;
			}
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x000EF244 File Offset: 0x000ED444
		public sealed override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._genericTypeDefinition.ToString());
			stringBuilder.Append('[');
			for (int i = 0; i < this._genericTypeArguments.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(this._genericTypeArguments[i].ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		// Token: 0x04002EB0 RID: 11952
		private readonly Type _genericTypeDefinition;

		// Token: 0x04002EB1 RID: 11953
		private readonly Type[] _genericTypeArguments;
	}
}
