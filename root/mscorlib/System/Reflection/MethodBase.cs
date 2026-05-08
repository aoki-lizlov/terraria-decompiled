using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Reflection
{
	// Token: 0x0200087D RID: 2173
	[Serializable]
	public abstract class MethodBase : MemberInfo
	{
		// Token: 0x060048A3 RID: 18595 RVA: 0x00047D3C File Offset: 0x00045F3C
		protected MethodBase()
		{
		}

		// Token: 0x060048A4 RID: 18596
		public abstract ParameterInfo[] GetParameters();

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060048A5 RID: 18597
		public abstract MethodAttributes Attributes { get; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060048A6 RID: 18598 RVA: 0x000EE509 File Offset: 0x000EC709
		public virtual MethodImplAttributes MethodImplementationFlags
		{
			get
			{
				return this.GetMethodImplementationFlags();
			}
		}

		// Token: 0x060048A7 RID: 18599
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		// Token: 0x060048A8 RID: 18600 RVA: 0x00084CDD File Offset: 0x00082EDD
		public virtual MethodBody GetMethodBody()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060048A9 RID: 18601 RVA: 0x00003FB7 File Offset: 0x000021B7
		public virtual CallingConventions CallingConvention
		{
			get
			{
				return CallingConventions.Standard;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060048AA RID: 18602 RVA: 0x000EE511 File Offset: 0x000EC711
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060048AB RID: 18603 RVA: 0x000EE522 File Offset: 0x000EC722
		public bool IsConstructor
		{
			get
			{
				return this is ConstructorInfo && !this.IsStatic && (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x060048AC RID: 18604 RVA: 0x000EE549 File Offset: 0x000EC749
		public bool IsFinal
		{
			get
			{
				return (this.Attributes & MethodAttributes.Final) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x060048AD RID: 18605 RVA: 0x000EE557 File Offset: 0x000EC757
		public bool IsHideBySig
		{
			get
			{
				return (this.Attributes & MethodAttributes.HideBySig) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x000EE568 File Offset: 0x000EC768
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & MethodAttributes.SpecialName) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x060048AF RID: 18607 RVA: 0x000EE579 File Offset: 0x000EC779
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x060048B0 RID: 18608 RVA: 0x000EE587 File Offset: 0x000EC787
		public bool IsVirtual
		{
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060048B1 RID: 18609 RVA: 0x000EE595 File Offset: 0x000EC795
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060048B2 RID: 18610 RVA: 0x000EE5A2 File Offset: 0x000EC7A2
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060048B3 RID: 18611 RVA: 0x000EE5AF File Offset: 0x000EC7AF
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x000EE5BC File Offset: 0x000EC7BC
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060048B5 RID: 18613 RVA: 0x000EE5C9 File Offset: 0x000EC7C9
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060048B6 RID: 18614 RVA: 0x000EE5D6 File Offset: 0x000EC7D6
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060048B7 RID: 18615 RVA: 0x000EE5E3 File Offset: 0x000EC7E3
		public virtual bool IsConstructedGenericMethod
		{
			get
			{
				return this.IsGenericMethod && !this.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060048B8 RID: 18616 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060048B9 RID: 18617 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060048BB RID: 18619 RVA: 0x0000408A File Offset: 0x0000228A
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x000EE5F8 File Offset: 0x000EC7F8
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object Invoke(object obj, object[] parameters)
		{
			return this.Invoke(obj, BindingFlags.Default, null, parameters, null);
		}

		// Token: 0x060048BD RID: 18621
		public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060048BE RID: 18622
		public abstract RuntimeMethodHandle MethodHandle { get; }

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060048BF RID: 18623 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060048C0 RID: 18624 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060048C1 RID: 18625 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x000EDDA6 File Offset: 0x000EBFA6
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x000EDDAF File Offset: 0x000EBFAF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x000EE608 File Offset: 0x000EC808
		public static bool operator ==(MethodBase left, MethodBase right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			MethodInfo methodInfo;
			MethodInfo methodInfo2;
			if ((methodInfo = left as MethodInfo) != null && (methodInfo2 = right as MethodInfo) != null)
			{
				return methodInfo == methodInfo2;
			}
			ConstructorInfo constructorInfo;
			ConstructorInfo constructorInfo2;
			return (constructorInfo = left as ConstructorInfo) != null && (constructorInfo2 = right as ConstructorInfo) != null && constructorInfo == constructorInfo2;
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x000EE674 File Offset: 0x000EC874
		public static bool operator !=(MethodBase left, MethodBase right)
		{
			return !(left == right);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x000EE680 File Offset: 0x000EC880
		internal virtual ParameterInfo[] GetParametersInternal()
		{
			return this.GetParameters();
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x000EE688 File Offset: 0x000EC888
		internal virtual int GetParametersCount()
		{
			return this.GetParametersInternal().Length;
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x000174FB File Offset: 0x000156FB
		internal virtual Type GetParameterType(int pos)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x000EE692 File Offset: 0x000EC892
		internal virtual int get_next_table_index(object obj, int table, int count)
		{
			if (this is MethodBuilder)
			{
				return ((MethodBuilder)this).get_next_table_index(obj, table, count);
			}
			if (this is ConstructorBuilder)
			{
				return ((ConstructorBuilder)this).get_next_table_index(obj, table, count);
			}
			throw new Exception("Method is not a builder method");
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x000EE6CC File Offset: 0x000EC8CC
		internal virtual string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x000EE71C File Offset: 0x000EC91C
		internal virtual Type[] GetParameterTypes()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			Type[] array = new Type[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				array[i] = parametersNoCopy[i].ParameterType;
			}
			return array;
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x000EE680 File Offset: 0x000EC880
		internal virtual ParameterInfo[] GetParametersNoCopy()
		{
			return this.GetParameters();
		}

		// Token: 0x060048CD RID: 18637 RVA: 0x000EE754 File Offset: 0x000EC954
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("The handle is invalid."));
			}
			MethodBase methodFromHandleInternalType = RuntimeMethodInfo.GetMethodFromHandleInternalType(handle.Value, IntPtr.Zero);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			Type declaringType = methodFromHandleInternalType.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot resolve method {0} because the declaring type of the method handle {1} is generic. Explicitly provide the declaring type to GetMethodFromHandle."), methodFromHandleInternalType, declaringType.GetGenericTypeDefinition()));
			}
			return methodFromHandleInternalType;
		}

		// Token: 0x060048CE RID: 18638 RVA: 0x000EE7DC File Offset: 0x000EC9DC
		[ComVisible(false)]
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("The handle is invalid."));
			}
			MethodBase methodFromHandleInternalType = RuntimeMethodInfo.GetMethodFromHandleInternalType(handle.Value, declaringType.Value);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return methodFromHandleInternalType;
		}

		// Token: 0x060048CF RID: 18639 RVA: 0x000EE82C File Offset: 0x000ECA2C
		internal static string ConstructParameters(Type[] parameterTypes, CallingConventions callingConvention, bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (Type type in parameterTypes)
			{
				stringBuilder.Append(text);
				string text2 = type.FormatTypeName(serialization);
				if (type.IsByRef && !serialization)
				{
					stringBuilder.Append(text2.TrimEnd(new char[] { '&' }));
					stringBuilder.Append(" ByRef");
				}
				else
				{
					stringBuilder.Append(text2);
				}
				text = ", ";
			}
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("...");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060048D0 RID: 18640
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MethodBase GetCurrentMethod();
	}
}
