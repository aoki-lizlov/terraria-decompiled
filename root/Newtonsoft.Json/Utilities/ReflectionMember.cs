using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000057 RID: 87
	internal class ReflectionMember
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001209E File Offset: 0x0001029E
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x000120A6 File Offset: 0x000102A6
		public Type MemberType
		{
			[CompilerGenerated]
			get
			{
				return this.<MemberType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MemberType>k__BackingField = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x000120AF File Offset: 0x000102AF
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x000120B7 File Offset: 0x000102B7
		public Func<object, object> Getter
		{
			[CompilerGenerated]
			get
			{
				return this.<Getter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Getter>k__BackingField = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000120C0 File Offset: 0x000102C0
		// (set) Token: 0x06000465 RID: 1125 RVA: 0x000120C8 File Offset: 0x000102C8
		public Action<object, object> Setter
		{
			[CompilerGenerated]
			get
			{
				return this.<Setter>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Setter>k__BackingField = value;
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00008020 File Offset: 0x00006220
		public ReflectionMember()
		{
		}

		// Token: 0x04000203 RID: 515
		[CompilerGenerated]
		private Type <MemberType>k__BackingField;

		// Token: 0x04000204 RID: 516
		[CompilerGenerated]
		private Func<object, object> <Getter>k__BackingField;

		// Token: 0x04000205 RID: 517
		[CompilerGenerated]
		private Action<object, object> <Setter>k__BackingField;
	}
}
