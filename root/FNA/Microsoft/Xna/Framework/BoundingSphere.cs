using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200000C RID: 12
	[TypeConverter(typeof(BoundingSphereConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct BoundingSphere : IEquatable<BoundingSphere>
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00007050 File Offset: 0x00005250
		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(new string[]
				{
					"Center( ",
					this.Center.DebugDisplayString,
					" ) \r\n",
					"Radius( ",
					this.Radius.ToString(),
					" ) "
				});
			}
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x000070A4 File Offset: 0x000052A4
		public BoundingSphere(Vector3 center, float radius)
		{
			this.Center = center;
			this.Radius = radius;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000070B4 File Offset: 0x000052B4
		public BoundingSphere Transform(Matrix matrix)
		{
			return new BoundingSphere
			{
				Center = Vector3.Transform(this.Center, matrix),
				Radius = this.Radius * (float)Math.Sqrt((double)Math.Max(matrix.M11 * matrix.M11 + matrix.M12 * matrix.M12 + matrix.M13 * matrix.M13, Math.Max(matrix.M21 * matrix.M21 + matrix.M22 * matrix.M22 + matrix.M23 * matrix.M23, matrix.M31 * matrix.M31 + matrix.M32 * matrix.M32 + matrix.M33 * matrix.M33)))
			};
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00007178 File Offset: 0x00005378
		public void Transform(ref Matrix matrix, out BoundingSphere result)
		{
			result.Center = Vector3.Transform(this.Center, matrix);
			result.Radius = this.Radius * (float)Math.Sqrt((double)Math.Max(matrix.M11 * matrix.M11 + matrix.M12 * matrix.M12 + matrix.M13 * matrix.M13, Math.Max(matrix.M21 * matrix.M21 + matrix.M22 * matrix.M22 + matrix.M23 * matrix.M23, matrix.M31 * matrix.M31 + matrix.M32 * matrix.M32 + matrix.M33 * matrix.M33)));
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00007235 File Offset: 0x00005435
		public void Contains(ref BoundingBox box, out ContainmentType result)
		{
			result = this.Contains(box);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00007245 File Offset: 0x00005445
		public void Contains(ref BoundingSphere sphere, out ContainmentType result)
		{
			result = this.Contains(sphere);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00007255 File Offset: 0x00005455
		public void Contains(ref Vector3 point, out ContainmentType result)
		{
			result = this.Contains(point);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00007268 File Offset: 0x00005468
		public ContainmentType Contains(BoundingBox box)
		{
			bool flag = true;
			foreach (Vector3 vector in box.GetCorners())
			{
				if (this.Contains(vector) == ContainmentType.Disjoint)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return ContainmentType.Contains;
			}
			double num = 0.0;
			if (this.Center.X < box.Min.X)
			{
				num += (double)((this.Center.X - box.Min.X) * (this.Center.X - box.Min.X));
			}
			else if (this.Center.X > box.Max.X)
			{
				num += (double)((this.Center.X - box.Max.X) * (this.Center.X - box.Max.X));
			}
			if (this.Center.Y < box.Min.Y)
			{
				num += (double)((this.Center.Y - box.Min.Y) * (this.Center.Y - box.Min.Y));
			}
			else if (this.Center.Y > box.Max.Y)
			{
				num += (double)((this.Center.Y - box.Max.Y) * (this.Center.Y - box.Max.Y));
			}
			if (this.Center.Z < box.Min.Z)
			{
				num += (double)((this.Center.Z - box.Min.Z) * (this.Center.Z - box.Min.Z));
			}
			else if (this.Center.Z > box.Max.Z)
			{
				num += (double)((this.Center.Z - box.Max.Z) * (this.Center.Z - box.Max.Z));
			}
			if (num <= (double)(this.Radius * this.Radius))
			{
				return ContainmentType.Intersects;
			}
			return ContainmentType.Disjoint;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00007490 File Offset: 0x00005690
		public ContainmentType Contains(BoundingFrustum frustum)
		{
			bool flag = true;
			foreach (Vector3 vector in frustum.GetCorners())
			{
				if (this.Contains(vector) == ContainmentType.Disjoint)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return ContainmentType.Contains;
			}
			if (0.0 <= (double)(this.Radius * this.Radius))
			{
				return ContainmentType.Intersects;
			}
			return ContainmentType.Disjoint;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x000074EC File Offset: 0x000056EC
		public ContainmentType Contains(BoundingSphere sphere)
		{
			float num;
			Vector3.DistanceSquared(ref sphere.Center, ref this.Center, out num);
			if (num > (sphere.Radius + this.Radius) * (sphere.Radius + this.Radius))
			{
				return ContainmentType.Disjoint;
			}
			if (num <= (this.Radius - sphere.Radius) * (this.Radius - sphere.Radius))
			{
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00007550 File Offset: 0x00005750
		public ContainmentType Contains(Vector3 point)
		{
			float num = this.Radius * this.Radius;
			float num2;
			Vector3.DistanceSquared(ref point, ref this.Center, out num2);
			if (num2 > num)
			{
				return ContainmentType.Disjoint;
			}
			if (num2 < num)
			{
				return ContainmentType.Contains;
			}
			return ContainmentType.Intersects;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00007587 File Offset: 0x00005787
		public bool Equals(BoundingSphere other)
		{
			return this.Center == other.Center && this.Radius == other.Radius;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000075AC File Offset: 0x000057AC
		public static BoundingSphere CreateFromBoundingBox(BoundingBox box)
		{
			BoundingSphere boundingSphere;
			BoundingSphere.CreateFromBoundingBox(ref box, out boundingSphere);
			return boundingSphere;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x000075C4 File Offset: 0x000057C4
		public static void CreateFromBoundingBox(ref BoundingBox box, out BoundingSphere result)
		{
			Vector3 vector = new Vector3((box.Min.X + box.Max.X) / 2f, (box.Min.Y + box.Max.Y) / 2f, (box.Min.Z + box.Max.Z) / 2f);
			float num = Vector3.Distance(vector, box.Max);
			result = new BoundingSphere(vector, num);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00007649 File Offset: 0x00005849
		public static BoundingSphere CreateFromFrustum(BoundingFrustum frustum)
		{
			return BoundingSphere.CreateFromPoints(frustum.GetCorners());
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00007658 File Offset: 0x00005858
		public static BoundingSphere CreateFromPoints(IEnumerable<Vector3> points)
		{
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = -vector;
			Vector3 vector3 = vector;
			Vector3 vector4 = -vector;
			Vector3 vector5 = vector;
			Vector3 vector6 = -vector;
			int num = 0;
			foreach (Vector3 vector7 in points)
			{
				num++;
				if (vector7.X < vector.X)
				{
					vector = vector7;
				}
				if (vector7.X > vector2.X)
				{
					vector2 = vector7;
				}
				if (vector7.Y < vector3.Y)
				{
					vector3 = vector7;
				}
				if (vector7.Y > vector4.Y)
				{
					vector4 = vector7;
				}
				if (vector7.Z < vector5.Z)
				{
					vector5 = vector7;
				}
				if (vector7.Z > vector6.Z)
				{
					vector6 = vector7;
				}
			}
			if (num == 0)
			{
				throw new ArgumentException("You should have at least one point in points.");
			}
			float num2 = Vector3.DistanceSquared(vector2, vector);
			float num3 = Vector3.DistanceSquared(vector4, vector3);
			float num4 = Vector3.DistanceSquared(vector6, vector5);
			Vector3 vector8 = vector;
			Vector3 vector9 = vector2;
			if (num3 > num2 && num3 > num4)
			{
				vector9 = vector4;
				vector8 = vector3;
			}
			if (num4 > num2 && num4 > num3)
			{
				vector9 = vector6;
				vector8 = vector5;
			}
			Vector3 vector10 = (vector8 + vector9) * 0.5f;
			float num5 = Vector3.Distance(vector9, vector10);
			float num6 = num5 * num5;
			foreach (Vector3 vector11 in points)
			{
				Vector3 vector12 = vector11 - vector10;
				float num7 = vector12.LengthSquared();
				if (num7 > num6)
				{
					float num8 = (float)Math.Sqrt((double)num7);
					Vector3 vector13 = vector12 / num8;
					vector10 = (vector10 - num5 * vector13 + vector11) / 2f;
					num5 = Vector3.Distance(vector11, vector10);
					num6 = num5 * num5;
				}
			}
			return new BoundingSphere(vector10, num5);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00007880 File Offset: 0x00005A80
		public static BoundingSphere CreateMerged(BoundingSphere original, BoundingSphere additional)
		{
			BoundingSphere boundingSphere;
			BoundingSphere.CreateMerged(ref original, ref additional, out boundingSphere);
			return boundingSphere;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0000789C File Offset: 0x00005A9C
		public static void CreateMerged(ref BoundingSphere original, ref BoundingSphere additional, out BoundingSphere result)
		{
			Vector3 vector = Vector3.Subtract(additional.Center, original.Center);
			float num = vector.Length();
			if (num <= original.Radius + additional.Radius)
			{
				if (num <= original.Radius - additional.Radius)
				{
					result = original;
					return;
				}
				if (num <= additional.Radius - original.Radius)
				{
					result = additional;
					return;
				}
			}
			float num2 = Math.Max(original.Radius - num, additional.Radius);
			float num3 = Math.Max(original.Radius + num, additional.Radius);
			vector += (num2 - num3) / (2f * vector.Length()) * vector;
			result = default(BoundingSphere);
			result.Center = original.Center + vector;
			result.Radius = (num2 + num3) / 2f;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0000797B File Offset: 0x00005B7B
		public bool Intersects(BoundingBox box)
		{
			return box.Intersects(this);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0000798A File Offset: 0x00005B8A
		public void Intersects(ref BoundingBox box, out bool result)
		{
			box.Intersects(ref this, out result);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00007994 File Offset: 0x00005B94
		public bool Intersects(BoundingFrustum frustum)
		{
			return frustum.Intersects(this);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000079A4 File Offset: 0x00005BA4
		public bool Intersects(BoundingSphere sphere)
		{
			bool flag;
			this.Intersects(ref sphere, out flag);
			return flag;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000079BC File Offset: 0x00005BBC
		public void Intersects(ref BoundingSphere sphere, out bool result)
		{
			float num;
			Vector3.DistanceSquared(ref sphere.Center, ref this.Center, out num);
			result = num <= (sphere.Radius + this.Radius) * (sphere.Radius + this.Radius);
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000079FF File Offset: 0x00005BFF
		public float? Intersects(Ray ray)
		{
			return ray.Intersects(this);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00007A0E File Offset: 0x00005C0E
		public void Intersects(ref Ray ray, out float? result)
		{
			ray.Intersects(ref this, out result);
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00007A18 File Offset: 0x00005C18
		public PlaneIntersectionType Intersects(Plane plane)
		{
			PlaneIntersectionType planeIntersectionType = PlaneIntersectionType.Front;
			this.Intersects(ref plane, out planeIntersectionType);
			return planeIntersectionType;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00007A34 File Offset: 0x00005C34
		public void Intersects(ref Plane plane, out PlaneIntersectionType result)
		{
			float num = 0f;
			Vector3.Dot(ref plane.Normal, ref this.Center, out num);
			num += plane.D;
			if (num > this.Radius)
			{
				result = PlaneIntersectionType.Front;
				return;
			}
			if (num < -this.Radius)
			{
				result = PlaneIntersectionType.Back;
				return;
			}
			result = PlaneIntersectionType.Intersecting;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00007A81 File Offset: 0x00005C81
		public override bool Equals(object obj)
		{
			return obj is BoundingSphere && this.Equals((BoundingSphere)obj);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00007A99 File Offset: 0x00005C99
		public override int GetHashCode()
		{
			return this.Center.GetHashCode() + this.Radius.GetHashCode();
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Center:",
				this.Center.ToString(),
				" Radius:",
				this.Radius.ToString(),
				"}"
			});
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00007B0A File Offset: 0x00005D0A
		public static bool operator ==(BoundingSphere a, BoundingSphere b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00007B14 File Offset: 0x00005D14
		public static bool operator !=(BoundingSphere a, BoundingSphere b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000411 RID: 1041
		public Vector3 Center;

		// Token: 0x04000412 RID: 1042
		public float Radius;
	}
}
