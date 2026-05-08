using System;

namespace NVorbis.Contracts
{
	// Token: 0x0200002E RID: 46
	public interface IPacket
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001D1 RID: 465
		bool IsResync { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001D2 RID: 466
		bool IsShort { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001D3 RID: 467
		long? GranulePosition { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001D4 RID: 468
		bool IsEndOfStream { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001D5 RID: 469
		int BitsRead { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001D6 RID: 470
		int BitsRemaining { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001D7 RID: 471
		int ContainerOverheadBits { get; }

		// Token: 0x060001D8 RID: 472
		ulong TryPeekBits(int count, out int bitsRead);

		// Token: 0x060001D9 RID: 473
		void SkipBits(int count);

		// Token: 0x060001DA RID: 474
		ulong ReadBits(int count);

		// Token: 0x060001DB RID: 475
		void Done();

		// Token: 0x060001DC RID: 476
		void Reset();
	}
}
