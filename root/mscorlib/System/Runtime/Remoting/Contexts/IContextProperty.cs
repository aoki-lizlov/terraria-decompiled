using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000566 RID: 1382
	[ComVisible(true)]
	public interface IContextProperty
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06003769 RID: 14185
		string Name { get; }

		// Token: 0x0600376A RID: 14186
		void Freeze(Context newContext);

		// Token: 0x0600376B RID: 14187
		bool IsNewContextOK(Context newCtx);
	}
}
