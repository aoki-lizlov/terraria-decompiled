using System;

namespace ReLogic.Content
{
	// Token: 0x0200009E RID: 158
	// (Invoke) Token: 0x060003A7 RID: 935
	public delegate void LoadAssetDelegate<T>(bool loadedSuccessfully, T theAsset) where T : class;
}
