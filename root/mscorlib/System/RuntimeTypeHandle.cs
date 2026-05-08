using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	// Token: 0x02000222 RID: 546
	[ComVisible(true)]
	[Serializable]
	public struct RuntimeTypeHandle : ISerializable
	{
		// Token: 0x06001AE2 RID: 6882 RVA: 0x000652AF File Offset: 0x000634AF
		internal RuntimeTypeHandle(IntPtr val)
		{
			this.value = val;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000652B8 File Offset: 0x000634B8
		internal RuntimeTypeHandle(RuntimeType type)
		{
			this = new RuntimeTypeHandle(type._impl.value);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000652CC File Offset: 0x000634CC
		private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			RuntimeType runtimeType = (RuntimeType)info.GetValue("TypeObj", typeof(RuntimeType));
			this.value = runtimeType.TypeHandle.Value;
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Insufficient state.");
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x00065333 File Offset: 0x00063533
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0006533C File Offset: 0x0006353C
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (this.value == IntPtr.Zero)
			{
				throw new SerializationException("Object fields may not be properly initialized");
			}
			info.AddValue("TypeObj", Type.GetTypeHandle(this), typeof(RuntimeType));
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000653A0 File Offset: 0x000635A0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeTypeHandle)obj).Value;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000653E8 File Offset: 0x000635E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public bool Equals(RuntimeTypeHandle handle)
		{
			return this.value == handle.Value;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x000653FC File Offset: 0x000635FC
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x00065409 File Offset: 0x00063609
		public static bool operator ==(RuntimeTypeHandle left, object right)
		{
			return right != null && right is RuntimeTypeHandle && left.Equals((RuntimeTypeHandle)right);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x00065425 File Offset: 0x00063625
		public static bool operator !=(RuntimeTypeHandle left, object right)
		{
			return right == null || !(right is RuntimeTypeHandle) || !left.Equals((RuntimeTypeHandle)right);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x00065444 File Offset: 0x00063644
		public static bool operator ==(object left, RuntimeTypeHandle right)
		{
			return left != null && left is RuntimeTypeHandle && ((RuntimeTypeHandle)left).Equals(right);
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x00065470 File Offset: 0x00063670
		public static bool operator !=(object left, RuntimeTypeHandle right)
		{
			return left == null || !(left is RuntimeTypeHandle) || !((RuntimeTypeHandle)left).Equals(right);
		}

		// Token: 0x06001AEE RID: 6894
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern TypeAttributes GetAttributes(RuntimeType type);

		// Token: 0x06001AEF RID: 6895 RVA: 0x0006549C File Offset: 0x0006369C
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public ModuleHandle GetModuleHandle()
		{
			if (this.value == IntPtr.Zero)
			{
				throw new InvalidOperationException("Object fields may not be properly initialized");
			}
			return Type.GetTypeFromHandle(this).Module.ModuleHandle;
		}

		// Token: 0x06001AF0 RID: 6896
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMetadataToken(RuntimeType type);

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000654D0 File Offset: 0x000636D0
		internal static int GetToken(RuntimeType type)
		{
			return RuntimeTypeHandle.GetMetadataToken(type);
		}

		// Token: 0x06001AF2 RID: 6898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetGenericTypeDefinition_impl(RuntimeType type);

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000654D8 File Offset: 0x000636D8
		internal static Type GetGenericTypeDefinition(RuntimeType type)
		{
			return RuntimeTypeHandle.GetGenericTypeDefinition_impl(type);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000654E0 File Offset: 0x000636E0
		internal static bool HasProxyAttribute(RuntimeType type)
		{
			throw new NotImplementedException("HasProxyAttribute");
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000654EC File Offset: 0x000636EC
		internal static bool IsPrimitive(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return (corElementType >= CorElementType.Boolean && corElementType <= CorElementType.R8) || corElementType == CorElementType.I || corElementType == CorElementType.U;
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x00065515 File Offset: 0x00063715
		internal static bool IsByRef(RuntimeType type)
		{
			return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.ByRef;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x00065521 File Offset: 0x00063721
		internal static bool IsPointer(RuntimeType type)
		{
			return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.Ptr;
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x00065530 File Offset: 0x00063730
		internal static bool IsArray(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray;
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00065550 File Offset: 0x00063750
		internal static bool IsSzArray(RuntimeType type)
		{
			return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.SzArray;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0006555C File Offset: 0x0006375C
		internal static bool HasElementType(RuntimeType type)
		{
			CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
			return corElementType == CorElementType.Array || corElementType == CorElementType.SzArray || corElementType == CorElementType.Ptr || corElementType == CorElementType.ByRef;
		}

		// Token: 0x06001AFB RID: 6907
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern CorElementType GetCorElementType(RuntimeType type);

		// Token: 0x06001AFC RID: 6908
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasInstantiation(RuntimeType type);

		// Token: 0x06001AFD RID: 6909
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsComObject(RuntimeType type);

		// Token: 0x06001AFE RID: 6910
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInstanceOfType(RuntimeType type, object o);

		// Token: 0x06001AFF RID: 6911
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool HasReferences(RuntimeType type);

		// Token: 0x06001B00 RID: 6912 RVA: 0x00065586 File Offset: 0x00063786
		internal static bool IsComObject(RuntimeType type, bool isGenericCOM)
		{
			return !isGenericCOM && RuntimeTypeHandle.IsComObject(type);
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x00047F63 File Offset: 0x00046163
		internal static bool IsContextful(RuntimeType type)
		{
			return typeof(ContextBoundObject).IsAssignableFrom(type);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0000408A File Offset: 0x0000228A
		internal static bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2)
		{
			return false;
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x00065593 File Offset: 0x00063793
		internal static bool IsInterface(RuntimeType type)
		{
			return (type.Attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
		}

		// Token: 0x06001B04 RID: 6916
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetArrayRank(RuntimeType type);

		// Token: 0x06001B05 RID: 6917
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

		// Token: 0x06001B06 RID: 6918
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetElementType(RuntimeType type);

		// Token: 0x06001B07 RID: 6919
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetModule(RuntimeType type);

		// Token: 0x06001B08 RID: 6920
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericVariable(RuntimeType type);

		// Token: 0x06001B09 RID: 6921
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType GetBaseType(RuntimeType type);

		// Token: 0x06001B0A RID: 6922 RVA: 0x000655A2 File Offset: 0x000637A2
		internal static bool CanCastTo(RuntimeType type, RuntimeType target)
		{
			return RuntimeTypeHandle.type_is_assignable_from(target, type);
		}

		// Token: 0x06001B0B RID: 6923
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool type_is_assignable_from(Type a, Type b);

		// Token: 0x06001B0C RID: 6924
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsGenericTypeDefinition(RuntimeType type);

		// Token: 0x06001B0D RID: 6925
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetGenericParameterInfo(RuntimeType type);

		// Token: 0x06001B0E RID: 6926 RVA: 0x000655AB File Offset: 0x000637AB
		internal static bool IsSubclassOf(RuntimeType childType, RuntimeType baseType)
		{
			return RuntimeTypeHandle.is_subclass_of(childType._impl.Value, baseType._impl.Value);
		}

		// Token: 0x06001B0F RID: 6927
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool is_subclass_of(IntPtr childType, IntPtr baseType);

		// Token: 0x06001B10 RID: 6928
		[PreserveDependency(".ctor()", "System.Runtime.CompilerServices.IsByRefLikeAttribute")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsByRefLike(RuntimeType type);

		// Token: 0x06001B11 RID: 6929 RVA: 0x000655C8 File Offset: 0x000637C8
		internal static bool IsTypeDefinition(RuntimeType type)
		{
			return !type.HasElementType && !type.IsConstructedGenericType && !type.IsGenericParameter;
		}

		// Token: 0x06001B12 RID: 6930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeType internal_from_name(string name, ref StackCrawlMark stackMark, Assembly callerAssembly, bool throwOnError, bool ignoreCase, bool reflectionOnly);

		// Token: 0x06001B13 RID: 6931 RVA: 0x000655E8 File Offset: 0x000637E8
		internal static RuntimeType GetTypeByName(string typeName, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, bool loadTypeFromPartialName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (typeName == string.Empty)
			{
				if (throwOnError)
				{
					throw new TypeLoadException("A null or zero length string does not represent a valid Type.");
				}
				return null;
			}
			else if (reflectionOnly)
			{
				int num = typeName.IndexOf(',');
				if (num < 0 || num == 0 || num == typeName.Length - 1)
				{
					throw new ArgumentException("Assembly qualifed type name is required", "typeName");
				}
				string text = typeName.Substring(num + 1);
				Assembly assembly;
				try
				{
					assembly = Assembly.ReflectionOnlyLoad(text);
				}
				catch
				{
					if (throwOnError)
					{
						throw;
					}
					return null;
				}
				return (RuntimeType)assembly.GetType(typeName.Substring(0, num), throwOnError, ignoreCase);
			}
			else
			{
				RuntimeType runtimeType = RuntimeTypeHandle.internal_from_name(typeName, ref stackMark, null, throwOnError, ignoreCase, false);
				if (throwOnError && runtimeType == null)
				{
					throw new TypeLoadException("Error loading '" + typeName + "'");
				}
				return runtimeType;
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000656C8 File Offset: 0x000638C8
		internal static IntPtr[] CopyRuntimeTypeHandles(RuntimeTypeHandle[] inHandles, out int length)
		{
			if (inHandles == null || inHandles.Length == 0)
			{
				length = 0;
				return null;
			}
			IntPtr[] array = new IntPtr[inHandles.Length];
			for (int i = 0; i < inHandles.Length; i++)
			{
				array[i] = inHandles[i].Value;
			}
			length = array.Length;
			return array;
		}

		// Token: 0x0400165F RID: 5727
		private IntPtr value;
	}
}
