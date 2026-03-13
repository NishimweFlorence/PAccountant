using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public AssetType Type { get; set; } = null!;
        public Currency Currency { get; set; } = null!;
        public decimal PurchaseValue { get; set; }
        public decimal CurrentValue { get; set; }
        public ValueChangeType ValueChange { get; set; } = ValueChangeType.None;
        public decimal Rate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
