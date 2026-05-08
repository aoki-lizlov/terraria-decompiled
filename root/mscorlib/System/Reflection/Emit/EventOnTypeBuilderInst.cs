using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x020008F2 RID: 2290
	[StructLayout(LayoutKind.Sequential)]
	internal class EventOnTypeBuilderInst : EventInfo
	{
		// Token: 0x06004F5E RID: 20318 RVA: 0x000FA27C File Offset: 0x000F847C
		internal EventOnTypeBuilderInst(TypeBuilderInstantiation instantiation, EventBuilder evt)
		{
			this.instantiation = instantiation;
			this.event_builder = evt;
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x000FA292 File Offset: 0x000F8492
		internal EventOnTypeBuilderInst(TypeBuilderInstantiation instantiation, EventInfo evt)
		{
			this.instantiation = instantiation;
			this.event_info = evt;
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004F60 RID: 20320 RVA: 0x000FA2A8 File Offset: 0x000F84A8
		public override EventAttributes Attributes
		{
			get
			{
				if (this.event_builder == null)
				{
					return this.event_info.Attributes;
				}
				return this.event_builder.attrs;
			}
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x000FA2CC File Offset: 0x000F84CC
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.add_method : this.event_info.GetAddMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06004F62 RID: 20322 RVA: 0x000FA320 File Offset: 0x000F8520
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.raise_method : this.event_info.GetRaiseMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06004F63 RID: 20323 RVA: 0x000FA374 File Offset: 0x000F8574
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MethodInfo methodInfo = ((this.event_builder != null) ? this.event_builder.remove_method : this.event_info.GetRemoveMethod(nonPublic));
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06004F64 RID: 20324 RVA: 0x000FA3C8 File Offset: 0x000F85C8
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MethodInfo[] array;
			if (this.event_builder == null)
			{
				array = this.event_info.GetOtherMethods(nonPublic);
			}
			else
			{
				MethodInfo[] array2 = this.event_builder.other_methods;
				array = array2;
			}
			MethodInfo[] array3 = array;
			if (array3 == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (MethodInfo methodInfo in array3)
			{
				if (nonPublic || methodInfo.IsPublic)
				{
					arrayList.Add(TypeBuilder.GetMethod(this.instantiation, methodInfo));
				}
			}
			MethodInfo[] array4 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array4, 0);
			return array4;
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06004F65 RID: 20325 RVA: 0x000FA458 File Offset: 0x000F8658
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06004F66 RID: 20326 RVA: 0x000FA460 File Offset: 0x000F8660
		public override string Name
		{
			get
			{
				if (this.event_builder == null)
				{
					return this.event_info.Name;
				}
				return this.event_builder.name;
			}
		}

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06004F67 RID: 20327 RVA: 0x000FA458 File Offset: 0x000F8658
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06004F68 RID: 20328 RVA: 0x00047E00 File Offset: 0x00046000
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x00047E00 File Offset: 0x00046000
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06004F6A RID: 20330 RVA: 0x00047E00 File Offset: 0x00046000
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x040030D7 RID: 12503
		private TypeBuilderInstantiation instantiation;

		// Token: 0x040030D8 RID: 12504
		private EventBuilder event_builder;

		// Token: 0x040030D9 RID: 12505
		private EventInfo event_info;
	}
}
