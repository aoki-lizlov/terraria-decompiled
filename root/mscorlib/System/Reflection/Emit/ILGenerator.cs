using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008FD RID: 2301
	[StructLayout(LayoutKind.Sequential)]
	public class ILGenerator : _ILGenerator
	{
		// Token: 0x06005002 RID: 20482 RVA: 0x000174FB File Offset: 0x000156FB
		void _ILGenerator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x000174FB File Offset: 0x000156FB
		void _ILGenerator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x000174FB File Offset: 0x000156FB
		void _ILGenerator.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x000174FB File Offset: 0x000156FB
		void _ILGenerator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x000FAE3E File Offset: 0x000F903E
		internal ILGenerator(Module m, TokenGenerator token_gen, int size)
		{
			if (size < 0)
			{
				size = 128;
			}
			this.code = new byte[size];
			this.token_fixups = new ILTokenInfo[8];
			this.module = m;
			this.token_gen = token_gen;
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x000FAE78 File Offset: 0x000F9078
		private void add_token_fixup(MemberInfo mi)
		{
			if (this.num_token_fixups == this.token_fixups.Length)
			{
				ILTokenInfo[] array = new ILTokenInfo[this.num_token_fixups * 2];
				this.token_fixups.CopyTo(array, 0);
				this.token_fixups = array;
			}
			this.token_fixups[this.num_token_fixups].member = mi;
			ILTokenInfo[] array2 = this.token_fixups;
			int num = this.num_token_fixups;
			this.num_token_fixups = num + 1;
			array2[num].code_pos = this.code_len;
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x000FAEF8 File Offset: 0x000F90F8
		private void make_room(int nbytes)
		{
			if (this.code_len + nbytes < this.code.Length)
			{
				return;
			}
			byte[] array = new byte[(this.code_len + nbytes) * 2 + 128];
			Array.Copy(this.code, 0, array, 0, this.code.Length);
			this.code = array;
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x000FAF4C File Offset: 0x000F914C
		private void emit_int(int val)
		{
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = (byte)(val & 255);
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = (byte)((val >> 8) & 255);
			byte[] array3 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array3[num] = (byte)((val >> 16) & 255);
			byte[] array4 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array4[num] = (byte)((val >> 24) & 255);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x000FAFE4 File Offset: 0x000F91E4
		private void ll_emit(OpCode opcode)
		{
			int num;
			if (opcode.Size == 2)
			{
				byte[] array = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array[num] = opcode.op1;
			}
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = opcode.op2;
			switch (opcode.StackBehaviourPush)
			{
			case StackBehaviour.Push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
			case StackBehaviour.Varpush:
				this.cur_stack++;
				break;
			case StackBehaviour.Push1_push1:
				this.cur_stack += 2;
				break;
			}
			if (this.max_stack < this.cur_stack)
			{
				this.max_stack = this.cur_stack;
			}
			switch (opcode.StackBehaviourPop)
			{
			case StackBehaviour.Pop1:
			case StackBehaviour.Popi:
			case StackBehaviour.Popref:
				this.cur_stack--;
				return;
			case StackBehaviour.Pop1_pop1:
			case StackBehaviour.Popi_pop1:
			case StackBehaviour.Popi_popi:
			case StackBehaviour.Popi_popi8:
			case StackBehaviour.Popi_popr4:
			case StackBehaviour.Popi_popr8:
			case StackBehaviour.Popref_pop1:
			case StackBehaviour.Popref_popi:
				this.cur_stack -= 2;
				return;
			case StackBehaviour.Popi_popi_popi:
			case StackBehaviour.Popref_popi_popi:
			case StackBehaviour.Popref_popi_popi8:
			case StackBehaviour.Popref_popi_popr4:
			case StackBehaviour.Popref_popi_popr8:
			case StackBehaviour.Popref_popi_popref:
				this.cur_stack -= 3;
				break;
			case StackBehaviour.Push0:
			case StackBehaviour.Push1:
			case StackBehaviour.Push1_push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
			case StackBehaviour.Varpop:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x000FB14B File Offset: 0x000F934B
		private static int target_len(OpCode opcode)
		{
			if (opcode.OperandType == OperandType.InlineBrTarget)
			{
				return 4;
			}
			return 1;
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x000FB15C File Offset: 0x000F935C
		private void InternalEndClause()
		{
			switch (this.ex_handlers[this.cur_block].LastClauseType())
			{
			case -1:
			case 0:
			case 1:
				this.Emit(OpCodes.Leave, this.ex_handlers[this.cur_block].end);
				return;
			case 2:
			case 4:
				this.Emit(OpCodes.Endfinally);
				break;
			case 3:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x000FB1D0 File Offset: 0x000F93D0
		public virtual void BeginCatchBlock(Type exceptionType)
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			if (exceptionType != null && exceptionType.IsUserType)
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			if (this.ex_handlers[this.cur_block].LastClauseType() == -1)
			{
				if (exceptionType != null)
				{
					throw new ArgumentException("Do not supply an exception type for filter clause");
				}
				this.Emit(OpCodes.Endfilter);
				this.ex_handlers[this.cur_block].PatchFilterClause(this.code_len);
			}
			else
			{
				this.InternalEndClause();
				this.ex_handlers[this.cur_block].AddCatch(exceptionType, this.code_len);
			}
			this.cur_stack = 1;
			if (this.max_stack < this.cur_stack)
			{
				this.max_stack = this.cur_stack;
			}
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x000FB2C0 File Offset: 0x000F94C0
		public virtual void BeginExceptFilterBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			this.InternalEndClause();
			this.ex_handlers[this.cur_block].AddFilter(this.code_len);
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x000FB31C File Offset: 0x000F951C
		public virtual Label BeginExceptionBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.ex_handlers != null)
			{
				this.cur_block = this.ex_handlers.Length;
				ILExceptionInfo[] array = new ILExceptionInfo[this.cur_block + 1];
				Array.Copy(this.ex_handlers, array, this.cur_block);
				this.ex_handlers = array;
			}
			else
			{
				this.ex_handlers = new ILExceptionInfo[1];
				this.cur_block = 0;
			}
			this.open_blocks.Push(this.cur_block);
			this.ex_handlers[this.cur_block].start = this.code_len;
			return this.ex_handlers[this.cur_block].end = this.DefineLabel();
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x000FB3E0 File Offset: 0x000F95E0
		public virtual void BeginFaultBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			if (this.ex_handlers[this.cur_block].LastClauseType() == -1)
			{
				this.Emit(OpCodes.Leave, this.ex_handlers[this.cur_block].end);
				this.ex_handlers[this.cur_block].PatchFilterClause(this.code_len);
			}
			this.InternalEndClause();
			this.ex_handlers[this.cur_block].AddFault(this.code_len);
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x000FB494 File Offset: 0x000F9694
		public virtual void BeginFinallyBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			this.InternalEndClause();
			if (this.ex_handlers[this.cur_block].LastClauseType() == -1)
			{
				this.Emit(OpCodes.Leave, this.ex_handlers[this.cur_block].end);
				this.ex_handlers[this.cur_block].PatchFilterClause(this.code_len);
			}
			this.ex_handlers[this.cur_block].AddFinally(this.code_len);
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void BeginScope()
		{
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x000FB546 File Offset: 0x000F9746
		public virtual LocalBuilder DeclareLocal(Type localType)
		{
			return this.DeclareLocal(localType, false);
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x000FB550 File Offset: 0x000F9750
		public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			if (localType.IsUserType)
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			LocalBuilder localBuilder = new LocalBuilder(localType, this);
			localBuilder.is_pinned = pinned;
			if (this.locals != null)
			{
				LocalBuilder[] array = new LocalBuilder[this.locals.Length + 1];
				Array.Copy(this.locals, array, this.locals.Length);
				array[this.locals.Length] = localBuilder;
				this.locals = array;
			}
			else
			{
				this.locals = new LocalBuilder[1];
				this.locals[0] = localBuilder;
			}
			localBuilder.position = (ushort)(this.locals.Length - 1);
			return localBuilder;
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x000FB5FC File Offset: 0x000F97FC
		public virtual Label DefineLabel()
		{
			if (this.labels == null)
			{
				this.labels = new ILGenerator.LabelData[4];
			}
			else if (this.num_labels >= this.labels.Length)
			{
				ILGenerator.LabelData[] array = new ILGenerator.LabelData[this.labels.Length * 2];
				Array.Copy(this.labels, array, this.labels.Length);
				this.labels = array;
			}
			this.labels[this.num_labels] = new ILGenerator.LabelData(-1, 0);
			int num = this.num_labels;
			this.num_labels = num + 1;
			return new Label(num);
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x000FB688 File Offset: 0x000F9888
		public virtual void Emit(OpCode opcode)
		{
			this.make_room(2);
			this.ll_emit(opcode);
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x000FB698 File Offset: 0x000F9898
		public virtual void Emit(OpCode opcode, byte arg)
		{
			this.make_room(3);
			this.ll_emit(opcode);
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = arg;
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x000FB6CC File Offset: 0x000F98CC
		[ComVisible(true)]
		public virtual void Emit(OpCode opcode, ConstructorInfo con)
		{
			int token = this.token_gen.GetToken(con, true);
			this.make_room(6);
			this.ll_emit(opcode);
			if (con.DeclaringType.Module == this.module || con is ConstructorOnTypeBuilderInst || con is ConstructorBuilder)
			{
				this.add_token_fixup(con);
			}
			this.emit_int(token);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= con.GetParametersCount();
			}
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x000FB74C File Offset: 0x000F994C
		public virtual void Emit(OpCode opcode, double arg)
		{
			byte[] bytes = BitConverter.GetBytes(arg);
			this.make_room(10);
			this.ll_emit(opcode);
			if (BitConverter.IsLittleEndian)
			{
				Array.Copy(bytes, 0, this.code, this.code_len, 8);
				this.code_len += 8;
				return;
			}
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = bytes[7];
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = bytes[6];
			byte[] array3 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array3[num] = bytes[5];
			byte[] array4 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array4[num] = bytes[4];
			byte[] array5 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array5[num] = bytes[3];
			byte[] array6 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array6[num] = bytes[2];
			byte[] array7 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array7[num] = bytes[1];
			byte[] array8 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array8[num] = bytes[0];
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x000FB874 File Offset: 0x000F9A74
		public virtual void Emit(OpCode opcode, FieldInfo field)
		{
			int token = this.token_gen.GetToken(field, true);
			this.make_room(6);
			this.ll_emit(opcode);
			if (field.DeclaringType.Module == this.module || field is FieldOnTypeBuilderInst || field is FieldBuilder)
			{
				this.add_token_fixup(field);
			}
			this.emit_int(token);
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x000FB8D4 File Offset: 0x000F9AD4
		public virtual void Emit(OpCode opcode, short arg)
		{
			this.make_room(4);
			this.ll_emit(opcode);
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = (byte)(arg & 255);
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = (byte)((arg >> 8) & 255);
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x000FB931 File Offset: 0x000F9B31
		public virtual void Emit(OpCode opcode, int arg)
		{
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(arg);
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x000FB948 File Offset: 0x000F9B48
		public virtual void Emit(OpCode opcode, long arg)
		{
			this.make_room(10);
			this.ll_emit(opcode);
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = (byte)(arg & 255L);
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = (byte)((arg >> 8) & 255L);
			byte[] array3 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array3[num] = (byte)((arg >> 16) & 255L);
			byte[] array4 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array4[num] = (byte)((arg >> 24) & 255L);
			byte[] array5 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array5[num] = (byte)((arg >> 32) & 255L);
			byte[] array6 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array6[num] = (byte)((arg >> 40) & 255L);
			byte[] array7 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array7[num] = (byte)((arg >> 48) & 255L);
			byte[] array8 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array8[num] = (byte)((arg >> 56) & 255L);
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x000FBA80 File Offset: 0x000F9C80
		public virtual void Emit(OpCode opcode, Label label)
		{
			int num = ILGenerator.target_len(opcode);
			this.make_room(6);
			this.ll_emit(opcode);
			if (this.cur_stack > this.labels[label.label].maxStack)
			{
				this.labels[label.label].maxStack = this.cur_stack;
			}
			if (this.fixups == null)
			{
				this.fixups = new ILGenerator.LabelFixup[4];
			}
			else if (this.num_fixups >= this.fixups.Length)
			{
				ILGenerator.LabelFixup[] array = new ILGenerator.LabelFixup[this.fixups.Length * 2];
				Array.Copy(this.fixups, array, this.fixups.Length);
				this.fixups = array;
			}
			this.fixups[this.num_fixups].offset = num;
			this.fixups[this.num_fixups].pos = this.code_len;
			this.fixups[this.num_fixups].label_idx = label.label;
			this.num_fixups++;
			this.code_len += num;
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x000FBB98 File Offset: 0x000F9D98
		public virtual void Emit(OpCode opcode, Label[] labels)
		{
			if (labels == null)
			{
				throw new ArgumentNullException("labels");
			}
			int num = labels.Length;
			this.make_room(6 + num * 4);
			this.ll_emit(opcode);
			for (int i = 0; i < num; i++)
			{
				if (this.cur_stack > this.labels[labels[i].label].maxStack)
				{
					this.labels[labels[i].label].maxStack = this.cur_stack;
				}
			}
			this.emit_int(num);
			if (this.fixups == null)
			{
				this.fixups = new ILGenerator.LabelFixup[4 + num];
			}
			else if (this.num_fixups + num >= this.fixups.Length)
			{
				ILGenerator.LabelFixup[] array = new ILGenerator.LabelFixup[num + this.fixups.Length * 2];
				Array.Copy(this.fixups, array, this.fixups.Length);
				this.fixups = array;
			}
			int j = 0;
			int num2 = num * 4;
			while (j < num)
			{
				this.fixups[this.num_fixups].offset = num2;
				this.fixups[this.num_fixups].pos = this.code_len;
				this.fixups[this.num_fixups].label_idx = labels[j].label;
				this.num_fixups++;
				this.code_len += 4;
				j++;
				num2 -= 4;
			}
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x000FBD04 File Offset: 0x000F9F04
		public virtual void Emit(OpCode opcode, LocalBuilder local)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			if (local.ilgen != this)
			{
				throw new ArgumentException("Trying to emit a local from a different ILGenerator.");
			}
			uint position = (uint)local.position;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			this.make_room(6);
			if (opcode.StackBehaviourPop == StackBehaviour.Pop1)
			{
				this.cur_stack--;
				flag2 = true;
			}
			else if (opcode.StackBehaviourPush == StackBehaviour.Push1 || opcode.StackBehaviourPush == StackBehaviour.Pushi)
			{
				this.cur_stack++;
				flag3 = true;
				if (this.cur_stack > this.max_stack)
				{
					this.max_stack = this.cur_stack;
				}
				flag = opcode.StackBehaviourPush == StackBehaviour.Pushi;
			}
			if (flag)
			{
				int num;
				if (position < 256U)
				{
					byte[] array = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array[num] = 18;
					byte[] array2 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array2[num] = (byte)position;
					return;
				}
				byte[] array3 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array3[num] = 254;
				byte[] array4 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array4[num] = 13;
				byte[] array5 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array5[num] = (byte)(position & 255U);
				byte[] array6 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array6[num] = (byte)((position >> 8) & 255U);
				return;
			}
			else if (flag2)
			{
				int num;
				if (position < 4U)
				{
					byte[] array7 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array7[num] = (byte)(10U + position);
					return;
				}
				if (position < 256U)
				{
					byte[] array8 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array8[num] = 19;
					byte[] array9 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array9[num] = (byte)position;
					return;
				}
				byte[] array10 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array10[num] = 254;
				byte[] array11 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array11[num] = 14;
				byte[] array12 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array12[num] = (byte)(position & 255U);
				byte[] array13 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array13[num] = (byte)((position >> 8) & 255U);
				return;
			}
			else
			{
				if (!flag3)
				{
					this.ll_emit(opcode);
					return;
				}
				int num;
				if (position < 4U)
				{
					byte[] array14 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array14[num] = (byte)(6U + position);
					return;
				}
				if (position < 256U)
				{
					byte[] array15 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array15[num] = 17;
					byte[] array16 = this.code;
					num = this.code_len;
					this.code_len = num + 1;
					array16[num] = (byte)position;
					return;
				}
				byte[] array17 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array17[num] = 254;
				byte[] array18 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array18[num] = 12;
				byte[] array19 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array19[num] = (byte)(position & 255U);
				byte[] array20 = this.code;
				num = this.code_len;
				this.code_len = num + 1;
				array20[num] = (byte)((position >> 8) & 255U);
				return;
			}
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x000FC06C File Offset: 0x000FA26C
		public virtual void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			if (meth is DynamicMethod && (opcode == OpCodes.Ldftn || opcode == OpCodes.Ldvirtftn || opcode == OpCodes.Ldtoken))
			{
				throw new ArgumentException("Ldtoken, Ldftn and Ldvirtftn OpCodes cannot target DynamicMethods.");
			}
			int token = this.token_gen.GetToken(meth, true);
			this.make_room(6);
			this.ll_emit(opcode);
			Type declaringType = meth.DeclaringType;
			if (declaringType != null && (declaringType.Module == this.module || meth is MethodOnTypeBuilderInst || meth is MethodBuilder))
			{
				this.add_token_fixup(meth);
			}
			this.emit_int(token);
			if (meth.ReturnType != typeof(void))
			{
				this.cur_stack++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= meth.GetParametersCount();
			}
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x000FC168 File Offset: 0x000FA368
		private void Emit(OpCode opcode, MethodInfo method, int token)
		{
			this.make_room(6);
			this.ll_emit(opcode);
			Type declaringType = method.DeclaringType;
			if (declaringType != null && (declaringType.Module == this.module || method is MethodBuilder))
			{
				this.add_token_fixup(method);
			}
			this.emit_int(token);
			if (method.ReturnType != typeof(void))
			{
				this.cur_stack++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				this.cur_stack -= method.GetParametersCount();
			}
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x000FC200 File Offset: 0x000FA400
		[CLSCompliant(false)]
		public void Emit(OpCode opcode, sbyte arg)
		{
			this.make_room(3);
			this.ll_emit(opcode);
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = (byte)arg;
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x000FC238 File Offset: 0x000FA438
		public virtual void Emit(OpCode opcode, SignatureHelper signature)
		{
			int token = this.token_gen.GetToken(signature);
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(token);
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x000FC268 File Offset: 0x000FA468
		public virtual void Emit(OpCode opcode, float arg)
		{
			byte[] bytes = BitConverter.GetBytes(arg);
			this.make_room(6);
			this.ll_emit(opcode);
			if (BitConverter.IsLittleEndian)
			{
				Array.Copy(bytes, 0, this.code, this.code_len, 4);
				this.code_len += 4;
				return;
			}
			byte[] array = this.code;
			int num = this.code_len;
			this.code_len = num + 1;
			array[num] = bytes[3];
			byte[] array2 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array2[num] = bytes[2];
			byte[] array3 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array3[num] = bytes[1];
			byte[] array4 = this.code;
			num = this.code_len;
			this.code_len = num + 1;
			array4[num] = bytes[0];
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x000FC320 File Offset: 0x000FA520
		public virtual void Emit(OpCode opcode, string str)
		{
			int token = this.token_gen.GetToken(str);
			this.make_room(6);
			this.ll_emit(opcode);
			this.emit_int(token);
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x000FC350 File Offset: 0x000FA550
		public virtual void Emit(OpCode opcode, Type cls)
		{
			if (cls != null && cls.IsByRef)
			{
				throw new ArgumentException("Cannot get TypeToken for a ByRef type.");
			}
			this.make_room(6);
			this.ll_emit(opcode);
			int token = this.token_gen.GetToken(cls, opcode != OpCodes.Ldtoken);
			if (cls is TypeBuilderInstantiation || cls is SymbolType || cls is TypeBuilder || cls is GenericTypeParameterBuilder || cls is EnumBuilder)
			{
				this.add_token_fixup(cls);
			}
			this.emit_int(token);
		}

		// Token: 0x06005028 RID: 20520 RVA: 0x000FC3D8 File Offset: 0x000FA5D8
		[MonoLimitation("vararg methods are not supported")]
		public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			short value = opcode.Value;
			if (value != OpCodes.Call.Value && value != OpCodes.Callvirt.Value)
			{
				throw new NotSupportedException("Only Call and CallVirt are allowed");
			}
			if ((methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				optionalParameterTypes = null;
			}
			if (optionalParameterTypes == null)
			{
				this.Emit(opcode, methodInfo);
				return;
			}
			if ((methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException("Method is not VarArgs method and optional types were passed");
			}
			int token = this.token_gen.GetToken(methodInfo, optionalParameterTypes);
			this.Emit(opcode, methodInfo, token);
		}

		// Token: 0x06005029 RID: 20521 RVA: 0x000FC46C File Offset: 0x000FA66C
		public virtual void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this.module as ModuleBuilder, (CallingConventions)0, unmanagedCallConv, returnType, parameterTypes);
			this.Emit(opcode, methodSigHelper);
		}

		// Token: 0x0600502A RID: 20522 RVA: 0x000FC498 File Offset: 0x000FA698
		public virtual void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			if (optionalParameterTypes != null)
			{
				throw new NotImplementedException();
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(this.module as ModuleBuilder, callingConvention, (CallingConvention)0, returnType, parameterTypes);
			this.Emit(opcode, methodSigHelper);
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x000FC4D0 File Offset: 0x000FA6D0
		public virtual void EmitWriteLine(FieldInfo fld)
		{
			if (fld == null)
			{
				throw new ArgumentNullException("fld");
			}
			if (fld.IsStatic)
			{
				this.Emit(OpCodes.Ldsfld, fld);
			}
			else
			{
				this.Emit(OpCodes.Ldarg_0);
				this.Emit(OpCodes.Ldfld, fld);
			}
			this.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { fld.FieldType }));
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x000FC54C File Offset: 0x000FA74C
		public virtual void EmitWriteLine(LocalBuilder localBuilder)
		{
			if (localBuilder == null)
			{
				throw new ArgumentNullException("localBuilder");
			}
			if (localBuilder.LocalType is TypeBuilder)
			{
				throw new ArgumentException("Output streams do not support TypeBuilders.");
			}
			this.Emit(OpCodes.Ldloc, localBuilder);
			this.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { localBuilder.LocalType }));
		}

		// Token: 0x0600502D RID: 20525 RVA: 0x000FC5B9 File Offset: 0x000FA7B9
		public virtual void EmitWriteLine(string value)
		{
			this.Emit(OpCodes.Ldstr, value);
			this.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }));
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x000FC5FC File Offset: 0x000FA7FC
		public virtual void EndExceptionBlock()
		{
			if (this.open_blocks == null)
			{
				this.open_blocks = new Stack(2);
			}
			if (this.open_blocks.Count <= 0)
			{
				throw new NotSupportedException("Not in an exception block");
			}
			if (this.ex_handlers[this.cur_block].LastClauseType() == -1)
			{
				throw new InvalidOperationException("Incorrect code generation for exception block.");
			}
			this.InternalEndClause();
			this.MarkLabel(this.ex_handlers[this.cur_block].end);
			this.ex_handlers[this.cur_block].End(this.code_len);
			this.ex_handlers[this.cur_block].Debug(this.cur_block);
			this.open_blocks.Pop();
			if (this.open_blocks.Count > 0)
			{
				this.cur_block = (int)this.open_blocks.Peek();
			}
		}

		// Token: 0x0600502F RID: 20527 RVA: 0x00004088 File Offset: 0x00002288
		public virtual void EndScope()
		{
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x000FC6E4 File Offset: 0x000FA8E4
		public virtual void MarkLabel(Label loc)
		{
			if (loc.label < 0 || loc.label >= this.num_labels)
			{
				throw new ArgumentException("The label is not valid");
			}
			if (this.labels[loc.label].addr >= 0)
			{
				throw new ArgumentException("The label was already defined");
			}
			this.labels[loc.label].addr = this.code_len;
			if (this.labels[loc.label].maxStack > this.cur_stack)
			{
				this.cur_stack = this.labels[loc.label].maxStack;
			}
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x000FC790 File Offset: 0x000FA990
		public virtual void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			if (this.currentSequence == null || this.currentSequence.Document != document)
			{
				if (this.sequencePointLists == null)
				{
					this.sequencePointLists = new ArrayList();
				}
				this.currentSequence = new SequencePointList(document);
				this.sequencePointLists.Add(this.currentSequence);
			}
			this.currentSequence.AddSequencePoint(this.code_len, startLine, startColumn, endLine, endColumn);
		}

		// Token: 0x06005032 RID: 20530 RVA: 0x000FC7FC File Offset: 0x000FA9FC
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			if (this.sequencePointLists != null)
			{
				SequencePointList sequencePointList = (SequencePointList)this.sequencePointLists[0];
				SequencePointList sequencePointList2 = (SequencePointList)this.sequencePointLists[this.sequencePointLists.Count - 1];
				symbolWriter.SetMethodSourceRange(sequencePointList.Document, sequencePointList.StartLine, sequencePointList.StartColumn, sequencePointList2.Document, sequencePointList2.EndLine, sequencePointList2.EndColumn);
				foreach (object obj in this.sequencePointLists)
				{
					SequencePointList sequencePointList3 = (SequencePointList)obj;
					symbolWriter.DefineSequencePoints(sequencePointList3.Document, sequencePointList3.GetOffsets(), sequencePointList3.GetLines(), sequencePointList3.GetColumns(), sequencePointList3.GetEndLines(), sequencePointList3.GetEndColumns());
				}
				if (this.locals != null)
				{
					foreach (LocalBuilder localBuilder in this.locals)
					{
						if (localBuilder.Name != null && localBuilder.Name.Length > 0)
						{
							SignatureHelper localVarSigHelper = SignatureHelper.GetLocalVarSigHelper(this.module as ModuleBuilder);
							localVarSigHelper.AddArgument(localBuilder.LocalType);
							byte[] signature = localVarSigHelper.GetSignature();
							symbolWriter.DefineLocalVariable(localBuilder.Name, FieldAttributes.Public, signature, SymAddressKind.ILOffset, (int)localBuilder.position, 0, 0, localBuilder.StartOffset, localBuilder.EndOffset);
						}
					}
				}
				this.sequencePointLists = null;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06005033 RID: 20531 RVA: 0x000FC978 File Offset: 0x000FAB78
		internal bool HasDebugInfo
		{
			get
			{
				return this.sequencePointLists != null;
			}
		}

		// Token: 0x06005034 RID: 20532 RVA: 0x000FC984 File Offset: 0x000FAB84
		public virtual void ThrowException(Type excType)
		{
			if (excType == null)
			{
				throw new ArgumentNullException("excType");
			}
			if (!(excType == typeof(Exception)) && !excType.IsSubclassOf(typeof(Exception)))
			{
				throw new ArgumentException("Type should be an exception type", "excType");
			}
			ConstructorInfo constructor = excType.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new ArgumentException("Type should have a default constructor", "excType");
			}
			this.Emit(OpCodes.Newobj, constructor);
			this.Emit(OpCodes.Throw);
		}

		// Token: 0x06005035 RID: 20533 RVA: 0x000174FB File Offset: 0x000156FB
		[MonoTODO("Not implemented")]
		public virtual void UsingNamespace(string usingNamespace)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x000FCA18 File Offset: 0x000FAC18
		internal void label_fixup(MethodBase mb)
		{
			for (int i = 0; i < this.num_fixups; i++)
			{
				if (this.labels[this.fixups[i].label_idx].addr < 0)
				{
					throw new ArgumentException(string.Format("Label #{0} is not marked in method `{1}'", this.fixups[i].label_idx + 1, mb.Name));
				}
				int num = this.labels[this.fixups[i].label_idx].addr - (this.fixups[i].pos + this.fixups[i].offset);
				if (this.fixups[i].offset == 1)
				{
					this.code[this.fixups[i].pos] = (byte)((sbyte)num);
				}
				else
				{
					int num2 = this.code_len;
					this.code_len = this.fixups[i].pos;
					this.emit_int(num);
					this.code_len = num2;
				}
			}
		}

		// Token: 0x06005037 RID: 20535 RVA: 0x000FCB34 File Offset: 0x000FAD34
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			for (int i = 0; i < this.num_token_fixups; i++)
			{
				int code_pos = this.token_fixups[i].code_pos;
				int num = (int)this.code[code_pos] | ((int)this.code[code_pos + 1] << 8) | ((int)this.code[code_pos + 2] << 16) | ((int)this.code[code_pos + 3] << 24);
				int num2;
				if (token_map.TryGetValue(num, out num2))
				{
					this.token_fixups[i].member = member_map[num];
					int num3 = this.code_len;
					this.code_len = code_pos;
					this.emit_int(num2);
					this.code_len = num3;
				}
			}
		}

		// Token: 0x06005038 RID: 20536 RVA: 0x000FCBDD File Offset: 0x000FADDD
		internal void SetExceptionHandlers(ILExceptionInfo[] exHandlers)
		{
			this.ex_handlers = exHandlers;
		}

		// Token: 0x06005039 RID: 20537 RVA: 0x000FCBE6 File Offset: 0x000FADE6
		internal void SetTokenFixups(ILTokenInfo[] tokenFixups)
		{
			this.token_fixups = tokenFixups;
		}

		// Token: 0x0600503A RID: 20538 RVA: 0x000FCBEF File Offset: 0x000FADEF
		internal void SetCode(byte[] code, int max_stack)
		{
			this.code = (byte[])code.Clone();
			this.code_len = code.Length;
			this.max_stack = max_stack;
			this.cur_stack = 0;
		}

		// Token: 0x0600503B RID: 20539 RVA: 0x000FCC1C File Offset: 0x000FAE1C
		internal unsafe void SetCode(byte* code, int code_size, int max_stack)
		{
			this.code = new byte[code_size];
			for (int i = 0; i < code_size; i++)
			{
				this.code[i] = code[i];
			}
			this.code_len = code_size;
			this.max_stack = max_stack;
			this.cur_stack = 0;
		}

		// Token: 0x0600503C RID: 20540 RVA: 0x000FCC64 File Offset: 0x000FAE64
		internal void Init(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.SetCode(il, maxStack);
			if (exceptionHandlers != null)
			{
				Dictionary<Tuple<int, int>, List<ExceptionHandler>> dictionary = new Dictionary<Tuple<int, int>, List<ExceptionHandler>>();
				foreach (ExceptionHandler exceptionHandler in exceptionHandlers)
				{
					Tuple<int, int> tuple = new Tuple<int, int>(exceptionHandler.TryOffset, exceptionHandler.TryLength);
					List<ExceptionHandler> list;
					if (!dictionary.TryGetValue(tuple, out list))
					{
						list = new List<ExceptionHandler>();
						dictionary.Add(tuple, list);
					}
					list.Add(exceptionHandler);
				}
				List<ILExceptionInfo> list2 = new List<ILExceptionInfo>();
				foreach (KeyValuePair<Tuple<int, int>, List<ExceptionHandler>> keyValuePair in dictionary)
				{
					ILExceptionInfo ilexceptionInfo = new ILExceptionInfo
					{
						start = keyValuePair.Key.Item1,
						len = keyValuePair.Key.Item2,
						handlers = new ILExceptionBlock[keyValuePair.Value.Count]
					};
					list2.Add(ilexceptionInfo);
					int num = 0;
					foreach (ExceptionHandler exceptionHandler2 in keyValuePair.Value)
					{
						ilexceptionInfo.handlers[num++] = new ILExceptionBlock
						{
							start = exceptionHandler2.HandlerOffset,
							len = exceptionHandler2.HandlerLength,
							filter_offset = exceptionHandler2.FilterOffset,
							type = (int)exceptionHandler2.Kind,
							extype = this.module.ResolveType(exceptionHandler2.ExceptionTypeToken)
						};
					}
				}
				this.SetExceptionHandlers(list2.ToArray());
			}
			if (tokenFixups != null)
			{
				List<ILTokenInfo> list3 = new List<ILTokenInfo>();
				foreach (int num2 in tokenFixups)
				{
					int num3 = (int)BitConverter.ToUInt32(il, num2);
					ILTokenInfo iltokenInfo = new ILTokenInfo
					{
						code_pos = num2,
						member = ((ModuleBuilder)this.module).ResolveOrGetRegisteredToken(num3, null, null)
					};
					list3.Add(iltokenInfo);
				}
				this.SetTokenFixups(list3.ToArray());
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x0600503D RID: 20541 RVA: 0x000FCF0C File Offset: 0x000FB10C
		internal TokenGenerator TokenGenerator
		{
			get
			{
				return this.token_gen;
			}
		}

		// Token: 0x0600503E RID: 20542 RVA: 0x000FCF14 File Offset: 0x000FB114
		[Obsolete("Use ILOffset", true)]
		internal static int Mono_GetCurrentOffset(ILGenerator ig)
		{
			return ig.code_len;
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x0600503F RID: 20543 RVA: 0x000FCF14 File Offset: 0x000FB114
		public virtual int ILOffset
		{
			get
			{
				return this.code_len;
			}
		}

		// Token: 0x0400310E RID: 12558
		private byte[] code;

		// Token: 0x0400310F RID: 12559
		private int code_len;

		// Token: 0x04003110 RID: 12560
		private int max_stack;

		// Token: 0x04003111 RID: 12561
		private int cur_stack;

		// Token: 0x04003112 RID: 12562
		private LocalBuilder[] locals;

		// Token: 0x04003113 RID: 12563
		private ILExceptionInfo[] ex_handlers;

		// Token: 0x04003114 RID: 12564
		private int num_token_fixups;

		// Token: 0x04003115 RID: 12565
		private ILTokenInfo[] token_fixups;

		// Token: 0x04003116 RID: 12566
		private ILGenerator.LabelData[] labels;

		// Token: 0x04003117 RID: 12567
		private int num_labels;

		// Token: 0x04003118 RID: 12568
		private ILGenerator.LabelFixup[] fixups;

		// Token: 0x04003119 RID: 12569
		private int num_fixups;

		// Token: 0x0400311A RID: 12570
		internal Module module;

		// Token: 0x0400311B RID: 12571
		private int cur_block;

		// Token: 0x0400311C RID: 12572
		private Stack open_blocks;

		// Token: 0x0400311D RID: 12573
		private TokenGenerator token_gen;

		// Token: 0x0400311E RID: 12574
		private const int defaultFixupSize = 4;

		// Token: 0x0400311F RID: 12575
		private const int defaultLabelsSize = 4;

		// Token: 0x04003120 RID: 12576
		private const int defaultExceptionStackSize = 2;

		// Token: 0x04003121 RID: 12577
		private ArrayList sequencePointLists;

		// Token: 0x04003122 RID: 12578
		private SequencePointList currentSequence;

		// Token: 0x020008FE RID: 2302
		private struct LabelFixup
		{
			// Token: 0x04003123 RID: 12579
			public int offset;

			// Token: 0x04003124 RID: 12580
			public int pos;

			// Token: 0x04003125 RID: 12581
			public int label_idx;
		}

		// Token: 0x020008FF RID: 2303
		private struct LabelData
		{
			// Token: 0x06005040 RID: 20544 RVA: 0x000FCF1C File Offset: 0x000FB11C
			public LabelData(int addr, int maxStack)
			{
				this.addr = addr;
				this.maxStack = maxStack;
			}

			// Token: 0x04003126 RID: 12582
			public int addr;

			// Token: 0x04003127 RID: 12583
			public int maxStack;
		}
	}
}
