namespace ContasBancariasAspNet.Api.DTOs;

public class AccountDto
{
    public long? Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Agency { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public decimal Limit { get; set; }

    // Construtor sem parâmetros para deserialização JSON
    public AccountDto()
    {
    }

    // Construtor com parâmetros
    public AccountDto(long? id, string number, string agency, decimal balance, decimal limit)
    {
        Id = id;
        Number = number;
        Agency = agency;
        Balance = balance;
        Limit = limit;
    }

    // Construtor a partir do modelo
    public AccountDto(Models.Account model)
    {
        Id = model.Id;
        Number = model.Number;
        Agency = model.Agency;
        Balance = model.Balance;
        Limit = model.Limit;
    }

    public Models.Account ToModel()
    {
        return new Models.Account
        {
            Id = Id ?? 0,
            Number = Number,
            Agency = Agency,
            Balance = Balance,
            Limit = Limit
        };
    }
}