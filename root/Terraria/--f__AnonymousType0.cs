using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000002 RID: 2
[CompilerGenerated]
internal sealed class <>f__AnonymousType0<<Count>j__TPar>
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public <Count>j__TPar Count
	{
		get
		{
			return this.<Count>i__Field;
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	[DebuggerHidden]
	public <>f__AnonymousType0(<Count>j__TPar Count)
	{
		this.<Count>i__Field = Count;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType0<<Count>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<Count>j__TPar>.Default.Equals(this.<Count>i__Field, <>f__AnonymousType.<Count>i__Field);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002097 File Offset: 0x00000297
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return -654001905 * -1521134295 + EqualityComparer<<Count>j__TPar>.Default.GetHashCode(this.<Count>i__Field);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020B8 File Offset: 0x000002B8
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ Count = {0} }}";
		object[] array = new object[1];
		int num = 0;
		<Count>j__TPar <Count>j__TPar = this.<Count>i__Field;
		ref <Count>j__TPar ptr = ref <Count>j__TPar;
		<Count>j__TPar <Count>j__TPar2 = default(<Count>j__TPar);
		object obj;
		if (<Count>j__TPar2 == null)
		{
			<Count>j__TPar2 = <Count>j__TPar;
			ptr = ref <Count>j__TPar2;
			if (<Count>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000001 RID: 1
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Count>j__TPar <Count>i__Field;
}
