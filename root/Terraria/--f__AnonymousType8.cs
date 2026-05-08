using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000A RID: 10
[CompilerGenerated]
internal sealed class <>f__AnonymousType8<<NeededWidth>j__TPar, <NeededHeight>j__TPar, <ActualWidth>j__TPar, <ActualHeight>j__TPar>
{
	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600002E RID: 46 RVA: 0x000028BB File Offset: 0x00000ABB
	public <NeededWidth>j__TPar NeededWidth
	{
		get
		{
			return this.<NeededWidth>i__Field;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600002F RID: 47 RVA: 0x000028C3 File Offset: 0x00000AC3
	public <NeededHeight>j__TPar NeededHeight
	{
		get
		{
			return this.<NeededHeight>i__Field;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000030 RID: 48 RVA: 0x000028CB File Offset: 0x00000ACB
	public <ActualWidth>j__TPar ActualWidth
	{
		get
		{
			return this.<ActualWidth>i__Field;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000031 RID: 49 RVA: 0x000028D3 File Offset: 0x00000AD3
	public <ActualHeight>j__TPar ActualHeight
	{
		get
		{
			return this.<ActualHeight>i__Field;
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000028DB File Offset: 0x00000ADB
	[DebuggerHidden]
	public <>f__AnonymousType8(<NeededWidth>j__TPar NeededWidth, <NeededHeight>j__TPar NeededHeight, <ActualWidth>j__TPar ActualWidth, <ActualHeight>j__TPar ActualHeight)
	{
		this.<NeededWidth>i__Field = NeededWidth;
		this.<NeededHeight>i__Field = NeededHeight;
		this.<ActualWidth>i__Field = ActualWidth;
		this.<ActualHeight>i__Field = ActualHeight;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002900 File Offset: 0x00000B00
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType8<<NeededWidth>j__TPar, <NeededHeight>j__TPar, <ActualWidth>j__TPar, <ActualHeight>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<NeededWidth>j__TPar>.Default.Equals(this.<NeededWidth>i__Field, <>f__AnonymousType.<NeededWidth>i__Field) && EqualityComparer<<NeededHeight>j__TPar>.Default.Equals(this.<NeededHeight>i__Field, <>f__AnonymousType.<NeededHeight>i__Field) && EqualityComparer<<ActualWidth>j__TPar>.Default.Equals(this.<ActualWidth>i__Field, <>f__AnonymousType.<ActualWidth>i__Field) && EqualityComparer<<ActualHeight>j__TPar>.Default.Equals(this.<ActualHeight>i__Field, <>f__AnonymousType.<ActualHeight>i__Field);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00002978 File Offset: 0x00000B78
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((-1460962016 * -1521134295 + EqualityComparer<<NeededWidth>j__TPar>.Default.GetHashCode(this.<NeededWidth>i__Field)) * -1521134295 + EqualityComparer<<NeededHeight>j__TPar>.Default.GetHashCode(this.<NeededHeight>i__Field)) * -1521134295 + EqualityComparer<<ActualWidth>j__TPar>.Default.GetHashCode(this.<ActualWidth>i__Field)) * -1521134295 + EqualityComparer<<ActualHeight>j__TPar>.Default.GetHashCode(this.<ActualHeight>i__Field);
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000029E8 File Offset: 0x00000BE8
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ NeededWidth = {0}, NeededHeight = {1}, ActualWidth = {2}, ActualHeight = {3} }}";
		object[] array = new object[4];
		int num = 0;
		<NeededWidth>j__TPar <NeededWidth>j__TPar = this.<NeededWidth>i__Field;
		ref <NeededWidth>j__TPar ptr = ref <NeededWidth>j__TPar;
		<NeededWidth>j__TPar <NeededWidth>j__TPar2 = default(<NeededWidth>j__TPar);
		object obj;
		if (<NeededWidth>j__TPar2 == null)
		{
			<NeededWidth>j__TPar2 = <NeededWidth>j__TPar;
			ptr = ref <NeededWidth>j__TPar2;
			if (<NeededWidth>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		int num2 = 1;
		<NeededHeight>j__TPar <NeededHeight>j__TPar = this.<NeededHeight>i__Field;
		ref <NeededHeight>j__TPar ptr2 = ref <NeededHeight>j__TPar;
		<NeededHeight>j__TPar <NeededHeight>j__TPar2 = default(<NeededHeight>j__TPar);
		object obj2;
		if (<NeededHeight>j__TPar2 == null)
		{
			<NeededHeight>j__TPar2 = <NeededHeight>j__TPar;
			ptr2 = ref <NeededHeight>j__TPar2;
			if (<NeededHeight>j__TPar2 == null)
			{
				obj2 = null;
				goto IL_0081;
			}
		}
		obj2 = ptr2.ToString();
		IL_0081:
		array[num2] = obj2;
		int num3 = 2;
		<ActualWidth>j__TPar <ActualWidth>j__TPar = this.<ActualWidth>i__Field;
		ref <ActualWidth>j__TPar ptr3 = ref <ActualWidth>j__TPar;
		<ActualWidth>j__TPar <ActualWidth>j__TPar2 = default(<ActualWidth>j__TPar);
		object obj3;
		if (<ActualWidth>j__TPar2 == null)
		{
			<ActualWidth>j__TPar2 = <ActualWidth>j__TPar;
			ptr3 = ref <ActualWidth>j__TPar2;
			if (<ActualWidth>j__TPar2 == null)
			{
				obj3 = null;
				goto IL_00C0;
			}
		}
		obj3 = ptr3.ToString();
		IL_00C0:
		array[num3] = obj3;
		int num4 = 3;
		<ActualHeight>j__TPar <ActualHeight>j__TPar = this.<ActualHeight>i__Field;
		ref <ActualHeight>j__TPar ptr4 = ref <ActualHeight>j__TPar;
		<ActualHeight>j__TPar <ActualHeight>j__TPar2 = default(<ActualHeight>j__TPar);
		object obj4;
		if (<ActualHeight>j__TPar2 == null)
		{
			<ActualHeight>j__TPar2 = <ActualHeight>j__TPar;
			ptr4 = ref <ActualHeight>j__TPar2;
			if (<ActualHeight>j__TPar2 == null)
			{
				obj4 = null;
				goto IL_00FF;
			}
		}
		obj4 = ptr4.ToString();
		IL_00FF:
		array[num4] = obj4;
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x0400000E RID: 14
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <NeededWidth>j__TPar <NeededWidth>i__Field;

	// Token: 0x0400000F RID: 15
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <NeededHeight>j__TPar <NeededHeight>i__Field;

	// Token: 0x04000010 RID: 16
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <ActualWidth>j__TPar <ActualWidth>i__Field;

	// Token: 0x04000011 RID: 17
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <ActualHeight>j__TPar <ActualHeight>i__Field;
}
