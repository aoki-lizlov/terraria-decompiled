using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200081A RID: 2074
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x0600465C RID: 18012 RVA: 0x000E6BEC File Offset: 0x000E4DEC
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x04002D10 RID: 11536
		private string typeName;
	}
}
