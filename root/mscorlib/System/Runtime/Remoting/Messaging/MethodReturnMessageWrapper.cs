using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020005FE RID: 1534
	[ComVisible(true)]
	public class MethodReturnMessageWrapper : InternalMessageWrapper, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06003B3A RID: 15162 RVA: 0x000CFB28 File Offset: 0x000CDD28
		public MethodReturnMessageWrapper(IMethodReturnMessage msg)
			: base(msg)
		{
			if (msg.Exception != null)
			{
				this._exception = msg.Exception;
				this._args = new object[0];
				return;
			}
			this._args = msg.Args;
			this._return = msg.ReturnValue;
			if (msg.MethodBase != null)
			{
				this._outArgInfo = new ArgInfo(msg.MethodBase, ArgInfoType.Out);
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06003B3B RID: 15163 RVA: 0x000CFB95 File Offset: 0x000CDD95
		public virtual int ArgCount
		{
			get
			{
				return this._args.Length;
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06003B3C RID: 15164 RVA: 0x000CFB9F File Offset: 0x000CDD9F
		// (set) Token: 0x06003B3D RID: 15165 RVA: 0x000CFBA7 File Offset: 0x000CDDA7
		public virtual object[] Args
		{
			get
			{
				return this._args;
			}
			set
			{
				this._args = value;
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06003B3E RID: 15166 RVA: 0x000CFBB0 File Offset: 0x000CDDB0
		// (set) Token: 0x06003B3F RID: 15167 RVA: 0x000CFBB8 File Offset: 0x000CDDB8
		public virtual Exception Exception
		{
			get
			{
				return this._exception;
			}
			set
			{
				this._exception = value;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06003B40 RID: 15168 RVA: 0x000CFBC1 File Offset: 0x000CDDC1
		public virtual bool HasVarArgs
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).HasVarArgs;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x000CFBD3 File Offset: 0x000CDDD3
		public virtual LogicalCallContext LogicalCallContext
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).LogicalCallContext;
			}
		}

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x000CFBE5 File Offset: 0x000CDDE5
		public virtual MethodBase MethodBase
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodBase;
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000CFBF7 File Offset: 0x000CDDF7
		public virtual string MethodName
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodName;
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x000CFC09 File Offset: 0x000CDE09
		public virtual object MethodSignature
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).MethodSignature;
			}
		}

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x000CFC1B File Offset: 0x000CDE1B
		public virtual int OutArgCount
		{
			get
			{
				if (this._outArgInfo == null)
				{
					return 0;
				}
				return this._outArgInfo.GetInOutArgCount();
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x000CFC32 File Offset: 0x000CDE32
		public virtual object[] OutArgs
		{
			get
			{
				if (this._outArgInfo == null)
				{
					return this._args;
				}
				return this._outArgInfo.GetInOutArgs(this._args);
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000CFC54 File Offset: 0x000CDE54
		public virtual IDictionary Properties
		{
			get
			{
				if (this._properties == null)
				{
					this._properties = new MethodReturnMessageWrapper.DictionaryWrapper(this, this.WrappedMessage.Properties);
				}
				return this._properties;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000CFC7B File Offset: 0x000CDE7B
		// (set) Token: 0x06003B49 RID: 15177 RVA: 0x000CFC83 File Offset: 0x000CDE83
		public virtual object ReturnValue
		{
			get
			{
				return this._return;
			}
			set
			{
				this._return = value;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06003B4A RID: 15178 RVA: 0x000CFC8C File Offset: 0x000CDE8C
		public virtual string TypeName
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).TypeName;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000CFC9E File Offset: 0x000CDE9E
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x000CFCB0 File Offset: 0x000CDEB0
		public string Uri
		{
			get
			{
				return ((IMethodReturnMessage)this.WrappedMessage).Uri;
			}
			set
			{
				this.Properties["__Uri"] = value;
			}
		}

		// Token: 0x06003B4D RID: 15181 RVA: 0x000CFCC3 File Offset: 0x000CDEC3
		public virtual object GetArg(int argNum)
		{
			return this._args[argNum];
		}

		// Token: 0x06003B4E RID: 15182 RVA: 0x000CFCCD File Offset: 0x000CDECD
		public virtual string GetArgName(int index)
		{
			return ((IMethodReturnMessage)this.WrappedMessage).GetArgName(index);
		}

		// Token: 0x06003B4F RID: 15183 RVA: 0x000CFCE0 File Offset: 0x000CDEE0
		public virtual object GetOutArg(int argNum)
		{
			return this._args[this._outArgInfo.GetInOutArgIndex(argNum)];
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x000CFCF5 File Offset: 0x000CDEF5
		public virtual string GetOutArgName(int index)
		{
			return this._outArgInfo.GetInOutArgName(index);
		}

		// Token: 0x04002641 RID: 9793
		private object[] _args;

		// Token: 0x04002642 RID: 9794
		private ArgInfo _outArgInfo;

		// Token: 0x04002643 RID: 9795
		private MethodReturnMessageWrapper.DictionaryWrapper _properties;

		// Token: 0x04002644 RID: 9796
		private Exception _exception;

		// Token: 0x04002645 RID: 9797
		private object _return;

		// Token: 0x020005FF RID: 1535
		private class DictionaryWrapper : MethodReturnDictionary
		{
			// Token: 0x06003B51 RID: 15185 RVA: 0x000CFD03 File Offset: 0x000CDF03
			public DictionaryWrapper(IMethodReturnMessage message, IDictionary wrappedDictionary)
				: base(message)
			{
				this._wrappedDictionary = wrappedDictionary;
				base.MethodKeys = MethodReturnMessageWrapper.DictionaryWrapper._keys;
			}

			// Token: 0x06003B52 RID: 15186 RVA: 0x000CFD1E File Offset: 0x000CDF1E
			protected override IDictionary AllocInternalProperties()
			{
				return this._wrappedDictionary;
			}

			// Token: 0x06003B53 RID: 15187 RVA: 0x000CFD28 File Offset: 0x000CDF28
			protected override void SetMethodProperty(string key, object value)
			{
				if (key == "__Args")
				{
					((MethodReturnMessageWrapper)this._message)._args = (object[])value;
					return;
				}
				if (key == "__Return")
				{
					((MethodReturnMessageWrapper)this._message)._return = value;
					return;
				}
				base.SetMethodProperty(key, value);
			}

			// Token: 0x06003B54 RID: 15188 RVA: 0x000CFD80 File Offset: 0x000CDF80
			protected override object GetMethodProperty(string key)
			{
				if (key == "__Args")
				{
					return ((MethodReturnMessageWrapper)this._message)._args;
				}
				if (key == "__Return")
				{
					return ((MethodReturnMessageWrapper)this._message)._return;
				}
				return base.GetMethodProperty(key);
			}

			// Token: 0x06003B55 RID: 15189 RVA: 0x000CFDD0 File Offset: 0x000CDFD0
			// Note: this type is marked as 'beforefieldinit'.
			static DictionaryWrapper()
			{
			}

			// Token: 0x04002646 RID: 9798
			private IDictionary _wrappedDictionary;

			// Token: 0x04002647 RID: 9799
			private static string[] _keys = new string[] { "__Args", "__Return" };
		}
	}
}
