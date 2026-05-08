using System;

namespace NVorbis.Contracts
{
	// Token: 0x02000033 RID: 51
	public interface IStreamStats
	{
		// Token: 0x060001FD RID: 509
		void ResetStats();

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001FE RID: 510
		int EffectiveBitRate { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001FF RID: 511
		int InstantBitRate { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000200 RID: 512
		long ContainerBits { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000201 RID: 513
		long OverheadBits { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000202 RID: 514
		long AudioBits { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000203 RID: 515
		long WasteBits { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000204 RID: 516
		int PacketCount { get; }
	}
}
