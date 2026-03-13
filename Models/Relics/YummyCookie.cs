using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005C8 RID: 1480
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class YummyCookie : RelicModel
	{
		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x060050C0 RID: 20672 RVA: 0x0021D97B File Offset: 0x0021BB7B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x0021D97E File Offset: 0x0021BB7E
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x060050C2 RID: 20674 RVA: 0x0021D984 File Offset: 0x0021BB84
		protected override string IconBaseName
		{
			get
			{
				CharacterModel characterModel;
				if (base.IsCanonical || base.Owner == null)
				{
					if (YummyCookie._cachedRandomCharacter == null)
					{
						YummyCookie._cachedRandomCharacter = Rng.Chaotic.NextItem<CharacterModel>(ModelDb.AllCharacters);
					}
					characterModel = YummyCookie._cachedRandomCharacter;
				}
				else
				{
					characterModel = base.Owner.Character;
				}
				string text;
				if (!(characterModel is Ironclad))
				{
					if (!(characterModel is Silent))
					{
						if (!(characterModel is Regent))
						{
							if (!(characterModel is Necrobinder))
							{
								if (!(characterModel is Defect))
								{
									text = "yummy_cookie_ironclad";
								}
								else
								{
									text = "yummy_cookie_defect";
								}
							}
							else
							{
								text = "yummy_cookie_necro";
							}
						}
						else
						{
							text = "yummy_cookie_regent";
						}
					}
					else
					{
						text = "yummy_cookie_silent";
					}
				}
				else
				{
					text = "yummy_cookie_ironclad";
				}
				return text;
			}
		}

		// Token: 0x060050C3 RID: 20675 RVA: 0x0021DA29 File Offset: 0x0021BC29
		protected override void AfterCloned()
		{
			base.AfterCloned();
			base.RelicIconChanged();
		}

		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x060050C4 RID: 20676 RVA: 0x0021DA37 File Offset: 0x0021BC37
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(4));
			}
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0021DA44 File Offset: 0x0021BC44
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.UpgradeSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForUpgrade(base.Owner, cardSelectorPrefs);
			List<CardModel> list = enumerable.ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x04002247 RID: 8775
		[Nullable(2)]
		private static CharacterModel _cachedRandomCharacter;
	}
}
