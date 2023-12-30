using System.Transactions;
using API.Dtos.Company;
using API.Entities;
using API.Interface;
using API.Utilities.Enum;

namespace API.Services;

public class CompanyService
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IVendorRepository _vendorRepository;

    public CompanyService(ICompanyRepository companyRepository, IVendorRepository vendorRepository)
    {
        _companyRepository = companyRepository;
        _vendorRepository = vendorRepository;
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
        using var scope = new TransactionScope();
        var company = _companyRepository.Create(createCompanyDto);
        if (company is null) return null; 
        
        //create vendor
        var vendor  = _vendorRepository.Create(new Vendor()
        {
            Guid =  Guid.NewGuid().ToString(),
            CompanyGuid = company.Guid,
            Status = VendorStatus.WaitingForApproval,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        });
        
        if (vendor is null) return null; 
        
        scope.Complete();
        
        return (GetCompanyDto)company;
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