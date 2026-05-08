using System;

namespace Terraria.DataStructures
{
	// Token: 0x0200052F RID: 1327
	public interface IRoomCheckFeedback_Spread
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060036F7 RID: 14071
		bool StopOnFail { get; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060036F8 RID: 14072
		bool DisplayText { get; }

		// Token: 0x060036F9 RID: 14073
		void BeginSpread(int x, int y);

		// Token: 0x060036FA RID: 14074
		void StartedInASolidTile(int x, int y);

		// Token: 0x060036FB RID: 14075
		void TooCloseToWorldEdge(int x, int y, int iteration);

		// Token: 0x060036FC RID: 14076
		void AnyBlockScannedHere(int x, int y, int iteration);

		// Token: 0x060036FD RID: 14077
		void RoomTooBig(int x, int y, int iteration);

		// Token: 0x060036FE RID: 14078
		void BlockingWall(int x, int y, int iteration);

		// Token: 0x060036FF RID: 14079
		void BlockingOpenGate(int x, int y, int iteration);

		// Token: 0x06003700 RID: 14080
		void Stinkbug(int x, int y, int iteration);

		// Token: 0x06003701 RID: 14081
		void EchoStinkbug(int x, int y, int iteration);

		// Token: 0x06003702 RID: 14082
		void MissingAWall(int x, int y, int iteration);

		// Token: 0x06003703 RID: 14083
		void UnsafeWall(int x, int y, int iteration);

		// Token: 0x06003704 RID: 14084
		void EndSpread();
	}
}
