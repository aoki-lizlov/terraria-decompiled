using System;

namespace Ionic.Zip
{
	// Token: 0x02000009 RID: 9
	public class ZipProgressEventArgs : EventArgs
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000020F5 File Offset: 0x000002F5
		internal ZipProgressEventArgs()
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000020FD File Offset: 0x000002FD
		internal ZipProgressEventArgs(string archiveName, ZipProgressEventType flavor)
		{
			this._archiveName = archiveName;
			this._flavor = flavor;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000211B File Offset: 0x0000031B
		public int EntriesTotal
		{
			get
			{
				return this._entriesTotal;
			}
			set
			{
				this._entriesTotal = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002124 File Offset: 0x00000324
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000212C File Offset: 0x0000032C
		public ZipEntry CurrentEntry
		{
			get
			{
				return this._latestEntry;
			}
			set
			{
				this._latestEntry = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002135 File Offset: 0x00000335
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000213D File Offset: 0x0000033D
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = this._cancel || value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002151 File Offset: 0x00000351
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002159 File Offset: 0x00000359
		public ZipProgressEventType EventType
		{
			get
			{
				return this._flavor;
			}
			set
			{
				this._flavor = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002162 File Offset: 0x00000362
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000216A File Offset: 0x0000036A
		public string ArchiveName
		{
			get
			{
				return this._archiveName;
			}
			set
			{
				this._archiveName = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002173 File Offset: 0x00000373
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000217B File Offset: 0x0000037B
		public long BytesTransferred
		{
			get
			{
				return this._bytesTransferred;
			}
			set
			{
				this._bytesTransferred = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002184 File Offset: 0x00000384
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000218C File Offset: 0x0000038C
		public long TotalBytesToTransfer
		{
			get
			{
				return this._totalBytesToTransfer;
			}
			set
			{
				this._totalBytesToTransfer = value;
			}
		}

		// Token: 0x0400001E RID: 30
		private int _entriesTotal;

		// Token: 0x0400001F RID: 31
		private bool _cancel;

		// Token: 0x04000020 RID: 32
		private ZipEntry _latestEntry;

		// Token: 0x04000021 RID: 33
		private ZipProgressEventType _flavor;

		// Token: 0x04000022 RID: 34
		private string _archiveName;

		// Token: 0x04000023 RID: 35
		private long _bytesTransferred;

		// Token: 0x04000024 RID: 36
		private long _totalBytesToTransfer;
	}
}
