using System;

namespace System.Reflection
{
	// Token: 0x02000876 RID: 2166
	public static class IntrospectionExtensions
	{
		// Token: 0x06004883 RID: 18563 RVA: 0x000EE2EC File Offset: 0x000EC4EC
		public static TypeInfo GetTypeInfo(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IReflectableType reflectableType = type as IReflectableType;
			if (reflectableType != null)
			{
				return reflectableType.GetTypeInfo();
			}
			return new TypeDelegator(type);
		}
	}
}
