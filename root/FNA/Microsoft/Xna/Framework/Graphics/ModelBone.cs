using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A1 RID: 161
	public sealed class ModelBone
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0002DC08 File Offset: 0x0002BE08
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x0002DC10 File Offset: 0x0002BE10
		public ModelBoneCollection Children
		{
			[CompilerGenerated]
			get
			{
				return this.<Children>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Children>k__BackingField = value;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0002DC19 File Offset: 0x0002BE19
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0002DC21 File Offset: 0x0002BE21
		public int Index
		{
			[CompilerGenerated]
			get
			{
				return this.<Index>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Index>k__BackingField = value;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0002DC2A File Offset: 0x0002BE2A
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0002DC32 File Offset: 0x0002BE32
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0002DC3B File Offset: 0x0002BE3B
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x0002DC43 File Offset: 0x0002BE43
		public ModelBone Parent
		{
			[CompilerGenerated]
			get
			{
				return this.<Parent>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Parent>k__BackingField = value;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0002DC4C File Offset: 0x0002BE4C
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x0002DC54 File Offset: 0x0002BE54
		public Matrix Transform
		{
			[CompilerGenerated]
			get
			{
				return this.<Transform>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Transform>k__BackingField = value;
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0002DC5D File Offset: 0x0002BE5D
		internal ModelBone()
		{
			this.Children = new ModelBoneCollection(new List<ModelBone>());
			this.meshes = new List<ModelMesh>();
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0002DC96 File Offset: 0x0002BE96
		internal void AddMesh(ModelMesh mesh)
		{
			this.meshes.Add(mesh);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0002DCA4 File Offset: 0x0002BEA4
		internal void AddChild(ModelBone modelBone)
		{
			this.children.Add(modelBone);
			this.Children = new ModelBoneCollection(this.children);
		}

		// Token: 0x040008FF RID: 2303
		[CompilerGenerated]
		private ModelBoneCollection <Children>k__BackingField;

		// Token: 0x04000900 RID: 2304
		[CompilerGenerated]
		private int <Index>k__BackingField;

		// Token: 0x04000901 RID: 2305
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000902 RID: 2306
		[CompilerGenerated]
		private ModelBone <Parent>k__BackingField;

		// Token: 0x04000903 RID: 2307
		[CompilerGenerated]
		private Matrix <Transform>k__BackingField;

		// Token: 0x04000904 RID: 2308
		private List<ModelBone> children = new List<ModelBone>();

		// Token: 0x04000905 RID: 2309
		private List<ModelMesh> meshes = new List<ModelMesh>();
	}
}
