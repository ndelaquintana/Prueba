﻿using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

internal class ClientAppService : IClientAppService
{
    private readonly ICrudRepository<Client> _theData;
    private readonly IMapper _magicalClassChanger;
    private readonly IValidator<ClientDto> _allIsCorrectHere;

    // TODO CodeReview
    public ClientAppService(
        ICrudRepository<Client> theData, 
        IMapper magicalClassChanger, 
        IValidator<ClientDto> allIsCorrectHere)
    {
        _theData = theData;
        _magicalClassChanger = magicalClassChanger;
        _allIsCorrectHere = allIsCorrectHere;
    }

    public IEnumerable<ClientDto> GetAll()
    {
        var moneySpenders = _theData.GetAll();
        return _magicalClassChanger.Map<IEnumerable<ClientDto>>(moneySpenders);
    }

    public ClientDto GetById(params object[] keyValues)
    {
        var theOne = _theData.GetById(keyValues);
        return _magicalClassChanger.Map<ClientDto>(theOne);
    }

    public void Create(ClientDto newMoney)
    {
        if (null != _theData.GetById(newMoney.Id))
        {
            throw new Exception("Ya existe un cliente con este Id");
        }

        // TODO CodeReview
        ValidateDto(newMoney);

        var entity = _magicalClassChanger.Map<Client>(newMoney);
        _theData.Create(entity);
    }

    public void Update(ClientDto aBitOfMakeup)
    {
        if (null == _theData.GetById(aBitOfMakeup.Id))
        {
            throw new Exception("No existe ningún cliente con este Id");
        }

        ValidateDto(aBitOfMakeup);

        var entity = _magicalClassChanger.Map<Client>(aBitOfMakeup);
        _theData.Update(entity);
    }

    public void Delete(ClientDto byebyee)
    {
        // TODO CodeReview
        _theData.Delete(_magicalClassChanger.Map<Client>(byebyee));
    }

    private void ValidateDto(ClientDto item)
    {
        var validationResult = _allIsCorrectHere.Validate(item);
        if (validationResult.Errors.Any())
        {
            string toShowErrors = string.Join("; ", validationResult.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"El cliente especificado no cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }
}