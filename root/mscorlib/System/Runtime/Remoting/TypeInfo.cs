using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000548 RID: 1352
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x0600368D RID: 13965 RVA: 0x000C604C File Offset: 0x000C424C
		public TypeInfo(Type type)
		{
			if (type.IsInterface)
			{
				this.serverType = typeof(MarshalByRefObject).AssemblyQualifiedName;
				this.serverHierarchy = new string[0];
				Type[] interfaces = type.GetInterfaces();
				this.interfacesImplemented = new string[interfaces.Length + 1];
				for (int i = 0; i < interfaces.Length; i++)
				{
					this.interfacesImplemented[i] = interfaces[i].AssemblyQualifiedName;
				}
				this.interfacesImplemented[interfaces.Length] = type.AssemblyQualifiedName;
				return;
			}
			this.serverType = type.AssemblyQualifiedName;
			int num = 0;
			Type type2 = type.BaseType;
			while (type2 != typeof(MarshalByRefObject) && type2 != null)
			{
				type2 = type2.BaseType;
				num++;
			}
			this.serverHierarchy = new string[num];
			type2 = type.BaseType;
			for (int j = 0; j < num; j++)
			{
				this.serverHierarchy[j] = type2.AssemblyQualifiedName;
				type2 = type2.BaseType;
			}
			Type[] interfaces2 = type.GetInterfaces();
			this.interfacesImplemented = new string[interfaces2.Length];
			for (int k = 0; k < interfaces2.Length; k++)
			{
				this.interfacesImplemented[k] = interfaces2[k].AssemblyQualifiedName;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600368E RID: 13966 RVA: 0x000C6181 File Offset: 0x000C4381
		// (set) Token: 0x0600368F RID: 13967 RVA: 0x000C6189 File Offset: 0x000C4389
		public string TypeName
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000C6194 File Offset: 0x000C4394
		public bool CanCastTo(Type fromType, object o)
		{
			if (fromType == typeof(object))
			{
				return true;
			}
			if (fromType == typeof(MarshalByRefObject))
			{
				return true;
			}
			string text = fromType.AssemblyQualifiedName;
			int num = text.IndexOf(',');
			if (num != -1)
			{
				num = text.IndexOf(',', num + 1);
			}
			if (num != -1)
			{
				text = text.Substring(0, num + 1);
			}
			else
			{
				text += ",";
			}
			if ((this.serverType + ",").StartsWith(text))
			{
				return true;
			}
			if (this.serverHierarchy != null)
			{
				string[] array = this.serverHierarchy;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			if (this.interfacesImplemented != null)
			{
				string[] array = this.interfacesImplemented;
				for (int i = 0; i < array.Length; i++)
				{
					if ((array[i] + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040024EF RID: 9455
		private string serverType;

		// Token: 0x040024F0 RID: 9456
		private string[] serverHierarchy;

		// Token: 0x040024F1 RID: 9457
		private string[] interfacesImplemented;
	}
}
