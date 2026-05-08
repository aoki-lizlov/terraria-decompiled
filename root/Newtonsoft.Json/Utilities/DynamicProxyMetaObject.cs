using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000048 RID: 72
	internal sealed class DynamicProxyMetaObject<T> : DynamicMetaObject
	{
		// Token: 0x060003DD RID: 989 RVA: 0x0000FBC9 File Offset: 0x0000DDC9
		internal DynamicProxyMetaObject(Expression expression, T value, DynamicProxy<T> proxy)
			: base(expression, BindingRestrictions.Empty, value)
		{
			this._proxy = proxy;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		private bool IsOverridden(string method)
		{
			return ReflectionUtils.IsMethodOverridden(this._proxy.GetType(), typeof(DynamicProxy<T>), method);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			if (!this.IsOverridden("TryGetMember"))
			{
				return base.BindGetMember(binder);
			}
			return this.CallMethodWithResult("TryGetMember", binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackGetMember(this, e), null);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000FC64 File Offset: 0x0000DE64
		public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			if (!this.IsOverridden("TrySetMember"))
			{
				return base.BindSetMember(binder, value);
			}
			return this.CallMethodReturnLast("TrySetMember", binder, DynamicProxyMetaObject<T>.GetArgs(new DynamicMetaObject[] { value }), (DynamicMetaObject e) => binder.FallbackSetMember(this, value, e));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			if (!this.IsOverridden("TryDeleteMember"))
			{
				return base.BindDeleteMember(binder);
			}
			return this.CallMethodNoResult("TryDeleteMember", binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackDeleteMember(this, e));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000FD40 File Offset: 0x0000DF40
		public override DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			if (!this.IsOverridden("TryConvert"))
			{
				return base.BindConvert(binder);
			}
			return this.CallMethodWithResult("TryConvert", binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackConvert(this, e), null);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000FDA0 File Offset: 0x0000DFA0
		public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryInvokeMember"))
			{
				return base.BindInvokeMember(binder, args);
			}
			DynamicProxyMetaObject<T>.Fallback fallback = (DynamicMetaObject e) => binder.FallbackInvokeMember(this, args, e);
			return this.BuildCallMethodWithResult("TryInvokeMember", binder, DynamicProxyMetaObject<T>.GetArgArray(args), this.BuildCallMethodWithResult("TryGetMember", new DynamicProxyMetaObject<T>.GetBinderAdapter(binder), DynamicProxyMetaObject<T>.NoArgs, fallback(null), (DynamicMetaObject e) => binder.FallbackInvoke(e, args, null)), null);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000FE40 File Offset: 0x0000E040
		public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryCreateInstance"))
			{
				return base.BindCreateInstance(binder, args);
			}
			return this.CallMethodWithResult("TryCreateInstance", binder, DynamicProxyMetaObject<T>.GetArgArray(args), (DynamicMetaObject e) => binder.FallbackCreateInstance(this, args, e), null);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000FEB4 File Offset: 0x0000E0B4
		public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			if (!this.IsOverridden("TryInvoke"))
			{
				return base.BindInvoke(binder, args);
			}
			return this.CallMethodWithResult("TryInvoke", binder, DynamicProxyMetaObject<T>.GetArgArray(args), (DynamicMetaObject e) => binder.FallbackInvoke(this, args, e), null);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000FF28 File Offset: 0x0000E128
		public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			if (!this.IsOverridden("TryBinaryOperation"))
			{
				return base.BindBinaryOperation(binder, arg);
			}
			return this.CallMethodWithResult("TryBinaryOperation", binder, DynamicProxyMetaObject<T>.GetArgs(new DynamicMetaObject[] { arg }), (DynamicMetaObject e) => binder.FallbackBinaryOperation(this, arg, e), null);
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000FFA4 File Offset: 0x0000E1A4
		public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			if (!this.IsOverridden("TryUnaryOperation"))
			{
				return base.BindUnaryOperation(binder);
			}
			return this.CallMethodWithResult("TryUnaryOperation", binder, DynamicProxyMetaObject<T>.NoArgs, (DynamicMetaObject e) => binder.FallbackUnaryOperation(this, e), null);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00010004 File Offset: 0x0000E204
		public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			if (!this.IsOverridden("TryGetIndex"))
			{
				return base.BindGetIndex(binder, indexes);
			}
			return this.CallMethodWithResult("TryGetIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes), (DynamicMetaObject e) => binder.FallbackGetIndex(this, indexes, e), null);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010078 File Offset: 0x0000E278
		public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			if (!this.IsOverridden("TrySetIndex"))
			{
				return base.BindSetIndex(binder, indexes, value);
			}
			return this.CallMethodReturnLast("TrySetIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes, value), (DynamicMetaObject e) => binder.FallbackSetIndex(this, indexes, value, e));
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000100FC File Offset: 0x0000E2FC
		public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			if (!this.IsOverridden("TryDeleteIndex"))
			{
				return base.BindDeleteIndex(binder, indexes);
			}
			return this.CallMethodNoResult("TryDeleteIndex", binder, DynamicProxyMetaObject<T>.GetArgArray(indexes), (DynamicMetaObject e) => binder.FallbackDeleteIndex(this, indexes, e));
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001016C File Offset: 0x0000E36C
		private static Expression[] NoArgs
		{
			get
			{
				return CollectionUtils.ArrayEmpty<Expression>();
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010173 File Offset: 0x0000E373
		private static IEnumerable<Expression> GetArgs(params DynamicMetaObject[] args)
		{
			return Enumerable.Select<DynamicMetaObject, Expression>(args, delegate(DynamicMetaObject arg)
			{
				Expression expression = arg.Expression;
				if (!expression.Type.IsValueType())
				{
					return expression;
				}
				return Expression.Convert(expression, typeof(object));
			});
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001019A File Offset: 0x0000E39A
		private static Expression[] GetArgArray(DynamicMetaObject[] args)
		{
			return new NewArrayExpression[] { Expression.NewArrayInit(typeof(object), DynamicProxyMetaObject<T>.GetArgs(args)) };
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000101BC File Offset: 0x0000E3BC
		private static Expression[] GetArgArray(DynamicMetaObject[] args, DynamicMetaObject value)
		{
			Expression expression = value.Expression;
			return new Expression[]
			{
				Expression.NewArrayInit(typeof(object), DynamicProxyMetaObject<T>.GetArgs(args)),
				expression.Type.IsValueType() ? Expression.Convert(expression, typeof(object)) : expression
			};
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00010214 File Offset: 0x0000E414
		private static ConstantExpression Constant(DynamicMetaObjectBinder binder)
		{
			Type type = binder.GetType();
			while (!type.IsVisible())
			{
				type = type.BaseType();
			}
			return Expression.Constant(binder, type);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00010240 File Offset: 0x0000E440
		private DynamicMetaObject CallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, DynamicProxyMetaObject<T>.Fallback fallback, DynamicProxyMetaObject<T>.Fallback fallbackInvoke = null)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			return this.BuildCallMethodWithResult(methodName, binder, args, dynamicMetaObject, fallbackInvoke);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010264 File Offset: 0x0000E464
		private DynamicMetaObject BuildCallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, DynamicMetaObject fallbackResult, DynamicProxyMetaObject<T>.Fallback fallbackInvoke)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			list.Add(parameterExpression);
			DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
			if (binder.ReturnType != typeof(object))
			{
				dynamicMetaObject = new DynamicMetaObject(Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType), dynamicMetaObject.Restrictions);
			}
			if (fallbackInvoke != null)
			{
				dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
			}
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[] { parameterExpression }, new Expression[] { Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), dynamicMetaObject.Expression, fallbackResult.Expression, binder.ReturnType) }), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00010380 File Offset: 0x0000E580
		private DynamicMetaObject CallMethodReturnLast(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, DynamicProxyMetaObject<T>.Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			list[list.Count - 1] = Expression.Assign(parameterExpression, list[list.Count - 1]);
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[] { parameterExpression }, new Expression[] { Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), parameterExpression, dynamicMetaObject.Expression, typeof(object)) }), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010464 File Offset: 0x0000E664
		private DynamicMetaObject CallMethodNoResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, DynamicProxyMetaObject<T>.Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(DynamicProxyMetaObject<T>.Constant(binder));
			list.AddRange(args);
			return new DynamicMetaObject(Expression.Condition(Expression.Call(Expression.Constant(this._proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), Expression.Empty(), dynamicMetaObject.Expression, typeof(void)), this.GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000104FF File Offset: 0x0000E6FF
		private BindingRestrictions GetRestrictions()
		{
			if (base.Value != null || !base.HasValue)
			{
				return BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
			}
			return BindingRestrictions.GetInstanceRestriction(base.Expression, null);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001052F File Offset: 0x0000E72F
		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return this._proxy.GetDynamicMemberNames((T)((object)base.Value));
		}

		// Token: 0x040001E5 RID: 485
		private readonly DynamicProxy<T> _proxy;

		// Token: 0x02000110 RID: 272
		// (Invoke) Token: 0x06000C72 RID: 3186
		private delegate DynamicMetaObject Fallback(DynamicMetaObject errorSuggestion);

		// Token: 0x02000111 RID: 273
		private sealed class GetBinderAdapter : GetMemberBinder
		{
			// Token: 0x06000C75 RID: 3189 RVA: 0x00030AE3 File Offset: 0x0002ECE3
			internal GetBinderAdapter(InvokeMemberBinder binder)
				: base(binder.Name, binder.IgnoreCase)
			{
			}

			// Token: 0x06000C76 RID: 3190 RVA: 0x00024289 File Offset: 0x00022489
			public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x02000112 RID: 274
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06000C77 RID: 3191 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000C78 RID: 3192 RVA: 0x00030AF7 File Offset: 0x0002ECF7
			internal DynamicMetaObject <BindGetMember>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackGetMember(this.<>4__this, e);
			}

			// Token: 0x04000444 RID: 1092
			public GetMemberBinder binder;

			// Token: 0x04000445 RID: 1093
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x02000113 RID: 275
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06000C79 RID: 3193 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06000C7A RID: 3194 RVA: 0x00030B0B File Offset: 0x0002ED0B
			internal DynamicMetaObject <BindSetMember>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackSetMember(this.<>4__this, this.value, e);
			}

			// Token: 0x04000446 RID: 1094
			public SetMemberBinder binder;

			// Token: 0x04000447 RID: 1095
			public DynamicMetaObject value;

			// Token: 0x04000448 RID: 1096
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x02000114 RID: 276
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000C7B RID: 3195 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000C7C RID: 3196 RVA: 0x00030B25 File Offset: 0x0002ED25
			internal DynamicMetaObject <BindDeleteMember>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackDeleteMember(this.<>4__this, e);
			}

			// Token: 0x04000449 RID: 1097
			public DeleteMemberBinder binder;

			// Token: 0x0400044A RID: 1098
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x02000115 RID: 277
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000C7D RID: 3197 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000C7E RID: 3198 RVA: 0x00030B39 File Offset: 0x0002ED39
			internal DynamicMetaObject <BindConvert>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackConvert(this.<>4__this, e);
			}

			// Token: 0x0400044B RID: 1099
			public ConvertBinder binder;

			// Token: 0x0400044C RID: 1100
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x02000116 RID: 278
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06000C7F RID: 3199 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000C80 RID: 3200 RVA: 0x00030B4D File Offset: 0x0002ED4D
			internal DynamicMetaObject <BindInvokeMember>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackInvokeMember(this.<>4__this, this.args, e);
			}

			// Token: 0x06000C81 RID: 3201 RVA: 0x00030B67 File Offset: 0x0002ED67
			internal DynamicMetaObject <BindInvokeMember>b__1(DynamicMetaObject e)
			{
				return this.binder.FallbackInvoke(e, this.args, null);
			}

			// Token: 0x0400044D RID: 1101
			public InvokeMemberBinder binder;

			// Token: 0x0400044E RID: 1102
			public DynamicMetaObject[] args;

			// Token: 0x0400044F RID: 1103
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x02000117 RID: 279
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06000C82 RID: 3202 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06000C83 RID: 3203 RVA: 0x00030B7C File Offset: 0x0002ED7C
			internal DynamicMetaObject <BindCreateInstance>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackCreateInstance(this.<>4__this, this.args, e);
			}

			// Token: 0x04000450 RID: 1104
			public CreateInstanceBinder binder;

			// Token: 0x04000451 RID: 1105
			public DynamicMetaObject[] args;

			// Token: 0x04000452 RID: 1106
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x02000118 RID: 280
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06000C84 RID: 3204 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06000C85 RID: 3205 RVA: 0x00030B96 File Offset: 0x0002ED96
			internal DynamicMetaObject <BindInvoke>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackInvoke(this.<>4__this, this.args, e);
			}

			// Token: 0x04000453 RID: 1107
			public InvokeBinder binder;

			// Token: 0x04000454 RID: 1108
			public DynamicMetaObject[] args;

			// Token: 0x04000455 RID: 1109
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x02000119 RID: 281
		[CompilerGenerated]
		private sealed class <>c__DisplayClass10_0
		{
			// Token: 0x06000C86 RID: 3206 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass10_0()
			{
			}

			// Token: 0x06000C87 RID: 3207 RVA: 0x00030BB0 File Offset: 0x0002EDB0
			internal DynamicMetaObject <BindBinaryOperation>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackBinaryOperation(this.<>4__this, this.arg, e);
			}

			// Token: 0x04000456 RID: 1110
			public BinaryOperationBinder binder;

			// Token: 0x04000457 RID: 1111
			public DynamicMetaObject arg;

			// Token: 0x04000458 RID: 1112
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x0200011A RID: 282
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0
		{
			// Token: 0x06000C88 RID: 3208 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x00030BCA File Offset: 0x0002EDCA
			internal DynamicMetaObject <BindUnaryOperation>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackUnaryOperation(this.<>4__this, e);
			}

			// Token: 0x04000459 RID: 1113
			public UnaryOperationBinder binder;

			// Token: 0x0400045A RID: 1114
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x0200011B RID: 283
		[CompilerGenerated]
		private sealed class <>c__DisplayClass12_0
		{
			// Token: 0x06000C8A RID: 3210 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass12_0()
			{
			}

			// Token: 0x06000C8B RID: 3211 RVA: 0x00030BDE File Offset: 0x0002EDDE
			internal DynamicMetaObject <BindGetIndex>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackGetIndex(this.<>4__this, this.indexes, e);
			}

			// Token: 0x0400045B RID: 1115
			public GetIndexBinder binder;

			// Token: 0x0400045C RID: 1116
			public DynamicMetaObject[] indexes;

			// Token: 0x0400045D RID: 1117
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x0200011C RID: 284
		[CompilerGenerated]
		private sealed class <>c__DisplayClass13_0
		{
			// Token: 0x06000C8C RID: 3212 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass13_0()
			{
			}

			// Token: 0x06000C8D RID: 3213 RVA: 0x00030BF8 File Offset: 0x0002EDF8
			internal DynamicMetaObject <BindSetIndex>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackSetIndex(this.<>4__this, this.indexes, this.value, e);
			}

			// Token: 0x0400045E RID: 1118
			public SetIndexBinder binder;

			// Token: 0x0400045F RID: 1119
			public DynamicMetaObject[] indexes;

			// Token: 0x04000460 RID: 1120
			public DynamicMetaObject value;

			// Token: 0x04000461 RID: 1121
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x0200011D RID: 285
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x06000C8E RID: 3214 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x06000C8F RID: 3215 RVA: 0x00030C18 File Offset: 0x0002EE18
			internal DynamicMetaObject <BindDeleteIndex>b__0(DynamicMetaObject e)
			{
				return this.binder.FallbackDeleteIndex(this.<>4__this, this.indexes, e);
			}

			// Token: 0x04000462 RID: 1122
			public DeleteIndexBinder binder;

			// Token: 0x04000463 RID: 1123
			public DynamicMetaObject[] indexes;

			// Token: 0x04000464 RID: 1124
			public DynamicProxyMetaObject<T> <>4__this;
		}

		// Token: 0x0200011E RID: 286
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000C90 RID: 3216 RVA: 0x00030C32 File Offset: 0x0002EE32
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000C91 RID: 3217 RVA: 0x00008020 File Offset: 0x00006220
			public <>c()
			{
			}

			// Token: 0x06000C92 RID: 3218 RVA: 0x00030C40 File Offset: 0x0002EE40
			internal Expression <GetArgs>b__18_0(DynamicMetaObject arg)
			{
				Expression expression = arg.Expression;
				if (!expression.Type.IsValueType())
				{
					return expression;
				}
				return Expression.Convert(expression, typeof(object));
			}

			// Token: 0x04000465 RID: 1125
			public static readonly DynamicProxyMetaObject<T>.<>c <>9 = new DynamicProxyMetaObject<T>.<>c();

			// Token: 0x04000466 RID: 1126
			public static Func<DynamicMetaObject, Expression> <>9__18_0;
		}
	}
}
