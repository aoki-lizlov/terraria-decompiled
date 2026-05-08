using System;
using System.IO;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x02000459 RID: 1113
	public class NormalButterflyLeashedCritter : FlyLeashedCritter
	{
		// Token: 0x0600327C RID: 12924 RVA: 0x005F03C6 File Offset: 0x005EE5C6
		protected override void SetDefaults(Item sample)
		{
			base.SetDefaults(sample);
			this.variant = (byte)sample.placeStyle;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x005F03DC File Offset: 0x005EE5DC
		protected override void CopyToDummy()
		{
			base.CopyToDummy();
			LeashedCritter._dummy.ai[2] = (float)this.variant;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x005F03F7 File Offset: 0x005EE5F7
		public override void NetSend(BinaryWriter writer, bool full)
		{
			base.NetSend(writer, full);
			if (full)
			{
				writer.Write(this.variant);
			}
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x005F0410 File Offset: 0x005EE610
		public override void NetReceive(BinaryReader reader, bool full)
		{
			base.NetReceive(reader, full);
			if (full)
			{
				this.variant = reader.ReadByte();
			}
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x005F0429 File Offset: 0x005EE629
		public NormalButterflyLeashedCritter()
		{
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x005F0431 File Offset: 0x005EE631
		// Note: this type is marked as 'beforefieldinit'.
		static NormalButterflyLeashedCritter()
		{
		}

		// Token: 0x04005829 RID: 22569
		public new static NormalButterflyLeashedCritter Prototype = new NormalButterflyLeashedCritter();

		// Token: 0x0400582A RID: 22570
		protected byte variant;
	}
}
