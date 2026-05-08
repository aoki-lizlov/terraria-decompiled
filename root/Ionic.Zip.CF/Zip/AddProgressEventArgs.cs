using System;

namespace Ionic.Zip
{
	// Token: 0x0200000B RID: 11
	public class AddProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002246 File Offset: 0x00000446
		internal AddProgressEventArgs()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000224E File Offset: 0x0000044E
		private AddProgressEventArgs(string archiveName, ZipProgressEventType flavor)
			: base(archiveName, flavor)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002258 File Offset: 0x00000458
		internal static AddProgressEventArgs AfterEntry(string archiveName, ZipEntry entry, int entriesTotal)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_AfterAddEntry)
			{
				EntriesTotal = entriesTotal,
				CurrentEntry = entry
			};
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000227C File Offset: 0x0000047C
		internal static AddProgressEventArgs Started(string archiveName)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_Started);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002294 File Offset: 0x00000494
		internal static AddProgressEventArgs Completed(string archiveName)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_Completed);
		}
	}
}
