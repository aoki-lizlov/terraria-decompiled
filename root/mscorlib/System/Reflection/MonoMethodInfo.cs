using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020008C7 RID: 2247
	internal struct MonoMethodInfo
	{
		// Token: 0x06004CAC RID: 19628
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_method_info(IntPtr handle, out MonoMethodInfo info);

		// Token: 0x06004CAD RID: 19629
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_method_attributes(IntPtr handle);

		// Token: 0x06004CAE RID: 19630 RVA: 0x000F3A0C File Offset: 0x000F1C0C
		internal static MonoMethodInfo GetMethodInfo(IntPtr handle)
		{
			MonoMethodInfo monoMethodInfo;
			MonoMethodInfo.get_method_info(handle, out monoMethodInfo);
			return monoMethodInfo;
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x000F3A22 File Offset: 0x000F1C22
		internal static Type GetDeclaringType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).parent;
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x000F3A2F File Offset: 0x000F1C2F
		internal static Type GetReturnType(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).ret;
		}

		// Token: 0x06004CB1 RID: 19633 RVA: 0x000F3A3C File Offset: 0x000F1C3C
		internal static MethodAttributes GetAttributes(IntPtr handle)
		{
			return (MethodAttributes)MonoMethodInfo.get_method_attributes(handle);
		}

		// Token: 0x06004CB2 RID: 19634 RVA: 0x000F3A44 File Offset: 0x000F1C44
		internal static CallingConventions GetCallingConvention(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).callconv;
		}

		// Token: 0x06004CB3 RID: 19635 RVA: 0x000F3A51 File Offset: 0x000F1C51
		internal static MethodImplAttributes GetMethodImplementationFlags(IntPtr handle)
		{
			return MonoMethodInfo.GetMethodInfo(handle).iattrs;
		}

		// Token: 0x06004CB4 RID: 19636
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ParameterInfo[] get_parameter_info(IntPtr handle, MemberInfo member);

		// Token: 0x06004CB5 RID: 19637 RVA: 0x000F3A5E File Offset: 0x000F1C5E
		internal static ParameterInfo[] GetParametersInfo(IntPtr handle, MemberInfo member)
		{
			return MonoMethodInfo.get_parameter_info(handle, member);
		}

		// Token: 0x06004CB6 RID: 19638
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MarshalAsAttribute get_retval_marshal(IntPtr handle);

		// Token: 0x06004CB7 RID: 19639 RVA: 0x000F3A67 File Offset: 0x000F1C67
		internal static ParameterInfo GetReturnParameterInfo(RuntimeMethodInfo method)
		{
			return RuntimeParameterInfo.New(MonoMethodInfo.GetReturnType(method.mhandle), method, MonoMethodInfo.get_retval_marshal(method.mhandle));
		}

		// Token: 0x04002FD4 RID: 12244
		private Type parent;

		// Token: 0x04002FD5 RID: 12245
		private Type ret;

		// Token: 0x04002FD6 RID: 12246
		internal MethodAttributes attrs;

		// Token: 0x04002FD7 RID: 12247
		internal MethodImplAttributes iattrs;

		// Token: 0x04002FD8 RID: 12248
		private CallingConventions callconv;
	}
}
