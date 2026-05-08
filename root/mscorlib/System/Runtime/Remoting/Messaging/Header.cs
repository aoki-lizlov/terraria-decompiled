using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005E9 RID: 1513
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		// Token: 0x06003A84 RID: 14980 RVA: 0x000CDD4D File Offset: 0x000CBF4D
		public Header(string _Name, object _Value)
			: this(_Name, _Value, true)
		{
		}

		// Token: 0x06003A85 RID: 14981 RVA: 0x000CDD58 File Offset: 0x000CBF58
		public Header(string _Name, object _Value, bool _MustUnderstand)
			: this(_Name, _Value, _MustUnderstand, null)
		{
		}

		// Token: 0x06003A86 RID: 14982 RVA: 0x000CDD64 File Offset: 0x000CBF64
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		// Token: 0x04002612 RID: 9746
		public string HeaderNamespace;

		// Token: 0x04002613 RID: 9747
		public bool MustUnderstand;

		// Token: 0x04002614 RID: 9748
		public string Name;

		// Token: 0x04002615 RID: 9749
		public object Value;
	}
}
