using System;
using Terraria.Enums;

namespace Terraria.DataStructures
{
	// Token: 0x02000539 RID: 1337
	public struct AnchorData
	{
		// Token: 0x06003752 RID: 14162 RVA: 0x0062EBBA File Offset: 0x0062CDBA
		public AnchorData(AnchorType type, int count, int start)
		{
			this.type = type;
			this.tileCount = count;
			this.checkStart = start;
		}

		// Token: 0x06003753 RID: 14163 RVA: 0x0062EBD1 File Offset: 0x0062CDD1
		public static bool operator ==(AnchorData data1, AnchorData data2)
		{
			return data1.type == data2.type && data1.tileCount == data2.tileCount && data1.checkStart == data2.checkStart;
		}

		// Token: 0x06003754 RID: 14164 RVA: 0x0062EBFF File Offset: 0x0062CDFF
		public static bool operator !=(AnchorData data1, AnchorData data2)
		{
			return data1.type != data2.type || data1.tileCount != data2.tileCount || data1.checkStart != data2.checkStart;
		}

		// Token: 0x06003755 RID: 14165 RVA: 0x0062EC30 File Offset: 0x0062CE30
		public override bool Equals(object obj)
		{
			return obj is AnchorData && (this.type == ((AnchorData)obj).type && this.tileCount == ((AnchorData)obj).tileCount) && this.checkStart == ((AnchorData)obj).checkStart;
		}

		// Token: 0x06003756 RID: 14166 RVA: 0x0062EC84 File Offset: 0x0062CE84
		public override int GetHashCode()
		{
			byte b = (byte)this.checkStart;
			byte b2 = (byte)this.tileCount;
			return ((int)((ushort)this.type) << 16) | ((int)b2 << 8) | (int)b;
		}

		// Token: 0x06003757 RID: 14167 RVA: 0x00009E46 File Offset: 0x00008046
		// Note: this type is marked as 'beforefieldinit'.
		static AnchorData()
		{
		}

		// Token: 0x04005B8A RID: 23434
		public AnchorType type;

		// Token: 0x04005B8B RID: 23435
		public int tileCount;

		// Token: 0x04005B8C RID: 23436
		public int checkStart;

		// Token: 0x04005B8D RID: 23437
		public static AnchorData Empty;
	}
}
