using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories.InMemory.Clients;

public class ClientIMRepository : DefaultIMRepository<Client>
{
    protected override void Initialize()
    {
        Create(new Client()
        {
            Id = 12345,
            DocType = "nif",
            DocNum = "11223344E",
            Email = "eromani@sample.com",
            GivenName = "Enriqueta",
            FamilyName1 = "Romani",
            Phone = "668668668"
        });

    }
}