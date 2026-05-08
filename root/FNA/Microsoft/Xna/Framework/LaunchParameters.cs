using System;
using System.Collections.Generic;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200002A RID: 42
	public class LaunchParameters : Dictionary<string, string>
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x00014340 File Offset: 0x00012540
		public LaunchParameters()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 0; i < commandLineArgs.Length; i++)
			{
				string text = commandLineArgs[i].TrimStart(LaunchParameters.flags);
				if (text.Length >= 3)
				{
					int num = text.IndexOf(":", 1, text.Length - 2);
					if (num >= 0)
					{
						string text2 = text.Substring(0, num);
						if (!base.ContainsKey(text2))
						{
							base.Add(text2, text.Substring(num + 1));
						}
					}
				}
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000143BC File Offset: 0x000125BC
		// Note: this type is marked as 'beforefieldinit'.
		static LaunchParameters()
		{
		}

		// Token: 0x04000587 RID: 1415
		private static readonly char[] flags = new char[] { '/', '-' };
	}
}
