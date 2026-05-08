using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework.Graphics
{
	// Token: 0x02000081 RID: 129
	public sealed class EffectAnnotation
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x000268C3 File Offset: 0x00024AC3
		// (set) Token: 0x0600113D RID: 4413 RVA: 0x000268CB File Offset: 0x00024ACB
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

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600113E RID: 4414 RVA: 0x000268D4 File Offset: 0x00024AD4
		// (set) Token: 0x0600113F RID: 4415 RVA: 0x000268DC File Offset: 0x00024ADC
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

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001140 RID: 4416 RVA: 0x000268E5 File Offset: 0x00024AE5
		// (set) Token: 0x06001141 RID: 4417 RVA: 0x000268ED File Offset: 0x00024AED
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

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x000268F6 File Offset: 0x00024AF6
		// (set) Token: 0x06001143 RID: 4419 RVA: 0x000268FE File Offset: 0x00024AFE
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

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00026907 File Offset: 0x00024B07
		// (set) Token: 0x06001145 RID: 4421 RVA: 0x0002690F File Offset: 0x00024B0F
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

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00026918 File Offset: 0x00024B18
		// (set) Token: 0x06001147 RID: 4423 RVA: 0x00026920 File Offset: 0x00024B20
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

		// Token: 0x06001148 RID: 4424 RVA: 0x0002692C File Offset: 0x00024B2C
		internal EffectAnnotation(string name, string semantic, int rowCount, int columnCount, EffectParameterClass parameterClass, EffectParameterType parameterType, IntPtr data)
		{
			this.Name = name;
			this.Semantic = semantic ?? string.Empty;
			this.RowCount = rowCount;
			this.ColumnCount = columnCount;
			this.ParameterClass = parameterClass;
			this.ParameterType = parameterType;
			this.values = data;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00026988 File Offset: 0x00024B88
		public unsafe bool GetValueBoolean()
		{
			int* ptr = (int*)(void*)this.values;
			return *ptr != 0;
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000269A8 File Offset: 0x00024BA8
		public unsafe int GetValueInt32()
		{
			int* ptr = (int*)(void*)this.values;
			return *ptr;
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000269C4 File Offset: 0x00024BC4
		public unsafe Matrix GetValueMatrix()
		{
			float* ptr = (float*)(void*)this.values;
			return new Matrix(*ptr, ptr[4], ptr[8], ptr[12], ptr[1], ptr[5], ptr[9], ptr[13], ptr[2], ptr[6], ptr[10], ptr[14], ptr[3], ptr[7], ptr[11], ptr[15]);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00026A54 File Offset: 0x00024C54
		public unsafe float GetValueSingle()
		{
			float* ptr = (float*)(void*)this.values;
			return *ptr;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00026A6F File Offset: 0x00024C6F
		public string GetValueString()
		{
			return this.cachedString;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00026A78 File Offset: 0x00024C78
		public unsafe Vector2 GetValueVector2()
		{
			float* ptr = (float*)(void*)this.values;
			return new Vector2(*ptr, ptr[1]);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x00026A9C File Offset: 0x00024C9C
		public unsafe Vector3 GetValueVector3()
		{
			float* ptr = (float*)(void*)this.values;
			return new Vector3(*ptr, ptr[1], ptr[2]);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00026AC8 File Offset: 0x00024CC8
		public unsafe Vector4 GetValueVector4()
		{
			float* ptr = (float*)(void*)this.values;
			return new Vector4(*ptr, ptr[1], ptr[2], ptr[3]);
		}

		// Token: 0x040007DD RID: 2013
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x040007DE RID: 2014
		[CompilerGenerated]
		private string <Semantic>k__BackingField;

		// Token: 0x040007DF RID: 2015
		[CompilerGenerated]
		private int <RowCount>k__BackingField;

		// Token: 0x040007E0 RID: 2016
		[CompilerGenerated]
		private int <ColumnCount>k__BackingField;

		// Token: 0x040007E1 RID: 2017
		[CompilerGenerated]
		private EffectParameterClass <ParameterClass>k__BackingField;

		// Token: 0x040007E2 RID: 2018
		[CompilerGenerated]
		private EffectParameterType <ParameterType>k__BackingField;

		// Token: 0x040007E3 RID: 2019
		internal string cachedString = string.Empty;

		// Token: 0x040007E4 RID: 2020
		private IntPtr values;
	}
}
