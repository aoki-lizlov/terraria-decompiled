using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x02000A19 RID: 2585
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with MS.NET")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class StackFrame
	{
		// Token: 0x06005FDD RID: 24541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_frame_info(int skip, bool needFileInfo, out MethodBase method, out int iloffset, out int native_offset, out string file, out int line, out int column);

		// Token: 0x06005FDE RID: 24542 RVA: 0x0014C2F0 File Offset: 0x0014A4F0
		public StackFrame()
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x0014C340 File Offset: 0x0014A540
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x0014C390 File Offset: 0x0014A590
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		// Token: 0x06005FE1 RID: 24545 RVA: 0x0014C3E0 File Offset: 0x0014A5E0
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames, bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		// Token: 0x06005FE2 RID: 24546 RVA: 0x0014C430 File Offset: 0x0014A630
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(string fileName, int lineNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = 0;
		}

		// Token: 0x06005FE3 RID: 24547 RVA: 0x0014C48C File Offset: 0x0014A68C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(string fileName, int lineNumber, int colNumber)
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out fileName, out lineNumber, out this.columnNumber);
			this.fileName = fileName;
			this.lineNumber = lineNumber;
			this.columnNumber = colNumber;
		}

		// Token: 0x06005FE4 RID: 24548 RVA: 0x0014C4E6 File Offset: 0x0014A6E6
		public virtual int GetFileLineNumber()
		{
			return this.lineNumber;
		}

		// Token: 0x06005FE5 RID: 24549 RVA: 0x0014C4EE File Offset: 0x0014A6EE
		public virtual int GetFileColumnNumber()
		{
			return this.columnNumber;
		}

		// Token: 0x06005FE6 RID: 24550 RVA: 0x0014C4F6 File Offset: 0x0014A6F6
		public virtual string GetFileName()
		{
			return this.fileName;
		}

		// Token: 0x06005FE7 RID: 24551 RVA: 0x0014C500 File Offset: 0x0014A700
		internal string GetSecureFileName()
		{
			string text = "<filename unknown>";
			if (this.fileName == null)
			{
				return text;
			}
			try
			{
				text = this.GetFileName();
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		// Token: 0x06005FE8 RID: 24552 RVA: 0x0014C53C File Offset: 0x0014A73C
		public virtual int GetILOffset()
		{
			return this.ilOffset;
		}

		// Token: 0x06005FE9 RID: 24553 RVA: 0x0014C544 File Offset: 0x0014A744
		public virtual MethodBase GetMethod()
		{
			return this.methodBase;
		}

		// Token: 0x06005FEA RID: 24554 RVA: 0x0014C54C File Offset: 0x0014A74C
		public virtual int GetNativeOffset()
		{
			return this.nativeOffset;
		}

		// Token: 0x06005FEB RID: 24555 RVA: 0x0014C554 File Offset: 0x0014A754
		internal long GetMethodAddress()
		{
			return this.methodAddress;
		}

		// Token: 0x06005FEC RID: 24556 RVA: 0x0014C55C File Offset: 0x0014A75C
		internal uint GetMethodIndex()
		{
			return this.methodIndex;
		}

		// Token: 0x06005FED RID: 24557 RVA: 0x0014C564 File Offset: 0x0014A764
		internal string GetInternalMethodName()
		{
			return this.internalMethodName;
		}

		// Token: 0x06005FEE RID: 24558 RVA: 0x0014C56C File Offset: 0x0014A76C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.methodBase == null)
			{
				stringBuilder.Append(Locale.GetText("<unknown method>"));
			}
			else
			{
				stringBuilder.Append(this.methodBase.Name);
			}
			stringBuilder.Append(Locale.GetText(" at "));
			if (this.ilOffset == -1)
			{
				stringBuilder.Append(Locale.GetText("<unknown offset>"));
			}
			else
			{
				stringBuilder.Append(Locale.GetText("offset "));
				stringBuilder.Append(this.ilOffset);
			}
			stringBuilder.Append(Locale.GetText(" in file:line:column "));
			stringBuilder.Append(this.GetSecureFileName());
			stringBuilder.AppendFormat(":{0}:{1}", this.lineNumber, this.columnNumber);
			return stringBuilder.ToString();
		}

		// Token: 0x040039BB RID: 14779
		public const int OFFSET_UNKNOWN = -1;

		// Token: 0x040039BC RID: 14780
		private int ilOffset = -1;

		// Token: 0x040039BD RID: 14781
		private int nativeOffset = -1;

		// Token: 0x040039BE RID: 14782
		private long methodAddress;

		// Token: 0x040039BF RID: 14783
		private uint methodIndex;

		// Token: 0x040039C0 RID: 14784
		private MethodBase methodBase;

		// Token: 0x040039C1 RID: 14785
		private string fileName;

		// Token: 0x040039C2 RID: 14786
		private int lineNumber;

		// Token: 0x040039C3 RID: 14787
		private int columnNumber;

		// Token: 0x040039C4 RID: 14788
		private string internalMethodName;
	}
}
