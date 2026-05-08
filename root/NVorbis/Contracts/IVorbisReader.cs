using System;
using System.Collections.Generic;
using System.IO;

namespace NVorbis.Contracts
{
	// Token: 0x02000035 RID: 53
	public interface IVorbisReader : IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000218 RID: 536
		// (remove) Token: 0x06000219 RID: 537
		event EventHandler<NewStreamEventArgs> NewStream;

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600021A RID: 538
		long ContainerOverheadBits { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600021B RID: 539
		long ContainerWasteBits { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600021C RID: 540
		IList<IStreamDecoder> Streams { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600021D RID: 541
		int StreamIndex { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600021E RID: 542
		int Channels { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600021F RID: 543
		int SampleRate { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000220 RID: 544
		int UpperBitrate { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000221 RID: 545
		int NominalBitrate { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000222 RID: 546
		int LowerBitrate { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000223 RID: 547
		TimeSpan TotalTime { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000224 RID: 548
		long TotalSamples { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000225 RID: 549
		// (set) Token: 0x06000226 RID: 550
		bool ClipSamples { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000227 RID: 551
		// (set) Token: 0x06000228 RID: 552
		TimeSpan TimePosition { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000229 RID: 553
		// (set) Token: 0x0600022A RID: 554
		long SamplePosition { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600022B RID: 555
		bool HasClipped { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600022C RID: 556
		bool IsEndOfStream { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600022D RID: 557
		IStreamStats StreamStats { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600022E RID: 558
		ITagData Tags { get; }

		// Token: 0x0600022F RID: 559
		bool FindNextStream();

		// Token: 0x06000230 RID: 560
		bool SwitchStreams(int index);

		// Token: 0x06000231 RID: 561
		int ReadSamples(float[] buffer, int offset, int count);

		// Token: 0x06000232 RID: 562
		void SeekTo(TimeSpan timePosition, SeekOrigin seekOrigin = 0);

		// Token: 0x06000233 RID: 563
		void SeekTo(long samplePosition, SeekOrigin seekOrigin = 0);
	}
}
