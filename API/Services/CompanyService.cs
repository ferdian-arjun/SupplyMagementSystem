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
    private readonly IUserRepository _userRepository;

    public CompanyService(ICompanyRepository companyRepository, IVendorRepository vendorRepository, IUserRepository userRepository)
    {
        _companyRepository = companyRepository;
        _vendorRepository = vendorRepository;
        _userRepository = userRepository;
    }
    
    public IEnumerable<GetCompanyWithStatusDto> Get()
    {
        var companies = _companyRepository.Get(where: company => company.DeletedAt == null, includes: company => company.TblTrVendors!);
        if (!companies.Any()) return Enumerable.Empty<GetCompanyWithStatusDto>();
        
        List<GetCompanyWithStatusDto> getCompanyDtos = new();
        foreach (var company in companies) {
            var companyVendor = company.TblTrVendors.FirstOrDefault();
            getCompanyDtos.Add(new GetCompanyWithStatusDto()
            {
                Guid = company.Guid,
                Name = company.Name,
                Email = company.Email,
                Telp = company.Telp,
                Image = company.Image,
                BusinessType = company.BusinessType,
                Type = company.Type,
                Status = companyVendor.Status.ToString(),
                ConfirmBy = (companyVendor.ConfirmBy != null)?_userRepository.GetByGuid(companyVendor.ConfirmBy).FullName : "-",
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            });
        };
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
        var getCompany = _companyRepository.Get(where: company => company.Guid == guid, includes: company => company.TblTrVendors!).FirstOrDefault();
        if (getCompany is null) return null;
        return (GetCompanyDto)getCompany;
    }

    public int DeleteCompany(string guid)
    {
        using var scope = new TransactionScope();
        var getCompany = _companyRepository.GetByGuid(guid);
        if (getCompany is null) return -1;
        var isDelete = _companyRepository.SoftDelete(getCompany);

        var getVendors = _vendorRepository.Get(where: vendor => vendor.CompanyGuid.Equals(getCompany.Guid));
        foreach (var vendor in getVendors)
        {
            _vendorRepository.SoftDelete(vendor);
        }
        
        scope.Complete();
            
        return isDelete ? 1 : 0;
    }

    public IEnumerable<GetWaitingForApprovalDto> GetWaitingForApproval()
    {
        
        var vendors = _vendorRepository.Get(where: vendor => vendor.Status.Equals(VendorStatus.WaitingForApproval));
        if (!vendors.Any()) return Enumerable.Empty<GetWaitingForApprovalDto>();

        List<GetWaitingForApprovalDto> getWaitingForApprovalDtos = new() ;
        
        foreach (var vendor in vendors)
        {
            var company = _companyRepository.GetByGuid(vendor.CompanyGuid);
            
            getWaitingForApprovalDtos.Add(new GetWaitingForApprovalDto()
            {
                Guid = company.Guid,
                VendorGuid = vendor.Guid,
                Name = company.Name,
                Email = company.Email,
                Telp = company.Telp,
                Type = company.Type,
                Image = company.Image,
                Status = vendor.Status.ToString(),
                BusinessType = company.BusinessType,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
            });
        }
       
        return getWaitingForApprovalDtos;
    }
}