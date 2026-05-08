using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000008 RID: 8
[CompilerGenerated]
internal sealed class <>f__AnonymousType6<<Percent>j__TPar>
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000022 RID: 34 RVA: 0x00002638 File Offset: 0x00000838
	public <Percent>j__TPar Percent
	{
		get
		{
			return this.<Percent>i__Field;
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002640 File Offset: 0x00000840
	[DebuggerHidden]
	public <>f__AnonymousType6(<Percent>j__TPar Percent)
	{
		this.<Percent>i__Field = Percent;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002650 File Offset: 0x00000850
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType6<<Percent>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<Percent>j__TPar>.Default.Equals(this.<Percent>i__Field, <>f__AnonymousType.<Percent>i__Field);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000267F File Offset: 0x0000087F
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return -2028095951 * -1521134295 + EqualityComparer<<Percent>j__TPar>.Default.GetHashCode(this.<Percent>i__Field);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000026A0 File Offset: 0x000008A0
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ Percent = {0} }}";
		object[] array = new object[1];
		int num = 0;
		<Percent>j__TPar <Percent>j__TPar = this.<Percent>i__Field;
		ref <Percent>j__TPar ptr = ref <Percent>j__TPar;
		<Percent>j__TPar <Percent>j__TPar2 = default(<Percent>j__TPar);
		object obj;
		if (<Percent>j__TPar2 == null)
		{
			<Percent>j__TPar2 = <Percent>j__TPar;
			ptr = ref <Percent>j__TPar2;
			if (<Percent>j__TPar2 == null)
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

	// Token: 0x0400000A RID: 10
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Percent>j__TPar <Percent>i__Field;
}
