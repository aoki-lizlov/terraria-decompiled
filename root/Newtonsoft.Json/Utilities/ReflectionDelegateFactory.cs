using System;
using System.Globalization;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000054 RID: 84
	internal abstract class ReflectionDelegateFactory
	{
		// Token: 0x06000448 RID: 1096 RVA: 0x00011E14 File Offset: 0x00010014
		public Func<T, object> CreateGet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateGet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateGet<T>(fieldInfo);
			}
			throw new Exception("Could not create getter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00011E68 File Offset: 0x00010068
		public Action<T, object> CreateSet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateSet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateSet<T>(fieldInfo);
			}
			throw new Exception("Could not create setter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
		}

		// Token: 0x0600044A RID: 1098
		public abstract MethodCall<T, object> CreateMethodCall<T>(MethodBase method);

		// Token: 0x0600044B RID: 1099
		public abstract ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method);

		// Token: 0x0600044C RID: 1100
		public abstract Func<T> CreateDefaultConstructor<T>(Type type);

		// Token: 0x0600044D RID: 1101
		public abstract Func<T, object> CreateGet<T>(PropertyInfo propertyInfo);

		// Token: 0x0600044E RID: 1102
		public abstract Func<T, object> CreateGet<T>(FieldInfo fieldInfo);

		// Token: 0x0600044F RID: 1103
		public abstract Action<T, object> CreateSet<T>(FieldInfo fieldInfo);

		// Token: 0x06000450 RID: 1104
		public abstract Action<T, object> CreateSet<T>(PropertyInfo propertyInfo);

		// Token: 0x06000451 RID: 1105 RVA: 0x00008020 File Offset: 0x00006220
		protected ReflectionDelegateFactory()
		{
		}
	}
}
