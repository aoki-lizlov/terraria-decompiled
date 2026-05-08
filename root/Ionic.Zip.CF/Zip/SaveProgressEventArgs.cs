using System;

namespace Ionic.Zip
{
	// Token: 0x0200000C RID: 12
	public class SaveProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000022AA File Offset: 0x000004AA
		internal SaveProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesSaved, ZipEntry entry)
			: base(archiveName, before ? ZipProgressEventType.Saving_BeforeWriteEntry : ZipProgressEventType.Saving_AfterWriteEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			this._entriesSaved = entriesSaved;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000022D3 File Offset: 0x000004D3
		internal SaveProgressEventArgs()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000022DB File Offset: 0x000004DB
		internal SaveProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000022E8 File Offset: 0x000004E8
		internal static SaveProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_EntryBytesRead)
			{
				ArchiveName = archiveName,
				CurrentEntry = entry,
				BytesTransferred = bytesXferred,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000231C File Offset: 0x0000051C
		internal static SaveProgressEventArgs Started(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Started);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002334 File Offset: 0x00000534
		internal static SaveProgressEventArgs Completed(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Completed);
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000234B File Offset: 0x0000054B
		public int EntriesSaved
		{
			get
			{
				return this._entriesSaved;
			}
		}

		// Token: 0x04000025 RID: 37
		private int _entriesSaved;
	}
}
