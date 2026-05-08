using System;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005DD RID: 1501
	internal class CADObjRef
	{
		// Token: 0x06003A2A RID: 14890 RVA: 0x000CCBD5 File Offset: 0x000CADD5
		public CADObjRef(ObjRef o, int sourceDomain)
		{
			this.objref = o;
			this.TypeInfo = o.SerializeType();
			this.SourceDomain = sourceDomain;
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06003A2B RID: 14891 RVA: 0x000CCBF7 File Offset: 0x000CADF7
		public string TypeName
		{
			get
			{
				return this.objref.TypeInfo.TypeName;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000CCC09 File Offset: 0x000CAE09
		public string URI
		{
			get
			{
				return this.objref.URI;
			}
		}

		// Token: 0x040025F4 RID: 9716
		internal ObjRef objref;

		// Token: 0x040025F5 RID: 9717
		internal int SourceDomain;

		// Token: 0x040025F6 RID: 9718
		internal byte[] TypeInfo;
	}
}
