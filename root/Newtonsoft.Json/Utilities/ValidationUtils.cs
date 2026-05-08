using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000073 RID: 115
	internal static class ValidationUtils
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x00017EEC File Offset: 0x000160EC
		public static void ArgumentNotNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}
	}
}
