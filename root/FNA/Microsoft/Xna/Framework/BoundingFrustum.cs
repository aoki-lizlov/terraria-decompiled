using System;
using System.Diagnostics;
using System.Text;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200000B RID: 11
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public class BoundingFrustum : IEquatable<BoundingFrustum>
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x000063A8 File Offset: 0x000045A8
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x000063B0 File Offset: 0x000045B0
		public Matrix Matrix
		{
			get
			{
				return this.matrix;
			}
			set
			{
				this.matrix = value;
				this.CreatePlanes();
				this.CreateCorners();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x000063C5 File Offset: 0x000045C5
		public Plane Near
		{
			get
			{
				return this.planes[0];
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x000063D3 File Offset: 0x000045D3
		public Plane Far
		{
			get
			{
				return this.planes[1];
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x000063E1 File Offset: 0x000045E1
		public Plane Left
		{
			get
			{
				return this.planes[2];
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000063EF File Offset: 0x000045EF
		public Plane Right
		{
			get
			{
				return this.planes[3];
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000063FD File Offset: 0x000045FD
		public Plane Top
		{
			get
			{
				return this.planes[4];
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0000640B File Offset: 0x0000460B
		public Plane Bottom
		{
			get
			{
				return this.planes[5];
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0000641C File Offset: 0x0000461C
		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(new string[]
				{
					"Near( ",
					this.planes[0].DebugDisplayString,
					" ) \r\n",
					"Far( ",
					this.planes[1].DebugDisplayString,
					" ) \r\n",
					"Left( ",
					this.planes[2].DebugDisplayString,
					" ) \r\n",
					"Right( ",
					this.planes[3].DebugDisplayString,
					" ) \r\n",
					"Top( ",
					this.planes[4].DebugDisplayString,
					" ) \r\n",
					"Bottom( ",
					this.planes[5].DebugDisplayString,
					" ) "
				});
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00006516 File Offset: 0x00004716
		public BoundingFrustum(Matrix value)
		{
			this.matrix = value;
			this.CreatePlanes();
			this.CreateCorners();
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0000654C File Offset: 0x0000474C
		public ContainmentType Contains(BoundingFrustum frustum)
		{
			if (this == frustum)
			{
				return ContainmentType.Contains;
			}
			bool flag = false;
			for (int i = 0; i < 6; i++)
			{
				PlaneIntersectionType planeIntersectionType;
				frustum.Intersects(ref this.planes[i], out planeIntersectionType);
				if (planeIntersectionType == PlaneIntersectionType.Front)
				{
					return ContainmentType.Disjoint;
				}
				if (planeIntersectionType == PlaneIntersectionType.Intersecting)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00006598 File Offset: 0x00004798
		public ContainmentType Contains(BoundingBox box)
		{
			ContainmentType containmentType = ContainmentType.Disjoint;
			this.Contains(ref box, out containmentType);
			return containmentType;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000065B4 File Offset: 0x000047B4
		public void Contains(ref BoundingBox box, out ContainmentType result)
		{
			bool flag = false;
			for (int i = 0; i < 6; i++)
			{
				PlaneIntersectionType planeIntersectionType = PlaneIntersectionType.Front;
				box.Intersects(ref this.planes[i], out planeIntersectionType);
				if (planeIntersectionType == PlaneIntersectionType.Front)
				{
					result = ContainmentType.Disjoint;
					return;
				}
				if (planeIntersectionType == PlaneIntersectionType.Intersecting)
				{
					flag = true;
				}
			}
			result = (flag ? ContainmentType.Intersects : ContainmentType.Contains);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x00006600 File Offset: 0x00004800
		public ContainmentType Contains(BoundingSphere sphere)
		{
			ContainmentType containmentType = ContainmentType.Disjoint;
			this.Contains(ref sphere, out containmentType);
			return containmentType;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0000661C File Offset: 0x0000481C
		public void Contains(ref BoundingSphere sphere, out ContainmentType result)
		{
			bool flag = false;
			for (int i = 0; i < 6; i++)
			{
				PlaneIntersectionType planeIntersectionType = PlaneIntersectionType.Front;
				sphere.Intersects(ref this.planes[i], out planeIntersectionType);
				if (planeIntersectionType == PlaneIntersectionType.Front)
				{
					result = ContainmentType.Disjoint;
					return;
				}
				if (planeIntersectionType == PlaneIntersectionType.Intersecting)
				{
					flag = true;
				}
			}
			result = (flag ? ContainmentType.Intersects : ContainmentType.Contains);
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00006668 File Offset: 0x00004868
		public ContainmentType Contains(Vector3 point)
		{
			ContainmentType containmentType = ContainmentType.Disjoint;
			this.Contains(ref point, out containmentType);
			return containmentType;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00006684 File Offset: 0x00004884
		public void Contains(ref Vector3 point, out ContainmentType result)
		{
			bool flag = false;
			for (int i = 0; i < 6; i++)
			{
				float num = point.X * this.planes[i].Normal.X + point.Y * this.planes[i].Normal.Y + point.Z * this.planes[i].Normal.Z + this.planes[i].D;
				if (num > 0f)
				{
					result = ContainmentType.Disjoint;
					return;
				}
				if (num == 0f)
				{
					flag = true;
					break;
				}
			}
			result = (flag ? ContainmentType.Intersects : ContainmentType.Contains);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00006732 File Offset: 0x00004932
		public Vector3[] GetCorners()
		{
			return (Vector3[])this.corners.Clone();
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00006744 File Offset: 0x00004944
		public void GetCorners(Vector3[] corners)
		{
			if (corners == null)
			{
				throw new ArgumentNullException("corners");
			}
			if (corners.Length < 8)
			{
				throw new ArgumentOutOfRangeException("corners");
			}
			this.corners.CopyTo(corners, 0);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00006772 File Offset: 0x00004972
		public bool Intersects(BoundingFrustum frustum)
		{
			return this.Contains(frustum) > ContainmentType.Disjoint;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00006780 File Offset: 0x00004980
		public bool Intersects(BoundingBox box)
		{
			bool flag = false;
			this.Intersects(ref box, out flag);
			return flag;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0000679C File Offset: 0x0000499C
		public void Intersects(ref BoundingBox box, out bool result)
		{
			ContainmentType containmentType = ContainmentType.Disjoint;
			this.Contains(ref box, out containmentType);
			result = containmentType > ContainmentType.Disjoint;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000067BC File Offset: 0x000049BC
		public bool Intersects(BoundingSphere sphere)
		{
			bool flag = false;
			this.Intersects(ref sphere, out flag);
			return flag;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000067D8 File Offset: 0x000049D8
		public void Intersects(ref BoundingSphere sphere, out bool result)
		{
			ContainmentType containmentType = ContainmentType.Disjoint;
			this.Contains(ref sphere, out containmentType);
			result = containmentType > ContainmentType.Disjoint;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000067F8 File Offset: 0x000049F8
		public PlaneIntersectionType Intersects(Plane plane)
		{
			PlaneIntersectionType planeIntersectionType;
			this.Intersects(ref plane, out planeIntersectionType);
			return planeIntersectionType;
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00006810 File Offset: 0x00004A10
		public void Intersects(ref Plane plane, out PlaneIntersectionType result)
		{
			result = plane.Intersects(ref this.corners[0]);
			for (int i = 1; i < this.corners.Length; i++)
			{
				if (plane.Intersects(ref this.corners[i]) != result)
				{
					result = PlaneIntersectionType.Intersecting;
				}
			}
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00006860 File Offset: 0x00004A60
		public float? Intersects(Ray ray)
		{
			float? num;
			this.Intersects(ref ray, out num);
			return num;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00006878 File Offset: 0x00004A78
		public void Intersects(ref Ray ray, out float? result)
		{
			ContainmentType containmentType;
			this.Contains(ref ray.Position, out containmentType);
			if (containmentType == ContainmentType.Disjoint)
			{
				result = null;
				return;
			}
			if (containmentType == ContainmentType.Contains)
			{
				result = new float?(0f);
				return;
			}
			if (containmentType != ContainmentType.Intersects)
			{
				throw new ArgumentOutOfRangeException("ctype");
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000068C8 File Offset: 0x00004AC8
		private void CreateCorners()
		{
			BoundingFrustum.IntersectionPoint(ref this.planes[0], ref this.planes[2], ref this.planes[4], out this.corners[0]);
			BoundingFrustum.IntersectionPoint(ref this.planes[0], ref this.planes[3], ref this.planes[4], out this.corners[1]);
			BoundingFrustum.IntersectionPoint(ref this.planes[0], ref this.planes[3], ref this.planes[5], out this.corners[2]);
			BoundingFrustum.IntersectionPoint(ref this.planes[0], ref this.planes[2], ref this.planes[5], out this.corners[3]);
			BoundingFrustum.IntersectionPoint(ref this.planes[1], ref this.planes[2], ref this.planes[4], out this.corners[4]);
			BoundingFrustum.IntersectionPoint(ref this.planes[1], ref this.planes[3], ref this.planes[4], out this.corners[5]);
			BoundingFrustum.IntersectionPoint(ref this.planes[1], ref this.planes[3], ref this.planes[5], out this.corners[6]);
			BoundingFrustum.IntersectionPoint(ref this.planes[1], ref this.planes[2], ref this.planes[5], out this.corners[7]);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00006A80 File Offset: 0x00004C80
		private void CreatePlanes()
		{
			this.planes[0] = new Plane(-this.matrix.M13, -this.matrix.M23, -this.matrix.M33, -this.matrix.M43);
			this.planes[1] = new Plane(this.matrix.M13 - this.matrix.M14, this.matrix.M23 - this.matrix.M24, this.matrix.M33 - this.matrix.M34, this.matrix.M43 - this.matrix.M44);
			this.planes[2] = new Plane(-this.matrix.M14 - this.matrix.M11, -this.matrix.M24 - this.matrix.M21, -this.matrix.M34 - this.matrix.M31, -this.matrix.M44 - this.matrix.M41);
			this.planes[3] = new Plane(this.matrix.M11 - this.matrix.M14, this.matrix.M21 - this.matrix.M24, this.matrix.M31 - this.matrix.M34, this.matrix.M41 - this.matrix.M44);
			this.planes[4] = new Plane(this.matrix.M12 - this.matrix.M14, this.matrix.M22 - this.matrix.M24, this.matrix.M32 - this.matrix.M34, this.matrix.M42 - this.matrix.M44);
			this.planes[5] = new Plane(-this.matrix.M14 - this.matrix.M12, -this.matrix.M24 - this.matrix.M22, -this.matrix.M34 - this.matrix.M32, -this.matrix.M44 - this.matrix.M42);
			this.NormalizePlane(ref this.planes[0]);
			this.NormalizePlane(ref this.planes[1]);
			this.NormalizePlane(ref this.planes[2]);
			this.NormalizePlane(ref this.planes[3]);
			this.NormalizePlane(ref this.planes[4]);
			this.NormalizePlane(ref this.planes[5]);
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00006D64 File Offset: 0x00004F64
		private void NormalizePlane(ref Plane p)
		{
			float num = 1f / p.Normal.Length();
			p.Normal.X = p.Normal.X * num;
			p.Normal.Y = p.Normal.Y * num;
			p.Normal.Z = p.Normal.Z * num;
			p.D *= num;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00006DC0 File Offset: 0x00004FC0
		private static void IntersectionPoint(ref Plane a, ref Plane b, ref Plane c, out Vector3 result)
		{
			Vector3 vector;
			Vector3.Cross(ref b.Normal, ref c.Normal, out vector);
			float num;
			Vector3.Dot(ref a.Normal, ref vector, out num);
			num *= -1f;
			Vector3.Cross(ref b.Normal, ref c.Normal, out vector);
			Vector3 vector2;
			Vector3.Multiply(ref vector, a.D, out vector2);
			Vector3.Cross(ref c.Normal, ref a.Normal, out vector);
			Vector3 vector3;
			Vector3.Multiply(ref vector, b.D, out vector3);
			Vector3.Cross(ref a.Normal, ref b.Normal, out vector);
			Vector3 vector4;
			Vector3.Multiply(ref vector, c.D, out vector4);
			result.X = (vector2.X + vector3.X + vector4.X) / num;
			result.Y = (vector2.Y + vector3.Y + vector4.Y) / num;
			result.Z = (vector2.Z + vector3.Z + vector4.Z) / num;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00006EB6 File Offset: 0x000050B6
		public static bool operator ==(BoundingFrustum a, BoundingFrustum b)
		{
			if (object.Equals(a, null))
			{
				return object.Equals(b, null);
			}
			if (object.Equals(b, null))
			{
				return object.Equals(a, null);
			}
			return a.matrix == b.matrix;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00006EEB File Offset: 0x000050EB
		public static bool operator !=(BoundingFrustum a, BoundingFrustum b)
		{
			return !(a == b);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00006EF7 File Offset: 0x000050F7
		public bool Equals(BoundingFrustum other)
		{
			return this == other;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00006F00 File Offset: 0x00005100
		public override bool Equals(object obj)
		{
			return obj is BoundingFrustum && this.Equals((BoundingFrustum)obj);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00006F18 File Offset: 0x00005118
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.Append("{Near:");
			stringBuilder.Append(this.planes[0].ToString());
			stringBuilder.Append(" Far:");
			stringBuilder.Append(this.planes[1].ToString());
			stringBuilder.Append(" Left:");
			stringBuilder.Append(this.planes[2].ToString());
			stringBuilder.Append(" Right:");
			stringBuilder.Append(this.planes[3].ToString());
			stringBuilder.Append(" Top:");
			stringBuilder.Append(this.planes[4].ToString());
			stringBuilder.Append(" Bottom:");
			stringBuilder.Append(this.planes[5].ToString());
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0000703C File Offset: 0x0000523C
		public override int GetHashCode()
		{
			return this.matrix.GetHashCode();
		}

		// Token: 0x0400040C RID: 1036
		public const int CornerCount = 8;

		// Token: 0x0400040D RID: 1037
		private Matrix matrix;

		// Token: 0x0400040E RID: 1038
		private readonly Vector3[] corners = new Vector3[8];

		// Token: 0x0400040F RID: 1039
		private readonly Plane[] planes = new Plane[6];

		// Token: 0x04000410 RID: 1040
		private const int PlaneCount = 6;
	}
}
