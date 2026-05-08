using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200087F RID: 2175
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class MethodInfo : MethodBase, _MethodInfo
	{
		// Token: 0x060048D1 RID: 18641 RVA: 0x000EDBEE File Offset: 0x000EBDEE
		protected MethodInfo()
		{
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060048D2 RID: 18642 RVA: 0x00048AA1 File Offset: 0x00046CA1
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060048D3 RID: 18643 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual ParameterInfo ReturnParameter
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060048D4 RID: 18644 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual Type ReturnType
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public override Type[] GetGenericArguments()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual MethodInfo GetGenericMethodDefinition()
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x060048D8 RID: 18648
		public abstract MethodInfo GetBaseDefinition();

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060048D9 RID: 18649
		public abstract ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

		// Token: 0x060048DA RID: 18650 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual Delegate CreateDelegate(Type delegateType)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x00047DC2 File Offset: 0x00045FC2
		public virtual Delegate CreateDelegate(Type delegateType, object target)
		{
			throw new NotSupportedException("Derived classes must provide an implementation.");
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x000EDC06 File Offset: 0x000EBE06
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x000EDC0F File Offset: 0x000EBE0F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x00064F2C File Offset: 0x0006312C
		public static bool operator ==(MethodInfo left, MethodInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x000EE8C4 File Offset: 0x000ECAC4
		public static bool operator !=(MethodInfo left, MethodInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodInfo.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x000174FB File Offset: 0x000156FB
		void _MethodInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x00047D48 File Offset: 0x00045F48
		Type _MethodInfo.GetType()
		{
			return base.GetType();
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060048E5 RID: 18661 RVA: 0x000EE8D0 File Offset: 0x000ECAD0
		internal virtual int GenericParameterCount
		{
			get
			{
				return this.GetGenericArguments().Length;
			}
		}
	}
}
