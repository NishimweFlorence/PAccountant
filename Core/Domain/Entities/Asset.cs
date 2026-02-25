namespace Domain.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string Category { get; set; }
        public string Currency { get; set; }
         public Decimal PurchaseValue { get; set; }
         public Decimal CurrentValue { get; set; }
        public DateTime  PurchaseDate { get; set; }
        public  DateTime  CreatedAt { get; set; }


    }
}
