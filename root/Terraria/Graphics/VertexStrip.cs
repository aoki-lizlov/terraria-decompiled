using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Graphics
{
	// Token: 0x020001D3 RID: 467
	public class VertexStrip
	{
		// Token: 0x06001F93 RID: 8083 RVA: 0x0051CB95 File Offset: 0x0051AD95
		public void Reset(int expectedVertexCount = 0)
		{
			this._vertexAmountCurrentlyMaintained = 0;
			this._indicesAmountCurrentlyMaintained = 0;
			if (this._vertices.Length < expectedVertexCount)
			{
				Array.Resize<VertexStrip.CustomVertexInfo>(ref this._vertices, expectedVertexCount);
			}
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x0051CBBC File Offset: 0x0051ADBC
		public void PrepareStrip(Vector2[] positions, float[] rotations, VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), int? expectedVertexPairsAmount = null, bool includeBacksides = false)
		{
			int num = positions.Length;
			this.Reset(num * 2);
			int num2 = num;
			if (expectedVertexPairsAmount != null)
			{
				num2 = expectedVertexPairsAmount.Value;
			}
			int num3 = 0;
			while (num3 < num && !(positions[num3] == Vector2.Zero))
			{
				Vector2 vector = positions[num3] + offsetForAllPositions;
				float num4 = MathHelper.WrapAngle(rotations[num3]);
				float num5 = (float)num3 / (float)(num2 - 1);
				this.AddVertexPair(colorFunction, widthFunction, vector, num4, num5);
				num3++;
			}
			this.PrepareIndices(includeBacksides);
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x0051CC40 File Offset: 0x0051AE40
		public void PrepareStripWithProceduralPadding(Vector2[] positions, float[] rotations, VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 offsetForAllPositions = default(Vector2), bool includeBacksides = false, bool tryStoppingOddBug = true)
		{
			this._temporaryPositionsCache.Clear();
			this._temporaryRotationsCache.Clear();
			int num = 0;
			while (num < positions.Length && !(positions[num] == Vector2.Zero))
			{
				Vector2 vector = positions[num];
				float num2 = MathHelper.WrapAngle(rotations[num]);
				this._temporaryPositionsCache.Add(vector);
				this._temporaryRotationsCache.Add(num2);
				if (num + 1 < positions.Length && positions[num + 1] != Vector2.Zero)
				{
					Vector2 vector2 = positions[num + 1];
					float num3 = MathHelper.WrapAngle(rotations[num + 1]);
					int num4 = (int)(Math.Abs(MathHelper.WrapAngle(num3 - num2)) / 0.2617994f);
					if (num4 != 0)
					{
						float num5 = vector.Distance(vector2);
						Vector2 vector3 = vector + num2.ToRotationVector2() * num5;
						Vector2 vector4 = vector2 + num3.ToRotationVector2() * -num5;
						int num6 = num4 + 2;
						float num7 = 1f / (float)num6;
						Vector2 vector5 = vector;
						for (float num8 = num7; num8 < 1f; num8 += num7)
						{
							Vector2 vector6 = Vector2.CatmullRom(vector3, vector, vector2, vector4, num8);
							float num9 = MathHelper.WrapAngle(vector6.DirectionTo(vector5).ToRotation());
							if (float.IsNaN(num9))
							{
								num9 = this._temporaryRotationsCache.Last<float>();
							}
							this._temporaryPositionsCache.Add(vector6);
							this._temporaryRotationsCache.Add(num9);
							vector5 = vector6;
						}
					}
				}
				num++;
			}
			this.Reset(this._temporaryPositionsCache.Count * 2);
			int count = this._temporaryPositionsCache.Count;
			Vector2 zero = Vector2.Zero;
			int num10 = 0;
			while (num10 < count && (!tryStoppingOddBug || !(this._temporaryPositionsCache[num10] == zero)))
			{
				Vector2 vector7 = this._temporaryPositionsCache[num10] + offsetForAllPositions;
				float num11 = this._temporaryRotationsCache[num10];
				float num12 = (float)num10 / (float)(count - 1);
				this.AddVertexPair(colorFunction, widthFunction, vector7, num11, num12);
				num10++;
			}
			this.PrepareIndices(includeBacksides);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x0051CE60 File Offset: 0x0051B060
		public void PrepareIndices(bool includeBacksides)
		{
			int num = this._vertexAmountCurrentlyMaintained / 2 - 1;
			int num2 = 6 + includeBacksides.ToInt() * 6;
			int num3 = num * num2;
			this._indicesAmountCurrentlyMaintained = num3;
			if (this._indices.Length < num3)
			{
				Array.Resize<short>(ref this._indices, num3);
			}
			short num4 = 0;
			while ((int)num4 < num)
			{
				short num5 = (short)((int)num4 * num2);
				int num6 = (int)(num4 * 2);
				this._indices[(int)num5] = (short)num6;
				this._indices[(int)(num5 + 1)] = (short)(num6 + 1);
				this._indices[(int)(num5 + 2)] = (short)(num6 + 2);
				this._indices[(int)(num5 + 3)] = (short)(num6 + 2);
				this._indices[(int)(num5 + 4)] = (short)(num6 + 1);
				this._indices[(int)(num5 + 5)] = (short)(num6 + 3);
				if (includeBacksides)
				{
					this._indices[(int)(num5 + 6)] = (short)(num6 + 2);
					this._indices[(int)(num5 + 7)] = (short)(num6 + 1);
					this._indices[(int)(num5 + 8)] = (short)num6;
					this._indices[(int)(num5 + 9)] = (short)(num6 + 2);
					this._indices[(int)(num5 + 10)] = (short)(num6 + 3);
					this._indices[(int)(num5 + 11)] = (short)(num6 + 1);
				}
				num4 += 1;
			}
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x0051CF84 File Offset: 0x0051B184
		public void AddVertexPair(VertexStrip.StripColorFunction colorFunction, VertexStrip.StripHalfWidthFunction widthFunction, Vector2 pos, float rot, float progressOnStrip)
		{
			Color color = colorFunction(progressOnStrip);
			float num = widthFunction(progressOnStrip);
			Vector2 vector = MathHelper.WrapAngle(rot - 1.5707964f).ToRotationVector2() * num;
			this.AddVertexPair(pos + vector, pos - vector, progressOnStrip, color);
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0051CFD4 File Offset: 0x0051B1D4
		public void AddVertexPair(Vector2 a, Vector2 b, Vector3 uvA, Vector3 uvB, Color vertexColor)
		{
			while (this._vertexAmountCurrentlyMaintained + 1 >= this._vertices.Length)
			{
				Array.Resize<VertexStrip.CustomVertexInfo>(ref this._vertices, this._vertices.Length * 2);
			}
			Vector2.Distance(a, b);
			this._vertices[this._vertexAmountCurrentlyMaintained].Position = a;
			this._vertices[this._vertexAmountCurrentlyMaintained + 1].Position = b;
			this._vertices[this._vertexAmountCurrentlyMaintained].TexCoord = uvA;
			this._vertices[this._vertexAmountCurrentlyMaintained + 1].TexCoord = uvB;
			this._vertices[this._vertexAmountCurrentlyMaintained].Color = vertexColor;
			this._vertices[this._vertexAmountCurrentlyMaintained + 1].Color = vertexColor;
			this._vertexAmountCurrentlyMaintained += 2;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0051D0B4 File Offset: 0x0051B2B4
		public void AddVertexPair(Vector2 a, Vector2 b, float uv_x, Color vertexColor)
		{
			while (this._vertexAmountCurrentlyMaintained + 1 >= this._vertices.Length)
			{
				Array.Resize<VertexStrip.CustomVertexInfo>(ref this._vertices, this._vertices.Length * 2);
			}
			float num = Vector2.Distance(a, b);
			this._vertices[this._vertexAmountCurrentlyMaintained].Position = a;
			this._vertices[this._vertexAmountCurrentlyMaintained + 1].Position = b;
			this._vertices[this._vertexAmountCurrentlyMaintained].TexCoord = new Vector3(uv_x, num, num);
			this._vertices[this._vertexAmountCurrentlyMaintained + 1].TexCoord = new Vector3(uv_x, 0f, num);
			this._vertices[this._vertexAmountCurrentlyMaintained].Color = vertexColor;
			this._vertices[this._vertexAmountCurrentlyMaintained + 1].Color = vertexColor;
			this._vertexAmountCurrentlyMaintained += 2;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x0051D1A4 File Offset: 0x0051B3A4
		public void AddVertexPair(Vector2 v1, Vector2 v2, float uv_x, Color color1, Color color2)
		{
			while (this._vertexAmountCurrentlyMaintained + 1 >= this._vertices.Length)
			{
				Array.Resize<VertexStrip.CustomVertexInfo>(ref this._vertices, this._vertices.Length * 2);
			}
			float num = Vector2.Distance(v1, v2);
			VertexStrip.CustomVertexInfo[] vertices = this._vertices;
			int num2 = this._vertexAmountCurrentlyMaintained;
			this._vertexAmountCurrentlyMaintained = num2 + 1;
			vertices[num2] = new VertexStrip.CustomVertexInfo(v1, color1, new Vector3(uv_x, num, num));
			VertexStrip.CustomVertexInfo[] vertices2 = this._vertices;
			num2 = this._vertexAmountCurrentlyMaintained;
			this._vertexAmountCurrentlyMaintained = num2 + 1;
			vertices2[num2] = new VertexStrip.CustomVertexInfo(v2, color2, new Vector3(uv_x, 0f, num));
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0051D240 File Offset: 0x0051B440
		public void DrawTrail()
		{
			if (this._vertexAmountCurrentlyMaintained < 3)
			{
				return;
			}
			GraphicsDevice graphicsDevice = Main.instance.GraphicsDevice;
			VertexBufferBinding[] vertexBuffers = graphicsDevice.GetVertexBuffers();
			IndexBuffer indices = graphicsDevice.Indices;
			graphicsDevice.DrawUserIndexedPrimitives<VertexStrip.CustomVertexInfo>(PrimitiveType.TriangleList, this._vertices, 0, this._vertexAmountCurrentlyMaintained, this._indices, 0, this._indicesAmountCurrentlyMaintained / 3);
			graphicsDevice.SetVertexBuffers(vertexBuffers);
			graphicsDevice.Indices = indices;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0051D29F File Offset: 0x0051B49F
		public VertexStrip()
		{
		}

		// Token: 0x04004A27 RID: 18983
		private VertexStrip.CustomVertexInfo[] _vertices = new VertexStrip.CustomVertexInfo[1];

		// Token: 0x04004A28 RID: 18984
		private int _vertexAmountCurrentlyMaintained;

		// Token: 0x04004A29 RID: 18985
		private short[] _indices = new short[1];

		// Token: 0x04004A2A RID: 18986
		private int _indicesAmountCurrentlyMaintained;

		// Token: 0x04004A2B RID: 18987
		private List<Vector2> _temporaryPositionsCache = new List<Vector2>();

		// Token: 0x04004A2C RID: 18988
		private List<float> _temporaryRotationsCache = new List<float>();

		// Token: 0x0200078D RID: 1933
		// (Invoke) Token: 0x0600416E RID: 16750
		public delegate Color StripColorFunction(float progressOnStrip);

		// Token: 0x0200078E RID: 1934
		// (Invoke) Token: 0x06004172 RID: 16754
		public delegate float StripHalfWidthFunction(float progressOnStrip);

		// Token: 0x0200078F RID: 1935
		private struct CustomVertexInfo : IVertexType
		{
			// Token: 0x06004175 RID: 16757 RVA: 0x006BA659 File Offset: 0x006B8859
			public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord)
			{
				this.Position = position;
				this.Color = color;
				this.TexCoord = texCoord;
			}

			// Token: 0x17000528 RID: 1320
			// (get) Token: 0x06004176 RID: 16758 RVA: 0x006BA670 File Offset: 0x006B8870
			public VertexDeclaration VertexDeclaration
			{
				get
				{
					return VertexStrip.CustomVertexInfo._vertexDeclaration;
				}
			}

			// Token: 0x06004177 RID: 16759 RVA: 0x006BA678 File Offset: 0x006B8878
			// Note: this type is marked as 'beforefieldinit'.
			static CustomVertexInfo()
			{
			}

			// Token: 0x0400702F RID: 28719
			public Vector2 Position;

			// Token: 0x04007030 RID: 28720
			public Color Color;

			// Token: 0x04007031 RID: 28721
			public Vector3 TexCoord;

			// Token: 0x04007032 RID: 28722
			private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[]
			{
				new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
				new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
				new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
			});
		}
	}
}
