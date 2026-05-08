using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000047 RID: 71
	public sealed class MediaQueue
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x0001FB57 File Offset: 0x0001DD57
		public Song ActiveSong
		{
			get
			{
				if (this.songs.Count == 0 || this.ActiveSongIndex < 0)
				{
					return null;
				}
				return this.songs[this.ActiveSongIndex];
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0001FB82 File Offset: 0x0001DD82
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x0001FB8A File Offset: 0x0001DD8A
		public int ActiveSongIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<ActiveSongIndex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ActiveSongIndex>k__BackingField = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0001FB93 File Offset: 0x0001DD93
		public int Count
		{
			get
			{
				return this.songs.Count;
			}
		}

		// Token: 0x17000143 RID: 323
		public Song this[int index]
		{
			get
			{
				return this.songs[index];
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0001FBAE File Offset: 0x0001DDAE
		internal MediaQueue()
		{
			this.ActiveSongIndex = -1;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		internal void Add(Song song)
		{
			this.songs.Add(song);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0001FBD6 File Offset: 0x0001DDD6
		internal void Clear()
		{
			this.songs.Clear();
		}

		// Token: 0x040005F0 RID: 1520
		[CompilerGenerated]
		private int <ActiveSongIndex>k__BackingField;

		// Token: 0x040005F1 RID: 1521
		private List<Song> songs = new List<Song>();
	}
}
