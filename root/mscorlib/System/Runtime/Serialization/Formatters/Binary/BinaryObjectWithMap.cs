using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000670 RID: 1648
	internal sealed class BinaryObjectWithMap : IStreamable
	{
		// Token: 0x06003DE8 RID: 15848 RVA: 0x000025BE File Offset: 0x000007BE
		internal BinaryObjectWithMap()
		{
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x000D6AD5 File Offset: 0x000D4CD5
		internal BinaryObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000D6AE4 File Offset: 0x000D4CE4
		internal void Set(int objectId, string name, int numMembers, string[] memberNames, int assemId)
		{
			this.objectId = objectId;
			this.name = name;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.assemId = assemId;
			if (assemId > 0)
			{
				this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapAssemId;
				return;
			}
			this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMap;
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000D6B20 File Offset: 0x000D4D20
		public void Write(__BinaryWriter sout)
		{
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.objectId);
			sout.WriteString(this.name);
			sout.WriteInt32(this.numMembers);
			for (int i = 0; i < this.numMembers; i++)
			{
				sout.WriteString(this.memberNames[i]);
			}
			if (this.assemId > 0)
			{
				sout.WriteInt32(this.assemId);
			}
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000D6B94 File Offset: 0x000D4D94
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			this.objectId = input.ReadInt32();
			this.name = input.ReadString();
			this.numMembers = input.ReadInt32();
			this.memberNames = new string[this.numMembers];
			for (int i = 0; i < this.numMembers; i++)
			{
				this.memberNames[i] = input.ReadString();
			}
			if (this.binaryHeaderEnum == BinaryHeaderEnum.ObjectWithMapAssemId)
			{
				this.assemId = input.ReadInt32();
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000D6C0C File Offset: 0x000D4E0C
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				for (int i = 0; i < this.numMembers; i++)
				{
				}
				BinaryHeaderEnum binaryHeaderEnum = this.binaryHeaderEnum;
			}
		}

		// Token: 0x040027E7 RID: 10215
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x040027E8 RID: 10216
		internal int objectId;

		// Token: 0x040027E9 RID: 10217
		internal string name;

		// Token: 0x040027EA RID: 10218
		internal int numMembers;

		// Token: 0x040027EB RID: 10219
		internal string[] memberNames;

		// Token: 0x040027EC RID: 10220
		internal int assemId;
	}
}
