using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000070 RID: 112
	internal struct TypeNameKey : IEquatable<TypeNameKey>
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x000178FD File Offset: 0x00015AFD
		public TypeNameKey(string assemblyName, string typeName)
		{
			this.AssemblyName = assemblyName;
			this.TypeName = typeName;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0001790D File Offset: 0x00015B0D
		public override int GetHashCode()
		{
			string assemblyName = this.AssemblyName;
			int num = ((assemblyName != null) ? assemblyName.GetHashCode() : 0);
			string typeName = this.TypeName;
			return num ^ ((typeName != null) ? typeName.GetHashCode() : 0);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00017934 File Offset: 0x00015B34
		public override bool Equals(object obj)
		{
			return obj is TypeNameKey && this.Equals((TypeNameKey)obj);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001794C File Offset: 0x00015B4C
		public bool Equals(TypeNameKey other)
		{
			return this.AssemblyName == other.AssemblyName && this.TypeName == other.TypeName;
		}

		// Token: 0x04000262 RID: 610
		internal readonly string AssemblyName;

		// Token: 0x04000263 RID: 611
		internal readonly string TypeName;
	}
}
