using System.Data.Common;
using System.Globalization;
using System.Threading.Channels;
using BankCards.DAL.Context;
using BankCards.Domain;
using BankCards.Interfaces;
using BankCards.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace BankCards.DAL.Repositories;

public class CardsRepositorySql : ICardRepository
{
    private IConnectionManager _connectionManager;

    private SqlConnection _connection => (SqlConnection)_connectionManager.GetOpenedConnection();

    private readonly ILogger<CardsRepositoryOrm> _logger;

    public CardsRepositorySql(IConnectionManager connectionManager, ILogger<CardsRepositoryOrm> logger)
    {
        _connectionManager = connectionManager;
        _logger = logger;
    }

    public async Task<IEnumerable<Card>> GetAllAsync(CancellationToken cancel = default)
    {
        var cards = new List<Card>();

        var command = new SqlCommand("SELECT Id, Type, Number, Created, ValidThru, Owner, Cvc FROM Cards", _connection);

        using var reader = await command.ExecuteReaderAsync(cancel).ConfigureAwait(false);
        while (await reader.ReadAsync(cancel).ConfigureAwait(false))
        {
            cards.Add(FromReader(reader));
        }
        return cards.ToArray();
    }

    public async Task<Card?> GetByIdAsync(int id, CancellationToken cancel = default)
    {
        var cards = new List<Card>();

        var command = new SqlCommand("SELECT Id, Type, Number, Created, ValidThru, Owner, Cvc FROM Cards WHERE id = @Id", _connection);
        command.Parameters.Add("@Id", System.Data.SqlDbType.Int);
        command.Parameters["@Id"].Value = id;

        using var reader = await command.ExecuteReaderAsync(cancel).ConfigureAwait(false);

        if (await reader.ReadAsync(cancel).ConfigureAwait(false))
        {
            return FromReader(reader);
        }
        else
            return null;
    }

    /// <summary>
    /// Возвращает карты по совпадению части номера
    /// EF функции sql inject безопасны
    /// </summary>
    public async Task<IEnumerable<Card>> GetByNumberAsync(string numberPattern, CancellationToken cancel = default)
    {
        var cards = new List<Card>();

        var command = new SqlCommand("SELECT Id, Type, Number, Created, ValidThru, Owner, Cvc FROM Cards WHERE Number LIKE @Number", _connection);
        command.Parameters.Add("@Number", System.Data.SqlDbType.NVarChar);
        command.Parameters["@Number"].Value = numberPattern;

        using var reader = await command.ExecuteReaderAsync(cancel).ConfigureAwait(false);
        while (await reader.ReadAsync(cancel).ConfigureAwait(false))
        {
            cards.Add(FromReader(reader));
        }
        return cards.ToArray();
    }

    public async Task<int> CreateAsync(Card entity, CancellationToken cancel = default)
    {
        var command = new SqlCommand("INSERT INTO Cards (Type, Number, Created, ValidThru, Owner, Cvc) VALUES(@Type, @Number, @Created, @ValidThru, @Owner, @Cvc); SELECT SCOPE_IDENTITY()", _connection);
        command.Parameters.Add("@Type", System.Data.SqlDbType.Int);
        command.Parameters.Add("@Number", System.Data.SqlDbType.NVarChar);
        command.Parameters.Add("@Created", System.Data.SqlDbType.DateTime);
        command.Parameters.Add("@ValidThru", System.Data.SqlDbType.DateTime);
        command.Parameters.Add("@Owner", System.Data.SqlDbType.NVarChar);
        command.Parameters.Add("@Cvc", System.Data.SqlDbType.Int);
        command.Parameters["@Type"].Value = entity.Type;
        command.Parameters["@Number"].Value = entity.Number;
        command.Parameters["@Created"].Value = entity.Created;
        command.Parameters["@ValidThru"].Value = entity.ValidThru;
        command.Parameters["@Owner"].Value = entity.Owner;
        command.Parameters["@Cvc"].Value = entity.Cvc;

        var newId = Convert.ToInt32(await command.ExecuteScalarAsync(cancel).ConfigureAwait(false));
        entity.Id = newId;
        return newId;
    }

    public async Task<bool> UpdateAsync(Card entity, CancellationToken cancel = default)
    {
        var command = new SqlCommand("UPDATE Cards SET Type=@Type, Number=@Number, Created=@Created, ValidThru=@ValidThru, Owner=@Owner, Cvc=@Cvc WHERE Id=@Id", _connection);
        command.Parameters.Add("@Id", System.Data.SqlDbType.Int);
        command.Parameters.Add("@Type", System.Data.SqlDbType.Int);
        command.Parameters.Add("@Number", System.Data.SqlDbType.NVarChar);
        command.Parameters.Add("@Created", System.Data.SqlDbType.DateTime);
        command.Parameters.Add("@ValidThru", System.Data.SqlDbType.DateTime);
        command.Parameters.Add("@Owner", System.Data.SqlDbType.NVarChar);
        command.Parameters.Add("@Cvc", System.Data.SqlDbType.Int);
        command.Parameters["@Id"].Value = entity.Id;
        command.Parameters["@Type"].Value = entity.Type;
        command.Parameters["@Number"].Value = entity.Number;
        command.Parameters["@Created"].Value = entity.Created;
        command.Parameters["@ValidThru"].Value = entity.ValidThru;
        command.Parameters["@Owner"].Value = entity.Owner;
        command.Parameters["@Cvc"].Value = entity.Cvc;

        await command.ExecuteNonQueryAsync(cancel).ConfigureAwait(false);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancel = default)
    {
        var command = new SqlCommand("DELETE FROM Cards WHERE Id=@Id", _connection);
        command.Parameters.Add("@Id", System.Data.SqlDbType.Int);
        command.Parameters["@Id"].Value = id;
        await command.ExecuteNonQueryAsync(cancel).ConfigureAwait(false);
        return true;
    }

    private Card FromReader(DbDataReader reader)
    {
        var card = new Card()
        {
            Id = reader.GetInt32(0),
            Type = (CardTypes)reader.GetInt32(1),
            Number = reader.GetString(2),
            Created = reader.GetDateTime(3),
            ValidThru = reader.GetDateTime(4),
            Owner = reader.GetString(5),
            Cvc = reader.GetInt32(6)
        };
        return card;
    }
}
