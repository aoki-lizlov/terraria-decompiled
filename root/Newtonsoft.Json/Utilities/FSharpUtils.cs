using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000050 RID: 80
	internal static class FSharpUtils
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x000116BB File Offset: 0x0000F8BB
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x000116C2 File Offset: 0x0000F8C2
		public static Assembly FSharpCoreAssembly
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<FSharpCoreAssembly>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<FSharpCoreAssembly>k__BackingField = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000116CA File Offset: 0x0000F8CA
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x000116D1 File Offset: 0x0000F8D1
		public static MethodCall<object, object> IsUnion
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<IsUnion>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<IsUnion>k__BackingField = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x000116D9 File Offset: 0x0000F8D9
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x000116E0 File Offset: 0x0000F8E0
		public static MethodCall<object, object> GetUnionCases
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<GetUnionCases>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<GetUnionCases>k__BackingField = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000116E8 File Offset: 0x0000F8E8
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x000116EF File Offset: 0x0000F8EF
		public static MethodCall<object, object> PreComputeUnionTagReader
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<PreComputeUnionTagReader>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<PreComputeUnionTagReader>k__BackingField = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000116F7 File Offset: 0x0000F8F7
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x000116FE File Offset: 0x0000F8FE
		public static MethodCall<object, object> PreComputeUnionReader
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<PreComputeUnionReader>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<PreComputeUnionReader>k__BackingField = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00011706 File Offset: 0x0000F906
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0001170D File Offset: 0x0000F90D
		public static MethodCall<object, object> PreComputeUnionConstructor
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<PreComputeUnionConstructor>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<PreComputeUnionConstructor>k__BackingField = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00011715 File Offset: 0x0000F915
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0001171C File Offset: 0x0000F91C
		public static Func<object, object> GetUnionCaseInfoDeclaringType
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<GetUnionCaseInfoDeclaringType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<GetUnionCaseInfoDeclaringType>k__BackingField = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00011724 File Offset: 0x0000F924
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0001172B File Offset: 0x0000F92B
		public static Func<object, object> GetUnionCaseInfoName
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<GetUnionCaseInfoName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<GetUnionCaseInfoName>k__BackingField = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00011733 File Offset: 0x0000F933
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0001173A File Offset: 0x0000F93A
		public static Func<object, object> GetUnionCaseInfoTag
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<GetUnionCaseInfoTag>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<GetUnionCaseInfoTag>k__BackingField = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00011742 File Offset: 0x0000F942
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x00011749 File Offset: 0x0000F949
		public static MethodCall<object, object> GetUnionCaseInfoFields
		{
			[CompilerGenerated]
			get
			{
				return FSharpUtils.<GetUnionCaseInfoFields>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				FSharpUtils.<GetUnionCaseInfoFields>k__BackingField = value;
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00011754 File Offset: 0x0000F954
		public static void EnsureInitialized(Assembly fsharpCoreAssembly)
		{
			if (!FSharpUtils._initialized)
			{
				object @lock = FSharpUtils.Lock;
				lock (@lock)
				{
					if (!FSharpUtils._initialized)
					{
						FSharpUtils.FSharpCoreAssembly = fsharpCoreAssembly;
						Type type = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpType");
						MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, "IsUnion", 24);
						FSharpUtils.IsUnion = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
						MethodInfo methodWithNonPublicFallback2 = FSharpUtils.GetMethodWithNonPublicFallback(type, "GetUnionCases", 24);
						FSharpUtils.GetUnionCases = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback2);
						Type type2 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpValue");
						FSharpUtils.PreComputeUnionTagReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionTagReader");
						FSharpUtils.PreComputeUnionReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionReader");
						FSharpUtils.PreComputeUnionConstructor = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionConstructor");
						Type type3 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.UnionCaseInfo");
						FSharpUtils.GetUnionCaseInfoName = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Name"));
						FSharpUtils.GetUnionCaseInfoTag = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Tag"));
						FSharpUtils.GetUnionCaseInfoDeclaringType = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("DeclaringType"));
						FSharpUtils.GetUnionCaseInfoFields = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(type3.GetMethod("GetFields"));
						FSharpUtils._ofSeq = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.ListModule").GetMethod("OfSeq");
						FSharpUtils._mapType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpMap`2");
						Thread.MemoryBarrier();
						FSharpUtils._initialized = true;
					}
				}
			}
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000118E4 File Offset: 0x0000FAE4
		private static MethodInfo GetMethodWithNonPublicFallback(Type type, string methodName, BindingFlags bindingFlags)
		{
			MethodInfo methodInfo = type.GetMethod(methodName, bindingFlags);
			if (methodInfo == null && (bindingFlags & 32) != 32)
			{
				methodInfo = type.GetMethod(methodName, bindingFlags | 32);
			}
			return methodInfo;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00011918 File Offset: 0x0000FB18
		private static MethodCall<object, object> CreateFSharpFuncCall(Type type, string methodName)
		{
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, methodName, 24);
			MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod("Invoke", 20);
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodCall<object, object> invoke = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object target, object[] args) => new FSharpFunction(call(target, args), invoke);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00011974 File Offset: 0x0000FB74
		public static ObjectConstructor<object> CreateSeq(Type t)
		{
			MethodInfo methodInfo = FSharpUtils._ofSeq.MakeGenericMethod(new Type[] { t });
			return JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(methodInfo);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000119A1 File Offset: 0x0000FBA1
		public static ObjectConstructor<object> CreateMap(Type keyType, Type valueType)
		{
			return (ObjectConstructor<object>)typeof(FSharpUtils).GetMethod("BuildMapCreator").MakeGenericMethod(new Type[] { keyType, valueType }).Invoke(null, null);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public static ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			ConstructorInfo constructor = FSharpUtils._mapType.MakeGenericType(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}).GetConstructor(new Type[] { typeof(IEnumerable<Tuple<TKey, TValue>>) });
			ObjectConstructor<object> ctorDelegate = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate(object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = Enumerable.Select<KeyValuePair<TKey, TValue>, Tuple<TKey, TValue>>((IEnumerable<KeyValuePair<TKey, TValue>>)args[0], (KeyValuePair<TKey, TValue> kv) => new Tuple<TKey, TValue>(kv.Key, kv.Value));
				return ctorDelegate(new object[] { enumerable });
			};
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00011A4B File Offset: 0x0000FC4B
		// Note: this type is marked as 'beforefieldinit'.
		static FSharpUtils()
		{
		}

		// Token: 0x040001ED RID: 493
		private static readonly object Lock = new object();

		// Token: 0x040001EE RID: 494
		private static bool _initialized;

		// Token: 0x040001EF RID: 495
		private static MethodInfo _ofSeq;

		// Token: 0x040001F0 RID: 496
		private static Type _mapType;

		// Token: 0x040001F1 RID: 497
		[CompilerGenerated]
		private static Assembly <FSharpCoreAssembly>k__BackingField;

		// Token: 0x040001F2 RID: 498
		[CompilerGenerated]
		private static MethodCall<object, object> <IsUnion>k__BackingField;

		// Token: 0x040001F3 RID: 499
		[CompilerGenerated]
		private static MethodCall<object, object> <GetUnionCases>k__BackingField;

		// Token: 0x040001F4 RID: 500
		[CompilerGenerated]
		private static MethodCall<object, object> <PreComputeUnionTagReader>k__BackingField;

		// Token: 0x040001F5 RID: 501
		[CompilerGenerated]
		private static MethodCall<object, object> <PreComputeUnionReader>k__BackingField;

		// Token: 0x040001F6 RID: 502
		[CompilerGenerated]
		private static MethodCall<object, object> <PreComputeUnionConstructor>k__BackingField;

		// Token: 0x040001F7 RID: 503
		[CompilerGenerated]
		private static Func<object, object> <GetUnionCaseInfoDeclaringType>k__BackingField;

		// Token: 0x040001F8 RID: 504
		[CompilerGenerated]
		private static Func<object, object> <GetUnionCaseInfoName>k__BackingField;

		// Token: 0x040001F9 RID: 505
		[CompilerGenerated]
		private static Func<object, object> <GetUnionCaseInfoTag>k__BackingField;

		// Token: 0x040001FA RID: 506
		[CompilerGenerated]
		private static MethodCall<object, object> <GetUnionCaseInfoFields>k__BackingField;

		// Token: 0x040001FB RID: 507
		public const string FSharpSetTypeName = "FSharpSet`1";

		// Token: 0x040001FC RID: 508
		public const string FSharpListTypeName = "FSharpList`1";

		// Token: 0x040001FD RID: 509
		public const string FSharpMapTypeName = "FSharpMap`2";

		// Token: 0x02000123 RID: 291
		[CompilerGenerated]
		private sealed class <>c__DisplayClass49_0
		{
			// Token: 0x06000C9E RID: 3230 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass49_0()
			{
			}

			// Token: 0x06000C9F RID: 3231 RVA: 0x00030EC1 File Offset: 0x0002F0C1
			internal object <CreateFSharpFuncCall>b__0(object target, object[] args)
			{
				return new FSharpFunction(this.call(target, args), this.invoke);
			}

			// Token: 0x04000476 RID: 1142
			public MethodCall<object, object> call;

			// Token: 0x04000477 RID: 1143
			public MethodCall<object, object> invoke;
		}

		// Token: 0x02000124 RID: 292
		[CompilerGenerated]
		private sealed class <>c__DisplayClass52_0<TKey, TValue>
		{
			// Token: 0x06000CA0 RID: 3232 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__DisplayClass52_0()
			{
			}

			// Token: 0x06000CA1 RID: 3233 RVA: 0x00030EDC File Offset: 0x0002F0DC
			internal object <BuildMapCreator>b__0(object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = Enumerable.Select<KeyValuePair<TKey, TValue>, Tuple<TKey, TValue>>((IEnumerable<KeyValuePair<TKey, TValue>>)args[0], (KeyValuePair<TKey, TValue> kv) => new Tuple<TKey, TValue>(kv.Key, kv.Value));
				return this.ctorDelegate(new object[] { enumerable });
			}

			// Token: 0x04000478 RID: 1144
			public ObjectConstructor<object> ctorDelegate;
		}

		// Token: 0x02000125 RID: 293
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__52<TKey, TValue>
		{
			// Token: 0x06000CA2 RID: 3234 RVA: 0x00030F2B File Offset: 0x0002F12B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__52()
			{
			}

			// Token: 0x06000CA3 RID: 3235 RVA: 0x00008020 File Offset: 0x00006220
			public <>c__52()
			{
			}

			// Token: 0x06000CA4 RID: 3236 RVA: 0x00030F37 File Offset: 0x0002F137
			internal Tuple<TKey, TValue> <BuildMapCreator>b__52_1(KeyValuePair<TKey, TValue> kv)
			{
				return new Tuple<TKey, TValue>(kv.Key, kv.Value);
			}

			// Token: 0x04000479 RID: 1145
			public static readonly FSharpUtils.<>c__52<TKey, TValue> <>9 = new FSharpUtils.<>c__52<TKey, TValue>();

			// Token: 0x0400047A RID: 1146
			public static Func<KeyValuePair<TKey, TValue>, Tuple<TKey, TValue>> <>9__52_1;
		}
	}
}
