using System;
using ReLogic.Content.Sources;

namespace ReLogic.Content
{
	// Token: 0x0200009D RID: 157
	public interface IAsyncAssetLoader : IDisposable
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003A0 RID: 928
		bool IsRunning { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003A1 RID: 929
		int Remaining { get; }

		// Token: 0x060003A2 RID: 930
		void Start();

		// Token: 0x060003A3 RID: 931
		void Stop();

		// Token: 0x060003A4 RID: 932
		void Load<T>(string assetName, IContentSource source, LoadAssetDelegate<T> callback) where T : class;

		// Token: 0x060003A5 RID: 933
		void TransferCompleted();
	}
}
