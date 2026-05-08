using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Dav1dfile;
using Microsoft.Xna.Framework.Graphics;
using SDL3;

namespace Microsoft.Xna.Framework.Media
{
	// Token: 0x02000059 RID: 89
	internal class VideoPlayerAV1 : BaseYUVPlayer, IVideoPlayerCodec, IDisposable
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00020A9B File Offset: 0x0001EC9B
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x00020AA3 File Offset: 0x0001ECA3
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

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x000136EB File Offset: 0x000118EB
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x00009E6B File Offset: 0x0000806B
		public bool IsMuted
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00020AAC File Offset: 0x0001ECAC
		public TimeSpan PlayPosition
		{
			get
			{
				return this.timer.Elapsed;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00020AB9 File Offset: 0x0001ECB9
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x00020AC1 File Offset: 0x0001ECC1
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

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00020ACA File Offset: 0x0001ECCA
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00020AD2 File Offset: 0x0001ECD2
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

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00020ADB File Offset: 0x0001ECDB
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00009E6B File Offset: 0x0000806B
		public float Volume
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x00020AE2 File Offset: 0x0001ECE2
		public VideoPlayerAV1()
		{
			this.IsLooped = false;
			this.IsMuted = true;
			this.State = MediaState.Stopped;
			this.Volume = 0f;
			this.timer = new Stopwatch();
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00020B15 File Offset: 0x0001ED15
		public override void Dispose()
		{
			if (base.IsDisposed)
			{
				return;
			}
			this.Stop();
			base.Dispose();
			if (this.context != IntPtr.Zero)
			{
				Bindings.df_close(this.context);
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00020B4C File Offset: 0x0001ED4C
		public Texture2D GetTexture()
		{
			base.checkDisposed();
			if (this.Video == null)
			{
				throw new InvalidOperationException();
			}
			if (this.State == MediaState.Stopped || this.context == IntPtr.Zero)
			{
				return this.videoTexture[0].RenderTarget as Texture2D;
			}
			int num = (int)(this.timer.Elapsed.TotalMilliseconds / (1000.0 / this.fps));
			if (num > this.currentFrame)
			{
				if (this.DecodeAndUpdateFrame(num - this.currentFrame) || this.currentFrame == -1)
				{
					float num2;
					if (this.bitsPerPixel == 12)
					{
						num2 = 16f;
					}
					else if (this.bitsPerPixel == 10)
					{
						num2 = 64f;
					}
					else
					{
						num2 = 1f;
					}
					this.shaderProgram.Parameters["RescaleFactor"].SetValue(new Vector4(num2, num2, num2, 1f));
					base.GL_pushState();
					this.currentDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
					base.GL_popState();
				}
				this.currentFrame = num;
			}
			if (Bindings.df_eos(this.context) == 1)
			{
				if (this.Video.needsDurationHack)
				{
					this.Video.Duration = this.timer.Elapsed;
				}
				this.timer.Stop();
				this.timer.Reset();
				Bindings.df_reset(this.context);
				if (this.IsLooped)
				{
					this.currentFrame = -1;
					this.timer.Start();
				}
				else
				{
					this.State = MediaState.Stopped;
				}
			}
			return this.videoTexture[0].RenderTarget as Texture2D;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		public void Play(Video video)
		{
			base.checkDisposed();
			this.Video = video;
			this.Video.parent = this;
			if (this.context != IntPtr.Zero)
			{
				Bindings.df_close(this.context);
				this.context = IntPtr.Zero;
			}
			if (this.Video.needsDurationHack)
			{
				this.Video.Duration = TimeSpan.MaxValue;
			}
			int num = Bindings.df_fopen(this.Video.handle, out this.context);
			if (this.context == IntPtr.Zero || num == 0)
			{
				throw new FileNotFoundException(this.Video.handle);
			}
			int num2;
			int num3;
			Bindings.PixelLayout pixelLayout;
			try
			{
				byte b;
				Bindings.df_videoinfo2(this.context, out num2, out num3, out pixelLayout, out b);
				if (b == 2)
				{
					this.bitsPerPixel = 12;
				}
				else if (b == 1)
				{
					this.bitsPerPixel = 10;
				}
				else
				{
					this.bitsPerPixel = 8;
				}
				if (Bindings.df_guessframerate(this.context, out this.fps) == 0)
				{
					this.fps = (double)this.Video.FramesPerSecond;
				}
			}
			catch
			{
				Bindings.df_videoinfo(this.context, out num2, out num3, out pixelLayout);
				this.bitsPerPixel = 8;
				this.fps = (double)this.Video.FramesPerSecond;
			}
			int num4;
			int num5;
			switch (pixelLayout)
			{
			case Bindings.PixelLayout.I420:
				num4 = num2 / 2;
				num5 = num3 / 2;
				break;
			case Bindings.PixelLayout.I422:
				num4 = num2 / 2;
				num5 = num3;
				break;
			case Bindings.PixelLayout.I444:
				num4 = num2;
				num5 = num3;
				break;
			default:
				throw new NotSupportedException("Unsupported pixel layout in AV1 file");
			}
			if (this.Video.Width != num2 || this.Video.Height != num3)
			{
				throw new InvalidOperationException("XNB/OGV width/height mismatch! Width: " + this.Video.Width.ToString() + " Height: " + this.Video.Height.ToString());
			}
			if (this.fps == 0.0)
			{
				throw new InvalidOperationException("Framerate not present in header or manually specified");
			}
			if (Math.Abs((double)this.Video.FramesPerSecond - this.fps) >= 1.0)
			{
				throw new InvalidOperationException("XNB/OGV framesPerSecond mismatch! FPS: " + this.Video.FramesPerSecond.ToString());
			}
			if (this.State != MediaState.Stopped)
			{
				return;
			}
			this.State = MediaState.Playing;
			this.currentFrame = -1;
			if (this.currentDevice != this.Video.GraphicsDevice)
			{
				base.GL_dispose();
				this.currentDevice = this.Video.GraphicsDevice;
				base.GL_initialize(Resources.YUVToRGBAEffectR);
			}
			RenderTargetBinding renderTargetBinding = this.videoTexture[0];
			this.videoTexture[0] = new RenderTargetBinding(new RenderTarget2D(this.currentDevice, num2, num3, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents));
			if (renderTargetBinding.RenderTarget != null)
			{
				renderTargetBinding.RenderTarget.Dispose();
			}
			base.GL_setupTextures(num2, num3, num4, num5, (this.bitsPerPixel > 8) ? SurfaceFormat.UShortEXT : SurfaceFormat.ByteEXT);
			this.timer.Start();
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x00020FE0 File Offset: 0x0001F1E0
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
			Bindings.df_reset(this.context);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x00021019 File Offset: 0x0001F219
		public void Pause()
		{
			base.checkDisposed();
			if (this.State != MediaState.Playing)
			{
				return;
			}
			this.State = MediaState.Paused;
			this.timer.Stop();
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0002103D File Offset: 0x0001F23D
		public void Resume()
		{
			base.checkDisposed();
			if (this.State != MediaState.Paused)
			{
				return;
			}
			this.State = MediaState.Playing;
			this.timer.Start();
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x00009E6B File Offset: 0x0000806B
		public void SetAudioTrackEXT(int track)
		{
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00009E6B File Offset: 0x0000806B
		public void SetVideoTrackEXT(int track)
		{
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00021064 File Offset: 0x0001F264
		private bool DecodeAndUpdateFrame(int frameCount = 1)
		{
			byte[] array = null;
			byte[] array2 = null;
			IntPtr intPtr;
			IntPtr intPtr2;
			IntPtr intPtr3;
			uint num;
			uint num2;
			uint num3;
			uint num4;
			if (Bindings.df_readvideo(this.context, frameCount, out intPtr, out intPtr2, out intPtr3, out num, out num2, out num3, out num4) != 1)
			{
				return false;
			}
			this.UploadDataToTexture(this.yuvTextures[0], intPtr, num, num3, ref array);
			this.UploadDataToTexture(this.yuvTextures[1], intPtr2, num2, num4, ref array2);
			this.UploadDataToTexture(this.yuvTextures[2], intPtr3, num2, num4, ref array2);
			return true;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000210D8 File Offset: 0x0001F2D8
		private unsafe void UploadDataToTexture(Texture2D texture, IntPtr data, uint length, uint stride, ref byte[] scratchBuffer)
		{
			int width = texture.Width;
			int height = texture.Height;
			int num = Math.Min((int)(length / stride), height);
			int num2 = ((this.bitsPerPixel > 8) ? 2 : 1);
			int num3 = num2 * width;
			if ((long)width == (long)((ulong)stride))
			{
				texture.SetDataPointerEXT(0, new Rectangle?(new Rectangle(0, 0, width, num)), data, (int)length);
				return;
			}
			Array.Resize<byte>(ref scratchBuffer, width * num * num2);
			byte[] array;
			byte* ptr;
			if ((array = scratchBuffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			for (int i = 0; i < num; i++)
			{
				SDL.SDL_memcpy((IntPtr)((void*)ptr) + num3 * i, data + (int)((ulong)stride * (ulong)((long)i)), (UIntPtr)((ulong)((long)num3)));
			}
			texture.SetDataPointerEXT(0, null, (IntPtr)((void*)ptr), scratchBuffer.Length);
			array = null;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000211B8 File Offset: 0x0001F3B8
		internal static VideoPlayer.VideoInfo ReadInfo(string fileName)
		{
			IntPtr intPtr;
			Bindings.df_fopen(fileName, out intPtr);
			int num;
			int num2;
			Bindings.PixelLayout pixelLayout;
			byte b;
			Bindings.df_videoinfo2(intPtr, out num, out num2, out pixelLayout, out b);
			double num3;
			if (Bindings.df_guessframerate(intPtr, out num3) == 0)
			{
				num3 = 0.0;
			}
			Bindings.df_close(intPtr);
			return new VideoPlayer.VideoInfo
			{
				fps = num3,
				width = num,
				height = num2
			};
		}

		// Token: 0x0400062E RID: 1582
		[CompilerGenerated]
		private bool <IsLooped>k__BackingField;

		// Token: 0x0400062F RID: 1583
		[CompilerGenerated]
		private MediaState <State>k__BackingField;

		// Token: 0x04000630 RID: 1584
		[CompilerGenerated]
		private Video <Video>k__BackingField;

		// Token: 0x04000631 RID: 1585
		private Stopwatch timer;

		// Token: 0x04000632 RID: 1586
		private IntPtr context;

		// Token: 0x04000633 RID: 1587
		private int bitsPerPixel;

		// Token: 0x04000634 RID: 1588
		private int currentFrame;

		// Token: 0x04000635 RID: 1589
		private double fps;
	}
}
