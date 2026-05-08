using System;

namespace System.IO
{
	// Token: 0x02000923 RID: 2339
	internal static class Error
	{
		// Token: 0x06005371 RID: 21361 RVA: 0x00118764 File Offset: 0x00116964
		internal static Exception GetStreamIsClosed()
		{
			return new ObjectDisposedException(null, "Cannot access a closed Stream.");
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x00118771 File Offset: 0x00116971
		internal static Exception GetEndOfFile()
		{
			return new EndOfStreamException("Unable to read beyond the end of the stream.");
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0011877D File Offset: 0x0011697D
		internal static Exception GetFileNotOpen()
		{
			return new ObjectDisposedException(null, "Cannot access a closed file.");
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x0011878A File Offset: 0x0011698A
		internal static Exception GetReadNotSupported()
		{
			return new NotSupportedException("Stream does not support reading.");
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x00118796 File Offset: 0x00116996
		internal static Exception GetSeekNotSupported()
		{
			return new NotSupportedException("Stream does not support seeking.");
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x001187A2 File Offset: 0x001169A2
		internal static Exception GetWriteNotSupported()
		{
			return new NotSupportedException("Stream does not support writing.");
		}
	}
}
