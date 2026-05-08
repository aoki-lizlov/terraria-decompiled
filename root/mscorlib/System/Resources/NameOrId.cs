using System;

namespace System.Resources
{
	// Token: 0x02000843 RID: 2115
	internal class NameOrId
	{
		// Token: 0x060047A0 RID: 18336 RVA: 0x000ECA13 File Offset: 0x000EAC13
		public NameOrId(string name)
		{
			this.name = name;
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x000ECA22 File Offset: 0x000EAC22
		public NameOrId(int id)
		{
			this.id = id;
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x000ECA31 File Offset: 0x000EAC31
		public bool IsName
		{
			get
			{
				return this.name != null;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060047A3 RID: 18339 RVA: 0x000ECA3C File Offset: 0x000EAC3C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060047A4 RID: 18340 RVA: 0x000ECA44 File Offset: 0x000EAC44
		public int Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x000ECA4C File Offset: 0x000EAC4C
		public override string ToString()
		{
			if (this.name != null)
			{
				return "Name(" + this.name + ")";
			}
			return "Id(" + this.id.ToString() + ")";
		}

		// Token: 0x04002DA8 RID: 11688
		private string name;

		// Token: 0x04002DA9 RID: 11689
		private int id;
	}
}
