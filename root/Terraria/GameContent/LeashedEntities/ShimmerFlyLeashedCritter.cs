using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.LeashedEntities
{
	// Token: 0x0200045D RID: 1117
	public class ShimmerFlyLeashedCritter : FlyLeashedCritter
	{
		// Token: 0x06003290 RID: 12944 RVA: 0x005F098E File Offset: 0x005EEB8E
		protected override void SetDefaults(Item sample)
		{
			base.SetDefaults(sample);
			if (Main.netMode == 0)
			{
				this.oldPositions = LeashedCritter._dummy.oldPos;
			}
			this.oldPositionsLength = (byte)LeashedCritter._dummy.oldPos.Length;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x005F09C1 File Offset: 0x005EEBC1
		public override void NetSend(BinaryWriter writer, bool full)
		{
			base.NetSend(writer, full);
			if (full)
			{
				writer.Write(this.oldPositionsLength);
			}
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x005F09DA File Offset: 0x005EEBDA
		public override void NetReceive(BinaryReader reader, bool full)
		{
			base.NetReceive(reader, full);
			if (full)
			{
				this.oldPositionsLength = reader.ReadByte();
				this.oldPositions = new Vector2[(int)this.oldPositionsLength];
			}
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x005F0A04 File Offset: 0x005EEC04
		protected override void VisualEffects()
		{
			base.VisualEffects();
			if (this.oldPositions == null)
			{
				return;
			}
			for (int i = this.oldPositions.Length - 1; i > 0; i--)
			{
				this.oldPositions[i] = this.oldPositions[i - 1];
			}
			this.oldPositions[0] = this.position + this.netOffset;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x005F0A6C File Offset: 0x005EEC6C
		public override void Draw()
		{
			Vector2[] oldPos = LeashedCritter._dummy.oldPos;
			LeashedCritter._dummy.oldPos = this.oldPositions;
			base.Draw();
			LeashedCritter._dummy.oldPos = oldPos;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x005F0429 File Offset: 0x005EE629
		public ShimmerFlyLeashedCritter()
		{
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x005F0AA5 File Offset: 0x005EECA5
		// Note: this type is marked as 'beforefieldinit'.
		static ShimmerFlyLeashedCritter()
		{
		}

		// Token: 0x04005832 RID: 22578
		public new static ShimmerFlyLeashedCritter Prototype = new ShimmerFlyLeashedCritter();

		// Token: 0x04005833 RID: 22579
		private byte oldPositionsLength;

		// Token: 0x04005834 RID: 22580
		private Vector2[] oldPositions;
	}
}
