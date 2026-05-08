using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000033 RID: 51
	[TypeConverter(typeof(RayConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Ray : IEquatable<Ray>
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00019E58 File Offset: 0x00018058
		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(new string[]
				{
					"Pos( ",
					this.Position.DebugDisplayString,
					" ) \r\n",
					"Dir( ",
					this.Direction.DebugDisplayString,
					" )"
				});
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00019EAC File Offset: 0x000180AC
		public Ray(Vector3 position, Vector3 direction)
		{
			this.Position = position;
			this.Direction = direction;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00019EBC File Offset: 0x000180BC
		public override bool Equals(object obj)
		{
			return obj is Ray && this.Equals((Ray)obj);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00019ED4 File Offset: 0x000180D4
		public bool Equals(Ray other)
		{
			return this.Position.Equals(other.Position) && this.Direction.Equals(other.Direction);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00019EFC File Offset: 0x000180FC
		public override int GetHashCode()
		{
			return this.Position.GetHashCode() ^ this.Direction.GetHashCode();
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00019F24 File Offset: 0x00018124
		public float? Intersects(BoundingBox box)
		{
			float? num = null;
			float? num2 = null;
			float? num4;
			if (MathHelper.WithinEpsilon(this.Direction.X, 0f))
			{
				if (this.Position.X < box.Min.X || this.Position.X > box.Max.X)
				{
					return null;
				}
			}
			else
			{
				num = new float?((box.Min.X - this.Position.X) / this.Direction.X);
				num2 = new float?((box.Max.X - this.Position.X) / this.Direction.X);
				float? num3 = num;
				num4 = num2;
				if ((num3.GetValueOrDefault() > num4.GetValueOrDefault()) & ((num3 != null) & (num4 != null)))
				{
					float? num5 = num;
					num = num2;
					num2 = num5;
				}
			}
			float num9;
			if (!MathHelper.WithinEpsilon(this.Direction.Y, 0f))
			{
				float num6 = (box.Min.Y - this.Position.Y) / this.Direction.Y;
				float num7 = (box.Max.Y - this.Position.Y) / this.Direction.Y;
				if (num6 > num7)
				{
					float num8 = num6;
					num6 = num7;
					num7 = num8;
				}
				if (num != null)
				{
					num4 = num;
					num9 = num7;
					if ((num4.GetValueOrDefault() > num9) & (num4 != null))
					{
						goto IL_01D2;
					}
				}
				if (num2 != null)
				{
					float num10 = num6;
					num4 = num2;
					if ((num10 > num4.GetValueOrDefault()) & (num4 != null))
					{
						goto IL_01D2;
					}
				}
				if (num != null)
				{
					float num11 = num6;
					num4 = num;
					if (!((num11 > num4.GetValueOrDefault()) & (num4 != null)))
					{
						goto IL_0205;
					}
				}
				num = new float?(num6);
				IL_0205:
				if (num2 != null)
				{
					float num12 = num7;
					num4 = num2;
					if (!((num12 < num4.GetValueOrDefault()) & (num4 != null)))
					{
						goto IL_022E;
					}
				}
				num2 = new float?(num7);
				goto IL_022E;
				IL_01D2:
				return null;
			}
			if (this.Position.Y < box.Min.Y || this.Position.Y > box.Max.Y)
			{
				return null;
			}
			IL_022E:
			if (!MathHelper.WithinEpsilon(this.Direction.Z, 0f))
			{
				float num13 = (box.Min.Z - this.Position.Z) / this.Direction.Z;
				float num14 = (box.Max.Z - this.Position.Z) / this.Direction.Z;
				if (num13 > num14)
				{
					float num15 = num13;
					num13 = num14;
					num14 = num15;
				}
				if (num != null)
				{
					num4 = num;
					num9 = num14;
					if ((num4.GetValueOrDefault() > num9) & (num4 != null))
					{
						goto IL_031E;
					}
				}
				if (num2 != null)
				{
					float num16 = num13;
					num4 = num2;
					if ((num16 > num4.GetValueOrDefault()) & (num4 != null))
					{
						goto IL_031E;
					}
				}
				if (num != null)
				{
					float num17 = num13;
					num4 = num;
					if (!((num17 > num4.GetValueOrDefault()) & (num4 != null)))
					{
						goto IL_0351;
					}
				}
				num = new float?(num13);
				IL_0351:
				if (num2 != null)
				{
					float num18 = num14;
					num4 = num2;
					if (!((num18 < num4.GetValueOrDefault()) & (num4 != null)))
					{
						goto IL_037A;
					}
				}
				num2 = new float?(num14);
				goto IL_037A;
				IL_031E:
				return null;
			}
			if (this.Position.Z < box.Min.Z || this.Position.Z > box.Max.Z)
			{
				return null;
			}
			IL_037A:
			if (num != null)
			{
				num4 = num;
				num9 = 0f;
				if ((num4.GetValueOrDefault() < num9) & (num4 != null))
				{
					num4 = num2;
					num9 = 0f;
					if ((num4.GetValueOrDefault() > num9) & (num4 != null))
					{
						return new float?(0f);
					}
				}
			}
			num4 = num;
			num9 = 0f;
			if ((num4.GetValueOrDefault() < num9) & (num4 != null))
			{
				return null;
			}
			return num;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0001A324 File Offset: 0x00018524
		public void Intersects(ref BoundingBox box, out float? result)
		{
			result = this.Intersects(box);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0001A338 File Offset: 0x00018538
		public float? Intersects(BoundingSphere sphere)
		{
			float? num;
			this.Intersects(ref sphere, out num);
			return num;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0001A350 File Offset: 0x00018550
		public float? Intersects(Plane plane)
		{
			float? num;
			this.Intersects(ref plane, out num);
			return num;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0001A368 File Offset: 0x00018568
		public float? Intersects(BoundingFrustum frustum)
		{
			float? num;
			frustum.Intersects(ref this, out num);
			return num;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0001A380 File Offset: 0x00018580
		public void Intersects(ref Plane plane, out float? result)
		{
			float num = Vector3.Dot(this.Direction, plane.Normal);
			if (Math.Abs(num) < 1E-05f)
			{
				result = null;
				return;
			}
			result = new float?((-plane.D - Vector3.Dot(plane.Normal, this.Position)) / num);
			float? num2 = result;
			float num3 = 0f;
			if ((num2.GetValueOrDefault() < num3) & (num2 != null))
			{
				num2 = result;
				num3 = -1E-05f;
				if ((num2.GetValueOrDefault() < num3) & (num2 != null))
				{
					result = null;
					return;
				}
				result = new float?(0f);
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0001A434 File Offset: 0x00018634
		public void Intersects(ref BoundingSphere sphere, out float? result)
		{
			Vector3 vector = sphere.Center - this.Position;
			float num = vector.LengthSquared();
			float num2 = sphere.Radius * sphere.Radius;
			if (num < num2)
			{
				result = new float?(0f);
				return;
			}
			float num3;
			Vector3.Dot(ref this.Direction, ref vector, out num3);
			if (num3 < 0f)
			{
				result = null;
				return;
			}
			float num4 = num2 + num3 * num3 - num;
			result = ((num4 < 0f) ? null : new float?(num3 - (float)Math.Sqrt((double)num4)));
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0001A4D1 File Offset: 0x000186D1
		public static bool operator !=(Ray a, Ray b)
		{
			return !a.Equals(b);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0001A4DE File Offset: 0x000186DE
		public static bool operator ==(Ray a, Ray b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0001A4E8 File Offset: 0x000186E8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{{Position:",
				this.Position.ToString(),
				" Direction:",
				this.Direction.ToString(),
				"}}"
			});
		}

		// Token: 0x040005B5 RID: 1461
		public Vector3 Position;

		// Token: 0x040005B6 RID: 1462
		public Vector3 Direction;
	}
}
