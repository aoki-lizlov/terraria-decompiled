using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000032 RID: 50
	[TypeConverter(typeof(QuaternionConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Quaternion : IEquatable<Quaternion>
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x00018D7F File Offset: 0x00016F7F
		public static Quaternion Identity
		{
			get
			{
				return Quaternion.identity;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x00018D88 File Offset: 0x00016F88
		internal string DebugDisplayString
		{
			get
			{
				if (this == Quaternion.Identity)
				{
					return "Identity";
				}
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

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00018E08 File Offset: 0x00017008
		public Quaternion(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00018E27 File Offset: 0x00017027
		public Quaternion(Vector3 vectorPart, float scalarPart)
		{
			this.X = vectorPart.X;
			this.Y = vectorPart.Y;
			this.Z = vectorPart.Z;
			this.W = scalarPart;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00018E54 File Offset: 0x00017054
		public void Conjugate()
		{
			this.X = -this.X;
			this.Y = -this.Y;
			this.Z = -this.Z;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00018E7D File Offset: 0x0001707D
		public override bool Equals(object obj)
		{
			return obj is Quaternion && this.Equals((Quaternion)obj);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00018E95 File Offset: 0x00017095
		public bool Equals(Quaternion other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00018ED1 File Offset: 0x000170D1
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00018F02 File Offset: 0x00017102
		public float Length()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W));
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x00018F42 File Offset: 0x00017142
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00018F7C File Offset: 0x0001717C
		public void Normalize()
		{
			float num = 1f / (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W));
			this.X *= num;
			this.Y *= num;
			this.Z *= num;
			this.W *= num;
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00019008 File Offset: 0x00017208
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

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00019084 File Offset: 0x00017284
		[Conditional("DEBUG")]
		internal void CheckForNaNs()
		{
			if (float.IsNaN(this.X) || float.IsNaN(this.Y) || float.IsNaN(this.Z) || float.IsNaN(this.W))
			{
				throw new InvalidOperationException("Quaternion contains NaNs!");
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x000190D0 File Offset: 0x000172D0
		public static Quaternion Add(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion3;
			Quaternion.Add(ref quaternion1, ref quaternion2, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000190EC File Offset: 0x000172EC
		public static void Add(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X + quaternion2.X;
			result.Y = quaternion1.Y + quaternion2.Y;
			result.Z = quaternion1.Z + quaternion2.Z;
			result.W = quaternion1.W + quaternion2.W;
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00019148 File Offset: 0x00017348
		public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
		{
			Quaternion quaternion;
			Quaternion.Concatenate(ref value1, ref value2, out quaternion);
			return quaternion;
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00019164 File Offset: 0x00017364
		public static void Concatenate(ref Quaternion value1, ref Quaternion value2, out Quaternion result)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			result.X = x2 * w + x * w2 + (y2 * z - z2 * y);
			result.Y = y2 * w + y * w2 + (z2 * x - x2 * z);
			result.Z = z2 * w + z * w2 + (x2 * y - y2 * x);
			result.W = w2 * w - (x2 * x + y2 * y + z2 * z);
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00019211 File Offset: 0x00017411
		public static Quaternion Conjugate(Quaternion value)
		{
			return new Quaternion(-value.X, -value.Y, -value.Z, value.W);
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00019233 File Offset: 0x00017433
		public static void Conjugate(ref Quaternion value, out Quaternion result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = value.W;
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00019268 File Offset: 0x00017468
		public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
		{
			Quaternion quaternion;
			Quaternion.CreateFromAxisAngle(ref axis, angle, out quaternion);
			return quaternion;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x00019280 File Offset: 0x00017480
		public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Quaternion result)
		{
			float num = angle * 0.5f;
			float num2 = (float)Math.Sin((double)num);
			float num3 = (float)Math.Cos((double)num);
			result.X = axis.X * num2;
			result.Y = axis.Y * num2;
			result.Z = axis.Z * num2;
			result.W = num3;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000192D8 File Offset: 0x000174D8
		public static Quaternion CreateFromRotationMatrix(Matrix matrix)
		{
			Quaternion quaternion;
			Quaternion.CreateFromRotationMatrix(ref matrix, out quaternion);
			return quaternion;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x000192F0 File Offset: 0x000174F0
		public static void CreateFromRotationMatrix(ref Matrix matrix, out Quaternion result)
		{
			float num = matrix.M11 + matrix.M22 + matrix.M33;
			float num2;
			if (num > 0f)
			{
				num2 = (float)Math.Sqrt((double)(num + 1f));
				result.W = num2 * 0.5f;
				num2 = 0.5f / num2;
				result.X = (matrix.M23 - matrix.M32) * num2;
				result.Y = (matrix.M31 - matrix.M13) * num2;
				result.Z = (matrix.M12 - matrix.M21) * num2;
				return;
			}
			float num3;
			if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
			{
				num2 = (float)Math.Sqrt((double)(1f + matrix.M11 - matrix.M22 - matrix.M33));
				num3 = 0.5f / num2;
				result.X = 0.5f * num2;
				result.Y = (matrix.M12 + matrix.M21) * num3;
				result.Z = (matrix.M13 + matrix.M31) * num3;
				result.W = (matrix.M23 - matrix.M32) * num3;
				return;
			}
			if (matrix.M22 > matrix.M33)
			{
				num2 = (float)Math.Sqrt((double)(1f + matrix.M22 - matrix.M11 - matrix.M33));
				num3 = 0.5f / num2;
				result.X = (matrix.M21 + matrix.M12) * num3;
				result.Y = 0.5f * num2;
				result.Z = (matrix.M32 + matrix.M23) * num3;
				result.W = (matrix.M31 - matrix.M13) * num3;
				return;
			}
			num2 = (float)Math.Sqrt((double)(1f + matrix.M33 - matrix.M11 - matrix.M22));
			num3 = 0.5f / num2;
			result.X = (matrix.M31 + matrix.M13) * num3;
			result.Y = (matrix.M32 + matrix.M23) * num3;
			result.Z = 0.5f * num2;
			result.W = (matrix.M12 - matrix.M21) * num3;
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00019510 File Offset: 0x00017710
		public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			Quaternion quaternion;
			Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out quaternion);
			return quaternion;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00019528 File Offset: 0x00017728
		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Quaternion result)
		{
			float num = roll * 0.5f;
			float num2 = (float)Math.Sin((double)num);
			float num3 = (float)Math.Cos((double)num);
			float num4 = pitch * 0.5f;
			float num5 = (float)Math.Sin((double)num4);
			float num6 = (float)Math.Cos((double)num4);
			float num7 = yaw * 0.5f;
			float num8 = (float)Math.Sin((double)num7);
			float num9 = (float)Math.Cos((double)num7);
			result.X = num9 * num5 * num3 + num8 * num6 * num2;
			result.Y = num8 * num6 * num3 - num9 * num5 * num2;
			result.Z = num9 * num6 * num2 - num8 * num5 * num3;
			result.W = num9 * num6 * num3 + num8 * num5 * num2;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x000195CC File Offset: 0x000177CC
		public static Quaternion Divide(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion3;
			Quaternion.Divide(ref quaternion1, ref quaternion2, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000195E8 File Offset: 0x000177E8
		public static void Divide(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			float x = quaternion1.X;
			float y = quaternion1.Y;
			float z = quaternion1.Z;
			float w = quaternion1.W;
			float num = quaternion2.X * quaternion2.X + quaternion2.Y * quaternion2.Y + quaternion2.Z * quaternion2.Z + quaternion2.W * quaternion2.W;
			float num2 = 1f / num;
			float num3 = -quaternion2.X * num2;
			float num4 = -quaternion2.Y * num2;
			float num5 = -quaternion2.Z * num2;
			float num6 = quaternion2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			result.X = x * num6 + num3 * w + num7;
			result.Y = y * num6 + num4 * w + num8;
			result.Z = z * num6 + num5 * w + num9;
			result.W = w * num6 - num10;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x000196F7 File Offset: 0x000178F7
		public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
		{
			return quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00019730 File Offset: 0x00017930
		public static void Dot(ref Quaternion quaternion1, ref Quaternion quaternion2, out float result)
		{
			result = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0001976C File Offset: 0x0001796C
		public static Quaternion Inverse(Quaternion quaternion)
		{
			Quaternion quaternion2;
			Quaternion.Inverse(ref quaternion, out quaternion2);
			return quaternion2;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00019784 File Offset: 0x00017984
		public static void Inverse(ref Quaternion quaternion, out Quaternion result)
		{
			float num = quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W;
			float num2 = 1f / num;
			result.X = -quaternion.X * num2;
			result.Y = -quaternion.Y * num2;
			result.Z = -quaternion.Z * num2;
			result.W = quaternion.W * num2;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0001980C File Offset: 0x00017A0C
		public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			Quaternion quaternion3;
			Quaternion.Lerp(ref quaternion1, ref quaternion2, amount, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00019828 File Offset: 0x00017A28
		public static void Lerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
		{
			float num = 1f - amount;
			if (quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W >= 0f)
			{
				result.X = num * quaternion1.X + amount * quaternion2.X;
				result.Y = num * quaternion1.Y + amount * quaternion2.Y;
				result.Z = num * quaternion1.Z + amount * quaternion2.Z;
				result.W = num * quaternion1.W + amount * quaternion2.W;
			}
			else
			{
				result.X = num * quaternion1.X - amount * quaternion2.X;
				result.Y = num * quaternion1.Y - amount * quaternion2.Y;
				result.Z = num * quaternion1.Z - amount * quaternion2.Z;
				result.W = num * quaternion1.W - amount * quaternion2.W;
			}
			float num2 = result.X * result.X + result.Y * result.Y + result.Z * result.Z + result.W * result.W;
			float num3 = 1f / (float)Math.Sqrt((double)num2);
			result.X *= num3;
			result.Y *= num3;
			result.Z *= num3;
			result.W *= num3;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x000199AC File Offset: 0x00017BAC
		public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			Quaternion quaternion3;
			Quaternion.Slerp(ref quaternion1, ref quaternion2, amount, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x000199C8 File Offset: 0x00017BC8
		public static void Slerp(ref Quaternion quaternion1, ref Quaternion quaternion2, float amount, out Quaternion result)
		{
			float num = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
			float num2 = 1f;
			if (num < 0f)
			{
				num2 = -1f;
				num = -num;
			}
			float num3;
			float num4;
			if (num > 0.999999f)
			{
				num3 = 1f - amount;
				num4 = amount * num2;
			}
			else
			{
				float num5 = (float)Math.Acos((double)num);
				float num6 = (float)(1.0 / Math.Sin((double)num5));
				num3 = (float)Math.Sin((double)((1f - amount) * num5)) * num6;
				num4 = num2 * ((float)Math.Sin((double)(amount * num5)) * num6);
			}
			result.X = num3 * quaternion1.X + num4 * quaternion2.X;
			result.Y = num3 * quaternion1.Y + num4 * quaternion2.Y;
			result.Z = num3 * quaternion1.Z + num4 * quaternion2.Z;
			result.W = num3 * quaternion1.W + num4 * quaternion2.W;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x00019AE4 File Offset: 0x00017CE4
		public static Quaternion Subtract(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion3;
			Quaternion.Subtract(ref quaternion1, ref quaternion2, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x00019B00 File Offset: 0x00017D00
		public static void Subtract(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			result.X = quaternion1.X - quaternion2.X;
			result.Y = quaternion1.Y - quaternion2.Y;
			result.Z = quaternion1.Z - quaternion2.Z;
			result.W = quaternion1.W - quaternion2.W;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00019B5C File Offset: 0x00017D5C
		public static Quaternion Multiply(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion3;
			Quaternion.Multiply(ref quaternion1, ref quaternion2, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00019B78 File Offset: 0x00017D78
		public static Quaternion Multiply(Quaternion quaternion1, float scaleFactor)
		{
			Quaternion quaternion2;
			Quaternion.Multiply(ref quaternion1, scaleFactor, out quaternion2);
			return quaternion2;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00019B90 File Offset: 0x00017D90
		public static void Multiply(ref Quaternion quaternion1, ref Quaternion quaternion2, out Quaternion result)
		{
			float x = quaternion1.X;
			float y = quaternion1.Y;
			float z = quaternion1.Z;
			float w = quaternion1.W;
			float x2 = quaternion2.X;
			float y2 = quaternion2.Y;
			float z2 = quaternion2.Z;
			float w2 = quaternion2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00019C4D File Offset: 0x00017E4D
		public static void Multiply(ref Quaternion quaternion1, float scaleFactor, out Quaternion result)
		{
			result.X = quaternion1.X * scaleFactor;
			result.Y = quaternion1.Y * scaleFactor;
			result.Z = quaternion1.Z * scaleFactor;
			result.W = quaternion1.W * scaleFactor;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00019C87 File Offset: 0x00017E87
		public static Quaternion Negate(Quaternion quaternion)
		{
			return new Quaternion(-quaternion.X, -quaternion.Y, -quaternion.Z, -quaternion.W);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00019CAA File Offset: 0x00017EAA
		public static void Negate(ref Quaternion quaternion, out Quaternion result)
		{
			result.X = -quaternion.X;
			result.Y = -quaternion.Y;
			result.Z = -quaternion.Z;
			result.W = -quaternion.W;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00019CE0 File Offset: 0x00017EE0
		public static Quaternion Normalize(Quaternion quaternion)
		{
			Quaternion quaternion2;
			Quaternion.Normalize(ref quaternion, out quaternion2);
			return quaternion2;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00019CF8 File Offset: 0x00017EF8
		public static void Normalize(ref Quaternion quaternion, out Quaternion result)
		{
			float num = 1f / (float)Math.Sqrt((double)(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W));
			result.X = quaternion.X * num;
			result.Y = quaternion.Y * num;
			result.Z = quaternion.Z * num;
			result.W = quaternion.W * num;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00019D84 File Offset: 0x00017F84
		public static Quaternion operator +(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion3;
			Quaternion.Add(ref quaternion1, ref quaternion2, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00019DA0 File Offset: 0x00017FA0
		public static Quaternion operator /(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion3;
			Quaternion.Divide(ref quaternion1, ref quaternion2, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00019DB9 File Offset: 0x00017FB9
		public static bool operator ==(Quaternion quaternion1, Quaternion quaternion2)
		{
			return quaternion1.Equals(quaternion2);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00019DC3 File Offset: 0x00017FC3
		public static bool operator !=(Quaternion quaternion1, Quaternion quaternion2)
		{
			return !quaternion1.Equals(quaternion2);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00019DD0 File Offset: 0x00017FD0
		public static Quaternion operator *(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion3;
			Quaternion.Multiply(ref quaternion1, ref quaternion2, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00019DEC File Offset: 0x00017FEC
		public static Quaternion operator *(Quaternion quaternion1, float scaleFactor)
		{
			Quaternion quaternion2;
			Quaternion.Multiply(ref quaternion1, scaleFactor, out quaternion2);
			return quaternion2;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00019E04 File Offset: 0x00018004
		public static Quaternion operator -(Quaternion quaternion1, Quaternion quaternion2)
		{
			Quaternion quaternion3;
			Quaternion.Subtract(ref quaternion1, ref quaternion2, out quaternion3);
			return quaternion3;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00019E20 File Offset: 0x00018020
		public static Quaternion operator -(Quaternion quaternion)
		{
			Quaternion quaternion2;
			Quaternion.Negate(ref quaternion, out quaternion2);
			return quaternion2;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00019E37 File Offset: 0x00018037
		// Note: this type is marked as 'beforefieldinit'.
		static Quaternion()
		{
		}

		// Token: 0x040005B0 RID: 1456
		public float X;

		// Token: 0x040005B1 RID: 1457
		public float Y;

		// Token: 0x040005B2 RID: 1458
		public float Z;

		// Token: 0x040005B3 RID: 1459
		public float W;

		// Token: 0x040005B4 RID: 1460
		private static Quaternion identity = new Quaternion(0f, 0f, 0f, 1f);
	}
}
