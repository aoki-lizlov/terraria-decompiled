using System;

namespace System.Reflection.Emit
{
	// Token: 0x020008FA RID: 2298
	internal struct ILExceptionInfo
	{
		// Token: 0x06004FF4 RID: 20468 RVA: 0x000FAB85 File Offset: 0x000F8D85
		internal int NumHandlers()
		{
			return this.handlers.Length;
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x000FAB90 File Offset: 0x000F8D90
		internal void AddCatch(Type extype, int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 0;
			this.handlers[num].start = offset;
			this.handlers[num].extype = extype;
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x000FABEC File Offset: 0x000F8DEC
		internal void AddFinally(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 2;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x000FAC48 File Offset: 0x000F8E48
		internal void AddFault(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = 4;
			this.handlers[num].start = offset;
			this.handlers[num].extype = null;
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x000FACA4 File Offset: 0x000F8EA4
		internal void AddFilter(int offset)
		{
			this.End(offset);
			this.add_block(offset);
			int num = this.handlers.Length - 1;
			this.handlers[num].type = -1;
			this.handlers[num].extype = null;
			this.handlers[num].filter_offset = offset;
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x000FAD00 File Offset: 0x000F8F00
		internal void End(int offset)
		{
			if (this.handlers == null)
			{
				return;
			}
			int num = this.handlers.Length - 1;
			if (num >= 0)
			{
				this.handlers[num].len = offset - this.handlers[num].start;
			}
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x000FAD49 File Offset: 0x000F8F49
		internal int LastClauseType()
		{
			if (this.handlers != null)
			{
				return this.handlers[this.handlers.Length - 1].type;
			}
			return 0;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x000FAD70 File Offset: 0x000F8F70
		internal void PatchFilterClause(int start)
		{
			if (this.handlers != null && this.handlers.Length != 0)
			{
				this.handlers[this.handlers.Length - 1].start = start;
				this.handlers[this.handlers.Length - 1].type = 1;
			}
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x00004088 File Offset: 0x00002288
		internal void Debug(int b)
		{
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x000FADC4 File Offset: 0x000F8FC4
		private void add_block(int offset)
		{
			if (this.handlers != null)
			{
				int num = this.handlers.Length;
				ILExceptionBlock[] array = new ILExceptionBlock[num + 1];
				Array.Copy(this.handlers, array, num);
				this.handlers = array;
				this.handlers[num].len = offset - this.handlers[num].start;
				return;
			}
			this.handlers = new ILExceptionBlock[1];
			this.len = offset - this.start;
		}

		// Token: 0x04003108 RID: 12552
		internal ILExceptionBlock[] handlers;

		// Token: 0x04003109 RID: 12553
		internal int start;

		// Token: 0x0400310A RID: 12554
		internal int len;

		// Token: 0x0400310B RID: 12555
		internal Label end;
	}
}
