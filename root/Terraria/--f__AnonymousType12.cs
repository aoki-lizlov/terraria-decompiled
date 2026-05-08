using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000E RID: 14
[CompilerGenerated]
internal sealed class <>f__AnonymousType12<<DisplayName>j__TPar, <FullId>j__TPar>
{
	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000046 RID: 70 RVA: 0x00002DB4 File Offset: 0x00000FB4
	public <DisplayName>j__TPar DisplayName
	{
		get
		{
			return this.<DisplayName>i__Field;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000047 RID: 71 RVA: 0x00002DBC File Offset: 0x00000FBC
	public <FullId>j__TPar FullId
	{
		get
		{
			return this.<FullId>i__Field;
		}
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00002DC4 File Offset: 0x00000FC4
	[DebuggerHidden]
	public <>f__AnonymousType12(<DisplayName>j__TPar DisplayName, <FullId>j__TPar FullId)
	{
		this.<DisplayName>i__Field = DisplayName;
		this.<FullId>i__Field = FullId;
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00002DDC File Offset: 0x00000FDC
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType12<<DisplayName>j__TPar, <FullId>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<DisplayName>j__TPar>.Default.Equals(this.<DisplayName>i__Field, <>f__AnonymousType.<DisplayName>i__Field) && EqualityComparer<<FullId>j__TPar>.Default.Equals(this.<FullId>i__Field, <>f__AnonymousType.<FullId>i__Field);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00002E23 File Offset: 0x00001023
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (-684399883 * -1521134295 + EqualityComparer<<DisplayName>j__TPar>.Default.GetHashCode(this.<DisplayName>i__Field)) * -1521134295 + EqualityComparer<<FullId>j__TPar>.Default.GetHashCode(this.<FullId>i__Field);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00002E58 File Offset: 0x00001058
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ DisplayName = {0}, FullId = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<DisplayName>j__TPar <DisplayName>j__TPar = this.<DisplayName>i__Field;
		ref <DisplayName>j__TPar ptr = ref <DisplayName>j__TPar;
		<DisplayName>j__TPar <DisplayName>j__TPar2 = default(<DisplayName>j__TPar);
		object obj;
		if (<DisplayName>j__TPar2 == null)
		{
			<DisplayName>j__TPar2 = <DisplayName>j__TPar;
			ptr = ref <DisplayName>j__TPar2;
			if (<DisplayName>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		int num2 = 1;
		<FullId>j__TPar <FullId>j__TPar = this.<FullId>i__Field;
		ref <FullId>j__TPar ptr2 = ref <FullId>j__TPar;
		<FullId>j__TPar <FullId>j__TPar2 = default(<FullId>j__TPar);
		object obj2;
		if (<FullId>j__TPar2 == null)
		{
			<FullId>j__TPar2 = <FullId>j__TPar;
			ptr2 = ref <FullId>j__TPar2;
			if (<FullId>j__TPar2 == null)
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

	// Token: 0x04000016 RID: 22
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <DisplayName>j__TPar <DisplayName>i__Field;

	// Token: 0x04000017 RID: 23
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <FullId>j__TPar <FullId>i__Field;
}
