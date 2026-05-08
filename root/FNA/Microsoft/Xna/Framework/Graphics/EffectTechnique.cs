using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x0200008A RID: 138
	public sealed class EffectTechnique
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00028D3A File Offset: 0x00026F3A
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00028D42 File Offset: 0x00026F42
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00028D4B File Offset: 0x00026F4B
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00028D53 File Offset: 0x00026F53
		public EffectPassCollection Passes
		{
			[CompilerGenerated]
			get
			{
				return this.<Passes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Passes>k__BackingField = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00028D5C File Offset: 0x00026F5C
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00028D64 File Offset: 0x00026F64
		public EffectAnnotationCollection Annotations
		{
			[CompilerGenerated]
			get
			{
				return this.<Annotations>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Annotations>k__BackingField = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00028D6D File Offset: 0x00026F6D
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00028D75 File Offset: 0x00026F75
		internal IntPtr TechniquePointer
		{
			[CompilerGenerated]
			get
			{
				return this.<TechniquePointer>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<TechniquePointer>k__BackingField = value;
			}
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00028D7E File Offset: 0x00026F7E
		internal EffectTechnique(string name, IntPtr pointer, EffectPassCollection passes, EffectAnnotationCollection annotations)
		{
			this.Name = name;
			this.Passes = passes;
			this.Annotations = annotations;
			this.TechniquePointer = pointer;
		}

		// Token: 0x04000810 RID: 2064
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000811 RID: 2065
		[CompilerGenerated]
		private EffectPassCollection <Passes>k__BackingField;

		// Token: 0x04000812 RID: 2066
		[CompilerGenerated]
		private EffectAnnotationCollection <Annotations>k__BackingField;

		// Token: 0x04000813 RID: 2067
		[CompilerGenerated]
		private IntPtr <TechniquePointer>k__BackingField;
	}
}
