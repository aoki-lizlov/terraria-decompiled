using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A4 RID: 164
	public sealed class ModelMesh
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0002DD9D File Offset: 0x0002BF9D
		// (set) Token: 0x060013C8 RID: 5064 RVA: 0x0002DDA5 File Offset: 0x0002BFA5
		public BoundingSphere BoundingSphere
		{
			[CompilerGenerated]
			get
			{
				return this.<BoundingSphere>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<BoundingSphere>k__BackingField = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0002DDAE File Offset: 0x0002BFAE
		// (set) Token: 0x060013CA RID: 5066 RVA: 0x0002DDB6 File Offset: 0x0002BFB6
		public ModelEffectCollection Effects
		{
			[CompilerGenerated]
			get
			{
				return this.<Effects>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Effects>k__BackingField = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x0002DDBF File Offset: 0x0002BFBF
		// (set) Token: 0x060013CC RID: 5068 RVA: 0x0002DDC7 File Offset: 0x0002BFC7
		public ModelMeshPartCollection MeshParts
		{
			[CompilerGenerated]
			get
			{
				return this.<MeshParts>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MeshParts>k__BackingField = value;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0002DDD0 File Offset: 0x0002BFD0
		// (set) Token: 0x060013CE RID: 5070 RVA: 0x0002DDD8 File Offset: 0x0002BFD8
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

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0002DDE1 File Offset: 0x0002BFE1
		// (set) Token: 0x060013D0 RID: 5072 RVA: 0x0002DDE9 File Offset: 0x0002BFE9
		public ModelBone ParentBone
		{
			[CompilerGenerated]
			get
			{
				return this.<ParentBone>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ParentBone>k__BackingField = value;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0002DDF2 File Offset: 0x0002BFF2
		// (set) Token: 0x060013D2 RID: 5074 RVA: 0x0002DDFA File Offset: 0x0002BFFA
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

		// Token: 0x060013D3 RID: 5075 RVA: 0x0002DE04 File Offset: 0x0002C004
		internal ModelMesh(GraphicsDevice graphicsDevice, List<ModelMeshPart> parts)
		{
			this.graphicsDevice = graphicsDevice;
			this.MeshParts = new ModelMeshPartCollection(parts);
			foreach (ModelMeshPart modelMeshPart in parts)
			{
				modelMeshPart.parent = this;
			}
			this.Effects = new ModelEffectCollection();
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0002DE74 File Offset: 0x0002C074
		public void Draw()
		{
			foreach (ModelMeshPart modelMeshPart in this.MeshParts)
			{
				Effect effect = modelMeshPart.Effect;
				if (modelMeshPart.PrimitiveCount > 0)
				{
					this.graphicsDevice.SetVertexBuffer(modelMeshPart.VertexBuffer);
					this.graphicsDevice.Indices = modelMeshPart.IndexBuffer;
					foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
					{
						effectPass.Apply();
						this.graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, modelMeshPart.VertexOffset, 0, modelMeshPart.NumVertices, modelMeshPart.StartIndex, modelMeshPart.PrimitiveCount);
					}
				}
			}
		}

		// Token: 0x04000906 RID: 2310
		[CompilerGenerated]
		private BoundingSphere <BoundingSphere>k__BackingField;

		// Token: 0x04000907 RID: 2311
		[CompilerGenerated]
		private ModelEffectCollection <Effects>k__BackingField;

		// Token: 0x04000908 RID: 2312
		[CompilerGenerated]
		private ModelMeshPartCollection <MeshParts>k__BackingField;

		// Token: 0x04000909 RID: 2313
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x0400090A RID: 2314
		[CompilerGenerated]
		private ModelBone <ParentBone>k__BackingField;

		// Token: 0x0400090B RID: 2315
		[CompilerGenerated]
		private object <Tag>k__BackingField;

		// Token: 0x0400090C RID: 2316
		private GraphicsDevice graphicsDevice;
	}
}
