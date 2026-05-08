using System;

namespace Ionic.Zip
{
	// Token: 0x0200000A RID: 10
	public class ReadProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002195 File Offset: 0x00000395
		internal ReadProgressEventArgs()
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000219D File Offset: 0x0000039D
		private ReadProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000021A8 File Offset: 0x000003A8
		internal static ReadProgressEventArgs Before(string archiveName, int entriesTotal)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_BeforeReadEntry)
			{
				EntriesTotal = entriesTotal
			};
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000021C8 File Offset: 0x000003C8
		internal static ReadProgressEventArgs After(string archiveName, ZipEntry entry, int entriesTotal)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_AfterReadEntry)
			{
				EntriesTotal = entriesTotal,
				CurrentEntry = entry
			};
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000021EC File Offset: 0x000003EC
		internal static ReadProgressEventArgs Started(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Started);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002204 File Offset: 0x00000404
		internal static ReadProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_ArchiveBytesRead)
			{
				CurrentEntry = entry,
				BytesTransferred = bytesXferred,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002230 File Offset: 0x00000430
		internal static ReadProgressEventArgs Completed(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Completed);
		}
	}
}
