using System;

namespace System.Globalization
{
	// Token: 0x020009D8 RID: 2520
	internal class CalendricalCalculationsHelper
	{
		// Token: 0x06005BBF RID: 23487 RVA: 0x0013A278 File Offset: 0x00138478
		private static double RadiansFromDegrees(double degree)
		{
			return degree * 3.141592653589793 / 180.0;
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x0013A28F File Offset: 0x0013848F
		private static double SinOfDegree(double degree)
		{
			return Math.Sin(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x0013A29C File Offset: 0x0013849C
		private static double CosOfDegree(double degree)
		{
			return Math.Cos(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x0013A2A9 File Offset: 0x001384A9
		private static double TanOfDegree(double degree)
		{
			return Math.Tan(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x0013A2B6 File Offset: 0x001384B6
		public static double Angle(int degrees, int minutes, double seconds)
		{
			return (seconds / 60.0 + (double)minutes) / 60.0 + (double)degrees;
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x0013A2D3 File Offset: 0x001384D3
		private static double Obliquity(double julianCenturies)
		{
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients, julianCenturies);
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x0013A2E0 File Offset: 0x001384E0
		internal static long GetNumberOfDays(DateTime date)
		{
			return date.Ticks / 864000000000L;
		}

		// Token: 0x06005BC6 RID: 23494 RVA: 0x0013A2F4 File Offset: 0x001384F4
		private static int GetGregorianYear(double numberOfDays)
		{
			return new DateTime(Math.Min((long)(Math.Floor(numberOfDays) * 864000000000.0), DateTime.MaxValue.Ticks)).Year;
		}

		// Token: 0x06005BC7 RID: 23495 RVA: 0x0013A330 File Offset: 0x00138530
		private static double Reminder(double divisor, double dividend)
		{
			double num = Math.Floor(divisor / dividend);
			return divisor - dividend * num;
		}

		// Token: 0x06005BC8 RID: 23496 RVA: 0x0013A34B File Offset: 0x0013854B
		private static double NormalizeLongitude(double longitude)
		{
			longitude = CalendricalCalculationsHelper.Reminder(longitude, 360.0);
			if (longitude < 0.0)
			{
				longitude += 360.0;
			}
			return longitude;
		}

		// Token: 0x06005BC9 RID: 23497 RVA: 0x0013A378 File Offset: 0x00138578
		public static double AsDayFraction(double longitude)
		{
			return longitude / 360.0;
		}

		// Token: 0x06005BCA RID: 23498 RVA: 0x0013A388 File Offset: 0x00138588
		private static double PolynomialSum(double[] coefficients, double indeterminate)
		{
			double num = coefficients[0];
			double num2 = 1.0;
			for (int i = 1; i < coefficients.Length; i++)
			{
				num2 *= indeterminate;
				num += coefficients[i] * num2;
			}
			return num;
		}

		// Token: 0x06005BCB RID: 23499 RVA: 0x0013A3BE File Offset: 0x001385BE
		private static double CenturiesFrom1900(int gregorianYear)
		{
			return (double)(CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 7, 1)) - CalendricalCalculationsHelper.StartOf1900Century) / 36525.0;
		}

		// Token: 0x06005BCC RID: 23500 RVA: 0x0013A3E0 File Offset: 0x001385E0
		private static double DefaultEphemerisCorrection(int gregorianYear)
		{
			double num = (double)(CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 1, 1)) - CalendricalCalculationsHelper.StartOf1810);
			return (Math.Pow(0.5 + num, 2.0) / 41048480.0 - 15.0) / 86400.0;
		}

		// Token: 0x06005BCD RID: 23501 RVA: 0x0013A439 File Offset: 0x00138639
		private static double EphemerisCorrection1988to2019(int gregorianYear)
		{
			return (double)(gregorianYear - 1933) / 86400.0;
		}

		// Token: 0x06005BCE RID: 23502 RVA: 0x0013A450 File Offset: 0x00138650
		private static double EphemerisCorrection1900to1987(int gregorianYear)
		{
			double num = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1900to1987, num);
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x0013A470 File Offset: 0x00138670
		private static double EphemerisCorrection1800to1899(int gregorianYear)
		{
			double num = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1800to1899, num);
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x0013A490 File Offset: 0x00138690
		private static double EphemerisCorrection1700to1799(int gregorianYear)
		{
			double num = (double)(gregorianYear - 1700);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1700to1799, num) / 86400.0;
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x0013A4BC File Offset: 0x001386BC
		private static double EphemerisCorrection1620to1699(int gregorianYear)
		{
			double num = (double)(gregorianYear - 1600);
			return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1620to1699, num) / 86400.0;
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x0013A4E8 File Offset: 0x001386E8
		private static double EphemerisCorrection(double time)
		{
			int gregorianYear = CalendricalCalculationsHelper.GetGregorianYear(time);
			CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] ephemerisCorrectionTable = CalendricalCalculationsHelper.EphemerisCorrectionTable;
			int i = 0;
			while (i < ephemerisCorrectionTable.Length)
			{
				CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap ephemerisCorrectionAlgorithmMap = ephemerisCorrectionTable[i];
				if (ephemerisCorrectionAlgorithmMap._lowestYear <= gregorianYear)
				{
					switch (ephemerisCorrectionAlgorithmMap._algorithm)
					{
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Default:
						return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019:
						return CalendricalCalculationsHelper.EphemerisCorrection1988to2019(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987:
						return CalendricalCalculationsHelper.EphemerisCorrection1900to1987(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899:
						return CalendricalCalculationsHelper.EphemerisCorrection1800to1899(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799:
						return CalendricalCalculationsHelper.EphemerisCorrection1700to1799(gregorianYear);
					case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699:
						return CalendricalCalculationsHelper.EphemerisCorrection1620to1699(gregorianYear);
					default:
						goto IL_007F;
					}
				}
				else
				{
					i++;
				}
			}
			IL_007F:
			return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x0013A57A File Offset: 0x0013877A
		public static double JulianCenturies(double moment)
		{
			return (moment + CalendricalCalculationsHelper.EphemerisCorrection(moment) - 730120.5) / 36525.0;
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x0013A598 File Offset: 0x00138798
		private static bool IsNegative(double value)
		{
			return Math.Sign(value) == -1;
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x0013A5A3 File Offset: 0x001387A3
		private static double CopySign(double value, double sign)
		{
			if (CalendricalCalculationsHelper.IsNegative(value) != CalendricalCalculationsHelper.IsNegative(sign))
			{
				return -value;
			}
			return value;
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x0013A5B8 File Offset: 0x001387B8
		private static double EquationOfTime(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.LambdaCoefficients, num);
			double num3 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.AnomalyCoefficients, num);
			double num4 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.EccentricityCoefficients, num);
			double num5 = CalendricalCalculationsHelper.TanOfDegree(CalendricalCalculationsHelper.Obliquity(num) / 2.0);
			double num6 = num5 * num5;
			double num7 = num6 * CalendricalCalculationsHelper.SinOfDegree(2.0 * num2) - 2.0 * num4 * CalendricalCalculationsHelper.SinOfDegree(num3) + 4.0 * num4 * num6 * CalendricalCalculationsHelper.SinOfDegree(num3) * CalendricalCalculationsHelper.CosOfDegree(2.0 * num2) - 0.5 * Math.Pow(num6, 2.0) * CalendricalCalculationsHelper.SinOfDegree(4.0 * num2) - 1.25 * Math.Pow(num4, 2.0) * CalendricalCalculationsHelper.SinOfDegree(2.0 * num3);
			double num8 = 6.283185307179586;
			double num9 = num7 / num8;
			return CalendricalCalculationsHelper.CopySign(Math.Min(Math.Abs(num9), 0.5), num9);
		}

		// Token: 0x06005BD7 RID: 23511 RVA: 0x0013A6DC File Offset: 0x001388DC
		private static double AsLocalTime(double apparentMidday, double longitude)
		{
			double num = apparentMidday - CalendricalCalculationsHelper.AsDayFraction(longitude);
			return apparentMidday - CalendricalCalculationsHelper.EquationOfTime(num);
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x0013A6FA File Offset: 0x001388FA
		public static double Midday(double date, double longitude)
		{
			return CalendricalCalculationsHelper.AsLocalTime(date + 0.5, longitude) - CalendricalCalculationsHelper.AsDayFraction(longitude);
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x0013A714 File Offset: 0x00138914
		private static double InitLongitude(double longitude)
		{
			return CalendricalCalculationsHelper.NormalizeLongitude(longitude + 180.0) - 180.0;
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x0013A730 File Offset: 0x00138930
		public static double MiddayAtPersianObservationSite(double date)
		{
			return CalendricalCalculationsHelper.Midday(date, CalendricalCalculationsHelper.InitLongitude(52.5));
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x0013A746 File Offset: 0x00138946
		private static double PeriodicTerm(double julianCenturies, int x, double y, double z)
		{
			return (double)x * CalendricalCalculationsHelper.SinOfDegree(y + z * julianCenturies);
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x0013A758 File Offset: 0x00138958
		private static double SumLongSequenceOfPeriodicTerms(double julianCenturies)
		{
			return 0.0 + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 403406, 270.54861, 0.9287892) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 195207, 340.19128, 35999.1376958) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 119433, 63.91854, 35999.4089666) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 112392, 331.2622, 35998.7287385) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 3891, 317.843, 71998.20261) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 2819, 86.631, 71998.4403) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 1721, 240.052, 36000.35726) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 660, 310.26, 71997.4812) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 350, 247.23, 32964.4678) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 334, 260.87, -19.441) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 314, 297.82, 445267.1117) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 268, 343.14, 45036.884) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 242, 166.79, 3.1008) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 234, 81.53, 22518.4434) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 158, 3.5, -19.9739) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 132, 132.75, 65928.9345) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 129, 182.95, 9038.0293) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 114, 162.03, 3034.7684) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 99, 29.8, 33718.148) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 93, 266.4, 3034.448) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 86, 249.2, -2280.773) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 78, 157.6, 29929.992) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 72, 257.8, 31556.493) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 68, 185.1, 149.588) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 64, 69.9, 9037.75) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 46, 8.0, 107997.405) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 38, 197.1, -4444.176) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 37, 250.4, 151.771) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 32, 65.3, 67555.316) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 29, 162.7, 31556.08) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 28, 341.5, -4561.54) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 291.6, 107996.706) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 98.5, 1221.655) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 25, 146.7, 62894.167) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 24, 110.0, 31437.369) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 5.2, 14578.298) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 342.6, -31931.757) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 20, 230.9, 34777.243) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 18, 256.1, 1221.999) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 17, 45.3, 62894.511) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 14, 242.9, -4442.039) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 115.2, 107997.909) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 151.8, 119.066) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 285.3, 16859.071) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 12, 53.3, -4.578) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 126.6, 26895.292) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 205.7, -39.127) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 85.9, 12297.536) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 146.1, 90073.778);
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x0013ACCC File Offset: 0x00138ECC
		private static double Aberration(double julianCenturies)
		{
			return 9.74E-05 * CalendricalCalculationsHelper.CosOfDegree(177.63 + 35999.01848 * julianCenturies) - 0.005575;
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x0013ACFC File Offset: 0x00138EFC
		private static double Nutation(double julianCenturies)
		{
			double num = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsA, julianCenturies);
			double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsB, julianCenturies);
			return -0.004778 * CalendricalCalculationsHelper.SinOfDegree(num) - 0.0003667 * CalendricalCalculationsHelper.SinOfDegree(num2);
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x0013AD44 File Offset: 0x00138F44
		public static double Compute(double time)
		{
			double num = CalendricalCalculationsHelper.JulianCenturies(time);
			return CalendricalCalculationsHelper.InitLongitude(282.7771834 + 36000.76953744 * num + 5.729577951308232E-06 * CalendricalCalculationsHelper.SumLongSequenceOfPeriodicTerms(num) + CalendricalCalculationsHelper.Aberration(num) + CalendricalCalculationsHelper.Nutation(num));
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x0013AD91 File Offset: 0x00138F91
		public static double AsSeason(double longitude)
		{
			if (longitude >= 0.0)
			{
				return longitude;
			}
			return longitude + 360.0;
		}

		// Token: 0x06005BE1 RID: 23521 RVA: 0x0013ADAC File Offset: 0x00138FAC
		private static double EstimatePrior(double longitude, double time)
		{
			double num = time - 1.0145616361111112 * CalendricalCalculationsHelper.AsSeason(CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(time) - longitude));
			double num2 = CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(num) - longitude);
			return Math.Min(time, num - 1.0145616361111112 * num2);
		}

		// Token: 0x06005BE2 RID: 23522 RVA: 0x0013ADFC File Offset: 0x00138FFC
		internal static long PersianNewYearOnOrBefore(long numberOfDays)
		{
			double num = (double)numberOfDays;
			long num2 = (long)Math.Floor(CalendricalCalculationsHelper.EstimatePrior(0.0, CalendricalCalculationsHelper.MiddayAtPersianObservationSite(num))) - 1L;
			long num3 = num2 + 3L;
			long num4;
			for (num4 = num2; num4 != num3; num4 += 1L)
			{
				double num5 = CalendricalCalculationsHelper.Compute(CalendricalCalculationsHelper.MiddayAtPersianObservationSite((double)num4));
				if (0.0 <= num5 && num5 <= 2.0)
				{
					break;
				}
			}
			return num4 - 1L;
		}

		// Token: 0x06005BE3 RID: 23523 RVA: 0x000025BE File Offset: 0x000007BE
		public CalendricalCalculationsHelper()
		{
		}

		// Token: 0x06005BE4 RID: 23524 RVA: 0x0013AE64 File Offset: 0x00139064
		// Note: this type is marked as 'beforefieldinit'.
		static CalendricalCalculationsHelper()
		{
		}

		// Token: 0x04003783 RID: 14211
		private const double FullCircleOfArc = 360.0;

		// Token: 0x04003784 RID: 14212
		private const int HalfCircleOfArc = 180;

		// Token: 0x04003785 RID: 14213
		private const double TwelveHours = 0.5;

		// Token: 0x04003786 RID: 14214
		private const double Noon2000Jan01 = 730120.5;

		// Token: 0x04003787 RID: 14215
		internal const double MeanTropicalYearInDays = 365.242189;

		// Token: 0x04003788 RID: 14216
		private const double MeanSpeedOfSun = 1.0145616361111112;

		// Token: 0x04003789 RID: 14217
		private const double LongitudeSpring = 0.0;

		// Token: 0x0400378A RID: 14218
		private const double TwoDegreesAfterSpring = 2.0;

		// Token: 0x0400378B RID: 14219
		private const int SecondsPerDay = 86400;

		// Token: 0x0400378C RID: 14220
		private const int DaysInUniformLengthCentury = 36525;

		// Token: 0x0400378D RID: 14221
		private const int SecondsPerMinute = 60;

		// Token: 0x0400378E RID: 14222
		private const int MinutesPerDegree = 60;

		// Token: 0x0400378F RID: 14223
		private static long StartOf1810 = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1810, 1, 1));

		// Token: 0x04003790 RID: 14224
		private static long StartOf1900Century = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1900, 1, 1));

		// Token: 0x04003791 RID: 14225
		private static double[] Coefficients1900to1987 = new double[] { -2E-05, 0.000297, 0.025184, -0.181133, 0.55304, -0.861938, 0.677066, -0.212591 };

		// Token: 0x04003792 RID: 14226
		private static double[] Coefficients1800to1899 = new double[]
		{
			-9E-06, 0.003844, 0.083563, 0.865736, 4.867575, 15.845535, 31.332267, 38.291999, 28.316289, 11.636204,
			2.043794
		};

		// Token: 0x04003793 RID: 14227
		private static double[] Coefficients1700to1799 = new double[] { 8.118780842, -0.005092142, 0.003336121, -2.66484E-05 };

		// Token: 0x04003794 RID: 14228
		private static double[] Coefficients1620to1699 = new double[] { 196.58333, -4.0675, 0.0219167 };

		// Token: 0x04003795 RID: 14229
		private static double[] LambdaCoefficients = new double[] { 280.46645, 36000.76983, 0.0003032 };

		// Token: 0x04003796 RID: 14230
		private static double[] AnomalyCoefficients = new double[] { 357.5291, 35999.0503, -0.0001559, -4.8E-07 };

		// Token: 0x04003797 RID: 14231
		private static double[] EccentricityCoefficients = new double[] { 0.016708617, -4.2037E-05, -1.236E-07 };

		// Token: 0x04003798 RID: 14232
		private static double[] Coefficients = new double[]
		{
			CalendricalCalculationsHelper.Angle(23, 26, 21.448),
			CalendricalCalculationsHelper.Angle(0, 0, -46.815),
			CalendricalCalculationsHelper.Angle(0, 0, -0.00059),
			CalendricalCalculationsHelper.Angle(0, 0, 0.001813)
		};

		// Token: 0x04003799 RID: 14233
		private static double[] CoefficientsA = new double[] { 124.9, -1934.134, 0.002063 };

		// Token: 0x0400379A RID: 14234
		private static double[] CoefficientsB = new double[] { 201.11, 72001.5377, 0.00057 };

		// Token: 0x0400379B RID: 14235
		private static CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] EphemerisCorrectionTable = new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[]
		{
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(2020, CalendricalCalculationsHelper.CorrectionAlgorithm.Default),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1988, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1900, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1800, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1700, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1620, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699),
			new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(int.MinValue, CalendricalCalculationsHelper.CorrectionAlgorithm.Default)
		};

		// Token: 0x020009D9 RID: 2521
		private enum CorrectionAlgorithm
		{
			// Token: 0x0400379D RID: 14237
			Default,
			// Token: 0x0400379E RID: 14238
			Year1988to2019,
			// Token: 0x0400379F RID: 14239
			Year1900to1987,
			// Token: 0x040037A0 RID: 14240
			Year1800to1899,
			// Token: 0x040037A1 RID: 14241
			Year1700to1799,
			// Token: 0x040037A2 RID: 14242
			Year1620to1699
		}

		// Token: 0x020009DA RID: 2522
		private struct EphemerisCorrectionAlgorithmMap
		{
			// Token: 0x06005BE5 RID: 23525 RVA: 0x0013B046 File Offset: 0x00139246
			public EphemerisCorrectionAlgorithmMap(int year, CalendricalCalculationsHelper.CorrectionAlgorithm algorithm)
			{
				this._lowestYear = year;
				this._algorithm = algorithm;
			}

			// Token: 0x040037A3 RID: 14243
			internal int _lowestYear;

			// Token: 0x040037A4 RID: 14244
			internal CalendricalCalculationsHelper.CorrectionAlgorithm _algorithm;
		}
	}
}
