using System;

namespace ReLogic.Text
{
	// Token: 0x02000011 RID: 17
	public interface IFontMetrics
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AA RID: 170
		int LineSpacing { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000AB RID: 171
		float CharacterSpacing { get; }

		// Token: 0x060000AC RID: 172
		GlyphMetrics GetCharacterMetrics(char character);
	}
}
