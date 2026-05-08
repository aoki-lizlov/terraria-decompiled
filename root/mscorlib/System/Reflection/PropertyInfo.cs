using System;
using System.Diagnostics;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x0200088C RID: 2188
	[Serializable]
	public abstract class PropertyInfo : MemberInfo
	{
		// Token: 0x06004952 RID: 18770 RVA: 0x00047D3C File Offset: 0x00045F3C
		protected PropertyInfo()
		{
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x0001E875 File Offset: 0x0001CA75
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06004954 RID: 18772
		public abstract Type PropertyType { get; }

		// Token: 0x06004955 RID: 18773
		public abstract ParameterInfo[] GetIndexParameters();

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06004956 RID: 18774
		public abstract PropertyAttributes Attributes { get; }

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x000EEEE2 File Offset: 0x000ED0E2
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & PropertyAttributes.SpecialName) > PropertyAttributes.None;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06004958 RID: 18776
		public abstract bool CanRead { get; }

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06004959 RID: 18777
		public abstract bool CanWrite { get; }

		// Token: 0x0600495A RID: 18778 RVA: 0x000EEEF3 File Offset: 0x000ED0F3
		public MethodInfo[] GetAccessors()
		{
			return this.GetAccessors(false);
		}

		// Token: 0x0600495B RID: 18779
		public abstract MethodInfo[] GetAccessors(bool nonPublic);

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600495C RID: 18780 RVA: 0x000EEEFC File Offset: 0x000ED0FC
		public virtual MethodInfo GetMethod
		{
			get
			{
				return this.GetGetMethod(true);
			}
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x000EEF05 File Offset: 0x000ED105
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		// Token: 0x0600495E RID: 18782
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600495F RID: 18783 RVA: 0x000EEF0E File Offset: 0x000ED10E
		public virtual MethodInfo SetMethod
		{
			get
			{
				return this.GetSetMethod(true);
			}
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x000EEF17 File Offset: 0x000ED117
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		// Token: 0x06004961 RID: 18785
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x06004962 RID: 18786 RVA: 0x000EECEA File Offset: 0x000ECEEA
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x000EECEA File Offset: 0x000ECEEA
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x000EEF20 File Offset: 0x000ED120
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object GetValue(object obj)
		{
			return this.GetValue(obj, null);
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x000EEF2A File Offset: 0x000ED12A
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06004966 RID: 18790
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06004967 RID: 18791 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual object GetConstantValue()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004968 RID: 18792 RVA: 0x00047D5E File Offset: 0x00045F5E
		public virtual object GetRawConstantValue()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004969 RID: 18793 RVA: 0x000EEF37 File Offset: 0x000ED137
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, null);
		}

		// Token: 0x0600496A RID: 18794 RVA: 0x000EEF42 File Offset: 0x000ED142
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		// Token: 0x0600496B RID: 18795
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x0600496C RID: 18796 RVA: 0x000EDDA6 File Offset: 0x000EBFA6
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600496D RID: 18797 RVA: 0x000EDDAF File Offset: 0x000EBFAF
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x00064F2C File Offset: 0x0006312C
		public static bool operator ==(PropertyInfo left, PropertyInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x0600496F RID: 18799 RVA: 0x000EEF50 File Offset: 0x000ED150
		public static bool operator !=(PropertyInfo left, PropertyInfo right)
		{
			return !(left == right);
		}
	}
}
