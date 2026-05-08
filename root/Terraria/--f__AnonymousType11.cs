using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000D RID: 13
[CompilerGenerated]
internal sealed class <>f__AnonymousType11<<FilePath>j__TPar, <Reason>j__TPar>
{
	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000040 RID: 64 RVA: 0x00002C7D File Offset: 0x00000E7D
	public <FilePath>j__TPar FilePath
	{
		get
		{
			return this.<FilePath>i__Field;
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C85 File Offset: 0x00000E85
	public <Reason>j__TPar Reason
	{
		get
		{
			return this.<Reason>i__Field;
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002C8D File Offset: 0x00000E8D
	[DebuggerHidden]
	public <>f__AnonymousType11(<FilePath>j__TPar FilePath, <Reason>j__TPar Reason)
	{
		this.<FilePath>i__Field = FilePath;
		this.<Reason>i__Field = Reason;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002CA4 File Offset: 0x00000EA4
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType11<<FilePath>j__TPar, <Reason>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<FilePath>j__TPar>.Default.Equals(this.<FilePath>i__Field, <>f__AnonymousType.<FilePath>i__Field) && EqualityComparer<<Reason>j__TPar>.Default.Equals(this.<Reason>i__Field, <>f__AnonymousType.<Reason>i__Field);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002CEB File Offset: 0x00000EEB
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (-1807250805 * -1521134295 + EqualityComparer<<FilePath>j__TPar>.Default.GetHashCode(this.<FilePath>i__Field)) * -1521134295 + EqualityComparer<<Reason>j__TPar>.Default.GetHashCode(this.<Reason>i__Field);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00002D20 File Offset: 0x00000F20
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ FilePath = {0}, Reason = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<FilePath>j__TPar <FilePath>j__TPar = this.<FilePath>i__Field;
		ref <FilePath>j__TPar ptr = ref <FilePath>j__TPar;
		<FilePath>j__TPar <FilePath>j__TPar2 = default(<FilePath>j__TPar);
		object obj;
		if (<FilePath>j__TPar2 == null)
		{
			<FilePath>j__TPar2 = <FilePath>j__TPar;
			ptr = ref <FilePath>j__TPar2;
			if (<FilePath>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		int num2 = 1;
		<Reason>j__TPar <Reason>j__TPar = this.<Reason>i__Field;
		ref <Reason>j__TPar ptr2 = ref <Reason>j__TPar;
		<Reason>j__TPar <Reason>j__TPar2 = default(<Reason>j__TPar);
		object obj2;
		if (<Reason>j__TPar2 == null)
		{
			<Reason>j__TPar2 = <Reason>j__TPar;
			ptr2 = ref <Reason>j__TPar2;
			if (<Reason>j__TPar2 == null)
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

	// Token: 0x04000014 RID: 20
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <FilePath>j__TPar <FilePath>i__Field;

	// Token: 0x04000015 RID: 21
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Reason>j__TPar <Reason>i__Field;
}
