using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A15 RID: 2581
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerDisplayAttribute : Attribute
	{
		// Token: 0x06005FB5 RID: 24501 RVA: 0x0014C0CF File Offset: 0x0014A2CF
		public DebuggerDisplayAttribute(string value)
		{
			if (value == null)
			{
				this.value = "";
			}
			else
			{
				this.value = value;
			}
			this.name = "";
			this.type = "";
		}

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06005FB6 RID: 24502 RVA: 0x0014C104 File Offset: 0x0014A304
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06005FB7 RID: 24503 RVA: 0x0014C10C File Offset: 0x0014A30C
		// (set) Token: 0x06005FB8 RID: 24504 RVA: 0x0014C114 File Offset: 0x0014A314
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x06005FB9 RID: 24505 RVA: 0x0014C11D File Offset: 0x0014A31D
		// (set) Token: 0x06005FBA RID: 24506 RVA: 0x0014C125 File Offset: 0x0014A325
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x06005FBC RID: 24508 RVA: 0x0014C157 File Offset: 0x0014A357
		// (set) Token: 0x06005FBB RID: 24507 RVA: 0x0014C12E File Offset: 0x0014A32E
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06005FBD RID: 24509 RVA: 0x0014C15F File Offset: 0x0014A35F
		// (set) Token: 0x06005FBE RID: 24510 RVA: 0x0014C167 File Offset: 0x0014A367
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040039B0 RID: 14768
		private string name;

		// Token: 0x040039B1 RID: 14769
		private string value;

		// Token: 0x040039B2 RID: 14770
		private string type;

		// Token: 0x040039B3 RID: 14771
		private string targetName;

		// Token: 0x040039B4 RID: 14772
		private Type target;
	}
}
