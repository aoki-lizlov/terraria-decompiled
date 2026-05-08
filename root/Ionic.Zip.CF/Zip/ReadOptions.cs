using System;
using System.IO;
using System.Text;

namespace Ionic.Zip
{
	// Token: 0x02000029 RID: 41
	public class ReadOptions
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000C11B File Offset: 0x0000A31B
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000C123 File Offset: 0x0000A323
		public EventHandler<ReadProgressEventArgs> ReadProgress
		{
			get
			{
				return this.<ReadProgress>k__BackingField;
			}
			set
			{
				this.<ReadProgress>k__BackingField = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C12C File Offset: 0x0000A32C
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000C134 File Offset: 0x0000A334
		public TextWriter StatusMessageWriter
		{
			get
			{
				return this.<StatusMessageWriter>k__BackingField;
			}
			set
			{
				this.<StatusMessageWriter>k__BackingField = value;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C13D File Offset: 0x0000A33D
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000C145 File Offset: 0x0000A345
		public Encoding Encoding
		{
			get
			{
				return this.<Encoding>k__BackingField;
			}
			set
			{
				this.<Encoding>k__BackingField = value;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C14E File Offset: 0x0000A34E
		public ReadOptions()
		{
		}

		// Token: 0x040000FD RID: 253
		private EventHandler<ReadProgressEventArgs> <ReadProgress>k__BackingField;

		// Token: 0x040000FE RID: 254
		private TextWriter <StatusMessageWriter>k__BackingField;

		// Token: 0x040000FF RID: 255
		private Encoding <Encoding>k__BackingField;
	}
}
