using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200073D RID: 1853
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	[Guid("b36b5c63-42ef-38bc-a07e-0b34c98f164a")]
	[CLSCompliant(false)]
	[ComVisible(true)]
	public interface _Exception
	{
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060042C3 RID: 17091
		// (set) Token: 0x060042C4 RID: 17092
		string HelpLink { get; set; }

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060042C5 RID: 17093
		Exception InnerException { get; }

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060042C6 RID: 17094
		string Message { get; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060042C7 RID: 17095
		// (set) Token: 0x060042C8 RID: 17096
		string Source { get; set; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060042C9 RID: 17097
		string StackTrace { get; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060042CA RID: 17098
		MethodBase TargetSite { get; }

		// Token: 0x060042CB RID: 17099
		bool Equals(object obj);

		// Token: 0x060042CC RID: 17100
		Exception GetBaseException();

		// Token: 0x060042CD RID: 17101
		int GetHashCode();

		// Token: 0x060042CE RID: 17102
		void GetObjectData(SerializationInfo info, StreamingContext context);

		// Token: 0x060042CF RID: 17103
		Type GetType();

		// Token: 0x060042D0 RID: 17104
		string ToString();
	}
}
