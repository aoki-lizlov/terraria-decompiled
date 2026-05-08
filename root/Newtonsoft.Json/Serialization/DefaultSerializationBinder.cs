using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008E RID: 142
	public class DefaultSerializationBinder : SerializationBinder, ISerializationBinder
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x0001B500 File Offset: 0x00019700
		public DefaultSerializationBinder()
		{
			this._typeCache = new ThreadSafeStore<TypeNameKey, Type>(new Func<TypeNameKey, Type>(this.GetTypeFromTypeNameKey));
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001B520 File Offset: 0x00019720
		private Type GetTypeFromTypeNameKey(TypeNameKey typeNameKey)
		{
			string assemblyName = typeNameKey.AssemblyName;
			string typeName = typeNameKey.TypeName;
			if (assemblyName == null)
			{
				return Type.GetType(typeName);
			}
			Assembly assembly = Assembly.LoadWithPartialName(assemblyName);
			if (assembly == null)
			{
				foreach (Assembly assembly2 in AppDomain.CurrentDomain.GetAssemblies())
				{
					if (assembly2.FullName == assemblyName || assembly2.GetName().Name == assemblyName)
					{
						assembly = assembly2;
						break;
					}
				}
			}
			if (assembly == null)
			{
				throw new JsonSerializationException("Could not load assembly '{0}'.".FormatWith(CultureInfo.InvariantCulture, assemblyName));
			}
			Type type = assembly.GetType(typeName);
			if (type == null)
			{
				if (typeName.IndexOf('`') >= 0)
				{
					try
					{
						type = this.GetGenericTypeFromTypeName(typeName, assembly);
					}
					catch (Exception ex)
					{
						throw new JsonSerializationException("Could not find type '{0}' in assembly '{1}'.".FormatWith(CultureInfo.InvariantCulture, typeName, assembly.FullName), ex);
					}
				}
				if (type == null)
				{
					throw new JsonSerializationException("Could not find type '{0}' in assembly '{1}'.".FormatWith(CultureInfo.InvariantCulture, typeName, assembly.FullName));
				}
			}
			return type;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001B644 File Offset: 0x00019844
		private Type GetGenericTypeFromTypeName(string typeName, Assembly assembly)
		{
			Type type = null;
			int num = typeName.IndexOf('[');
			if (num >= 0)
			{
				string text = typeName.Substring(0, num);
				Type type2 = assembly.GetType(text);
				if (type2 != null)
				{
					List<Type> list = new List<Type>();
					int num2 = 0;
					int num3 = 0;
					int num4 = typeName.Length - 1;
					for (int i = num + 1; i < num4; i++)
					{
						char c = typeName.get_Chars(i);
						if (c != '[')
						{
							if (c == ']')
							{
								num2--;
								if (num2 == 0)
								{
									TypeNameKey typeNameKey = ReflectionUtils.SplitFullyQualifiedTypeName(typeName.Substring(num3, i - num3));
									list.Add(this.GetTypeByName(typeNameKey));
								}
							}
						}
						else
						{
							if (num2 == 0)
							{
								num3 = i + 1;
							}
							num2++;
						}
					}
					type = type2.MakeGenericType(list.ToArray());
				}
			}
			return type;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0001B710 File Offset: 0x00019910
		private Type GetTypeByName(TypeNameKey typeNameKey)
		{
			return this._typeCache.Get(typeNameKey);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0001B71E File Offset: 0x0001991E
		public override Type BindToType(string assemblyName, string typeName)
		{
			return this.GetTypeByName(new TypeNameKey(assemblyName, typeName));
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0001B72D File Offset: 0x0001992D
		public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = serializedType.Assembly.FullName;
			typeName = serializedType.FullName;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0001B744 File Offset: 0x00019944
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultSerializationBinder()
		{
		}

		// Token: 0x0400029F RID: 671
		internal static readonly DefaultSerializationBinder Instance = new DefaultSerializationBinder();

		// Token: 0x040002A0 RID: 672
		private readonly ThreadSafeStore<TypeNameKey, Type> _typeCache;
	}
}
