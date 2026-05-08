using System;
using System.Runtime.CompilerServices;
using ReLogic.Content.Sources;

namespace ReLogic.Content
{
	// Token: 0x02000088 RID: 136
	public sealed class Asset<T> : IAsset, IDisposable where T : class
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000BC56 File Offset: 0x00009E56
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000BC5E File Offset: 0x00009E5E
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000BC67 File Offset: 0x00009E67
		public bool IsLoaded
		{
			get
			{
				return this.State == AssetState.Loaded;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0000BC72 File Offset: 0x00009E72
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0000BC7A File Offset: 0x00009E7A
		public AssetState State
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000BC83 File Offset: 0x00009E83
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0000BC8B File Offset: 0x00009E8B
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0000BC94 File Offset: 0x00009E94
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public IContentSource Source
		{
			[CompilerGenerated]
			get
			{
				return this.<Source>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Source>k__BackingField = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0000BCA5 File Offset: 0x00009EA5
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0000BCAD File Offset: 0x00009EAD
		public T Value
		{
			[CompilerGenerated]
			get
			{
				return this.<Value>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Value>k__BackingField = value;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000BCB6 File Offset: 0x00009EB6
		internal Asset(string name)
		{
			this.State = AssetState.NotLoaded;
			this.Name = name;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000BCCC File Offset: 0x00009ECC
		public static explicit operator T(Asset<T> asset)
		{
			return asset.Value;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		internal void Unload()
		{
			IDisposable disposable = this.Value as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.State = AssetState.NotLoaded;
			this.Value = default(T);
			this.Source = null;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000BD18 File Offset: 0x00009F18
		internal void SubmitLoadedContent(T value, IContentSource source)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			IDisposable disposable = this.Value as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			this.State = AssetState.Loaded;
			this.Value = value;
			this.Source = source;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000BD75 File Offset: 0x00009F75
		internal void SetToLoadingState()
		{
			this.State = AssetState.Loading;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000BD80 File Offset: 0x00009F80
		private void Dispose(bool disposing)
		{
			if (this.IsDisposed)
			{
				return;
			}
			if (disposing && this.Value != null)
			{
				IDisposable disposable = this.Value as IDisposable;
				if (this.IsLoaded && disposable != null)
				{
					disposable.Dispose();
				}
				this.Value = default(T);
			}
			this.IsDisposed = true;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000BDDE File Offset: 0x00009FDE
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000BDE7 File Offset: 0x00009FE7
		// Note: this type is marked as 'beforefieldinit'.
		static Asset()
		{
		}

		// Token: 0x040004F4 RID: 1268
		public static readonly Asset<T> Empty = new Asset<T>("");

		// Token: 0x040004F5 RID: 1269
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040004F6 RID: 1270
		[CompilerGenerated]
		private AssetState <State>k__BackingField;

		// Token: 0x040004F7 RID: 1271
		[CompilerGenerated]
		private bool <IsDisposed>k__BackingField;

		// Token: 0x040004F8 RID: 1272
		[CompilerGenerated]
		private IContentSource <Source>k__BackingField;

		// Token: 0x040004F9 RID: 1273
		[CompilerGenerated]
		private T <Value>k__BackingField;
	}
}
