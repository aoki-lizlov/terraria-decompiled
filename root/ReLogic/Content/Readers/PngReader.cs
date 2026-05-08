using System;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ReLogic.Content.Readers
{
	// Token: 0x020000A2 RID: 162
	public class PngReader : IAssetReader, IDisposable
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0000D224 File Offset: 0x0000B424
		public PngReader(GraphicsDevice graphicsDevice)
		{
			this._graphicsDevice = graphicsDevice;
			this._colorProcessingCache = new ThreadLocal<Color[]>();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000D240 File Offset: 0x0000B440
		public T FromStream<T>(Stream stream) where T : class
		{
			if (typeof(T) != typeof(Texture2D))
			{
				throw AssetLoadException.FromInvalidReader<PngReader, T>();
			}
			Texture2D texture2D = Texture2D.FromStream(this._graphicsDevice, stream);
			int num = texture2D.Width * texture2D.Height;
			if (!this._colorProcessingCache.IsValueCreated || this._colorProcessingCache.Value.Length < num)
			{
				this._colorProcessingCache.Value = new Color[num];
			}
			Color[] value = this._colorProcessingCache.Value;
			texture2D.GetData<Color>(value, 0, num);
			for (int num2 = 0; num2 != num; num2++)
			{
				value[num2] = Color.FromNonPremultiplied(value[num2].ToVector4());
			}
			texture2D.SetData<Color>(value, 0, num);
			return texture2D as T;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000D304 File Offset: 0x0000B504
		protected virtual void Dispose(bool disposing)
		{
			if (this._disposedValue)
			{
				return;
			}
			if (disposing)
			{
				this._colorProcessingCache.Dispose();
			}
			this._disposedValue = true;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000D324 File Offset: 0x0000B524
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000524 RID: 1316
		private readonly GraphicsDevice _graphicsDevice;

		// Token: 0x04000525 RID: 1317
		private readonly ThreadLocal<Color[]> _colorProcessingCache;

		// Token: 0x04000526 RID: 1318
		private bool _disposedValue;
	}
}
