using System;
using System.IO;

namespace NVorbis.Contracts
{
	// Token: 0x02000032 RID: 50
	public interface IStreamDecoder : IDisposable
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001E9 RID: 489
		int Channels { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001EA RID: 490
		int SampleRate { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001EB RID: 491
		int UpperBitrate { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001EC RID: 492
		int NominalBitrate { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001ED RID: 493
		int LowerBitrate { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001EE RID: 494
		ITagData Tags { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001EF RID: 495
		TimeSpan TotalTime { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001F0 RID: 496
		long TotalSamples { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001F1 RID: 497
		// (set) Token: 0x060001F2 RID: 498
		TimeSpan TimePosition { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001F3 RID: 499
		// (set) Token: 0x060001F4 RID: 500
		long SamplePosition { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001F5 RID: 501
		// (set) Token: 0x060001F6 RID: 502
		bool ClipSamples { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001F7 RID: 503
		bool HasClipped { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001F8 RID: 504
		bool IsEndOfStream { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001F9 RID: 505
		IStreamStats Stats { get; }

		// Token: 0x060001FA RID: 506
		void SeekTo(TimeSpan timePosition, SeekOrigin seekOrigin = 0);

		// Token: 0x060001FB RID: 507
		void SeekTo(long samplePosition, SeekOrigin seekOrigin = 0);

		// Token: 0x060001FC RID: 508
		int Read(float[] buffer, int offset, int count);
	}
}
