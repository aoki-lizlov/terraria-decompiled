using System;
using System.Diagnostics;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x02000865 RID: 2149
	[Serializable]
	public abstract class ConstructorInfo : MethodBase
	{
		// Token: 0x06004815 RID: 18453 RVA: 0x000EDBEE File Offset: 0x000EBDEE
		protected ConstructorInfo()
		{
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06004816 RID: 18454 RVA: 0x00003FB7 File Offset: 0x000021B7
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x000EDBF6 File Offset: 0x000EBDF6
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object Invoke(object[] parameters)
		{
			return this.Invoke(BindingFlags.CreateInstance, null, parameters, null);
		}

		// Token: 0x06004818 RID: 18456
		public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x06004819 RID: 18457 RVA: 0x000EDC06 File Offset: 0x000EBE06
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x000EDC0F File Offset: 0x000EBE0F
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x00064F2C File Offset: 0x0006312C
		public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x000EDC17 File Offset: 0x000EBE17
		public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
		{
			return !(left == right);
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x000EDC23 File Offset: 0x000EBE23
		// Note: this type is marked as 'beforefieldinit'.
		static ConstructorInfo()
		{
		}

		// Token: 0x04002E03 RID: 11779
		public static readonly string ConstructorName = ".ctor";

		// Token: 0x04002E04 RID: 11780
		public static readonly string TypeConstructorName = ".cctor";
	}
}
