using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000666 RID: 1638
	internal sealed class SerializationHeaderRecord : IStreamable
	{
		// Token: 0x06003DAB RID: 15787 RVA: 0x000D5C37 File Offset: 0x000D3E37
		internal SerializationHeaderRecord()
		{
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000D5C46 File Offset: 0x000D3E46
		internal SerializationHeaderRecord(BinaryHeaderEnum binaryHeaderEnum, int topId, int headerId, int majorVersion, int minorVersion)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
			this.topId = topId;
			this.headerId = headerId;
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000D5C7C File Offset: 0x000D3E7C
		public void Write(__BinaryWriter sout)
		{
			this.majorVersion = this.binaryFormatterMajorVersion;
			this.minorVersion = this.binaryFormatterMinorVersion;
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.topId);
			sout.WriteInt32(this.headerId);
			sout.WriteInt32(this.binaryFormatterMajorVersion);
			sout.WriteInt32(this.binaryFormatterMinorVersion);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000BEDB0 File Offset: 0x000BCFB0
		private static int GetInt32(byte[] buffer, int index)
		{
			return (int)buffer[index] | ((int)buffer[index + 1] << 8) | ((int)buffer[index + 2] << 16) | ((int)buffer[index + 3] << 24);
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000D5CE0 File Offset: 0x000D3EE0
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			byte[] array = input.ReadBytes(17);
			if (array.Length < 17)
			{
				__Error.EndOfFile();
			}
			this.majorVersion = SerializationHeaderRecord.GetInt32(array, 9);
			if (this.majorVersion > this.binaryFormatterMajorVersion)
			{
				throw new SerializationException(Environment.GetResourceString("The input stream is not a valid binary format. The starting contents (in bytes) are: {0} ...", new object[] { BitConverter.ToString(array) }));
			}
			this.binaryHeaderEnum = (BinaryHeaderEnum)array[0];
			this.topId = SerializationHeaderRecord.GetInt32(array, 1);
			this.headerId = SerializationHeaderRecord.GetInt32(array, 5);
			this.minorVersion = SerializationHeaderRecord.GetInt32(array, 13);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x00004088 File Offset: 0x00002288
		public void Dump()
		{
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000D5BA5 File Offset: 0x000D3DA5
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x040027BA RID: 10170
		internal int binaryFormatterMajorVersion = 1;

		// Token: 0x040027BB RID: 10171
		internal int binaryFormatterMinorVersion;

		// Token: 0x040027BC RID: 10172
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x040027BD RID: 10173
		internal int topId;

		// Token: 0x040027BE RID: 10174
		internal int headerId;

		// Token: 0x040027BF RID: 10175
		internal int majorVersion;

		// Token: 0x040027C0 RID: 10176
		internal int minorVersion;
	}
}
