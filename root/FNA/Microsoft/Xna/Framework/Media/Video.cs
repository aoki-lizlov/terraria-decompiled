using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000057 RID: 87
	public sealed class Video
	{
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x000204C4 File Offset: 0x0001E6C4
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x000204CC File Offset: 0x0001E6CC
		public int Width
		{
			[CompilerGenerated]
			get
			{
				return this.<Width>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Width>k__BackingField = value;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x000204D5 File Offset: 0x0001E6D5
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x000204DD File Offset: 0x0001E6DD
		public int Height
		{
			[CompilerGenerated]
			get
			{
				return this.<Height>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Height>k__BackingField = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000204E6 File Offset: 0x0001E6E6
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x000204EE File Offset: 0x0001E6EE
		public float FramesPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this.<FramesPerSecond>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FramesPerSecond>k__BackingField = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x000204F7 File Offset: 0x0001E6F7
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x000204FF File Offset: 0x0001E6FF
		public VideoSoundtrackType VideoSoundtrackType
		{
			[CompilerGenerated]
			get
			{
				return this.<VideoSoundtrackType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<VideoSoundtrackType>k__BackingField = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00020508 File Offset: 0x0001E708
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00020510 File Offset: 0x0001E710
		public TimeSpan Duration
		{
			[CompilerGenerated]
			get
			{
				return this.<Duration>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Duration>k__BackingField = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00020519 File Offset: 0x0001E719
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00020521 File Offset: 0x0001E721
		internal GraphicsDevice GraphicsDevice
		{
			[CompilerGenerated]
			get
			{
				return this.<GraphicsDevice>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<GraphicsDevice>k__BackingField = value;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0002052A File Offset: 0x0001E72A
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x00020532 File Offset: 0x0001E732
		internal string Codec
		{
			[CompilerGenerated]
			get
			{
				return this.<Codec>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Codec>k__BackingField = value;
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0002053C File Offset: 0x0001E73C
		internal Video(string fileName, GraphicsDevice device)
		{
			this.handle = fileName;
			this.GraphicsDevice = device;
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException(fileName);
			}
			this.Codec = Video.GuessCodec(fileName);
			VideoPlayer.VideoInfo videoInfo = VideoPlayer.codecInfoReaders[this.Codec](fileName);
			this.Width = videoInfo.width;
			this.Height = videoInfo.height;
			this.FramesPerSecond = (float)videoInfo.fps;
			this.Duration = TimeSpan.MaxValue;
			this.needsDurationHack = true;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x000205D4 File Offset: 0x0001E7D4
		internal Video(string fileName, GraphicsDevice device, int durationMS, int width, int height, float framesPerSecond, VideoSoundtrackType soundtrackType)
			: this(fileName, device, durationMS, width, height, framesPerSecond, soundtrackType, Video.GuessCodec(fileName))
		{
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x000205F8 File Offset: 0x0001E7F8
		internal Video(string fileName, GraphicsDevice device, int durationMS, int width, int height, float framesPerSecond, VideoSoundtrackType soundtrackType, string codec)
		{
			this.handle = fileName;
			this.GraphicsDevice = device;
			this.Width = width;
			this.Height = height;
			this.FramesPerSecond = framesPerSecond;
			this.Codec = codec;
			this.Duration = TimeSpan.FromMilliseconds((double)durationMS);
			this.needsDurationHack = false;
			this.VideoSoundtrackType = soundtrackType;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00020664 File Offset: 0x0001E864
		public static Video FromUriEXT(Uri uri, GraphicsDevice graphicsDevice)
		{
			string text;
			if (uri.IsAbsoluteUri)
			{
				if (!uri.IsFile)
				{
					throw new InvalidOperationException("Only local file URIs are supported for now");
				}
				text = uri.LocalPath;
			}
			else
			{
				text = Path.Combine(TitleLocation.Path, uri.ToString());
			}
			return new Video(text, graphicsDevice);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000206AD File Offset: 0x0001E8AD
		public void SetAudioTrackEXT(int track)
		{
			this.audioTrack = track;
			if (this.parent != null)
			{
				this.parent.SetAudioTrackEXT(track);
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000206CA File Offset: 0x0001E8CA
		public void SetVideoTrackEXT(int track)
		{
			this.videoTrack = track;
			if (this.parent != null)
			{
				this.parent.SetVideoTrackEXT(track);
			}
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000206E8 File Offset: 0x0001E8E8
		private static string GuessCodec(string filename)
		{
			filename = filename.ToLower();
			foreach (KeyValuePair<string, string> keyValuePair in VideoPlayer.codecExtensions)
			{
				if (filename.EndsWith(keyValuePair.Key))
				{
					return keyValuePair.Value;
				}
			}
			return "Theora";
		}

		// Token: 0x0400061A RID: 1562
		[CompilerGenerated]
		private int <Width>k__BackingField;

		// Token: 0x0400061B RID: 1563
		[CompilerGenerated]
		private int <Height>k__BackingField;

		// Token: 0x0400061C RID: 1564
		[CompilerGenerated]
		private float <FramesPerSecond>k__BackingField;

		// Token: 0x0400061D RID: 1565
		[CompilerGenerated]
		private VideoSoundtrackType <VideoSoundtrackType>k__BackingField;

		// Token: 0x0400061E RID: 1566
		[CompilerGenerated]
		private TimeSpan <Duration>k__BackingField;

		// Token: 0x0400061F RID: 1567
		[CompilerGenerated]
		private GraphicsDevice <GraphicsDevice>k__BackingField;

		// Token: 0x04000620 RID: 1568
		[CompilerGenerated]
		private string <Codec>k__BackingField;

		// Token: 0x04000621 RID: 1569
		internal string handle;

		// Token: 0x04000622 RID: 1570
		internal bool needsDurationHack;

		// Token: 0x04000623 RID: 1571
		internal int audioTrack = -1;

		// Token: 0x04000624 RID: 1572
		internal int videoTrack = -1;

		// Token: 0x04000625 RID: 1573
		internal IVideoPlayerCodec parent;
	}
}
