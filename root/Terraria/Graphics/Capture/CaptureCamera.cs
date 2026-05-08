using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.Utilities;

namespace Terraria.Graphics.Capture
{
	// Token: 0x020001DC RID: 476
	internal class CaptureCamera : IDisposable
	{
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x00520355 File Offset: 0x0051E555
		public bool IsCapturing
		{
			get
			{
				Monitor.Enter(this._captureLock);
				bool flag = this._activeSettings != null;
				Monitor.Exit(this._captureLock);
				return flag;
			}
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x00520378 File Offset: 0x0051E578
		public CaptureCamera(GraphicsDevice graphics)
		{
			CaptureCamera.CameraExists = true;
			this._graphics = graphics;
			this._spriteBatch = new SpriteBatch(graphics);
			try
			{
				this._frameBuffer = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, DepthFormat.None);
				this._filterFrameBuffer1 = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, DepthFormat.None);
				this._filterFrameBuffer2 = new RenderTarget2D(graphics, 2048, 2048, false, graphics.PresentationParameters.BackBufferFormat, DepthFormat.None);
				this._waterTarget = new WorldSceneLayerTarget(graphics, 2048, 2048);
			}
			catch
			{
				Main.CaptureModeDisabled = true;
				return;
			}
			this._downscaleSampleState = SamplerState.AnisotropicClamp;
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x00520460 File Offset: 0x0051E660
		public void Capture(CaptureSettings settings)
		{
			Main.GlobalTimerPaused = true;
			if (this._activeSettings != null)
			{
				throw new InvalidOperationException("Capture called while another capture was already active.");
			}
			try
			{
				object captureLock = this._captureLock;
				lock (captureLock)
				{
					this._activeSettings = settings;
					this._Capture();
				}
			}
			catch (OutOfMemoryException ex)
			{
				Console.WriteLine(ex);
				this._renderQueue.Clear();
				this._outputData = null;
				this.FinishCapture();
				Main.NewText(Language.GetTextValue("Error.CaptureOutOfMemory"), byte.MaxValue, 0, 0);
			}
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x00520504 File Offset: 0x0051E704
		private void _Capture()
		{
			Microsoft.Xna.Framework.Rectangle area = this._activeSettings.Area;
			float num = 1f;
			if (this._activeSettings.UseScaling)
			{
				if (area.Width * 16 > 4096)
				{
					num = 4096f / (float)(area.Width * 16);
				}
				if (area.Height * 16 > 4096)
				{
					num = Math.Min(num, 4096f / (float)(area.Height * 16));
				}
				num = Math.Min(1f, num);
				this._outputImageSize = new Size((int)MathHelper.Clamp((float)((int)(num * (float)(area.Width * 16))), 1f, 4096f), (int)MathHelper.Clamp((float)((int)(num * (float)(area.Height * 16))), 1f, 4096f));
				this._outputData = new byte[4 * this._outputImageSize.Width * this._outputImageSize.Height];
				int num2 = (int)Math.Floor((double)(num * 2048f));
				this._scaledFrameData = new byte[4 * num2 * num2];
				this._scaledFrameBuffer = new RenderTarget2D(this._graphics, num2, num2, false, this._graphics.PresentationParameters.BackBufferFormat, DepthFormat.None);
			}
			else
			{
				this._outputData = new byte[16777216];
			}
			this._tilesProcessed = 0f;
			this._totalTiles = (float)(area.Width * area.Height);
			for (int i = area.X; i < area.X + area.Width; i += 126)
			{
				for (int j = area.Y; j < area.Y + area.Height; j += 126)
				{
					int num3 = Math.Min(128, area.X + area.Width - i);
					int num4 = Math.Min(128, area.Y + area.Height - j);
					int num5 = (int)Math.Floor((double)(num * (float)(num3 * 16)));
					int num6 = (int)Math.Floor((double)(num * (float)(num4 * 16)));
					int num7 = (int)Math.Floor((double)(num * (float)((i - area.X) * 16)));
					int num8 = (int)Math.Floor((double)(num * (float)((j - area.Y) * 16)));
					this._renderQueue.Enqueue(new CaptureCamera.CaptureChunk(new Microsoft.Xna.Framework.Rectangle(i, j, num3, num4), new Microsoft.Xna.Framework.Rectangle(num7, num8, num5, num6)));
				}
			}
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x00520764 File Offset: 0x0051E964
		public void DrawTick()
		{
			Monitor.Enter(this._captureLock);
			if (this._activeSettings == null)
			{
				return;
			}
			if (this._renderQueue.Count > 0)
			{
				CaptureCamera.CaptureChunk captureChunk = this._renderQueue.Dequeue();
				this._graphics.SetRenderTarget(null);
				this._graphics.Clear(Microsoft.Xna.Framework.Color.Transparent);
				Main.instance.TilesRenderer.PrepareForAreaDrawing(captureChunk.Area.Left, captureChunk.Area.Right, captureChunk.Area.Top, captureChunk.Area.Bottom, false);
				Main.instance.TilePaintSystem.PrepareAllRequests();
				this._graphics.SetRenderTarget(this._frameBuffer);
				Main.instance.DrawCapture(captureChunk.Area, this._activeSettings, this);
				if (this._activeSettings.UseScaling)
				{
					this._graphics.SetRenderTarget(this._scaledFrameBuffer);
					this._graphics.Clear(Microsoft.Xna.Framework.Color.Transparent);
					this._spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, this._downscaleSampleState, DepthStencilState.Default, RasterizerState.CullNone);
					this._spriteBatch.Draw(this._frameBuffer, new Microsoft.Xna.Framework.Rectangle(0, 0, this._scaledFrameBuffer.Width, this._scaledFrameBuffer.Height), Microsoft.Xna.Framework.Color.White);
					this._spriteBatch.End();
					this._graphics.SetRenderTarget(null);
					this._scaledFrameBuffer.GetData<byte>(this._scaledFrameData, 0, this._scaledFrameBuffer.Width * this._scaledFrameBuffer.Height * 4);
					this.DrawBytesToBuffer(this._scaledFrameData, this._outputData, this._scaledFrameBuffer.Width, this._outputImageSize.Width, captureChunk.ScaledArea);
				}
				else
				{
					this._graphics.SetRenderTarget(null);
					this.SaveImage(this._frameBuffer, captureChunk.ScaledArea.Width, captureChunk.ScaledArea.Height, ImageFormat.Png, this._activeSettings.OutputName, string.Concat(new object[]
					{
						captureChunk.Area.X,
						"-",
						captureChunk.Area.Y,
						".png"
					}));
				}
				this._tilesProcessed += (float)(captureChunk.Area.Width * captureChunk.Area.Height);
			}
			if (this._renderQueue.Count == 0)
			{
				this.FinishCapture();
			}
			Monitor.Exit(this._captureLock);
		}

		// Token: 0x06002007 RID: 8199 RVA: 0x005209F8 File Offset: 0x0051EBF8
		public void BeginDrawCapture()
		{
			if (Lighting.NotRetro)
			{
				Main.instance.GraphicsDevice.SetRenderTarget(Main.skyTarget);
				Main.instance.GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.Transparent);
				Filters.Scene.BeginCapture(this._filterFrameBuffer1);
			}
			Main.waterTarget = this._waterTarget;
		}

