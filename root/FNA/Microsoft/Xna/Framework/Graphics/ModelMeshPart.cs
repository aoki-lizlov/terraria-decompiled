using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A6 RID: 166
	public sealed class ModelMeshPart
	{
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0002E00C File Offset: 0x0002C20C
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x0002E014 File Offset: 0x0002C214
		public Effect Effect
		{
			get
			{
				return this.INTERNAL_effect;
			}
			set
			{
				if (value == this.INTERNAL_effect)
				{
					return;
				}
				if (this.INTERNAL_effect != null)
				{
					bool flag = true;
					foreach (ModelMeshPart modelMeshPart in this.parent.MeshParts)
					{
						if (modelMeshPart != this && modelMeshPart.INTERNAL_effect == this.INTERNAL_effect)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.parent.Effects.Remove(this.INTERNAL_effect);
					}
				}
				this.INTERNAL_effect = value;
				if (this.INTERNAL_effect != null && !this.parent.Effects.Contains(this.INTERNAL_effect))
				{
					this.parent.Effects.Add(value);
				}
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0002E0E0 File Offset: 0x0002C2E0
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x0002E0E8 File Offset: 0x0002C2E8
		public IndexBuffer IndexBuffer
		{
			[CompilerGenerated]
			get
			{
				return this.<IndexBuffer>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<IndexBuffer>k__BackingField = value;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0002E0F1 File Offset: 0x0002C2F1
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0002E0F9 File Offset: 0x0002C2F9
		public int NumVertices
		{
			[CompilerGenerated]
			get
			{
				return this.<NumVertices>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<NumVertices>k__BackingField = value;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0002E102 File Offset: 0x0002C302
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0002E10A File Offset: 0x0002C30A
		public int PrimitiveCount
		{
			[CompilerGenerated]
			get
			{
				return this.<PrimitiveCount>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<PrimitiveCount>k__BackingField = value;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0002E113 File Offset: 0x0002C313
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x0002E11B File Offset: 0x0002C31B
		public int StartIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<StartIndex>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<StartIndex>k__BackingField = value;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0002E124 File Offset: 0x0002C324
		// (set) Token: 0x060013E4 RID: 5092 RVA: 0x0002E12C File Offset: 0x0002C32C
		public object Tag
		{
			[CompilerGenerated]
			get
			{
				return this.<Tag>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tag>k__BackingField = value;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0002E135 File Offset: 0x0002C335
		// (set) Token: 0x060013E6 RID: 5094 RVA: 0x0002E13D File Offset: 0x0002C33D
		public VertexBuffer VertexBuffer
		{
			[CompilerGenerated]
			get
			{
				return this.<VertexBuffer>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<VertexBuffer>k__BackingField = value;
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0002E146 File Offset: 0x0002C346
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x0002E14E File Offset: 0x0002C34E
		public int VertexOffset
		{
			[CompilerGenerated]
			get
			{
				return this.<VertexOffset>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<VertexOffset>k__BackingField = value;
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x000136F5 File Offset: 0x000118F5
		internal ModelMeshPart()
		{
		}

		// Token: 0x0400090D RID: 2317
		[CompilerGenerated]
		private IndexBuffer <IndexBuffer>k__BackingField;

		// Token: 0x0400090E RID: 2318
		[CompilerGenerated]
		private int <NumVertices>k__BackingField;

		// Token: 0x0400090F RID: 2319
		[CompilerGenerated]
		private int <PrimitiveCount>k__BackingField;

		// Token: 0x04000910 RID: 2320
		[CompilerGenerated]
		private int <StartIndex>k__BackingField;

		// Token: 0x04000911 RID: 2321
		[CompilerGenerated]
		private object <Tag>k__BackingField;

		// Token: 0x04000912 RID: 2322
		[CompilerGenerated]
		private VertexBuffer <VertexBuffer>k__BackingField;

		// Token: 0x04000913 RID: 2323
		[CompilerGenerated]
		private int <VertexOffset>k__BackingField;

		// Token: 0x04000914 RID: 2324
		internal ModelMesh parent;

		// Token: 0x04000915 RID: 2325
		private Effect INTERNAL_effect;
	}
}
