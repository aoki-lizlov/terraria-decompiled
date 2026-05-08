using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x0200005A RID: 90
	internal sealed class VideoPlayerTheora : BaseYUVPlayer, IVideoPlayerCodec, IDisposable
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0002121A File Offset: 0x0001F41A
		// (set) Token: 0x06000FB3 RID: 4019 RVA: 0x00021222 File Offset: 0x0001F422
		public bool IsLooped
		{
			[CompilerGenerated]
			get
			{
				return this.<IsLooped>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsLooped>k__BackingField = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0002122B File Offset: 0x0001F42B
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x00021233 File Offset: 0x0001F433
		public bool IsMuted
		{
			get
			{
				return this.backing_ismuted;
			}
			set
			{
				this.backing_ismuted = value;
				this.UpdateVolume();
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x00021242 File Offset: 0x0001F442
		public TimeSpan PlayPosition
		{
			get
			{
				return this.timer.Elapsed;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x0002124F File Offset: 0x0001F44F
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x00021257 File Offset: 0x0001F457
		public MediaState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<State>k__BackingField = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00021260 File Offset: 0x0001F460
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00021268 File Offset: 0x0001F468
		public Video Video
		{
			[CompilerGenerated]
			get
			{
				return this.<Video>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Video>k__BackingField = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00021271 File Offset: 0x0001F471
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x00021279 File Offset: 0x0001F479
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
				this.UpdateVolume();
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000212B4 File Offset: 0x0001F4B4
		private void UpdateVolume()
		{
			if (this.audioStream == null)
			{
				return;
			}
			if (this.IsMuted)
			{
				this.audioStream.Volume = 0f;
				return;
			}
			this.audioStream.Volume = this.Volume * (1f / SoundEffect.MasterVolume);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00021300 File Offset: 0x0001F500
		public VideoPlayerTheora()
		{
			this.IsLooped = false;
			this.IsMuted = false;
			this.State = MediaState.Stopped;
			this.Volume = 1f;
			this.timer = new Stopwatch();
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00021350 File Offset: 0x0001F550
		public override void Dispose()
		{
			if (base.IsDisposed)
			{
				return;
			}
			this.Stop();
			base.Dispose();
			if (this.audioStream != null)
			{
				this.audioStream.Dispose();
				this.audioStream = null;
			}
			if (this.yuvData != IntPtr.Zero)
			{
				FNAPlatform.Free(this.yuvData);
				this.yuvData = IntPtr.Zero;
			}
			if (this.theora != IntPtr.Zero)
			{
				Theorafile.tf_close(ref this.theora);
			}
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000213D8 File Offset: 0x0001F5D8
		public Texture2D GetTexture()
		{
			base.checkDisposed();
			if (this.Video == null)
			{
				throw new InvalidOperationException();
			}
			if (this.State == MediaState.Stopped || this.theora == IntPtr.Zero || Theorafile.tf_hasvideo(this.theora) == 0)
			{
				return this.videoTexture[0].RenderTarget as Texture2D;
			}
			int num = (int)(this.timer.Elapsed.TotalMilliseconds / (1000.0 / this.fps));
			if (num > this.currentFrame)
			{
				if (Theorafile.tf_readvideo(this.theora, this.yuvData, num - this.currentFrame) == 1 || this.currentFrame == -1)
				{
					this.UpdateTexture();
				}
				this.currentFrame = num;
			}
			bool flag = Theorafile.tf_eos(this.theora) == 1;
			if (this.audioStream != null)
			{
				flag &= this.audioStream.PendingBufferCount == 0;
			}
			if (flag)
			{
				if (this.Video.needsDurationHack)
				{
					this.Video.Duration = this.timer.Elapsed;
				}
				this.timer.Stop();
				this.timer.Reset();
				if (this.audioStream != null)
				{
					this.audioStream.Stop();
					this.audioStream.Dispose();
					this.audioStream = null;
				}
				Theorafile.tf_reset(this.theora);
				if (this.IsLooped)
				{
					this.InitializeTheoraStream();
					this.timer.Start();
					if (this.audioStream != null)
					{
						this.audioStream.Play();
					}
				}
				else
				{
					this.State = MediaState.Stopped;
				}
			}
			return this.videoTexture[0].RenderTarget as Texture2D;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0002157C File Offset: 0x0001F77C
		public void Play(Video video)
		{
			base.checkDisposed();
			this.Video = video;
			this.Video.parent = this;
			if (this.theora != IntPtr.Zero)
			{
				Theorafile.tf_close(ref this.theora);
				this.theora = IntPtr.Zero;
			}
			if (this.Video.needsDurationHack)
			{
				this.Video.Duration = TimeSpan.MaxValue;
			}
			Theorafile.tf_fopen(this.Video.handle, out this.theora);
			if (this.theora == IntPtr.Zero)
			{
				throw new FileNotFoundException(this.Video.handle);
			}
			int num;
			int num2;
			Theorafile.th_pixel_fmt th_pixel_fmt;
			Theorafile.tf_videoinfo(this.theora, out num, out num2, out this.fps, out th_pixel_fmt);
			int num3;
			int num4;
			if (th_pixel_fmt == Theorafile.th_pixel_fmt.TH_PF_420)
			{
				num3 = num / 2;
				num4 = num2 / 2;
			}
			else if (th_pixel_fmt == Theorafile.th_pixel_fmt.TH_PF_422)
			{
				num3 = num / 2;
				num4 = num2;
			}
			else
			{
				if (th_pixel_fmt != Theorafile.th_pixel_fmt.TH_PF_444)
				{
					throw new NotSupportedException("Unrecognized YUV format!");
				}
				num3 = num;
				num4 = num2;
			}
			if (this.Video.Width != num || this.Video.Height != num2)
			{
				throw new InvalidOperationException("XNB/OGV width/height mismatch! Width: " + this.Video.Width.ToString() + " Height: " + this.Video.Height.ToString());
			}
			if (Math.Abs((double)this.Video.FramesPerSecond - this.fps) >= 1.0)
			{
				throw new InvalidOperationException("XNB/OGV framesPerSecond mismatch! FPS: " + this.Video.FramesPerSecond.ToString());
			}
			if (this.Video.audioTrack >= 0)
			{
				this.SetAudioTrackEXT(this.Video.audioTrack);
			}
			if (this.Video.videoTrack >= 0)
			{
				this.SetVideoTrackEXT(this.Video.videoTrack);
			}
			if (this.State != MediaState.Stopped)
			{
				return;
			}
			this.State = MediaState.Playing;
			if (this.yuvData != IntPtr.Zero)
			{
				FNAPlatform.Free(this.yuvData);
			}
			this.yuvDataLen = num * num2 + num3 * num4 * 2;
			this.yuvData = FNAPlatform.Malloc(this.yuvDataLen);
			this.InitializeTheoraStream();
			if (Theorafile.tf_hasvideo(this.theora) == 1)
			{
				if (this.currentDevice != this.Video.GraphicsDevice)
				{
					base.GL_dispose();
					this.currentDevice = this.Video.GraphicsDevice;
					base.GL_initialize(Resources.YUVToRGBAEffect);
				}
				RenderTargetBinding renderTargetBinding = this.videoTexture[0];
				this.videoTexture[0] = new RenderTargetBinding(new RenderTarget2D(this.currentDevice, num, num2, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents));
				if (renderTargetBinding.RenderTarget != null)
				{
					renderTargetBinding.RenderTarget.Dispose();
				}
				base.GL_setupTextures(num, num2, num3, num4, SurfaceFormat.Alpha8);
			}
			this.timer.Start();
			if (this.audioStream != null)
			{
				this.audioStream.Play();
			}
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00021860 File Offset: 0x0001FA60
		public void Stop()
		{
			base.checkDisposed();
			if (this.State == MediaState.Stopped)
			{
				return;
			}
			this.State = MediaState.Stopped;
			this.timer.Stop();
			this.timer.Reset();
			if (this.audioStream != null)
			{
				this.audioStream.Stop();
				this.audioStream.Dispose();
				this.audioStream = null;
			}
			Theorafile.tf_reset(this.theora);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000218C9 File Offset: 0x0001FAC9
		public void Pause()
		{
			base.checkDisposed();
			if (this.State != MediaState.Playing)
			{
				return;
			}
			this.State = MediaState.Paused;
			this.timer.Stop();
			if (this.audioStream != null)
			{
				this.audioStream.Pause();
			}
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x00021900 File Offset: 0x0001FB00
		public void Resume()
		{
			base.checkDisposed();
			if (this.State != MediaState.Paused)
			{
				return;
			}
			this.State = MediaState.Playing;
			this.timer.Start();
			if (this.audioStream != null)
			{
				this.audioStream.Resume();
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x00021937 File Offset: 0x0001FB37
		public void SetAudioTrackEXT(int track)
		{
			if (this.theora != IntPtr.Zero)
			{
				Theorafile.tf_setaudiotrack(this.theora, track);
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00021958 File Offset: 0x0001FB58
		public void SetVideoTrackEXT(int track)
		{
			if (this.theora != IntPtr.Zero)
			{
				Theorafile.tf_setvideotrack(this.theora, track);
			}
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002197C File Offset: 0x0001FB7C
		private void OnBufferRequest(object sender, EventArgs args)
		{
			int num = Theorafile.tf_readaudio(this.theora, this.audioDataPtr, 8192);
			if (num > 0)
			{
				this.audioStream.SubmitFloatBufferEXT(VideoPlayerTheora.audioData, 0, num);
				return;
			}
			if (Theorafile.tf_eos(this.theora) == 1)
			{
				this.audioStream.BufferNeeded -= this.OnBufferRequest;
			}
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000219DC File Offset: 0x0001FBDC
		private void UpdateTexture()
		{
			FNA3D.FNA3D_SetTextureDataYUV(this.currentDevice.GLDevice, this.yuvTextures[0].texture, this.yuvTextures[1].texture, this.yuvTextures[2].texture, this.yuvTextures[0].Width, this.yuvTextures[0].Height, this.yuvTextures[1].Width, this.yuvTextures[1].Height, this.yuvData, this.yuvDataLen);
			base.GL_pushState();
			this.currentDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
			base.GL_popState();
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x00021A7C File Offset: 0x0001FC7C
		private void InitializeTheoraStream()
		{
			while (Theorafile.tf_readvideo(this.theora, this.yuvData, 1) == 0)
			{
			}
			if (Theorafile.tf_hasaudio(this.theora) == 1)
			{
				int num;
				int num2;
				Theorafile.tf_audioinfo(this.theora, out num, out num2);
				this.audioStream = new DynamicSoundEffectInstance(num2, (AudioChannels)num);
				this.audioStream.BufferNeeded += this.OnBufferRequest;
				this.UpdateVolume();
				for (int i = 0; i < 4; i++)
				{
					this.OnBufferRequest(this.audioStream, EventArgs.Empty);
					if (this.audioStream.PendingBufferCount == i)
					{
						break;
					}
				}
			}
			this.currentFrame = -1;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00021B18 File Offset: 0x0001FD18
		internal static VideoPlayer.VideoInfo ReadInfo(string fileName)
		{
			IntPtr intPtr;
			Theorafile.tf_fopen(fileName, out intPtr);
			int num;
			int num2;
			double num3;
			Theorafile.th_pixel_fmt th_pixel_fmt;
			Theorafile.tf_videoinfo(intPtr, out num, out num2, out num3, out th_pixel_fmt);
			Theorafile.tf_close(ref intPtr);
			return new VideoPlayer.VideoInfo
			{
				fps = num3,
				width = num,
				height = num2
			};
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00021B66 File Offset: 0x0001FD66
		// Note: this type is marked as 'beforefieldinit'.
		static VideoPlayerTheora()
		{
		}

		// Token: 0x04000636 RID: 1590
		[CompilerGenerated]
		private bool <IsLooped>k__BackingField;

		// Token: 0x04000637 RID: 1591
		private bool backing_ismuted;

		// Token: 0x04000638 RID: 1592
		[CompilerGenerated]
		private MediaState <State>k__BackingField;

		// Token: 0x04000639 RID: 1593
		[CompilerGenerated]
		private Video <Video>k__BackingField;

		// Token: 0x0400063A RID: 1594
		private float backing_volume;

		// Token: 0x0400063B RID: 1595
		private Stopwatch timer;

		// Token: 0x0400063C RID: 1596
		private IntPtr theora;

		// Token: 0x0400063D RID: 1597
		private double fps;

		// Token: 0x0400063E RID: 1598
		private IntPtr yuvData;

		// Token: 0x0400063F RID: 1599
		private int yuvDataLen;

		// Token: 0x04000640 RID: 1600
		private int currentFrame;

		// Token: 0x04000641 RID: 1601
		private const int AUDIO_BUFFER_SIZE = 8192;

		// Token: 0x04000642 RID: 1602
		private static readonly float[] audioData = new float[8192];

		// Token: 0x04000643 RID: 1603
		private static GCHandle audioHandle = GCHandle.Alloc(VideoPlayerTheora.audioData, GCHandleType.Pinned);

		// Token: 0x04000644 RID: 1604
		private IntPtr audioDataPtr = VideoPlayerTheora.audioHandle.AddrOfPinnedObject();

		// Token: 0x04000645 RID: 1605
		private DynamicSoundEffectInstance audioStream;
	}
}
