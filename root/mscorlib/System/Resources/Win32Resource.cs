using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x02000844 RID: 2116
	internal abstract class Win32Resource
	{
		// Token: 0x060047A6 RID: 18342 RVA: 0x000ECA86 File Offset: 0x000EAC86
		internal Win32Resource(NameOrId type, NameOrId name, int language)
		{
			this.type = type;
			this.name = name;
			this.language = language;
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x000ECAA3 File Offset: 0x000EACA3
		internal Win32Resource(Win32ResourceType type, int name, int language)
		{
			this.type = new NameOrId((int)type);
			this.name = new NameOrId(name);
			this.language = language;
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060047A8 RID: 18344 RVA: 0x000ECACA File Offset: 0x000EACCA
		public Win32ResourceType ResourceType
		{
			get
			{
				if (this.type.IsName)
				{
					return (Win32ResourceType)(-1);
				}
				return (Win32ResourceType)this.type.Id;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060047A9 RID: 18345 RVA: 0x000ECAE6 File Offset: 0x000EACE6
		public NameOrId Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060047AA RID: 18346 RVA: 0x000ECAEE File Offset: 0x000EACEE
		public NameOrId Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060047AB RID: 18347 RVA: 0x000ECAF6 File Offset: 0x000EACF6
		public int Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x060047AC RID: 18348
		public abstract void WriteTo(Stream s);

		// Token: 0x060047AD RID: 18349 RVA: 0x000ECB00 File Offset: 0x000EAD00
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "Win32Resource (Kind=";
			array[1] = this.ResourceType.ToString();
			array[2] = ", Name=";
			int num = 3;
			NameOrId nameOrId = this.name;
			array[num] = ((nameOrId != null) ? nameOrId.ToString() : null);
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x04002DAA RID: 11690
		private NameOrId type;

		// Token: 0x04002DAB RID: 11691
		private NameOrId name;

		// Token: 0x04002DAC RID: 11692
		private int language;
	}
}
