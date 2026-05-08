using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000091 RID: 145
	public interface IValueProvider
	{
		// Token: 0x0600069E RID: 1694
		void SetValue(object target, object value);

		// Token: 0x0600069F RID: 1695
		object GetValue(object target);
	}
}
