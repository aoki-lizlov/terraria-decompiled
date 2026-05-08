using System;
using System.Runtime.CompilerServices;
using Terraria.GameInput;

namespace Terraria.GameContent.UI
{
	// Token: 0x02000367 RID: 871
	public class TextDisplayCache
	{
		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06002918 RID: 10520 RVA: 0x00577E05 File Offset: 0x00576005
		// (set) Token: 0x06002919 RID: 10521 RVA: 0x00577E0D File Offset: 0x0057600D
		public string[] TextLines
		{
			[CompilerGenerated]
			get
			{
				return this.<TextLines>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TextLines>k__BackingField = value;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600291A RID: 10522 RVA: 0x00577E16 File Offset: 0x00576016
		// (set) Token: 0x0600291B RID: 10523 RVA: 0x00577E1E File Offset: 0x0057601E
		public int AmountOfLines
		{
			[CompilerGenerated]
			get
			{
				return this.<AmountOfLines>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AmountOfLines>k__BackingField = value;
			}
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00577E28 File Offset: 0x00576028
		public void PrepareCache(string text)
		{
			if (!(false | (Main.screenWidth != this._lastScreenWidth) | (Main.screenHeight != this._lastScreenHeight) | (this._originalText != text) | (PlayerInput.CurrentInputMode != this._lastInputMode)))
			{
				return;
			}
			this._lastScreenWidth = Main.screenWidth;
			this._lastScreenHeight = Main.screenHeight;
			this._originalText = text;
			this._lastInputMode = PlayerInput.CurrentInputMode;
			int num;
			this.TextLines = Utils.WordwrapString(text, FontAssets.MouseText.Value, 460, 10, out num);
			this.AmountOfLines = num;
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0000357B File Offset: 0x0000177B
		public TextDisplayCache()
		{
		}

		// Token: 0x040051AF RID: 20911
		private string _originalText;

		// Token: 0x040051B0 RID: 20912
		private int _lastScreenWidth;

		// Token: 0x040051B1 RID: 20913
		private int _lastScreenHeight;

		// Token: 0x040051B2 RID: 20914
		private InputMode _lastInputMode;

		// Token: 0x040051B3 RID: 20915
		[CompilerGenerated]
		private string[] <TextLines>k__BackingField;

		// Token: 0x040051B4 RID: 20916
		[CompilerGenerated]
		private int <AmountOfLines>k__BackingField;
	}
}
