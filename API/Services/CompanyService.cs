using API.Dtos.Company;
using API.Interface;

namespace API.Services;

public class CompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }
    
    public IEnumerable<GetCompanyDto> Get()
    {
        var companies = _companyRepository.GetAll();
        if (!companies.Any()) return Enumerable.Empty<GetCompanyDto>();
        
        List<GetCompanyDto> getCompanyDtos = new();
        foreach (var company in companies) getCompanyDtos.Add((GetCompanyDto)company);
        return getCompanyDtos;
    }

    public GetCompanyDto? CreateCompany(CreateCompanyDto createCompanyDto)
    {
        var createAccountStatus = _companyRepository.Create(createCompanyDto);
        if (createAccountStatus is null) return null; 
        return (GetCompanyDto)createAccountStatus;
    }

    public int UpdateCompany(UpdateCompanyDto updateCompanyDto)
    {
        var getCompany = _companyRepository.GetByGuid(updateCompanyDto.Guid);
        if (getCompany is null) return -1;

        var isUpdate = _companyRepository.Update(updateCompanyDto);
        return isUpdate ? 1 : 0;
    }

    public GetCompanyDto? GetByGuid(string guid)
    {
        var getCompany = _companyRepository.GetByGuid(guid);
        if (getCompany is null) return null;
        return (GetCompanyDto)getCompany;
    }

    public int DeleteCompany(string guid)
    {
        var getCompany = _companyRepository.GetByGuid(guid);
        if (getCompany is null) return -1;
        var isDelete = _companyRepository.SoftDelete(getCompany);
        return isDelete ? 1 : 0;
    }
}