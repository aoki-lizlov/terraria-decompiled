using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000003 RID: 3
[CompilerGenerated]
internal sealed class <>f__AnonymousType1<<NPCName>j__TPar>
{
	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000006 RID: 6 RVA: 0x00002111 File Offset: 0x00000311
	public <NPCName>j__TPar NPCName
	{
		get
		{
			return this.<NPCName>i__Field;
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002119 File Offset: 0x00000319
	[DebuggerHidden]
	public <>f__AnonymousType1(<NPCName>j__TPar NPCName)
	{
		this.<NPCName>i__Field = NPCName;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType1<<NPCName>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<NPCName>j__TPar>.Default.Equals(this.<NPCName>i__Field, <>f__AnonymousType.<NPCName>i__Field);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002157 File Offset: 0x00000357
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return 355974006 * -1521134295 + EqualityComparer<<NPCName>j__TPar>.Default.GetHashCode(this.<NPCName>i__Field);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002178 File Offset: 0x00000378
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ NPCName = {0} }}";
		object[] array = new object[1];
		int num = 0;
		<NPCName>j__TPar <NPCName>j__TPar = this.<NPCName>i__Field;
		ref <NPCName>j__TPar ptr = ref <NPCName>j__TPar;
		<NPCName>j__TPar <NPCName>j__TPar2 = default(<NPCName>j__TPar);
		object obj;
		if (<NPCName>j__TPar2 == null)
		{
			<NPCName>j__TPar2 = <NPCName>j__TPar;
			ptr = ref <NPCName>j__TPar2;
			if (<NPCName>j__TPar2 == null)
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

	// Token: 0x04000002 RID: 2
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <NPCName>j__TPar <NPCName>i__Field;
}
