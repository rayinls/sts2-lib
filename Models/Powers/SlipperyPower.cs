using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200069A RID: 1690
	public sealed class SlipperyPower : PowerModel
	{
		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x0600556D RID: 21869 RVA: 0x0022672F File Offset: 0x0022492F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012AC RID: 4780
		// (get) Token: 0x0600556E RID: 21870 RVA: 0x00226732 File Offset: 0x00224932
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012AD RID: 4781
		// (get) Token: 0x0600556F RID: 21871 RVA: 0x00226735 File Offset: 0x00224935
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005570 RID: 21872 RVA: 0x00226738 File Offset: 0x00224938
		[NullableContext(2)]
		public override decimal ModifyDamageCap(Creature target, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return decimal.MaxValue;
			}
			return 1m;
		}

		// Token: 0x06005571 RID: 21873 RVA: 0x00226754 File Offset: 0x00224954
		[NullableContext(1)]
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.TotalDamage != 0)
				{
					base.Flash();
					await PowerCmd.Decrement(this);
				}
			}
		}
	}
}
