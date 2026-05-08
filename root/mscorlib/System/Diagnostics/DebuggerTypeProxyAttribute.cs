using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A14 RID: 2580
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		// Token: 0x06005FAE RID: 24494 RVA: 0x0014C04E File Offset: 0x0014A24E
		public DebuggerTypeProxyAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.typeName = type.AssemblyQualifiedName;
		}

		// Token: 0x06005FAF RID: 24495 RVA: 0x0014C076 File Offset: 0x0014A276
		public DebuggerTypeProxyAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x06005FB0 RID: 24496 RVA: 0x0014C085 File Offset: 0x0014A285
		public string ProxyTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06005FB2 RID: 24498 RVA: 0x0014C0B6 File Offset: 0x0014A2B6
		// (set) Token: 0x06005FB1 RID: 24497 RVA: 0x0014C08D File Offset: 0x0014A28D
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

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06005FB3 RID: 24499 RVA: 0x0014C0BE File Offset: 0x0014A2BE
		// (set) Token: 0x06005FB4 RID: 24500 RVA: 0x0014C0C6 File Offset: 0x0014A2C6
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

		// Token: 0x040039AD RID: 14765
		private string typeName;

		// Token: 0x040039AE RID: 14766
		private string targetName;

		// Token: 0x040039AF RID: 14767
		private Type target;
	}
}
