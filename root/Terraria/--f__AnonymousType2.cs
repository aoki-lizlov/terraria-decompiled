using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000004 RID: 4
[CompilerGenerated]
internal sealed class <>f__AnonymousType2<<index>j__TPar, <state>j__TPar>
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000B RID: 11 RVA: 0x000021D1 File Offset: 0x000003D1
	public <index>j__TPar index
	{
		get
		{
			return this.<index>i__Field;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600000C RID: 12 RVA: 0x000021D9 File Offset: 0x000003D9
	public <state>j__TPar state
	{
		get
		{
			return this.<state>i__Field;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021E1 File Offset: 0x000003E1
	[DebuggerHidden]
	public <>f__AnonymousType2(<index>j__TPar index, <state>j__TPar state)
	{
		this.<index>i__Field = index;
		this.<state>i__Field = state;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021F8 File Offset: 0x000003F8
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType2<<index>j__TPar, <state>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<index>j__TPar>.Default.Equals(this.<index>i__Field, <>f__AnonymousType.<index>i__Field) && EqualityComparer<<state>j__TPar>.Default.Equals(this.<state>i__Field, <>f__AnonymousType.<state>i__Field);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000223F File Offset: 0x0000043F
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (1877120171 * -1521134295 + EqualityComparer<<index>j__TPar>.Default.GetHashCode(this.<index>i__Field)) * -1521134295 + EqualityComparer<<state>j__TPar>.Default.GetHashCode(this.<state>i__Field);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002274 File Offset: 0x00000474
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ index = {0}, state = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<index>j__TPar <index>j__TPar = this.<index>i__Field;
		ref <index>j__TPar ptr = ref <index>j__TPar;
		<index>j__TPar <index>j__TPar2 = default(<index>j__TPar);
		object obj;
		if (<index>j__TPar2 == null)
		{
			<index>j__TPar2 = <index>j__TPar;
			ptr = ref <index>j__TPar2;
			if (<index>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		int num2 = 1;
		<state>j__TPar <state>j__TPar = this.<state>i__Field;
		ref <state>j__TPar ptr2 = ref <state>j__TPar;
		<state>j__TPar <state>j__TPar2 = default(<state>j__TPar);
		object obj2;
		if (<state>j__TPar2 == null)
		{
			<state>j__TPar2 = <state>j__TPar;
			ptr2 = ref <state>j__TPar2;
			if (<state>j__TPar2 == null)
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

	// Token: 0x04000003 RID: 3
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <index>j__TPar <index>i__Field;

	// Token: 0x04000004 RID: 4
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <state>j__TPar <state>i__Field;
}
