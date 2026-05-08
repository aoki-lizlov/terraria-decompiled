using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000C RID: 12
[CompilerGenerated]
internal sealed class <>f__AnonymousType10<<Reason>j__TPar>
{
	// Token: 0x17000013 RID: 19
	// (get) Token: 0x0600003B RID: 59 RVA: 0x00002BBD File Offset: 0x00000DBD
	public <Reason>j__TPar Reason
	{
		get
		{
			return this.<Reason>i__Field;
		}
	}

	// Token: 0x0600003C RID: 60 RVA: 0x00002BC5 File Offset: 0x00000DC5
	[DebuggerHidden]
	public <>f__AnonymousType10(<Reason>j__TPar Reason)
	{
		this.<Reason>i__Field = Reason;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00002BD4 File Offset: 0x00000DD4
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType10<<Reason>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<Reason>j__TPar>.Default.Equals(this.<Reason>i__Field, <>f__AnonymousType.<Reason>i__Field);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00002C03 File Offset: 0x00000E03
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return 604355816 * -1521134295 + EqualityComparer<<Reason>j__TPar>.Default.GetHashCode(this.<Reason>i__Field);
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00002C24 File Offset: 0x00000E24
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ Reason = {0} }}";
		object[] array = new object[1];
		int num = 0;
		<Reason>j__TPar <Reason>j__TPar = this.<Reason>i__Field;
		ref <Reason>j__TPar ptr = ref <Reason>j__TPar;
		<Reason>j__TPar <Reason>j__TPar2 = default(<Reason>j__TPar);
		object obj;
		if (<Reason>j__TPar2 == null)
		{
			<Reason>j__TPar2 = <Reason>j__TPar;
			ptr = ref <Reason>j__TPar2;
			if (<Reason>j__TPar2 == null)
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

	// Token: 0x04000013 RID: 19
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Reason>j__TPar <Reason>i__Field;
}
