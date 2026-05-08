using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020008B0 RID: 2224
	public static class CustomAttributeExtensions
	{
		// Token: 0x06004B10 RID: 19216 RVA: 0x000F0B09 File Offset: 0x000EED09
		public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x000F0B12 File Offset: 0x000EED12
		public static Attribute GetCustomAttribute(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x000F0B1B File Offset: 0x000EED1B
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x000F0B24 File Offset: 0x000EED24
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x000F0B2D File Offset: 0x000EED2D
		public static T GetCustomAttribute<T>(this Assembly element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x000F0B44 File Offset: 0x000EED44
		public static T GetCustomAttribute<T>(this Module element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x000F0B5B File Offset: 0x000EED5B
		public static T GetCustomAttribute<T>(this MemberInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x000F0B72 File Offset: 0x000EED72
		public static T GetCustomAttribute<T>(this ParameterInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x000F0B89 File Offset: 0x000EED89
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x000F0B93 File Offset: 0x000EED93
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x06004B1A RID: 19226 RVA: 0x000F0B9D File Offset: 0x000EED9D
		public static T GetCustomAttribute<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		// Token: 0x06004B1B RID: 19227 RVA: 0x000F0BB5 File Offset: 0x000EEDB5
		public static T GetCustomAttribute<T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		// Token: 0x06004B1C RID: 19228 RVA: 0x000F0BCD File Offset: 0x000EEDCD
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x06004B1D RID: 19229 RVA: 0x000F0BD5 File Offset: 0x000EEDD5
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x06004B1E RID: 19230 RVA: 0x000F0BDD File Offset: 0x000EEDDD
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x06004B1F RID: 19231 RVA: 0x000F0BE5 File Offset: 0x000EEDE5
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x000F0BED File Offset: 0x000EEDED
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		// Token: 0x06004B21 RID: 19233 RVA: 0x000F0BF6 File Offset: 0x000EEDF6
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x000F0BFF File Offset: 0x000EEDFF
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x06004B23 RID: 19235 RVA: 0x000F0C08 File Offset: 0x000EEE08
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x06004B24 RID: 19236 RVA: 0x000F0C11 File Offset: 0x000EEE11
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x06004B25 RID: 19237 RVA: 0x000F0C1A File Offset: 0x000EEE1A
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x06004B26 RID: 19238 RVA: 0x000F0C23 File Offset: 0x000EEE23
		public static IEnumerable<T> GetCustomAttributes<T>(this Assembly element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x000F0C3A File Offset: 0x000EEE3A
		public static IEnumerable<T> GetCustomAttributes<T>(this Module element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x06004B28 RID: 19240 RVA: 0x000F0C51 File Offset: 0x000EEE51
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x06004B29 RID: 19241 RVA: 0x000F0C68 File Offset: 0x000EEE68
		public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x06004B2A RID: 19242 RVA: 0x000F0C7F File Offset: 0x000EEE7F
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		// Token: 0x06004B2B RID: 19243 RVA: 0x000F0C89 File Offset: 0x000EEE89
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x000F0C93 File Offset: 0x000EEE93
		public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x000F0CAB File Offset: 0x000EEEAB
		public static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x000F0CC3 File Offset: 0x000EEEC3
		public static bool IsDefined(this Assembly element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x000F0CCC File Offset: 0x000EEECC
		public static bool IsDefined(this Module element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x06004B30 RID: 19248 RVA: 0x000F0CD5 File Offset: 0x000EEED5
		public static bool IsDefined(this MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x000F0CDE File Offset: 0x000EEEDE
		public static bool IsDefined(this ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x06004B32 RID: 19250 RVA: 0x000F0CE7 File Offset: 0x000EEEE7
		public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}

		// Token: 0x06004B33 RID: 19251 RVA: 0x000F0CF1 File Offset: 0x000EEEF1
		public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}
	}
}
