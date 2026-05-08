using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005F7 RID: 1527
	internal class MCMDictionary : MessageDictionary
	{
		// Token: 0x06003ADA RID: 15066 RVA: 0x000CE800 File Offset: 0x000CCA00
		public MCMDictionary(IMethodMessage message)
			: base(message)
		{
			base.MethodKeys = MCMDictionary.InternalKeys;
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x000CE814 File Offset: 0x000CCA14
		// Note: this type is marked as 'beforefieldinit'.
		static MCMDictionary()
		{
		}

		// Token: 0x04002623 RID: 9763
		public static string[] InternalKeys = new string[] { "__Uri", "__MethodName", "__TypeName", "__MethodSignature", "__Args", "__CallContext" };
	}
}
