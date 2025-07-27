using APIWeb1.Dtos.AppUsers;
using APIWeb1.Models;

namespace APIWeb1.Dtos.Admin
{
    public class PaginationGetAllCompany
    {
        public List<Company> Company { get; set; }
        public int Total { get; set; }
    }
}
