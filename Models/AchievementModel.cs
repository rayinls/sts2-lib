using System;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200048D RID: 1165
	public abstract class AchievementModel : AbstractModel
	{
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x001FBE2A File Offset: 0x001FA02A
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}
	}
}
