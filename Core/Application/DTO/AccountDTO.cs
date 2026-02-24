namespace Application.DTO
{
    public class AccountCreateDTO
    {
       
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal balance {get;set;}
        public string? Currency {get;set;}
        public string? Status {get;set;}
    }

    public class AccountUpdateDTO
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public decimal balance {get;set;}
        public string? Status {get;set;}
        
    }

    
}