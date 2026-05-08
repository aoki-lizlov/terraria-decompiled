using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x02000A10 RID: 2576
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
	[ComVisible(true)]
	public sealed class DebuggableAttribute : Attribute
	{
		// Token: 0x06005FA7 RID: 24487 RVA: 0x0014BFBA File Offset: 0x0014A1BA
		public DebuggableAttribute(bool isJITTrackingEnabled, bool isJITOptimizerDisabled)
		{
			this.m_debuggingModes = DebuggableAttribute.DebuggingModes.None;
			if (isJITTrackingEnabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.Default;
			}
			if (isJITOptimizerDisabled)
			{
				this.m_debuggingModes |= DebuggableAttribute.DebuggingModes.DisableOptimizations;
			}
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x0014BFEF File Offset: 0x0014A1EF
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.m_debuggingModes = modes;
		}

		// Token: 0x17001006 RID: 4102
		// (get) Token: 0x06005FA9 RID: 24489 RVA: 0x0014BFFE File Offset: 0x0014A1FE
		public bool IsJITTrackingEnabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.Default) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		// Token: 0x17001007 RID: 4103
		// (get) Token: 0x06005FAA RID: 24490 RVA: 0x0014C00B File Offset: 0x0014A20B
		public bool IsJITOptimizerDisabled
		{
			get
			{
				return (this.m_debuggingModes & DebuggableAttribute.DebuggingModes.DisableOptimizations) > DebuggableAttribute.DebuggingModes.None;
			}
		}

		// Token: 0x17001008 RID: 4104
		// (get) Token: 0x06005FAB RID: 24491 RVA: 0x0014C01C File Offset: 0x0014A21C
		public DebuggableAttribute.DebuggingModes DebuggingFlags
		{
			get
			{
				return this.m_debuggingModes;
			}
		}

		// Token: 0x040039A1 RID: 14753
		private DebuggableAttribute.DebuggingModes m_debuggingModes;

		// Token: 0x02000A11 RID: 2577
		[Flags]
		[ComVisible(true)]
		public enum DebuggingModes
		{
			// Token: 0x040039A3 RID: 14755
			None = 0,
			// Token: 0x040039A4 RID: 14756
			Default = 1,
			// Token: 0x040039A5 RID: 14757
			DisableOptimizations = 256,
			// Token: 0x040039A6 RID: 14758
			IgnoreSymbolStoreSequencePoints = 2,
			// Token: 0x040039A7 RID: 14759
			EnableEditAndContinue = 4
		}
	}
}
