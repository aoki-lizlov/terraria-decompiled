using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000006 RID: 6
[CompilerGenerated]
internal sealed class <>f__AnonymousType4<<BiomeName>j__TPar>
{
	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000017 RID: 23 RVA: 0x00002440 File Offset: 0x00000640
	public <BiomeName>j__TPar BiomeName
	{
		get
		{
			return this.<BiomeName>i__Field;
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002448 File Offset: 0x00000648
	[DebuggerHidden]
	public <>f__AnonymousType4(<BiomeName>j__TPar BiomeName)
	{
		this.<BiomeName>i__Field = BiomeName;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002458 File Offset: 0x00000658
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType4<<BiomeName>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<BiomeName>j__TPar>.Default.Equals(this.<BiomeName>i__Field, <>f__AnonymousType.<BiomeName>i__Field);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002487 File Offset: 0x00000687
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return 2047557675 * -1521134295 + EqualityComparer<<BiomeName>j__TPar>.Default.GetHashCode(this.<BiomeName>i__Field);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000024A8 File Offset: 0x000006A8
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ BiomeName = {0} }}";
		object[] array = new object[1];
		int num = 0;
		<BiomeName>j__TPar <BiomeName>j__TPar = this.<BiomeName>i__Field;
		ref <BiomeName>j__TPar ptr = ref <BiomeName>j__TPar;
		<BiomeName>j__TPar <BiomeName>j__TPar2 = default(<BiomeName>j__TPar);
		object obj;
		if (<BiomeName>j__TPar2 == null)
		{
			<BiomeName>j__TPar2 = <BiomeName>j__TPar;
			ptr = ref <BiomeName>j__TPar2;
			if (<BiomeName>j__TPar2 == null)
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

	// Token: 0x04000007 RID: 7
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <BiomeName>j__TPar <BiomeName>i__Field;
}
