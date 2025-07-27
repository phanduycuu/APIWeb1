using APIWeb1.Dtos.AppUsers;

namespace APIWeb1.Dtos.Admin
{
    public class PaginationGetAllAccount
    {
        public List<AdminAccountUser> AccountUser { get; set; }
        public int Total { get; set; }
    }
}
