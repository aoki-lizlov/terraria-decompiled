using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x02000866 RID: 2150
	[Serializable]
	public class CustomAttributeFormatException : FormatException
	{
		// Token: 0x0600481E RID: 18462 RVA: 0x000EDC39 File Offset: 0x000EBE39
		public CustomAttributeFormatException()
			: this("Binary format of the specified custom attribute was invalid.")
		{
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x000EDC46 File Offset: 0x000EBE46
		public CustomAttributeFormatException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x000EDC50 File Offset: 0x000EBE50
		public CustomAttributeFormatException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146232827;
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x000EDC65 File Offset: 0x000EBE65
		protected CustomAttributeFormatException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
