using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x02000B0D RID: 2829
	internal sealed class StackDebugView<T>
	{
		// Token: 0x0600681D RID: 26653 RVA: 0x00161043 File Offset: 0x0015F243
		public StackDebugView(Stack<T> stack)
		{
			if (stack == null)
			{
				throw new ArgumentNullException("stack");
			}
			this._stack = stack;
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x0600681E RID: 26654 RVA: 0x00161060 File Offset: 0x0015F260
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._stack.ToArray();
			}
		}

		// Token: 0x04003C52 RID: 15442
		private readonly Stack<T> _stack;
	}
}
