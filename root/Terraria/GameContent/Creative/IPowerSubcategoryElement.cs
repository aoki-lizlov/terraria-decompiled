using System;
using Terraria.GameContent.UI.Elements;

namespace Terraria.GameContent.Creative
{
	// Token: 0x02000327 RID: 807
	public interface IPowerSubcategoryElement
	{
		// Token: 0x060027CD RID: 10189
		GroupOptionButton<int> GetOptionButton(CreativePowerUIElementRequestInfo info, int optionIndex, int currentOptionIndex);
	}
}
