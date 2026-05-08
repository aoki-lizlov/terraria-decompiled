using System;

namespace System.Reflection.Emit
{
	// Token: 0x020008E2 RID: 2274
	internal class GenericInstanceKey
	{
		// Token: 0x06004DFC RID: 19964 RVA: 0x000F6048 File Offset: 0x000F4248
		internal GenericInstanceKey(Type gtd, Type[] args)
		{
			this.gtd = gtd;
			this.args = args;
			this.hash_code = gtd.GetHashCode();
			for (int i = 0; i < args.Length; i++)
			{
				this.hash_code ^= args[i].GetHashCode();
			}
		}

		// Token: 0x06004DFD RID: 19965 RVA: 0x000F6098 File Offset: 0x000F4298
		private static bool IsBoundedVector(Type type)
		{
			ArrayType arrayType = type as ArrayType;
			if (arrayType != null)
			{
				return arrayType.GetEffectiveRank() == 1;
			}
			return type.ToString().EndsWith("[*]", StringComparison.Ordinal);
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x000F60D0 File Offset: 0x000F42D0
		private static bool TypeEquals(Type a, Type b)
		{
			if (a == b)
			{
				return true;
			}
			if (!a.HasElementType)
			{
				if (a.IsGenericType)
				{
					if (!b.IsGenericType)
					{
						return false;
					}
					if (a.IsGenericParameter)
					{
						return a == b;
					}
					if (a.IsGenericParameter)
					{
						return false;
					}
					if (a.IsGenericTypeDefinition)
					{
						if (!b.IsGenericTypeDefinition)
						{
							return false;
						}
					}
					else
					{
						if (b.IsGenericTypeDefinition)
						{
							return false;
						}
						if (!GenericInstanceKey.TypeEquals(a.GetGenericTypeDefinition(), b.GetGenericTypeDefinition()))
						{
							return false;
						}
						Type[] genericArguments = a.GetGenericArguments();
						Type[] genericArguments2 = b.GetGenericArguments();
						for (int i = 0; i < genericArguments.Length; i++)
						{
							if (!GenericInstanceKey.TypeEquals(genericArguments[i], genericArguments2[i]))
							{
								return false;
							}
						}
					}
				}
				return a == b;
			}
			if (!b.HasElementType)
			{
				return false;
			}
			if (!GenericInstanceKey.TypeEquals(a.GetElementType(), b.GetElementType()))
			{
				return false;
			}
			if (a.IsArray)
			{
				if (!b.IsArray)
				{
					return false;
				}
				int arrayRank = a.GetArrayRank();
				if (arrayRank != b.GetArrayRank())
				{
					return false;
				}
				if (arrayRank == 1 && GenericInstanceKey.IsBoundedVector(a) != GenericInstanceKey.IsBoundedVector(b))
				{
					return false;
				}
			}
			else if (a.IsByRef)
			{
				if (!b.IsByRef)
				{
					return false;
				}
			}
			else if (a.IsPointer && !b.IsPointer)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x000F6200 File Offset: 0x000F4400
		public override bool Equals(object obj)
		{
			GenericInstanceKey genericInstanceKey = obj as GenericInstanceKey;
			if (genericInstanceKey == null)
			{
				return false;
			}
			if (this.gtd != genericInstanceKey.gtd)
			{
				return false;
			}
			for (int i = 0; i < this.args.Length; i++)
			{
				Type type = this.args[i];
				Type type2 = genericInstanceKey.args[i];
				if (type != type2 && !type.Equals(type2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x000F6268 File Offset: 0x000F4468
		public override int GetHashCode()
		{
			return this.hash_code;
		}

		// Token: 0x04003061 RID: 12385
		private Type gtd;

		// Token: 0x04003062 RID: 12386
		internal Type[] args;

		// Token: 0x04003063 RID: 12387
		private int hash_code;
	}
}
