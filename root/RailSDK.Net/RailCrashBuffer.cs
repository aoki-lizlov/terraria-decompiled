using System;

namespace rail
{
	// Token: 0x02000191 RID: 401
	public interface RailCrashBuffer
	{
		// Token: 0x060018B7 RID: 6327
		string GetData();

		// Token: 0x060018B8 RID: 6328
		uint GetBufferLength();

		// Token: 0x060018B9 RID: 6329
		uint GetValidLength();

		// Token: 0x060018BA RID: 6330
		uint SetData(string data, uint length, uint offset);

		// Token: 0x060018BB RID: 6331
		uint SetData(string data, uint length);

		// Token: 0x060018BC RID: 6332
		uint AppendData(string data, uint length);
	}
}
