using System.Text.Json.Serialization;

namespace ContasBancariasAspNet.Api.DTOs;

public class UserDto
{
    public long? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountDto? Account { get; set; }
    public CardDto? Card { get; set; }

    // Construtor sem parâmetros para deserialização JSON
    public UserDto()
    {
    }

    // Construtor com parâmetros para facilitar criação
    public UserDto(long? id, string name, AccountDto? account, CardDto? card)
    {
        Id = id;
        Name = name;
        Account = account;
        Card = card;
    }

    // Construtor a partir do modelo
    public UserDto(Models.User model)
    {
        Id = model.Id;
        Name = model.Name;
        Account = model.Account != null ? new AccountDto(model.Account) : null;
        Card = model.Card != null ? new CardDto(model.Card) : null;
    }

    public Models.User ToModel()
    {
        return new Models.User
        {
            Id = Id ?? 0,
            Name = Name,
            Account = Account?.ToModel(),
            Card = Card?.ToModel()
        };
    }
}