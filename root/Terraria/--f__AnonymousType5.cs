using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000007 RID: 7
[CompilerGenerated]
internal sealed class <>f__AnonymousType5<<Width>j__TPar, <Height>j__TPar>
{
	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600001C RID: 28 RVA: 0x00002501 File Offset: 0x00000701
	public <Width>j__TPar Width
	{
		get
		{
			return this.<Width>i__Field;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600001D RID: 29 RVA: 0x00002509 File Offset: 0x00000709
	public <Height>j__TPar Height
	{
		get
		{
			return this.<Height>i__Field;
		}
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002511 File Offset: 0x00000711
	[DebuggerHidden]
	public <>f__AnonymousType5(<Width>j__TPar Width, <Height>j__TPar Height)
	{
		this.<Width>i__Field = Width;
		this.<Height>i__Field = Height;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002528 File Offset: 0x00000728
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType5<<Width>j__TPar, <Height>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<Width>j__TPar>.Default.Equals(this.<Width>i__Field, <>f__AnonymousType.<Width>i__Field) && EqualityComparer<<Height>j__TPar>.Default.Equals(this.<Height>i__Field, <>f__AnonymousType.<Height>i__Field);
	}

	// Token: 0x06000020 RID: 32 RVA: 0x0000256F File Offset: 0x0000076F
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (-197155105 * -1521134295 + EqualityComparer<<Width>j__TPar>.Default.GetHashCode(this.<Width>i__Field)) * -1521134295 + EqualityComparer<<Height>j__TPar>.Default.GetHashCode(this.<Height>i__Field);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000025A4 File Offset: 0x000007A4
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ Width = {0}, Height = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<Width>j__TPar <Width>j__TPar = this.<Width>i__Field;
		ref <Width>j__TPar ptr = ref <Width>j__TPar;
		<Width>j__TPar <Width>j__TPar2 = default(<Width>j__TPar);
		object obj;
		if (<Width>j__TPar2 == null)
		{
			<Width>j__TPar2 = <Width>j__TPar;
			ptr = ref <Width>j__TPar2;
			if (<Width>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		int num2 = 1;
		<Height>j__TPar <Height>j__TPar = this.<Height>i__Field;
		ref <Height>j__TPar ptr2 = ref <Height>j__TPar;
		<Height>j__TPar <Height>j__TPar2 = default(<Height>j__TPar);
		object obj2;
		if (<Height>j__TPar2 == null)
		{
			<Height>j__TPar2 = <Height>j__TPar;
			ptr2 = ref <Height>j__TPar2;
			if (<Height>j__TPar2 == null)
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

	// Token: 0x04000008 RID: 8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Width>j__TPar <Width>i__Field;

	// Token: 0x04000009 RID: 9
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Height>j__TPar <Height>i__Field;
}
