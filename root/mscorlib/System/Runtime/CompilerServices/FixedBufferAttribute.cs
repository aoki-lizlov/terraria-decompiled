using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007C2 RID: 1986
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FixedBufferAttribute : Attribute
	{
		// Token: 0x06004590 RID: 17808 RVA: 0x000E51CD File Offset: 0x000E33CD
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.ElementType = elementType;
			this.Length = length;
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06004591 RID: 17809 RVA: 0x000E51E3 File Offset: 0x000E33E3
		public Type ElementType
		{
			[CompilerGenerated]
			get
			{
				return this.<ElementType>k__BackingField;
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x000E51EB File Offset: 0x000E33EB
		public int Length
		{
			[CompilerGenerated]
			get
			{
				return this.<Length>k__BackingField;
			}
		}

		// Token: 0x04002CB9 RID: 11449
		[CompilerGenerated]
		private readonly Type <ElementType>k__BackingField;

		// Token: 0x04002CBA RID: 11450
		[CompilerGenerated]
		private readonly int <Length>k__BackingField;
	}
}
