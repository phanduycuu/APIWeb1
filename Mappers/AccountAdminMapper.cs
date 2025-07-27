using APIWeb1.Dtos.Account;
using APIWeb1.Dtos.Companys;
using APIWeb1.Models;

namespace APIWeb1.Mappers
{
    public static class AccountAdminMapper
    {
        public static AccountAdmin ToAccountAdminDto(this AppUser appuserModel)
        {
            var companyname="";
            if (appuserModel.Company!=null)
            {
                companyname=appuserModel.Company.Name;  
            }
            return new AccountAdmin
            {
                Id = appuserModel.Id,
                Fullname=appuserModel.Fullname,
                Username=appuserModel.UserName,
                Email=appuserModel.Email,
                CompanyName=companyname,
                Status = appuserModel.Status

            };
        }
    }
}
