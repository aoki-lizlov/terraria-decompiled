using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000058 RID: 88
	public sealed class VideoPlayer : IDisposable
	{
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x0002075C File Offset: 0x0001E95C
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x00020764 File Offset: 0x0001E964
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0002076D File Offset: 0x0001E96D
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x00020775 File Offset: 0x0001E975
		public bool IsLooped
		{
			get
			{
				return this.backing_islooped;
			}
			set
			{
				this.backing_islooped = value;
				if (this.impl != null)
				{
					this.impl.IsLooped = value;
				}
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00020792 File Offset: 0x0001E992
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x0002079A File Offset: 0x0001E99A
		public bool IsMuted
		{
			get
			{
				return this.backing_ismuted;
			}
			set
			{
				this.backing_ismuted = value;
				if (this.impl != null)
				{
					this.impl.IsMuted = value;
				}
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x000207B7 File Offset: 0x0001E9B7
		public TimeSpan PlayPosition
		{
			get
			{
				if (this.impl == null)
				{
					return TimeSpan.Zero;
				}
				return this.impl.PlayPosition;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x000207D2 File Offset: 0x0001E9D2
		public MediaState State
		{
			get
			{
				if (this.impl == null)
				{
					return MediaState.Stopped;
				}
				return this.impl.State;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000207E9 File Offset: 0x0001E9E9
		public Video Video
		{
			get
			{
				if (this.impl == null)
				{
					return null;
				}
				return this.impl.Video;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00020800 File Offset: 0x0001EA00
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x00020808 File Offset: 0x0001EA08
		public float Volume
		{
			get
			{
				return this.backing_volume;
			}
			set
			{
				if (value > 1f)
				{
					this.backing_volume = 1f;
				}
				else if (value < 0f)
				{
					this.backing_volume = 0f;
				}
				else
				{
					this.backing_volume = value;
				}
				if (this.impl != null)
				{
					this.impl.Volume = this.backing_volume;
				}
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0002085F File Offset: 0x0001EA5F
		private void checkDisposed()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException("VideoPlayer");
			}
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00020874 File Offset: 0x0001EA74
		public VideoPlayer()
		{
			this.IsDisposed = false;
			this.IsLooped = false;
			this.IsMuted = false;
			this.Volume = 1f;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0002089C File Offset: 0x0001EA9C
		public void Dispose()
		{
			this.checkDisposed();
			if (this.impl != null)
			{
				this.impl.Dispose();
			}
			this.impl = null;
			this.IsDisposed = true;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000208C5 File Offset: 0x0001EAC5
		public Texture2D GetTexture()
		{
			this.checkDisposed();
			return this.impl.GetTexture();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000208D8 File Offset: 0x0001EAD8
		public void Play(Video video)
		{
			this.checkDisposed();
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
			this.impl = VideoPlayer.codecPlayers[video.Codec]();
			this.impl.IsLooped = this.IsLooped;
			this.impl.IsMuted = this.IsMuted;
			this.impl.Volume = this.Volume;
			this.impl.Play(video);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002095F File Offset: 0x0001EB5F
		public void Stop()
		{
			this.checkDisposed();
			if (this.impl != null)
			{
				this.impl.Stop();
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0002097A File Offset: 0x0001EB7A
		public void Pause()
		{
			this.checkDisposed();
			if (this.impl != null)
			{
				this.impl.Pause();
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00020995 File Offset: 0x0001EB95
		public void Resume()
		{
			this.checkDisposed();
			if (this.impl != null)
			{
				this.impl.Resume();
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000209B0 File Offset: 0x0001EBB0
		public void SetAudioTrackEXT(int track)
		{
			if (this.impl != null)
			{
				this.impl.SetAudioTrackEXT(track);
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000209C6 File Offset: 0x0001EBC6
		public void SetVideoTrackEXT(int track)
		{
			if (this.impl != null)
			{
				this.impl.SetVideoTrackEXT(track);
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000209DC File Offset: 0x0001EBDC
		// Note: this type is marked as 'beforefieldinit'.
		static VideoPlayer()
		{
		}

		// Token: 0x04000626 RID: 1574
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x04000627 RID: 1575
		private bool backing_islooped;

		// Token: 0x04000628 RID: 1576
		private bool backing_ismuted;

		// Token: 0x04000629 RID: 1577
		private float backing_volume;

		// Token: 0x0400062A RID: 1578
		private IVideoPlayerCodec impl;

		// Token: 0x0400062B RID: 1579
		internal static Dictionary<string, string> codecExtensions = new Dictionary<string, string>
		{
			{ "obu", "AV1" },
			{ "av1", "AV1" },
			{ "ogv", "Theora" }
		};

		// Token: 0x0400062C RID: 1580
		internal static Dictionary<string, Func<string, VideoPlayer.VideoInfo>> codecInfoReaders = new Dictionary<string, Func<string, VideoPlayer.VideoInfo>>
		{
			{
				"AV1",
				new Func<string, VideoPlayer.VideoInfo>(VideoPlayerAV1.ReadInfo)
			},
			{
				"Theora",
				new Func<string, VideoPlayer.VideoInfo>(VideoPlayerTheora.ReadInfo)
			}
		};

		// Token: 0x0400062D RID: 1581
		internal static Dictionary<string, Func<IVideoPlayerCodec>> codecPlayers = new Dictionary<string, Func<IVideoPlayerCodec>>
		{
			{
				"AV1",
				() => new VideoPlayerAV1()
			},
			{
				"Theora",
				() => new VideoPlayerTheora()
			}
		};

		// Token: 0x0200039F RID: 927
		public struct VideoInfo
		{
			// Token: 0x04001C3D RID: 7229
			public int width;

			// Token: 0x04001C3E RID: 7230
			public int height;

			// Token: 0x04001C3F RID: 7231
			public double fps;
		}

		// Token: 0x020003A0 RID: 928
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001AB4 RID: 6836 RVA: 0x0003F710 File Offset: 0x0003D910
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001AB5 RID: 6837 RVA: 0x000136F5 File Offset: 0x000118F5
			public <>c()
			{
			}

			// Token: 0x06001AB6 RID: 6838 RVA: 0x0003F71C File Offset: 0x0003D91C
			internal IVideoPlayerCodec <.cctor>b__37_0()
			{
				return new VideoPlayerAV1();
			}

			// Token: 0x06001AB7 RID: 6839 RVA: 0x0003F723 File Offset: 0x0003D923
			internal IVideoPlayerCodec <.cctor>b__37_1()
			{
				return new VideoPlayerTheora();
			}

			// Token: 0x04001C40 RID: 7232
			public static readonly VideoPlayer.<>c <>9 = new VideoPlayer.<>c();
		}
	}
}
