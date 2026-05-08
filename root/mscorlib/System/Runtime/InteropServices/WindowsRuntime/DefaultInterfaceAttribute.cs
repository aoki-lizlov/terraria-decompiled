using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000752 RID: 1874
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class DefaultInterfaceAttribute : Attribute
	{
		// Token: 0x06004410 RID: 17424 RVA: 0x000E34E2 File Offset: 0x000E16E2
		public DefaultInterfaceAttribute(Type defaultInterface)
		{
			this.m_defaultInterface = defaultInterface;
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x000E34F1 File Offset: 0x000E16F1
		public Type DefaultInterface
		{
			get
			{
				return this.m_defaultInterface;
			}
		}

		// Token: 0x04002B9E RID: 11166
		private Type m_defaultInterface;
	}
}
