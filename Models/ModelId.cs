using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using MegaCrit.Sts2.Core.Helpers;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200049B RID: 1179
	[NullableContext(1)]
	[Nullable(0)]
	public class ModelId : IComparable<ModelId>, IEquatable<ModelId>
	{
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060047FA RID: 18426 RVA: 0x002029ED File Offset: 0x00200BED
		[CompilerGenerated]
		protected virtual Type EqualityContract
		{
			[CompilerGenerated]
			get
			{
				return typeof(ModelId);
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060047FB RID: 18427 RVA: 0x002029F9 File Offset: 0x00200BF9
		public string Category { get; }

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060047FC RID: 18428 RVA: 0x00202A01 File Offset: 0x00200C01
		public string Entry { get; }

		// Token: 0x060047FD RID: 18429 RVA: 0x00202A09 File Offset: 0x00200C09
		public ModelId(string category, string entry)
		{
			if (category.EndsWith("_MODEL"))
			{
				throw new ArgumentException("Category cannot end with '_MODEL'.", "category");
			}
			this.Category = category;
			this.Entry = entry;
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x00202A3C File Offset: 0x00200C3C
		public static ModelId Deserialize(string json)
		{
			string[] array = json.Split('.', StringSplitOptions.None);
			if (array.Length != 2)
			{
				throw new JsonException("'" + json + "' does not match the expected ModelId form.");
			}
			return new ModelId(array[0], array[1]);
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x00202A7A File Offset: 0x00200C7A
		public override string ToString()
		{
			return this.Category + "." + this.Entry;
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x00202A94 File Offset: 0x00200C94
		[NullableContext(2)]
		public int CompareTo(ModelId other)
		{
			int num = string.Compare(this.Category, (other != null) ? other.Category : null, StringComparison.Ordinal);
			if (num != 0)
			{
				return num;
			}
			return string.Compare(this.Entry, (other != null) ? other.Entry : null, StringComparison.Ordinal);
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x00202AD7 File Offset: 0x00200CD7
		public static string SlugifyCategory<[Nullable(2)] T>()
		{
			return ModelId.SlugifyCategory(typeof(T).Name);
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x00202AF0 File Offset: 0x00200CF0
		public static string SlugifyCategory(string category)
		{
			string text = StringHelper.Slugify(category);
			if (text.EndsWith("_MODEL"))
			{
				string text2 = text;
				int length = "_MODEL".Length;
				text = text2.Substring(0, text2.Length - length);
			}
			return text;
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x00202B2F File Offset: 0x00200D2F
		[CompilerGenerated]
		protected virtual bool PrintMembers(StringBuilder builder)
		{
			RuntimeHelpers.EnsureSufficientExecutionStack();
			builder.Append("Category = ");
			builder.Append(this.Category);
			builder.Append(", Entry = ");
			builder.Append(this.Entry);
			return true;
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x00202B69 File Offset: 0x00200D69
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator !=(ModelId left, ModelId right)
		{
			return !(left == right);
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x00202B75 File Offset: 0x00200D75
		[NullableContext(2)]
		[CompilerGenerated]
		public static bool operator ==(ModelId left, ModelId right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x00202B89 File Offset: 0x00200D89
		[CompilerGenerated]
		public override int GetHashCode()
		{
			return (EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Category>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Entry>k__BackingField);
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x00202BC9 File Offset: 0x00200DC9
		[NullableContext(2)]
		[CompilerGenerated]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ModelId);
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x00202BD8 File Offset: 0x00200DD8
		[NullableContext(2)]
		[CompilerGenerated]
		public virtual bool Equals(ModelId other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Category>k__BackingField, other.<Category>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Entry>k__BackingField, other.<Entry>k__BackingField));
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x00202C39 File Offset: 0x00200E39
		[CompilerGenerated]
		protected ModelId(ModelId original)
		{
			this.Category = original.<Category>k__BackingField;
			this.Entry = original.<Entry>k__BackingField;
		}

		// Token: 0x04001AF8 RID: 6904
		public static readonly ModelId none = new ModelId("NONE", "NONE");

		// Token: 0x04001AF9 RID: 6905
		private const string _bannedSuffix = "_MODEL";
	}
}
