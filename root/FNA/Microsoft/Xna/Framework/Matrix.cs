using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Xna.Framework.Design;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200002C RID: 44
	[TypeConverter(typeof(MatrixConverter))]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	[Serializable]
	public struct Matrix : IEquatable<Matrix>
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00014670 File Offset: 0x00012870
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00014689 File Offset: 0x00012889
		public Vector3 Backward
		{
			get
			{
				return new Vector3(this.M31, this.M32, this.M33);
			}
			set
			{
				this.M31 = value.X;
				this.M32 = value.Y;
				this.M33 = value.Z;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x000146AF File Offset: 0x000128AF
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x000146CB File Offset: 0x000128CB
		public Vector3 Down
		{
			get
			{
				return new Vector3(-this.M21, -this.M22, -this.M23);
			}
			set
			{
				this.M21 = -value.X;
				this.M22 = -value.Y;
				this.M23 = -value.Z;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x000146F4 File Offset: 0x000128F4
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x00014710 File Offset: 0x00012910
		public Vector3 Forward
		{
			get
			{
				return new Vector3(-this.M31, -this.M32, -this.M33);
			}
			set
			{
				this.M31 = -value.X;
				this.M32 = -value.Y;
				this.M33 = -value.Z;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00014739 File Offset: 0x00012939
		public static Matrix Identity
		{
			get
			{
				return Matrix.identity;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x00014740 File Offset: 0x00012940
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x0001475C File Offset: 0x0001295C
		public Vector3 Left
		{
			get
			{
				return new Vector3(-this.M11, -this.M12, -this.M13);
			}
			set
			{
				this.M11 = -value.X;
				this.M12 = -value.Y;
				this.M13 = -value.Z;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x00014785 File Offset: 0x00012985
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x0001479E File Offset: 0x0001299E
		public Vector3 Right
		{
			get
			{
				return new Vector3(this.M11, this.M12, this.M13);
			}
			set
			{
				this.M11 = value.X;
				this.M12 = value.Y;
				this.M13 = value.Z;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x000147C4 File Offset: 0x000129C4
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x000147DD File Offset: 0x000129DD
		public Vector3 Translation
		{
			get
			{
				return new Vector3(this.M41, this.M42, this.M43);
			}
			set
			{
				this.M41 = value.X;
				this.M42 = value.Y;
				this.M43 = value.Z;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x00014803 File Offset: 0x00012A03
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0001481C File Offset: 0x00012A1C
		public Vector3 Up
		{
			get
			{
				return new Vector3(this.M21, this.M22, this.M23);
			}
			set
			{
				this.M21 = value.X;
				this.M22 = value.Y;
				this.M23 = value.Z;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x00014844 File Offset: 0x00012A44
		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(new string[]
				{
					"( ",
					this.M11.ToString(),
					" ",
					this.M12.ToString(),
					" ",
					this.M13.ToString(),
					" ",
					this.M14.ToString(),
					" ) \r\n",
					"( ",
					this.M21.ToString(),
					" ",
					this.M22.ToString(),
					" ",
					this.M23.ToString(),
					" ",
					this.M24.ToString(),
					" ) \r\n",
					"( ",
					this.M31.ToString(),
					" ",
					this.M32.ToString(),
					" ",
					this.M33.ToString(),
					" ",
					this.M34.ToString(),
					" ) \r\n",
					"( ",
					this.M41.ToString(),
					" ",
					this.M42.ToString(),
					" ",
					this.M43.ToString(),
					" ",
					this.M44.ToString(),
					" )"
				});
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000149F8 File Offset: 0x00012BF8
		public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00014A84 File Offset: 0x00012C84
		public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
		{
			translation.X = this.M41;
			translation.Y = this.M42;
			translation.Z = this.M43;
			float num = (float)((Math.Sign(this.M11 * this.M12 * this.M13 * this.M14) < 0) ? (-1) : 1);
			float num2 = (float)((Math.Sign(this.M21 * this.M22 * this.M23 * this.M24) < 0) ? (-1) : 1);
			float num3 = (float)((Math.Sign(this.M31 * this.M32 * this.M33 * this.M34) < 0) ? (-1) : 1);
			scale.X = num * (float)Math.Sqrt((double)(this.M11 * this.M11 + this.M12 * this.M12 + this.M13 * this.M13));
			scale.Y = num2 * (float)Math.Sqrt((double)(this.M21 * this.M21 + this.M22 * this.M22 + this.M23 * this.M23));
			scale.Z = num3 * (float)Math.Sqrt((double)(this.M31 * this.M31 + this.M32 * this.M32 + this.M33 * this.M33));
			if (MathHelper.WithinEpsilon(scale.X, 0f) || MathHelper.WithinEpsilon(scale.Y, 0f) || MathHelper.WithinEpsilon(scale.Z, 0f))
			{
				rotation = Quaternion.Identity;
				return false;
			}
			Matrix matrix = new Matrix(this.M11 / scale.X, this.M12 / scale.X, this.M13 / scale.X, 0f, this.M21 / scale.Y, this.M22 / scale.Y, this.M23 / scale.Y, 0f, this.M31 / scale.Z, this.M32 / scale.Z, this.M33 / scale.Z, 0f, 0f, 0f, 0f, 1f);
			rotation = Quaternion.CreateFromRotationMatrix(matrix);
			return true;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00014CC8 File Offset: 0x00012EC8
		public float Determinant()
		{
			float num = this.M33 * this.M44 - this.M34 * this.M43;
			float num2 = this.M32 * this.M44 - this.M34 * this.M42;
			float num3 = this.M32 * this.M43 - this.M33 * this.M42;
			float num4 = this.M31 * this.M44 - this.M34 * this.M41;
			float num5 = this.M31 * this.M43 - this.M33 * this.M41;
			float num6 = this.M31 * this.M42 - this.M32 * this.M41;
			return this.M11 * (this.M22 * num - this.M23 * num2 + this.M24 * num3) - this.M12 * (this.M21 * num - this.M23 * num4 + this.M24 * num5) + this.M13 * (this.M21 * num2 - this.M22 * num4 + this.M24 * num6) - this.M14 * (this.M21 * num3 - this.M22 * num5 + this.M23 * num6);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00014E0C File Offset: 0x0001300C
		public bool Equals(Matrix other)
		{
			return this.M11 == other.M11 && this.M12 == other.M12 && this.M13 == other.M13 && this.M14 == other.M14 && this.M21 == other.M21 && this.M22 == other.M22 && this.M23 == other.M23 && this.M24 == other.M24 && this.M31 == other.M31 && this.M32 == other.M32 && this.M33 == other.M33 && this.M34 == other.M34 && this.M41 == other.M41 && this.M42 == other.M42 && this.M43 == other.M43 && this.M44 == other.M44;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x00014F0D File Offset: 0x0001310D
		public override bool Equals(object obj)
		{
			return obj is Matrix && this.Equals((Matrix)obj);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00014F28 File Offset: 0x00013128
		public override int GetHashCode()
		{
			return this.M11.GetHashCode() + this.M12.GetHashCode() + this.M13.GetHashCode() + this.M14.GetHashCode() + this.M21.GetHashCode() + this.M22.GetHashCode() + this.M23.GetHashCode() + this.M24.GetHashCode() + this.M31.GetHashCode() + this.M32.GetHashCode() + this.M33.GetHashCode() + this.M34.GetHashCode() + this.M41.GetHashCode() + this.M42.GetHashCode() + this.M43.GetHashCode() + this.M44.GetHashCode();
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x00014FF4 File Offset: 0x000131F4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"{M11:",
				this.M11.ToString(),
				" M12:",
				this.M12.ToString(),
				" M13:",
				this.M13.ToString(),
				" M14:",
				this.M14.ToString(),
				"} {M21:",
				this.M21.ToString(),
				" M22:",
				this.M22.ToString(),
				" M23:",
				this.M23.ToString(),
				" M24:",
				this.M24.ToString(),
				"} {M31:",
				this.M31.ToString(),
				" M32:",
				this.M32.ToString(),
				" M33:",
				this.M33.ToString(),
				" M34:",
				this.M34.ToString(),
				"} {M41:",
				this.M41.ToString(),
				" M42:",
				this.M42.ToString(),
				" M43:",
				this.M43.ToString(),
				" M44:",
				this.M44.ToString(),
				"}"
			});
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x00015190 File Offset: 0x00013390
		[Conditional("DEBUG")]
		internal void CheckForNaNs()
		{
			if (float.IsNaN(this.M11) || float.IsNaN(this.M12) || float.IsNaN(this.M13) || float.IsNaN(this.M14) || float.IsNaN(this.M21) || float.IsNaN(this.M22) || float.IsNaN(this.M23) || float.IsNaN(this.M24) || float.IsNaN(this.M31) || float.IsNaN(this.M32) || float.IsNaN(this.M33) || float.IsNaN(this.M34) || float.IsNaN(this.M41) || float.IsNaN(this.M42) || float.IsNaN(this.M43) || float.IsNaN(this.M44))
			{
				throw new InvalidOperationException("Matrix contains NaNs!");
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0001528C File Offset: 0x0001348C
		public static Matrix Add(Matrix matrix1, Matrix matrix2)
		{
			matrix1.M11 += matrix2.M11;
			matrix1.M12 += matrix2.M12;
			matrix1.M13 += matrix2.M13;
			matrix1.M14 += matrix2.M14;
			matrix1.M21 += matrix2.M21;
			matrix1.M22 += matrix2.M22;
			matrix1.M23 += matrix2.M23;
			matrix1.M24 += matrix2.M24;
			matrix1.M31 += matrix2.M31;
			matrix1.M32 += matrix2.M32;
			matrix1.M33 += matrix2.M33;
			matrix1.M34 += matrix2.M34;
			matrix1.M41 += matrix2.M41;
			matrix1.M42 += matrix2.M42;
			matrix1.M43 += matrix2.M43;
			matrix1.M44 += matrix2.M44;
			return matrix1;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000153AC File Offset: 0x000135AC
		public static void Add(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
		{
			result.M11 = matrix1.M11 + matrix2.M11;
			result.M12 = matrix1.M12 + matrix2.M12;
			result.M13 = matrix1.M13 + matrix2.M13;
			result.M14 = matrix1.M14 + matrix2.M14;
			result.M21 = matrix1.M21 + matrix2.M21;
			result.M22 = matrix1.M22 + matrix2.M22;
			result.M23 = matrix1.M23 + matrix2.M23;
			result.M24 = matrix1.M24 + matrix2.M24;
			result.M31 = matrix1.M31 + matrix2.M31;
			result.M32 = matrix1.M32 + matrix2.M32;
			result.M33 = matrix1.M33 + matrix2.M33;
			result.M34 = matrix1.M34 + matrix2.M34;
			result.M41 = matrix1.M41 + matrix2.M41;
			result.M42 = matrix1.M42 + matrix2.M42;
			result.M43 = matrix1.M43 + matrix2.M43;
			result.M44 = matrix1.M44 + matrix2.M44;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000154EC File Offset: 0x000136EC
		public static Matrix CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3? cameraForwardVector)
		{
			Matrix matrix;
			Matrix.CreateBillboard(ref objectPosition, ref cameraPosition, ref cameraUpVector, cameraForwardVector, out matrix);
			return matrix;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x00015508 File Offset: 0x00013708
		public static void CreateBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix result)
		{
			Vector3 vector = objectPosition - cameraPosition;
			float num = vector.LengthSquared();
			if (num < 0.0001f)
			{
				vector = ((cameraForwardVector != null) ? (-cameraForwardVector.Value) : Vector3.Forward);
			}
			else
			{
				Vector3.Multiply(ref vector, 1f / (float)Math.Sqrt((double)num), out vector);
			}
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0f;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0f;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00015654 File Offset: 0x00013854
		public static Matrix CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector)
		{
			Matrix matrix;
			Matrix.CreateConstrainedBillboard(ref objectPosition, ref cameraPosition, ref rotateAxis, cameraForwardVector, objectForwardVector, out matrix);
			return matrix;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00015674 File Offset: 0x00013874
		public static void CreateConstrainedBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix result)
		{
			Vector3 vector;
			vector.X = objectPosition.X - cameraPosition.X;
			vector.Y = objectPosition.Y - cameraPosition.Y;
			vector.Z = objectPosition.Z - cameraPosition.Z;
			float num = vector.LengthSquared();
			if (num < 0.0001f)
			{
				vector = ((cameraForwardVector != null) ? (-cameraForwardVector.Value) : Vector3.Forward);
			}
			else
			{
				Vector3.Multiply(ref vector, 1f / (float)Math.Sqrt((double)num), out vector);
			}
			Vector3 vector2 = rotateAxis;
			float num2;
			Vector3.Dot(ref rotateAxis, ref vector, out num2);
			Vector3 vector3;
			Vector3 vector4;
			if (Math.Abs(num2) > 0.9982547f)
			{
				if (objectForwardVector != null)
				{
					vector3 = objectForwardVector.Value;
					Vector3.Dot(ref rotateAxis, ref vector3, out num2);
					if (Math.Abs(num2) > 0.9982547f)
					{
						num2 = rotateAxis.X * Vector3.Forward.X + rotateAxis.Y * Vector3.Forward.Y + rotateAxis.Z * Vector3.Forward.Z;
						vector3 = ((Math.Abs(num2) > 0.9982547f) ? Vector3.Right : Vector3.Forward);
					}
				}
				else
				{
					num2 = rotateAxis.X * Vector3.Forward.X + rotateAxis.Y * Vector3.Forward.Y + rotateAxis.Z * Vector3.Forward.Z;
					vector3 = ((Math.Abs(num2) > 0.9982547f) ? Vector3.Right : Vector3.Forward);
				}
				Vector3.Cross(ref rotateAxis, ref vector3, out vector4);
				vector4.Normalize();
				Vector3.Cross(ref vector4, ref rotateAxis, out vector3);
				vector3.Normalize();
			}
			else
			{
				Vector3.Cross(ref rotateAxis, ref vector, out vector4);
				vector4.Normalize();
				Vector3.Cross(ref vector4, ref vector2, out vector3);
				vector3.Normalize();
			}
			result.M11 = vector4.X;
			result.M12 = vector4.Y;
			result.M13 = vector4.Z;
			result.M14 = 0f;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = 0f;
			result.M31 = vector3.X;
			result.M32 = vector3.Y;
			result.M33 = vector3.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0001590C File Offset: 0x00013B0C
		public static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
		{
			Matrix matrix;
			Matrix.CreateFromAxisAngle(ref axis, angle, out matrix);
			return matrix;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x00015924 File Offset: 0x00013B24
		public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Matrix result)
		{
			float x = axis.X;
			float y = axis.Y;
			float z = axis.Z;
			float num = (float)Math.Sin((double)angle);
			float num2 = (float)Math.Cos((double)angle);
			float num3 = x * x;
			float num4 = y * y;
			float num5 = z * z;
			float num6 = x * y;
			float num7 = x * z;
			float num8 = y * z;
			result.M11 = num3 + num2 * (1f - num3);
			result.M12 = num6 - num2 * num6 + num * z;
			result.M13 = num7 - num2 * num7 - num * y;
			result.M14 = 0f;
			result.M21 = num6 - num2 * num6 - num * z;
			result.M22 = num4 + num2 * (1f - num4);
			result.M23 = num8 - num2 * num8 + num * x;
			result.M24 = 0f;
			result.M31 = num7 - num2 * num7 + num * y;
			result.M32 = num8 - num2 * num8 - num * x;
			result.M33 = num5 + num2 * (1f - num5);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00015A6C File Offset: 0x00013C6C
		public static Matrix CreateFromQuaternion(Quaternion quaternion)
		{
			Matrix matrix;
			Matrix.CreateFromQuaternion(ref quaternion, out matrix);
			return matrix;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00015A84 File Offset: 0x00013C84
		public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix result)
		{
			float num = quaternion.X * quaternion.X;
			float num2 = quaternion.Y * quaternion.Y;
			float num3 = quaternion.Z * quaternion.Z;
			float num4 = quaternion.X * quaternion.Y;
			float num5 = quaternion.Z * quaternion.W;
			float num6 = quaternion.Z * quaternion.X;
			float num7 = quaternion.Y * quaternion.W;
			float num8 = quaternion.Y * quaternion.Z;
			float num9 = quaternion.X * quaternion.W;
			result.M11 = 1f - 2f * (num2 + num3);
			result.M12 = 2f * (num4 + num5);
			result.M13 = 2f * (num6 - num7);
			result.M14 = 0f;
			result.M21 = 2f * (num4 - num5);
			result.M22 = 1f - 2f * (num3 + num);
			result.M23 = 2f * (num8 + num9);
			result.M24 = 0f;
			result.M31 = 2f * (num6 + num7);
			result.M32 = 2f * (num8 - num9);
			result.M33 = 1f - 2f * (num2 + num);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00015C04 File Offset: 0x00013E04
		public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			Matrix matrix;
			Matrix.CreateFromYawPitchRoll(yaw, pitch, roll, out matrix);
			return matrix;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00015C1C File Offset: 0x00013E1C
		public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Matrix result)
		{
			Quaternion quaternion;
			Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out quaternion);
			Matrix.CreateFromQuaternion(ref quaternion, out result);
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00015C3C File Offset: 0x00013E3C
		public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
		{
			Matrix matrix;
			Matrix.CreateLookAt(ref cameraPosition, ref cameraTarget, ref cameraUpVector, out matrix);
			return matrix;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00015C58 File Offset: 0x00013E58
		public static void CreateLookAt(ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix result)
		{
			Vector3 vector = Vector3.Normalize(cameraPosition - cameraTarget);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = 0f;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = 0f;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = -Vector3.Dot(vector2, cameraPosition);
			result.M42 = -Vector3.Dot(vector3, cameraPosition);
			result.M43 = -Vector3.Dot(vector, cameraPosition);
			result.M44 = 1f;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00015D68 File Offset: 0x00013F68
		public static Matrix CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
		{
			Matrix matrix;
			Matrix.CreateOrthographic(width, height, zNearPlane, zFarPlane, out matrix);
			return matrix;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00015D84 File Offset: 0x00013F84
		public static void CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane, out Matrix result)
		{
			result.M11 = 2f / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (result.M42 = 0f);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00015E4C File Offset: 0x0001404C
		public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane)
		{
			Matrix matrix;
			Matrix.CreateOrthographicOffCenter(left, right, bottom, top, zNearPlane, zFarPlane, out matrix);
			return matrix;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00015E6C File Offset: 0x0001406C
		public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane, out Matrix result)
		{
			result.M11 = (float)(2.0 / ((double)right - (double)left));
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = (float)(2.0 / ((double)top - (double)bottom));
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = (float)(1.0 / ((double)zNearPlane - (double)zFarPlane));
			result.M34 = 0f;
			result.M41 = (float)(((double)left + (double)right) / ((double)left - (double)right));
			result.M42 = (float)(((double)top + (double)bottom) / ((double)bottom - (double)top));
			result.M43 = (float)((double)zNearPlane / ((double)zNearPlane - (double)zFarPlane));
			result.M44 = 1f;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00015F74 File Offset: 0x00014174
		public static Matrix CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
		{
			Matrix matrix;
			Matrix.CreatePerspective(width, height, nearPlaneDistance, farPlaneDistance, out matrix);
			return matrix;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00015F90 File Offset: 0x00014190
		public static void CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentException("nearPlaneDistance <= 0");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentException("farPlaneDistance <= 0");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentException("nearPlaneDistance >= farPlaneDistance");
			}
			result.M11 = 2f * nearPlaneDistance / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M31 = (result.M32 = 0f);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00016090 File Offset: 0x00014290
		public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
		{
			Matrix matrix;
			Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, nearPlaneDistance, farPlaneDistance, out matrix);
			return matrix;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000160AC File Offset: 0x000142AC
		public static void CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
		{
			if (fieldOfView <= 0f || fieldOfView >= 3.141593f)
			{
				throw new ArgumentException("fieldOfView <= 0 or >= PI");
			}
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentException("nearPlaneDistance <= 0");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentException("farPlaneDistance <= 0");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentException("nearPlaneDistance >= farPlaneDistance");
			}
			float num = 1f / (float)Math.Tan((double)(fieldOfView * 0.5f));
			float num2 = num / aspectRatio;
			result.M11 = num2;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = num;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (result.M32 = 0f);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000161D0 File Offset: 0x000143D0
		public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
		{
			Matrix matrix;
			Matrix.CreatePerspectiveOffCenter(left, right, bottom, top, nearPlaneDistance, farPlaneDistance, out matrix);
			return matrix;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000161F0 File Offset: 0x000143F0
		public static void CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentException("nearPlaneDistance <= 0");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentException("farPlaneDistance <= 0");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentException("nearPlaneDistance >= farPlaneDistance");
			}
			result.M11 = 2f * nearPlaneDistance / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -1f;
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M41 = (result.M42 = (result.M44 = 0f));
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00016308 File Offset: 0x00014508
		public static Matrix CreateRotationX(float radians)
		{
			Matrix matrix;
			Matrix.CreateRotationX(radians, out matrix);
			return matrix;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00016320 File Offset: 0x00014520
		public static void CreateRotationX(float radians, out Matrix result)
		{
			result = Matrix.Identity;
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			result.M22 = num;
			result.M23 = num2;
			result.M32 = -num2;
			result.M33 = num;
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00016368 File Offset: 0x00014568
		public static Matrix CreateRotationY(float radians)
		{
			Matrix matrix;
			Matrix.CreateRotationY(radians, out matrix);
			return matrix;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00016380 File Offset: 0x00014580
		public static void CreateRotationY(float radians, out Matrix result)
		{
			result = Matrix.Identity;
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			result.M11 = num;
			result.M13 = -num2;
			result.M31 = num2;
			result.M33 = num;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000163C8 File Offset: 0x000145C8
		public static Matrix CreateRotationZ(float radians)
		{
			Matrix matrix;
			Matrix.CreateRotationZ(radians, out matrix);
			return matrix;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000163E0 File Offset: 0x000145E0
		public static void CreateRotationZ(float radians, out Matrix result)
		{
			result = Matrix.Identity;
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			result.M11 = num;
			result.M12 = num2;
			result.M21 = -num2;
			result.M22 = num;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00016428 File Offset: 0x00014628
		public static Matrix CreateScale(float scale)
		{
			Matrix matrix;
			Matrix.CreateScale(scale, scale, scale, out matrix);
			return matrix;
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00016440 File Offset: 0x00014640
		public static void CreateScale(float scale, out Matrix result)
		{
			Matrix.CreateScale(scale, scale, scale, out result);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0001644C File Offset: 0x0001464C
		public static Matrix CreateScale(float xScale, float yScale, float zScale)
		{
			Matrix matrix;
			Matrix.CreateScale(xScale, yScale, zScale, out matrix);
			return matrix;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00016464 File Offset: 0x00014664
		public static void CreateScale(float xScale, float yScale, float zScale, out Matrix result)
		{
			result.M11 = xScale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = zScale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00016518 File Offset: 0x00014718
		public static Matrix CreateScale(Vector3 scales)
		{
			Matrix matrix;
			Matrix.CreateScale(ref scales, out matrix);
			return matrix;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00016530 File Offset: 0x00014730
		public static void CreateScale(ref Vector3 scales, out Matrix result)
		{
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scales.Z;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000165F0 File Offset: 0x000147F0
		public static Matrix CreateShadow(Vector3 lightDirection, Plane plane)
		{
			Matrix matrix;
			Matrix.CreateShadow(ref lightDirection, ref plane, out matrix);
			return matrix;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0001660C File Offset: 0x0001480C
		public static void CreateShadow(ref Vector3 lightDirection, ref Plane plane, out Matrix result)
		{
			float num = plane.Normal.X * lightDirection.X + plane.Normal.Y * lightDirection.Y + plane.Normal.Z * lightDirection.Z;
			float num2 = -plane.Normal.X;
			float num3 = -plane.Normal.Y;
			float num4 = -plane.Normal.Z;
			float num5 = -plane.D;
			result.M11 = num2 * lightDirection.X + num;
			result.M12 = num2 * lightDirection.Y;
			result.M13 = num2 * lightDirection.Z;
			result.M14 = 0f;
			result.M21 = num3 * lightDirection.X;
			result.M22 = num3 * lightDirection.Y + num;
			result.M23 = num3 * lightDirection.Z;
			result.M24 = 0f;
			result.M31 = num4 * lightDirection.X;
			result.M32 = num4 * lightDirection.Y;
			result.M33 = num4 * lightDirection.Z + num;
			result.M34 = 0f;
			result.M41 = num5 * lightDirection.X;
			result.M42 = num5 * lightDirection.Y;
			result.M43 = num5 * lightDirection.Z;
			result.M44 = num;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0001675C File Offset: 0x0001495C
		public static Matrix CreateTranslation(float xPosition, float yPosition, float zPosition)
		{
			Matrix matrix;
			Matrix.CreateTranslation(xPosition, yPosition, zPosition, out matrix);
			return matrix;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00016774 File Offset: 0x00014974
		public static void CreateTranslation(ref Vector3 position, out Matrix result)
		{
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1f;
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00016834 File Offset: 0x00014A34
		public static Matrix CreateTranslation(Vector3 position)
		{
			Matrix matrix;
			Matrix.CreateTranslation(ref position, out matrix);
			return matrix;
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0001684C File Offset: 0x00014A4C
		public static void CreateTranslation(float xPosition, float yPosition, float zPosition, out Matrix result)
		{
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1f;
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00016900 File Offset: 0x00014B00
		public static Matrix CreateReflection(Plane value)
		{
			Matrix matrix;
			Matrix.CreateReflection(ref value, out matrix);
			return matrix;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00016918 File Offset: 0x00014B18
		public static void CreateReflection(ref Plane value, out Matrix result)
		{
			Plane plane;
			Plane.Normalize(ref value, out plane);
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float num = -2f * x;
			float num2 = -2f * y;
			float num3 = -2f * z;
			result.M11 = num * x + 1f;
			result.M12 = num2 * x;
			result.M13 = num3 * x;
			result.M14 = 0f;
			result.M21 = num * y;
			result.M22 = num2 * y + 1f;
			result.M23 = num3 * y;
			result.M24 = 0f;
			result.M31 = num * z;
			result.M32 = num2 * z;
			result.M33 = num3 * z + 1f;
			result.M34 = 0f;
			result.M41 = num * plane.D;
			result.M42 = num2 * plane.D;
			result.M43 = num3 * plane.D;
			result.M44 = 1f;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00016A34 File Offset: 0x00014C34
		public static Matrix CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
		{
			Matrix matrix;
			Matrix.CreateWorld(ref position, ref forward, ref up, out matrix);
			return matrix;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00016A50 File Offset: 0x00014C50
		public static void CreateWorld(ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix result)
		{
			Vector3 vector;
			Vector3.Normalize(ref forward, out vector);
			Vector3 vector2;
			Vector3.Cross(ref forward, ref up, out vector2);
			Vector3 vector3;
			Vector3.Cross(ref vector2, ref forward, out vector3);
			vector2.Normalize();
			vector3.Normalize();
			result = default(Matrix);
			result.Right = vector2;
			result.Up = vector3;
			result.Forward = vector;
			result.Translation = position;
			result.M44 = 1f;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00016ABC File Offset: 0x00014CBC
		public static Matrix Divide(Matrix matrix1, Matrix matrix2)
		{
			matrix1.M11 /= matrix2.M11;
			matrix1.M12 /= matrix2.M12;
			matrix1.M13 /= matrix2.M13;
			matrix1.M14 /= matrix2.M14;
			matrix1.M21 /= matrix2.M21;
			matrix1.M22 /= matrix2.M22;
			matrix1.M23 /= matrix2.M23;
			matrix1.M24 /= matrix2.M24;
			matrix1.M31 /= matrix2.M31;
			matrix1.M32 /= matrix2.M32;
			matrix1.M33 /= matrix2.M33;
			matrix1.M34 /= matrix2.M34;
			matrix1.M41 /= matrix2.M41;
			matrix1.M42 /= matrix2.M42;
			matrix1.M43 /= matrix2.M43;
			matrix1.M44 /= matrix2.M44;
			return matrix1;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00016C0C File Offset: 0x00014E0C
		public static void Divide(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
		{
			result.M11 = matrix1.M11 / matrix2.M11;
			result.M12 = matrix1.M12 / matrix2.M12;
			result.M13 = matrix1.M13 / matrix2.M13;
			result.M14 = matrix1.M14 / matrix2.M14;
			result.M21 = matrix1.M21 / matrix2.M21;
			result.M22 = matrix1.M22 / matrix2.M22;
			result.M23 = matrix1.M23 / matrix2.M23;
			result.M24 = matrix1.M24 / matrix2.M24;
			result.M31 = matrix1.M31 / matrix2.M31;
			result.M32 = matrix1.M32 / matrix2.M32;
			result.M33 = matrix1.M33 / matrix2.M33;
			result.M34 = matrix1.M34 / matrix2.M34;
			result.M41 = matrix1.M41 / matrix2.M41;
			result.M42 = matrix1.M42 / matrix2.M42;
			result.M43 = matrix1.M43 / matrix2.M43;
			result.M44 = matrix1.M44 / matrix2.M44;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00016D4C File Offset: 0x00014F4C
		public static Matrix Divide(Matrix matrix1, float divider)
		{
			matrix1.M11 /= divider;
			matrix1.M12 /= divider;
			matrix1.M13 /= divider;
			matrix1.M14 /= divider;
			matrix1.M21 /= divider;
			matrix1.M22 /= divider;
			matrix1.M23 /= divider;
			matrix1.M24 /= divider;
			matrix1.M31 /= divider;
			matrix1.M32 /= divider;
			matrix1.M33 /= divider;
			matrix1.M34 /= divider;
			matrix1.M41 /= divider;
			matrix1.M42 /= divider;
			matrix1.M43 /= divider;
			matrix1.M44 /= divider;
			return matrix1;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00016E4C File Offset: 0x0001504C
		public static void Divide(ref Matrix matrix1, float divider, out Matrix result)
		{
			result.M11 = matrix1.M11 / divider;
			result.M12 = matrix1.M12 / divider;
			result.M13 = matrix1.M13 / divider;
			result.M14 = matrix1.M14 / divider;
			result.M21 = matrix1.M21 / divider;
			result.M22 = matrix1.M22 / divider;
			result.M23 = matrix1.M23 / divider;
			result.M24 = matrix1.M24 / divider;
			result.M31 = matrix1.M31 / divider;
			result.M32 = matrix1.M32 / divider;
			result.M33 = matrix1.M33 / divider;
			result.M34 = matrix1.M34 / divider;
			result.M41 = matrix1.M41 / divider;
			result.M42 = matrix1.M42 / divider;
			result.M43 = matrix1.M43 / divider;
			result.M44 = matrix1.M44 / divider;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00016F39 File Offset: 0x00015139
		public static Matrix Invert(Matrix matrix)
		{
			Matrix.Invert(ref matrix, out matrix);
			return matrix;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00016F48 File Offset: 0x00015148
		public static void Invert(ref Matrix matrix, out Matrix result)
		{
			float m = matrix.M11;
			float m2 = matrix.M12;
			float m3 = matrix.M13;
			float m4 = matrix.M14;
			float m5 = matrix.M21;
			float m6 = matrix.M22;
			float m7 = matrix.M23;
			float m8 = matrix.M24;
			float m9 = matrix.M31;
			float m10 = matrix.M32;
			float m11 = matrix.M33;
			float m12 = matrix.M34;
			float m13 = matrix.M41;
			float m14 = matrix.M42;
			float m15 = matrix.M43;
			float m16 = matrix.M44;
			float num = (float)((double)m11 * (double)m16 - (double)m12 * (double)m15);
			float num2 = (float)((double)m10 * (double)m16 - (double)m12 * (double)m14);
			float num3 = (float)((double)m10 * (double)m15 - (double)m11 * (double)m14);
			float num4 = (float)((double)m9 * (double)m16 - (double)m12 * (double)m13);
			float num5 = (float)((double)m9 * (double)m15 - (double)m11 * (double)m13);
			float num6 = (float)((double)m9 * (double)m14 - (double)m10 * (double)m13);
			float num7 = (float)((double)m6 * (double)num - (double)m7 * (double)num2 + (double)m8 * (double)num3);
			float num8 = (float)(-(float)((double)m5 * (double)num - (double)m7 * (double)num4 + (double)m8 * (double)num5));
			float num9 = (float)((double)m5 * (double)num2 - (double)m6 * (double)num4 + (double)m8 * (double)num6);
			float num10 = (float)(-(float)((double)m5 * (double)num3 - (double)m6 * (double)num5 + (double)m7 * (double)num6));
			float num11 = (float)(1.0 / ((double)m * (double)num7 + (double)m2 * (double)num8 + (double)m3 * (double)num9 + (double)m4 * (double)num10));
			result.M11 = num7 * num11;
			result.M21 = num8 * num11;
			result.M31 = num9 * num11;
			result.M41 = num10 * num11;
			result.M12 = (float)(-(float)((double)m2 * (double)num - (double)m3 * (double)num2 + (double)m4 * (double)num3) * (double)num11);
			result.M22 = (float)(((double)m * (double)num - (double)m3 * (double)num4 + (double)m4 * (double)num5) * (double)num11);
			result.M32 = (float)(-(float)((double)m * (double)num2 - (double)m2 * (double)num4 + (double)m4 * (double)num6) * (double)num11);
			result.M42 = (float)(((double)m * (double)num3 - (double)m2 * (double)num5 + (double)m3 * (double)num6) * (double)num11);
			float num12 = (float)((double)m7 * (double)m16 - (double)m8 * (double)m15);
			float num13 = (float)((double)m6 * (double)m16 - (double)m8 * (double)m14);
			float num14 = (float)((double)m6 * (double)m15 - (double)m7 * (double)m14);
			float num15 = (float)((double)m5 * (double)m16 - (double)m8 * (double)m13);
			float num16 = (float)((double)m5 * (double)m15 - (double)m7 * (double)m13);
			float num17 = (float)((double)m5 * (double)m14 - (double)m6 * (double)m13);
			result.M13 = (float)(((double)m2 * (double)num12 - (double)m3 * (double)num13 + (double)m4 * (double)num14) * (double)num11);
			result.M23 = (float)(-(float)((double)m * (double)num12 - (double)m3 * (double)num15 + (double)m4 * (double)num16) * (double)num11);
			result.M33 = (float)(((double)m * (double)num13 - (double)m2 * (double)num15 + (double)m4 * (double)num17) * (double)num11);
			result.M43 = (float)(-(float)((double)m * (double)num14 - (double)m2 * (double)num16 + (double)m3 * (double)num17) * (double)num11);
			float num18 = (float)((double)m7 * (double)m12 - (double)m8 * (double)m11);
			float num19 = (float)((double)m6 * (double)m12 - (double)m8 * (double)m10);
			float num20 = (float)((double)m6 * (double)m11 - (double)m7 * (double)m10);
			float num21 = (float)((double)m5 * (double)m12 - (double)m8 * (double)m9);
			float num22 = (float)((double)m5 * (double)m11 - (double)m7 * (double)m9);
			float num23 = (float)((double)m5 * (double)m10 - (double)m6 * (double)m9);
			result.M14 = (float)(-(float)((double)m2 * (double)num18 - (double)m3 * (double)num19 + (double)m4 * (double)num20) * (double)num11);
			result.M24 = (float)(((double)m * (double)num18 - (double)m3 * (double)num21 + (double)m4 * (double)num22) * (double)num11);
			result.M34 = (float)(-(float)((double)m * (double)num19 - (double)m2 * (double)num21 + (double)m4 * (double)num23) * (double)num11);
			result.M44 = (float)(((double)m * (double)num20 - (double)m2 * (double)num22 + (double)m3 * (double)num23) * (double)num11);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00017344 File Offset: 0x00015544
		public static Matrix Lerp(Matrix matrix1, Matrix matrix2, float amount)
		{
			matrix1.M11 += (matrix2.M11 - matrix1.M11) * amount;
			matrix1.M12 += (matrix2.M12 - matrix1.M12) * amount;
			matrix1.M13 += (matrix2.M13 - matrix1.M13) * amount;
			matrix1.M14 += (matrix2.M14 - matrix1.M14) * amount;
			matrix1.M21 += (matrix2.M21 - matrix1.M21) * amount;
			matrix1.M22 += (matrix2.M22 - matrix1.M22) * amount;
			matrix1.M23 += (matrix2.M23 - matrix1.M23) * amount;
			matrix1.M24 += (matrix2.M24 - matrix1.M24) * amount;
			matrix1.M31 += (matrix2.M31 - matrix1.M31) * amount;
			matrix1.M32 += (matrix2.M32 - matrix1.M32) * amount;
			matrix1.M33 += (matrix2.M33 - matrix1.M33) * amount;
			matrix1.M34 += (matrix2.M34 - matrix1.M34) * amount;
			matrix1.M41 += (matrix2.M41 - matrix1.M41) * amount;
			matrix1.M42 += (matrix2.M42 - matrix1.M42) * amount;
			matrix1.M43 += (matrix2.M43 - matrix1.M43) * amount;
			matrix1.M44 += (matrix2.M44 - matrix1.M44) * amount;
			return matrix1;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00017524 File Offset: 0x00015724
		public static void Lerp(ref Matrix matrix1, ref Matrix matrix2, float amount, out Matrix result)
		{
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
			result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
			result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
			result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
			result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
			result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
			result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
			result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000176F4 File Offset: 0x000158F4
		public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
		{
			float num = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
			float num2 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
			float num3 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
			float num4 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
			float num5 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
			float num6 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
			float num7 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
			float num8 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
			float num9 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
			float num10 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
			float num11 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
			float num12 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
			float num13 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
			float num14 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
			float num15 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
			float num16 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
			matrix1.M11 = num;
			matrix1.M12 = num2;
			matrix1.M13 = num3;
			matrix1.M14 = num4;
			matrix1.M21 = num5;
			matrix1.M22 = num6;
			matrix1.M23 = num7;
			matrix1.M24 = num8;
			matrix1.M31 = num9;
			matrix1.M32 = num10;
			matrix1.M33 = num11;
			matrix1.M34 = num12;
			matrix1.M41 = num13;
			matrix1.M42 = num14;
			matrix1.M43 = num15;
			matrix1.M44 = num16;
			return matrix1;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00017B1C File Offset: 0x00015D1C
		public static void Multiply(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
		{
			float num = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
			float num2 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
			float num3 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
			float num4 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
			float num5 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
			float num6 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
			float num7 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
			float num8 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
			float num9 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
			float num10 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
			float num11 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
			float num12 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
			float num13 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
			float num14 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
			float num15 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
			float num16 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
			result.M11 = num;
			result.M12 = num2;
			result.M13 = num3;
			result.M14 = num4;
			result.M21 = num5;
			result.M22 = num6;
			result.M23 = num7;
			result.M24 = num8;
			result.M31 = num9;
			result.M32 = num10;
			result.M33 = num11;
			result.M34 = num12;
			result.M41 = num13;
			result.M42 = num14;
			result.M43 = num15;
			result.M44 = num16;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00017F34 File Offset: 0x00016134
		public static Matrix Multiply(Matrix matrix1, float scaleFactor)
		{
			matrix1.M11 *= scaleFactor;
			matrix1.M12 *= scaleFactor;
			matrix1.M13 *= scaleFactor;
			matrix1.M14 *= scaleFactor;
			matrix1.M21 *= scaleFactor;
			matrix1.M22 *= scaleFactor;
			matrix1.M23 *= scaleFactor;
			matrix1.M24 *= scaleFactor;
			matrix1.M31 *= scaleFactor;
			matrix1.M32 *= scaleFactor;
			matrix1.M33 *= scaleFactor;
			matrix1.M34 *= scaleFactor;
			matrix1.M41 *= scaleFactor;
			matrix1.M42 *= scaleFactor;
			matrix1.M43 *= scaleFactor;
			matrix1.M44 *= scaleFactor;
			return matrix1;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00018004 File Offset: 0x00016204
		public static void Multiply(ref Matrix matrix1, float scaleFactor, out Matrix result)
		{
			result.M11 = matrix1.M11 * scaleFactor;
			result.M12 = matrix1.M12 * scaleFactor;
			result.M13 = matrix1.M13 * scaleFactor;
			result.M14 = matrix1.M14 * scaleFactor;
			result.M21 = matrix1.M21 * scaleFactor;
			result.M22 = matrix1.M22 * scaleFactor;
			result.M23 = matrix1.M23 * scaleFactor;
			result.M24 = matrix1.M24 * scaleFactor;
			result.M31 = matrix1.M31 * scaleFactor;
			result.M32 = matrix1.M32 * scaleFactor;
			result.M33 = matrix1.M33 * scaleFactor;
			result.M34 = matrix1.M34 * scaleFactor;
			result.M41 = matrix1.M41 * scaleFactor;
			result.M42 = matrix1.M42 * scaleFactor;
			result.M43 = matrix1.M43 * scaleFactor;
			result.M44 = matrix1.M44 * scaleFactor;
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000180F4 File Offset: 0x000162F4
		public static Matrix Negate(Matrix matrix)
		{
			matrix.M11 = -matrix.M11;
			matrix.M12 = -matrix.M12;
			matrix.M13 = -matrix.M13;
			matrix.M14 = -matrix.M14;
			matrix.M21 = -matrix.M21;
			matrix.M22 = -matrix.M22;
			matrix.M23 = -matrix.M23;
			matrix.M24 = -matrix.M24;
			matrix.M31 = -matrix.M31;
			matrix.M32 = -matrix.M32;
			matrix.M33 = -matrix.M33;
			matrix.M34 = -matrix.M34;
			matrix.M41 = -matrix.M41;
			matrix.M42 = -matrix.M42;
			matrix.M43 = -matrix.M43;
			matrix.M44 = -matrix.M44;
			return matrix;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000181E4 File Offset: 0x000163E4
		public static void Negate(ref Matrix matrix, out Matrix result)
		{
			result.M11 = -matrix.M11;
			result.M12 = -matrix.M12;
			result.M13 = -matrix.M13;
			result.M14 = -matrix.M14;
			result.M21 = -matrix.M21;
			result.M22 = -matrix.M22;
			result.M23 = -matrix.M23;
			result.M24 = -matrix.M24;
			result.M31 = -matrix.M31;
			result.M32 = -matrix.M32;
			result.M33 = -matrix.M33;
			result.M34 = -matrix.M34;
			result.M41 = -matrix.M41;
			result.M42 = -matrix.M42;
			result.M43 = -matrix.M43;
			result.M44 = -matrix.M44;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000182C4 File Offset: 0x000164C4
		public static Matrix Subtract(Matrix matrix1, Matrix matrix2)
		{
			matrix1.M11 -= matrix2.M11;
			matrix1.M12 -= matrix2.M12;
			matrix1.M13 -= matrix2.M13;
			matrix1.M14 -= matrix2.M14;
			matrix1.M21 -= matrix2.M21;
			matrix1.M22 -= matrix2.M22;
			matrix1.M23 -= matrix2.M23;
			matrix1.M24 -= matrix2.M24;
			matrix1.M31 -= matrix2.M31;
			matrix1.M32 -= matrix2.M32;
			matrix1.M33 -= matrix2.M33;
			matrix1.M34 -= matrix2.M34;
			matrix1.M41 -= matrix2.M41;
			matrix1.M42 -= matrix2.M42;
			matrix1.M43 -= matrix2.M43;
			matrix1.M44 -= matrix2.M44;
			return matrix1;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000183E4 File Offset: 0x000165E4
		public static void Subtract(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
		{
			result.M11 = matrix1.M11 - matrix2.M11;
			result.M12 = matrix1.M12 - matrix2.M12;
			result.M13 = matrix1.M13 - matrix2.M13;
			result.M14 = matrix1.M14 - matrix2.M14;
			result.M21 = matrix1.M21 - matrix2.M21;
			result.M22 = matrix1.M22 - matrix2.M22;
			result.M23 = matrix1.M23 - matrix2.M23;
			result.M24 = matrix1.M24 - matrix2.M24;
			result.M31 = matrix1.M31 - matrix2.M31;
			result.M32 = matrix1.M32 - matrix2.M32;
			result.M33 = matrix1.M33 - matrix2.M33;
			result.M34 = matrix1.M34 - matrix2.M34;
			result.M41 = matrix1.M41 - matrix2.M41;
			result.M42 = matrix1.M42 - matrix2.M42;
			result.M43 = matrix1.M43 - matrix2.M43;
			result.M44 = matrix1.M44 - matrix2.M44;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00018524 File Offset: 0x00016724
		public static Matrix Transpose(Matrix matrix)
		{
			Matrix matrix2;
			Matrix.Transpose(ref matrix, out matrix2);
			return matrix2;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0001853C File Offset: 0x0001673C
		public static void Transpose(ref Matrix matrix, out Matrix result)
		{
			Matrix matrix2;
			matrix2.M11 = matrix.M11;
			matrix2.M12 = matrix.M21;
			matrix2.M13 = matrix.M31;
			matrix2.M14 = matrix.M41;
			matrix2.M21 = matrix.M12;
			matrix2.M22 = matrix.M22;
			matrix2.M23 = matrix.M32;
			matrix2.M24 = matrix.M42;
			matrix2.M31 = matrix.M13;
			matrix2.M32 = matrix.M23;
			matrix2.M33 = matrix.M33;
			matrix2.M34 = matrix.M43;
			matrix2.M41 = matrix.M14;
			matrix2.M42 = matrix.M24;
			matrix2.M43 = matrix.M34;
			matrix2.M44 = matrix.M44;
			result = matrix2;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00018620 File Offset: 0x00016820
		public static Matrix Transform(Matrix value, Quaternion rotation)
		{
			Matrix matrix;
			Matrix.Transform(ref value, ref rotation, out matrix);
			return matrix;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0001863C File Offset: 0x0001683C
		public static void Transform(ref Matrix value, ref Quaternion rotation, out Matrix result)
		{
			Matrix matrix = Matrix.CreateFromQuaternion(rotation);
			Matrix.Multiply(ref value, ref matrix, out result);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0001865E File Offset: 0x0001685E
		public static Matrix operator +(Matrix matrix1, Matrix matrix2)
		{
			return Matrix.Add(matrix1, matrix2);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00018667 File Offset: 0x00016867
		public static Matrix operator /(Matrix matrix1, Matrix matrix2)
		{
			return Matrix.Divide(matrix1, matrix2);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00018670 File Offset: 0x00016870
		public static Matrix operator /(Matrix matrix, float divider)
		{
			return Matrix.Divide(matrix, divider);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00018679 File Offset: 0x00016879
		public static bool operator ==(Matrix matrix1, Matrix matrix2)
		{
			return matrix1.Equals(matrix2);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00018683 File Offset: 0x00016883
		public static bool operator !=(Matrix matrix1, Matrix matrix2)
		{
			return !matrix1.Equals(matrix2);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00018690 File Offset: 0x00016890
		public static Matrix operator *(Matrix matrix1, Matrix matrix2)
		{
			return Matrix.Multiply(matrix1, matrix2);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x00018699 File Offset: 0x00016899
		public static Matrix operator *(Matrix matrix, float scaleFactor)
		{
			return Matrix.Multiply(matrix, scaleFactor);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000186A2 File Offset: 0x000168A2
		public static Matrix operator -(Matrix matrix1, Matrix matrix2)
		{
			return Matrix.Subtract(matrix1, matrix2);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x000186AB File Offset: 0x000168AB
		public static Matrix operator -(Matrix matrix)
		{
			return Matrix.Negate(matrix);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000186B4 File Offset: 0x000168B4
		// Note: this type is marked as 'beforefieldinit'.
		static Matrix()
		{
		}

		// Token: 0x04000590 RID: 1424
		public float M11;

		// Token: 0x04000591 RID: 1425
		public float M12;

		// Token: 0x04000592 RID: 1426
		public float M13;

		// Token: 0x04000593 RID: 1427
		public float M14;

		// Token: 0x04000594 RID: 1428
		public float M21;

		// Token: 0x04000595 RID: 1429
		public float M22;

		// Token: 0x04000596 RID: 1430
		public float M23;

		// Token: 0x04000597 RID: 1431
		public float M24;

		// Token: 0x04000598 RID: 1432
		public float M31;

		// Token: 0x04000599 RID: 1433
		public float M32;

		// Token: 0x0400059A RID: 1434
		public float M33;

		// Token: 0x0400059B RID: 1435
		public float M34;

		// Token: 0x0400059C RID: 1436
		public float M41;

		// Token: 0x0400059D RID: 1437
		public float M42;

		// Token: 0x0400059E RID: 1438
		public float M43;

		// Token: 0x0400059F RID: 1439
		public float M44;

		// Token: 0x040005A0 RID: 1440
		private static Matrix identity = new Matrix(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
	}
}
