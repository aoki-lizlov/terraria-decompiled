using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x0200089B RID: 2203
	[Serializable]
	public sealed class TargetInvocationException : ApplicationException
	{
		// Token: 0x06004A51 RID: 19025 RVA: 0x000EF7C0 File Offset: 0x000ED9C0
		public TargetInvocationException(Exception inner)
			: base("Exception has been thrown by the target of an invocation.", inner)
		{
			base.HResult = -2146232828;
		}

		// Token: 0x06004A52 RID: 19026 RVA: 0x000EF7D9 File Offset: 0x000ED9D9
		public TargetInvocationException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232828;
		}

		// Token: 0x06004A53 RID: 19027 RVA: 0x0006F2E5 File Offset: 0x0006D4E5
		internal TargetInvocationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
