using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Localization;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000491 RID: 1169
	[NullableContext(1)]
	[Nullable(0)]
	public class BestiaryMonsterMove
	{
		// Token: 0x060045D2 RID: 17874 RVA: 0x001FD410 File Offset: 0x001FB610
		public BestiaryMonsterMove(string moveName, string animId, string sfx = "", float sfxDelay = 0f)
		{
			this.moveName = moveName;
			this.animId = animId;
			this.sfx = sfx;
			this.sfxDelay = sfxDelay;
			this.InitName();
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x001FD43B File Offset: 0x001FB63B
		public BestiaryMonsterMove(LocString moveName, string animId, string sfx = "", float sfxDelay = 0f)
		{
			this.moveName = moveName.GetRawText();
			this.animId = animId;
			this.sfx = sfx;
			this.sfxDelay = sfxDelay;
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x001FD468 File Offset: 0x001FB668
		private void InitName()
		{
			string text = this.animId;
			string text2;
			if (this.animId.StartsWith("attack"))
			{
				text2 = BestiaryMonsterMove._attackMoveName.GetRawText();
			}
			else if (!(text == "cast"))
			{
				if (!(text == "die"))
				{
					if (!(text == "hurt"))
					{
						if (!(text == "revive"))
						{
							if (!(text == "stun"))
							{
								text2 = "MISSING_CASE";
							}
							else
							{
								text2 = BestiaryMonsterMove._stunMoveName.GetRawText();
							}
						}
						else
						{
							text2 = BestiaryMonsterMove._reviveMoveName.GetRawText();
						}
					}
					else
					{
						text2 = BestiaryMonsterMove._hurtMoveName.GetRawText();
					}
				}
				else
				{
					text2 = BestiaryMonsterMove._dieMoveName.GetRawText();
				}
			}
			else
			{
				text2 = BestiaryMonsterMove._castMoveName.GetRawText();
			}
			this.moveName = text2;
		}

		// Token: 0x04001A8F RID: 6799
		private static readonly LocString _attackMoveName = new LocString("bestiary", "ACTION_NAME.attack");

		// Token: 0x04001A90 RID: 6800
		private static readonly LocString _castMoveName = new LocString("bestiary", "ACTION_NAME.cast");

		// Token: 0x04001A91 RID: 6801
		private static readonly LocString _dieMoveName = new LocString("bestiary", "ACTION_NAME.die");

		// Token: 0x04001A92 RID: 6802
		private static readonly LocString _hurtMoveName = new LocString("bestiary", "ACTION_NAME.hurt");

		// Token: 0x04001A93 RID: 6803
		private static readonly LocString _reviveMoveName = new LocString("bestiary", "ACTION_NAME.revive");

		// Token: 0x04001A94 RID: 6804
		private static readonly LocString _stunMoveName = new LocString("bestiary", "ACTION_NAME.stun");

		// Token: 0x04001A95 RID: 6805
		public string moveName;

		// Token: 0x04001A96 RID: 6806
		public readonly string animId;

		// Token: 0x04001A97 RID: 6807
		[Nullable(2)]
		public readonly string sfx;

		// Token: 0x04001A98 RID: 6808
		public float sfxDelay;
	}
}
