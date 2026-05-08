using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000052 RID: 82
	public sealed class SongCollection : IEnumerable<Song>, IEnumerable, IDisposable
	{
		// Token: 0x17000166 RID: 358
		public Song this[int index]
		{
			get
			{
				return this.innerlist[index];
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0001FD67 File Offset: 0x0001DF67
		public int Count
		{
			get
			{
				return this.innerlist.Count;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0001FD74 File Offset: 0x0001DF74
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		public bool IsDisposed
		{
			[CompilerGenerated]
			get
			{
				return this.<IsDisposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsDisposed>k__BackingField = value;
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0001FD85 File Offset: 0x0001DF85
		internal SongCollection(List<Song> songs)
		{
			this.innerlist = songs;
			this.IsDisposed = false;
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0001FD9B File Offset: 0x0001DF9B
		public void Dispose()
		{
			this.innerlist.Clear();
			this.IsDisposed = true;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0001FDAF File Offset: 0x0001DFAF
		public IEnumerator<Song> GetEnumerator()
		{
			return this.innerlist.GetEnumerator();
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0001FDAF File Offset: 0x0001DFAF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.innerlist.GetEnumerator();
		}

		// Token: 0x040005FE RID: 1534
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x040005FF RID: 1535
		private List<Song> innerlist;
	}
}
