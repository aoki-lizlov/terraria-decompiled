using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F1 RID: 2289
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_EventBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class EventBuilder : _EventBuilder
	{
		// Token: 0x06004F50 RID: 20304 RVA: 0x000174FB File Offset: 0x000156FB
		void _EventBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x000174FB File Offset: 0x000156FB
		void _EventBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x000174FB File Offset: 0x000156FB
		void _EventBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x000174FB File Offset: 0x000156FB
		void _EventBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x000FA05E File Offset: 0x000F825E
		internal EventBuilder(TypeBuilder tb, string eventName, EventAttributes eventAttrs, Type eventType)
		{
			this.name = eventName;
			this.attrs = eventAttrs;
			this.type = eventType;
			this.typeb = tb;
			this.table_idx = this.get_next_table_index(this, 20, 1);
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x000FA093 File Offset: 0x000F8293
		internal int get_next_table_index(object obj, int table, int count)
		{
			return this.typeb.get_next_table_index(obj, table, count);
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x000FA0A4 File Offset: 0x000F82A4
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			if (this.other_methods != null)
			{
				MethodBuilder[] array = new MethodBuilder[this.other_methods.Length + 1];
				this.other_methods.CopyTo(array, 0);
				this.other_methods = array;
			}
			else
			{
				this.other_methods = new MethodBuilder[1];
			}
			this.other_methods[this.other_methods.Length - 1] = mdBuilder;
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x000FA117 File Offset: 0x000F8317
		public EventToken GetEventToken()
		{
			return new EventToken(335544320 | this.table_idx);
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x000FA12A File Offset: 0x000F832A
		public void SetAddOnMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.add_method = mdBuilder;
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x000FA14D File Offset: 0x000F834D
		public void SetRaiseMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.raise_method = mdBuilder;
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x000FA170 File Offset: 0x000F8370
		public void SetRemoveOnMethod(MethodBuilder mdBuilder)
		{
			if (mdBuilder == null)
			{
				throw new ArgumentNullException("mdBuilder");
			}
			this.RejectIfCreated();
			this.remove_method = mdBuilder;
		}

		// Token: 0x06004F5B RID: 20315 RVA: 0x000FA194 File Offset: 0x000F8394
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			this.RejectIfCreated();
			if (customBuilder.Ctor.ReflectedType.FullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= EventAttributes.SpecialName;
				return;
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
				return;
			}
			this.cattrs = new CustomAttributeBuilder[1];
			this.cattrs[0] = customBuilder;
		}

		// Token: 0x06004F5C RID: 20316 RVA: 0x000FA231 File Offset: 0x000F8431
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x000FA262 File Offset: 0x000F8462
		private void RejectIfCreated()
		{
			if (this.typeb.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x040030CD RID: 12493
		internal string name;

		// Token: 0x040030CE RID: 12494
		private Type type;

		// Token: 0x040030CF RID: 12495
		private TypeBuilder typeb;

		// Token: 0x040030D0 RID: 12496
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040030D1 RID: 12497
		internal MethodBuilder add_method;

		// Token: 0x040030D2 RID: 12498
		internal MethodBuilder remove_method;

		// Token: 0x040030D3 RID: 12499
		internal MethodBuilder raise_method;

		// Token: 0x040030D4 RID: 12500
		internal MethodBuilder[] other_methods;

		// Token: 0x040030D5 RID: 12501
		internal EventAttributes attrs;

		// Token: 0x040030D6 RID: 12502
		private int table_idx;
	}
}
