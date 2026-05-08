using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using ReLogic.Content.Sources;

namespace ReLogic.Content
{
	// Token: 0x02000093 RID: 147
	public class AssetRepository : IAssetRepository, IDisposable
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000C2C6 File Offset: 0x0000A4C6
		public int PendingAssets
		{
			get
			{
				return this._asyncLoader.Remaining;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000C2D3 File Offset: 0x0000A4D3
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000C2DB File Offset: 0x0000A4DB
		public int TotalAssets
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalAssets>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TotalAssets>k__BackingField = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		public int LoadedAssets
		{
			[CompilerGenerated]
			get
			{
				return this.<LoadedAssets>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<LoadedAssets>k__BackingField = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000C2F5 File Offset: 0x0000A4F5
		public bool IsAsyncLoadingEnabled
		{
			get
			{
				return this._asyncLoader.IsRunning;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000C302 File Offset: 0x0000A502
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000C30A File Offset: 0x0000A50A
		public AssetValueUpdated AssetValueUpdatedHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<AssetValueUpdatedHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AssetValueUpdatedHandler>k__BackingField = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000C313 File Offset: 0x0000A513
		// (set) Token: 0x06000343 RID: 835 RVA: 0x0000C31B File Offset: 0x0000A51B
		public FailedToLoadAssetCustomAction AssetLoadFailHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<AssetLoadFailHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AssetLoadFailHandler>k__BackingField = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000C324 File Offset: 0x0000A524
		// (set) Token: 0x06000345 RID: 837 RVA: 0x0000C32C File Offset: 0x0000A52C
		public AssetWatcherValueUpdated AssetWatcherValueUpdatedHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<AssetWatcherValueUpdatedHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AssetWatcherValueUpdatedHandler>k__BackingField = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000C335 File Offset: 0x0000A535
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000C33D File Offset: 0x0000A53D
		public AssetWatcherUpdateFailed AssetWatcherUpdateFailedHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<AssetWatcherUpdateFailedHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AssetWatcherUpdateFailedHandler>k__BackingField = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000C346 File Offset: 0x0000A546
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000C34E File Offset: 0x0000A54E
		public ContentFileUpdated ContentFileUpdatedHandler
		{
			[CompilerGenerated]
			get
			{
				return this.<ContentFileUpdatedHandler>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ContentFileUpdatedHandler>k__BackingField = value;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C358 File Offset: 0x0000A558
		public AssetRepository(IAssetLoader syncLoader, IAsyncAssetLoader asyncLoader)
		{
			this._loader = syncLoader;
			this._asyncLoader = asyncLoader;
			this._asyncLoader.Start();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000C3BC File Offset: 0x0000A5BC
		internal AssetRepository(IAssetLoader syncLoader, IAsyncAssetLoader asyncLoader, bool useAsync)
		{
			this._loader = syncLoader;
			this._asyncLoader = asyncLoader;
			if (useAsync)
			{
				this._asyncLoader.Start();
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C424 File Offset: 0x0000A624
		public void SetSources(IEnumerable<IContentSource> sources, AssetRequestMode mode = AssetRequestMode.ImmediateLoad)
		{
			this.ThrowIfDisposed();
			object requestLock = this._requestLock;
			lock (requestLock)
			{
				while (this._asyncLoader.Remaining > 0)
				{
					this._asyncLoader.TransferCompleted();
				}
				this._sources = Enumerable.ToList<IContentSource>(sources);
				this.ReloadAssetsIfSourceChanged(mode);
			}
			if (this._changeWatcher != null)
			{
				this._changeWatcher.UpdateSources(sources);
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public Asset<T> Request<T>(string assetName, AssetRequestMode mode = AssetRequestMode.ImmediateLoad) where T : class
		{
			this.ThrowIfDisposed();
			assetName = AssetPathHelper.CleanPath(assetName);
			object requestLock = this._requestLock;
			Asset<T> asset3;
			lock (requestLock)
			{
				Asset<T> asset = null;
				IAsset asset2;
				if (this._assets.TryGetValue(assetName, ref asset2))
				{
					asset = asset2 as Asset<T>;
				}
				if (asset == null)
				{
					asset = new Asset<T>(assetName);
					this._assets[assetName] = asset;
				}
				else if (asset.State != AssetState.NotLoaded)
				{
					return asset;
				}
				this.LoadAsset<T>(asset, mode);
				asset3 = asset;
			}
			return asset3;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000C53C File Offset: 0x0000A73C
		public void TransferCompletedAssets()
		{
			this.ThrowIfDisposed();
			object requestLock = this._requestLock;
			lock (requestLock)
			{
				this._asyncLoader.TransferCompleted();
			}
			this.UpdateWatchedAssetChanges();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000C590 File Offset: 0x0000A790
		public void EnableAssetWatcher()
		{
			this.ThrowIfDisposed();
			if (this._changeWatcher == null)
			{
				this._changeWatcher = new AssetChangeWatcher();
				this._changeWatcher.UpdateSources(this._sources);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		private void UpdateWatchedAssetChanges()
		{
			if (this._changeWatcher == null)
			{
				return;
			}
			foreach (FileChangeWatcher<IContentSource>.FileUpdate fileUpdate in this._changeWatcher.GetUpdates())
			{
				if (!this.UpdateWatchedAsset(fileUpdate.Source, fileUpdate.Path, fileUpdate.FullPath) && this.ContentFileUpdatedHandler != null)
				{
					this.ContentFileUpdatedHandler(fileUpdate.Source, fileUpdate.Path, fileUpdate.FullPath);
				}
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000C634 File Offset: 0x0000A834
		private bool UpdateWatchedAsset(IContentSource source, string path, string fullPath)
		{
			string extension = Path.GetExtension(path);
			string text = path.Substring(0, path.Length - extension.Length);
			object requestLock = this._requestLock;
			bool flag2;
			lock (requestLock)
			{
				if (!source.HasAsset(text))
				{
					source.Refresh();
				}
				IAsset asset;
				if (!this._assets.TryGetValue(text, ref asset))
				{
					flag2 = false;
				}
				else if (!asset.IsLoaded)
				{
					flag2 = true;
				}
				else
				{
					IContentSource source2 = asset.Source;
					if (source != asset.Source && source != this.FindSourceForAsset(asset.Name))
					{
						flag2 = true;
					}
					else
					{
						try
						{
							if (!this.UpdateAssetValue(asset, source, extension, () => File.OpenRead(fullPath)))
							{
								return false;
							}
							if (this.AssetWatcherValueUpdatedHandler != null)
							{
								this.AssetWatcherValueUpdatedHandler(asset);
							}
						}
						catch (Exception ex)
						{
							if (this.AssetWatcherUpdateFailedHandler != null)
							{
								this.AssetWatcherUpdateFailedHandler(asset, ex);
							}
						}
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000C758 File Offset: 0x0000A958
		private void ReloadAssetsIfSourceChanged(AssetRequestMode mode)
		{
			foreach (IAsset asset2 in Enumerable.Where<IAsset>(this._assets.Values, (IAsset asset) => asset.IsLoaded))
			{
				IContentSource contentSource = this.FindSourceForAsset(asset2.Name);
				if (contentSource == null)
				{
					this.ForceReloadAsset(asset2, AssetRequestMode.DoNotLoad);
				}
				else if (asset2.Source != contentSource)
				{
					this.ForceReloadAsset(asset2, (asset2.State == AssetState.NotLoaded) ? AssetRequestMode.DoNotLoad : mode);
				}
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000C800 File Offset: 0x0000AA00
		private void LoadAsset<T>(Asset<T> asset, AssetRequestMode mode) where T : class
		{
			if (mode == AssetRequestMode.DoNotLoad)
			{
				return;
			}
			this.EnsureTypeSpecificActionsExist<T>();
			int totalAssets = this.TotalAssets;
			this.TotalAssets = totalAssets + 1;
			asset.SetToLoadingState();
			try
			{
				this.TryLoadingAsset<T>(asset, mode);
			}
			catch (Exception ex)
			{
				if (this.AssetLoadFailHandler != null)
				{
					this.AssetLoadFailHandler(asset.Name, ex);
				}
				throw;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000C864 File Offset: 0x0000AA64
		private void TryLoadingAsset<T>(Asset<T> asset, AssetRequestMode mode) where T : class
		{
			IContentSource source = this.FindSourceForAsset(asset.Name);
			AssetRequestMode mode2 = mode;
			if (mode2 == AssetRequestMode.ImmediateLoad)
			{
				bool flag = true;
				IRejectionReason rejectionReason = null;
				T t;
				try
				{
					if (!this._loader.TryLoad<T>(asset.Name, source, out t))
					{
						source.RejectAsset(asset.Name, new ContentRejectionFromFailedLoad_ByAssetExtensionType());
						this.TryLoadingAsset<T>(asset, mode);
						return;
					}
				}
				catch (Exception ex)
				{
					bool flag2 = false;
					if (ex is UnauthorizedAccessException)
					{
						flag2 = true;
					}
					if (ex is FileNotFoundException)
					{
						flag2 = true;
					}
					if (ex is DirectoryNotFoundException)
					{
						flag2 = true;
					}
					if (ex is AssetLoadException)
					{
						flag2 = true;
					}
					if (flag2)
					{
						throw;
					}
					throw AssetLoadException.FromAssetException(asset.Name, ex);
				}
				if (source.ContentValidator != null)
				{
					flag = source.ContentValidator.AssetIsValid<T>(t, asset.Name, out rejectionReason);
				}
				if (flag)
				{
					this.SubmitLoadedContent<T>(asset, t, source);
					int loadedAssets = this.LoadedAssets;
					this.LoadedAssets = loadedAssets + 1;
					return;
				}
				source.RejectAsset(asset.Name, rejectionReason);
				this.TryLoadingAsset<T>(asset, mode);
				return;
			}
			if (mode2 != AssetRequestMode.AsyncLoad)
			{
				throw new ArgumentOutOfRangeException("mode", mode, null);
			}
			this._asyncLoader.Load<T>(asset.Name, source, delegate(bool proper, T content)
			{
				if (!proper)
				{
					source.RejectAsset(asset.Name, new ContentRejectionFromFailedLoad_ByAssetExtensionType());
					this.TryLoadingAsset<T>(asset, mode);
					return;
				}
				bool flag3 = true;
				IRejectionReason rejectionReason2 = null;
				if (source.ContentValidator != null)
				{
					flag3 = source.ContentValidator.AssetIsValid<T>(content, asset.Name, out rejectionReason2);
				}
				if (flag3)
				{
					this.SubmitLoadedContent<T>(asset, content, source);
					int loadedAssets2 = this.LoadedAssets;
					this.LoadedAssets = loadedAssets2 + 1;
					return;
				}
				source.RejectAsset(asset.Name, rejectionReason2);
				this.TryLoadingAsset<T>(asset, mode);
			});
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000CA34 File Offset: 0x0000AC34
		private void ForceReloadAsset(IAsset asset, AssetRequestMode mode)
		{
			if (mode != AssetRequestMode.DoNotLoad)
			{
				int loadedAssets = this.LoadedAssets;
				this.LoadedAssets = loadedAssets - 1;
			}
			this._typeSpecificReloadActions[asset.GetType()].Invoke(asset, mode);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000CA6C File Offset: 0x0000AC6C
		private void EnsureTypeSpecificActionsExist<T>() where T : class
		{
			this._typeSpecificReloadActions[typeof(Asset<T>)] = new Action<IAsset, AssetRequestMode>(this.ForceReloadAsset<T>);
			this._typeSpecificUpdateActions[typeof(Asset<T>)] = new Func<IAsset, IContentSource, string, Func<Stream>, bool>(this.UpdateAssetValue<T>);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000CABC File Offset: 0x0000ACBC
		private void ForceReloadAsset<T>(IAsset asset, AssetRequestMode mode) where T : class
		{
			Asset<T> asset2 = (Asset<T>)asset;
			asset2.Unload();
			this.LoadAsset<T>(asset2, mode);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000CADE File Offset: 0x0000ACDE
		private bool UpdateAssetValue(IAsset asset, IContentSource source, string extension, Func<Stream> getStream)
		{
			return this._typeSpecificUpdateActions[asset.GetType()].Invoke(asset, source, extension, getStream);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000CAFC File Offset: 0x0000ACFC
		private bool UpdateAssetValue<T>(IAsset asset, IContentSource source, string extension, Func<Stream> getStream) where T : class
		{
			T t;
			if (!this._loader.TryLoad<T>(extension, getStream, out t))
			{
				return false;
			}
			IContentValidator contentValidator = source.ContentValidator;
			IRejectionReason rejectionReason;
			if (contentValidator != null && !contentValidator.AssetIsValid<T>(t, asset.Name, out rejectionReason))
			{
				throw AssetLoadException.RejectedByValidator(rejectionReason);
			}
			this.SubmitLoadedContent<T>((Asset<T>)asset, t, source);
			return true;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000CB4E File Offset: 0x0000AD4E
		private void SubmitLoadedContent<T>(Asset<T> asset, T value, IContentSource source) where T : class
		{
			asset.SubmitLoadedContent(value, source);
			if (this.AssetValueUpdatedHandler != null)
			{
				this.AssetValueUpdatedHandler(asset, value);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000CB74 File Offset: 0x0000AD74
		private IContentSource FindSourceForAsset(string assetName)
		{
			IContentSource contentSource = Enumerable.FirstOrDefault<IContentSource>(this._sources, (IContentSource source) => source.HasAsset(assetName));
			if (contentSource == null)
			{
				throw AssetLoadException.FromMissingAsset(assetName, null);
			}
			return contentSource;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000CBB5 File Offset: 0x0000ADB5
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException("AssetRepository is disposed.");
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		protected virtual void Dispose(bool disposing)
		{
			if (this._isDisposed)
			{
				return;
			}
			if (disposing)
			{
				this._asyncLoader.Dispose();
				foreach (KeyValuePair<string, IAsset> keyValuePair in this._assets)
				{
					keyValuePair.Value.Dispose();
				}
				if (this._changeWatcher != null)
				{
					this._changeWatcher.Dispose();
				}
			}
			this._isDisposed = true;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000CC58 File Offset: 0x0000AE58
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000506 RID: 1286
		[CompilerGenerated]
		private int <TotalAssets>k__BackingField;

		// Token: 0x04000507 RID: 1287
		[CompilerGenerated]
		private int <LoadedAssets>k__BackingField;

		// Token: 0x04000508 RID: 1288
		private readonly Dictionary<string, IAsset> _assets = new Dictionary<string, IAsset>();

		// Token: 0x04000509 RID: 1289
		private IEnumerable<IContentSource> _sources = new IContentSource[0];

		// Token: 0x0400050A RID: 1290
		private readonly Dictionary<Type, Action<IAsset, AssetRequestMode>> _typeSpecificReloadActions = new Dictionary<Type, Action<IAsset, AssetRequestMode>>();

		// Token: 0x0400050B RID: 1291
		private readonly Dictionary<Type, Func<IAsset, IContentSource, string, Func<Stream>, bool>> _typeSpecificUpdateActions = new Dictionary<Type, Func<IAsset, IContentSource, string, Func<Stream>, bool>>();

		// Token: 0x0400050C RID: 1292
		private readonly IAsyncAssetLoader _asyncLoader;

		// Token: 0x0400050D RID: 1293
		private readonly IAssetLoader _loader;

		// Token: 0x0400050E RID: 1294
		private AssetChangeWatcher _changeWatcher;

		// Token: 0x0400050F RID: 1295
		private readonly object _requestLock = new object();

		// Token: 0x04000510 RID: 1296
		[CompilerGenerated]
		private AssetValueUpdated <AssetValueUpdatedHandler>k__BackingField;

		// Token: 0x04000511 RID: 1297
		[CompilerGenerated]
		private FailedToLoadAssetCustomAction <AssetLoadFailHandler>k__BackingField;

		// Token: 0x04000512 RID: 1298
		[CompilerGenerated]
		private AssetWatcherValueUpdated <AssetWatcherValueUpdatedHandler>k__BackingField;

		// Token: 0x04000513 RID: 1299
		[CompilerGenerated]
		private AssetWatcherUpdateFailed <AssetWatcherUpdateFailedHandler>k__BackingField;

		// Token: 0x04000514 RID: 1300
		[CompilerGenerated]
		private ContentFileUpdated <ContentFileUpdatedHandler>k__BackingField;

		// Token: 0x04000515 RID: 1301
		private bool _isDisposed;

		// Token: 0x020000E9 RID: 233
		[CompilerGenerated]
		private sealed class <>c__DisplayClass47_0
		{
			// Token: 0x0600047A RID: 1146 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass47_0()
			{
			}

			// Token: 0x0600047B RID: 1147 RVA: 0x0000E657 File Offset: 0x0000C857
			internal Stream <UpdateWatchedAsset>b__0()
			{
				return File.OpenRead(this.fullPath);
			}

			// Token: 0x0400061B RID: 1563
			public string fullPath;
		}

		// Token: 0x020000EA RID: 234
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600047C RID: 1148 RVA: 0x0000E664 File Offset: 0x0000C864
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c()
			{
			}

			// Token: 0x0600047E RID: 1150 RVA: 0x0000E670 File Offset: 0x0000C870
			internal bool <ReloadAssetsIfSourceChanged>b__48_0(IAsset asset)
			{
				return asset.IsLoaded;
			}

			// Token: 0x0400061C RID: 1564
			public static readonly AssetRepository.<>c <>9 = new AssetRepository.<>c();

			// Token: 0x0400061D RID: 1565
			public static Func<IAsset, bool> <>9__48_0;
		}

		// Token: 0x020000EB RID: 235
		[CompilerGenerated]
		private sealed class <>c__DisplayClass50_0<T> where T : class
		{
			// Token: 0x0600047F RID: 1151 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass50_0()
			{
			}

			// Token: 0x06000480 RID: 1152 RVA: 0x0000E678 File Offset: 0x0000C878
			internal void <TryLoadingAsset>b__0(bool proper, T content)
			{
				if (!proper)
				{
					this.source.RejectAsset(this.asset.Name, new ContentRejectionFromFailedLoad_ByAssetExtensionType());
					this.<>4__this.TryLoadingAsset<T>(this.asset, this.mode);
					return;
				}
				bool flag = true;
				IRejectionReason rejectionReason = null;
				if (this.source.ContentValidator != null)
				{
					flag = this.source.ContentValidator.AssetIsValid<T>(content, this.asset.Name, out rejectionReason);
				}
				if (flag)
				{
					this.<>4__this.SubmitLoadedContent<T>(this.asset, content, this.source);
					int loadedAssets = this.<>4__this.LoadedAssets;
					this.<>4__this.LoadedAssets = loadedAssets + 1;
					return;
				}
				this.source.RejectAsset(this.asset.Name, rejectionReason);
				this.<>4__this.TryLoadingAsset<T>(this.asset, this.mode);
			}

			// Token: 0x0400061E RID: 1566
			public IContentSource source;

			// Token: 0x0400061F RID: 1567
			public Asset<T> asset;

			// Token: 0x04000620 RID: 1568
			public AssetRepository <>4__this;

			// Token: 0x04000621 RID: 1569
			public AssetRequestMode mode;
		}

		// Token: 0x020000EC RID: 236
		[CompilerGenerated]
		private sealed class <>c__DisplayClass57_0
		{
			// Token: 0x06000481 RID: 1153 RVA: 0x0000448A File Offset: 0x0000268A
			public <>c__DisplayClass57_0()
			{
			}

			// Token: 0x06000482 RID: 1154 RVA: 0x0000E74F File Offset: 0x0000C94F
			internal bool <FindSourceForAsset>b__0(IContentSource source)
			{
				return source.HasAsset(this.assetName);
			}

			// Token: 0x04000622 RID: 1570
			public string assetName;
		}
	}
}
