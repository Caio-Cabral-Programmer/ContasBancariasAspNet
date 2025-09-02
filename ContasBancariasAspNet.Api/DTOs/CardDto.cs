namespace ContasBancariasAspNet.Api.DTOs;

public class CardDto
{
    public long? Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public decimal Limit { get; set; }

    // Construtor sem parâmetros para deserialização JSON
    public CardDto()
    {
    }

    // Construtor com parâmetros
    public CardDto(long? id, string number, decimal limit)
    {
        Id = id;
        Number = number;
        Limit = limit;
    }

    // Construtor a partir do modelo
    public CardDto(Models.Card model)
    {
        Id = model.Id;
        Number = model.Number;
        Limit = model.Limit;
    }

    public Models.Card ToModel()
    {
        return new Models.Card
        {
            Id = Id ?? 0,
            Number = Number,
            Limit = Limit
        };
    }
}