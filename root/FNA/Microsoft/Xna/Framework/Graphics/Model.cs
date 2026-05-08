using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x020000A0 RID: 160
	public class Model
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0002D94F File Offset: 0x0002BB4F
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0002D957 File Offset: 0x0002BB57
		public ModelBoneCollection Bones
		{
			[CompilerGenerated]
			get
			{
				return this.<Bones>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Bones>k__BackingField = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0002D960 File Offset: 0x0002BB60
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x0002D968 File Offset: 0x0002BB68
		public ModelMeshCollection Meshes
		{
			[CompilerGenerated]
			get
			{
				return this.<Meshes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Meshes>k__BackingField = value;
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0002D971 File Offset: 0x0002BB71
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x0002D979 File Offset: 0x0002BB79
		public ModelBone Root
		{
			[CompilerGenerated]
			get
			{
				return this.<Root>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Root>k__BackingField = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0002D982 File Offset: 0x0002BB82
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x0002D98A File Offset: 0x0002BB8A
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

		// Token: 0x060013AC RID: 5036 RVA: 0x0002D993 File Offset: 0x0002BB93
		internal Model(GraphicsDevice graphicsDevice, List<ModelBone> bones, List<ModelMesh> meshes)
		{
			this.Bones = new ModelBoneCollection(bones);
			this.Meshes = new ModelMeshCollection(meshes);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0002D9B4 File Offset: 0x0002BBB4
		public void Draw(Matrix world, Matrix view, Matrix projection)
		{
			int count = this.Bones.Count;
			if (Model.sharedDrawBoneMatrices == null || Model.sharedDrawBoneMatrices.Length < count)
			{
				Model.sharedDrawBoneMatrices = new Matrix[count];
			}
			this.CopyAbsoluteBoneTransformsTo(Model.sharedDrawBoneMatrices);
			foreach (ModelMesh modelMesh in this.Meshes)
			{
				foreach (Effect effect in modelMesh.Effects)
				{
					IEffectMatrices effectMatrices = effect as IEffectMatrices;
					if (effectMatrices == null)
					{
						throw new InvalidOperationException();
					}
					effectMatrices.World = Model.sharedDrawBoneMatrices[modelMesh.ParentBone.Index] * world;
					effectMatrices.View = view;
					effectMatrices.Projection = projection;
				}
				modelMesh.Draw();
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0002DAB4 File Offset: 0x0002BCB4
		public void CopyAbsoluteBoneTransformsTo(Matrix[] destinationBoneTransforms)
		{
			if (destinationBoneTransforms == null)
			{
				throw new ArgumentNullException("destinationBoneTransforms");
			}
			if (destinationBoneTransforms.Length < this.Bones.Count)
			{
				throw new ArgumentOutOfRangeException("destinationBoneTransforms");
			}
			int count = this.Bones.Count;
			for (int i = 0; i < count; i++)
			{
				ModelBone modelBone = this.Bones[i];
				if (modelBone.Parent == null)
				{
					destinationBoneTransforms[i] = modelBone.Transform;
				}
				else
				{
					int index = modelBone.Parent.Index;
					Matrix transform = modelBone.Transform;
					Matrix.Multiply(ref transform, ref destinationBoneTransforms[index], out destinationBoneTransforms[i]);
				}
			}
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0002DB50 File Offset: 0x0002BD50
		public void CopyBoneTransformsFrom(Matrix[] sourceBoneTransforms)
		{
			if (sourceBoneTransforms == null)
			{
				throw new ArgumentNullException("sourceBoneTransforms");
			}
			if (sourceBoneTransforms.Length < this.Bones.Count)
			{
				throw new ArgumentOutOfRangeException("sourceBoneTransforms");
			}
			for (int i = 0; i < sourceBoneTransforms.Length; i++)
			{
				this.Bones[i].Transform = sourceBoneTransforms[i];
			}
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0002DBAC File Offset: 0x0002BDAC
		public void CopyBoneTransformsTo(Matrix[] destinationBoneTransforms)
		{
			if (destinationBoneTransforms == null)
			{
				throw new ArgumentNullException("destinationBoneTransforms");
			}
			if (destinationBoneTransforms.Length < this.Bones.Count)
			{
				throw new ArgumentOutOfRangeException("destinationBoneTransforms");
			}
			for (int i = 0; i < destinationBoneTransforms.Length; i++)
			{
				destinationBoneTransforms[i] = this.Bones[i].Transform;
			}
		}

		// Token: 0x040008FA RID: 2298
		[CompilerGenerated]
		private ModelBoneCollection <Bones>k__BackingField;

		// Token: 0x040008FB RID: 2299
		[CompilerGenerated]
		private ModelMeshCollection <Meshes>k__BackingField;

		// Token: 0x040008FC RID: 2300
		[CompilerGenerated]
		private ModelBone <Root>k__BackingField;

		// Token: 0x040008FD RID: 2301
		[CompilerGenerated]
		private object <Tag>k__BackingField;

		// Token: 0x040008FE RID: 2302
		private static Matrix[] sharedDrawBoneMatrices;
	}
}
