using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x0200011F RID: 287
	internal sealed class MemoryDebugView<T>
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0002B7F7 File Offset: 0x000299F7
		public MemoryDebugView(Memory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002B80B File Offset: 0x00029A0B
		public MemoryDebugView(ReadOnlyMemory<T> memory)
		{
			this._memory = memory;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002B81A File Offset: 0x00029A1A
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._memory.ToArray();
			}
		}

		// Token: 0x04001103 RID: 4355
		private readonly ReadOnlyMemory<T> _memory;
	}
}
