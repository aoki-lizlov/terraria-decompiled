using System;

namespace rail
{
	// Token: 0x02000143 RID: 323
	public interface IRailFile : IRailComponent
	{
		// Token: 0x060017FD RID: 6141
		string GetFilename();

		// Token: 0x060017FE RID: 6142
		uint Read(byte[] buff, uint bytes_to_read, out RailResult result);

		// Token: 0x060017FF RID: 6143
		uint Read(byte[] buff, uint bytes_to_read);

		// Token: 0x06001800 RID: 6144
		uint Write(byte[] buff, uint bytes_to_write, out RailResult result);

		// Token: 0x06001801 RID: 6145
		uint Write(byte[] buff, uint bytes_to_write);

		// Token: 0x06001802 RID: 6146
		RailResult AsyncRead(uint bytes_to_read, string user_data);

		// Token: 0x06001803 RID: 6147
		RailResult AsyncWrite(byte[] buffer, uint bytes_to_write, string user_data);

		// Token: 0x06001804 RID: 6148
		uint GetSize();

		// Token: 0x06001805 RID: 6149
		void Close();
	}
}
