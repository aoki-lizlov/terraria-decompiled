using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000757 RID: 1879
	[AttributeUsage(AttributeTargets.Delegate | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = false)]
	public sealed class ReturnValueNameAttribute : Attribute
	{
		// Token: 0x0600441B RID: 17435 RVA: 0x000E354E File Offset: 0x000E174E
		public ReturnValueNameAttribute(string name)
		{
			this.m_Name = name;
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x0600441C RID: 17436 RVA: 0x000E355D File Offset: 0x000E175D
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x04002BA4 RID: 11172
		private string m_Name;
	}
}
