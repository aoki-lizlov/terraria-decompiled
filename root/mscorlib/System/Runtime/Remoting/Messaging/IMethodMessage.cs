using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005F0 RID: 1520
	[ComVisible(true)]
	public interface IMethodMessage : IMessage
	{
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003A99 RID: 15001
		int ArgCount { get; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06003A9A RID: 15002
		object[] Args { get; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06003A9B RID: 15003
		bool HasVarArgs { get; }

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06003A9C RID: 15004
		LogicalCallContext LogicalCallContext { get; }

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06003A9D RID: 15005
		MethodBase MethodBase { get; }

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06003A9E RID: 15006
		string MethodName { get; }

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06003A9F RID: 15007
		object MethodSignature { get; }

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06003AA0 RID: 15008
		string TypeName { get; }

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06003AA1 RID: 15009
		string Uri { get; }

		// Token: 0x06003AA2 RID: 15010
		object GetArg(int argNum);

		// Token: 0x06003AA3 RID: 15011
		string GetArgName(int index);
	}
}
