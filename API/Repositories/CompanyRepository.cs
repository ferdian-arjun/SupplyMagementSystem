using API.Context;
using API.Entities;
using API.Interface;

namespace API.Repositories;

public class CompanyRepository : GeneralRepository<Company>, ICompanyRepository
{
    public CompanyRepository(MyContext context) : base(context)
    {
    }
}