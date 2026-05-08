using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ReLogic.Utilities
{
	// Token: 0x02000009 RID: 9
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Vector2D : IEquatable<Vector2D>
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002BBE File Offset: 0x00000DBE
		public static Vector2D Zero
		{
			get
			{
				return Vector2D.zeroVector;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002BC5 File Offset: 0x00000DC5
		public static Vector2D One
		{
			get
			{
				return Vector2D.unitVector;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002BCC File Offset: 0x00000DCC
		public static Vector2D UnitX
		{
			get
			{
				return Vector2D.unitXVector;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002BD3 File Offset: 0x00000DD3
		public static Vector2D UnitY
		{
			get
			{
				return Vector2D.unitYVector;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002BDA File Offset: 0x00000DDA
		internal string DebugDisplayString
		{
			get
			{
				return this.X.ToString() + " " + this.Y.ToString();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002BFC File Offset: 0x00000DFC
		public Vector2D(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002C0C File Offset: 0x00000E0C
		public Vector2D(double value)
		{
			this.X = value;
			this.Y = value;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002C1C File Offset: 0x00000E1C
		public override bool Equals(object obj)
		{
			return obj is Vector2D && this.Equals((Vector2D)obj);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002C34 File Offset: 0x00000E34
		public bool Equals(Vector2D other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C54 File Offset: 0x00000E54
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C6D File Offset: 0x00000E6D
		public double Length()
		{
			return Math.Sqrt(this.X * this.X + this.Y * this.Y);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C8F File Offset: 0x00000E8F
		public double LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CAC File Offset: 0x00000EAC
		public void Normalize()
		{
			double num = 1.0 / Math.Sqrt(this.X * this.X + this.Y * this.Y);
			this.X *= num;
			this.Y *= num;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D00 File Offset: 0x00000F00
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{X:",
				this.X.ToString(),
				" Y:",
				this.Y.ToString(),
				"}"
			});
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static Vector2D Add(Vector2D value1, Vector2D value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D71 File Offset: 0x00000F71
		public static void Add(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D99 File Offset: 0x00000F99
		public static Vector2D Barycentric(Vector2D value1, Vector2D value2, Vector2D value3, double amount1, double amount2)
		{
			return new Vector2D(Vector2D.Barycentric(value1.X, value2.X, value3.X, amount1, amount2), Vector2D.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public static void Barycentric(ref Vector2D value1, ref Vector2D value2, ref Vector2D value3, double amount1, double amount2, out Vector2D result)
		{
			result.X = Vector2D.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
			result.Y = Vector2D.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E24 File Offset: 0x00001024
		public static Vector2D CatmullRom(Vector2D value1, Vector2D value2, Vector2D value3, Vector2D value4, double amount)
		{
			return new Vector2D(Vector2D.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount), Vector2D.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E74 File Offset: 0x00001074
		public static void CatmullRom(ref Vector2D value1, ref Vector2D value2, ref Vector2D value3, ref Vector2D value4, double amount, out Vector2D result)
		{
			result.X = Vector2D.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
			result.Y = Vector2D.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002ECD File Offset: 0x000010CD
		public static Vector2D Clamp(Vector2D value1, Vector2D min, Vector2D max)
		{
			return new Vector2D(Vector2D.Clamp(value1.X, min.X, max.X), Vector2D.Clamp(value1.Y, min.Y, max.Y));
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F02 File Offset: 0x00001102
		public static void Clamp(ref Vector2D value1, ref Vector2D min, ref Vector2D max, out Vector2D result)
		{
			result.X = Vector2D.Clamp(value1.X, min.X, max.X);
			result.Y = Vector2D.Clamp(value1.Y, min.Y, max.Y);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F40 File Offset: 0x00001140
		public static double Distance(Vector2D value1, Vector2D value2)
		{
			double num = value1.X - value2.X;
			double num2 = value1.Y - value2.Y;
			return Math.Sqrt(num * num + num2 * num2);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F74 File Offset: 0x00001174
		public static void Distance(ref Vector2D value1, ref Vector2D value2, out double result)
		{
			double num = value1.X - value2.X;
			double num2 = value1.Y - value2.Y;
			result = Math.Sqrt(num * num + num2 * num2);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002FAC File Offset: 0x000011AC
		public static double DistanceSquared(Vector2D value1, Vector2D value2)
		{
			double num = value1.X - value2.X;
			double num2 = value1.Y - value2.Y;
			return num * num + num2 * num2;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FDC File Offset: 0x000011DC
		public static void DistanceSquared(ref Vector2D value1, ref Vector2D value2, out double result)
		{
			double num = value1.X - value2.X;
			double num2 = value1.Y - value2.Y;
			result = num * num + num2 * num2;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000300E File Offset: 0x0000120E
		public static Vector2D Divide(Vector2D value1, Vector2D value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003033 File Offset: 0x00001233
		public static void Divide(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000305C File Offset: 0x0000125C
		public static Vector2D Divide(Vector2D value1, double divider)
		{
			double num = 1.0 / divider;
			value1.X *= num;
			value1.Y *= num;
			return value1;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003090 File Offset: 0x00001290
		public static void Divide(ref Vector2D value1, double divider, out Vector2D result)
		{
			double num = 1.0 / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000030C5 File Offset: 0x000012C5
		public static double Dot(Vector2D value1, Vector2D value2)
		{
			return value1.X * value2.X + value1.Y * value2.Y;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030E2 File Offset: 0x000012E2
		public static void Dot(ref Vector2D value1, ref Vector2D value2, out double result)
		{
			result = value1.X * value2.X + value1.Y * value2.Y;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003101 File Offset: 0x00001301
		public static double Cross(Vector2D value1, Vector2D value2)
		{
			return value1.X * value2.Y - value1.Y * value2.X;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003120 File Offset: 0x00001320
		public static Vector2D Hermite(Vector2D value1, Vector2D tangent1, Vector2D value2, Vector2D tangent2, double amount)
		{
			Vector2D vector2D = default(Vector2D);
			Vector2D.Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out vector2D);
			return vector2D;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003148 File Offset: 0x00001348
		public static void Hermite(ref Vector2D value1, ref Vector2D tangent1, ref Vector2D value2, ref Vector2D tangent2, double amount, out Vector2D result)
		{
			result.X = Vector2D.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
			result.Y = Vector2D.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031A1 File Offset: 0x000013A1
		public static Vector2D Lerp(Vector2D value1, Vector2D value2, double amount)
		{
			return new Vector2D(Vector2D.Lerp(value1.X, value2.X, amount), Vector2D.Lerp(value1.Y, value2.Y, amount));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031CC File Offset: 0x000013CC
		public static void Lerp(ref Vector2D value1, ref Vector2D value2, double amount, out Vector2D result)
		{
			result.X = Vector2D.Lerp(value1.X, value2.X, amount);
			result.Y = Vector2D.Lerp(value1.Y, value2.Y, amount);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031FE File Offset: 0x000013FE
		public static Vector2D Max(Vector2D value1, Vector2D value2)
		{
			return new Vector2D((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003240 File Offset: 0x00001440
		public static void Max(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
		{
			result.X = ((value1.X > value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003291 File Offset: 0x00001491
		public static Vector2D Min(Vector2D value1, Vector2D value2)
		{
			return new Vector2D((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032D0 File Offset: 0x000014D0
		public static void Min(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
		{
			result.X = ((value1.X < value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003321 File Offset: 0x00001521
		public static Vector2D Multiply(Vector2D value1, Vector2D value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003346 File Offset: 0x00001546
		public static Vector2D Multiply(Vector2D value1, double scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			return value1;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003361 File Offset: 0x00001561
		public static void Multiply(ref Vector2D value1, double scaleFactor, out Vector2D result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000337F File Offset: 0x0000157F
		public static void Multiply(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000033A7 File Offset: 0x000015A7
		public static Vector2D Negate(Vector2D value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000033C6 File Offset: 0x000015C6
		public static void Negate(ref Vector2D value, out Vector2D result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000033E4 File Offset: 0x000015E4
		public static Vector2D Normalize(Vector2D value)
		{
			double num = 1.0 / Math.Sqrt(value.X * value.X + value.Y * value.Y);
			value.X *= num;
			value.Y *= num;
			return value;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003438 File Offset: 0x00001638
		public static void Normalize(ref Vector2D value, out Vector2D result)
		{
			double num = 1.0 / Math.Sqrt(value.X * value.X + value.Y * value.Y);
			result.X = value.X * num;
			result.Y = value.Y * num;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000348C File Offset: 0x0000168C
		public static Vector2D Reflect(Vector2D vector, Vector2D normal)
		{
			double num = 2.0 * (vector.X * normal.X + vector.Y * normal.Y);
			Vector2D vector2D;
			vector2D.X = vector.X - normal.X * num;
			vector2D.Y = vector.Y - normal.Y * num;
			return vector2D;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000034EC File Offset: 0x000016EC
		public static void Reflect(ref Vector2D vector, ref Vector2D normal, out Vector2D result)
		{
			double num = 2.0 * (vector.X * normal.X + vector.Y * normal.Y);
			result.X = vector.X - normal.X * num;
			result.Y = vector.Y - normal.Y * num;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003549 File Offset: 0x00001749
		public static Vector2D SmoothStep(Vector2D value1, Vector2D value2, double amount)
		{
			return new Vector2D(Vector2D.SmoothStep(value1.X, value2.X, amount), Vector2D.SmoothStep(value1.Y, value2.Y, amount));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003574 File Offset: 0x00001774
		public static void SmoothStep(ref Vector2D value1, ref Vector2D value2, double amount, out Vector2D result)
		{
			result.X = Vector2D.SmoothStep(value1.X, value2.X, amount);
			result.Y = Vector2D.SmoothStep(value1.Y, value2.Y, amount);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000035A6 File Offset: 0x000017A6
		public static Vector2D Subtract(Vector2D value1, Vector2D value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000035CB File Offset: 0x000017CB
		public static void Subtract(ref Vector2D value1, ref Vector2D value2, out Vector2D result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000033A7 File Offset: 0x000015A7
		public static Vector2D operator -(Vector2D value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002C34 File Offset: 0x00000E34
		public static bool operator ==(Vector2D value1, Vector2D value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000035F3 File Offset: 0x000017F3
		public static bool operator !=(Vector2D value1, Vector2D value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static Vector2D operator +(Vector2D value1, Vector2D value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000035A6 File Offset: 0x000017A6
		public static Vector2D operator -(Vector2D value1, Vector2D value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003321 File Offset: 0x00001521
		public static Vector2D operator *(Vector2D value1, Vector2D value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003346 File Offset: 0x00001546
		public static Vector2D operator *(Vector2D value, double scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000035FF File Offset: 0x000017FF
		public static Vector2D operator *(double scaleFactor, Vector2D value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000300E File Offset: 0x0000120E
		public static Vector2D operator /(Vector2D value1, Vector2D value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000361C File Offset: 0x0000181C
		public static Vector2D operator /(Vector2D value1, double divider)
		{
			double num = 1.0 / divider;
			value1.X *= num;
			value1.Y *= num;
			return value1;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000364E File Offset: 0x0000184E
		public static implicit operator Vector2D(Point point)
		{
			return new Vector2D((double)point.X, (double)point.Y);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003663 File Offset: 0x00001863
		public static double Clamp(double value, double min, double max)
		{
			value = ((value > max) ? max : value);
			value = ((value < min) ? min : value);
			return value;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000367A File Offset: 0x0000187A
		public static double Lerp(double value1, double value2, double amount)
		{
			return value1 + (value2 - value1) * amount;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003684 File Offset: 0x00001884
		public static double SmoothStep(double value1, double value2, double amount)
		{
			double num = Vector2D.Clamp(amount, 0.0, 1.0);
			return Vector2D.Hermite(value1, 0.0, value2, 0.0, num);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000036C8 File Offset: 0x000018C8
		public static double Hermite(double value1, double tangent1, double value2, double tangent2, double amount)
		{
			double num = amount * amount * amount;
			double num2 = amount * amount;
			double num3;
			if (Math.Abs(amount) <= Vector2D.DoubleEpsilon)
			{
				num3 = value1;
			}
			else if (amount == 1.0)
			{
				num3 = value2;
			}
			else
			{
				num3 = (2.0 * value1 - 2.0 * value2 + tangent2 + tangent1) * num + (3.0 * value2 - 3.0 * value1 - 2.0 * tangent1 - tangent2) * num2 + tangent1 * amount + value1;
			}
			return num3;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003769 File Offset: 0x00001969
		public static double Barycentric(double value1, double value2, double value3, double amount1, double amount2)
		{
			return value1 + (value2 - value1) * amount1 + (value3 - value1) * amount2;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000377C File Offset: 0x0000197C
		public static double CatmullRom(double value1, double value2, double value3, double value4, double amount)
		{
			double num = amount * amount;
			double num2 = num * amount;
			return 0.5 * (2.0 * value2 + (value3 - value1) * amount + (2.0 * value1 - 5.0 * value2 + 4.0 * value3 - value4) * num + (3.0 * value2 - value1 - 3.0 * value3 + value4) * num2);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000037F8 File Offset: 0x000019F8
		// Note: this type is marked as 'beforefieldinit'.
		static Vector2D()
		{
		}

		// Token: 0x04000013 RID: 19
		public double X;

		// Token: 0x04000014 RID: 20
		public double Y;

		// Token: 0x04000015 RID: 21
		private static Vector2D zeroVector = new Vector2D(0.0, 0.0);

		// Token: 0x04000016 RID: 22
		private static Vector2D unitVector = new Vector2D(1.0, 1.0);

		// Token: 0x04000017 RID: 23
		private static Vector2D unitXVector = new Vector2D(1.0, 0.0);

		// Token: 0x04000018 RID: 24
		private static Vector2D unitYVector = new Vector2D(0.0, 1.0);

		// Token: 0x04000019 RID: 25
		public static readonly double DoubleEpsilon = Math.Pow(0.5, 53.0);
	}
}
