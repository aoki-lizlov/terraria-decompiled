using System;
using System.Runtime.CompilerServices;

namespace SteelSeries.GameSense
{
	// Token: 0x02000048 RID: 72
	public class JsonMsg : QueueMsg
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00008981 File Offset: 0x00006B81
		public override Uri uri
		{
			get
			{
				return QueueMsgSendEvent._uri;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00008988 File Offset: 0x00006B88
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00008990 File Offset: 0x00006B90
		public override object data
		{
			[CompilerGenerated]
			get
			{
				return this.<data>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<data>k__BackingField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00008999 File Offset: 0x00006B99
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000089A1 File Offset: 0x00006BA1
		public string JsonText
		{
			[CompilerGenerated]
			get
			{
				return this.<JsonText>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<JsonText>k__BackingField = value;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000059C8 File Offset: 0x00003BC8
		public override bool IsCritical()
		{
			return false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000089AA File Offset: 0x00006BAA
		public JsonMsg()
		{
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000040C8 File Offset: 0x000022C8
		// Note: this type is marked as 'beforefieldinit'.
		static JsonMsg()
		{
		}

		// Token: 0x04000150 RID: 336
		public static Uri _bitmapEventUri;

		// Token: 0x04000151 RID: 337
		[CompilerGenerated]
		private object <data>k__BackingField;

		// Token: 0x04000152 RID: 338
		[CompilerGenerated]
		private string <JsonText>k__BackingField;
	}
}
