using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020008C5 RID: 2245
	internal abstract class RtFieldInfo : FieldInfo
	{
		// Token: 0x06004C7C RID: 19580
		internal abstract object UnsafeGetValue(object obj);

		// Token: 0x06004C7D RID: 19581
		internal abstract void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x06004C7E RID: 19582
		internal abstract void CheckConsistency(object target);

		// Token: 0x06004C7F RID: 19583 RVA: 0x000F3691 File Offset: 0x000F1891
		protected RtFieldInfo()
		{
		}
	}
}
