using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000B42 RID: 2882
	internal sealed class ReadOnlySequenceDebugView<T>
	{
		// Token: 0x06006968 RID: 26984 RVA: 0x001659A0 File Offset: 0x00163BA0
		public ReadOnlySequenceDebugView(ReadOnlySequence<T> sequence)
		{
			this._array = (in sequence).ToArray<T>();
			int num = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory in sequence)
			{
				num++;
			}
			ReadOnlyMemory<T>[] array = new ReadOnlyMemory<T>[num];
			int num2 = 0;
			foreach (ReadOnlyMemory<T> readOnlyMemory2 in sequence)
			{
				array[num2] = readOnlyMemory2;
				num2++;
			}
			this._segments = new ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments
			{
				Segments = array
			};
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06006969 RID: 26985 RVA: 0x00165A2B File Offset: 0x00163C2B
		public ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments BufferSegments
		{
			get
			{
				return this._segments;
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x0600696A RID: 26986 RVA: 0x00165A33 File Offset: 0x00163C33
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x04003CA8 RID: 15528
		private readonly T[] _array;

		// Token: 0x04003CA9 RID: 15529
		private readonly ReadOnlySequenceDebugView<T>.ReadOnlySequenceDebugViewSegments _segments;

		// Token: 0x02000B43 RID: 2883
		[DebuggerDisplay("Count: {Segments.Length}", Name = "Segments")]
		public struct ReadOnlySequenceDebugViewSegments
		{
			// Token: 0x1700125C RID: 4700
			// (get) Token: 0x0600696B RID: 26987 RVA: 0x00165A3B File Offset: 0x00163C3B
			// (set) Token: 0x0600696C RID: 26988 RVA: 0x00165A43 File Offset: 0x00163C43
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public ReadOnlyMemory<T>[] Segments
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<Segments>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Segments>k__BackingField = value;
				}
			}

			// Token: 0x04003CAA RID: 15530
			[CompilerGenerated]
			private ReadOnlyMemory<T>[] <Segments>k__BackingField;
		}
	}
}
