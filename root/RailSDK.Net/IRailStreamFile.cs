using System;

namespace rail
{
	// Token: 0x02000145 RID: 325
	public interface IRailStreamFile : IRailComponent
	{
		// Token: 0x0600181D RID: 6173
		string GetFilename();

		// Token: 0x0600181E RID: 6174
		RailResult AsyncRead(int offset, uint bytes_to_read, string user_data);

		// Token: 0x0600181F RID: 6175
		RailResult AsyncWrite(byte[] buff, uint bytes_to_write, string user_data);

		// Token: 0x06001820 RID: 6176
		ulong GetSize();

		// Token: 0x06001821 RID: 6177
		RailResult Close();

		// Token: 0x06001822 RID: 6178
		void Cancel();
	}
}
