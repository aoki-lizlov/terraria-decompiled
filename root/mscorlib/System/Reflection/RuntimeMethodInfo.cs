using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020008C8 RID: 2248
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeMethodInfo : MethodInfo, ISerializable
	{
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004CB8 RID: 19640 RVA: 0x0000408A File Offset: 0x0000228A
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004CB9 RID: 19641 RVA: 0x000F3A85 File Offset: 0x000F1C85
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004CBA RID: 19642 RVA: 0x000F3417 File Offset: 0x000F1617
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x000F3A90 File Offset: 0x000F1C90
		internal override string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			TypeNameFormatFlags typeNameFormatFlags = (serialization ? TypeNameFormatFlags.FormatSerialization : TypeNameFormatFlags.FormatBasic);
			if (this.IsGenericMethod)
			{
				stringBuilder.Append(RuntimeMethodHandle.ConstructInstantiation(this, typeNameFormatFlags));
			}
			stringBuilder.Append("(");
			RuntimeParameterInfo.FormatParameters(stringBuilder, this.GetParametersNoCopy(), this.CallingConvention, serialization);
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x000F3AFC File Offset: 0x000F1CFC
		public override Delegate CreateDelegate(Type delegateType)
		{
			return Delegate.CreateDelegate(delegateType, this);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x000F3B05 File Offset: 0x000F1D05
		public override Delegate CreateDelegate(Type delegateType, object target)
		{
			return Delegate.CreateDelegate(delegateType, target, this);
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x000F3B0F File Offset: 0x000F1D0F
		public override string ToString()
		{
			return this.ReturnType.FormatTypeName() + " " + this.FormatNameAndSig(false);
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x000F3B2D File Offset: 0x000F1D2D
		internal RuntimeModule GetRuntimeModule()
		{
			return ((RuntimeType)this.DeclaringType).GetRuntimeModule();
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x000F3B40 File Offset: 0x000F1D40
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Method, (this.IsGenericMethod & !this.IsGenericMethodDefinition) ? this.GetGenericArguments() : null);
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x000F3B95 File Offset: 0x000F1D95
		internal string SerializationToString()
		{
			return this.ReturnType.FormatTypeName(true) + " " + this.FormatNameAndSig(true);
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x000F3BB4 File Offset: 0x000F1DB4
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(handle.Value, IntPtr.Zero, false);
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x000F3BC8 File Offset: 0x000F1DC8
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle, RuntimeTypeHandle reflectedType)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(handle.Value, reflectedType.Value, false);
		}

		// Token: 0x06004CC4 RID: 19652
		[PreserveDependency(".ctor(System.Reflection.ExceptionHandlingClause[],System.Reflection.LocalVariableInfo[],System.Byte[],System.Boolean,System.Int32,System.Int32)", "System.Reflection.MethodBody")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBody GetMethodBodyInternal(IntPtr handle);

		// Token: 0x06004CC5 RID: 19653 RVA: 0x000F3BDE File Offset: 0x000F1DDE
		internal static MethodBody GetMethodBody(IntPtr handle)
		{
			return RuntimeMethodInfo.GetMethodBodyInternal(handle);
		}

		// Token: 0x06004CC6 RID: 19654 RVA: 0x000F3BE6 File Offset: 0x000F1DE6
		internal static MethodBase GetMethodFromHandleInternalType(IntPtr method_handle, IntPtr type_handle)
		{
			return RuntimeMethodInfo.GetMethodFromHandleInternalType_native(method_handle, type_handle, true);
		}

		// Token: 0x06004CC7 RID: 19655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MethodBase GetMethodFromHandleInternalType_native(IntPtr method_handle, IntPtr type_handle, bool genericCheck);

		// Token: 0x06004CC8 RID: 19656 RVA: 0x000F3BF0 File Offset: 0x000F1DF0
		internal RuntimeMethodInfo()
		{
		}

		// Token: 0x06004CC9 RID: 19657 RVA: 0x000F3BF8 File Offset: 0x000F1DF8
		internal RuntimeMethodInfo(RuntimeMethodHandle mhandle)
		{
			this.mhandle = mhandle.Value;
		}

		// Token: 0x06004CCA RID: 19658
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string get_name(MethodBase method);

		// Token: 0x06004CCB RID: 19659
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeMethodInfo get_base_method(RuntimeMethodInfo method, bool definition);

		// Token: 0x06004CCC RID: 19660
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeMethodInfo method);

		// Token: 0x06004CCD RID: 19661 RVA: 0x000F3C0D File Offset: 0x000F1E0D
		public override MethodInfo GetBaseDefinition()
		{
			return RuntimeMethodInfo.get_base_method(this, true);
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x000F3C16 File Offset: 0x000F1E16
		internal MethodInfo GetBaseMethod()
		{
			return RuntimeMethodInfo.get_base_method(this, false);
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x000F3C1F File Offset: 0x000F1E1F
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004CD0 RID: 19664 RVA: 0x000F3C27 File Offset: 0x000F1E27
		public override Type ReturnType
		{
			get
			{
				return MonoMethodInfo.GetReturnType(this.mhandle);
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x000F3C1F File Offset: 0x000F1E1F
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return MonoMethodInfo.GetReturnParameterInfo(this);
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004CD2 RID: 19666 RVA: 0x000F3C34 File Offset: 0x000F1E34
		public override int MetadataToken
		{
			get
			{
				return RuntimeMethodInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x000F3C3C File Offset: 0x000F1E3C
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x000F3C4C File Offset: 0x000F1E4C
		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if (parametersInfo.Length == 0)
			{
				return parametersInfo;
			}
			ParameterInfo[] array = new ParameterInfo[parametersInfo.Length];
			Array.FastCopy(parametersInfo, 0, array, 0, parametersInfo.Length);
			return array;
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x000F3C83 File Offset: 0x000F1E83
		internal override ParameterInfo[] GetParametersInternal()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x000F3C91 File Offset: 0x000F1E91
		internal override int GetParametersCount()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this).Length;
		}

		// Token: 0x06004CD7 RID: 19671
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x06004CD8 RID: 19672 RVA: 0x000F3CA4 File Offset: 0x000F1EA4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (!base.IsStatic && !this.DeclaringType.IsInstanceOfType(obj))
			{
				if (obj == null)
				{
					throw new TargetException("Non-static method requires a target.");
				}
				throw new TargetException("Object does not match target type.");
			}
			else
			{
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				ParameterInfo[] parametersInternal = this.GetParametersInternal();
				RuntimeMethodInfo.ConvertValues(binder, parameters, parametersInternal, culture, invokeAttr);
				if (this.ContainsGenericParameters)
				{
					throw new InvalidOperationException("Late bound operations cannot be performed on types or methods for which ContainsGenericParameters is true.");
				}
				object obj2 = null;
				Exception ex;
				if ((invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default)
				{
					try
					{
						obj2 = this.InternalInvoke(obj, parameters, out ex);
						goto IL_0090;
					}
					catch (ThreadAbortException)
					{
						throw;
					}
					catch (OverflowException)
					{
						throw;
					}
					catch (Exception ex2)
					{
						throw new TargetInvocationException(ex2);
					}
				}
				obj2 = this.InternalInvoke(obj, parameters, out ex);
				IL_0090:
				if (ex != null)
				{
					throw ex;
				}
				return obj2;
			}
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x000F3D70 File Offset: 0x000F1F70
		internal static void ConvertValues(Binder binder, object[] args, ParameterInfo[] pinfo, CultureInfo culture, BindingFlags invokeAttr)
		{
			if (args == null)
			{
				if (pinfo.Length == 0)
				{
					return;
				}
				throw new TargetParameterCountException();
			}
			else
			{
				if (pinfo.Length != args.Length)
				{
					throw new TargetParameterCountException();
				}
				for (int i = 0; i < args.Length; i++)
				{
					object obj = args[i];
					ParameterInfo parameterInfo = pinfo[i];
					if (obj == Type.Missing)
					{
						if (parameterInfo.DefaultValue == DBNull.Value)
						{
							throw new ArgumentException(Environment.GetResourceString("Missing parameter does not have a default value."), "parameters");
						}
						args[i] = parameterInfo.DefaultValue;
					}
					else
					{
						RuntimeType runtimeType = (RuntimeType)parameterInfo.ParameterType;
						args[i] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
					}
				}
				return;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004CDA RID: 19674 RVA: 0x000F3DFE File Offset: 0x000F1FFE
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06004CDB RID: 19675 RVA: 0x000F3E0B File Offset: 0x000F200B
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06004CDC RID: 19676 RVA: 0x000F3E18 File Offset: 0x000F2018
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06004CDD RID: 19677 RVA: 0x000F3E25 File Offset: 0x000F2025
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004CDE RID: 19678 RVA: 0x000F3E2D File Offset: 0x000F202D
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x000F3E3A File Offset: 0x000F203A
		public override string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return RuntimeMethodInfo.get_name(this);
			}
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x000534DE File Offset: 0x000516DE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x000F133D File Offset: 0x000EF53D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x000F1346 File Offset: 0x000EF546
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004CE3 RID: 19683
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void GetPInvoke(out PInvokeAttributes flags, out string entryPoint, out string dllName);

		// Token: 0x06004CE4 RID: 19684 RVA: 0x000F3E54 File Offset: 0x000F2054
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new PreserveSigAttribute();
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = DllImportAttribute.GetCustomAttribute(this);
			}
			return array;
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x000F3ED8 File Offset: 0x000F20D8
		internal CustomAttributeData[] GetPseudoCustomAttributesData()
		{
			int num = 0;
			MonoMethodInfo methodInfo = MonoMethodInfo.GetMethodInfo(this.mhandle);
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				num++;
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			CustomAttributeData[] array = new CustomAttributeData[num];
			num = 0;
			if ((methodInfo.iattrs & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				array[num++] = new CustomAttributeData(typeof(PreserveSigAttribute).GetConstructor(Type.EmptyTypes));
			}
			if ((methodInfo.attrs & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope)
			{
				array[num++] = this.GetDllImportAttributeData();
			}
			return array;
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x000F3F70 File Offset: 0x000F2170
		private CustomAttributeData GetDllImportAttributeData()
		{
			if ((this.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			string text = null;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string text2;
			this.GetPInvoke(out pinvokeAttributes, out text2, out text);
			CharSet charSet;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				goto IL_005C;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				goto IL_005C;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				goto IL_005C;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				goto IL_005C;
			}
			charSet = CharSet.None;
			IL_005C:
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			CallingConvention callingConvention;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvWinapi)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.Winapi;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl;
					goto IL_00BB;
				}
			}
			else
			{
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvStdcall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.StdCall;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvThiscall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.ThisCall;
					goto IL_00BB;
				}
				if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
				{
					callingConvention = global::System.Runtime.InteropServices.CallingConvention.FastCall;
					goto IL_00BB;
				}
			}
			callingConvention = global::System.Runtime.InteropServices.CallingConvention.Cdecl;
			IL_00BB:
			bool flag = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool flag2 = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool flag3 = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool flag4 = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool flag5 = (this.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(typeof(string), text)
			};
			Type typeFromHandle = typeof(DllImportAttribute);
			CustomAttributeNamedArgument[] array2 = new CustomAttributeNamedArgument[]
			{
				new CustomAttributeNamedArgument(typeFromHandle.GetField("EntryPoint"), text2),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CharSet"), charSet),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ExactSpelling"), flag),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("SetLastError"), flag2),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("PreserveSig"), flag5),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CallingConvention"), callingConvention),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("BestFitMapping"), flag3),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ThrowOnUnmappableChar"), flag4)
			};
			return new CustomAttributeData(typeFromHandle.GetConstructor(new Type[] { typeof(string) }), array, array2);
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x000F41BC File Offset: 0x000F23BC
		public override MethodInfo MakeGenericMethod(params Type[] methodInstantiation)
		{
			if (methodInstantiation == null)
			{
				throw new ArgumentNullException("methodInstantiation");
			}
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("not a generic method definition");
			}
			if (this.GetGenericArguments().Length != methodInstantiation.Length)
			{
				throw new ArgumentException("Incorrect length");
			}
			bool flag = false;
			foreach (Type type in methodInstantiation)
			{
				if (type == null)
				{
					throw new ArgumentNullException();
				}
				if (!(type is RuntimeType))
				{
					flag = true;
				}
			}
			if (flag)
			{
				if (RuntimeFeature.IsDynamicCodeSupported)
				{
					return new MethodOnTypeBuilderInst(this, methodInstantiation);
				}
				throw new NotSupportedException("User types are not supported under full aot");
			}
			else
			{
				MethodInfo methodInfo = this.MakeGenericMethod_impl(methodInstantiation);
				if (methodInfo == null)
				{
					throw new ArgumentException(string.Format("The method has {0} generic parameter(s) but {1} generic argument(s) were provided.", this.GetGenericArguments().Length, methodInstantiation.Length));
				}
				return methodInfo;
			}
		}

		// Token: 0x06004CE8 RID: 19688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo MakeGenericMethod_impl(Type[] types);

		// Token: 0x06004CE9 RID: 19689
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern Type[] GetGenericArguments();

		// Token: 0x06004CEA RID: 19690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MethodInfo GetGenericMethodDefinition_impl();

		// Token: 0x06004CEB RID: 19691 RVA: 0x000F4281 File Offset: 0x000F2481
		public override MethodInfo GetGenericMethodDefinition()
		{
			MethodInfo genericMethodDefinition_impl = this.GetGenericMethodDefinition_impl();
			if (genericMethodDefinition_impl == null)
			{
				throw new InvalidOperationException();
			}
			return genericMethodDefinition_impl;
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004CEC RID: 19692
		public override extern bool IsGenericMethodDefinition
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004CED RID: 19693
		public override extern bool IsGenericMethod
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004CEE RID: 19694 RVA: 0x000F4298 File Offset: 0x000F2498
		public override bool ContainsGenericParameters
		{
			get
			{
				if (this.IsGenericMethod)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (genericArguments[i].ContainsGenericParameters)
						{
							return true;
						}
					}
				}
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x000F42D9 File Offset: 0x000F24D9
		public override MethodBody GetMethodBody()
		{
			return RuntimeMethodInfo.GetMethodBody(this.mhandle);
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x000F3670 File Offset: 0x000F1870
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004CF1 RID: 19697
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x000F42E6 File Offset: 0x000F24E6
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004CF3 RID: 19699 RVA: 0x000F42F1 File Offset: 0x000F24F1
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06004CF4 RID: 19700 RVA: 0x000F42FC File Offset: 0x000F24FC
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x06004CF5 RID: 19701 RVA: 0x000F4307 File Offset: 0x000F2507
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeMethodInfo>(other);
		}

		// Token: 0x04002FD9 RID: 12249
		internal IntPtr mhandle;

		// Token: 0x04002FDA RID: 12250
		private string name;

		// Token: 0x04002FDB RID: 12251
		private Type reftype;
	}
}
