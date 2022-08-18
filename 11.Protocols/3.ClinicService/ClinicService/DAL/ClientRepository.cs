using ClinicService.DAL.Interfaces;
using ClinicService.Data;

namespace ClinicService.DAL;

public class ClientRepository : IClientRepository
{
    private readonly ClinicServiceDbContext _dbContext;
    private readonly ILogger<ClientRepository> _logger;

    public ClientRepository(ClinicServiceDbContext dbContext, ILogger<ClientRepository> logger)
        => (_dbContext, _logger) = (dbContext, logger);

    public IList<Client> GetAll()
    {
        return _dbContext.Clients.ToList();
    }

    public Client? GetById(int id)
    {
        return _dbContext.Clients.FirstOrDefault(client => client.ClientId == id);
    }

    public int Add(Client item)
    {
        _dbContext.Clients.Add(item);
        _dbContext.SaveChanges();
        return item.ClientId;
    }

    public void Update(Client item)
    {
        if (item == null)
            throw new NullReferenceException();

        var client = GetById(item.ClientId);
        if (client == null)
            throw new KeyNotFoundException();

        client.Document = item.Document;
        client.Surname = item.Surname;
        client.FirstName = item.FirstName;
        client.Patronymic = item.Patronymic;

        _dbContext.Update(client);
        _dbContext.SaveChanges();
    }

    public void Delete(Client item)
    {
        if (item == null)
            throw new NullReferenceException();

        Delete(item.ClientId);
    }

    public void Delete(int id)
    {
        var client = GetById(id);
        
        if (client == null)
            throw new KeyNotFoundException();
        
        _dbContext.Remove(client);
        _dbContext.SaveChanges();
    }
}
