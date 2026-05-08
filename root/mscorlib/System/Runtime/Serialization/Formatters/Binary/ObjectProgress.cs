using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000678 RID: 1656
	internal sealed class ObjectProgress
	{
		// Token: 0x06003E18 RID: 15896 RVA: 0x000D7678 File Offset: 0x000D5878
		internal ObjectProgress()
		{
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000D7694 File Offset: 0x000D5894
		[Conditional("SER_LOGGING")]
		private void Counter()
		{
			lock (this)
			{
				this.opRecordId = ObjectProgress.opRecordIdCount++;
				if (ObjectProgress.opRecordIdCount > 1000)
				{
					ObjectProgress.opRecordIdCount = 1;
				}
			}
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x000D76F0 File Offset: 0x000D58F0
		internal void Init()
		{
			this.isInitial = false;
			this.count = 0;
			this.expectedType = BinaryTypeEnum.ObjectUrt;
			this.expectedTypeInformation = null;
			this.name = null;
			this.objectTypeEnum = InternalObjectTypeE.Empty;
			this.memberTypeEnum = InternalMemberTypeE.Empty;
			this.memberValueEnum = InternalMemberValueE.Empty;
			this.dtType = null;
			this.numItems = 0;
			this.nullCount = 0;
			this.typeInformation = null;
			this.memberLength = 0;
			this.binaryTypeEnumA = null;
			this.typeInformationA = null;
			this.memberNames = null;
			this.memberTypes = null;
			this.pr.Init();
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x000D777F File Offset: 0x000D597F
		internal void ArrayCountIncrement(int value)
		{
			this.count += value;
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x000D7790 File Offset: 0x000D5990
		internal bool GetNext(out BinaryTypeEnum outBinaryTypeEnum, out object outTypeInformation)
		{
			outBinaryTypeEnum = BinaryTypeEnum.Primitive;
			outTypeInformation = null;
			if (this.objectTypeEnum == InternalObjectTypeE.Array)
			{
				if (this.count == this.numItems)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnum;
				outTypeInformation = this.typeInformation;
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.count++;
				return true;
			}
			else
			{
				if (this.count == this.memberLength && !this.isInitial)
				{
					return false;
				}
				outBinaryTypeEnum = this.binaryTypeEnumA[this.count];
				outTypeInformation = this.typeInformationA[this.count];
				if (this.count == 0)
				{
					this.isInitial = false;
				}
				this.name = this.memberNames[this.count];
				Type[] array = this.memberTypes;
				this.dtType = this.memberTypes[this.count];
				this.count++;
				return true;
			}
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x000D786C File Offset: 0x000D5A6C
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectProgress()
		{
		}

		// Token: 0x0400280E RID: 10254
		internal static int opRecordIdCount = 1;

		// Token: 0x0400280F RID: 10255
		internal int opRecordId;

		// Token: 0x04002810 RID: 10256
		internal bool isInitial;

		// Token: 0x04002811 RID: 10257
		internal int count;

		// Token: 0x04002812 RID: 10258
		internal BinaryTypeEnum expectedType = BinaryTypeEnum.ObjectUrt;

		// Token: 0x04002813 RID: 10259
		internal object expectedTypeInformation;

		// Token: 0x04002814 RID: 10260
		internal string name;

		// Token: 0x04002815 RID: 10261
		internal InternalObjectTypeE objectTypeEnum;

		// Token: 0x04002816 RID: 10262
		internal InternalMemberTypeE memberTypeEnum;

		// Token: 0x04002817 RID: 10263
		internal InternalMemberValueE memberValueEnum;

		// Token: 0x04002818 RID: 10264
		internal Type dtType;

		// Token: 0x04002819 RID: 10265
		internal int numItems;

		// Token: 0x0400281A RID: 10266
		internal BinaryTypeEnum binaryTypeEnum;

		// Token: 0x0400281B RID: 10267
		internal object typeInformation;

		// Token: 0x0400281C RID: 10268
		internal int nullCount;

		// Token: 0x0400281D RID: 10269
		internal int memberLength;

		// Token: 0x0400281E RID: 10270
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x0400281F RID: 10271
		internal object[] typeInformationA;

		// Token: 0x04002820 RID: 10272
		internal string[] memberNames;

		// Token: 0x04002821 RID: 10273
		internal Type[] memberTypes;

		// Token: 0x04002822 RID: 10274
		internal ParseRecord pr = new ParseRecord();
	}
}
