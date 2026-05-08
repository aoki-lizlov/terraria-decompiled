using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200000A RID: 10
	[TypeConverter(typeof(BoundingBoxConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct BoundingBox : IEquatable<BoundingBox>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00005188 File Offset: 0x00003388
		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(new string[]
				{
					"Min( ",
					this.Min.DebugDisplayString,
					" ) \r\n",
					"Max( ",
					this.Max.DebugDisplayString,
					" )"
				});
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000051DC File Offset: 0x000033DC
		public BoundingBox(Vector3 min, Vector3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000051EC File Offset: 0x000033EC
		public void Contains(ref BoundingBox box, out ContainmentType result)
		{
			result = this.Contains(box);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000051FC File Offset: 0x000033FC
		public void Contains(ref BoundingSphere sphere, out ContainmentType result)
		{
			result = this.Contains(sphere);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0000520C File Offset: 0x0000340C
		public ContainmentType Contains(Vector3 point)
		{
			ContainmentType containmentType;
			this.Contains(ref point, out containmentType);
			return containmentType;
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00005224 File Offset: 0x00003424
		public ContainmentType Contains(BoundingBox box)
		{
			if (box.Max.X < this.Min.X || box.Min.X > this.Max.X || box.Max.Y < this.Min.Y || box.Min.Y > this.Max.Y || box.Max.Z < this.Min.Z || box.Min.Z > this.Max.Z)
			{
				return ContainmentType.Disjoint;
			}
			if (box.Min.X >= this.Min.X && box.Max.X <= this.Max.X && box.Min.Y >= this.Min.Y && box.Max.Y <= this.Max.Y && box.Min.Z >= this.Min.Z && box.Max.Z <= this.Max.Z)
			{
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00005358 File Offset: 0x00003558
		public ContainmentType Contains(BoundingFrustum frustum)
		{
			Vector3[] corners = frustum.GetCorners();
			int i;
			for (i = 0; i < corners.Length; i++)
			{
				ContainmentType containmentType;
				this.Contains(ref corners[i], out containmentType);
				if (containmentType == ContainmentType.Disjoint)
				{
					break;
				}
			}
			if (i == corners.Length)
			{
				return ContainmentType.Contains;
			}
			if (i != 0)
			{
				return ContainmentType.Intersects;
			}
			for (i++; i < corners.Length; i++)
			{
				ContainmentType containmentType;
				this.Contains(ref corners[i], out containmentType);
				if (containmentType != ContainmentType.Contains)
				{
					return ContainmentType.Intersects;
				}
			}
			return ContainmentType.Contains;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000053C0 File Offset: 0x000035C0
		public ContainmentType Contains(BoundingSphere sphere)
		{
			if (sphere.Center.X - this.Min.X >= sphere.Radius && sphere.Center.Y - this.Min.Y >= sphere.Radius && sphere.Center.Z - this.Min.Z >= sphere.Radius && this.Max.X - sphere.Center.X >= sphere.Radius && this.Max.Y - sphere.Center.Y >= sphere.Radius && this.Max.Z - sphere.Center.Z >= sphere.Radius)
			{
				return ContainmentType.Contains;
			}
			double num = 0.0;
			double num2 = (double)(sphere.Center.X - this.Min.X);
			if (num2 < 0.0)
			{
				if (num2 < (double)(-(double)sphere.Radius))
				{
					return ContainmentType.Disjoint;
				}
				num += num2 * num2;
			}
			else
			{
				num2 = (double)(sphere.Center.X - this.Max.X);
				if (num2 > 0.0)
				{
					if (num2 > (double)sphere.Radius)
					{
						return ContainmentType.Disjoint;
					}
					num += num2 * num2;
				}
			}
			num2 = (double)(sphere.Center.Y - this.Min.Y);
			if (num2 < 0.0)
			{
				if (num2 < (double)(-(double)sphere.Radius))
				{
					return ContainmentType.Disjoint;
				}
				num += num2 * num2;
			}
			else
			{
				num2 = (double)(sphere.Center.Y - this.Max.Y);
				if (num2 > 0.0)
				{
					if (num2 > (double)sphere.Radius)
					{
						return ContainmentType.Disjoint;
					}
					num += num2 * num2;
				}
			}
			num2 = (double)(sphere.Center.Z - this.Min.Z);
			if (num2 < 0.0)
			{
				if (num2 < (double)(-(double)sphere.Radius))
				{
					return ContainmentType.Disjoint;
				}
				num += num2 * num2;
			}
			else
			{
				num2 = (double)(sphere.Center.Z - this.Max.Z);
				if (num2 > 0.0)
				{
					if (num2 > (double)sphere.Radius)
					{
						return ContainmentType.Disjoint;
					}
					num += num2 * num2;
				}
			}
			if (num <= (double)(sphere.Radius * sphere.Radius))
			{
				return ContainmentType.Intersects;
			}
			return ContainmentType.Disjoint;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x00005600 File Offset: 0x00003800
		public void Contains(ref Vector3 point, out ContainmentType result)
		{
			if (point.X < this.Min.X || point.X > this.Max.X || point.Y < this.Min.Y || point.Y > this.Max.Y || point.Z < this.Min.Z || point.Z > this.Max.Z)
			{
				result = ContainmentType.Disjoint;
				return;
			}
			result = ContainmentType.Contains;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00005688 File Offset: 0x00003888
		public Vector3[] GetCorners()
		{
			return new Vector3[]
			{
				new Vector3(this.Min.X, this.Max.Y, this.Max.Z),
				new Vector3(this.Max.X, this.Max.Y, this.Max.Z),
				new Vector3(this.Max.X, this.Min.Y, this.Max.Z),
				new Vector3(this.Min.X, this.Min.Y, this.Max.Z),
				new Vector3(this.Min.X, this.Max.Y, this.Min.Z),
				new Vector3(this.Max.X, this.Max.Y, this.Min.Z),
				new Vector3(this.Max.X, this.Min.Y, this.Min.Z),
				new Vector3(this.Min.X, this.Min.Y, this.Min.Z)
			};
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00005804 File Offset: 0x00003A04
		public void GetCorners(Vector3[] corners)
		{
			if (corners == null)
			{
				throw new ArgumentNullException("corners");
			}
			if (corners.Length < 8)
			{
				throw new ArgumentOutOfRangeException("corners", "Not Enought Corners");
			}
			corners[0].X = this.Min.X;
			corners[0].Y = this.Max.Y;
			corners[0].Z = this.Max.Z;
			corners[1].X = this.Max.X;
			corners[1].Y = this.Max.Y;
			corners[1].Z = this.Max.Z;
			corners[2].X = this.Max.X;
			corners[2].Y = this.Min.Y;
			corners[2].Z = this.Max.Z;
			corners[3].X = this.Min.X;
			corners[3].Y = this.Min.Y;
			corners[3].Z = this.Max.Z;
			corners[4].X = this.Min.X;
			corners[4].Y = this.Max.Y;
			corners[4].Z = this.Min.Z;
			corners[5].X = this.Max.X;
			corners[5].Y = this.Max.Y;
			corners[5].Z = this.Min.Z;
			corners[6].X = this.Max.X;
			corners[6].Y = this.Min.Y;
			corners[6].Z = this.Min.Z;
			corners[7].X = this.Min.X;
			corners[7].Y = this.Min.Y;
			corners[7].Z = this.Min.Z;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00005A5D File Offset: 0x00003C5D
		public float? Intersects(Ray ray)
		{
			return ray.Intersects(this);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00005A6C File Offset: 0x00003C6C
		public void Intersects(ref Ray ray, out float? result)
		{
			result = this.Intersects(ray);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00005A80 File Offset: 0x00003C80
		public bool Intersects(BoundingFrustum frustum)
		{
			return frustum.Intersects(this);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00005A8E File Offset: 0x00003C8E
		public void Intersects(ref BoundingSphere sphere, out bool result)
		{
			result = this.Intersects(sphere);
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00005AA0 File Offset: 0x00003CA0
		public bool Intersects(BoundingBox box)
		{
			bool flag;
			this.Intersects(ref box, out flag);
			return flag;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00005AB8 File Offset: 0x00003CB8
		public PlaneIntersectionType Intersects(Plane plane)
		{
			PlaneIntersectionType planeIntersectionType;
			this.Intersects(ref plane, out planeIntersectionType);
			return planeIntersectionType;
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public void Intersects(ref BoundingBox box, out bool result)
		{
			if (this.Max.X < box.Min.X || this.Min.X > box.Max.X)
			{
				result = false;
				return;
			}
			if (this.Max.Y < box.Min.Y || this.Min.Y > box.Max.Y)
			{
				result = false;
				return;
			}
			result = this.Max.Z >= box.Min.Z && this.Min.Z <= box.Max.Z;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00005B80 File Offset: 0x00003D80
		public bool Intersects(BoundingSphere sphere)
		{
			if (sphere.Center.X - this.Min.X > sphere.Radius && sphere.Center.Y - this.Min.Y > sphere.Radius && sphere.Center.Z - this.Min.Z > sphere.Radius && this.Max.X - sphere.Center.X > sphere.Radius && this.Max.Y - sphere.Center.Y > sphere.Radius && this.Max.Z - sphere.Center.Z > sphere.Radius)
			{
				return true;
			}
			float num = sphere.Radius * sphere.Radius;
			double num2 = 0.0;
			if (sphere.Center.X < this.Min.X)
			{
				num2 += (double)((sphere.Center.X - this.Min.X) * (sphere.Center.X - this.Min.X));
			}
			else if (sphere.Center.X > this.Max.X)
			{
				num2 += (double)((sphere.Center.X - this.Max.X) * (sphere.Center.X - this.Max.X));
			}
			if (sphere.Center.Y < this.Min.Y)
			{
				num2 += (double)((sphere.Center.Y - this.Min.Y) * (sphere.Center.Y - this.Min.Y));
			}
			else if (sphere.Center.Y > this.Max.Y)
			{
				num2 += (double)((sphere.Center.Y - this.Max.Y) * (sphere.Center.Y - this.Max.Y));
			}
			if (sphere.Center.Z < this.Min.Z)
			{
				num2 += (double)((sphere.Center.Z - this.Min.Z) * (sphere.Center.Z - this.Min.Z));
			}
			else if (sphere.Center.Z > this.Max.Z)
			{
				num2 += (double)((sphere.Center.Z - this.Max.Z) * (sphere.Center.Z - this.Max.Z));
			}
			return num2 <= (double)num;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00005E34 File Offset: 0x00004034
		public void Intersects(ref Plane plane, out PlaneIntersectionType result)
		{
			Vector3 vector;
			Vector3 vector2;
			if (plane.Normal.X >= 0f)
			{
				vector.X = this.Max.X;
				vector2.X = this.Min.X;
			}
			else
			{
				vector.X = this.Min.X;
				vector2.X = this.Max.X;
			}
			if (plane.Normal.Y >= 0f)
			{
				vector.Y = this.Max.Y;
				vector2.Y = this.Min.Y;
			}
			else
			{
				vector.Y = this.Min.Y;
				vector2.Y = this.Max.Y;
			}
			if (plane.Normal.Z >= 0f)
			{
				vector.Z = this.Max.Z;
				vector2.Z = this.Min.Z;
			}
			else
			{
				vector.Z = this.Min.Z;
				vector2.Z = this.Max.Z;
			}
			if (plane.Normal.X * vector2.X + plane.Normal.Y * vector2.Y + plane.Normal.Z * vector2.Z + plane.D > 0f)
			{
				result = PlaneIntersectionType.Front;
				return;
			}
			if (plane.Normal.X * vector.X + plane.Normal.Y * vector.Y + plane.Normal.Z * vector.Z + plane.D < 0f)
			{
				result = PlaneIntersectionType.Back;
				return;
			}
			result = PlaneIntersectionType.Intersecting;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00005FEC File Offset: 0x000041EC
		public bool Equals(BoundingBox other)
		{
			return this.Min == other.Min && this.Max == other.Max;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00006014 File Offset: 0x00004214
		public static BoundingBox CreateFromPoints(IEnumerable<Vector3> points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			bool flag = true;
			Vector3 maxVector = BoundingBox.MaxVector3;
			Vector3 minVector = BoundingBox.MinVector3;
			foreach (Vector3 vector in points)
			{
				maxVector.X = ((maxVector.X < vector.X) ? maxVector.X : vector.X);
				maxVector.Y = ((maxVector.Y < vector.Y) ? maxVector.Y : vector.Y);
				maxVector.Z = ((maxVector.Z < vector.Z) ? maxVector.Z : vector.Z);
				minVector.X = ((minVector.X > vector.X) ? minVector.X : vector.X);
				minVector.Y = ((minVector.Y > vector.Y) ? minVector.Y : vector.Y);
				minVector.Z = ((minVector.Z > vector.Z) ? minVector.Z : vector.Z);
				flag = false;
			}
			if (flag)
			{
				throw new ArgumentException("Collection is empty", "points");
			}
			return new BoundingBox(maxVector, minVector);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00006174 File Offset: 0x00004374
		public static BoundingBox CreateFromSphere(BoundingSphere sphere)
		{
			BoundingBox boundingBox;
			BoundingBox.CreateFromSphere(ref sphere, out boundingBox);
			return boundingBox;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0000618C File Offset: 0x0000438C
		public static void CreateFromSphere(ref BoundingSphere sphere, out BoundingBox result)
		{
			Vector3 vector = new Vector3(sphere.Radius);
			result.Min = sphere.Center - vector;
			result.Max = sphere.Center + vector;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000061CC File Offset: 0x000043CC
		public static BoundingBox CreateMerged(BoundingBox original, BoundingBox additional)
		{
			BoundingBox boundingBox;
			BoundingBox.CreateMerged(ref original, ref additional, out boundingBox);
			return boundingBox;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000061E8 File Offset: 0x000043E8
		public static void CreateMerged(ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
		{
			result.Min.X = Math.Min(original.Min.X, additional.Min.X);
			result.Min.Y = Math.Min(original.Min.Y, additional.Min.Y);
			result.Min.Z = Math.Min(original.Min.Z, additional.Min.Z);
			result.Max.X = Math.Max(original.Max.X, additional.Max.X);
			result.Max.Y = Math.Max(original.Max.Y, additional.Max.Y);
			result.Max.Z = Math.Max(original.Max.Z, additional.Max.Z);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000062D9 File Offset: 0x000044D9
		public override bool Equals(object obj)
		{
			return obj is BoundingBox && this.Equals((BoundingBox)obj);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000062F1 File Offset: 0x000044F1
		public override int GetHashCode()
		{
			return this.Min.GetHashCode() + this.Max.GetHashCode();
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00006316 File Offset: 0x00004516
		public static bool operator ==(BoundingBox a, BoundingBox b)
		{
			return a.Equals(b);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00006320 File Offset: 0x00004520
		public static bool operator !=(BoundingBox a, BoundingBox b)
		{
			return !a.Equals(b);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00006330 File Offset: 0x00004530
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{{Min:",
				this.Min.ToString(),
				" Max:",
				this.Max.ToString(),
				"}}"
			});
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00006388 File Offset: 0x00004588
		// Note: this type is marked as 'beforefieldinit'.
		static BoundingBox()
		{
		}

		// Token: 0x04000407 RID: 1031
		public Vector3 Min;

		// Token: 0x04000408 RID: 1032
		public Vector3 Max;

		// Token: 0x04000409 RID: 1033
		public const int CornerCount = 8;

		// Token: 0x0400040A RID: 1034
		private static Vector3 MaxVector3 = new Vector3(float.MaxValue);

		// Token: 0x0400040B RID: 1035
		private static Vector3 MinVector3 = new Vector3(float.MinValue);
	}
}
