using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000690 RID: 1680
	internal sealed class NameInfo
	{
		// Token: 0x06003F5E RID: 16222 RVA: 0x000025BE File Offset: 0x000007BE
		internal NameInfo()
		{
		}

		// Token: 0x06003F5F RID: 16223 RVA: 0x000DF48C File Offset: 0x000DD68C
		internal void Init()
		{
			this.NIFullName = null;
			this.NIobjectId = 0L;
			this.NIassemId = 0L;
			this.NIprimitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
			this.NItype = null;
			this.NIisSealed = false;
			this.NItransmitTypeOnObject = false;
			this.NItransmitTypeOnMember = false;
			this.NIisParentTypeOnObject = false;
			this.NIisArray = false;
			this.NIisArrayItem = false;
			this.NIarrayEnum = InternalArrayTypeE.Empty;
			this.NIsealedStatusChecked = false;
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06003F60 RID: 16224 RVA: 0x000DF4F6 File Offset: 0x000DD6F6
		public bool IsSealed
		{
			get
			{
				if (!this.NIsealedStatusChecked)
				{
					this.NIisSealed = this.NItype.IsSealed;
					this.NIsealedStatusChecked = true;
				}
				return this.NIisSealed;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x000DF51E File Offset: 0x000DD71E
		// (set) Token: 0x06003F62 RID: 16226 RVA: 0x000DF53F File Offset: 0x000DD73F
		public string NIname
		{
			get
			{
				if (this.NIFullName == null)
				{
					this.NIFullName = this.NItype.FullName;
				}
				return this.NIFullName;
			}
			set
			{
				this.NIFullName = value;
			}
		}

		// Token: 0x04002943 RID: 10563
		internal string NIFullName;

		// Token: 0x04002944 RID: 10564
		internal long NIobjectId;

		// Token: 0x04002945 RID: 10565
		internal long NIassemId;

		// Token: 0x04002946 RID: 10566
		internal InternalPrimitiveTypeE NIprimitiveTypeEnum;

		// Token: 0x04002947 RID: 10567
		internal Type NItype;

		// Token: 0x04002948 RID: 10568
		internal bool NIisSealed;

		// Token: 0x04002949 RID: 10569
		internal bool NIisArray;

		// Token: 0x0400294A RID: 10570
		internal bool NIisArrayItem;

		// Token: 0x0400294B RID: 10571
		internal bool NItransmitTypeOnObject;

		// Token: 0x0400294C RID: 10572
		internal bool NItransmitTypeOnMember;

		// Token: 0x0400294D RID: 10573
		internal bool NIisParentTypeOnObject;

		// Token: 0x0400294E RID: 10574
		internal InternalArrayTypeE NIarrayEnum;

		// Token: 0x0400294F RID: 10575
		private bool NIsealedStatusChecked;
	}
}
