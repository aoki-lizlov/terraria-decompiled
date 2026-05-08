using System;

namespace System
{
	// Token: 0x020000EE RID: 238
	internal ref struct DTSubString
	{
		// Token: 0x170000C9 RID: 201
		internal unsafe char this[int relativeIndex]
		{
			get
			{
				return (char)(*this.s[this.index + relativeIndex]);
			}
		}

		// Token: 0x04000FD0 RID: 4048
		internal ReadOnlySpan<char> s;

		// Token: 0x04000FD1 RID: 4049
		internal int index;

		// Token: 0x04000FD2 RID: 4050
		internal int length;

		// Token: 0x04000FD3 RID: 4051
		internal DTSubStringType type;

		// Token: 0x04000FD4 RID: 4052
		internal int value;
	}
}
