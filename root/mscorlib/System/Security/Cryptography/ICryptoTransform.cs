using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x02000468 RID: 1128
	[ComVisible(true)]
	public interface ICryptoTransform : IDisposable
	{
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002EC0 RID: 11968
		int InputBlockSize { get; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002EC1 RID: 11969
		int OutputBlockSize { get; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06002EC2 RID: 11970
		bool CanTransformMultipleBlocks { get; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06002EC3 RID: 11971
		bool CanReuseTransform { get; }

		// Token: 0x06002EC4 RID: 11972
		int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset);

		// Token: 0x06002EC5 RID: 11973
		byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount);
	}
}
