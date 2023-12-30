using API.Utilities.Enum;

namespace API.Dtos.Vendor;

public class UpdateStatusVendorDto
{
    public string guid { get; set; }
    public string UserValidatorGuid { get; set; }
    public VendorStatus status { get; set; }
}