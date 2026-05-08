using System;

namespace System.Runtime.Serialization
{
	// Token: 0x0200061C RID: 1564
	[Serializable]
	public class SerializationException : SystemException
	{
		// Token: 0x06003BE7 RID: 15335 RVA: 0x000D0EA3 File Offset: 0x000CF0A3
		public SerializationException()
			: base(SerializationException.s_nullMessage)
		{
			base.HResult = -2146233076;
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x000D0EBB File Offset: 0x000CF0BB
		public SerializationException(string message)
			: base(message)
		{
			base.HResult = -2146233076;
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x000D0ECF File Offset: 0x000CF0CF
		public SerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2146233076;
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x000183F5 File Offset: 0x000165F5
		protected SerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x000D0EE4 File Offset: 0x000CF0E4
		// Note: this type is marked as 'beforefieldinit'.
		static SerializationException()
		{
		}

		// Token: 0x04002699 RID: 9881
		private static string s_nullMessage = "Serialization error.";
	}
}
