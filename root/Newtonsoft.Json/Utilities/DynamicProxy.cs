using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000046 RID: 70
	internal class DynamicProxy<T>
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0000F9B6 File Offset: 0x0000DBB6
		public virtual IEnumerable<string> GetDynamicMemberNames(T instance)
		{
			return CollectionUtils.ArrayEmpty<string>();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000F9BD File Offset: 0x0000DBBD
		public virtual bool TryBinaryOperation(T instance, BinaryOperationBinder binder, object arg, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		public virtual bool TryConvert(T instance, ConvertBinder binder, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000F9BD File Offset: 0x0000DBBD
		public virtual bool TryCreateInstance(T instance, CreateInstanceBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public virtual bool TryDeleteIndex(T instance, DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public virtual bool TryDeleteMember(T instance, DeleteMemberBinder binder)
		{
			return false;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000F9BD File Offset: 0x0000DBBD
		public virtual bool TryGetIndex(T instance, GetIndexBinder binder, object[] indexes, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		public virtual bool TryGetMember(T instance, GetMemberBinder binder, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000F9BD File Offset: 0x0000DBBD
		public virtual bool TryInvoke(T instance, InvokeBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000F9BD File Offset: 0x0000DBBD
		public virtual bool TryInvokeMember(T instance, InvokeMemberBinder binder, object[] args, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public virtual bool TrySetIndex(T instance, SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000F9CA File Offset: 0x0000DBCA
		public virtual bool TrySetMember(T instance, SetMemberBinder binder, object value)
		{
			return false;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000F9C4 File Offset: 0x0000DBC4
		public virtual bool TryUnaryOperation(T instance, UnaryOperationBinder binder, out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00008020 File Offset: 0x00006220
		public DynamicProxy()
		{
		}
	}
}
