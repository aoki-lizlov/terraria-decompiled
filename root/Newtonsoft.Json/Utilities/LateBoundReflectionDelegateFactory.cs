using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000055 RID: 85
	internal class LateBoundReflectionDelegateFactory : ReflectionDelegateFactory
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00011EBA File Offset: 0x000100BA
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return LateBoundReflectionDelegateFactory._instance;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00011EC4 File Offset: 0x000100C4
		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return (object[] a) => c.Invoke(a);
			}
			return (object[] a) => method.Invoke(null, a);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00011F28 File Offset: 0x00010128
		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return (T o, object[] a) => c.Invoke(a);
			}
			return (T o, object[] a) => method.Invoke(o, a);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00011F8C File Offset: 0x0001018C
		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsValueType())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			ConstructorInfo constructorInfo = ReflectionUtils.GetDefaultConstructor(type, true);
			return () => (T)((object)constructorInfo.Invoke(null));
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00011FEE File Offset: 0x000101EE
		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return (T o) => propertyInfo.GetValue(o, null);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00012017 File Offset: 0x00010217
		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return (T o) => fieldInfo.GetValue(o);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00012040 File Offset: 0x00010240
		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return delegate(T o, object v)
			{
				fieldInfo.SetValue(o, v);
			};
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00012069 File Offset: 0x00010269
		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return delegate(T o, object v)
			{
				propertyInfo.SetValue(o, v, null);
			};
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00010F55 File Offset: 0x0000F155
		public LateBoundReflectionDelegateFactory()
		{
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00012092 File Offset: 0x00010292
		// Note: this type is marked as 'beforefieldinit'.
		static LateBoundReflectionDelegateFactory()
		{
		}

		// Token: 0x04000202 RID: 514
		private static readonly LateBoundReflectionDelegateFactory _instance = new LateBoundReflectionDelegateFactory();

		// Token: 0x02000127 RID: 295
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x06000CA6 RID: 3238 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x06000CA7 RID: 3239 RVA: 0x00030F6C File Offset: 0x0002F16C
			internal object <CreateParameterizedConstructor>b__0(object[] a)
			{
				return this.c.Invoke(a);
			}

			// Token: 0x06000CA8 RID: 3240 RVA: 0x00030F87 File Offset: 0x0002F187
			internal object <CreateParameterizedConstructor>b__1(object[] a)
			{
				return this.method.Invoke(null, a);
			}

			// Token: 0x0400047E RID: 1150
			public ConstructorInfo c;

			// Token: 0x0400047F RID: 1151
			public MethodBase method;
		}

		// Token: 0x02000128 RID: 296
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0<T>
		{
			// Token: 0x06000CA9 RID: 3241 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06000CAA RID: 3242 RVA: 0x00030F96 File Offset: 0x0002F196
			internal object <CreateMethodCall>b__0(T o, object[] a)
			{
				return this.c.Invoke(a);
			}

			// Token: 0x06000CAB RID: 3243 RVA: 0x00030FA4 File Offset: 0x0002F1A4
			internal object <CreateMethodCall>b__1(T o, object[] a)
			{
				return this.method.Invoke(o, a);
			}

			// Token: 0x04000480 RID: 1152
			public ConstructorInfo c;

			// Token: 0x04000481 RID: 1153
			public MethodBase method;
		}

		// Token: 0x02000129 RID: 297
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0<T>
		{
			// Token: 0x06000CAC RID: 3244 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000CAD RID: 3245 RVA: 0x00030FB8 File Offset: 0x0002F1B8
			internal T <CreateDefaultConstructor>b__0()
			{
				return (T)((object)Activator.CreateInstance(this.type));
			}

			// Token: 0x06000CAE RID: 3246 RVA: 0x00030FCA File Offset: 0x0002F1CA
			internal T <CreateDefaultConstructor>b__1()
			{
				return (T)((object)this.constructorInfo.Invoke(null));
			}

			// Token: 0x04000482 RID: 1154
			public Type type;

			// Token: 0x04000483 RID: 1155
			public ConstructorInfo constructorInfo;
		}

		// Token: 0x0200012A RID: 298
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0<T>
		{
			// Token: 0x06000CAF RID: 3247 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000CB0 RID: 3248 RVA: 0x00030FDD File Offset: 0x0002F1DD
			internal object <CreateGet>b__0(T o)
			{
				return this.propertyInfo.GetValue(o, null);
			}

			// Token: 0x04000484 RID: 1156
			public PropertyInfo propertyInfo;
		}

		// Token: 0x0200012B RID: 299
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0<T>
		{
			// Token: 0x06000CB1 RID: 3249 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000CB2 RID: 3250 RVA: 0x00030FF1 File Offset: 0x0002F1F1
			internal object <CreateGet>b__0(T o)
			{
				return this.fieldInfo.GetValue(o);
			}

			// Token: 0x04000485 RID: 1157
			public FieldInfo fieldInfo;
		}

		// Token: 0x0200012C RID: 300
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0<T>
		{
			// Token: 0x06000CB3 RID: 3251 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x00031004 File Offset: 0x0002F204
			internal void <CreateSet>b__0(T o, object v)
			{
				this.fieldInfo.SetValue(o, v);
			}

			// Token: 0x04000486 RID: 1158
			public FieldInfo fieldInfo;
		}

		// Token: 0x0200012D RID: 301
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0<T>
		{
			// Token: 0x06000CB5 RID: 3253 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06000CB6 RID: 3254 RVA: 0x00031018 File Offset: 0x0002F218
			internal void <CreateSet>b__0(T o, object v)
			{
				this.propertyInfo.SetValue(o, v, null);
			}

			// Token: 0x04000487 RID: 1159
			public PropertyInfo propertyInfo;
		}
	}
}
