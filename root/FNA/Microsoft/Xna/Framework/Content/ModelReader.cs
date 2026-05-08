using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework.Content
{
	// Token: 0x02000122 RID: 290
	internal class ModelReader : ContentTypeReader<Model>
	{
		// Token: 0x06001752 RID: 5970 RVA: 0x00039692 File Offset: 0x00037892
		public ModelReader()
		{
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0003969C File Offset: 0x0003789C
		private static int ReadBoneReference(ContentReader reader, uint boneCount)
		{
			uint num;
			if (boneCount < 255U)
			{
				num = (uint)reader.ReadByte();
			}
			else
			{
				num = reader.ReadUInt32();
			}
			if (num != 0U)
			{
				return (int)(num - 1U);
			}
			return -1;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000396CC File Offset: 0x000378CC
		protected internal override Model Read(ContentReader reader, Model existingInstance)
		{
			uint num = reader.ReadUInt32();
			List<ModelBone> list = new List<ModelBone>((int)num);
			for (uint num2 = 0U; num2 < num; num2 += 1U)
			{
				string text = reader.ReadObject<string>();
				Matrix matrix = reader.ReadMatrix();
				ModelBone modelBone = new ModelBone
				{
					Transform = matrix,
					Index = (int)num2,
					Name = text
				};
				list.Add(modelBone);
			}
			int num3 = 0;
			while ((long)num3 < (long)((ulong)num))
			{
				ModelBone modelBone2 = list[num3];
				int num4 = ModelReader.ReadBoneReference(reader, num);
				if (num4 != -1)
				{
					modelBone2.Parent = list[num4];
				}
				uint num5 = reader.ReadUInt32();
				if (num5 != 0U)
				{
					for (uint num6 = 0U; num6 < num5; num6 += 1U)
					{
						int num7 = ModelReader.ReadBoneReference(reader, num);
						if (num7 != -1)
						{
							modelBone2.AddChild(list[num7]);
						}
					}
				}
				num3++;
			}
			List<ModelMesh> list2 = new List<ModelMesh>();
			int num8 = reader.ReadInt32();
			GraphicsDevice graphicsDevice = reader.ContentManager.GetGraphicsDevice();
			for (int i = 0; i < num8; i++)
			{
				string text2 = reader.ReadObject<string>();
				int num9 = ModelReader.ReadBoneReference(reader, num);
				BoundingSphere boundingSphere = reader.ReadBoundingSphere();
				object obj = reader.ReadObject<object>();
				int num10 = reader.ReadInt32();
				List<ModelMeshPart> parts = new List<ModelMeshPart>(num10);
				uint num11 = 0U;
				while ((ulong)num11 < (ulong)((long)num10))
				{
					ModelMeshPart modelMeshPart;
					if (existingInstance != null)
					{
						modelMeshPart = existingInstance.Meshes[i].MeshParts[(int)num11];
					}
					else
					{
						modelMeshPart = new ModelMeshPart();
					}
					modelMeshPart.VertexOffset = reader.ReadInt32();
					modelMeshPart.NumVertices = reader.ReadInt32();
					modelMeshPart.StartIndex = reader.ReadInt32();
					modelMeshPart.PrimitiveCount = reader.ReadInt32();
					modelMeshPart.Tag = reader.ReadObject<object>();
					parts.Add(modelMeshPart);
					int jj = (int)num11;
					reader.ReadSharedResource<VertexBuffer>(delegate(VertexBuffer v)
					{
						parts[jj].VertexBuffer = v;
					});
					reader.ReadSharedResource<IndexBuffer>(delegate(IndexBuffer v)
					{
						parts[jj].IndexBuffer = v;
					});
					reader.ReadSharedResource<Effect>(delegate(Effect v)
					{
						parts[jj].Effect = v;
					});
					num11 += 1U;
				}
				if (existingInstance == null)
				{
					ModelMesh modelMesh = new ModelMesh(graphicsDevice, parts);
					modelMesh.Tag = obj;
					modelMesh.Name = text2;
					modelMesh.ParentBone = list[num9];
					modelMesh.ParentBone.AddMesh(modelMesh);
					modelMesh.BoundingSphere = boundingSphere;
					list2.Add(modelMesh);
				}
			}
			if (existingInstance != null)
			{
				ModelReader.ReadBoneReference(reader, num);
				reader.ReadObject<object>();
				return existingInstance;
			}
			int num12 = ModelReader.ReadBoneReference(reader, num);
			return new Model(graphicsDevice, list, list2)
			{
				Root = list[num12],
				Tag = reader.ReadObject<object>()
			};
		}

		// Token: 0x020003DB RID: 987
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x06001AF6 RID: 6902 RVA: 0x000136F5 File Offset: 0x000118F5
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x04001DE6 RID: 7654
			public List<ModelMeshPart> parts;
		}

		// Token: 0x020003DC RID: 988
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_1
		{
			// Token: 0x06001AF7 RID: 6903 RVA: 0x000136F5 File Offset: 0x000118F5
			public <>c__DisplayClass2_1()
			{
			}

			// Token: 0x06001AF8 RID: 6904 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
			internal void <Read>b__0(VertexBuffer v)
			{
				this.CS$<>8__locals1.parts[this.jj].VertexBuffer = v;
			}

			// Token: 0x06001AF9 RID: 6905 RVA: 0x0003FA06 File Offset: 0x0003DC06
			internal void <Read>b__1(IndexBuffer v)
			{
				this.CS$<>8__locals1.parts[this.jj].IndexBuffer = v;
			}

			// Token: 0x06001AFA RID: 6906 RVA: 0x0003FA24 File Offset: 0x0003DC24
			internal void <Read>b__2(Effect v)
			{
				this.CS$<>8__locals1.parts[this.jj].Effect = v;
			}

			// Token: 0x04001DE7 RID: 7655
			public int jj;

			// Token: 0x04001DE8 RID: 7656
			public ModelReader.<>c__DisplayClass2_0 CS$<>8__locals1;
		}
	}
}
