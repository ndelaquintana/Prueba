using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories;

public class ClientIMRepository : ICrudRepository<Client>
{
    private readonly ICollection<Client> _clients;
    private readonly HashSet<int> _clientIds;
    public ClientIMRepository()
    {
        _clients = new HashSet<Client>()
        {
            new Client()
            {
                Id = 1,
                DocType = "nif",
                DocNum = "11223344E",
                Email = "eromani@sample.com",
                GivenName = "Enriqueta",
                FamilyName1 = "Romani",
                Phone = "668668668"
            }
        };
        _clientIds = new HashSet<int>(
            _clients.Select(z => z.Id)
            );
    }

    public IEnumerable<Client> GetAll()
    {
        // TODO CodeReview
        return _clients;
    }

    public Client? GetById(params object[] keyValues)
    {
        return _clients.SingleOrDefault(x => x.Id.Equals(keyValues[0]));
    }

    public void Create(Client item)
    {
        // TODO CodeReview
        if (_clientIds.Contains(item.Id))
            throw new InvalidDataException();

        _clients.Add(item);
        _clientIds.Add(item.Id);
    }

    public void Update(Client item)
    {
        // TODO CodeReview
        if (!_clientIds.Contains(item.Id))
            throw new InvalidDataException();
        Delete(item);
        Create(item);
    }

    public void Delete(Client item)
    {
        // TODO CodeReview
        var toDeleteItem = _clients.Single(x => x.Id.Equals(item.Id));
        _clients.Remove(toDeleteItem);
        _clientIds.Remove(toDeleteItem.Id);
    }
}
