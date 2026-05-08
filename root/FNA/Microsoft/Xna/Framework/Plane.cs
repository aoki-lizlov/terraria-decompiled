using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200002D RID: 45
	[TypeConverter(typeof(PlaneConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Plane : IEquatable<Plane>
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0001871B File Offset: 0x0001691B
		internal string DebugDisplayString
		{
			get
			{
				return this.Normal.DebugDisplayString + " " + this.D.ToString();
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0001873D File Offset: 0x0001693D
		public Plane(Vector4 value)
		{
			this = new Plane(new Vector3(value.X, value.Y, value.Z), value.W);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x00018762 File Offset: 0x00016962
		public Plane(Vector3 normal, float d)
		{
			this.Normal = normal;
			this.D = d;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00018774 File Offset: 0x00016974
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Vector3.Normalize(ref vector3, out this.Normal);
			this.D = -Vector3.Dot(this.Normal, a);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000187B7 File Offset: 0x000169B7
		public Plane(float a, float b, float c, float d)
		{
			this = new Plane(new Vector3(a, b, c), d);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000187CC File Offset: 0x000169CC
		public float Dot(Vector4 value)
		{
			return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D * value.W;
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00018820 File Offset: 0x00016A20
		public void Dot(ref Vector4 value, out float result)
		{
			result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D * value.W;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00018878 File Offset: 0x00016A78
		public float DotCoordinate(Vector3 value)
		{
			return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000188C4 File Offset: 0x00016AC4
		public void DotCoordinate(ref Vector3 value, out float result)
		{
			result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x00018912 File Offset: 0x00016B12
		public float DotNormal(Vector3 value)
		{
			return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0001894C File Offset: 0x00016B4C
		public void DotNormal(ref Vector3 value, out float result)
		{
			result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z;
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00018988 File Offset: 0x00016B88
		public void Normalize()
		{
			float num = this.Normal.Length();
			float num2 = 1f / num;
			Vector3.Multiply(ref this.Normal, num2, out this.Normal);
			this.D *= num2;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000189C9 File Offset: 0x00016BC9
		public PlaneIntersectionType Intersects(BoundingBox box)
		{
			return box.Intersects(this);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000189D8 File Offset: 0x00016BD8
		public void Intersects(ref BoundingBox box, out PlaneIntersectionType result)
		{
			box.Intersects(ref this, out result);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000189E2 File Offset: 0x00016BE2
		public PlaneIntersectionType Intersects(BoundingSphere sphere)
		{
			return sphere.Intersects(this);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000189F1 File Offset: 0x00016BF1
		public void Intersects(ref BoundingSphere sphere, out PlaneIntersectionType result)
		{
			sphere.Intersects(ref this, out result);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000189FB File Offset: 0x00016BFB
		public PlaneIntersectionType Intersects(BoundingFrustum frustum)
		{
			return frustum.Intersects(this);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00018A0C File Offset: 0x00016C0C
		internal PlaneIntersectionType Intersects(ref Vector3 point)
		{
			float num;
			this.DotCoordinate(ref point, out num);
			if (num > 0f)
			{
				return PlaneIntersectionType.Front;
			}
			if (num < 0f)
			{
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Intersecting;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x00018A38 File Offset: 0x00016C38
		public static Plane Normalize(Plane value)
		{
			Plane plane;
			Plane.Normalize(ref value, out plane);
			return plane;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00018A50 File Offset: 0x00016C50
		public static void Normalize(ref Plane value, out Plane result)
		{
			float num = value.Normal.Length();
			float num2 = 1f / num;
			Vector3.Multiply(ref value.Normal, num2, out result.Normal);
			result.D = value.D * num2;
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00018A94 File Offset: 0x00016C94
		public static Plane Transform(Plane plane, Matrix matrix)
		{
			Plane plane2;
			Plane.Transform(ref plane, ref matrix, out plane2);
			return plane2;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00018AB0 File Offset: 0x00016CB0
		public static void Transform(ref Plane plane, ref Matrix matrix, out Plane result)
		{
			Matrix matrix2;
			Matrix.Invert(ref matrix, out matrix2);
			Matrix.Transpose(ref matrix2, out matrix2);
			Vector4 vector = new Vector4(plane.Normal, plane.D);
			Vector4 vector2;
			Vector4.Transform(ref vector, ref matrix2, out vector2);
			result = new Plane(vector2);
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00018AF8 File Offset: 0x00016CF8
		public static Plane Transform(Plane plane, Quaternion rotation)
		{
			Plane plane2;
			Plane.Transform(ref plane, ref rotation, out plane2);
			return plane2;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00018B11 File Offset: 0x00016D11
		public static void Transform(ref Plane plane, ref Quaternion rotation, out Plane result)
		{
			Vector3.Transform(ref plane.Normal, ref rotation, out result.Normal);
			result.D = plane.D;
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00018B31 File Offset: 0x00016D31
		public static bool operator !=(Plane plane1, Plane plane2)
		{
			return !plane1.Equals(plane2);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00018B3E File Offset: 0x00016D3E
		public static bool operator ==(Plane plane1, Plane plane2)
		{
			return plane1.Equals(plane2);
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00018B48 File Offset: 0x00016D48
		public override bool Equals(object obj)
		{
			return obj is Plane && this.Equals((Plane)obj);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00018B60 File Offset: 0x00016D60
		public bool Equals(Plane other)
		{
			return this.Normal == other.Normal && this.D == other.D;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00018B85 File Offset: 0x00016D85
		public override int GetHashCode()
		{
			return this.Normal.GetHashCode() ^ this.D.GetHashCode();
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00018BA4 File Offset: 0x00016DA4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{Normal:",
				this.Normal.ToString(),
				" D:",
				this.D.ToString(),
				"}"
			});
		}

		// Token: 0x040005A1 RID: 1441
		public Vector3 Normal;

		// Token: 0x040005A2 RID: 1442
		public float D;
	}
}
