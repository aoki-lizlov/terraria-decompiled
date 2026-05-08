using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000B RID: 11
[CompilerGenerated]
internal sealed class <>f__AnonymousType9<<Amount>j__TPar>
{
	// Token: 0x17000012 RID: 18
	// (get) Token: 0x06000036 RID: 54 RVA: 0x00002AFA File Offset: 0x00000CFA
	public <Amount>j__TPar Amount
	{
		get
		{
			return this.<Amount>i__Field;
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002B02 File Offset: 0x00000D02
	[DebuggerHidden]
	public <>f__AnonymousType9(<Amount>j__TPar Amount)
	{
		this.<Amount>i__Field = Amount;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00002B14 File Offset: 0x00000D14
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType9<<Amount>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<Amount>j__TPar>.Default.Equals(this.<Amount>i__Field, <>f__AnonymousType.<Amount>i__Field);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00002B43 File Offset: 0x00000D43
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return -1939625864 * -1521134295 + EqualityComparer<<Amount>j__TPar>.Default.GetHashCode(this.<Amount>i__Field);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x00002B64 File Offset: 0x00000D64
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ Amount = {0} }}";
		object[] array = new object[1];
		int num = 0;
		<Amount>j__TPar <Amount>j__TPar = this.<Amount>i__Field;
		ref <Amount>j__TPar ptr = ref <Amount>j__TPar;
		<Amount>j__TPar <Amount>j__TPar2 = default(<Amount>j__TPar);
		object obj;
		if (<Amount>j__TPar2 == null)
		{
			<Amount>j__TPar2 = <Amount>j__TPar;
			ptr = ref <Amount>j__TPar2;
			if (<Amount>j__TPar2 == null)
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

	// Token: 0x04000012 RID: 18
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Amount>j__TPar <Amount>i__Field;
}
