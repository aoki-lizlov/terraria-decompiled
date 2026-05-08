using System;
using Terraria.UI;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020003B1 RID: 945
	public class UISortableElement : UIElement
	{
		// Token: 0x06002C59 RID: 11353 RVA: 0x00598340 File Offset: 0x00596540
		public UISortableElement(int index)
		{
			this.OrderIndex = index;
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x00598350 File Offset: 0x00596550
		public override int CompareTo(object obj)
		{
			UISortableElement uisortableElement = obj as UISortableElement;
			if (uisortableElement != null)
			{
				return this.OrderIndex.CompareTo(uisortableElement.OrderIndex);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x040053F2 RID: 21490
		public int OrderIndex;
	}
}
