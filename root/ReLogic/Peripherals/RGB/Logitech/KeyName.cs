using System;

namespace ReLogic.Peripherals.RGB.Logitech
{
	// Token: 0x02000041 RID: 65
	internal enum KeyName
	{
		// Token: 0x04000141 RID: 321
		ESC = 1,
		// Token: 0x04000142 RID: 322
		F1 = 59,
		// Token: 0x04000143 RID: 323
		F2,
		// Token: 0x04000144 RID: 324
		F3,
		// Token: 0x04000145 RID: 325
		F4,
		// Token: 0x04000146 RID: 326
		F5,
		// Token: 0x04000147 RID: 327
		F6,
		// Token: 0x04000148 RID: 328
		F7,
		// Token: 0x04000149 RID: 329
		F8,
		// Token: 0x0400014A RID: 330
		F9,
		// Token: 0x0400014B RID: 331
		F10,
		// Token: 0x0400014C RID: 332
		F11 = 87,
		// Token: 0x0400014D RID: 333
		F12,
		// Token: 0x0400014E RID: 334
		PRINT_SCREEN = 311,
		// Token: 0x0400014F RID: 335
		SCROLL_LOCK = 70,
		// Token: 0x04000150 RID: 336
		PAUSE_BREAK = 69,
		// Token: 0x04000151 RID: 337
		TILDE = 41,
		// Token: 0x04000152 RID: 338
		ONE = 2,
		// Token: 0x04000153 RID: 339
		TWO,
		// Token: 0x04000154 RID: 340
		THREE,
		// Token: 0x04000155 RID: 341
		FOUR,
		// Token: 0x04000156 RID: 342
		FIVE,
		// Token: 0x04000157 RID: 343
		SIX,
		// Token: 0x04000158 RID: 344
		SEVEN,
		// Token: 0x04000159 RID: 345
		EIGHT,
		// Token: 0x0400015A RID: 346
		NINE,
		// Token: 0x0400015B RID: 347
		ZERO,
		// Token: 0x0400015C RID: 348
		MINUS,
		// Token: 0x0400015D RID: 349
		EQUALS,
		// Token: 0x0400015E RID: 350
		BACKSPACE,
		// Token: 0x0400015F RID: 351
		INSERT = 338,
		// Token: 0x04000160 RID: 352
		HOME = 327,
		// Token: 0x04000161 RID: 353
		PAGE_UP = 329,
		// Token: 0x04000162 RID: 354
		NUM_LOCK = 325,
		// Token: 0x04000163 RID: 355
		NUM_SLASH = 309,
		// Token: 0x04000164 RID: 356
		NUM_ASTERISK = 55,
		// Token: 0x04000165 RID: 357
		NUM_MINUS = 74,
		// Token: 0x04000166 RID: 358
		TAB = 15,
		// Token: 0x04000167 RID: 359
		Q,
		// Token: 0x04000168 RID: 360
		W,
		// Token: 0x04000169 RID: 361
		E,
		// Token: 0x0400016A RID: 362
		R,
		// Token: 0x0400016B RID: 363
		T,
		// Token: 0x0400016C RID: 364
		Y,
		// Token: 0x0400016D RID: 365
		U,
		// Token: 0x0400016E RID: 366
		I,
		// Token: 0x0400016F RID: 367
		O,
		// Token: 0x04000170 RID: 368
		P,
		// Token: 0x04000171 RID: 369
		OPEN_BRACKET,
		// Token: 0x04000172 RID: 370
		CLOSE_BRACKET,
		// Token: 0x04000173 RID: 371
		BACKSLASH = 43,
		// Token: 0x04000174 RID: 372
		KEYBOARD_DELETE = 339,
		// Token: 0x04000175 RID: 373
		END = 335,
		// Token: 0x04000176 RID: 374
		PAGE_DOWN = 337,
		// Token: 0x04000177 RID: 375
		NUM_SEVEN = 71,
		// Token: 0x04000178 RID: 376
		NUM_EIGHT,
		// Token: 0x04000179 RID: 377
		NUM_NINE,
		// Token: 0x0400017A RID: 378
		NUM_PLUS = 78,
		// Token: 0x0400017B RID: 379
		CAPS_LOCK = 58,
		// Token: 0x0400017C RID: 380
		A = 30,
		// Token: 0x0400017D RID: 381
		S,
		// Token: 0x0400017E RID: 382
		D,
		// Token: 0x0400017F RID: 383
		F,
		// Token: 0x04000180 RID: 384
		G,
		// Token: 0x04000181 RID: 385
		H,
		// Token: 0x04000182 RID: 386
		J,
		// Token: 0x04000183 RID: 387
		K,
		// Token: 0x04000184 RID: 388
		L,
		// Token: 0x04000185 RID: 389
		SEMICOLON,
		// Token: 0x04000186 RID: 390
		APOSTROPHE,
		// Token: 0x04000187 RID: 391
		ENTER = 28,
		// Token: 0x04000188 RID: 392
		NUM_FOUR = 75,
		// Token: 0x04000189 RID: 393
		NUM_FIVE,
		// Token: 0x0400018A RID: 394
		NUM_SIX,
		// Token: 0x0400018B RID: 395
		LEFT_SHIFT = 42,
		// Token: 0x0400018C RID: 396
		Z = 44,
		// Token: 0x0400018D RID: 397
		X,
		// Token: 0x0400018E RID: 398
		C,
		// Token: 0x0400018F RID: 399
		V,
		// Token: 0x04000190 RID: 400
		B,
		// Token: 0x04000191 RID: 401
		N,
		// Token: 0x04000192 RID: 402
		M,
		// Token: 0x04000193 RID: 403
		COMMA,
		// Token: 0x04000194 RID: 404
		PERIOD,
		// Token: 0x04000195 RID: 405
		FORWARD_SLASH,
		// Token: 0x04000196 RID: 406
		RIGHT_SHIFT,
		// Token: 0x04000197 RID: 407
		ARROW_UP = 328,
		// Token: 0x04000198 RID: 408
		NUM_ONE = 79,
		// Token: 0x04000199 RID: 409
		NUM_TWO,
		// Token: 0x0400019A RID: 410
		NUM_THREE,
		// Token: 0x0400019B RID: 411
		NUM_ENTER = 284,
		// Token: 0x0400019C RID: 412
		LEFT_CONTROL = 29,
		// Token: 0x0400019D RID: 413
		LEFT_WINDOWS = 347,
		// Token: 0x0400019E RID: 414
		LEFT_ALT = 56,
		// Token: 0x0400019F RID: 415
		SPACE,
		// Token: 0x040001A0 RID: 416
		RIGHT_ALT = 312,
		// Token: 0x040001A1 RID: 417
		RIGHT_WINDOWS = 348,
		// Token: 0x040001A2 RID: 418
		APPLICATION_SELECT,
		// Token: 0x040001A3 RID: 419
		RIGHT_CONTROL = 285,
		// Token: 0x040001A4 RID: 420
		ARROW_LEFT = 331,
		// Token: 0x040001A5 RID: 421
		ARROW_DOWN = 336,
		// Token: 0x040001A6 RID: 422
		ARROW_RIGHT = 333,
		// Token: 0x040001A7 RID: 423
		NUM_ZERO = 82,
		// Token: 0x040001A8 RID: 424
		NUM_PERIOD
	}
}
