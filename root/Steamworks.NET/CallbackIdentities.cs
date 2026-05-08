using System;

namespace Steamworks
{
	// Token: 0x02000185 RID: 389
	internal class CallbackIdentities
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x0000D124 File Offset: 0x0000B324
		public static int GetCallbackIdentity(Type callbackStruct)
		{
			object[] customAttributes = callbackStruct.GetCustomAttributes(typeof(CallbackIdentityAttribute), false);
			int num = 0;
			if (num >= customAttributes.Length)
			{
				throw new Exception("Callback number not found for struct " + ((callbackStruct != null) ? callbackStruct.ToString() : null));
			}
			return ((CallbackIdentityAttribute)customAttributes[num]).Identity;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0000CD9E File Offset: 0x0000AF9E
		public CallbackIdentities()
		{
		}
	}
}
