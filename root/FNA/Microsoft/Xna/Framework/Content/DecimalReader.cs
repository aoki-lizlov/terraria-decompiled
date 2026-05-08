using System;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000113 RID: 275
	internal class DecimalReader : ContentTypeReader<decimal>
	{
		// Token: 0x0600172F RID: 5935 RVA: 0x00038EDD File Offset: 0x000370DD
		internal DecimalReader()
		{
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x00038EE5 File Offset: 0x000370E5
		protected internal override decimal Read(ContentReader input, decimal existingInstance)
		{
			return input.ReadDecimal();
		}
	}
}
