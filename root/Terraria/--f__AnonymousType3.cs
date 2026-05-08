using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000005 RID: 5
[CompilerGenerated]
internal sealed class <>f__AnonymousType3<<itemId>j__TPar, <count>j__TPar>
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000011 RID: 17 RVA: 0x00002308 File Offset: 0x00000508
	public <itemId>j__TPar itemId
	{
		get
		{
			return this.<itemId>i__Field;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000012 RID: 18 RVA: 0x00002310 File Offset: 0x00000510
	public <count>j__TPar count
	{
		get
		{
			return this.<count>i__Field;
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002318 File Offset: 0x00000518
	[DebuggerHidden]
	public <>f__AnonymousType3(<itemId>j__TPar itemId, <count>j__TPar count)
	{
		this.<itemId>i__Field = itemId;
		this.<count>i__Field = count;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002330 File Offset: 0x00000530
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType3<<itemId>j__TPar, <count>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<itemId>j__TPar>.Default.Equals(this.<itemId>i__Field, <>f__AnonymousType.<itemId>i__Field) && EqualityComparer<<count>j__TPar>.Default.Equals(this.<count>i__Field, <>f__AnonymousType.<count>i__Field);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002377 File Offset: 0x00000577
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (1164955317 * -1521134295 + EqualityComparer<<itemId>j__TPar>.Default.GetHashCode(this.<itemId>i__Field)) * -1521134295 + EqualityComparer<<count>j__TPar>.Default.GetHashCode(this.<count>i__Field);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000023AC File Offset: 0x000005AC
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ itemId = {0}, count = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<itemId>j__TPar <itemId>j__TPar = this.<itemId>i__Field;
		ref <itemId>j__TPar ptr = ref <itemId>j__TPar;
		<itemId>j__TPar <itemId>j__TPar2 = default(<itemId>j__TPar);
		object obj;
		if (<itemId>j__TPar2 == null)
		{
			<itemId>j__TPar2 = <itemId>j__TPar;
			ptr = ref <itemId>j__TPar2;
			if (<itemId>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		int num2 = 1;
		<count>j__TPar <count>j__TPar = this.<count>i__Field;
		ref <count>j__TPar ptr2 = ref <count>j__TPar;
		<count>j__TPar <count>j__TPar2 = default(<count>j__TPar);
		object obj2;
		if (<count>j__TPar2 == null)
		{
			<count>j__TPar2 = <count>j__TPar;
			ptr2 = ref <count>j__TPar2;
			if (<count>j__TPar2 == null)
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

	// Token: 0x04000005 RID: 5
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <itemId>j__TPar <itemId>i__Field;

	// Token: 0x04000006 RID: 6
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <count>j__TPar <count>i__Field;
}
