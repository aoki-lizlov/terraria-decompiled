using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000903 RID: 2307
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_LocalBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class LocalBuilder : LocalVariableInfo, _LocalBuilder
	{
		// Token: 0x06005053 RID: 20563 RVA: 0x000174FB File Offset: 0x000156FB
		void _LocalBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x000174FB File Offset: 0x000156FB
		void _LocalBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x000174FB File Offset: 0x000156FB
		void _LocalBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x000174FB File Offset: 0x000156FB
		void _LocalBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005057 RID: 20567 RVA: 0x000FD200 File Offset: 0x000FB400
		internal LocalBuilder(Type t, ILGenerator ilgen)
		{
			this.type = t;
			this.ilgen = ilgen;
		}

		// Token: 0x06005058 RID: 20568 RVA: 0x000FD216 File Offset: 0x000FB416
		public void SetLocalSymInfo(string name, int startOffset, int endOffset)
		{
			this.name = name;
			this.startOffset = startOffset;
			this.endOffset = endOffset;
		}

		// Token: 0x06005059 RID: 20569 RVA: 0x000FD22D File Offset: 0x000FB42D
		public void SetLocalSymInfo(string name)
		{
			this.SetLocalSymInfo(name, 0, 0);
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600505A RID: 20570 RVA: 0x000F2B35 File Offset: 0x000F0D35
		public override Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x0600505B RID: 20571 RVA: 0x000F2B25 File Offset: 0x000F0D25
		public override bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x0600505C RID: 20572 RVA: 0x000F2B2D File Offset: 0x000F0D2D
		public override int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x0600505D RID: 20573 RVA: 0x000FD238 File Offset: 0x000FB438
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600505E RID: 20574 RVA: 0x000FD240 File Offset: 0x000FB440
		internal int StartOffset
		{
			get
			{
				return this.startOffset;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600505F RID: 20575 RVA: 0x000FD248 File Offset: 0x000FB448
		internal int EndOffset
		{
			get
			{
				return this.endOffset;
			}
		}

		// Token: 0x04003132 RID: 12594
		private string name;

		// Token: 0x04003133 RID: 12595
		internal ILGenerator ilgen;

		// Token: 0x04003134 RID: 12596
		private int startOffset;

		// Token: 0x04003135 RID: 12597
		private int endOffset;
	}
}
