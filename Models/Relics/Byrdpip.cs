using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004CC RID: 1228
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Byrdpip : RelicModel
	{
		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x00211FFC File Offset: 0x002101FC
		public override bool AddsPet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x06004A74 RID: 19060 RVA: 0x00211FFF File Offset: 0x002101FF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x06004A75 RID: 19061 RVA: 0x00212002 File Offset: 0x00210202
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x06004A76 RID: 19062 RVA: 0x00212005 File Offset: 0x00210205
		public override bool SpawnsPets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x06004A77 RID: 19063 RVA: 0x00212008 File Offset: 0x00210208
		public static string[] SkinOptions
		{
			get
			{
				return new string[] { "version1", "version2", "version3", "version4" };
			}
		}

		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x00212030 File Offset: 0x00210230
		// (set) Token: 0x06004A79 RID: 19065 RVA: 0x00212038 File Offset: 0x00210238
		[SavedProperty]
		public string Skin
		{
			get
			{
				return this._skin;
			}
			set
			{
				base.AssertMutable();
				this._skin = value;
			}
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x00212047 File Offset: 0x00210247
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<ByrdSwoop>(false);
			}
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x00212050 File Offset: 0x00210250
		public override async Task AfterObtained()
		{
			this.Skin = new Rng((uint)(base.Owner.NetId + (ulong)base.Owner.RunState.Rng.Seed), 0).NextItem<string>(Byrdpip.SkinOptions);
			List<CardModel> list = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c is ByrdonisEgg).ToList<CardModel>();
			if (CombatManager.Instance.IsInProgress)
			{
				list.AddRange(base.Owner.PlayerCombatState.AllCards.Where((CardModel c) => c is ByrdonisEgg));
			}
			foreach (CardModel cardModel in list)
			{
				await CardCmd.TransformTo<ByrdSwoop>(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			if (CombatManager.Instance.IsInProgress)
			{
				await this.SummonPet();
			}
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x00212094 File Offset: 0x00210294
		public override async Task BeforeCombatStart()
		{
			await this.SummonPet();
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x002120D8 File Offset: 0x002102D8
		private async Task SummonPet()
		{
			await PlayerCmd.AddPet<Byrdpip>(base.Owner);
		}

		// Token: 0x04002199 RID: 8601
		private string _skin = Byrdpip.SkinOptions[0];
	}
}
