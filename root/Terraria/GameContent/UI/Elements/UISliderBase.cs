using System;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x020003EF RID: 1007
	public class UISliderBase : UIElement
	{
		// Token: 0x06002E9B RID: 11931 RVA: 0x005ABD88 File Offset: 0x005A9F88
		internal int GetUsageLevel()
		{
			int num = 0;
			if (UISliderBase.CurrentLockedSlider == this)
			{
				num = 1;
			}
			else if (UISliderBase.CurrentLockedSlider != null)
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x005ABDAD File Offset: 0x005A9FAD
		public static void EscapeElements()
		{
			UISliderBase.CurrentLockedSlider = null;
			UISliderBase.CurrentAimedSlider = null;
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x005A2DAD File Offset: 0x005A0FAD
		public UISliderBase()
		{
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static UISliderBase()
		{
		}

		// Token: 0x040055B9 RID: 21945
		internal const int UsageLevel_NotSelected = 0;

		// Token: 0x040055BA RID: 21946
		internal const int UsageLevel_SelectedAndLocked = 1;

		// Token: 0x040055BB RID: 21947
		internal const int UsageLevel_OtherElementIsLocked = 2;

		// Token: 0x040055BC RID: 21948
		public static UIElement CurrentLockedSlider;

		// Token: 0x040055BD RID: 21949
		public static UIElement CurrentAimedSlider;
	}
}
