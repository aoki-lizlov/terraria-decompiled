using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004E RID: 78
	internal class ExpressionReflectionDelegateFactory : ReflectionDelegateFactory
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00010F69 File Offset: 0x0000F169
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return ExpressionReflectionDelegateFactory._instance;
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010F70 File Offset: 0x0000F170
		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			Type typeFromHandle = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]), "args");
			Expression expression = this.BuildMethodCall(method, typeFromHandle, null, parameterExpression);
			return (ObjectConstructor<object>)Expression.Lambda(typeof(ObjectConstructor<object>), expression, new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010FD8 File Offset: 0x0000F1D8
		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			Type typeFromHandle = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "target");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object[]), "args");
			Expression expression = this.BuildMethodCall(method, typeFromHandle, parameterExpression, parameterExpression2);
			return (MethodCall<T, object>)Expression.Lambda(typeof(MethodCall<T, object>), expression, new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile();
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00011050 File Offset: 0x0000F250
		private Expression BuildMethodCall(MethodBase method, Type type, ParameterExpression targetParameterExpression, ParameterExpression argsParameterExpression)
		{
			ParameterInfo[] parameters = method.GetParameters();
			Expression[] array;
			IList<ExpressionReflectionDelegateFactory.ByRefParameter> list;
			if (parameters.Length == 0)
			{
				array = CollectionUtils.ArrayEmpty<Expression>();
				list = CollectionUtils.ArrayEmpty<ExpressionReflectionDelegateFactory.ByRefParameter>();
			}
			else
			{
				array = new Expression[parameters.Length];
				list = new List<ExpressionReflectionDelegateFactory.ByRefParameter>();
				for (int i = 0; i < parameters.Length; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					Type type2 = parameterInfo.ParameterType;
					bool flag = false;
					if (type2.IsByRef)
					{
						type2 = type2.GetElementType();
						flag = true;
					}
					Expression expression = Expression.Constant(i);
					Expression expression2 = Expression.ArrayIndex(argsParameterExpression, expression);
					Expression expression3 = this.EnsureCastExpression(expression2, type2, !flag);
					if (flag)
					{
						ParameterExpression parameterExpression = Expression.Variable(type2);
						list.Add(new ExpressionReflectionDelegateFactory.ByRefParameter
						{
							Value = expression3,
							Variable = parameterExpression,
							IsOut = parameterInfo.IsOut
						});
						expression3 = parameterExpression;
					}
					array[i] = expression3;
				}
			}
			Expression expression4;
			if (method.IsConstructor)
			{
				expression4 = Expression.New((ConstructorInfo)method, array);
			}
			else if (method.IsStatic)
			{
				expression4 = Expression.Call((MethodInfo)method, array);
			}
			else
			{
				expression4 = Expression.Call(this.EnsureCastExpression(targetParameterExpression, method.DeclaringType, false), (MethodInfo)method, array);
			}
			MethodInfo methodInfo = method as MethodInfo;
			if (methodInfo != null)
			{
				if (methodInfo.ReturnType != typeof(void))
				{
					expression4 = this.EnsureCastExpression(expression4, type, false);
				}
				else
				{
					expression4 = Expression.Block(expression4, Expression.Constant(null));
				}
			}
			else
			{
				expression4 = this.EnsureCastExpression(expression4, type, false);
			}
			if (list.Count > 0)
			{
				IList<ParameterExpression> list2 = new List<ParameterExpression>();
				IList<Expression> list3 = new List<Expression>();
				foreach (ExpressionReflectionDelegateFactory.ByRefParameter byRefParameter in list)
				{
					if (!byRefParameter.IsOut)
					{
						list3.Add(Expression.Assign(byRefParameter.Variable, byRefParameter.Value));
					}
					list2.Add(byRefParameter.Variable);
				}
				list3.Add(expression4);
				expression4 = Expression.Block(list2, list3);
			}
			return expression4;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00011260 File Offset: 0x0000F460
		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsAbstract())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			Func<T> func;
			try
			{
				Type typeFromHandle = typeof(T);
				Expression expression = Expression.New(type);
				expression = this.EnsureCastExpression(expression, typeFromHandle, false);
				func = (Func<T>)Expression.Lambda(typeof(Func<T>), expression, new ParameterExpression[0]).Compile();
			}
			catch
			{
				func = () => (T)((object)Activator.CreateInstance(type));
			}
			return func;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001130C File Offset: 0x0000F50C
		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			Type typeFromHandle = typeof(T);
			Type typeFromHandle2 = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "instance");
			Expression expression;
			if (propertyInfo.GetGetMethod(true).IsStatic)
			{
				expression = Expression.MakeMemberAccess(null, propertyInfo);
			}
			else
			{
				expression = Expression.MakeMemberAccess(this.EnsureCastExpression(parameterExpression, propertyInfo.DeclaringType, false), propertyInfo);
			}
			expression = this.EnsureCastExpression(expression, typeFromHandle2, false);
			return (Func<T, object>)Expression.Lambda(typeof(Func<T, object>), expression, new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000113A0 File Offset: 0x0000F5A0
		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "source");
			Expression expression;
			if (fieldInfo.IsStatic)
			{
				expression = Expression.Field(null, fieldInfo);
			}
			else
			{
				expression = Expression.Field(this.EnsureCastExpression(parameterExpression, fieldInfo.DeclaringType, false), fieldInfo);
			}
			expression = this.EnsureCastExpression(expression, typeof(object), false);
			return Expression.Lambda<Func<T, object>>(expression, new ParameterExpression[] { parameterExpression }).Compile();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001141C File Offset: 0x0000F61C
		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			if (fieldInfo.DeclaringType.IsValueType() || fieldInfo.IsInitOnly)
			{
				return LateBoundReflectionDelegateFactory.Instance.CreateSet<T>(fieldInfo);
			}
			ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "source");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object), "value");
			Expression expression;
			if (fieldInfo.IsStatic)
			{
				expression = Expression.Field(null, fieldInfo);
			}
			else
			{
				expression = Expression.Field(this.EnsureCastExpression(parameterExpression, fieldInfo.DeclaringType, false), fieldInfo);
			}
			Expression expression2 = this.EnsureCastExpression(parameterExpression2, expression.Type, false);
			BinaryExpression binaryExpression = Expression.Assign(expression, expression2);
			return (Action<T, object>)Expression.Lambda(typeof(Action<T, object>), binaryExpression, new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile();
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000114E8 File Offset: 0x0000F6E8
		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			if (propertyInfo.DeclaringType.IsValueType())
			{
				return LateBoundReflectionDelegateFactory.Instance.CreateSet<T>(propertyInfo);
			}
			Type typeFromHandle = typeof(T);
			Type typeFromHandle2 = typeof(object);
			ParameterExpression parameterExpression = Expression.Parameter(typeFromHandle, "instance");
			ParameterExpression parameterExpression2 = Expression.Parameter(typeFromHandle2, "value");
			Expression expression = this.EnsureCastExpression(parameterExpression2, propertyInfo.PropertyType, false);
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			Expression expression2;
			if (setMethod.IsStatic)
			{
				expression2 = Expression.Call(setMethod, expression);
			}
			else
			{
				expression2 = Expression.Call(this.EnsureCastExpression(parameterExpression, propertyInfo.DeclaringType, false), setMethod, new Expression[] { expression });
			}
			return (Action<T, object>)Expression.Lambda(typeof(Action<T, object>), expression2, new ParameterExpression[] { parameterExpression, parameterExpression2 }).Compile();
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000115BC File Offset: 0x0000F7BC
		private Expression EnsureCastExpression(Expression expression, Type targetType, bool allowWidening = false)
		{
			Type type = expression.Type;
			if (type == targetType || (!type.IsValueType() && targetType.IsAssignableFrom(type)))
			{
				return expression;
			}
			if (targetType.IsValueType())
			{
				Expression expression2 = Expression.Unbox(expression, targetType);
				if (allowWidening && targetType.IsPrimitive())
				{
					MethodInfo method = typeof(Convert).GetMethod("To" + targetType.Name, new Type[] { typeof(object) });
					if (method != null)
					{
						expression2 = Expression.Condition(Expression.TypeIs(expression, targetType), expression2, Expression.Call(method, expression));
					}
				}
				return Expression.Condition(Expression.Equal(expression, Expression.Constant(null, typeof(object))), Expression.Default(targetType), expression2);
			}
			return Expression.Convert(expression, targetType);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00010F55 File Offset: 0x0000F155
		public ExpressionReflectionDelegateFactory()
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00011685 File Offset: 0x0000F885
		// Note: this type is marked as 'beforefieldinit'.
		static ExpressionReflectionDelegateFactory()
		{
		}

		// Token: 0x040001EA RID: 490
		private static readonly ExpressionReflectionDelegateFactory _instance = new ExpressionReflectionDelegateFactory();

		// Token: 0x02000121 RID: 289
		private class ByRefParameter
		{
			// Token: 0x06000C9A RID: 3226 RVA: 0x00008020 File Offset: 0x00006220
			public ByRefParameter()
			{
			}

			// Token: 0x04000472 RID: 1138
			public Expression Value;

			// Token: 0x04000473 RID: 1139
			public ParameterExpression Variable;

			// Token: 0x04000474 RID: 1140
			public bool IsOut;
		}

		// Token: 0x02000122 RID: 290
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0<T>
		{
			// Token: 0x06000C9B RID: 3227 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000C9C RID: 3228 RVA: 0x00030EAF File Offset: 0x0002F0AF
			internal T <CreateDefaultConstructor>b__0()
			{
				return (T)((object)Activator.CreateInstance(this.type));
			}

			// Token: 0x06000C9D RID: 3229 RVA: 0x00030EAF File Offset: 0x0002F0AF
			internal T <CreateDefaultConstructor>b__1()
			{
				return (T)((object)Activator.CreateInstance(this.type));
			}

			// Token: 0x04000475 RID: 1141
			public Type type;
		}
	}
}
