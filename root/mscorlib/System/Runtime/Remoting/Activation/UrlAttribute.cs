using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x020005A6 RID: 1446
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlAttribute : ContextAttribute
	{
		// Token: 0x06003883 RID: 14467 RVA: 0x000CA96F File Offset: 0x000C8B6F
		public UrlAttribute(string callsiteURL)
			: base(callsiteURL)
		{
			this.url = callsiteURL;
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06003884 RID: 14468 RVA: 0x000CA97F File Offset: 0x000C8B7F
		public string UrlValue
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000CA987 File Offset: 0x000C8B87
		public override bool Equals(object o)
		{
			return o is UrlAttribute && ((UrlAttribute)o).UrlValue == this.url;
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000CA9A9 File Offset: 0x000C8BA9
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x00004088 File Offset: 0x00002288
		[ComVisible(true)]
		public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x00003FB7 File Offset: 0x000021B7
		[ComVisible(true)]
		public override bool IsContextOK(Context ctx, IConstructionCallMessage msg)
		{
			return true;
		}

		// Token: 0x04002581 RID: 9601
		private string url;
	}
}
