using System;
using System.IO;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x02000442 RID: 1090
	public struct ParticleOrchestraSettings
	{
		// Token: 0x06003148 RID: 12616 RVA: 0x005C9806 File Offset: 0x005C7A06
		public void Serialize(BinaryWriter writer)
		{
			writer.WriteVector2(this.PositionInWorld);
			writer.WriteVector2(this.MovementVector);
			writer.Write(this.UniqueInfoPiece);
			writer.Write(this.IndexOfPlayerWhoInvokedThis);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x005C9838 File Offset: 0x005C7A38
		public void DeserializeFrom(BinaryReader reader)
		{
			this.PositionInWorld = reader.ReadVector2();
			this.MovementVector = reader.ReadVector2();
			this.UniqueInfoPiece = reader.ReadInt32();
			this.IndexOfPlayerWhoInvokedThis = reader.ReadByte();
		}

		// Token: 0x04005774 RID: 22388
		public Vector2 PositionInWorld;

		// Token: 0x04005775 RID: 22389
		public Vector2 MovementVector;

		// Token: 0x04005776 RID: 22390
		public int UniqueInfoPiece;

		// Token: 0x04005777 RID: 22391
		public byte IndexOfPlayerWhoInvokedThis;
	}
}
