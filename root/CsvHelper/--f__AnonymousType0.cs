using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000002 RID: 2
[CompilerGenerated]
internal sealed class <>f__AnonymousType0<<pair>j__TPar, <index>j__TPar>
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public <pair>j__TPar pair
	{
		get
		{
			return this.<pair>i__Field;
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	public <index>j__TPar index
	{
		get
		{
			return this.<index>i__Field;
		}
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
	[DebuggerHidden]
	public <>f__AnonymousType0(<pair>j__TPar pair, <index>j__TPar index)
	{
		this.<pair>i__Field = pair;
		this.<index>i__Field = index;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002078 File Offset: 0x00000278
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType0<<pair>j__TPar, <index>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<pair>j__TPar>.Default.Equals(this.<pair>i__Field, <>f__AnonymousType.<pair>i__Field) && EqualityComparer<<index>j__TPar>.Default.Equals(this.<index>i__Field, <>f__AnonymousType.<index>i__Field);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x000020BF File Offset: 0x000002BF
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (2120376782 * -1521134295 + EqualityComparer<<pair>j__TPar>.Default.GetHashCode(this.<pair>i__Field)) * -1521134295 + EqualityComparer<<index>j__TPar>.Default.GetHashCode(this.<index>i__Field);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x000020F4 File Offset: 0x000002F4
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ pair = {0}, index = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<pair>j__TPar <pair>j__TPar = this.<pair>i__Field;
		ref <pair>j__TPar ptr = ref <pair>j__TPar;
		<pair>j__TPar <pair>j__TPar2 = default(<pair>j__TPar);
		object obj;
		if (<pair>j__TPar2 == null)
		{
			<pair>j__TPar2 = <pair>j__TPar;
			ptr = ref <pair>j__TPar2;
			if (<pair>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		int num2 = 1;
		<index>j__TPar <index>j__TPar = this.<index>i__Field;
		ref <index>j__TPar ptr2 = ref <index>j__TPar;
		<index>j__TPar <index>j__TPar2 = default(<index>j__TPar);
		object obj2;
		if (<index>j__TPar2 == null)
		{
			<index>j__TPar2 = <index>j__TPar;
			ptr2 = ref <index>j__TPar2;
			if (<index>j__TPar2 == null)
			{
				obj2 = null;
				goto IL_0081;
			}
		}
		obj2 = ptr2.ToString();
		IL_0081:
		array[num2] = obj2;
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x04000001 RID: 1
	[DebuggerBrowsable(0)]
	private readonly <pair>j__TPar <pair>i__Field;

	// Token: 0x04000002 RID: 2
	[DebuggerBrowsable(0)]
	private readonly <index>j__TPar <index>i__Field;
}
