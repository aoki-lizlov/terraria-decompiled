using System;
using ReLogic.Content;
using Terraria.Localization;

namespace Terraria.GameContent
{
	// Token: 0x02000271 RID: 625
	public class ContentRejectionFromSize : IRejectionReason
	{
		// Token: 0x06002420 RID: 9248 RVA: 0x0054AA11 File Offset: 0x00548C11
		public ContentRejectionFromSize(int neededWidth, int neededHeight, int actualWidth, int actualHeight)
		{
			this._neededWidth = neededWidth;
			this._neededHeight = neededHeight;
			this._actualWidth = actualWidth;
			this._actualHeight = actualHeight;
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x0054AA36 File Offset: 0x00548C36
		public string GetReason()
		{
			return Language.GetTextValueWith("AssetRejections.BadSize", new
			{
				NeededWidth = this._neededWidth,
				NeededHeight = this._neededHeight,
				ActualWidth = this._actualWidth,
				ActualHeight = this._actualHeight
			});
		}

		// Token: 0x04004DD7 RID: 19927
		private int _neededWidth;

		// Token: 0x04004DD8 RID: 19928
		private int _neededHeight;

		// Token: 0x04004DD9 RID: 19929
		private int _actualWidth;

		// Token: 0x04004DDA RID: 19930
		private int _actualHeight;
	}
}
