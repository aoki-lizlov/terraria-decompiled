using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020002BF RID: 703
	[ComVisible(true)]
	public struct LockCookie
	{
		// Token: 0x0600207D RID: 8317 RVA: 0x00076BE6 File Offset: 0x00074DE6
		internal LockCookie(int thread_id)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = 0;
			this.WriterLocks = 0;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x00076BFD File Offset: 0x00074DFD
		internal LockCookie(int thread_id, int reader_locks, int writer_locks)
		{
			this.ThreadId = thread_id;
			this.ReaderLocks = reader_locks;
			this.WriterLocks = writer_locks;
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x00076C14 File Offset: 0x00074E14
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x00076C26 File Offset: 0x00074E26
		public bool Equals(LockCookie obj)
		{
			return this.ThreadId == obj.ThreadId && this.ReaderLocks == obj.ReaderLocks && this.WriterLocks == obj.WriterLocks;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x00076C55 File Offset: 0x00074E55
		public override bool Equals(object obj)
		{
			return obj is LockCookie && obj.Equals(this);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x00076C72 File Offset: 0x00074E72
		public static bool operator ==(LockCookie a, LockCookie b)
		{
			return a.Equals(b);
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00076C7C File Offset: 0x00074E7C
		public static bool operator !=(LockCookie a, LockCookie b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04001A38 RID: 6712
		internal int ThreadId;

		// Token: 0x04001A39 RID: 6713
		internal int ReaderLocks;

		// Token: 0x04001A3A RID: 6714
		internal int WriterLocks;
	}
}
