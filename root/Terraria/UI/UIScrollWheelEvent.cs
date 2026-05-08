using System;
using Microsoft.Xna.Framework;

namespace Terraria.UI
{
	// Token: 0x02000100 RID: 256
	public class UIScrollWheelEvent : UIMouseEvent
	{
		// Token: 0x06001A28 RID: 6696 RVA: 0x004F4F82 File Offset: 0x004F3182
		public UIScrollWheelEvent(UIElement target, Vector2 mousePosition, int scrollWheelValue)
			: base(target, mousePosition)
		{
			this.ScrollWheelValue = scrollWheelValue;
		}

		// Token: 0x0400139C RID: 5020
		public readonly int ScrollWheelValue;
	}
}
