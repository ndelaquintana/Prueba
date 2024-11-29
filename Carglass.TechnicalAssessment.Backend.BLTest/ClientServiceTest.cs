using AutoMapper;
using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.BL.Clients.Converter;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.DL.Repositories.InMemory;
using Carglass.TechnicalAssessment.Backend.DL.Repositories.InMemory.Clients;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using Newtonsoft.Json;

namespace Carglass.TechnicalAssessment.Backend.BLTest;

public class ClientServiceTest
{
    public ClientAppService BuildService()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ClientProfileConverter>();
        });
        var mapper = new Mapper(config);

        var repository = new ClientIMRepository();
        var validator = new ClientDtoValidator();
        return new ClientAppService(repository, validator, mapper);
    }

    //[Fact]
    public void HappyInsert()
    {
        var item = new ClientDto()
        {
            Id = 4,
            DocType = "nif",
            DocNum = "11111111E",
            Email = "eromani@sample.com",
            GivenName = "Enriqueta",
            FamilyName1 = "Romani",
            Phone = "668668668"
        };
        var service = BuildService();
        service.Create(item);
        var actual = service.GetById(item.Id);
        var actualJson = JsonConvert.SerializeObject(actual);
        var expectedJson = JsonConvert.SerializeObject(item);
        Assert.Equal(expectedJson, actualJson);
    }

    [Fact]
    public void IdDupValInsert()
    {
        var item = new ClientDto()
        {
            Id = 6,
            DocType = "nif",
            DocNum = "51223344E",
            Email = "eromani@sample.com",
            GivenName = "Enriqueta",
            FamilyName1 = "Romani",
            Phone = "668668668"
        };

        Action testCode = () =>
        {
            var service = BuildService();
            service.Create(item);
            service.Create(item);
        };

        var ex = Record.Exception(testCode);
        Assert.NotNull(ex);
    }

    [Fact]
    public void DocNumDupValInsert()
    {
        var item2 = new ClientDto()
        {
            Id = 2,
            DocType = "nif",
            DocNum = "21223344E",
            Email = "eromani@sample.com",
            GivenName = "Enriqueta",
            FamilyName1 = "Romani",
            Phone = "668668668"
        };

        var item3 = new ClientDto()
        {
            Id = 3,
            DocType = "nif",
            DocNum = "21223344E",
            Email = "eromani@sample.com",
            GivenName = "Enriqueta",
            FamilyName1 = "Romani",
            Phone = "668668668"
        };

        Action testCode = () =>
        {
            var service = BuildService();
            service.Create(item2);
            service.Create(item3);
        };

        var ex = Record.Exception(testCode);
        Assert.NotNull(ex);
    }

    //[Fact]
    public void HappyUpdate()
    {
        var item2 = new ClientDto()
        {
            Id = 2,
            DocType = "nif",
            DocNum = "21223344E",
            Email = "eromani@sample.com",
            GivenName = "Enriqueta",
            FamilyName1 = "Romani",
            Phone = "668668668"
        };

        var service = BuildService();
        service.Create(item2);
        item2.FamilyName1 = "Otro";
        service.Update(item2);
        var actual = service.GetById(item2.Id);

        var actualJson = JsonConvert.SerializeObject(actual);
        var expectedJson = JsonConvert.SerializeObject(item2);
        Assert.Equal(expectedJson, actualJson);
    }
}
