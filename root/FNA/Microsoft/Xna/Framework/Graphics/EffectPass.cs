using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000088 RID: 136
	public sealed class EffectPass
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00028B5A File Offset: 0x00026D5A
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x00028B62 File Offset: 0x00026D62
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

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x00028B6B File Offset: 0x00026D6B
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x00028B73 File Offset: 0x00026D73
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

		// Token: 0x060011A4 RID: 4516 RVA: 0x00028B7C File Offset: 0x00026D7C
		internal EffectPass(string name, EffectAnnotationCollection annotations, Effect parent, IntPtr technique, uint passIndex)
		{
			this.Name = name;
			this.Annotations = annotations;
			this.parentEffect = parent;
			this.parentTechnique = technique;
			this.pass = passIndex;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00028BAC File Offset: 0x00026DAC
		public void Apply()
		{
			if (this.parentTechnique != this.parentEffect.CurrentTechnique.TechniquePointer)
			{
				throw new InvalidOperationException("Applied a pass not in the current technique!");
			}
			this.parentEffect.OnApply();
			this.parentEffect.INTERNAL_applyEffect(this.pass);
		}

		// Token: 0x04000809 RID: 2057
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x0400080A RID: 2058
		[CompilerGenerated]
		private EffectAnnotationCollection <Annotations>k__BackingField;

		// Token: 0x0400080B RID: 2059
		private Effect parentEffect;

		// Token: 0x0400080C RID: 2060
		private IntPtr parentTechnique;

		// Token: 0x0400080D RID: 2061
		private uint pass;
	}
}
