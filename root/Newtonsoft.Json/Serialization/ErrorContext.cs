using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008F RID: 143
	public class ErrorContext
	{
		// Token: 0x06000694 RID: 1684 RVA: 0x0001B750 File Offset: 0x00019950
		internal ErrorContext(object originalObject, object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001B775 File Offset: 0x00019975
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0001B77D File Offset: 0x0001997D
		internal bool Traced
		{
			[CompilerGenerated]
			get
			{
				return this.<Traced>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Traced>k__BackingField = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001B786 File Offset: 0x00019986
		public Exception Error
		{
			[CompilerGenerated]
			get
			{
				return this.<Error>k__BackingField;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001B78E File Offset: 0x0001998E
		public object OriginalObject
		{
			[CompilerGenerated]
			get
			{
				return this.<OriginalObject>k__BackingField;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001B796 File Offset: 0x00019996
		public object Member
		{
			[CompilerGenerated]
			get
			{
				return this.<Member>k__BackingField;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001B79E File Offset: 0x0001999E
		public string Path
		{
			[CompilerGenerated]
			get
			{
				return this.<Path>k__BackingField;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001B7A6 File Offset: 0x000199A6
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001B7AE File Offset: 0x000199AE
		public bool Handled
		{
			[CompilerGenerated]
			get
			{
				return this.<Handled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Handled>k__BackingField = value;
			}
		}

		// Token: 0x040002A1 RID: 673
		[CompilerGenerated]
		private bool <Traced>k__BackingField;

		// Token: 0x040002A2 RID: 674
		[CompilerGenerated]
		private readonly Exception <Error>k__BackingField;

		// Token: 0x040002A3 RID: 675
		[CompilerGenerated]
		private readonly object <OriginalObject>k__BackingField;

		// Token: 0x040002A4 RID: 676
		[CompilerGenerated]
		private readonly object <Member>k__BackingField;

		// Token: 0x040002A5 RID: 677
		[CompilerGenerated]
		private readonly string <Path>k__BackingField;

		// Token: 0x040002A6 RID: 678
		[CompilerGenerated]
		private bool <Handled>k__BackingField;
	}
}
