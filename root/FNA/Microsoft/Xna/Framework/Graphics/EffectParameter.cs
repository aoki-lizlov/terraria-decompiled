using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000084 RID: 132
	public sealed class EffectParameter
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00026BC4 File Offset: 0x00024DC4
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x00026BCC File Offset: 0x00024DCC
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x00026BD5 File Offset: 0x00024DD5
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x00026BDD File Offset: 0x00024DDD
		public string Semantic
		{
			[CompilerGenerated]
			get
			{
				return this.<Semantic>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Semantic>k__BackingField = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00026BE6 File Offset: 0x00024DE6
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x00026BEE File Offset: 0x00024DEE
		public int RowCount
		{
			[CompilerGenerated]
			get
			{
				return this.<RowCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<RowCount>k__BackingField = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x00026BF7 File Offset: 0x00024DF7
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x00026BFF File Offset: 0x00024DFF
		public int ColumnCount
		{
			[CompilerGenerated]
			get
			{
				return this.<ColumnCount>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ColumnCount>k__BackingField = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x00026C08 File Offset: 0x00024E08
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x00026C10 File Offset: 0x00024E10
		public EffectParameterClass ParameterClass
		{
			[CompilerGenerated]
			get
			{
				return this.<ParameterClass>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ParameterClass>k__BackingField = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x00026C19 File Offset: 0x00024E19
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x00026C21 File Offset: 0x00024E21
		public EffectParameterType ParameterType
		{
			[CompilerGenerated]
			get
			{
				return this.<ParameterType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ParameterType>k__BackingField = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x00026C2A File Offset: 0x00024E2A
		public EffectParameterCollection Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.BuildElementList();
				}
				return this.elements;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00026C40 File Offset: 0x00024E40
		public EffectParameterCollection StructureMembers
		{
			get
			{
				if (this.members == null)
				{
					this.BuildMemberList();
				}
				return this.members;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00026C56 File Offset: 0x00024E56
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x00026C5E File Offset: 0x00024E5E
		public EffectAnnotationCollection Annotations
		{
			[CompilerGenerated]
			get
			{
				return this.<Annotations>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Annotations>k__BackingField = value;
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00026C68 File Offset: 0x00024E68
		internal EffectParameter(string name, string semantic, int rowCount, int columnCount, int elementCount, EffectParameterClass parameterClass, EffectParameterType parameterType, IntPtr mojoType, EffectAnnotationCollection annotations, IntPtr data, uint dataSizeBytes, Effect effect)
		{
			if (data == IntPtr.Zero)
			{
				throw new ArgumentNullException("data");
			}
			this.Name = name;
			this.Semantic = semantic ?? string.Empty;
			this.RowCount = rowCount;
			this.ColumnCount = columnCount;
			this.elementCount = elementCount;
			this.ParameterClass = parameterClass;
			this.ParameterType = parameterType;
			this.mojoType = mojoType;
			this.Annotations = annotations;
			this.values = data;
			this.valuesSizeBytes = dataSizeBytes;
			this.outer = effect;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00026D08 File Offset: 0x00024F08
		internal EffectParameter(string name, string semantic, int rowCount, int columnCount, int elementCount, EffectParameterClass parameterClass, EffectParameterType parameterType, EffectParameterCollection structureMembers, EffectAnnotationCollection annotations, IntPtr data, uint dataSizeBytes, Effect effect)
		{
			if (data == IntPtr.Zero)
			{
				throw new ArgumentNullException("data");
			}
			this.Name = name;
			this.Semantic = semantic ?? string.Empty;
			this.RowCount = rowCount;
			this.ColumnCount = columnCount;
			this.elementCount = elementCount;
			this.ParameterClass = parameterClass;
			this.ParameterType = parameterType;
			this.members = structureMembers;
			this.Annotations = annotations;
			this.values = data;
			this.valuesSizeBytes = dataSizeBytes;
			this.outer = effect;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00026DA5 File Offset: 0x00024FA5
		internal void BuildMemberList()
		{
			this.members = Effect.INTERNAL_readEffectParameterStructureMembers(this, this.mojoType, this.outer);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00026DC0 File Offset: 0x00024FC0
		internal void BuildElementList()
		{
			int num = 0;
			List<EffectParameter> list = new List<EffectParameter>(this.elementCount);
			EffectParameterCollection structureMembers = this.StructureMembers;
			for (int i = 0; i < this.elementCount; i++)
			{
				EffectParameterCollection effectParameterCollection = null;
				if (structureMembers != null)
				{
					List<EffectParameter> list2 = new List<EffectParameter>();
					for (int j = 0; j < structureMembers.Count; j++)
					{
						int num2 = 0;
						if (structureMembers[j].Elements != null)
						{
							num2 = structureMembers[j].Elements.Count;
						}
						int num3 = structureMembers[j].RowCount * 4;
						if (num2 > 0)
						{
							num3 *= num2;
						}
						list2.Add(new EffectParameter(structureMembers[j].Name, structureMembers[j].Semantic, structureMembers[j].RowCount, structureMembers[j].ColumnCount, num2, structureMembers[j].ParameterClass, structureMembers[j].ParameterType, IntPtr.Zero, structureMembers[j].Annotations, new IntPtr(this.values.ToInt64() + (long)num), (uint)(num3 * 4), this.outer));
						num += num3 * 4;
					}
					effectParameterCollection = new EffectParameterCollection(list2);
				}
				list.Add(new EffectParameter(null, null, this.RowCount, this.ColumnCount, 0, this.ParameterClass, this.ParameterType, effectParameterCollection, null, new IntPtr(this.values.ToInt64() + (long)(i * this.RowCount * 16)), 0U, this.outer));
			}
			this.elements = new EffectParameterCollection(list);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00026F5C File Offset: 0x0002515C
		public unsafe bool GetValueBoolean()
		{
			int* ptr = (int*)(void*)this.values;
			return *ptr != 0;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00026F7C File Offset: 0x0002517C
		public unsafe bool[] GetValueBooleanArray(int count)
		{
			bool[] array = new bool[count];
			int* ptr = (int*)(void*)this.values;
			int i = 0;
			while (i < array.Length)
			{
				int j = 0;
				while (j < this.ColumnCount)
				{
					array[i] = ptr[j] != 0;
					j++;
					i++;
				}
				ptr += 4;
			}
			return array;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00026FD0 File Offset: 0x000251D0
		public unsafe int GetValueInt32()
		{
			int* ptr = (int*)(void*)this.values;
			return *ptr;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00026FEC File Offset: 0x000251EC
		public int[] GetValueInt32Array(int count)
		{
			int[] array = new int[count];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				Marshal.Copy(this.values + num, array, i, this.ColumnCount);
				i += this.ColumnCount;
				num += 16;
			}
			return array;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00027034 File Offset: 0x00025234
		public unsafe Matrix GetValueMatrixTranspose()
		{
			float* ptr = (float*)(void*)this.values;
			return new Matrix(*ptr, ptr[1], ptr[2], ptr[3], ptr[4], ptr[5], ptr[6], ptr[7], ptr[8], ptr[9], ptr[10], ptr[11], ptr[12], ptr[13], ptr[14], ptr[15]);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000270C4 File Offset: 0x000252C4
		public unsafe Matrix[] GetValueMatrixTransposeArray(int count)
		{
			Matrix[] array = new Matrix[count];
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < count)
			{
				array[i] = new Matrix(*ptr, ptr[1], ptr[2], ptr[3], ptr[4], ptr[5], ptr[6], ptr[7], ptr[8], ptr[9], ptr[10], ptr[11], ptr[12], ptr[13], ptr[14], ptr[15]);
				i++;
				ptr += 16;
			}
			return array;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0002717C File Offset: 0x0002537C
		public unsafe Matrix GetValueMatrix()
		{
			float* ptr = (float*)(void*)this.values;
			return new Matrix(*ptr, ptr[4], ptr[8], ptr[12], ptr[1], ptr[5], ptr[9], ptr[13], ptr[2], ptr[6], ptr[10], ptr[14], ptr[3], ptr[7], ptr[11], ptr[15]);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0002720C File Offset: 0x0002540C
		public unsafe Matrix[] GetValueMatrixArray(int count)
		{
			Matrix[] array = new Matrix[count];
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < count)
			{
				array[i] = new Matrix(*ptr, ptr[4], ptr[8], ptr[12], ptr[1], ptr[5], ptr[9], ptr[13], ptr[2], ptr[6], ptr[10], ptr[14], ptr[3], ptr[7], ptr[11], ptr[15]);
				i++;
				ptr += 16;
			}
			return array;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000272C4 File Offset: 0x000254C4
		public unsafe Quaternion GetValueQuaternion()
		{
			float* ptr = (float*)(void*)this.values;
			return new Quaternion(*ptr, ptr[1], ptr[2], ptr[3]);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000272F8 File Offset: 0x000254F8
		public unsafe Quaternion[] GetValueQuaternionArray(int count)
		{
			Quaternion[] array = new Quaternion[count];
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < count)
			{
				array[i] = new Quaternion(*ptr, ptr[1], ptr[2], ptr[3]);
				i++;
				ptr += 4;
			}
			return array;
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0002734C File Offset: 0x0002554C
		public unsafe float GetValueSingle()
		{
			float* ptr = (float*)(void*)this.values;
			return *ptr;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00027368 File Offset: 0x00025568
		public float[] GetValueSingleArray(int count)
		{
			float[] array = new float[count];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				Marshal.Copy(this.values + num, array, i, this.ColumnCount);
				i += this.ColumnCount;
				num += 16;
			}
			return array;
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000273B0 File Offset: 0x000255B0
		public string GetValueString()
		{
			return this.cachedString;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000273B8 File Offset: 0x000255B8
		public Texture2D GetValueTexture2D()
		{
			return (Texture2D)this.texture;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000273C5 File Offset: 0x000255C5
		public Texture3D GetValueTexture3D()
		{
			return (Texture3D)this.texture;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000273D2 File Offset: 0x000255D2
		public TextureCube GetValueTextureCube()
		{
			return (TextureCube)this.texture;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000273E0 File Offset: 0x000255E0
		public unsafe Vector2 GetValueVector2()
		{
			float* ptr = (float*)(void*)this.values;
			return new Vector2(*ptr, ptr[1]);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00027404 File Offset: 0x00025604
		public unsafe Vector2[] GetValueVector2Array(int count)
		{
			Vector2[] array = new Vector2[count];
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < count)
			{
				array[i] = new Vector2(*ptr, ptr[1]);
				i++;
				ptr += 4;
			}
			return array;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0002744C File Offset: 0x0002564C
		public unsafe Vector3 GetValueVector3()
		{
			float* ptr = (float*)(void*)this.values;
			return new Vector3(*ptr, ptr[1], ptr[2]);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00027478 File Offset: 0x00025678
		public unsafe Vector3[] GetValueVector3Array(int count)
		{
			Vector3[] array = new Vector3[count];
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < count)
			{
				array[i] = new Vector3(*ptr, ptr[1], ptr[2]);
				i++;
				ptr += 4;
			}
			return array;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000274C8 File Offset: 0x000256C8
		public unsafe Vector4 GetValueVector4()
		{
			float* ptr = (float*)(void*)this.values;
			return new Vector4(*ptr, ptr[1], ptr[2], ptr[3]);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000274FC File Offset: 0x000256FC
		public unsafe Vector4[] GetValueVector4Array(int count)
		{
			Vector4[] array = new Vector4[count];
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < count)
			{
				array[i] = new Vector4(*ptr, ptr[1], ptr[2], ptr[3]);
				i++;
				ptr += 4;
			}
			return array;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00027550 File Offset: 0x00025750
		public unsafe void SetValue(bool value)
		{
			int* ptr = (int*)(void*)this.values;
			*ptr = ((value > false) ? 1 : 0);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00027570 File Offset: 0x00025770
		public unsafe void SetValue(bool[] value)
		{
			int* ptr = (int*)(void*)this.values;
			int i = 0;
			while (i < value.Length)
			{
				int j = 0;
				while (j < this.ColumnCount)
				{
					ptr[j] = ((value[i] > false) ? 1 : 0);
					j++;
					i++;
				}
				ptr += 4;
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000275BC File Offset: 0x000257BC
		public unsafe void SetValue(int value)
		{
			if (this.ParameterType == EffectParameterType.Single)
			{
				float* ptr = (float*)(void*)this.values;
				*ptr = (float)value;
				return;
			}
			int* ptr2 = (int*)(void*)this.values;
			*ptr2 = value;
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000275F4 File Offset: 0x000257F4
		public void SetValue(int[] value)
		{
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				Marshal.Copy(value, i, this.values + num, this.ColumnCount);
				i += this.ColumnCount;
				num += 16;
			}
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00027634 File Offset: 0x00025834
		public unsafe void SetValueTranspose(Matrix value)
		{
			float* ptr = (float*)(void*)this.values;
			if (this.ColumnCount == 4 && this.RowCount == 4)
			{
				*ptr = value.M11;
				ptr[1] = value.M12;
				ptr[2] = value.M13;
				ptr[3] = value.M14;
				ptr[4] = value.M21;
				ptr[5] = value.M22;
				ptr[6] = value.M23;
				ptr[7] = value.M24;
				ptr[8] = value.M31;
				ptr[9] = value.M32;
				ptr[10] = value.M33;
				ptr[11] = value.M34;
				ptr[12] = value.M41;
				ptr[13] = value.M42;
				ptr[14] = value.M43;
				ptr[15] = value.M44;
				return;
			}
			if (this.ColumnCount == 3 && this.RowCount == 3)
			{
				*ptr = value.M11;
				ptr[1] = value.M12;
				ptr[2] = value.M13;
				ptr[4] = value.M21;
				ptr[5] = value.M22;
				ptr[6] = value.M23;
				ptr[8] = value.M31;
				ptr[9] = value.M32;
				ptr[10] = value.M33;
				return;
			}
			if (this.ColumnCount == 4 && this.RowCount == 3)
			{
				*ptr = value.M11;
				ptr[1] = value.M12;
				ptr[2] = value.M13;
				ptr[4] = value.M21;
				ptr[5] = value.M22;
				ptr[6] = value.M23;
				ptr[8] = value.M31;
				ptr[9] = value.M32;
				ptr[10] = value.M33;
				ptr[12] = value.M41;
				ptr[13] = value.M42;
				ptr[14] = value.M43;
				return;
			}
			if (this.ColumnCount == 3 && this.RowCount == 4)
			{
				*ptr = value.M11;
				ptr[1] = value.M12;
				ptr[2] = value.M13;
				ptr[3] = value.M14;
				ptr[4] = value.M21;
				ptr[5] = value.M22;
				ptr[6] = value.M23;
				ptr[7] = value.M24;
				ptr[8] = value.M31;
				ptr[9] = value.M32;
				ptr[10] = value.M33;
				ptr[11] = value.M34;
				return;
			}
			if (this.ColumnCount == 2 && this.RowCount == 2)
			{
				*ptr = value.M11;
				ptr[1] = value.M12;
				ptr[4] = value.M21;
				ptr[5] = value.M22;
				return;
			}
			throw new NotImplementedException("Matrix Size: " + this.RowCount.ToString() + " " + this.ColumnCount.ToString());
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00027988 File Offset: 0x00025B88
		public unsafe void SetValueTranspose(Matrix[] value)
		{
			float* ptr = (float*)(void*)this.values;
			if (this.ColumnCount == 4 && this.RowCount == 4)
			{
				int i = 0;
				while (i < value.Length)
				{
					*ptr = value[i].M11;
					ptr[1] = value[i].M12;
					ptr[2] = value[i].M13;
					ptr[3] = value[i].M14;
					ptr[4] = value[i].M21;
					ptr[5] = value[i].M22;
					ptr[6] = value[i].M23;
					ptr[7] = value[i].M24;
					ptr[8] = value[i].M31;
					ptr[9] = value[i].M32;
					ptr[10] = value[i].M33;
					ptr[11] = value[i].M34;
					ptr[12] = value[i].M41;
					ptr[13] = value[i].M42;
					ptr[14] = value[i].M43;
					ptr[15] = value[i].M44;
					i++;
					ptr += 16;
				}
				return;
			}
			if (this.ColumnCount == 3 && this.RowCount == 3)
			{
				int j = 0;
				while (j < value.Length)
				{
					*ptr = value[j].M11;
					ptr[1] = value[j].M12;
					ptr[2] = value[j].M13;
					ptr[4] = value[j].M21;
					ptr[5] = value[j].M22;
					ptr[6] = value[j].M23;
					ptr[8] = value[j].M31;
					ptr[9] = value[j].M32;
					ptr[10] = value[j].M33;
					j++;
					ptr += 12;
				}
				return;
			}
			if (this.ColumnCount == 4 && this.RowCount == 3)
			{
				int k = 0;
				while (k < value.Length)
				{
					*ptr = value[k].M11;
					ptr[1] = value[k].M12;
					ptr[2] = value[k].M13;
					ptr[4] = value[k].M21;
					ptr[5] = value[k].M22;
					ptr[6] = value[k].M23;
					ptr[8] = value[k].M31;
					ptr[9] = value[k].M32;
					ptr[10] = value[k].M33;
					ptr[12] = value[k].M41;
					ptr[13] = value[k].M42;
					ptr[14] = value[k].M43;
					k++;
					ptr += 16;
				}
				return;
			}
			if (this.ColumnCount == 3 && this.RowCount == 4)
			{
				int l = 0;
				while (l < value.Length)
				{
					*ptr = value[l].M11;
					ptr[1] = value[l].M12;
					ptr[2] = value[l].M13;
					ptr[3] = value[l].M14;
					ptr[4] = value[l].M21;
					ptr[5] = value[l].M22;
					ptr[6] = value[l].M23;
					ptr[7] = value[l].M24;
					ptr[8] = value[l].M31;
					ptr[9] = value[l].M32;
					ptr[10] = value[l].M33;
					ptr[11] = value[l].M34;
					l++;
					ptr += 12;
				}
				return;
			}
			if (this.ColumnCount == 2 && this.RowCount == 2)
			{
				int m = 0;
				while (m < value.Length)
				{
					*ptr = value[m].M11;
					ptr[1] = value[m].M12;
					ptr[4] = value[m].M21;
					ptr[5] = value[m].M22;
					m++;
					ptr += 8;
				}
				return;
			}
			throw new NotImplementedException("Matrix Size: " + this.RowCount.ToString() + " " + this.ColumnCount.ToString());
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00027EC0 File Offset: 0x000260C0
		public unsafe void SetValue(Matrix value)
		{
			float* ptr = (float*)(void*)this.values;
			if (this.ColumnCount == 4 && this.RowCount == 4)
			{
				*ptr = value.M11;
				ptr[1] = value.M21;
				ptr[2] = value.M31;
				ptr[3] = value.M41;
				ptr[4] = value.M12;
				ptr[5] = value.M22;
				ptr[6] = value.M32;
				ptr[7] = value.M42;
				ptr[8] = value.M13;
				ptr[9] = value.M23;
				ptr[10] = value.M33;
				ptr[11] = value.M43;
				ptr[12] = value.M14;
				ptr[13] = value.M24;
				ptr[14] = value.M34;
				ptr[15] = value.M44;
				return;
			}
			if (this.ColumnCount == 3 && this.RowCount == 3)
			{
				*ptr = value.M11;
				ptr[1] = value.M21;
				ptr[2] = value.M31;
				ptr[4] = value.M12;
				ptr[5] = value.M22;
				ptr[6] = value.M32;
				ptr[8] = value.M13;
				ptr[9] = value.M23;
				ptr[10] = value.M33;
				return;
			}
			if (this.ColumnCount == 4 && this.RowCount == 3)
			{
				*ptr = value.M11;
				ptr[1] = value.M21;
				ptr[2] = value.M31;
				ptr[3] = value.M41;
				ptr[4] = value.M12;
				ptr[5] = value.M22;
				ptr[6] = value.M32;
				ptr[7] = value.M42;
				ptr[8] = value.M13;
				ptr[9] = value.M23;
				ptr[10] = value.M33;
				ptr[11] = value.M43;
				return;
			}
			if (this.ColumnCount == 3 && this.RowCount == 4)
			{
				*ptr = value.M11;
				ptr[1] = value.M21;
				ptr[2] = value.M31;
				ptr[4] = value.M12;
				ptr[5] = value.M22;
				ptr[6] = value.M32;
				ptr[8] = value.M13;
				ptr[9] = value.M23;
				ptr[10] = value.M33;
				ptr[12] = value.M14;
				ptr[13] = value.M24;
				ptr[14] = value.M34;
				return;
			}
			if (this.ColumnCount == 2 && this.RowCount == 2)
			{
				*ptr = value.M11;
				ptr[1] = value.M21;
				ptr[4] = value.M12;
				ptr[5] = value.M22;
				return;
			}
			throw new NotImplementedException("Matrix Size: " + this.RowCount.ToString() + " " + this.ColumnCount.ToString());
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00028214 File Offset: 0x00026414
		public unsafe void SetValue(Matrix[] value)
		{
			float* ptr = (float*)(void*)this.values;
			if (this.ColumnCount == 4 && this.RowCount == 4)
			{
				int i = 0;
				while (i < value.Length)
				{
					*ptr = value[i].M11;
					ptr[1] = value[i].M21;
					ptr[2] = value[i].M31;
					ptr[3] = value[i].M41;
					ptr[4] = value[i].M12;
					ptr[5] = value[i].M22;
					ptr[6] = value[i].M32;
					ptr[7] = value[i].M42;
					ptr[8] = value[i].M13;
					ptr[9] = value[i].M23;
					ptr[10] = value[i].M33;
					ptr[11] = value[i].M43;
					ptr[12] = value[i].M14;
					ptr[13] = value[i].M24;
					ptr[14] = value[i].M34;
					ptr[15] = value[i].M44;
					i++;
					ptr += 16;
				}
				return;
			}
			if (this.ColumnCount == 3 && this.RowCount == 3)
			{
				int j = 0;
				while (j < value.Length)
				{
					*ptr = value[j].M11;
					ptr[1] = value[j].M21;
					ptr[2] = value[j].M31;
					ptr[4] = value[j].M12;
					ptr[5] = value[j].M22;
					ptr[6] = value[j].M32;
					ptr[8] = value[j].M13;
					ptr[9] = value[j].M23;
					ptr[10] = value[j].M33;
					j++;
					ptr += 12;
				}
				return;
			}
			if (this.ColumnCount == 4 && this.RowCount == 3)
			{
				int k = 0;
				while (k < value.Length)
				{
					*ptr = value[k].M11;
					ptr[1] = value[k].M21;
					ptr[2] = value[k].M31;
					ptr[3] = value[k].M41;
					ptr[4] = value[k].M12;
					ptr[5] = value[k].M22;
					ptr[6] = value[k].M32;
					ptr[7] = value[k].M42;
					ptr[8] = value[k].M13;
					ptr[9] = value[k].M23;
					ptr[10] = value[k].M33;
					ptr[11] = value[k].M43;
					k++;
					ptr += 12;
				}
				return;
			}
			if (this.ColumnCount == 3 && this.RowCount == 4)
			{
				int l = 0;
				while (l < value.Length)
				{
					*ptr = value[l].M11;
					ptr[1] = value[l].M21;
					ptr[2] = value[l].M31;
					ptr[4] = value[l].M12;
					ptr[5] = value[l].M22;
					ptr[6] = value[l].M32;
					ptr[8] = value[l].M13;
					ptr[9] = value[l].M23;
					ptr[10] = value[l].M33;
					ptr[12] = value[l].M14;
					ptr[13] = value[l].M24;
					ptr[14] = value[l].M34;
					l++;
					ptr += 16;
				}
				return;
			}
			if (this.ColumnCount == 2 && this.RowCount == 2)
			{
				int m = 0;
				while (m < value.Length)
				{
					*ptr = value[m].M11;
					ptr[1] = value[m].M21;
					ptr[4] = value[m].M12;
					ptr[5] = value[m].M22;
					m++;
					ptr += 8;
				}
				return;
			}
			throw new NotImplementedException("Matrix Size: " + this.RowCount.ToString() + " " + this.ColumnCount.ToString());
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0002874C File Offset: 0x0002694C
		public unsafe void SetValue(Quaternion value)
		{
			float* ptr = (float*)(void*)this.values;
			*ptr = value.X;
			ptr[1] = value.Y;
			ptr[2] = value.Z;
			ptr[3] = value.W;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00028794 File Offset: 0x00026994
		public unsafe void SetValue(Quaternion[] value)
		{
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < value.Length)
			{
				*ptr = value[i].X;
				ptr[1] = value[i].Y;
				ptr[2] = value[i].Z;
				ptr[3] = value[i].W;
				i++;
				ptr += 4;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00028808 File Offset: 0x00026A08
		public unsafe void SetValue(float value)
		{
			float* ptr = (float*)(void*)this.values;
			*ptr = value;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00028824 File Offset: 0x00026A24
		public void SetValue(float[] value)
		{
			int i = 0;
			int num = 0;
			while (i < value.Length)
			{
				Marshal.Copy(value, i, this.values + num, this.ColumnCount);
				i += this.ColumnCount;
				num += 16;
			}
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00028864 File Offset: 0x00026A64
		public void SetValue(string value)
		{
			throw new NotImplementedException("effect->objects[?]");
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00028870 File Offset: 0x00026A70
		public void SetValue(Texture value)
		{
			this.texture = value;
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0002887C File Offset: 0x00026A7C
		public unsafe void SetValue(Vector2 value)
		{
			float* ptr = (float*)(void*)this.values;
			*ptr = value.X;
			ptr[1] = value.Y;
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000288A8 File Offset: 0x00026AA8
		public unsafe void SetValue(Vector2[] value)
		{
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < value.Length)
			{
				*ptr = value[i].X;
				ptr[1] = value[i].Y;
				i++;
				ptr += 4;
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000288F4 File Offset: 0x00026AF4
		public unsafe void SetValue(Vector3 value)
		{
			float* ptr = (float*)(void*)this.values;
			*ptr = value.X;
			ptr[1] = value.Y;
			ptr[2] = value.Z;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0002892C File Offset: 0x00026B2C
		public unsafe void SetValue(Vector3[] value)
		{
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < value.Length)
			{
				*ptr = value[i].X;
				ptr[1] = value[i].Y;
				ptr[2] = value[i].Z;
				i++;
				ptr += 4;
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0002898C File Offset: 0x00026B8C
		public unsafe void SetValue(Vector4 value)
		{
			float* ptr = (float*)(void*)this.values;
			*ptr = value.X;
			ptr[1] = value.Y;
			ptr[2] = value.Z;
			ptr[3] = value.W;
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000289D4 File Offset: 0x00026BD4
		public unsafe void SetValue(Vector4[] value)
		{
			float* ptr = (float*)(void*)this.values;
			int i = 0;
			while (i < value.Length)
			{
				*ptr = value[i].X;
				ptr[1] = value[i].Y;
				ptr[2] = value[i].Z;
				ptr[3] = value[i].W;
				i++;
				ptr += 4;
			}
		}

		// Token: 0x040007E7 RID: 2023
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040007E8 RID: 2024
		[CompilerGenerated]
		private string <Semantic>k__BackingField;

		// Token: 0x040007E9 RID: 2025
		[CompilerGenerated]
		private int <RowCount>k__BackingField;

		// Token: 0x040007EA RID: 2026
		[CompilerGenerated]
		private int <ColumnCount>k__BackingField;

		// Token: 0x040007EB RID: 2027
		[CompilerGenerated]
		private EffectParameterClass <ParameterClass>k__BackingField;

		// Token: 0x040007EC RID: 2028
		[CompilerGenerated]
		private EffectParameterType <ParameterType>k__BackingField;

		// Token: 0x040007ED RID: 2029
		[CompilerGenerated]
		private EffectAnnotationCollection <Annotations>k__BackingField;

		// Token: 0x040007EE RID: 2030
		internal Texture texture;

		// Token: 0x040007EF RID: 2031
		internal string cachedString = string.Empty;

		// Token: 0x040007F0 RID: 2032
		internal IntPtr values;

		// Token: 0x040007F1 RID: 2033
		internal uint valuesSizeBytes;

		// Token: 0x040007F2 RID: 2034
		internal IntPtr mojoType;

		// Token: 0x040007F3 RID: 2035
		internal int elementCount;

		// Token: 0x040007F4 RID: 2036
		internal EffectParameterCollection elements;

		// Token: 0x040007F5 RID: 2037
		internal EffectParameterCollection members;

		// Token: 0x040007F6 RID: 2038
		private Effect outer;
	}
}
