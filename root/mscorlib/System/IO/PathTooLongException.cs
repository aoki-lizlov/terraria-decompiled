using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x0200092D RID: 2349
	[Serializable]
	public class PathTooLongException : IOException
	{
		// Token: 0x060053D6 RID: 21462 RVA: 0x0011A0AF File Offset: 0x001182AF
		public PathTooLongException()
			: base("The specified file name or path is too long, or a component of the specified path is too long.")
		{
			base.HResult = -2147024690;
		}

		// Token: 0x060053D7 RID: 21463 RVA: 0x0011A0C7 File Offset: 0x001182C7
		public PathTooLongException(string message)
			: base(message)
		{
			base.HResult = -2147024690;
		}

		// Token: 0x060053D8 RID: 21464 RVA: 0x0011A0DB File Offset: 0x001182DB
		public PathTooLongException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147024690;
		}

		// Token: 0x060053D9 RID: 21465 RVA: 0x00118719 File Offset: 0x00116919
		protected PathTooLongException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
