using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000046 RID: 70
	public static class MediaPlayer
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		public static bool GameHasControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x0001F5EB File Offset: 0x0001D7EB
		public static bool IsMuted
		{
			get
			{
				return MediaPlayer.INTERNAL_isMuted;
			}
			set
			{
				MediaPlayer.INTERNAL_isMuted = value;
				FAudio.XNA_SetSongVolume(MediaPlayer.INTERNAL_isMuted ? 0f : MediaPlayer.INTERNAL_volume);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0001F60B File Offset: 0x0001D80B
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x0001F612 File Offset: 0x0001D812
		public static bool IsRepeating
		{
			[CompilerGenerated]
			get
			{
				return MediaPlayer.<IsRepeating>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				MediaPlayer.<IsRepeating>k__BackingField = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0001F61A File Offset: 0x0001D81A
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x0001F621 File Offset: 0x0001D821
		public static bool IsShuffled
		{
			[CompilerGenerated]
			get
			{
				return MediaPlayer.<IsShuffled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				MediaPlayer.<IsShuffled>k__BackingField = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0001F629 File Offset: 0x0001D829
		public static TimeSpan PlayPosition
		{
			get
			{
				return MediaPlayer.timer.Elapsed;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0001F635 File Offset: 0x0001D835
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x0001F63C File Offset: 0x0001D83C
		public static MediaQueue Queue
		{
			[CompilerGenerated]
			get
			{
				return MediaPlayer.<Queue>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				MediaPlayer.<Queue>k__BackingField = value;
			}
		} = new MediaQueue();

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0001F644 File Offset: 0x0001D844
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0001F64B File Offset: 0x0001D84B
		public static MediaState State
		{
			get
			{
				return MediaPlayer.INTERNAL_state;
			}
			private set
			{
				if (MediaPlayer.INTERNAL_state != value)
				{
					MediaPlayer.INTERNAL_state = value;
					FrameworkDispatcher.MediaStateChanged = true;
				}
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0001F661 File Offset: 0x0001D861
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x0001F668 File Offset: 0x0001D868
		public static float Volume
		{
			get
			{
				return MediaPlayer.INTERNAL_volume;
			}
			set
			{
				MediaPlayer.INTERNAL_volume = MathHelper.Clamp(value, 0f, 1f);
				FAudio.XNA_SetSongVolume(MediaPlayer.IsMuted ? 0f : MediaPlayer.INTERNAL_volume);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0001F697 File Offset: 0x0001D897
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x0001F6A1 File Offset: 0x0001D8A1
		public static bool IsVisualizationEnabled
		{
			get
			{
				return FAudio.XNA_VisualizationEnabled() == 1U;
			}
			set
			{
				FAudio.XNA_EnableVisualization((value > false) ? 1U : 0U);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000ED4 RID: 3796 RVA: 0x0001F6AC File Offset: 0x0001D8AC
		// (remove) Token: 0x06000ED5 RID: 3797 RVA: 0x0001F6E0 File Offset: 0x0001D8E0
		public static event EventHandler<EventArgs> ActiveSongChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = MediaPlayer.ActiveSongChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref MediaPlayer.ActiveSongChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = MediaPlayer.ActiveSongChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref MediaPlayer.ActiveSongChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000ED6 RID: 3798 RVA: 0x0001F714 File Offset: 0x0001D914
		// (remove) Token: 0x06000ED7 RID: 3799 RVA: 0x0001F748 File Offset: 0x0001D948
		public static event EventHandler<EventArgs> MediaStateChanged
		{
			[CompilerGenerated]
			add
			{
				EventHandler<EventArgs> eventHandler = MediaPlayer.MediaStateChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref MediaPlayer.MediaStateChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<EventArgs> eventHandler = MediaPlayer.MediaStateChanged;
				EventHandler<EventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<EventArgs> eventHandler3 = (EventHandler<EventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<EventArgs>>(ref MediaPlayer.MediaStateChanged, eventHandler3, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0001F77C File Offset: 0x0001D97C
		static MediaPlayer()
		{
			AppDomain.CurrentDomain.ProcessExit += MediaPlayer.ProgramExit;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0001F7DF File Offset: 0x0001D9DF
		public static void MoveNext()
		{
			MediaPlayer.NextSong(1);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0001F7E7 File Offset: 0x0001D9E7
		public static void MovePrevious()
		{
			MediaPlayer.NextSong(-1);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0001F7EF File Offset: 0x0001D9EF
		public static void Pause()
		{
			if (MediaPlayer.State != MediaState.Playing || MediaPlayer.Queue.ActiveSong == null)
			{
				return;
			}
			FAudio.XNA_PauseSong();
			MediaPlayer.timer.Stop();
			MediaPlayer.State = MediaState.Paused;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0001F824 File Offset: 0x0001DA24
		public static void Play(Song song)
		{
			Song song2 = ((MediaPlayer.Queue.Count > 0) ? MediaPlayer.Queue[0] : null);
			MediaPlayer.Queue.Clear();
			MediaPlayer.numSongsInQueuePlayed = 0;
			MediaPlayer.LoadSong(song);
			MediaPlayer.Queue.ActiveSongIndex = 0;
			MediaPlayer.PlaySong(song);
			if (song2 != song)
			{
				FrameworkDispatcher.ActiveSongChanged = true;
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0001F881 File Offset: 0x0001DA81
		public static void Play(SongCollection songs)
		{
			MediaPlayer.Play(songs, 0);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0001F88C File Offset: 0x0001DA8C
		public static void Play(SongCollection songs, int index)
		{
			MediaPlayer.Queue.Clear();
			MediaPlayer.numSongsInQueuePlayed = 0;
			foreach (Song song in songs)
			{
				MediaPlayer.LoadSong(song);
			}
			MediaPlayer.Queue.ActiveSongIndex = index;
			MediaPlayer.PlaySong(MediaPlayer.Queue.ActiveSong);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0001F8FC File Offset: 0x0001DAFC
		public static void Resume()
		{
			if (MediaPlayer.State != MediaState.Paused)
			{
				return;
			}
			FAudio.XNA_ResumeSong();
			MediaPlayer.timer.Start();
			MediaPlayer.State = MediaState.Playing;
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0001F91C File Offset: 0x0001DB1C
		public static void Stop()
		{
			if (MediaPlayer.State == MediaState.Stopped)
			{
				return;
			}
			FAudio.XNA_StopSong();
			MediaPlayer.timer.Stop();
			MediaPlayer.timer.Reset();
			for (int i = 0; i < MediaPlayer.Queue.Count; i++)
			{
				MediaPlayer.Queue[i].PlayCount = 0;
			}
			MediaPlayer.State = MediaState.Stopped;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0001F976 File Offset: 0x0001DB76
		public static void GetVisualizationData(VisualizationData data)
		{
			FAudio.XNA_GetSongVisualizationData(data.freq, data.samp, 256U);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0001F990 File Offset: 0x0001DB90
		internal static void Update()
		{
			if (MediaPlayer.Queue == null || MediaPlayer.Queue.ActiveSong == null || MediaPlayer.State != MediaState.Playing || FAudio.XNA_GetSongEnded() == 0U)
			{
				return;
			}
			MediaPlayer.numSongsInQueuePlayed++;
			if (MediaPlayer.numSongsInQueuePlayed >= MediaPlayer.Queue.Count)
			{
				MediaPlayer.numSongsInQueuePlayed = 0;
				if (!MediaPlayer.IsRepeating)
				{
					MediaPlayer.Stop();
					FrameworkDispatcher.ActiveSongChanged = true;
					return;
				}
			}
			MediaPlayer.MoveNext();
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0001FA01 File Offset: 0x0001DC01
		internal static void OnActiveSongChanged()
		{
			if (MediaPlayer.ActiveSongChanged != null)
			{
				MediaPlayer.ActiveSongChanged(null, EventArgs.Empty);
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0001FA1A File Offset: 0x0001DC1A
		internal static void OnMediaStateChanged()
		{
			if (MediaPlayer.MediaStateChanged != null)
			{
				MediaPlayer.MediaStateChanged(null, EventArgs.Empty);
			}
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0001FA33 File Offset: 0x0001DC33
		private static void LoadSong(Song song)
		{
			MediaPlayer.Queue.Add(new Song(song.handle, song.Name));
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0001FA50 File Offset: 0x0001DC50
		private static void NextSong(int direction)
		{
			MediaPlayer.Stop();
			if (MediaPlayer.IsRepeating && MediaPlayer.Queue.ActiveSongIndex >= MediaPlayer.Queue.Count - 1)
			{
				MediaPlayer.Queue.ActiveSongIndex = 0;
				direction = 0;
			}
			if (MediaPlayer.IsShuffled)
			{
				MediaPlayer.Queue.ActiveSongIndex = MediaPlayer.random.Next(MediaPlayer.Queue.Count);
			}
			else
			{
				MediaPlayer.Queue.ActiveSongIndex = MathHelper.Clamp(MediaPlayer.Queue.ActiveSongIndex + direction, 0, MediaPlayer.Queue.Count - 1);
			}
			Song song = MediaPlayer.Queue[MediaPlayer.Queue.ActiveSongIndex];
			if (song != null)
			{
				MediaPlayer.PlaySong(song);
			}
			FrameworkDispatcher.ActiveSongChanged = true;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0001FB08 File Offset: 0x0001DD08
		private static void PlaySong(Song song)
		{
			if (!MediaPlayer.initialized)
			{
				FAudio.XNA_SongInit();
				MediaPlayer.initialized = true;
			}
			song.Duration = TimeSpan.FromSeconds((double)FAudio.XNA_PlaySong(song.handle));
			MediaPlayer.timer.Start();
			MediaPlayer.State = MediaState.Playing;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0001FB43 File Offset: 0x0001DD43
		private static void ProgramExit(object sender, EventArgs e)
		{
			if (MediaPlayer.initialized)
			{
				FAudio.XNA_SongQuit();
				MediaPlayer.initialized = false;
			}
		}

		// Token: 0x040005E4 RID: 1508
		[CompilerGenerated]
		private static bool <IsRepeating>k__BackingField;

		// Token: 0x040005E5 RID: 1509
		[CompilerGenerated]
		private static bool <IsShuffled>k__BackingField;

		// Token: 0x040005E6 RID: 1510
		[CompilerGenerated]
		private static MediaQueue <Queue>k__BackingField;

		// Token: 0x040005E7 RID: 1511
		[CompilerGenerated]
		private static EventHandler<EventArgs> ActiveSongChanged;

		// Token: 0x040005E8 RID: 1512
		[CompilerGenerated]
		private static EventHandler<EventArgs> MediaStateChanged;

		// Token: 0x040005E9 RID: 1513
		private static bool INTERNAL_isMuted = false;

		// Token: 0x040005EA RID: 1514
		private static MediaState INTERNAL_state = MediaState.Stopped;

		// Token: 0x040005EB RID: 1515
		private static float INTERNAL_volume = 1f;

		// Token: 0x040005EC RID: 1516
		private static bool initialized = false;

		// Token: 0x040005ED RID: 1517
		private static int numSongsInQueuePlayed = 0;

		// Token: 0x040005EE RID: 1518
		private static Stopwatch timer = new Stopwatch();

		// Token: 0x040005EF RID: 1519
		private static readonly Random random = new Random();
	}
}
