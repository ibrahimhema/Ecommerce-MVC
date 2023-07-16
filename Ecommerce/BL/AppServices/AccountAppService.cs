using BL.Bases;
using BL.ViewModels;
using DAL.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppServices
{
    public class AccountAppService : BaseAppService
    {
        public ApplicationUser Find (string username, string password)
        {
            return TheUnitOfWork.Account.Find(username, password);
        }

        public ApplicationUser Find(string username)
        {
            return TheUnitOfWork.Account.Find(username);
        }

        public ApplicationUser FindById (string id)
        {
            return TheUnitOfWork.Account.Where(u => u.Id == id).FirstOrDefault();
        }

        public IdentityResult Register (RegisterViewModel userModel)
        {
            ApplicationUser user = Mapper.Map<ApplicationUser>(userModel);
            return TheUnitOfWork.Account.Register(user);
        }

        public IdentityResult AssignToRole(string userId, Role_Name role)
        {
            return TheUnitOfWork.Account.AssignToRole(userId, role.ToString());
        }


        public ApplicationUser FindByEmailAndPassword (string email, string password)
        {
            return TheUnitOfWork.Account.FindByEmailAndPassword(email, password);
        }

        public IdentityResult AssignToRole(string userId, string roleName)
        {
            return TheUnitOfWork.Account.AssignToRole(userId, roleName);
        }
        
        public IdentityResult RemoveFromRole(string userId, string roleName)
        {
            if (!IsInRole(userId, Role_Name.User))
                AssignToRole(userId, Role_Name.User);
            return TheUnitOfWork.Account.RemoveFromRole(userId, roleName);
        }

        public bool IsInRole(string user_id, Role_Name role)
        {
            return TheUnitOfWork.Account.IsInRole(user_id, role);
        }

        public bool HasEmail(string email)
        {
            return TheUnitOfWork.Account.HasEmail(email);
        }


        public List<ApplicationUser> GetAllAdmins()
        {
            return
                TheUnitOfWork.Account.GetAllUsersByRole(Role_Name.Admin);
               
        }
        
        public List<AdminDisplayUserViewModel> GetAllUsers()
        {
            return Mapper
                .Map<List<AdminDisplayUserViewModel>>(
                TheUnitOfWork.Account.GetAllUsersByRole(Role_Name.User)
                );
        }
        
        public List<AdminDisplayUserViewModel> GetAllVendors()
        {
            return Mapper
                .Map<List<AdminDisplayUserViewModel>>(
                TheUnitOfWork.Account.GetAllUsersByRole(Role_Name.Vendor)
                );
        }
        public List<ApplicationUser> GetAllVendorsData()
        {
            return

                TheUnitOfWork.Account.GetAllUsersByRole(Role_Name.Vendor);
                
        }
        public List<ApplicationUser> GetAllUsersData()
        {
            return

                TheUnitOfWork.Account.GetAllUsersByRole(Role_Name.User);

        }
        public IdentityResult Edit(ApplicationUser user)
        {
            return TheUnitOfWork.Account.Edit(user);
        }
        public ProfileViewModel FindProfile(string username)
        {
            return Mapper.Map<ProfileViewModel>(TheUnitOfWork.Account.Find(username));
        }
        public ProfileEditViewModel GetForEdit(string username)
        {
            return Mapper.Map<ProfileEditViewModel>(Find(username));
        }
        public string GetRoleByUserId(string userId)
        {
            return TheUnitOfWork.Account.GetRoleByUserId(userId);
        }
    }

}
