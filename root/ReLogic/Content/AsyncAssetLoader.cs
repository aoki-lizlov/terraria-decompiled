using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ReLogic.Content.Sources;
using ReLogic.Threading;

namespace ReLogic.Content
{
	// Token: 0x02000094 RID: 148
	public class AsyncAssetLoader : IAsyncAssetLoader, IDisposable
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000CC61 File Offset: 0x0000AE61
		public bool IsRunning
		{
			get
			{
				return this._loadDispatcher.IsRunning;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000CC6E File Offset: 0x0000AE6E
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000CC76 File Offset: 0x0000AE76
		public int Remaining
		{
			[CompilerGenerated]
			get
			{
				return this.<Remaining>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Remaining>k__BackingField = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000CC7F File Offset: 0x0000AE7F
		internal int AssetsReadyForTransfer
		{
			get
			{
				return this._pendingCallbacks.Count + this._pendingDelayedCallbacks.Count;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000CC98 File Offset: 0x0000AE98
		public AsyncAssetLoader(AssetReaderCollection readers)
		{
			this._readers = readers;
			this._maxCreationsPerTransfer = int.MaxValue;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		public AsyncAssetLoader(AssetReaderCollection readers, int maxCreationsPerTransfer)
		{
			this._readers = readers;
			this._maxCreationsPerTransfer = maxCreationsPerTransfer;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public void Load<T>(string assetName, IContentSource source, LoadAssetDelegate<T> callback) where T : class
		{
			int remaining = this.Remaining;
			this.Remaining = remaining + 1;
			if (this._delayedLoadTypes.Contains(typeof(T)))
			{
				this.DelayedLoad<T>(assetName, source, callback);
				return;
			}
			this.FullLoad<T>(assetName, source, callback);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000CD83 File Offset: 0x0000AF83
		public void RequireTypeCreationOnTransfer(Type type)
		{
			this._delayedLoadTypes.Add(type);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000CD94 File Offset: 0x0000AF94
		public void TransferCompleted()
		{
			Action action;
			while (this._pendingCallbacks.TryDequeue(ref action))
			{
				action.Invoke();
				int num = this.Remaining;
				this.Remaining = num - 1;
			}
			int num2 = 0;
			while (num2 < this._maxCreationsPerTransfer && this._pendingDelayedCallbacks.TryDequeue(ref action))
			{
				action.Invoke();
				int num = this.Remaining;
				this.Remaining = num - 1;
				num2++;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000CDFE File Offset: 0x0000AFFE
		public void Start()
		{
			this._loadDispatcher.Start();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000CE0B File Offset: 0x0000B00B
		public void Stop()
		{
			this._loadDispatcher.Stop();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000CE18 File Offset: 0x0000B018
		private void FullLoad<T>(string assetName, IContentSource source, LoadAssetDelegate<T> callback) where T : class
		{
			this._loadDispatcher.Queue(delegate
			{
				string extension = source.GetExtension(assetName);
				T resultAsset = default(T);
				if (this._readers.CanReadExtension(extension))
				{
					resultAsset = this._readers.Read<T>(source.OpenStream(assetName), extension);
				}
				this._pendingCallbacks.Enqueue(delegate
				{
					callback(resultAsset != null, resultAsset);
				});
			});
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000CE60 File Offset: 0x0000B060
		private void DelayedLoad<T>(string assetName, IContentSource source, LoadAssetDelegate<T> callback) where T : class
		{
			AsyncAssetLoader.<>c__DisplayClass22_0<T> CS$<>8__locals1 = new AsyncAssetLoader.<>c__DisplayClass22_0<T>();
			CS$<>8__locals1.source = source;
			CS$<>8__locals1.assetName = assetName;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.callback = callback;
			this._loadDispatcher.Queue(delegate
			{
				AsyncAssetLoader.<>c__DisplayClass22_1<T> CS$<>8__locals2 = new AsyncAssetLoader.<>c__DisplayClass22_1<T>();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.extension = CS$<>8__locals1.source.GetExtension(CS$<>8__locals1.assetName);
				if (!CS$<>8__locals1.<>4__this._readers.CanReadExtension(CS$<>8__locals2.extension))
				{
					ConcurrentQueue<Action> pendingDelayedCallbacks = CS$<>8__locals1.<>4__this._pendingDelayedCallbacks;
					Action action;
					if ((action = CS$<>8__locals1.<>9__1) == null)
					{
						action = (CS$<>8__locals1.<>9__1 = delegate
						{
							CS$<>8__locals1.callback(false, default(T));
						});
					}
					pendingDelayedCallbacks.Enqueue(action);
					return;
				}
				byte[] rawContent;
				using (Stream stream = CS$<>8__locals1.source.OpenStream(CS$<>8__locals1.assetName))
				{
					rawContent = new byte[stream.Length];
					stream.Read(rawContent, 0, rawContent.Length);
				}
				CS$<>8__locals1.<>4__this._pendingDelayedCallbacks.Enqueue(delegate
				{
					T t;
					using (MemoryStream memoryStream = new MemoryStream(rawContent, 0, rawContent.Length, false, true))
					{
						t = CS$<>8__locals2.CS$<>8__locals1.<>4__this._readers.Read<T>(memoryStream, CS$<>8__locals2.extension);
					}
					CS$<>8__locals2.CS$<>8__locals1.callback(true, t);
				});
			});
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000CEA6 File Offset: 0x0000B0A6
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				this.Stop();
				this._loadDispatcher.Dispose();
			}
			this._isDisposed = true;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000CECC File Offset: 0x0000B0CC
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000516 RID: 1302
		[CompilerGenerated]
		private int <Remaining>k__BackingField;

		// Token: 0x04000517 RID: 1303
		private readonly AsyncActionDispatcher _loadDispatcher = new AsyncActionDispatcher();

		// Token: 0x04000518 RID: 1304
		private readonly AssetReaderCollection _readers;

		// Token: 0x04000519 RID: 1305
		private readonly ConcurrentQueue<Action> _pendingCallbacks = new ConcurrentQueue<Action>();

		// Token: 0x0400051A RID: 1306
		private readonly ConcurrentQueue<Action> _pendingDelayedCallbacks = new ConcurrentQueue<Action>();

		// Token: 0x0400051B RID: 1307
		private readonly HashSet<Type> _delayedLoadTypes = new HashSet<Type>();

		// Token: 0x0400051C RID: 1308
		private readonly int _maxCreationsPerTransfer;

		// Token: 0x0400051D RID: 1309
		private bool _isDisposed;

		// Token: 0x020000ED RID: 237
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0<T> where T : class
		{
			// Token: 0x06000483 RID: 1155 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x0000E760 File Offset: 0x0000C960
			internal void <FullLoad>b__0()
			{
				AsyncAssetLoader.<>c__DisplayClass21_1<T> CS$<>8__locals1 = new AsyncAssetLoader.<>c__DisplayClass21_1<T>();
				CS$<>8__locals1.CS$<>8__locals1 = this;
				string extension = this.source.GetExtension(this.assetName);
				CS$<>8__locals1.resultAsset = default(T);
				if (this.<>4__this._readers.CanReadExtension(extension))
				{
					CS$<>8__locals1.resultAsset = this.<>4__this._readers.Read<T>(this.source.OpenStream(this.assetName), extension);
				}
				this.<>4__this._pendingCallbacks.Enqueue(delegate
				{
					CS$<>8__locals1.CS$<>8__locals1.callback(CS$<>8__locals1.resultAsset != null, CS$<>8__locals1.resultAsset);
				});
			}

			// Token: 0x04000623 RID: 1571
			public IContentSource source;

			// Token: 0x04000624 RID: 1572
			public string assetName;

			// Token: 0x04000625 RID: 1573
			public AsyncAssetLoader <>4__this;

			// Token: 0x04000626 RID: 1574
			public LoadAssetDelegate<T> callback;
		}

		// Token: 0x020000EE RID: 238
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_1<T> where T : class
		{
			// Token: 0x06000485 RID: 1157 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass21_1()
			{
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x0000E7EF File Offset: 0x0000C9EF
			internal void <FullLoad>b__1()
			{
				this.CS$<>8__locals1.callback(this.resultAsset != null, this.resultAsset);
			}

			// Token: 0x04000627 RID: 1575
			public T resultAsset;

			// Token: 0x04000628 RID: 1576
			public AsyncAssetLoader.<>c__DisplayClass21_0<T> CS$<>8__locals1;
		}

		// Token: 0x020000EF RID: 239
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0<T> where T : class
		{
			// Token: 0x06000487 RID: 1159 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x06000488 RID: 1160 RVA: 0x0000E818 File Offset: 0x0000CA18
			internal void <DelayedLoad>b__0()
			{
				AsyncAssetLoader.<>c__DisplayClass22_1<T> CS$<>8__locals1 = new AsyncAssetLoader.<>c__DisplayClass22_1<T>();
				CS$<>8__locals1.CS$<>8__locals1 = this;
				CS$<>8__locals1.extension = this.source.GetExtension(this.assetName);
				if (!this.<>4__this._readers.CanReadExtension(CS$<>8__locals1.extension))
				{
					ConcurrentQueue<Action> pendingDelayedCallbacks = this.<>4__this._pendingDelayedCallbacks;
					Action action;
					if ((action = this.<>9__1) == null)
					{
						action = (this.<>9__1 = delegate
						{
							this.callback(false, default(T));
						});
					}
					pendingDelayedCallbacks.Enqueue(action);
					return;
				}
				AsyncAssetLoader.<>c__DisplayClass22_2<T> CS$<>8__locals2 = new AsyncAssetLoader.<>c__DisplayClass22_2<T>();
				CS$<>8__locals2.CS$<>8__locals2 = CS$<>8__locals1;
				using (Stream stream = this.source.OpenStream(this.assetName))
				{
					CS$<>8__locals2.rawContent = new byte[stream.Length];
					stream.Read(CS$<>8__locals2.rawContent, 0, CS$<>8__locals2.rawContent.Length);
				}
				this.<>4__this._pendingDelayedCallbacks.Enqueue(delegate
				{
					T t;
					using (MemoryStream memoryStream = new MemoryStream(CS$<>8__locals2.rawContent, 0, CS$<>8__locals2.rawContent.Length, false, true))
					{
						t = CS$<>8__locals2.CS$<>8__locals2.CS$<>8__locals1.<>4__this._readers.Read<T>(memoryStream, CS$<>8__locals2.CS$<>8__locals2.extension);
					}
					CS$<>8__locals2.CS$<>8__locals2.CS$<>8__locals1.callback(true, t);
				});
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x0000E910 File Offset: 0x0000CB10
			internal void <DelayedLoad>b__1()
			{
				this.callback(false, default(T));
			}

			// Token: 0x04000629 RID: 1577
			public IContentSource source;

			// Token: 0x0400062A RID: 1578
			public string assetName;

			// Token: 0x0400062B RID: 1579
			public AsyncAssetLoader <>4__this;

			// Token: 0x0400062C RID: 1580
			public LoadAssetDelegate<T> callback;

			// Token: 0x0400062D RID: 1581
			public Action <>9__1;
		}

		// Token: 0x020000F0 RID: 240
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_1<T> where T : class
		{
			// Token: 0x0600048A RID: 1162 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass22_1()
			{
			}

			// Token: 0x0400062E RID: 1582
			public string extension;

			// Token: 0x0400062F RID: 1583
			public AsyncAssetLoader.<>c__DisplayClass22_0<T> CS$<>8__locals1;
		}

		// Token: 0x020000F1 RID: 241
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_2<T> where T : class
		{
			// Token: 0x0600048B RID: 1163 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass22_2()
			{
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x0000E934 File Offset: 0x0000CB34
			internal void <DelayedLoad>b__2()
			{
				T t;
				using (MemoryStream memoryStream = new MemoryStream(this.rawContent, 0, this.rawContent.Length, false, true))
				{
					t = this.CS$<>8__locals2.CS$<>8__locals1.<>4__this._readers.Read<T>(memoryStream, this.CS$<>8__locals2.extension);
				}
				this.CS$<>8__locals2.CS$<>8__locals1.callback(true, t);
			}

			// Token: 0x04000630 RID: 1584
			public byte[] rawContent;

			// Token: 0x04000631 RID: 1585
			public AsyncAssetLoader.<>c__DisplayClass22_1<T> CS$<>8__locals2;
		}
	}
}
