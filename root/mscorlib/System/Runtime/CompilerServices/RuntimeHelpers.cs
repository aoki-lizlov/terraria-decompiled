using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000822 RID: 2082
	public static class RuntimeHelpers
	{
		// Token: 0x06004684 RID: 18052
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeArray(Array array, IntPtr fldHandle);

		// Token: 0x06004685 RID: 18053 RVA: 0x000E77DF File Offset: 0x000E59DF
		public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle)
		{
			if (array == null || fldHandle.Value == IntPtr.Zero)
			{
				throw new ArgumentNullException();
			}
			RuntimeHelpers.InitializeArray(array, fldHandle.Value);
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06004686 RID: 18054
		public static extern int OffsetToStringData
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x00064F43 File Offset: 0x00063143
		public static int GetHashCode(object o)
		{
			return object.InternalGetHashCode(o);
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x000E780A File Offset: 0x000E5A0A
		public new static bool Equals(object o1, object o2)
		{
			if (o1 == o2)
			{
				return true;
			}
			if (o1 == null || o2 == null)
			{
				return false;
			}
			if (o1 is ValueType)
			{
				return ValueType.DefaultEquals(o1, o2);
			}
			return object.Equals(o1, o2);
		}

		// Token: 0x06004689 RID: 18057
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object GetObjectValue(object obj);

		// Token: 0x0600468A RID: 18058
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RunClassConstructor(IntPtr type);

		// Token: 0x0600468B RID: 18059 RVA: 0x000E7831 File Offset: 0x000E5A31
		public static void RunClassConstructor(RuntimeTypeHandle type)
		{
			if (type.Value == IntPtr.Zero)
			{
				throw new ArgumentException("Handle is not initialized.", "type");
			}
			RuntimeHelpers.RunClassConstructor(type.Value);
		}

		// Token: 0x0600468C RID: 18060
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SufficientExecutionStack();

		// Token: 0x0600468D RID: 18061 RVA: 0x000E7862 File Offset: 0x000E5A62
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void EnsureSufficientExecutionStack()
		{
			if (RuntimeHelpers.SufficientExecutionStack())
			{
				return;
			}
			throw new InsufficientExecutionStackException();
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x000E7871 File Offset: 0x000E5A71
		public static bool TryEnsureSufficientExecutionStack()
		{
			return RuntimeHelpers.SufficientExecutionStack();
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x00004088 File Offset: 0x00002288
		public static void ExecuteCodeWithGuaranteedCleanup(RuntimeHelpers.TryCode code, RuntimeHelpers.CleanupCode backoutCode, object userData)
		{
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x00004088 File Offset: 0x00002288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegions()
		{
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x00004088 File Offset: 0x00002288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void PrepareConstrainedRegionsNoOP()
		{
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x00004088 File Offset: 0x00002288
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void ProbeForSufficientStack()
		{
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x00004088 File Offset: 0x00002288
		[SecurityCritical]
		public static void PrepareDelegate(Delegate d)
		{
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x00004088 File Offset: 0x00002288
		[SecurityCritical]
		public static void PrepareContractedDelegate(Delegate d)
		{
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x00004088 File Offset: 0x00002288
		public static void PrepareMethod(RuntimeMethodHandle method)
		{
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x00004088 File Offset: 0x00002288
		public static void PrepareMethod(RuntimeMethodHandle method, RuntimeTypeHandle[] instantiation)
		{
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000E7878 File Offset: 0x000E5A78
		public static void RunModuleConstructor(ModuleHandle module)
		{
			if (module == ModuleHandle.EmptyHandle)
			{
				throw new ArgumentException("Handle is not initialized.", "module");
			}
			RuntimeHelpers.RunModuleConstructor(module.Value);
		}

		// Token: 0x06004698 RID: 18072
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RunModuleConstructor(IntPtr module);

		// Token: 0x06004699 RID: 18073 RVA: 0x000E78A3 File Offset: 0x000E5AA3
		public static bool IsReferenceOrContainsReferences<T>()
		{
			return !typeof(T).IsValueType || RuntimeTypeHandle.HasReferences(typeof(T) as RuntimeType);
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000E78CC File Offset: 0x000E5ACC
		public static object GetUninitializedObject(Type type)
		{
			return FormatterServices.GetUninitializedObject(type);
		}

		// Token: 0x0600469B RID: 18075 RVA: 0x000E78D4 File Offset: 0x000E5AD4
		public static T[] GetSubArray<T>(T[] array, Range range)
		{
			Type elementType = array.GetType().GetElementType();
			Span<T> span = array.AsSpan(range);
			if (elementType.IsValueType)
			{
				return span.ToArray();
			}
			T[] array2 = (T[])Array.CreateInstance(elementType, span.Length);
			span.CopyTo(array2);
			return array2;
		}

		// Token: 0x02000823 RID: 2083
		// (Invoke) Token: 0x0600469D RID: 18077
		public delegate void TryCode(object userData);

		// Token: 0x02000824 RID: 2084
		// (Invoke) Token: 0x060046A1 RID: 18081
		public delegate void CleanupCode(object userData, bool exceptionThrown);
	}
}
