using System;

namespace ReLogic.Peripherals.RGB.Corsair
{
	// Token: 0x02000056 RID: 86
	internal enum CorsairLedId
	{
		// Token: 0x04000204 RID: 516
		CLI_Invalid,
		// Token: 0x04000205 RID: 517
		CLK_Escape,
		// Token: 0x04000206 RID: 518
		CLK_F1,
		// Token: 0x04000207 RID: 519
		CLK_F2,
		// Token: 0x04000208 RID: 520
		CLK_F3,
		// Token: 0x04000209 RID: 521
		CLK_F4,
		// Token: 0x0400020A RID: 522
		CLK_F5,
		// Token: 0x0400020B RID: 523
		CLK_F6,
		// Token: 0x0400020C RID: 524
		CLK_F7,
		// Token: 0x0400020D RID: 525
		CLK_F8,
		// Token: 0x0400020E RID: 526
		CLK_F9,
		// Token: 0x0400020F RID: 527
		CLK_F10,
		// Token: 0x04000210 RID: 528
		CLK_F11,
		// Token: 0x04000211 RID: 529
		CLK_GraveAccentAndTilde,
		// Token: 0x04000212 RID: 530
		CLK_1,
		// Token: 0x04000213 RID: 531
		CLK_2,
		// Token: 0x04000214 RID: 532
		CLK_3,
		// Token: 0x04000215 RID: 533
		CLK_4,
		// Token: 0x04000216 RID: 534
		CLK_5,
		// Token: 0x04000217 RID: 535
		CLK_6,
		// Token: 0x04000218 RID: 536
		CLK_7,
		// Token: 0x04000219 RID: 537
		CLK_8,
		// Token: 0x0400021A RID: 538
		CLK_9,
		// Token: 0x0400021B RID: 539
		CLK_0,
		// Token: 0x0400021C RID: 540
		CLK_MinusAndUnderscore,
		// Token: 0x0400021D RID: 541
		CLK_Tab,
		// Token: 0x0400021E RID: 542
		CLK_Q,
		// Token: 0x0400021F RID: 543
		CLK_W,
		// Token: 0x04000220 RID: 544
		CLK_E,
		// Token: 0x04000221 RID: 545
		CLK_R,
		// Token: 0x04000222 RID: 546
		CLK_T,
		// Token: 0x04000223 RID: 547
		CLK_Y,
		// Token: 0x04000224 RID: 548
		CLK_U,
		// Token: 0x04000225 RID: 549
		CLK_I,
		// Token: 0x04000226 RID: 550
		CLK_O,
		// Token: 0x04000227 RID: 551
		CLK_P,
		// Token: 0x04000228 RID: 552
		CLK_BracketLeft,
		// Token: 0x04000229 RID: 553
		CLK_CapsLock,
		// Token: 0x0400022A RID: 554
		CLK_A,
		// Token: 0x0400022B RID: 555
		CLK_S,
		// Token: 0x0400022C RID: 556
		CLK_D,
		// Token: 0x0400022D RID: 557
		CLK_F,
		// Token: 0x0400022E RID: 558
		CLK_G,
		// Token: 0x0400022F RID: 559
		CLK_H,
		// Token: 0x04000230 RID: 560
		CLK_J,
		// Token: 0x04000231 RID: 561
		CLK_K,
		// Token: 0x04000232 RID: 562
		CLK_L,
		// Token: 0x04000233 RID: 563
		CLK_SemicolonAndColon,
		// Token: 0x04000234 RID: 564
		CLK_ApostropheAndDoubleQuote,
		// Token: 0x04000235 RID: 565
		CLK_LeftShift,
		// Token: 0x04000236 RID: 566
		CLK_NonUsBackslash,
		// Token: 0x04000237 RID: 567
		CLK_Z,
		// Token: 0x04000238 RID: 568
		CLK_X,
		// Token: 0x04000239 RID: 569
		CLK_C,
		// Token: 0x0400023A RID: 570
		CLK_V,
		// Token: 0x0400023B RID: 571
		CLK_B,
		// Token: 0x0400023C RID: 572
		CLK_N,
		// Token: 0x0400023D RID: 573
		CLK_M,
		// Token: 0x0400023E RID: 574
		CLK_CommaAndLessThan,
		// Token: 0x0400023F RID: 575
		CLK_PeriodAndBiggerThan,
		// Token: 0x04000240 RID: 576
		CLK_SlashAndQuestionMark,
		// Token: 0x04000241 RID: 577
		CLK_LeftCtrl,
		// Token: 0x04000242 RID: 578
		CLK_LeftGui,
		// Token: 0x04000243 RID: 579
		CLK_LeftAlt,
		// Token: 0x04000244 RID: 580
		CLK_Lang2,
		// Token: 0x04000245 RID: 581
		CLK_Space,
		// Token: 0x04000246 RID: 582
		CLK_Lang1,
		// Token: 0x04000247 RID: 583
		CLK_International2,
		// Token: 0x04000248 RID: 584
		CLK_RightAlt,
		// Token: 0x04000249 RID: 585
		CLK_RightGui,
		// Token: 0x0400024A RID: 586
		CLK_Application,
		// Token: 0x0400024B RID: 587
		CLK_LedProgramming,
		// Token: 0x0400024C RID: 588
		CLK_Brightness,
		// Token: 0x0400024D RID: 589
		CLK_F12,
		// Token: 0x0400024E RID: 590
		CLK_PrintScreen,
		// Token: 0x0400024F RID: 591
		CLK_ScrollLock,
		// Token: 0x04000250 RID: 592
		CLK_PauseBreak,
		// Token: 0x04000251 RID: 593
		CLK_Insert,
		// Token: 0x04000252 RID: 594
		CLK_Home,
		// Token: 0x04000253 RID: 595
		CLK_PageUp,
		// Token: 0x04000254 RID: 596
		CLK_BracketRight,
		// Token: 0x04000255 RID: 597
		CLK_Backslash,
		// Token: 0x04000256 RID: 598
		CLK_NonUsTilde,
		// Token: 0x04000257 RID: 599
		CLK_Enter,
		// Token: 0x04000258 RID: 600
		CLK_International1,
		// Token: 0x04000259 RID: 601
		CLK_EqualsAndPlus,
		// Token: 0x0400025A RID: 602
		CLK_International3,
		// Token: 0x0400025B RID: 603
		CLK_Backspace,
		// Token: 0x0400025C RID: 604
		CLK_Delete,
		// Token: 0x0400025D RID: 605
		CLK_End,
		// Token: 0x0400025E RID: 606
		CLK_PageDown,
		// Token: 0x0400025F RID: 607
		CLK_RightShift,
		// Token: 0x04000260 RID: 608
		CLK_RightCtrl,
		// Token: 0x04000261 RID: 609
		CLK_UpArrow,
		// Token: 0x04000262 RID: 610
		CLK_LeftArrow,
		// Token: 0x04000263 RID: 611
		CLK_DownArrow,
		// Token: 0x04000264 RID: 612
		CLK_RightArrow,
		// Token: 0x04000265 RID: 613
		CLK_WinLock,
		// Token: 0x04000266 RID: 614
		CLK_Mute,
		// Token: 0x04000267 RID: 615
		CLK_Stop,
		// Token: 0x04000268 RID: 616
		CLK_ScanPreviousTrack,
		// Token: 0x04000269 RID: 617
		CLK_PlayPause,
		// Token: 0x0400026A RID: 618
		CLK_ScanNextTrack,
		// Token: 0x0400026B RID: 619
		CLK_NumLock,
		// Token: 0x0400026C RID: 620
		CLK_KeypadSlash,
		// Token: 0x0400026D RID: 621
		CLK_KeypadAsterisk,
		// Token: 0x0400026E RID: 622
		CLK_KeypadMinus,
		// Token: 0x0400026F RID: 623
		CLK_KeypadPlus,
		// Token: 0x04000270 RID: 624
		CLK_KeypadEnter,
		// Token: 0x04000271 RID: 625
		CLK_Keypad7,
		// Token: 0x04000272 RID: 626
		CLK_Keypad8,
		// Token: 0x04000273 RID: 627
		CLK_Keypad9,
		// Token: 0x04000274 RID: 628
		CLK_KeypadComma,
		// Token: 0x04000275 RID: 629
		CLK_Keypad4,
		// Token: 0x04000276 RID: 630
		CLK_Keypad5,
		// Token: 0x04000277 RID: 631
		CLK_Keypad6,
		// Token: 0x04000278 RID: 632
		CLK_Keypad1,
		// Token: 0x04000279 RID: 633
		CLK_Keypad2,
		// Token: 0x0400027A RID: 634
		CLK_Keypad3,
		// Token: 0x0400027B RID: 635
		CLK_Keypad0,
		// Token: 0x0400027C RID: 636
		CLK_KeypadPeriodAndDelete,
		// Token: 0x0400027D RID: 637
		CLK_G1,
		// Token: 0x0400027E RID: 638
		CLK_G2,
		// Token: 0x0400027F RID: 639
		CLK_G3,
		// Token: 0x04000280 RID: 640
		CLK_G4,
		// Token: 0x04000281 RID: 641
		CLK_G5,
		// Token: 0x04000282 RID: 642
		CLK_G6,
		// Token: 0x04000283 RID: 643
		CLK_G7,
		// Token: 0x04000284 RID: 644
		CLK_G8,
		// Token: 0x04000285 RID: 645
		CLK_G9,
		// Token: 0x04000286 RID: 646
		CLK_G10,
		// Token: 0x04000287 RID: 647
		CLK_VolumeUp,
		// Token: 0x04000288 RID: 648
		CLK_VolumeDown,
		// Token: 0x04000289 RID: 649
		CLK_MR,
		// Token: 0x0400028A RID: 650
		CLK_M1,
		// Token: 0x0400028B RID: 651
		CLK_M2,
		// Token: 0x0400028C RID: 652
		CLK_M3,
		// Token: 0x0400028D RID: 653
		CLK_G11,
		// Token: 0x0400028E RID: 654
		CLK_G12,
		// Token: 0x0400028F RID: 655
		CLK_G13,
		// Token: 0x04000290 RID: 656
		CLK_G14,
		// Token: 0x04000291 RID: 657
		CLK_G15,
		// Token: 0x04000292 RID: 658
		CLK_G16,
		// Token: 0x04000293 RID: 659
		CLK_G17,
		// Token: 0x04000294 RID: 660
		CLK_G18,
		// Token: 0x04000295 RID: 661
		CLK_International5,
		// Token: 0x04000296 RID: 662
		CLK_International4,
		// Token: 0x04000297 RID: 663
		CLK_Fn,
		// Token: 0x04000298 RID: 664
		CLM_1,
		// Token: 0x04000299 RID: 665
		CLM_2,
		// Token: 0x0400029A RID: 666
		CLM_3,
		// Token: 0x0400029B RID: 667
		CLM_4,
		// Token: 0x0400029C RID: 668
		CLH_LeftLogo,
		// Token: 0x0400029D RID: 669
		CLH_RightLogo,
		// Token: 0x0400029E RID: 670
		CLK_Logo,
		// Token: 0x0400029F RID: 671
		CLMM_Zone1,
		// Token: 0x040002A0 RID: 672
		CLMM_Zone2,
		// Token: 0x040002A1 RID: 673
		CLMM_Zone3,
		// Token: 0x040002A2 RID: 674
		CLMM_Zone4,
		// Token: 0x040002A3 RID: 675
		CLMM_Zone5,
		// Token: 0x040002A4 RID: 676
		CLMM_Zone6,
		// Token: 0x040002A5 RID: 677
		CLMM_Zone7,
		// Token: 0x040002A6 RID: 678
		CLMM_Zone8,
		// Token: 0x040002A7 RID: 679
		CLMM_Zone9,
		// Token: 0x040002A8 RID: 680
		CLMM_Zone10,
		// Token: 0x040002A9 RID: 681
		CLMM_Zone11,
		// Token: 0x040002AA RID: 682
		CLMM_Zone12,
		// Token: 0x040002AB RID: 683
		CLMM_Zone13,
		// Token: 0x040002AC RID: 684
		CLMM_Zone14,
		// Token: 0x040002AD RID: 685
		CLMM_Zone15,
		// Token: 0x040002AE RID: 686
		CLKLP_Zone1,
		// Token: 0x040002AF RID: 687
		CLKLP_Zone2,
		// Token: 0x040002B0 RID: 688
		CLKLP_Zone3,
		// Token: 0x040002B1 RID: 689
		CLKLP_Zone4,
		// Token: 0x040002B2 RID: 690
		CLKLP_Zone5,
		// Token: 0x040002B3 RID: 691
		CLKLP_Zone6,
		// Token: 0x040002B4 RID: 692
		CLKLP_Zone7,
		// Token: 0x040002B5 RID: 693
		CLKLP_Zone8,
		// Token: 0x040002B6 RID: 694
		CLKLP_Zone9,
		// Token: 0x040002B7 RID: 695
		CLKLP_Zone10,
		// Token: 0x040002B8 RID: 696
		CLKLP_Zone11,
		// Token: 0x040002B9 RID: 697
		CLKLP_Zone12,
		// Token: 0x040002BA RID: 698
		CLKLP_Zone13,
		// Token: 0x040002BB RID: 699
		CLKLP_Zone14,
		// Token: 0x040002BC RID: 700
		CLKLP_Zone15,
		// Token: 0x040002BD RID: 701
		CLKLP_Zone16,
		// Token: 0x040002BE RID: 702
		CLKLP_Zone17,
		// Token: 0x040002BF RID: 703
		CLKLP_Zone18,
		// Token: 0x040002C0 RID: 704
		CLKLP_Zone19,
		// Token: 0x040002C1 RID: 705
		CLM_5,
		// Token: 0x040002C2 RID: 706
		CLM_6,
		// Token: 0x040002C3 RID: 707
		CLI_Last = 190
	}
}
