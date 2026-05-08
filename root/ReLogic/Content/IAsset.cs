using System;
using ReLogic.Content.Sources;

namespace ReLogic.Content
{
	// Token: 0x02000096 RID: 150
	public interface IAsset : IDisposable
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000376 RID: 886
		AssetState State { get; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000377 RID: 887
		IContentSource Source { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000378 RID: 888
		string Name { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000379 RID: 889
		bool IsLoaded { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600037A RID: 890
		bool IsDisposed { get; }
	}
}
