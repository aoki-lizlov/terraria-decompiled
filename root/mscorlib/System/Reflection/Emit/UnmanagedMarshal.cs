using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200091A RID: 2330
	[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UnmanagedMarshal
	{
		// Token: 0x060052B6 RID: 21174 RVA: 0x001056BA File Offset: 0x001038BA
		private UnmanagedMarshal(UnmanagedType maint, int cnt)
		{
			this.count = cnt;
			this.t = maint;
			this.tbase = maint;
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x001056D7 File Offset: 0x001038D7
		private UnmanagedMarshal(UnmanagedType maint, UnmanagedType elemt)
		{
			this.count = 0;
			this.t = maint;
			this.tbase = elemt;
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x001056F4 File Offset: 0x001038F4
		public UnmanagedType BaseType
		{
			get
			{
				if (this.t == UnmanagedType.LPArray)
				{
					throw new ArgumentException();
				}
				if (this.t == UnmanagedType.SafeArray)
				{
					throw new ArgumentException();
				}
				return this.tbase;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x060052B9 RID: 21177 RVA: 0x0010571C File Offset: 0x0010391C
		public int ElementCount
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x060052BA RID: 21178 RVA: 0x00105724 File Offset: 0x00103924
		public UnmanagedType GetUnmanagedType
		{
			get
			{
				return this.t;
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x060052BB RID: 21179 RVA: 0x0010572C File Offset: 0x0010392C
		public Guid IIDGuid
		{
			get
			{
				return new Guid(this.guid);
			}
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x00105739 File Offset: 0x00103939
		public static UnmanagedMarshal DefineByValArray(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValArray, elemCount);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x00105743 File Offset: 0x00103943
		public static UnmanagedMarshal DefineByValTStr(int elemCount)
		{
			return new UnmanagedMarshal(UnmanagedType.ByValTStr, elemCount);
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x0010574D File Offset: 0x0010394D
		public static UnmanagedMarshal DefineLPArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, elemType);
		}

		// Token: 0x060052BF RID: 21183 RVA: 0x00105757 File Offset: 0x00103957
		public static UnmanagedMarshal DefineSafeArray(UnmanagedType elemType)
		{
			return new UnmanagedMarshal(UnmanagedType.SafeArray, elemType);
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x00105761 File Offset: 0x00103961
		public static UnmanagedMarshal DefineUnmanagedMarshal(UnmanagedType unmanagedType)
		{
			return new UnmanagedMarshal(unmanagedType, unmanagedType);
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x0010576C File Offset: 0x0010396C
		internal static UnmanagedMarshal DefineCustom(Type typeref, string cookie, string mtype, Guid id)
		{
			UnmanagedMarshal unmanagedMarshal = new UnmanagedMarshal(UnmanagedType.CustomMarshaler, UnmanagedType.CustomMarshaler);
			unmanagedMarshal.mcookie = cookie;
			unmanagedMarshal.marshaltype = mtype;
			unmanagedMarshal.marshaltyperef = typeref;
			if (id == Guid.Empty)
			{
				unmanagedMarshal.guid = string.Empty;
			}
			else
			{
				unmanagedMarshal.guid = id.ToString();
			}
			return unmanagedMarshal;
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x001057C6 File Offset: 0x001039C6
		internal static UnmanagedMarshal DefineLPArrayInternal(UnmanagedType elemType, int sizeConst, int sizeParamIndex)
		{
			return new UnmanagedMarshal(UnmanagedType.LPArray, elemType)
			{
				count = sizeConst,
				param_num = sizeParamIndex,
				has_size = true
			};
		}

		// Token: 0x040032BE RID: 12990
		private int count;

		// Token: 0x040032BF RID: 12991
		private UnmanagedType t;

		// Token: 0x040032C0 RID: 12992
		private UnmanagedType tbase;

		// Token: 0x040032C1 RID: 12993
		private string guid;

		// Token: 0x040032C2 RID: 12994
		private string mcookie;

		// Token: 0x040032C3 RID: 12995
		private string marshaltype;

		// Token: 0x040032C4 RID: 12996
		internal Type marshaltyperef;

		// Token: 0x040032C5 RID: 12997
		private int param_num;

		// Token: 0x040032C6 RID: 12998
		private bool has_size;
	}
}
