using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000009 RID: 9
[CompilerGenerated]
internal sealed class <>f__AnonymousType7<<Adjective>j__TPar, <Location>j__TPar, <Noun>j__TPar>
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000027 RID: 39 RVA: 0x000026F9 File Offset: 0x000008F9
	public <Adjective>j__TPar Adjective
	{
		get
		{
			return this.<Adjective>i__Field;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000028 RID: 40 RVA: 0x00002701 File Offset: 0x00000901
	public <Location>j__TPar Location
	{
		get
		{
			return this.<Location>i__Field;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000029 RID: 41 RVA: 0x00002709 File Offset: 0x00000909
	public <Noun>j__TPar Noun
	{
		get
		{
			return this.<Noun>i__Field;
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002711 File Offset: 0x00000911
	[DebuggerHidden]
	public <>f__AnonymousType7(<Adjective>j__TPar Adjective, <Location>j__TPar Location, <Noun>j__TPar Noun)
	{
		this.<Adjective>i__Field = Adjective;
		this.<Location>i__Field = Location;
		this.<Noun>i__Field = Noun;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002730 File Offset: 0x00000930
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType7<<Adjective>j__TPar, <Location>j__TPar, <Noun>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<Adjective>j__TPar>.Default.Equals(this.<Adjective>i__Field, <>f__AnonymousType.<Adjective>i__Field) && EqualityComparer<<Location>j__TPar>.Default.Equals(this.<Location>i__Field, <>f__AnonymousType.<Location>i__Field) && EqualityComparer<<Noun>j__TPar>.Default.Equals(this.<Noun>i__Field, <>f__AnonymousType.<Noun>i__Field);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002790 File Offset: 0x00000990
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return ((-499666512 * -1521134295 + EqualityComparer<<Adjective>j__TPar>.Default.GetHashCode(this.<Adjective>i__Field)) * -1521134295 + EqualityComparer<<Location>j__TPar>.Default.GetHashCode(this.<Location>i__Field)) * -1521134295 + EqualityComparer<<Noun>j__TPar>.Default.GetHashCode(this.<Noun>i__Field);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000027E8 File Offset: 0x000009E8
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider formatProvider = null;
		string text = "{{ Adjective = {0}, Location = {1}, Noun = {2} }}";
		object[] array = new object[3];
		int num = 0;
		<Adjective>j__TPar <Adjective>j__TPar = this.<Adjective>i__Field;
		ref <Adjective>j__TPar ptr = ref <Adjective>j__TPar;
		<Adjective>j__TPar <Adjective>j__TPar2 = default(<Adjective>j__TPar);
		object obj;
		if (<Adjective>j__TPar2 == null)
		{
			<Adjective>j__TPar2 = <Adjective>j__TPar;
			ptr = ref <Adjective>j__TPar2;
			if (<Adjective>j__TPar2 == null)
			{
				obj = null;
				goto IL_0046;
			}
		}
		obj = ptr.ToString();
		IL_0046:
		array[num] = obj;
		int num2 = 1;
		<Location>j__TPar <Location>j__TPar = this.<Location>i__Field;
		ref <Location>j__TPar ptr2 = ref <Location>j__TPar;
		<Location>j__TPar <Location>j__TPar2 = default(<Location>j__TPar);
		object obj2;
		if (<Location>j__TPar2 == null)
		{
			<Location>j__TPar2 = <Location>j__TPar;
			ptr2 = ref <Location>j__TPar2;
			if (<Location>j__TPar2 == null)
			{
				obj2 = null;
				goto IL_0081;
			}
		}
		obj2 = ptr2.ToString();
		IL_0081:
		array[num2] = obj2;
		int num3 = 2;
		<Noun>j__TPar <Noun>j__TPar = this.<Noun>i__Field;
		ref <Noun>j__TPar ptr3 = ref <Noun>j__TPar;
		<Noun>j__TPar <Noun>j__TPar2 = default(<Noun>j__TPar);
		object obj3;
		if (<Noun>j__TPar2 == null)
		{
			<Noun>j__TPar2 = <Noun>j__TPar;
			ptr3 = ref <Noun>j__TPar2;
			if (<Noun>j__TPar2 == null)
			{
				obj3 = null;
				goto IL_00C0;
			}
		}
		obj3 = ptr3.ToString();
		IL_00C0:
		array[num3] = obj3;
		return string.Format(formatProvider, text, array);
	}

	// Token: 0x0400000B RID: 11
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Adjective>j__TPar <Adjective>i__Field;

	// Token: 0x0400000C RID: 12
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Location>j__TPar <Location>i__Field;

	// Token: 0x0400000D RID: 13
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Noun>j__TPar <Noun>i__Field;
}
