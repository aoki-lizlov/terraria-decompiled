using System;

namespace System.Resources
{
	// Token: 0x02000833 RID: 2099
	public interface IResourceWriter : IDisposable
	{
		// Token: 0x060046EE RID: 18158
		void AddResource(string name, string value);

		// Token: 0x060046EF RID: 18159
		void AddResource(string name, object value);

		// Token: 0x060046F0 RID: 18160
		void AddResource(string name, byte[] value);

		// Token: 0x060046F1 RID: 18161
		void Close();

		// Token: 0x060046F2 RID: 18162
		void Generate();
	}
}
