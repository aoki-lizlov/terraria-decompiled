using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004F RID: 79
	internal class FSharpFunction
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x00011691 File Offset: 0x0000F891
		public FSharpFunction(object instance, MethodCall<object, object> invoker)
		{
			this._instance = instance;
			this._invoker = invoker;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000116A7 File Offset: 0x0000F8A7
		public object Invoke(params object[] args)
		{
			return this._invoker(this._instance, args);
		}

		// Token: 0x040001EB RID: 491
		private readonly object _instance;

		// Token: 0x040001EC RID: 492
		private readonly MethodCall<object, object> _invoker;
	}
}
