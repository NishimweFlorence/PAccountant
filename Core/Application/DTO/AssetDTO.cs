namespace Application.DTO
{
    public class AssetCreateDTO
    {
           public String Name { get; set; } = string.Empty;
        public string Type { get; set; } = "Other";
        public string Currency { get; set; } = "RWF";
         public Decimal PurchaseValue { get; set; }
         public Decimal CurrentValue { get; set; }
         public string ValueChange { get; set; } = "None";
         public decimal Rate { get; set; }
        public DateTime  PurchaseDate { get; set; }
        public  DateTime  CreatedAt { get; set; }
    }

    public class AssetUpdateDTO
    {
           public String Name { get; set; } = string.Empty;
        public string Type { get; set; } = "Other";
        public string Currency { get; set; } = "RWF";
         public Decimal PurchaseValue { get; set; }
         public Decimal CurrentValue { get; set; }
         public string ValueChange { get; set; } = "None";
         public decimal Rate { get; set; }
        public DateTime  PurchaseDate { get; set; }
        public  DateTime  CreatedAt { get; set; }
        
    }

    
}
