using System;
using System.IO;

namespace Terraria.DataStructures
{
	// Token: 0x0200058E RID: 1422
	public class CachedBuffer
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x0600383E RID: 14398 RVA: 0x00631704 File Offset: 0x0062F904
		public int Length
		{
			get
			{
				return this.Data.Length;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x0063170E File Offset: 0x0062F90E
		public bool IsActive
		{
			get
			{
				return this._isActive;
			}
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x00631718 File Offset: 0x0062F918
		public CachedBuffer(byte[] data)
		{
			this.Data = data;
			this._memoryStream = new MemoryStream(data);
			this.Writer = new BinaryWriter(this._memoryStream);
			this.Reader = new BinaryReader(this._memoryStream);
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x00631767 File Offset: 0x0062F967
		internal CachedBuffer Activate()
		{
			this._isActive = true;
			this._memoryStream.Position = 0L;
			return this;
		}

		// Token: 0x06003842 RID: 14402 RVA: 0x0063177E File Offset: 0x0062F97E
		public void Recycle()
		{
			if (this._isActive)
			{
				this._isActive = false;
				BufferPool.Recycle(this);
			}
		}

		// Token: 0x04005C66 RID: 23654
		public readonly byte[] Data;

		// Token: 0x04005C67 RID: 23655
		public readonly BinaryWriter Writer;

		// Token: 0x04005C68 RID: 23656
		public readonly BinaryReader Reader;

		// Token: 0x04005C69 RID: 23657
		private readonly MemoryStream _memoryStream;

		// Token: 0x04005C6A RID: 23658
		private bool _isActive = true;
	}
}
