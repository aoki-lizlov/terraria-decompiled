using System;
using System.IO;

namespace Terraria.DataStructures
{
	// Token: 0x02000548 RID: 1352
	public struct BinaryWriterHelper
	{
		// Token: 0x0600377E RID: 14206 RVA: 0x0062F54D File Offset: 0x0062D74D
		public void ReservePointToFillLengthLaterByFilling6Bytes(BinaryWriter writer)
		{
			this._placeInWriter = writer.BaseStream.Position;
			writer.Write(0U);
			writer.Write(0);
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x0062F570 File Offset: 0x0062D770
		public void FillReservedPoint(BinaryWriter writer, ushort dataId)
		{
			long position = writer.BaseStream.Position;
			writer.BaseStream.Position = this._placeInWriter;
			long num = position - this._placeInWriter - 4L;
			writer.Write((int)num);
			writer.Write(dataId);
			writer.BaseStream.Position = position;
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x0062F5C4 File Offset: 0x0062D7C4
		public void FillOnlyIfThereIsLengthOrRevertToSavedPosition(BinaryWriter writer, ushort dataId, out bool wroteSomething)
		{
			wroteSomething = false;
			long position = writer.BaseStream.Position;
			writer.BaseStream.Position = this._placeInWriter;
			long num = position - this._placeInWriter - 4L;
			if (num == 0L)
			{
				return;
			}
			writer.Write((int)num);
			writer.Write(dataId);
			writer.BaseStream.Position = position;
			wroteSomething = true;
		}

		// Token: 0x04005BB2 RID: 23474
		private long _placeInWriter;
	}
}
