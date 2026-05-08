using System;
using System.Reflection;

namespace MonoGame.Utilities
{
	// Token: 0x02000008 RID: 8
	internal static class AssemblyHelper
	{
		// Token: 0x060008C8 RID: 2248 RVA: 0x00005020 File Offset: 0x00003220
		public static string GetDefaultWindowTitle()
		{
			string text = string.Empty;
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			if (entryAssembly != null)
			{
				try
				{
					AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(entryAssembly, typeof(AssemblyTitleAttribute));
					if (assemblyTitleAttribute != null)
					{
						text = assemblyTitleAttribute.Title;
					}
				}
				catch
				{
				}
				if (string.IsNullOrEmpty(text))
				{
					text = entryAssembly.GetName().Name;
				}
			}
			return text;
		}
	}
}
