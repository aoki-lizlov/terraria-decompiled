using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x020006C4 RID: 1732
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[ComVisible(true)]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		// Token: 0x06004010 RID: 16400 RVA: 0x000E0A6A File Offset: 0x000DEC6A
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this._val = sourceInterfaces;
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x000E0A79 File Offset: 0x000DEC79
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this._val = sourceInterface.FullName;
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x000E0A8D File Offset: 0x000DEC8D
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000E0AB4 File Offset: 0x000DECB4
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this._val = string.Concat(new string[] { sourceInterface1.FullName, "\0", sourceInterface2.FullName, "\0", sourceInterface3.FullName });
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x000E0B04 File Offset: 0x000DED04
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this._val = string.Concat(new string[] { sourceInterface1.FullName, "\0", sourceInterface2.FullName, "\0", sourceInterface3.FullName, "\0", sourceInterface4.FullName });
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x000E0B65 File Offset: 0x000DED65
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x040029C6 RID: 10694
		internal string _val;
	}
}
