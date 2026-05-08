using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000AB RID: 171
	public class JsonLoadSettings
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x00022DAC File Offset: 0x00020FAC
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00022DC2 File Offset: 0x00020FC2
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00022DCA File Offset: 0x00020FCA
		public CommentHandling CommentHandling
		{
			get
			{
				return this._commentHandling;
			}
			set
			{
				if (value < CommentHandling.Ignore || value > CommentHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._commentHandling = value;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00022DE6 File Offset: 0x00020FE6
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00022DEE File Offset: 0x00020FEE
		public LineInfoHandling LineInfoHandling
		{
			get
			{
				return this._lineInfoHandling;
			}
			set
			{
				if (value < LineInfoHandling.Ignore || value > LineInfoHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lineInfoHandling = value;
			}
		}

		// Token: 0x04000330 RID: 816
		private CommentHandling _commentHandling;

		// Token: 0x04000331 RID: 817
		private LineInfoHandling _lineInfoHandling;
	}
}
