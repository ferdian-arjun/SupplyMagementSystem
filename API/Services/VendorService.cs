using System.Data;
using API.Dtos.Vendor;
using API.Entities;
using API.Interface;
using API.Repositories;
using API.Utilities.Enum;
using API.Utilities.Handler;

namespace API.Services;

public class VendorService
{
    private readonly IVendorRepository _vendorRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IUserRepository _userRepository;

    public VendorService(IVendorRepository vendorRepository, ICompanyRepository companyRepository, IUserRepository userRepository)
    {
        _vendorRepository = vendorRepository;
        _companyRepository = companyRepository;
        _userRepository = userRepository;
    }
    
    public IEnumerable<GetVendorDto> Get()
    {
        var vendors = _vendorRepository.Get(where: vendor => vendor.Status == VendorStatus.Approval, includes: vendor => vendor.ConfirmByNavigation!);
        if (!vendors.Any()) return Enumerable.Empty<GetVendorDto>();
        
        List<GetVendorDto> getProjectDtos = new();
        foreach (var vendor in vendors)
        {
            var company = _companyRepository.GetByGuid(vendor.CompanyGuid);
            getProjectDtos.Add(new GetVendorDto()
            {
                Guid = vendor.Guid,
                CompanyGuid = vendor.CompanyGuid,
                CompanyName = company.Name,
                CompanyEmail = company.Email,
                CompanyImage = company.Image,
                CompanyBusinessType = company.BusinessType,
                CompanyTelp = company.Telp,
                CompanyType = company.Type,
                Status = vendor.Status.ToString(),
                ConfirmBy = vendor.ConfirmByNavigation?.FullName,
                CreatedAt = vendor.CreatedAt,
                UpdatedAt = vendor.UpdatedAt,
            });
        }
        
        return getProjectDtos;
    }
    
    public GetVendorDto GetByGuid(string guid)
    {
        var vendor = _vendorRepository.GetByGuid(guid);
        if (vendor is null) return null;
        
        var company = _companyRepository.GetByGuid(vendor.CompanyGuid);
        
        return new GetVendorDto()
        {
            Guid = vendor.Guid,
            CompanyGuid = vendor.CompanyGuid,
            CompanyName = company.Name,
            CompanyEmail = company.Email,
            CompanyImage = company.Image,
            CompanyBusinessType = company.BusinessType,
            CompanyTelp = company.Telp,
            CompanyType = company.Type,
            Status = vendor.Status.ToString(),
            ConfirmBy = vendor.ConfirmByNavigation?.FullName,
            CreatedAt = vendor.CreatedAt,
            UpdatedAt = vendor.UpdatedAt,
        };
    }

    public int UpdateStatus(UpdateStatusVendorDto updateStatusVendorDto)
    {
        var vendor = _vendorRepository.GetByGuid(updateStatusVendorDto.guid);
        if (vendor is null) return -1;

        var user = _userRepository.GetByGuid(updateStatusVendorDto.UserValidatorGuid);
        if (user is null) return -2;

        vendor.ConfirmBy = user.Guid;
        vendor.UpdatedAt = DateTime.Now;
        vendor.Status = updateStatusVendorDto.status;
        
        var updateStatus = _vendorRepository.Update(vendor);

        return updateStatus ? 1 : 0;
    }
}