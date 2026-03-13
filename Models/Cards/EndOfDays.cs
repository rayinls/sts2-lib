using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000932 RID: 2354
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EndOfDays : CardModel
	{
		// Token: 0x06006A24 RID: 27172 RVA: 0x0025A869 File Offset: 0x00258A69
		public EndOfDays()
			: base(3, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies, true)
		{
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x06006A25 RID: 27173 RVA: 0x0025A876 File Offset: 0x00258A76
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<DoomPower>(29m));
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x06006A26 RID: 27174 RVA: 0x0025A889 File Offset: 0x00258A89
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x06006A27 RID: 27175 RVA: 0x0025A898 File Offset: 0x00258A98
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			Vector2? sideCenterFloor = VfxCmd.GetSideCenterFloor(CombatSide.Enemy, base.CombatState);
			if (sideCenterFloor != null)
			{
				NLargeMagicMissileVfx nlargeMagicMissileVfx = NLargeMagicMissileVfx.Create(sideCenterFloor.Value, new Color("8c2447"));
				if (nlargeMagicMissileVfx != null)
				{
					NCombatRoom instance = NCombatRoom.Instance;
					if (instance != null)
					{
						instance.CombatVfxContainer.AddChildSafely(nlargeMagicMissileVfx);
					}
					await Cmd.Wait(nlargeMagicMissileVfx.WaitTime, false);
				}
			}
			foreach (Creature creature in base.CombatState.HittableEnemies)
			{
				await PowerCmd.Apply<DoomPower>(creature, base.DynamicVars.Doom.BaseValue, base.Owner.Creature, this, false);
			}
			IEnumerator<Creature> enumerator = null;
			await DoomPower.DoomKill(DoomPower.GetDoomedCreatures(base.CombatState.HittableEnemies));
		}

		// Token: 0x06006A28 RID: 27176 RVA: 0x0025A8DB File Offset: 0x00258ADB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Doom.UpgradeValueBy(8m);
		}

		// Token: 0x04002574 RID: 9588
		public const int doomAmount = 29;
	}
}
