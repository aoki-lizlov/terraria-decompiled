using System;
using System.Collections.Generic;
using System.IO;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000326 RID: 806
	public interface ICreativePower
	{
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060027C2 RID: 10178
		// (set) Token: 0x060027C3 RID: 10179
		ushort PowerId { get; set; }

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060027C4 RID: 10180
		// (set) Token: 0x060027C5 RID: 10181
		string ServerConfigName { get; set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060027C6 RID: 10182
		// (set) Token: 0x060027C7 RID: 10183
		PowerPermissionLevel CurrentPermissionLevel { get; set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060027C8 RID: 10184
		// (set) Token: 0x060027C9 RID: 10185
		PowerPermissionLevel DefaultPermissionLevel { get; set; }

		// Token: 0x060027CA RID: 10186
		void DeserializeNetMessage(BinaryReader reader, int userId);

		// Token: 0x060027CB RID: 10187
		void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements);

		// Token: 0x060027CC RID: 10188
		bool GetIsUnlocked();
	}
}
