using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework.Content;

namespace ReLogic.Content.Readers
{
	// Token: 0x020000A3 RID: 163
	public class XnbReader : IAssetReader, IDisposable
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000D330 File Offset: 0x0000B530
		public XnbReader(IServiceProvider services)
		{
			this._contentLoader = new ThreadLocal<XnbReader.InternalContentManager>(() => new XnbReader.InternalContentManager(services));
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000D367 File Offset: 0x0000B567
		public T FromStream<T>(Stream stream) where T : class
		{
			XnbReader.InternalContentManager value = this._contentLoader.Value;
			value.SetStream(stream);
			return value.Load<T>();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000D380 File Offset: 0x0000B580
		protected virtual void Dispose(bool disposing)
		{
			if (this._disposedValue)
			{
				return;
			}
			if (disposing)
			{
				this._contentLoader.Dispose();
			}
			this._disposedValue = true;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000D3A0 File Offset: 0x0000B5A0
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000527 RID: 1319
		private readonly ThreadLocal<XnbReader.InternalContentManager> _contentLoader;

		// Token: 0x04000528 RID: 1320
		private bool _disposedValue;

		// Token: 0x020000F4 RID: 244
		private class InternalContentManager : ContentManager
		{
			// Token: 0x06000496 RID: 1174 RVA: 0x0000EA5A File Offset: 0x0000CC5A
			public InternalContentManager(IServiceProvider serviceProvider)
				: base(serviceProvider)
			{
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x0000EA63 File Offset: 0x0000CC63
			public void SetStream(Stream stream)
			{
				this._stream = stream;
			}

			// Token: 0x06000498 RID: 1176 RVA: 0x0000EA6C File Offset: 0x0000CC6C
			public T Load<T>()
			{
				return base.ReadAsset<T>("XnaAsset", null);
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x0000EA7A File Offset: 0x0000CC7A
			protected override Stream OpenStream(string assetName)
			{
				return this._stream;
			}

			// Token: 0x04000637 RID: 1591
			private Stream _stream;
		}

		// Token: 0x020000F5 RID: 245
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0
		{
			// Token: 0x0600049A RID: 1178 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x0600049B RID: 1179 RVA: 0x0000EA82 File Offset: 0x0000CC82
			internal XnbReader.InternalContentManager <.ctor>b__0()
			{
				return new XnbReader.InternalContentManager(this.services);
			}

			// Token: 0x04000638 RID: 1592
			public IServiceProvider services;
		}
	}
}
