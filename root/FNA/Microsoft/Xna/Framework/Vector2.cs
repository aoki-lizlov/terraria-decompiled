using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000039 RID: 57
	[TypeConverter(typeof(Vector2Converter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Vector2 : IEquatable<Vector2>
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0001AD27 File Offset: 0x00018F27
		public static Vector2 Zero
		{
			get
			{
				return Vector2.zeroVector;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0001AD2E File Offset: 0x00018F2E
		public static Vector2 One
		{
			get
			{
				return Vector2.unitVector;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0001AD35 File Offset: 0x00018F35
		public static Vector2 UnitX
		{
			get
			{
				return Vector2.unitXVector;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0001AD3C File Offset: 0x00018F3C
		public static Vector2 UnitY
		{
			get
			{
				return Vector2.unitYVector;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000D56 RID: 3414 RVA: 0x0001AD43 File Offset: 0x00018F43
		internal string DebugDisplayString
		{
			get
			{
				return this.X.ToString() + " " + this.Y.ToString();
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001AD65 File Offset: 0x00018F65
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001AD75 File Offset: 0x00018F75
		public Vector2(float value)
		{
			this.X = value;
			this.Y = value;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0001AD85 File Offset: 0x00018F85
		public override bool Equals(object obj)
		{
			return obj is Vector2 && this.Equals((Vector2)obj);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0001AD9D File Offset: 0x00018F9D
		public bool Equals(Vector2 other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001ADBD File Offset: 0x00018FBD
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode();
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0001ADD6 File Offset: 0x00018FD6
		public float Length()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y));
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0001ADFA File Offset: 0x00018FFA
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0001AE18 File Offset: 0x00019018
		public void Normalize()
		{
			float num = 1f / (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y));
			this.X *= num;
			this.Y *= num;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0001AE6C File Offset: 0x0001906C
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

		// Token: 0x06000D60 RID: 3424 RVA: 0x0001AEB8 File Offset: 0x000190B8
		[Conditional("DEBUG")]
		internal void CheckForNaNs()
		{
			if (float.IsNaN(this.X) || float.IsNaN(this.Y))
			{
				throw new InvalidOperationException("Vector2 contains NaNs!");
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0001AEDF File Offset: 0x000190DF
		public static Vector2 Add(Vector2 value1, Vector2 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0001AF04 File Offset: 0x00019104
		public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0001AF2C File Offset: 0x0001912C
		public static Vector2 Barycentric(Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2)
		{
			return new Vector2(MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2), MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0001AF68 File Offset: 0x00019168
		public static void Barycentric(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, float amount1, float amount2, out Vector2 result)
		{
			result.X = MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2);
			result.Y = MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0001AFB8 File Offset: 0x000191B8
		public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
		{
			return new Vector2(MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount), MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0001B008 File Offset: 0x00019208
		public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
		{
			result.X = MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount);
			result.Y = MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0001B061 File Offset: 0x00019261
		public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
		{
			return new Vector2(MathHelper.Clamp(value1.X, min.X, max.X), MathHelper.Clamp(value1.Y, min.Y, max.Y));
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0001B096 File Offset: 0x00019296
		public static void Clamp(ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
		{
			result.X = MathHelper.Clamp(value1.X, min.X, max.X);
			result.Y = MathHelper.Clamp(value1.Y, min.Y, max.Y);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0001B0D4 File Offset: 0x000192D4
		public static float Distance(Vector2 value1, Vector2 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0001B10C File Offset: 0x0001930C
		public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			result = (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0001B148 File Offset: 0x00019348
		public static float DistanceSquared(Vector2 value1, Vector2 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			return num * num + num2 * num2;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0001B178 File Offset: 0x00019378
		public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			result = num * num + num2 * num2;
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0001B1AA File Offset: 0x000193AA
		public static Vector2 Divide(Vector2 value1, Vector2 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0001B1CF File Offset: 0x000193CF
		public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0001B1F7 File Offset: 0x000193F7
		public static Vector2 Divide(Vector2 value1, float divider)
		{
			value1.X /= divider;
			value1.Y /= divider;
			return value1;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0001B212 File Offset: 0x00019412
		public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
		{
			result.X = value1.X / divider;
			result.Y = value1.Y / divider;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0001B230 File Offset: 0x00019430
		public static float Dot(Vector2 value1, Vector2 value2)
		{
			return value1.X * value2.X + value1.Y * value2.Y;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0001B24D File Offset: 0x0001944D
		public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			result = value1.X * value2.X + value1.Y * value2.Y;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0001B26C File Offset: 0x0001946C
		public static Vector2 Hermite(Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount)
		{
			Vector2 vector = default(Vector2);
			Vector2.Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out vector);
			return vector;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0001B294 File Offset: 0x00019494
		public static void Hermite(ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, float amount, out Vector2 result)
		{
			result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
			result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0001B2ED File Offset: 0x000194ED
		public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
		{
			return new Vector2(MathHelper.Lerp(value1.X, value2.X, amount), MathHelper.Lerp(value1.Y, value2.Y, amount));
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0001B318 File Offset: 0x00019518
		public static void Lerp(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
		{
			result.X = MathHelper.Lerp(value1.X, value2.X, amount);
			result.Y = MathHelper.Lerp(value1.Y, value2.Y, amount);
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0001B34A File Offset: 0x0001954A
		public static Vector2 Max(Vector2 value1, Vector2 value2)
		{
			return new Vector2((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0001B38C File Offset: 0x0001958C
		public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = ((value1.X > value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0001B3DD File Offset: 0x000195DD
		public static Vector2 Min(Vector2 value1, Vector2 value2)
		{
			return new Vector2((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0001B41C File Offset: 0x0001961C
		public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = ((value1.X < value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0001B46D File Offset: 0x0001966D
		public static Vector2 Multiply(Vector2 value1, Vector2 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0001B492 File Offset: 0x00019692
		public static Vector2 Multiply(Vector2 value1, float scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			return value1;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0001B4AD File Offset: 0x000196AD
		public static void Multiply(ref Vector2 value1, float scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0001B4CB File Offset: 0x000196CB
		public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0001B4F3 File Offset: 0x000196F3
		public static Vector2 Negate(Vector2 value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0001B512 File Offset: 0x00019712
		public static void Negate(ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0001B530 File Offset: 0x00019730
		public static Vector2 Normalize(Vector2 value)
		{
			float num = 1f / (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y));
			value.X *= num;
			value.Y *= num;
			return value;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0001B580 File Offset: 0x00019780
		public static void Normalize(ref Vector2 value, out Vector2 result)
		{
			float num = 1f / (float)Math.Sqrt((double)(value.X * value.X + value.Y * value.Y));
			result.X = value.X * num;
			result.Y = value.Y * num;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0001B5D4 File Offset: 0x000197D4
		public static Vector2 Reflect(Vector2 vector, Vector2 normal)
		{
			float num = 2f * (vector.X * normal.X + vector.Y * normal.Y);
			Vector2 vector2;
			vector2.X = vector.X - normal.X * num;
			vector2.Y = vector.Y - normal.Y * num;
			return vector2;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0001B630 File Offset: 0x00019830
		public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
		{
			float num = 2f * (vector.X * normal.X + vector.Y * normal.Y);
			result.X = vector.X - normal.X * num;
			result.Y = vector.Y - normal.Y * num;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0001B689 File Offset: 0x00019889
		public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
		{
			return new Vector2(MathHelper.SmoothStep(value1.X, value2.X, amount), MathHelper.SmoothStep(value1.Y, value2.Y, amount));
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0001B6B4 File Offset: 0x000198B4
		public static void SmoothStep(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
		{
			result.X = MathHelper.SmoothStep(value1.X, value2.X, amount);
			result.Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0001B6E6 File Offset: 0x000198E6
		public static Vector2 Subtract(Vector2 value1, Vector2 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0001B70B File Offset: 0x0001990B
		public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x0001B734 File Offset: 0x00019934
		public static Vector2 Transform(Vector2 position, Matrix matrix)
		{
			return new Vector2(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0001B78C File Offset: 0x0001998C
		public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector2 result)
		{
			float num = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
			float num2 = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
			result.X = num;
			result.Y = num2;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0001B7ED File Offset: 0x000199ED
		public static Vector2 Transform(Vector2 value, Quaternion rotation)
		{
			Vector2.Transform(ref value, ref rotation, out value);
			return value;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x0001B7FC File Offset: 0x000199FC
		public static void Transform(ref Vector2 value, ref Quaternion rotation, out Vector2 result)
		{
			float num = 2f * -(rotation.Z * value.Y);
			float num2 = 2f * (rotation.Z * value.X);
			float num3 = 2f * (rotation.X * value.Y - rotation.Y * value.X);
			result.X = value.X + num * rotation.W + (rotation.Y * num3 - rotation.Z * num2);
			result.Y = value.Y + num2 * rotation.W + (rotation.Z * num - rotation.X * num3);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0001B8A2 File Offset: 0x00019AA2
		public static void Transform(Vector2[] sourceArray, ref Matrix matrix, Vector2[] destinationArray)
		{
			Vector2.Transform(sourceArray, 0, ref matrix, destinationArray, 0, sourceArray.Length);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		public static void Transform(Vector2[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2[] destinationArray, int destinationIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				Vector2 vector = sourceArray[sourceIndex + i];
				Vector2 vector2 = destinationArray[destinationIndex + i];
				vector2.X = vector.X * matrix.M11 + vector.Y * matrix.M21 + matrix.M41;
				vector2.Y = vector.X * matrix.M12 + vector.Y * matrix.M22 + matrix.M42;
				destinationArray[destinationIndex + i] = vector2;
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x0001B940 File Offset: 0x00019B40
		public static void Transform(Vector2[] sourceArray, ref Quaternion rotation, Vector2[] destinationArray)
		{
			Vector2.Transform(sourceArray, 0, ref rotation, destinationArray, 0, sourceArray.Length);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0001B950 File Offset: 0x00019B50
		public static void Transform(Vector2[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector2[] destinationArray, int destinationIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				Vector2 vector = sourceArray[sourceIndex + i];
				Vector2 vector2;
				Vector2.Transform(ref vector, ref rotation, out vector2);
				destinationArray[destinationIndex + i] = vector2;
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0001B989 File Offset: 0x00019B89
		public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
		{
			return new Vector2(normal.X * matrix.M11 + normal.Y * matrix.M21, normal.X * matrix.M12 + normal.Y * matrix.M22);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0001B9C8 File Offset: 0x00019BC8
		public static void TransformNormal(ref Vector2 normal, ref Matrix matrix, out Vector2 result)
		{
			float num = normal.X * matrix.M11 + normal.Y * matrix.M21;
			float num2 = normal.X * matrix.M12 + normal.Y * matrix.M22;
			result.X = num;
			result.Y = num2;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0001BA1B File Offset: 0x00019C1B
		public static void TransformNormal(Vector2[] sourceArray, ref Matrix matrix, Vector2[] destinationArray)
		{
			Vector2.TransformNormal(sourceArray, 0, ref matrix, destinationArray, 0, sourceArray.Length);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0001BA2C File Offset: 0x00019C2C
		public static void TransformNormal(Vector2[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2[] destinationArray, int destinationIndex, int length)
		{
			for (int i = 0; i < length; i++)
			{
				Vector2 vector = sourceArray[sourceIndex + i];
				Vector2 vector2;
				vector2.X = vector.X * matrix.M11 + vector.Y * matrix.M21;
				vector2.Y = vector.X * matrix.M12 + vector.Y * matrix.M22;
				destinationArray[destinationIndex + i] = vector2;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0001B4F3 File Offset: 0x000196F3
		public static Vector2 operator -(Vector2 value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0001AD9D File Offset: 0x00018F9D
		public static bool operator ==(Vector2 value1, Vector2 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y;
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0001BA9F File Offset: 0x00019C9F
		public static bool operator !=(Vector2 value1, Vector2 value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0001AEDF File Offset: 0x000190DF
		public static Vector2 operator +(Vector2 value1, Vector2 value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0001B6E6 File Offset: 0x000198E6
		public static Vector2 operator -(Vector2 value1, Vector2 value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0001B46D File Offset: 0x0001966D
		public static Vector2 operator *(Vector2 value1, Vector2 value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0001B492 File Offset: 0x00019692
		public static Vector2 operator *(Vector2 value, float scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0001BAAB File Offset: 0x00019CAB
		public static Vector2 operator *(float scaleFactor, Vector2 value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0001B1AA File Offset: 0x000193AA
		public static Vector2 operator /(Vector2 value1, Vector2 value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0001B1F7 File Offset: 0x000193F7
		public static Vector2 operator /(Vector2 value1, float divider)
		{
			value1.X /= divider;
			value1.Y /= divider;
			return value1;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		// Note: this type is marked as 'beforefieldinit'.
		static Vector2()
		{
		}

		// Token: 0x040005BD RID: 1469
		public float X;

		// Token: 0x040005BE RID: 1470
		public float Y;

		// Token: 0x040005BF RID: 1471
		private static Vector2 zeroVector = new Vector2(0f, 0f);

		// Token: 0x040005C0 RID: 1472
		private static Vector2 unitVector = new Vector2(1f, 1f);

		// Token: 0x040005C1 RID: 1473
		private static Vector2 unitXVector = new Vector2(1f, 0f);

		// Token: 0x040005C2 RID: 1474
		private static Vector2 unitYVector = new Vector2(0f, 1f);
	}
}
