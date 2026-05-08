using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x0200089C RID: 2204
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		// Token: 0x06004A54 RID: 19028 RVA: 0x000EF7EE File Offset: 0x000ED9EE
		public TargetParameterCountException()
			: base("Number of parameters specified does not match the expected number.")
		{
			base.HResult = -2147352562;
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x000EF806 File Offset: 0x000EDA06
		public TargetParameterCountException(string message)
			: base(message)
		{
			base.HResult = -2147352562;
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x000EF81A File Offset: 0x000EDA1A
		public TargetParameterCountException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2147352562;
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x0006F2E5 File Offset: 0x0006D4E5
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
