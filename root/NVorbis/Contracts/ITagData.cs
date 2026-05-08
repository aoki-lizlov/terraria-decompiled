using System;
using System.Collections.Generic;

namespace NVorbis.Contracts
{
	// Token: 0x02000034 RID: 52
	public interface ITagData
	{
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000205 RID: 517
		IDictionary<string, IList<string>> All { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000206 RID: 518
		string EncoderVendor { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000207 RID: 519
		string Title { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000208 RID: 520
		string Version { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000209 RID: 521
		string Album { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600020A RID: 522
		string TrackNumber { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600020B RID: 523
		string Artist { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600020C RID: 524
		IList<string> Performers { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600020D RID: 525
		string Copyright { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600020E RID: 526
		string License { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600020F RID: 527
		string Organization { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000210 RID: 528
		string Description { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000211 RID: 529
		IList<string> Genres { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000212 RID: 530
		IList<string> Dates { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000213 RID: 531
		IList<string> Locations { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000214 RID: 532
		string Contact { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000215 RID: 533
		string Isrc { get; }

		// Token: 0x06000216 RID: 534
		string GetTagSingle(string key, bool concatenate = false);

		// Token: 0x06000217 RID: 535
		IList<string> GetTagMulti(string key);
	}
}
