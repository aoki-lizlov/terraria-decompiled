using System;

namespace System.Reflection
{
	// Token: 0x020008A6 RID: 2214
	public static class EventInfoExtensions
	{
		// Token: 0x06004AD8 RID: 19160 RVA: 0x000F0179 File Offset: 0x000EE379
		public static MethodInfo GetAddMethod(EventInfo eventInfo)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetAddMethod();
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x000F018C File Offset: 0x000EE38C
		public static MethodInfo GetAddMethod(EventInfo eventInfo, bool nonPublic)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetAddMethod(nonPublic);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x000F01A0 File Offset: 0x000EE3A0
		public static MethodInfo GetRaiseMethod(EventInfo eventInfo)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetRaiseMethod();
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x000F01B3 File Offset: 0x000EE3B3
		public static MethodInfo GetRaiseMethod(EventInfo eventInfo, bool nonPublic)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetRaiseMethod(nonPublic);
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x000F01C7 File Offset: 0x000EE3C7
		public static MethodInfo GetRemoveMethod(EventInfo eventInfo)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetRemoveMethod();
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x000F01DA File Offset: 0x000EE3DA
		public static MethodInfo GetRemoveMethod(EventInfo eventInfo, bool nonPublic)
		{
			Requires.NotNull(eventInfo, "eventInfo");
			return eventInfo.GetRemoveMethod(nonPublic);
		}
	}
}