		// Token: 0x06002008 RID: 8200 RVA: 0x00520A4F File Offset: 0x0051EC4F
		public void EndDrawCapture(Vector2 screenSize, Vector2 sceneSize, Vector2 sceneOffset)
		{
			if (Lighting.NotRetro)
			{
				Filters.Scene.EndCapture(this._frameBuffer, this._filterFrameBuffer1, this._filterFrameBuffer2, screenSize, sceneSize, sceneOffset);
			}
		}

		// Token: 0x06002009 RID: 8201 RVA: 0x00520A78 File Offset: 0x0051EC78
		private unsafe void DrawBytesToBuffer(byte[] sourceBuffer, byte[] destinationBuffer, int sourceBufferWidth, int destinationBufferWidth, Microsoft.Xna.Framework.Rectangle area)
		{
			fixed (byte* ptr = &destinationBuffer[0])
			{
				byte* ptr2 = ptr;
				fixed (byte* ptr3 = &sourceBuffer[0])
				{
					byte* ptr4 = ptr3;
					ptr2 += destinationBufferWidth * area.Y + area.X << 2;
					for (int i = 0; i < area.Height; i++)
					{
						for (int j = 0; j < area.Width; j++)
						{
							if (Program.IsXna)
							{
								ptr2[2] = *ptr4;
								ptr2[1] = ptr4[1];
								*ptr2 = ptr4[2];
								ptr2[3] = ptr4[3];
							}
							else
							{
								*ptr2 = *ptr4;
								ptr2[1] = ptr4[1];
								ptr2[2] = ptr4[2];
								ptr2[3] = ptr4[3];
							}
							ptr4 += 4;
							ptr2 += 4;
						}
						ptr4 += sourceBufferWidth - area.Width << 2;
						ptr2 += destinationBufferWidth - area.Width << 2;
					}
				}
			}
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x00520B50 File Offset: 0x0051ED50
		public float GetProgress()
		{
			return this._tilesProcessed / this._totalTiles;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00520B5F File Offset: 0x0051ED5F
		private bool SaveImage(int width, int height, ImageFormat imageFormat, string filename)
		{
			return CaptureCamera.SaveImage(this._outputData, width, height, imageFormat, filename);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00520B74 File Offset: 0x0051ED74
		public static bool SaveImage(byte[] data, int width, int height, ImageFormat imageFormat, string filename)
		{
			if (!Utils.TryCreatingDirectory(Main.SavePath + Path.DirectorySeparatorChar.ToString() + "Captures" + Path.DirectorySeparatorChar.ToString()))
			{
				return false;
			}
			bool flag;
			try
			{
				if (Program.IsFna)
				{
					PlatformUtilities.SavePng(filename, width, height, data);
				}
				else
				{
					using (Bitmap bitmap = new Bitmap(width, height))
					{
						global::System.Drawing.Rectangle rectangle = new global::System.Drawing.Rectangle(0, 0, width, height);
						BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
						IntPtr scan = bitmapData.Scan0;
						Marshal.Copy(data, 0, scan, width * height * 4);
						bitmap.UnlockBits(bitmapData);
						bitmap.Save(filename, imageFormat);
						bitmap.Dispose();
					}
				}
				flag = true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00520C50 File Offset: 0x0051EE50
		private void SaveImage(Texture2D texture, int width, int height, ImageFormat imageFormat, string foldername, string filename)
		{
			string text = string.Concat(new string[]
			{
				Main.SavePath,
				Path.DirectorySeparatorChar.ToString(),
				"Captures",
				Path.DirectorySeparatorChar.ToString(),
				foldername
			});
			string text2 = Path.Combine(text, filename);
			if (!Utils.TryCreatingDirectory(text))
			{
				return;
			}
			if (Program.IsFna)
			{
				int num = texture.Width * texture.Height * 4;
				texture.GetData<byte>(this._outputData, 0, num);
				int num2 = 0;
				int num3 = 0;
				for (int i = 0; i < height; i++)
				{
					for (int j = 0; j < width; j++)
					{
						this._outputData[num3] = this._outputData[num2];
						this._outputData[num3 + 1] = this._outputData[num2 + 1];
						this._outputData[num3 + 2] = this._outputData[num2 + 2];
						this._outputData[num3 + 3] = this._outputData[num2 + 3];
						num2 += 4;
						num3 += 4;
					}
					num2 += texture.Width - width << 2;
				}
				PlatformUtilities.SavePng(text2, width, height, this._outputData);
				return;
			}
			using (Bitmap bitmap = new Bitmap(width, height))
			{
				global::System.Drawing.Rectangle rectangle = new global::System.Drawing.Rectangle(0, 0, width, height);
				int num4 = texture.Width * texture.Height * 4;
				texture.GetData<byte>(this._outputData, 0, num4);
				int num5 = 0;
				int num6 = 0;
				for (int k = 0; k < height; k++)
				{
					for (int l = 0; l < width; l++)
					{
						byte b = this._outputData[num5 + 2];
						this._outputData[num6 + 2] = this._outputData[num5];
						this._outputData[num6] = b;
						this._outputData[num6 + 1] = this._outputData[num5 + 1];
						this._outputData[num6 + 3] = this._outputData[num5 + 3];
						num5 += 4;
						num6 += 4;
					}
					num5 += texture.Width - width << 2;
				}
				BitmapData bitmapData = bitmap.LockBits(rectangle, ImageLockMode.WriteOnly, PixelFormat.Format32bppPArgb);
				IntPtr scan = bitmapData.Scan0;
				Marshal.Copy(this._outputData, 0, scan, width * height * 4);
				bitmap.UnlockBits(bitmapData);
				bitmap.Save(text2, imageFormat);
			}
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x00520EB4 File Offset: 0x0051F0B4
		private void FinishCapture()
		{
			if (this._activeSettings.UseScaling && this._outputData != null)
			{
				int num = 0;
				while (!this.SaveImage(this._outputImageSize.Width, this._outputImageSize.Height, ImageFormat.Png, string.Concat(new string[]
				{
					Main.SavePath,
					Path.DirectorySeparatorChar.ToString(),
					"Captures",
					Path.DirectorySeparatorChar.ToString(),
					this._activeSettings.OutputName,
					".png"
				})))
				{
					GC.Collect();
					Thread.Sleep(5);
					num++;
					Console.WriteLine(Language.GetTextValue("Error.CaptureError"));
					if (num > 5)
					{
						Console.WriteLine(Language.GetTextValue("Error.UnableToCapture"));
						break;
					}
				}
			}
			this._outputData = null;
			this._scaledFrameData = null;
			Main.GlobalTimerPaused = false;
			CaptureInterface.EndCamera();
			if (this._scaledFrameBuffer != null)
			{
				this._scaledFrameBuffer.Dispose();
				this._scaledFrameBuffer = null;
			}
			this._activeSettings = null;
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00520FC4 File Offset: 0x0051F1C4
		public void Dispose()
		{
			if (!Main.dedServ)
			{
				Monitor.Enter(this._captureLock);
				if (this._isDisposed)
				{
					Monitor.Exit(this._captureLock);
					return;
				}
				this._frameBuffer.Dispose();
				this._filterFrameBuffer1.Dispose();
				this._filterFrameBuffer2.Dispose();
				this._waterTarget.Dispose();
				if (this._scaledFrameBuffer != null)
				{
					this._scaledFrameBuffer.Dispose();
					this._scaledFrameBuffer = null;
				}
				CaptureCamera.CameraExists = false;
				this._isDisposed = true;
				Monitor.Exit(this._captureLock);
			}
		}

		// Token: 0x04004A72 RID: 19058
		private static bool CameraExists;

		// Token: 0x04004A73 RID: 19059
		public const int CHUNK_SIZE = 128;

		// Token: 0x04004A74 RID: 19060
		public const int FRAMEBUFFER_PIXEL_SIZE = 2048;

		// Token: 0x04004A75 RID: 19061
		public const int INNER_CHUNK_SIZE = 126;

		// Token: 0x04004A76 RID: 19062
		public const int MAX_IMAGE_SIZE = 4096;

		// Token: 0x04004A77 RID: 19063
		public const string CAPTURE_DIRECTORY = "Captures";

		// Token: 0x04004A78 RID: 19064
		private RenderTarget2D _frameBuffer;

		// Token: 0x04004A79 RID: 19065
		private RenderTarget2D _scaledFrameBuffer;

		// Token: 0x04004A7A RID: 19066
		private RenderTarget2D _filterFrameBuffer1;

		// Token: 0x04004A7B RID: 19067
		private RenderTarget2D _filterFrameBuffer2;

		// Token: 0x04004A7C RID: 19068
		private WorldSceneLayerTarget _waterTarget;

		// Token: 0x04004A7D RID: 19069
		private GraphicsDevice _graphics;

		// Token: 0x04004A7E RID: 19070
		private readonly object _captureLock = new object();

		// Token: 0x04004A7F RID: 19071
		private bool _isDisposed;

		// Token: 0x04004A80 RID: 19072
		private CaptureSettings _activeSettings;

		// Token: 0x04004A81 RID: 19073
		private Queue<CaptureCamera.CaptureChunk> _renderQueue = new Queue<CaptureCamera.CaptureChunk>();

		// Token: 0x04004A82 RID: 19074
		private SpriteBatch _spriteBatch;

		// Token: 0x04004A83 RID: 19075
		private byte[] _scaledFrameData;

		// Token: 0x04004A84 RID: 19076
		private byte[] _outputData;

		// Token: 0x04004A85 RID: 19077
		private Size _outputImageSize;

		// Token: 0x04004A86 RID: 19078
		private SamplerState _downscaleSampleState;

		// Token: 0x04004A87 RID: 19079
		private float _tilesProcessed;

		// Token: 0x04004A88 RID: 19080
		private float _totalTiles;

		// Token: 0x0200079F RID: 1951
		private class CaptureChunk
		{
			// Token: 0x060041A4 RID: 16804 RVA: 0x006BC4BC File Offset: 0x006BA6BC
			public CaptureChunk(Microsoft.Xna.Framework.Rectangle area, Microsoft.Xna.Framework.Rectangle scaledArea)
			{
				this.Area = area;
				this.ScaledArea = scaledArea;
			}

			// Token: 0x0400707B RID: 28795
			public readonly Microsoft.Xna.Framework.Rectangle Area;

			// Token: 0x0400707C RID: 28796
			public readonly Microsoft.Xna.Framework.Rectangle ScaledArea;
		}
	}
}
