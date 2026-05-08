using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000083 RID: 131
	public class JsonDynamicContract : JsonContainerContract
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00019036 File Offset: 0x00017236
		public JsonPropertyCollection Properties
		{
			[CompilerGenerated]
			get
			{
				return this.<Properties>k__BackingField;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0001903E File Offset: 0x0001723E
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00019046 File Offset: 0x00017246
		public Func<string, string> PropertyNameResolver
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyNameResolver>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PropertyNameResolver>k__BackingField = value;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001904F File Offset: 0x0001724F
		private static CallSite<Func<CallSite, object, object>> CreateCallSiteGetter(string name)
		{
			return CallSite<Func<CallSite, object, object>>.Create(new NoThrowGetBinderMember((GetMemberBinder)DynamicUtils.BinderWrapper.GetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00019070 File Offset: 0x00017270
		private static CallSite<Func<CallSite, object, object, object>> CreateCallSiteSetter(string name)
		{
			return CallSite<Func<CallSite, object, object, object>>.Create(new NoThrowSetBinderMember((SetMemberBinder)DynamicUtils.BinderWrapper.SetMember(name, typeof(DynamicUtils))));
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00019094 File Offset: 0x00017294
		public JsonDynamicContract(Type underlyingType)
			: base(underlyingType)
		{
			this.ContractType = JsonContractType.Dynamic;
			this.Properties = new JsonPropertyCollection(base.UnderlyingType);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000190F0 File Offset: 0x000172F0
		internal bool TryGetMember(IDynamicMetaObjectProvider dynamicProvider, string name, out object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object>> callSite = this._callSiteGetters.Get(name);
			object obj = callSite.Target.Invoke(callSite, dynamicProvider);
			if (obj != NoThrowExpressionVisitor.ErrorResult)
			{
				value = obj;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00019134 File Offset: 0x00017334
		internal bool TrySetMember(IDynamicMetaObjectProvider dynamicProvider, string name, object value)
		{
			ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");
			CallSite<Func<CallSite, object, object, object>> callSite = this._callSiteSetters.Get(name);
			return callSite.Target.Invoke(callSite, dynamicProvider, value) != NoThrowExpressionVisitor.ErrorResult;
		}

		// Token: 0x04000280 RID: 640
		[CompilerGenerated]
		private readonly JsonPropertyCollection <Properties>k__BackingField;

		// Token: 0x04000281 RID: 641
		[CompilerGenerated]
		private Func<string, string> <PropertyNameResolver>k__BackingField;

		// Token: 0x04000282 RID: 642
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>> _callSiteGetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object>>>(JsonDynamicContract.CreateCallSiteGetter));

		// Token: 0x04000283 RID: 643
		private readonly ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>> _callSiteSetters = new ThreadSafeStore<string, CallSite<Func<CallSite, object, object, object>>>(new Func<string, CallSite<Func<CallSite, object, object, object>>>(JsonDynamicContract.CreateCallSiteSetter));
	}
}
