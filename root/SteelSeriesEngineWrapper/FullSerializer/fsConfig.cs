using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FullSerializer
{
	// Token: 0x02000005 RID: 5
	public class fsConfig
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002748 File Offset: 0x00000948
		public fsConfig()
		{
		}

		// Token: 0x04000005 RID: 5
		public Type[] SerializeAttributes = new Type[] { typeof(fsPropertyAttribute) };

		// Token: 0x04000006 RID: 6
		public Type[] IgnoreSerializeAttributes = new Type[]
		{
			typeof(NonSerializedAttribute),
			typeof(fsIgnoreAttribute)
		};

		// Token: 0x04000007 RID: 7
		public fsMemberSerialization DefaultMemberSerialization = fsMemberSerialization.Default;

		// Token: 0x04000008 RID: 8
		public Func<string, MemberInfo, string> GetJsonNameFromMemberName = (string name, MemberInfo info) => name;

		// Token: 0x04000009 RID: 9
		public bool SerializeNonAutoProperties;

		// Token: 0x0400000A RID: 10
		public bool SerializeNonPublicSetProperties = true;

		// Token: 0x0400000B RID: 11
		public string CustomDateTimeFormatString;

		// Token: 0x0400000C RID: 12
		public bool Serialize64BitIntegerAsString;

		// Token: 0x0400000D RID: 13
		public bool SerializeEnumsAsInteger;

		// Token: 0x020000B3 RID: 179
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600028F RID: 655 RVA: 0x00009B50 File Offset: 0x00007D50
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000290 RID: 656 RVA: 0x00002493 File Offset: 0x00000693
			public <>c()
			{
			}

			// Token: 0x06000291 RID: 657 RVA: 0x00009B5C File Offset: 0x00007D5C
			internal string <.ctor>b__9_0(string name, MemberInfo info)
			{
				return name;
			}

			// Token: 0x0400024A RID: 586
			public static readonly fsConfig.<>c <>9 = new fsConfig.<>c();

			// Token: 0x0400024B RID: 587
			public static Func<string, MemberInfo, string> <>9__9_0;
		}
	}
}
