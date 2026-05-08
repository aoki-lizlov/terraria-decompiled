using System;

namespace Mono.Xml
{
	// Token: 0x02000046 RID: 70
	internal class SmallXmlParserException : SystemException
	{
		// Token: 0x06000134 RID: 308 RVA: 0x000052A3 File Offset: 0x000034A3
		public SmallXmlParserException(string msg, int line, int column)
			: base(string.Format("{0}. At ({1},{2})", msg, line, column))
		{
			this.line = line;
			this.column = column;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000052D0 File Offset: 0x000034D0
		public int Line
		{
			get
			{
				return this.line;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000052D8 File Offset: 0x000034D8
		public int Column
		{
			get
			{
				return this.column;
			}
		}

		// Token: 0x04000D22 RID: 3362
		private int line;

		// Token: 0x04000D23 RID: 3363
		private int column;
	}
}
