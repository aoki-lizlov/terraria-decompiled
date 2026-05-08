using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000021 RID: 33
	public class GameServiceContainer : IServiceProvider
	{
		// Token: 0x06000BC9 RID: 3017 RVA: 0x00013570 File Offset: 0x00011770
		public GameServiceContainer()
		{
			this.services = new Dictionary<Type, object>();
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00013584 File Offset: 0x00011784
		public void AddService(Type type, object provider)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (!type.IsAssignableFrom(provider.GetType()))
			{
				throw new ArgumentException("The provider does not match the specified service type!");
			}
			this.services.Add(type, provider);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000135DC File Offset: 0x000117DC
		public object GetService(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj;
			if (this.services.TryGetValue(type, out obj))
			{
				return obj;
			}
			return null;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00013610 File Offset: 0x00011810
		public void RemoveService(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.services.Remove(type);
		}

		// Token: 0x04000562 RID: 1378
		private Dictionary<Type, object> services;
	}
}
