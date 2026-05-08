using System;

namespace System.Reflection
{
	// Token: 0x0200088D RID: 2189
	public abstract class ReflectionContext
	{
		// Token: 0x06004970 RID: 18800 RVA: 0x000025BE File Offset: 0x000007BE
		protected ReflectionContext()
		{
		}

		// Token: 0x06004971 RID: 18801
		public abstract Assembly MapAssembly(Assembly assembly);

		// Token: 0x06004972 RID: 18802
		public abstract TypeInfo MapType(TypeInfo type);

		// Token: 0x06004973 RID: 18803 RVA: 0x000EEF5C File Offset: 0x000ED15C
		public virtual TypeInfo GetTypeForObject(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.MapType(value.GetType().GetTypeInfo());
		}
	}
}
