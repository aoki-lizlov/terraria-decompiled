using System;

namespace Terraria.WorldBuilding
{
	// Token: 0x020000A9 RID: 169
	public class GenerationProgress
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x004DDEFC File Offset: 0x004DC0FC
		// (set) Token: 0x0600174A RID: 5962 RVA: 0x004DDF14 File Offset: 0x004DC114
		public string Message
		{
			get
			{
				return string.Format(this._message, this.Value);
			}
			set
			{
				this._message = value.Replace("%", "{0:0.0%}");
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x004DDF2C File Offset: 0x004DC12C
		// (set) Token: 0x0600174C RID: 5964 RVA: 0x004DDF34 File Offset: 0x004DC134
		public string MessageNoFormatting
		{
			get
			{
				return this._message;
			}
			set
			{
				this._message = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x004DDF5D File Offset: 0x004DC15D
		// (set) Token: 0x0600174D RID: 5965 RVA: 0x004DDF3D File Offset: 0x004DC13D
		public double Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = Utils.Clamp<double>(value, 0.0, 1.0);
			}
		}

		// Token: 0x17000289 RID: 649
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x004DDF65 File Offset: 0x004DC165
		public double TotalWeightedProgress
		{
			set
			{
				this._totalWeightedProgress = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x004DDF6E File Offset: 0x004DC16E
		public double TotalProgress
		{
			get
			{
				if (this.TotalWeight == 0.0)
				{
					return 0.0;
				}
				return (this.Value * this.CurrentPassWeight + this._totalWeightedProgress) / this.TotalWeight;
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x004DDFA6 File Offset: 0x004DC1A6
		public void Set(double value)
		{
			this.Value = value;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x004DDFAF File Offset: 0x004DC1AF
		public void Set(double value, double min, double max)
		{
			this.Value = min + value * (max - min);
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x004DDFBE File Offset: 0x004DC1BE
		public void Start(double weight)
		{
			this.CurrentPassWeight = weight;
			this._value = 0.0;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x004DDFD6 File Offset: 0x004DC1D6
		public void End()
		{
			this._totalWeightedProgress += this.CurrentPassWeight;
			this._value = 0.0;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x004DDFFA File Offset: 0x004DC1FA
		public GenerationProgress()
		{
		}

		// Token: 0x040011DE RID: 4574
		private string _message = "";

		// Token: 0x040011DF RID: 4575
		private double _value;

		// Token: 0x040011E0 RID: 4576
		private double _totalWeightedProgress;

		// Token: 0x040011E1 RID: 4577
		public double TotalWeight;

		// Token: 0x040011E2 RID: 4578
		public double CurrentPassWeight = 1.0;
	}
}
