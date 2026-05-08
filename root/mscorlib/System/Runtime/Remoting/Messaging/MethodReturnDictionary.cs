using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005FD RID: 1533
	internal class MethodReturnDictionary : MessageDictionary
	{
		// Token: 0x06003B38 RID: 15160 RVA: 0x000CFA99 File Offset: 0x000CDC99
		public MethodReturnDictionary(IMethodReturnMessage message)
			: base(message)
		{
			if (message.Exception == null)
			{
				base.MethodKeys = MethodReturnDictionary.InternalReturnKeys;
				return;
			}
			base.MethodKeys = MethodReturnDictionary.InternalExceptionKeys;
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000CFAC4 File Offset: 0x000CDCC4
		// Note: this type is marked as 'beforefieldinit'.
		static MethodReturnDictionary()
		{
		}

		// Token: 0x0400263F RID: 9791
		public static string[] InternalReturnKeys = new string[] { "__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__OutArgs", "__Return", "__CallContext" };

		// Token: 0x04002640 RID: 9792
		public static string[] InternalExceptionKeys = new string[] { "__CallContext" };
	}
}
