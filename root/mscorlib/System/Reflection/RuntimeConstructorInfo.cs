using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020008C9 RID: 2249
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeConstructorInfo : ConstructorInfo, ISerializable
	{
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06004CF6 RID: 19702 RVA: 0x000F4310 File Offset: 0x000F2510
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004CF7 RID: 19703 RVA: 0x000F4318 File Offset: 0x000F2518
		internal RuntimeModule GetRuntimeModule()
		{
			return RuntimeTypeHandle.GetModule((RuntimeType)this.DeclaringType);
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06004CF8 RID: 19704 RVA: 0x0000408A File Offset: 0x0000228A
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x000F3417 File Offset: 0x000F1617
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004CFA RID: 19706 RVA: 0x000F432A File Offset: 0x000F252A
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), this.SerializationToString(), MemberTypes.Constructor, null);
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x000F435A File Offset: 0x000F255A
		internal string SerializationToString()
		{
			return this.FormatNameAndSig(true);
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x000F4363 File Offset: 0x000F2563
		internal void SerializationInvoke(object target, SerializationInfo info, StreamingContext context)
		{
			base.Invoke(target, new object[] { info, context });
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x000F4380 File Offset: 0x000F2580
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MonoMethodInfo.GetMethodImplementationFlags(this.mhandle);
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x000F438D File Offset: 0x000F258D
		public override ParameterInfo[] GetParameters()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x000F438D File Offset: 0x000F258D
		internal override ParameterInfo[] GetParametersInternal()
		{
			return MonoMethodInfo.GetParametersInfo(this.mhandle, this);
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x000F439C File Offset: 0x000F259C
		internal override int GetParametersCount()
		{
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			if (parametersInfo != null)
			{
				return parametersInfo.Length;
			}
			return 0;
		}

		// Token: 0x06004D01 RID: 19713
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern object InternalInvoke(object obj, object[] parameters, out Exception exc);

		// Token: 0x06004D02 RID: 19714 RVA: 0x000F43BE File Offset: 0x000F25BE
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (obj == null)
			{
				if (!base.IsStatic)
				{
					throw new TargetException("Instance constructor requires a target");
				}
			}
			else if (!this.DeclaringType.IsInstanceOfType(obj))
			{
				throw new TargetException("Constructor does not match target type");
			}
			return this.DoInvoke(obj, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x000F43FC File Offset: 0x000F25FC
		private object DoInvoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			ParameterInfo[] parametersInfo = MonoMethodInfo.GetParametersInfo(this.mhandle, this);
			RuntimeMethodInfo.ConvertValues(binder, parameters, parametersInfo, culture, invokeAttr);
			if (obj == null && this.DeclaringType.ContainsGenericParameters)
			{
				string text = "Cannot create an instance of ";
				Type declaringType = this.DeclaringType;
				throw new MemberAccessException(text + ((declaringType != null) ? declaringType.ToString() : null) + " because Type.ContainsGenericParameters is true.");
			}
			if ((invokeAttr & BindingFlags.CreateInstance) != BindingFlags.Default && this.DeclaringType.IsAbstract)
			{
				throw new MemberAccessException(string.Format("Cannot create an instance of {0} because it is an abstract class", this.DeclaringType));
			}
			return this.InternalInvoke(obj, parameters, (invokeAttr & BindingFlags.DoNotWrapExceptions) == BindingFlags.Default);
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x000F44A4 File Offset: 0x000F26A4
		public object InternalInvoke(object obj, object[] parameters, bool wrapExceptions)
		{
			object obj2 = null;
			Exception ex;
			if (wrapExceptions)
			{
				try
				{
					obj2 = this.InternalInvoke(obj, parameters, out ex);
					goto IL_0026;
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
			IL_0026:
			if (ex != null)
			{
				throw ex;
			}
			if (obj != null)
			{
				return null;
			}
			return obj2;
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x000F4500 File Offset: 0x000F2700
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			return this.DoInvoke(null, invokeAttr, binder, parameters, culture);
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06004D06 RID: 19718 RVA: 0x000F450E File Offset: 0x000F270E
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return new RuntimeMethodHandle(this.mhandle);
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x000F451B File Offset: 0x000F271B
		public override MethodAttributes Attributes
		{
			get
			{
				return MonoMethodInfo.GetAttributes(this.mhandle);
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06004D08 RID: 19720 RVA: 0x000F4528 File Offset: 0x000F2728
		public override CallingConventions CallingConvention
		{
			get
			{
				return MonoMethodInfo.GetCallingConvention(this.mhandle);
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x000F4535 File Offset: 0x000F2735
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.DeclaringType.ContainsGenericParameters;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004D0A RID: 19722 RVA: 0x000F4542 File Offset: 0x000F2742
		public override Type ReflectedType
		{
			get
			{
				return this.reftype;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004D0B RID: 19723 RVA: 0x000F454A File Offset: 0x000F274A
		public override Type DeclaringType
		{
			get
			{
				return MonoMethodInfo.GetDeclaringType(this.mhandle);
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004D0C RID: 19724 RVA: 0x000F4557 File Offset: 0x000F2757
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

		// Token: 0x06004D0D RID: 19725 RVA: 0x000534DE File Offset: 0x000516DE
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x000F133D File Offset: 0x000EF53D
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x000F1346 File Offset: 0x000EF546
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x000F456E File Offset: 0x000F276E
		public override MethodBody GetMethodBody()
		{
			return RuntimeMethodInfo.GetMethodBody(this.mhandle);
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x000F457B File Offset: 0x000F277B
		public override string ToString()
		{
			return "Void " + this.FormatNameAndSig(false);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x000F3670 File Offset: 0x000F1870
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004D13 RID: 19731
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x06004D14 RID: 19732 RVA: 0x000F458E File Offset: 0x000F278E
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeConstructorInfo>(other);
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004D15 RID: 19733 RVA: 0x000F4597 File Offset: 0x000F2797
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004D16 RID: 19734 RVA: 0x000F45A2 File Offset: 0x000F27A2
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06004D17 RID: 19735 RVA: 0x000F45AD File Offset: 0x000F27AD
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06004D18 RID: 19736 RVA: 0x000F45B8 File Offset: 0x000F27B8
		public override int MetadataToken
		{
			get
			{
				return RuntimeConstructorInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004D19 RID: 19737
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeConstructorInfo method);

		// Token: 0x06004D1A RID: 19738 RVA: 0x000F45C0 File Offset: 0x000F27C0
		public RuntimeConstructorInfo()
		{
		}

		// Token: 0x04002FDC RID: 12252
		internal IntPtr mhandle;

		// Token: 0x04002FDD RID: 12253
		private string name;

		// Token: 0x04002FDE RID: 12254
		private Type reftype;
	}
}
