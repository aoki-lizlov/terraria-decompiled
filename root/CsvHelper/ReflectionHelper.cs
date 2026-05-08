using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using CsvHelper.Configuration;

namespace CsvHelper
{
	// Token: 0x02000017 RID: 23
	internal static class ReflectionHelper
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00005855 File Offset: 0x00003A55
		public static T CreateInstance<T>(params object[] args)
		{
			return (T)((object)ReflectionHelper.CreateInstance(typeof(T), args));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000586C File Offset: 0x00003A6C
		public static object CreateInstance(Type type, params object[] args)
		{
			Type[] array = Enumerable.ToArray<Type>(Enumerable.Select<object, Type>(args, (object a) => a.GetType()));
			ParameterExpression[] array2 = Enumerable.ToArray<ParameterExpression>(Enumerable.Select<Type, ParameterExpression>(array, (Type t, int i) => Expression.Parameter(t, "var" + i)));
			Delegate @delegate = Expression.Lambda(Expression.New(type.GetConstructor(array), array2), array2).Compile();
			object obj;
			try
			{
				obj = @delegate.DynamicInvoke(args);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return obj;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000590C File Offset: 0x00003B0C
		public static T GetAttribute<T>(PropertyInfo property, bool inherit) where T : Attribute
		{
			T t = default(T);
			List<object> list = Enumerable.ToList<object>(property.GetCustomAttributes(typeof(T), inherit));
			if (list.Count > 0)
			{
				t = list[0] as T;
			}
			return t;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005954 File Offset: 0x00003B54
		public static T[] GetAttributes<T>(PropertyInfo property, bool inherit) where T : Attribute
		{
			return Enumerable.ToArray<T>(Enumerable.Cast<T>(property.GetCustomAttributes(typeof(T), inherit)));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005971 File Offset: 0x00003B71
		public static NewExpression GetConstructor<T>(Expression<Func<T>> expression)
		{
			NewExpression newExpression = expression.Body as NewExpression;
			if (newExpression == null)
			{
				throw new ArgumentException("Not a constructor expression.", "expression");
			}
			return newExpression;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005994 File Offset: 0x00003B94
		public static PropertyInfo GetProperty<TModel>(Expression<Func<TModel, object>> expression)
		{
			MemberInfo member = ReflectionHelper.GetMemberExpression<TModel, object>(expression).Member;
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo == null)
			{
				throw new CsvConfigurationException(string.Format("'{0}' is not a property. Did you try to map a field by accident?", member.Name));
			}
			return propertyInfo;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000059D4 File Offset: 0x00003BD4
		private static MemberExpression GetMemberExpression<TModel, T>(Expression<Func<TModel, T>> expression)
		{
			MemberExpression memberExpression = null;
			if (expression.Body.NodeType == 10)
			{
				memberExpression = ((UnaryExpression)expression.Body).Operand as MemberExpression;
			}
			else if (expression.Body.NodeType == 23)
			{
				memberExpression = expression.Body as MemberExpression;
			}
			if (memberExpression == null)
			{
				throw new ArgumentException("Not a member access", "expression");
			}
			return memberExpression;
		}

		// Token: 0x02000049 RID: 73
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000261 RID: 609 RVA: 0x00008103 File Offset: 0x00006303
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000262 RID: 610 RVA: 0x00002253 File Offset: 0x00000453
			public <>c()
			{
			}

			// Token: 0x06000263 RID: 611 RVA: 0x0000810F File Offset: 0x0000630F
			internal Type <CreateInstance>b__1_0(object a)
			{
				return a.GetType();
			}

			// Token: 0x06000264 RID: 612 RVA: 0x00008117 File Offset: 0x00006317
			internal ParameterExpression <CreateInstance>b__1_1(Type t, int i)
			{
				return Expression.Parameter(t, "var" + i);
			}

			// Token: 0x0400008E RID: 142
			public static readonly ReflectionHelper.<>c <>9 = new ReflectionHelper.<>c();

			// Token: 0x0400008F RID: 143
			public static Func<object, Type> <>9__1_0;

			// Token: 0x04000090 RID: 144
			public static Func<Type, int, ParameterExpression> <>9__1_1;
		}
	}
}
