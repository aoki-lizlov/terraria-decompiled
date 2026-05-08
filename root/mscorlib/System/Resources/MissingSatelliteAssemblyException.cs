using System;
using System.Runtime.Serialization;

namespace System.Resources
{
	// Token: 0x0200082B RID: 2091
	[Serializable]
	public class MissingSatelliteAssemblyException : SystemException
	{
		// Token: 0x060046CD RID: 18125 RVA: 0x000E7AC0 File Offset: 0x000E5CC0
		public MissingSatelliteAssemblyException()
			: base("Resource lookup fell back to the ultimate fallback resources in a satellite assembly, but that satellite either was not found or could not be loaded. Please consider reinstalling or repairing the application.")
		{
			base.HResult = -2146233034;
		}

		// Token: 0x060046CE RID: 18126 RVA: 0x000E7AD8 File Offset: 0x000E5CD8
		public MissingSatelliteAssemblyException(string message)
			: base(message)
		{
			base.HResult = -2146233034;
		}

		// Token: 0x060046CF RID: 18127 RVA: 0x000E7AEC File Offset: 0x000E5CEC
		public MissingSatelliteAssemblyException(string message, string cultureName)
			: base(message)
		{
			base.HResult = -2146233034;
			this._cultureName = cultureName;
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x000E7B07 File Offset: 0x000E5D07
		public MissingSatelliteAssemblyException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233034;
		}

		// Token: 0x060046D1 RID: 18129 RVA: 0x000183F5 File Offset: 0x000165F5
		protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x060046D2 RID: 18130 RVA: 0x000E7B1C File Offset: 0x000E5D1C
		public string CultureName
		{
			get
			{
				return this._cultureName;
			}
		}

		// Token: 0x04002D21 RID: 11553
		private string _cultureName;
	}
}
