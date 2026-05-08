using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200003B RID: 59
	[TypeConverter(typeof(Vector4Converter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Vector4 : IEquatable<Vector4>
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0001D31C File Offset: 0x0001B51C
		public static Vector4 Zero
		{
			get
			{
				return Vector4.zero;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0001D323 File Offset: 0x0001B523
		public static Vector4 One
		{
			get
			{
				return Vector4.unit;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0001D32A File Offset: 0x0001B52A
		public static Vector4 UnitX
		{
			get
			{
				return Vector4.unitX;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0001D331 File Offset: 0x0001B531
		public static Vector4 UnitY
		{
			get
			{
				return Vector4.unitY;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0001D338 File Offset: 0x0001B538
		public static Vector4 UnitZ
		{
			get
			{
				return Vector4.unitZ;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0001D33F File Offset: 0x0001B53F
		public static Vector4 UnitW
		{
			get
			{
				return Vector4.unitW;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0001D348 File Offset: 0x0001B548
		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(new string[]
				{
					this.X.ToString(),
					" ",
					this.Y.ToString(),
					" ",
					this.Z.ToString(),
					" ",
					this.W.ToString()
				});
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		public Vector4(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0001D3CF File Offset: 0x0001B5CF
		public Vector4(Vector2 value, float z, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0001D3F7 File Offset: 0x0001B5F7
		public Vector4(Vector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0001D424 File Offset: 0x0001B624
		public Vector4(float value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
			this.W = value;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0001D442 File Offset: 0x0001B642
		public override bool Equals(object obj)
		{
			return obj is Vector4 && this.Equals((Vector4)obj);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0001D45A File Offset: 0x0001B65A
		public bool Equals(Vector4 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0001D496 File Offset: 0x0001B696
		public override int GetHashCode()
		{
			return this.W.GetHashCode() + this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0001D4C7 File Offset: 0x0001B6C7
		public float Length()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W));
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0001D507 File Offset: 0x0001B707
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0001D540 File Offset: 0x0001B740
		public void Normalize()
		{
			float num = 1f / (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W));
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0001D5CC File Offset: 0x0001B7CC
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X:",
				this.X.ToString(),
				" Y:",
				this.Y.ToString(),
				" Z:",
				this.Z.ToString(),
				" W:",
				this.W.ToString(),
				"}"
			});
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0001D648 File Offset: 0x0001B848
		[Conditional("DEBUG")]
		internal void CheckForNaNs()
		{
			if (float.IsNaN(this.X) || float.IsNaN(this.Y) || float.IsNaN(this.Z) || float.IsNaN(this.W))
			{
				throw new InvalidOperationException("Vector4 contains NaNs!");
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0001D694 File Offset: 0x0001B894
		public static Vector4 Add(Vector4 value1, Vector4 value2)
		{
			value1.W += value2.W;
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		public static void Add(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.W = value1.W + value2.W;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0001D744 File Offset: 0x0001B944
		public static Vector4 Barycentric(Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2)
		{
			return new Vector4(MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2), MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2), MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2), MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2));
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		public static void Barycentric(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, float amount1, float amount2, out Vector4 result)
		{
			result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
			result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
			result.Z = MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2);
			result.W = MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0001D854 File Offset: 0x0001BA54
		public static Vector4 CatmullRom(Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount)
		{
			return new Vector4(MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount), MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount), MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount), MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount));
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0001D8E4 File Offset: 0x0001BAE4
		public static void CatmullRom(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, float amount, out Vector4 result)
		{
			result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
			result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
			result.Z = MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount);
			result.W = MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0001D98C File Offset: 0x0001BB8C
		public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
		{
			return new Vector4(MathHelper.Clamp(value1.X, min.X, max.X), MathHelper.Clamp(value1.Y, min.Y, max.Y), MathHelper.Clamp(value1.Z, min.Z, max.Z), MathHelper.Clamp(value1.W, min.W, max.W));
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0001D9FC File Offset: 0x0001BBFC
		public static void Clamp(ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
		{
			result.X = MathHelper.Clamp(value1.X, min.X, max.X);
			result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
			result.Z = MathHelper.Clamp(value1.Z, min.Z, max.Z);
			result.W = MathHelper.Clamp(value1.W, min.W, max.W);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0001DA7D File Offset: 0x0001BC7D
		public static float Distance(Vector4 value1, Vector4 value2)
		{
			return (float)Math.Sqrt((double)Vector4.DistanceSquared(value1, value2));
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0001DA8D File Offset: 0x0001BC8D
		public static void Distance(ref Vector4 value1, ref Vector4 value2, out float result)
		{
			result = (float)Math.Sqrt((double)Vector4.DistanceSquared(value1, value2));
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0001DAAC File Offset: 0x0001BCAC
		public static float DistanceSquared(Vector4 value1, Vector4 value2)
		{
			return (value1.W - value2.W) * (value1.W - value2.W) + (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y) + (value1.Z - value2.Z) * (value1.Z - value2.Z);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0001DB28 File Offset: 0x0001BD28
		public static void DistanceSquared(ref Vector4 value1, ref Vector4 value2, out float result)
		{
			result = (value1.W - value2.W) * (value1.W - value2.W) + (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y) + (value1.Z - value2.Z) * (value1.Z - value2.Z);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		public static Vector4 Divide(Vector4 value1, Vector4 value2)
		{
			value1.W /= value2.W;
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0001DBFA File Offset: 0x0001BDFA
		public static Vector4 Divide(Vector4 value1, float divider)
		{
			value1.W /= divider;
			value1.X /= divider;
			value1.Y /= divider;
			value1.Z /= divider;
			return value1;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0001DC2D File Offset: 0x0001BE2D
		public static void Divide(ref Vector4 value1, float divider, out Vector4 result)
		{
			result.W = value1.W / divider;
			result.X = value1.X / divider;
			result.Y = value1.Y / divider;
			result.Z = value1.Z / divider;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0001DC68 File Offset: 0x0001BE68
		public static void Divide(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.W = value1.W / value2.W;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0001DCC1 File Offset: 0x0001BEC1
		public static float Dot(Vector4 vector1, Vector4 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0001DCFA File Offset: 0x0001BEFA
		public static void Dot(ref Vector4 vector1, ref Vector4 vector2, out float result)
		{
			result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0001DD38 File Offset: 0x0001BF38
		public static Vector4 Hermite(Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, float amount)
		{
			return new Vector4(MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount), MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount), MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount), MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount));
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
		public static void Hermite(ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, float amount, out Vector4 result)
		{
			result.W = MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount);
			result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
			result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
			result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0001DE70 File Offset: 0x0001C070
		public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4(MathHelper.Lerp(value1.X, value2.X, amount), MathHelper.Lerp(value1.Y, value2.Y, amount), MathHelper.Lerp(value1.Z, value2.Z, amount), MathHelper.Lerp(value1.W, value2.W, amount));
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0001DECC File Offset: 0x0001C0CC
		public static void Lerp(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
		{
			result.X = MathHelper.Lerp(value1.X, value2.X, amount);
			result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
			result.Z = MathHelper.Lerp(value1.Z, value2.Z, amount);
			result.W = MathHelper.Lerp(value1.W, value2.W, amount);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0001DF3C File Offset: 0x0001C13C
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			return new Vector4(MathHelper.Max(value1.X, value2.X), MathHelper.Max(value1.Y, value2.Y), MathHelper.Max(value1.Z, value2.Z), MathHelper.Max(value1.W, value2.W));
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0001DF94 File Offset: 0x0001C194
		public static void Max(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = MathHelper.Max(value1.X, value2.X);
			result.Y = MathHelper.Max(value1.Y, value2.Y);
			result.Z = MathHelper.Max(value1.Z, value2.Z);
			result.W = MathHelper.Max(value1.W, value2.W);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0001E000 File Offset: 0x0001C200
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			return new Vector4(MathHelper.Min(value1.X, value2.X), MathHelper.Min(value1.Y, value2.Y), MathHelper.Min(value1.Z, value2.Z), MathHelper.Min(value1.W, value2.W));
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0001E058 File Offset: 0x0001C258
		public static void Min(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = MathHelper.Min(value1.X, value2.X);
			result.Y = MathHelper.Min(value1.Y, value2.Y);
			result.Z = MathHelper.Min(value1.Z, value2.Z);
			result.W = MathHelper.Min(value1.W, value2.W);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0001E0C4 File Offset: 0x0001C2C4
		public static Vector4 Multiply(Vector4 value1, Vector4 value2)
		{
			value1.W *= value2.W;
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0001E116 File Offset: 0x0001C316
		public static Vector4 Multiply(Vector4 value1, float scaleFactor)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0001E149 File Offset: 0x0001C349
		public static void Multiply(ref Vector4 value1, float scaleFactor, out Vector4 result)
		{
			result.W = value1.W * scaleFactor;
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0001E184 File Offset: 0x0001C384
		public static void Multiply(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.W = value1.W * value2.W;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0001E1DD File Offset: 0x0001C3DD
		public static Vector4 Negate(Vector4 value)
		{
			value = new Vector4(-value.X, -value.Y, -value.Z, -value.W);
			return value;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0001E203 File Offset: 0x0001C403
		public static void Negate(ref Vector4 value, out Vector4 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0001E23C File Offset: 0x0001C43C
		public static Vector4 Normalize(Vector4 vector)
		{
			float num = 1f / (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W));
			return new Vector4(vector.X * num, vector.Y * num, vector.Z * num, vector.W * num);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0001E2B4 File Offset: 0x0001C4B4
		public static void Normalize(ref Vector4 vector, out Vector4 result)
		{
			float num = 1f / (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W));
			result.X = vector.X * num;
			result.Y = vector.Y * num;
			result.Z = vector.Z * num;
			result.W = vector.W * num;
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0001E340 File Offset: 0x0001C540
		public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4(MathHelper.SmoothStep(value1.X, value2.X, amount), MathHelper.SmoothStep(value1.Y, value2.Y, amount), MathHelper.SmoothStep(value1.Z, value2.Z, amount), MathHelper.SmoothStep(value1.W, value2.W, amount));
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0001E39C File Offset: 0x0001C59C
		public static void SmoothStep(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
		{
			result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
			result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
			result.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
			result.W = MathHelper.SmoothStep(value1.W, value2.W, amount);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0001E40C File Offset: 0x0001C60C
		public static Vector4 Subtract(Vector4 value1, Vector4 value2)
		{
			value1.W -= value2.W;
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0001E460 File Offset: 0x0001C660
		public static void Subtract(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.W = value1.W - value2.W;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0001E4BC File Offset: 0x0001C6BC
		public static Vector4 Transform(Vector2 position, Matrix matrix)
		{
			Vector4 vector;
			Vector4.Transform(ref position, ref matrix, out vector);
			return vector;
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0001E4D8 File Offset: 0x0001C6D8
		public static Vector4 Transform(Vector3 position, Matrix matrix)
		{
			Vector4 vector;
			Vector4.Transform(ref position, ref matrix, out vector);
			return vector;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0001E4F1 File Offset: 0x0001C6F1
		public static Vector4 Transform(Vector4 vector, Matrix matrix)
		{
			Vector4.Transform(ref vector, ref matrix, out vector);
			return vector;
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0001E500 File Offset: 0x0001C700
		public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector4 result)
		{
			result = new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0001E5A0 File Offset: 0x0001C7A0
		public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector4 result)
		{
			float num = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
			float num2 = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
			float num3 = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
			float num4 = position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44;
			result.X = num;
			result.Y = num2;
			result.Z = num3;
			result.W = num4;
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0001E690 File Offset: 0x0001C890
		public static void Transform(ref Vector4 vector, ref Matrix matrix, out Vector4 result)
		{
			float num = vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41;
			float num2 = vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42;
			float num3 = vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43;
			float num4 = vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44;
			result.X = num;
			result.Y = num2;
			result.Z = num3;
			result.W = num4;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0001E79C File Offset: 0x0001C99C
		public static void Transform(Vector4[] sourceArray, ref Matrix matrix, Vector4[] destinationArray)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (destinationArray.Length < sourceArray.Length)
			{
				throw new ArgumentException("destinationArray is too small to contain the result.");
			}
			for (int i = 0; i < sourceArray.Length; i++)
			{
				Vector4.Transform(ref sourceArray[i], ref matrix, out destinationArray[i]);
			}
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0001E7FC File Offset: 0x0001C9FC
		public static void Transform(Vector4[] sourceArray, int sourceIndex, ref Matrix matrix, Vector4[] destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (destinationIndex + length > destinationArray.Length)
			{
				throw new ArgumentException("destinationArray is too small to contain the result.");
			}
			if (sourceIndex + length > sourceArray.Length)
			{
				throw new ArgumentException("The combination of sourceIndex and length was greater than sourceArray.Length.");
			}
			for (int i = 0; i < length; i++)
			{
				Vector4.Transform(ref sourceArray[i + sourceIndex], ref matrix, out destinationArray[i + destinationIndex]);
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0001E874 File Offset: 0x0001CA74
		public static Vector4 Transform(Vector2 value, Quaternion rotation)
		{
			Vector4 vector;
			Vector4.Transform(ref value, ref rotation, out vector);
			return vector;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0001E890 File Offset: 0x0001CA90
		public static Vector4 Transform(Vector3 value, Quaternion rotation)
		{
			Vector4 vector;
			Vector4.Transform(ref value, ref rotation, out vector);
			return vector;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0001E8AC File Offset: 0x0001CAAC
		public static Vector4 Transform(Vector4 value, Quaternion rotation)
		{
			Vector4 vector;
			Vector4.Transform(ref value, ref rotation, out vector);
			return vector;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0001E8C8 File Offset: 0x0001CAC8
		public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector4 result)
		{
			double num = (double)(rotation.X + rotation.X);
			double num2 = (double)(rotation.Y + rotation.Y);
			double num3 = (double)(rotation.Z + rotation.Z);
			double num4 = (double)rotation.W * num;
			double num5 = (double)rotation.W * num2;
			double num6 = (double)rotation.W * num3;
			double num7 = (double)rotation.X * num;
			double num8 = (double)rotation.X * num2;
			double num9 = (double)rotation.X * num3;
			double num10 = (double)rotation.Y * num2;
			double num11 = (double)rotation.Y * num3;
			double num12 = (double)rotation.Z * num3;
			result.X = (float)((double)value.X * (1.0 - num10 - num12) + (double)value.Y * (num8 - num6));
			result.Y = (float)((double)value.X * (num8 + num6) + (double)value.Y * (1.0 - num7 - num12));
			result.Z = (float)((double)value.X * (num9 - num5) + (double)value.Y * (num11 + num4));
			result.W = 1f;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
		public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector4 result)
		{
			double num = (double)(rotation.X + rotation.X);
			double num2 = (double)(rotation.Y + rotation.Y);
			double num3 = (double)(rotation.Z + rotation.Z);
			double num4 = (double)rotation.W * num;
			double num5 = (double)rotation.W * num2;
			double num6 = (double)rotation.W * num3;
			double num7 = (double)rotation.X * num;
			double num8 = (double)rotation.X * num2;
			double num9 = (double)rotation.X * num3;
			double num10 = (double)rotation.Y * num2;
			double num11 = (double)rotation.Y * num3;
			double num12 = (double)rotation.Z * num3;
			result.X = (float)((double)value.X * (1.0 - num10 - num12) + (double)value.Y * (num8 - num6) + (double)value.Z * (num9 + num5));
			result.Y = (float)((double)value.X * (num8 + num6) + (double)value.Y * (1.0 - num7 - num12) + (double)value.Z * (num11 - num4));
			result.Z = (float)((double)value.X * (num9 - num5) + (double)value.Y * (num11 + num4) + (double)value.Z * (1.0 - num7 - num10));
			result.W = 1f;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0001EB3C File Offset: 0x0001CD3C
		public static void Transform(ref Vector4 value, ref Quaternion rotation, out Vector4 result)
		{
			double num = (double)(rotation.X + rotation.X);
			double num2 = (double)(rotation.Y + rotation.Y);
			double num3 = (double)(rotation.Z + rotation.Z);
			double num4 = (double)rotation.W * num;
			double num5 = (double)rotation.W * num2;
			double num6 = (double)rotation.W * num3;
			double num7 = (double)rotation.X * num;
			double num8 = (double)rotation.X * num2;
			double num9 = (double)rotation.X * num3;
			double num10 = (double)rotation.Y * num2;
			double num11 = (double)rotation.Y * num3;
			double num12 = (double)rotation.Z * num3;
			result.X = (float)((double)value.X * (1.0 - num10 - num12) + (double)value.Y * (num8 - num6) + (double)value.Z * (num9 + num5));
			result.Y = (float)((double)value.X * (num8 + num6) + (double)value.Y * (1.0 - num7 - num12) + (double)value.Z * (num11 - num4));
			result.Z = (float)((double)value.X * (num9 - num5) + (double)value.Y * (num11 + num4) + (double)value.Z * (1.0 - num7 - num10));
			result.W = value.W;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0001EC90 File Offset: 0x0001CE90
		public static void Transform(Vector4[] sourceArray, ref Quaternion rotation, Vector4[] destinationArray)
		{
			if (sourceArray == null)
			{
				throw new ArgumentException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentException("destinationArray");
			}
			if (destinationArray.Length < sourceArray.Length)
			{
				throw new ArgumentException("destinationArray is too small to contain the result.");
			}
			for (int i = 0; i < sourceArray.Length; i++)
			{
				Vector4.Transform(ref sourceArray[i], ref rotation, out destinationArray[i]);
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0001ECF0 File Offset: 0x0001CEF0
		public static void Transform(Vector4[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector4[] destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentException("destinationArray");
			}
			if (destinationIndex + length > destinationArray.Length)
			{
				throw new ArgumentException("destinationArray is too small to contain the result.");
			}
			if (sourceIndex + length > sourceArray.Length)
			{
				throw new ArgumentException("The combination of sourceIndex and length was greater than sourceArray.Length.");
			}
			for (int i = 0; i < length; i++)
			{
				Vector4.Transform(ref sourceArray[i + sourceIndex], ref rotation, out destinationArray[i + destinationIndex]);
			}
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0001ED68 File Offset: 0x0001CF68
		public static Vector4 operator -(Vector4 value)
		{
			return new Vector4(-value.X, -value.Y, -value.Z, -value.W);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0001D45A File Offset: 0x0001B65A
		public static bool operator ==(Vector4 value1, Vector4 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.W == value2.W;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0001ED8B File Offset: 0x0001CF8B
		public static bool operator !=(Vector4 value1, Vector4 value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0001ED98 File Offset: 0x0001CF98
		public static Vector4 operator +(Vector4 value1, Vector4 value2)
		{
			value1.W += value2.W;
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0001EDEC File Offset: 0x0001CFEC
		public static Vector4 operator -(Vector4 value1, Vector4 value2)
		{
			value1.W -= value2.W;
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0001EE40 File Offset: 0x0001D040
		public static Vector4 operator *(Vector4 value1, Vector4 value2)
		{
			value1.W *= value2.W;
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0001E116 File Offset: 0x0001C316
		public static Vector4 operator *(Vector4 value1, float scaleFactor)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0001EE92 File Offset: 0x0001D092
		public static Vector4 operator *(float scaleFactor, Vector4 value1)
		{
			value1.W *= scaleFactor;
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0001EEC8 File Offset: 0x0001D0C8
		public static Vector4 operator /(Vector4 value1, Vector4 value2)
		{
			value1.W /= value2.W;
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0001DBFA File Offset: 0x0001BDFA
		public static Vector4 operator /(Vector4 value1, float divider)
		{
			value1.W /= divider;
			value1.X /= divider;
			value1.Y /= divider;
			value1.Z /= divider;
			return value1;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x0001EF1C File Offset: 0x0001D11C
		// Note: this type is marked as 'beforefieldinit'.
		static Vector4()
		{
		}

		// Token: 0x040005D1 RID: 1489
		public float X;

		// Token: 0x040005D2 RID: 1490
		public float Y;

		// Token: 0x040005D3 RID: 1491
		public float Z;

		// Token: 0x040005D4 RID: 1492
		public float W;

		// Token: 0x040005D5 RID: 1493
		private static Vector4 zero = default(Vector4);

		// Token: 0x040005D6 RID: 1494
		private static Vector4 unit = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x040005D7 RID: 1495
		private static Vector4 unitX = new Vector4(1f, 0f, 0f, 0f);

		// Token: 0x040005D8 RID: 1496
		private static Vector4 unitY = new Vector4(0f, 1f, 0f, 0f);

		// Token: 0x040005D9 RID: 1497
		private static Vector4 unitZ = new Vector4(0f, 0f, 1f, 0f);

		// Token: 0x040005DA RID: 1498
		private static Vector4 unitW = new Vector4(0f, 0f, 0f, 1f);
	}
}
