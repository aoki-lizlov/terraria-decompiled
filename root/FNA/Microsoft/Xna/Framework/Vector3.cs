using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200003A RID: 58
	[TypeConverter(typeof(Vector3Converter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Vector3 : IEquatable<Vector3>
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0001BB25 File Offset: 0x00019D25
		public static Vector3 Zero
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0001BB2C File Offset: 0x00019D2C
		public static Vector3 One
		{
			get
			{
				return Vector3.one;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0001BB33 File Offset: 0x00019D33
		public static Vector3 UnitX
		{
			get
			{
				return Vector3.unitX;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0001BB3A File Offset: 0x00019D3A
		public static Vector3 UnitY
		{
			get
			{
				return Vector3.unitY;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0001BB41 File Offset: 0x00019D41
		public static Vector3 UnitZ
		{
			get
			{
				return Vector3.unitZ;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0001BB48 File Offset: 0x00019D48
		public static Vector3 Up
		{
			get
			{
				return Vector3.up;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0001BB4F File Offset: 0x00019D4F
		public static Vector3 Down
		{
			get
			{
				return Vector3.down;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0001BB56 File Offset: 0x00019D56
		public static Vector3 Right
		{
			get
			{
				return Vector3.right;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0001BB5D File Offset: 0x00019D5D
		public static Vector3 Left
		{
			get
			{
				return Vector3.left;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0001BB64 File Offset: 0x00019D64
		public static Vector3 Forward
		{
			get
			{
				return Vector3.forward;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0001BB6B File Offset: 0x00019D6B
		public static Vector3 Backward
		{
			get
			{
				return Vector3.backward;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0001BB74 File Offset: 0x00019D74
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
					this.Z.ToString()
				});
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0001BBC6 File Offset: 0x00019DC6
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0001BBDD File Offset: 0x00019DDD
		public Vector3(float value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0001BBF4 File Offset: 0x00019DF4
		public Vector3(Vector2 value, float z)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0001BC15 File Offset: 0x00019E15
		public override bool Equals(object obj)
		{
			return obj is Vector3 && this.Equals((Vector3)obj);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0001BC2D File Offset: 0x00019E2D
		public bool Equals(Vector3 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0001BC5B File Offset: 0x00019E5B
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0001BC80 File Offset: 0x00019E80
		public float Length()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z));
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0001BCB2 File Offset: 0x00019EB2
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		public void Normalize()
		{
			float num = 1f / (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z));
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0001BD50 File Offset: 0x00019F50
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			stringBuilder.Append("{X:");
			stringBuilder.Append(this.X);
			stringBuilder.Append(" Y:");
			stringBuilder.Append(this.Y);
			stringBuilder.Append(" Z:");
			stringBuilder.Append(this.Z);
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		[Conditional("DEBUG")]
		internal void CheckForNaNs()
		{
			if (float.IsNaN(this.X) || float.IsNaN(this.Y) || float.IsNaN(this.Z))
			{
				throw new InvalidOperationException("Vector3 contains NaNs!");
			}
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public static Vector3 Add(Vector3 value1, Vector3 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0001BE2A File Offset: 0x0001A02A
		public static void Add(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0001BE68 File Offset: 0x0001A068
		public static Vector3 Barycentric(Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2)
		{
			return new Vector3(MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2), MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2), MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2));
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0001BEC8 File Offset: 0x0001A0C8
		public static void Barycentric(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, float amount1, float amount2, out Vector3 result)
		{
			result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
			result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
			result.Z = MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001BF38 File Offset: 0x0001A138
		public static Vector3 CatmullRom(Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount)
		{
			return new Vector3(MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount), MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount), MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount));
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public static void CatmullRom(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, float amount, out Vector3 result)
		{
			result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
			result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
			result.Z = MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001C028 File Offset: 0x0001A228
		public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
		{
			return new Vector3(MathHelper.Clamp(value1.X, min.X, max.X), MathHelper.Clamp(value1.Y, min.Y, max.Y), MathHelper.Clamp(value1.Z, min.Z, max.Z));
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0001C080 File Offset: 0x0001A280
		public static void Clamp(ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
		{
			result.X = MathHelper.Clamp(value1.X, min.X, max.X);
			result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
			result.Z = MathHelper.Clamp(value1.Z, min.Z, max.Z);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
		{
			Vector3.Cross(ref vector1, ref vector2, out vector1);
			return vector1;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0001C0F4 File Offset: 0x0001A2F4
		public static void Cross(ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
		{
			float num = vector1.Y * vector2.Z - vector2.Y * vector1.Z;
			float num2 = -(vector1.X * vector2.Z - vector2.X * vector1.Z);
			float num3 = vector1.X * vector2.Y - vector2.X * vector1.Y;
			result.X = num;
			result.Y = num2;
			result.Z = num3;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0001C16C File Offset: 0x0001A36C
		public static float Distance(Vector3 vector1, Vector3 vector2)
		{
			float num;
			Vector3.DistanceSquared(ref vector1, ref vector2, out num);
			return (float)Math.Sqrt((double)num);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0001C18C File Offset: 0x0001A38C
		public static void Distance(ref Vector3 value1, ref Vector3 value2, out float result)
		{
			Vector3.DistanceSquared(ref value1, ref value2, out result);
			result = (float)Math.Sqrt((double)result);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0001C1A4 File Offset: 0x0001A3A4
		public static float DistanceSquared(Vector3 value1, Vector3 value2)
		{
			return (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y) + (value1.Z - value2.Z) * (value1.Z - value2.Z);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0001C204 File Offset: 0x0001A404
		public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out float result)
		{
			result = (value1.X - value2.X) * (value1.X - value2.X) + (value1.Y - value2.Y) * (value1.Y - value2.Y) + (value1.Z - value2.Z) * (value1.Z - value2.Z);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0001C266 File Offset: 0x0001A466
		public static Vector3 Divide(Vector3 value1, Vector3 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0001C29C File Offset: 0x0001A49C
		public static void Divide(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0001C2D7 File Offset: 0x0001A4D7
		public static Vector3 Divide(Vector3 value1, float value2)
		{
			value1.X /= value2;
			value1.Y /= value2;
			value1.Z /= value2;
			return value1;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0001C2FE File Offset: 0x0001A4FE
		public static void Divide(ref Vector3 value1, float value2, out Vector3 result)
		{
			result.X = value1.X / value2;
			result.Y = value1.Y / value2;
			result.Z = value1.Z / value2;
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0001C32A File Offset: 0x0001A52A
		public static float Dot(Vector3 vector1, Vector3 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0001C355 File Offset: 0x0001A555
		public static void Dot(ref Vector3 vector1, ref Vector3 vector2, out float result)
		{
			result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0001C384 File Offset: 0x0001A584
		public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount)
		{
			Vector3 vector = default(Vector3);
			Vector3.Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out vector);
			return vector;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		public static void Hermite(ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, float amount, out Vector3 result)
		{
			result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
			result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
			result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0001C42B File Offset: 0x0001A62B
		public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
		{
			return new Vector3(MathHelper.Lerp(value1.X, value2.X, amount), MathHelper.Lerp(value1.Y, value2.Y, amount), MathHelper.Lerp(value1.Z, value2.Z, amount));
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0001C468 File Offset: 0x0001A668
		public static void Lerp(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
		{
			result.X = MathHelper.Lerp(value1.X, value2.X, amount);
			result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
			result.Z = MathHelper.Lerp(value1.Z, value2.Z, amount);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0001C4BD File Offset: 0x0001A6BD
		public static Vector3 Max(Vector3 value1, Vector3 value2)
		{
			return new Vector3(MathHelper.Max(value1.X, value2.X), MathHelper.Max(value1.Y, value2.Y), MathHelper.Max(value1.Z, value2.Z));
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		public static void Max(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = MathHelper.Max(value1.X, value2.X);
			result.Y = MathHelper.Max(value1.Y, value2.Y);
			result.Z = MathHelper.Max(value1.Z, value2.Z);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0001C54A File Offset: 0x0001A74A
		public static Vector3 Min(Vector3 value1, Vector3 value2)
		{
			return new Vector3(MathHelper.Min(value1.X, value2.X), MathHelper.Min(value1.Y, value2.Y), MathHelper.Min(value1.Z, value2.Z));
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0001C584 File Offset: 0x0001A784
		public static void Min(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = MathHelper.Min(value1.X, value2.X);
			result.Y = MathHelper.Min(value1.Y, value2.Y);
			result.Z = MathHelper.Min(value1.Z, value2.Z);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0001C5D6 File Offset: 0x0001A7D6
		public static Vector3 Multiply(Vector3 value1, Vector3 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0001C60C File Offset: 0x0001A80C
		public static Vector3 Multiply(Vector3 value1, float scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			value1.Z *= scaleFactor;
			return value1;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0001C633 File Offset: 0x0001A833
		public static void Multiply(ref Vector3 value1, float scaleFactor, out Vector3 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0001C65F File Offset: 0x0001A85F
		public static void Multiply(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0001C69A File Offset: 0x0001A89A
		public static Vector3 Negate(Vector3 value)
		{
			value = new Vector3(-value.X, -value.Y, -value.Z);
			return value;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0001C6B9 File Offset: 0x0001A8B9
		public static void Negate(ref Vector3 value, out Vector3 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0001C6E4 File Offset: 0x0001A8E4
		public static Vector3 Normalize(Vector3 value)
		{
			float num = 1f / (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y + value.Z * value.Z));
			return new Vector3(value.X * num, value.Y * num, value.Z * num);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0001C748 File Offset: 0x0001A948
		public static void Normalize(ref Vector3 value, out Vector3 result)
		{
			float num = 1f / (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y + value.Z * value.Z));
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
		public static Vector3 Reflect(Vector3 vector, Vector3 normal)
		{
			float num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
			Vector3 vector2;
			vector2.X = vector.X - 2f * normal.X * num;
			vector2.Y = vector.Y - 2f * normal.Y * num;
			vector2.Z = vector.Z - 2f * normal.Z * num;
			return vector2;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0001C844 File Offset: 0x0001AA44
		public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
		{
			float num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
			result.X = vector.X - 2f * normal.X * num;
			result.Y = vector.Y - 2f * normal.Y * num;
			result.Z = vector.Z - 2f * normal.Z * num;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0001C8CC File Offset: 0x0001AACC
		public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
		{
			return new Vector3(MathHelper.SmoothStep(value1.X, value2.X, amount), MathHelper.SmoothStep(value1.Y, value2.Y, amount), MathHelper.SmoothStep(value1.Z, value2.Z, amount));
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0001C90C File Offset: 0x0001AB0C
		public static void SmoothStep(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
		{
			result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
			result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
			result.Z = MathHelper.SmoothStep(value1.Z, value2.Z, amount);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0001C961 File Offset: 0x0001AB61
		public static Vector3 Subtract(Vector3 value1, Vector3 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0001C997 File Offset: 0x0001AB97
		public static void Subtract(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0001C9D2 File Offset: 0x0001ABD2
		public static Vector3 Transform(Vector3 position, Matrix matrix)
		{
			Vector3.Transform(ref position, ref matrix, out position);
			return position;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
		public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector3 result)
		{
			float num = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
			float num2 = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
			float num3 = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
			result.X = num;
			result.Y = num2;
			result.Z = num3;
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0001CA98 File Offset: 0x0001AC98
		public static void Transform(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray)
		{
			for (int i = 0; i < sourceArray.Length; i++)
			{
				Vector3 vector = sourceArray[i];
				destinationArray[i] = new Vector3(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43);
			}
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0001CB60 File Offset: 0x0001AD60
		public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix, Vector3[] destinationArray, int destinationIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				Vector3 vector = sourceArray[sourceIndex + i];
				destinationArray[destinationIndex + i] = new Vector3(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + matrix.M43);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0001CC2C File Offset: 0x0001AE2C
		public static Vector3 Transform(Vector3 value, Quaternion rotation)
		{
			Vector3 vector;
			Vector3.Transform(ref value, ref rotation, out vector);
			return vector;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0001CC48 File Offset: 0x0001AE48
		public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector3 result)
		{
			float num = 2f * (rotation.Y * value.Z - rotation.Z * value.Y);
			float num2 = 2f * (rotation.Z * value.X - rotation.X * value.Z);
			float num3 = 2f * (rotation.X * value.Y - rotation.Y * value.X);
			result.X = value.X + num * rotation.W + (rotation.Y * num3 - rotation.Z * num2);
			result.Y = value.Y + num2 * rotation.W + (rotation.Z * num - rotation.X * num3);
			result.Z = value.Z + num3 * rotation.W + (rotation.X * num2 - rotation.Y * num);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0001CD30 File Offset: 0x0001AF30
		public static void Transform(Vector3[] sourceArray, ref Quaternion rotation, Vector3[] destinationArray)
		{
			for (int i = 0; i < sourceArray.Length; i++)
			{
				Vector3 vector = sourceArray[i];
				float num = 2f * (rotation.Y * vector.Z - rotation.Z * vector.Y);
				float num2 = 2f * (rotation.Z * vector.X - rotation.X * vector.Z);
				float num3 = 2f * (rotation.X * vector.Y - rotation.Y * vector.X);
				destinationArray[i] = new Vector3(vector.X + num * rotation.W + (rotation.Y * num3 - rotation.Z * num2), vector.Y + num2 * rotation.W + (rotation.Z * num - rotation.X * num3), vector.Z + num3 * rotation.W + (rotation.X * num2 - rotation.Y * num));
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0001CE34 File Offset: 0x0001B034
		public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector3[] destinationArray, int destinationIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				Vector3 vector = sourceArray[sourceIndex + i];
				float num = 2f * (rotation.Y * vector.Z - rotation.Z * vector.Y);
				float num2 = 2f * (rotation.Z * vector.X - rotation.X * vector.Z);
				float num3 = 2f * (rotation.X * vector.Y - rotation.Y * vector.X);
				destinationArray[destinationIndex + i] = new Vector3(vector.X + num * rotation.W + (rotation.Y * num3 - rotation.Z * num2), vector.Y + num2 * rotation.W + (rotation.Z * num - rotation.X * num3), vector.Z + num3 * rotation.W + (rotation.X * num2 - rotation.Y * num));
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0001CF3A File Offset: 0x0001B13A
		public static Vector3 TransformNormal(Vector3 normal, Matrix matrix)
		{
			Vector3.TransformNormal(ref normal, ref matrix, out normal);
			return normal;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0001CF48 File Offset: 0x0001B148
		public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 result)
		{
			float num = normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31;
			float num2 = normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32;
			float num3 = normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33;
			result.X = num;
			result.Y = num2;
			result.Z = num3;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
		public static void TransformNormal(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray)
		{
			for (int i = 0; i < sourceArray.Length; i++)
			{
				Vector3 vector = sourceArray[i];
				destinationArray[i].X = vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31;
				destinationArray[i].Y = vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32;
				destinationArray[i].Z = vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33;
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0001D0B0 File Offset: 0x0001B2B0
		public static void TransformNormal(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix, Vector3[] destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("sourceArray");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("destinationArray");
			}
			if (sourceIndex + length > sourceArray.Length)
			{
				throw new ArgumentException("the combination of sourceIndex and length was greater than sourceArray.Length");
			}
			if (destinationIndex + length > destinationArray.Length)
			{
				throw new ArgumentException("destinationArray is too small to contain the result");
			}
			for (int i = 0; i < length; i++)
			{
				Vector3 vector = sourceArray[i + sourceIndex];
				destinationArray[i + destinationIndex].X = vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31;
				destinationArray[i + destinationIndex].Y = vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32;
				destinationArray[i + destinationIndex].Z = vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33;
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0001BC2D File Offset: 0x00019E2D
		public static bool operator ==(Vector3 value1, Vector3 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0001D1C7 File Offset: 0x0001B3C7
		public static bool operator !=(Vector3 value1, Vector3 value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0001BDF4 File Offset: 0x00019FF4
		public static Vector3 operator +(Vector3 value1, Vector3 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			value1.Z += value2.Z;
			return value1;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0001C69A File Offset: 0x0001A89A
		public static Vector3 operator -(Vector3 value)
		{
			value = new Vector3(-value.X, -value.Y, -value.Z);
			return value;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0001C961 File Offset: 0x0001AB61
		public static Vector3 operator -(Vector3 value1, Vector3 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			value1.Z -= value2.Z;
			return value1;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0001C5D6 File Offset: 0x0001A7D6
		public static Vector3 operator *(Vector3 value1, Vector3 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			value1.Z *= value2.Z;
			return value1;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0001C60C File Offset: 0x0001A80C
		public static Vector3 operator *(Vector3 value, float scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			value.Z *= scaleFactor;
			return value;
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0001D1D3 File Offset: 0x0001B3D3
		public static Vector3 operator *(float scaleFactor, Vector3 value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			value.Z *= scaleFactor;
			return value;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0001C266 File Offset: 0x0001A466
		public static Vector3 operator /(Vector3 value1, Vector3 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			value1.Z /= value2.Z;
			return value1;
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0001C2D7 File Offset: 0x0001A4D7
		public static Vector3 operator /(Vector3 value, float divider)
		{
			value.X /= divider;
			value.Y /= divider;
			value.Z /= divider;
			return value;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0001D1FC File Offset: 0x0001B3FC
		// Note: this type is marked as 'beforefieldinit'.
		static Vector3()
		{
		}

		// Token: 0x040005C3 RID: 1475
		private static Vector3 zero = new Vector3(0f, 0f, 0f);

		// Token: 0x040005C4 RID: 1476
		private static Vector3 one = new Vector3(1f, 1f, 1f);

		// Token: 0x040005C5 RID: 1477
		private static Vector3 unitX = new Vector3(1f, 0f, 0f);

		// Token: 0x040005C6 RID: 1478
		private static Vector3 unitY = new Vector3(0f, 1f, 0f);

		// Token: 0x040005C7 RID: 1479
		private static Vector3 unitZ = new Vector3(0f, 0f, 1f);

		// Token: 0x040005C8 RID: 1480
		private static Vector3 up = new Vector3(0f, 1f, 0f);

		// Token: 0x040005C9 RID: 1481
		private static Vector3 down = new Vector3(0f, -1f, 0f);

		// Token: 0x040005CA RID: 1482
		private static Vector3 right = new Vector3(1f, 0f, 0f);

		// Token: 0x040005CB RID: 1483
		private static Vector3 left = new Vector3(-1f, 0f, 0f);

		// Token: 0x040005CC RID: 1484
		private static Vector3 forward = new Vector3(0f, 0f, -1f);

		// Token: 0x040005CD RID: 1485
		private static Vector3 backward = new Vector3(0f, 0f, 1f);

		// Token: 0x040005CE RID: 1486
		public float X;

		// Token: 0x040005CF RID: 1487
		public float Y;

		// Token: 0x040005D0 RID: 1488
		public float Z;
	}
}
