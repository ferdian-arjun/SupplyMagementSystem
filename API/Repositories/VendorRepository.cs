using API.Context;
using API.Entities;
using API.Interface;

namespace API.Repositories;

public class VendorRepository : GeneralRepository<Vendor>, IVendorRepository
{
    public VendorRepository(MyContext context) : base(context)
    {
    }
}