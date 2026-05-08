using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020002C6 RID: 710
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class InternalThread : CriticalFinalizerObject
	{
		// Token: 0x060020D2 RID: 8402
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Thread_free_internal();

		// Token: 0x060020D3 RID: 8403 RVA: 0x00077D14 File Offset: 0x00075F14
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~InternalThread()
		{
			this.Thread_free_internal();
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x00077D40 File Offset: 0x00075F40
		public InternalThread()
		{
		}

		// Token: 0x04001A51 RID: 6737
		private int lock_thread_id;

		// Token: 0x04001A52 RID: 6738
		private IntPtr handle;

		// Token: 0x04001A53 RID: 6739
		private IntPtr native_handle;

		// Token: 0x04001A54 RID: 6740
		private IntPtr name_chars;

		// Token: 0x04001A55 RID: 6741
		private int name_free;

		// Token: 0x04001A56 RID: 6742
		private int name_length;

		// Token: 0x04001A57 RID: 6743
		private ThreadState state;

		// Token: 0x04001A58 RID: 6744
		private object abort_exc;

		// Token: 0x04001A59 RID: 6745
		private int abort_state_handle;

		// Token: 0x04001A5A RID: 6746
		internal long thread_id;

		// Token: 0x04001A5B RID: 6747
		private IntPtr debugger_thread;

		// Token: 0x04001A5C RID: 6748
		private UIntPtr static_data;

		// Token: 0x04001A5D RID: 6749
		private IntPtr runtime_thread_info;

		// Token: 0x04001A5E RID: 6750
		private object current_appcontext;

		// Token: 0x04001A5F RID: 6751
		private object root_domain_thread;

		// Token: 0x04001A60 RID: 6752
		internal byte[] _serialized_principal;

		// Token: 0x04001A61 RID: 6753
		internal int _serialized_principal_version;

		// Token: 0x04001A62 RID: 6754
		private IntPtr appdomain_refs;

		// Token: 0x04001A63 RID: 6755
		private int interruption_requested;

		// Token: 0x04001A64 RID: 6756
		private IntPtr longlived;

		// Token: 0x04001A65 RID: 6757
		internal bool threadpool_thread;

		// Token: 0x04001A66 RID: 6758
		private bool thread_interrupt_requested;

		// Token: 0x04001A67 RID: 6759
		internal int stack_size;

		// Token: 0x04001A68 RID: 6760
		internal byte apartment_state;

		// Token: 0x04001A69 RID: 6761
		internal volatile int critical_region_level;

		// Token: 0x04001A6A RID: 6762
		internal int managed_id;

		// Token: 0x04001A6B RID: 6763
		private int small_id;

		// Token: 0x04001A6C RID: 6764
		private IntPtr manage_callback;

		// Token: 0x04001A6D RID: 6765
		private IntPtr flags;

		// Token: 0x04001A6E RID: 6766
		private IntPtr thread_pinning_ref;

		// Token: 0x04001A6F RID: 6767
		private IntPtr abort_protected_block_count;

		// Token: 0x04001A70 RID: 6768
		private int priority = 2;

		// Token: 0x04001A71 RID: 6769
		private IntPtr owned_mutex;

		// Token: 0x04001A72 RID: 6770
		private IntPtr suspended_event;

		// Token: 0x04001A73 RID: 6771
		private int self_suspended;

		// Token: 0x04001A74 RID: 6772
		private IntPtr thread_state;

		// Token: 0x04001A75 RID: 6773
		private IntPtr netcore0;

		// Token: 0x04001A76 RID: 6774
		private IntPtr netcore1;

		// Token: 0x04001A77 RID: 6775
		private IntPtr netcore2;

		// Token: 0x04001A78 RID: 6776
		private IntPtr last;
	}
}
