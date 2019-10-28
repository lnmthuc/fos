﻿using FOS.Model.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Services.SPUserService
{
    public interface ISPUserService
    {
        Task<List<User>> GetUsers();
        Task<Model.Dto.GraphUser> GetCurrentUserGraph();
        Task<User> GetCurrentUser();
        Task<User> GetUserById(string Id);
        Task<List<User>> GetGroups();
        Task<byte[]> GetAvatar(string Id, string avatarName);
        Task<List<User>> GetUsersByName(string searchName);

        Task<List<User>> GroupListMemers(string groupId);
        Task<List<User>> SearchGroupOrUserByName(string searchName);
        Task<bool> ValidateIsHost(int eventId);
        Microsoft.SharePoint.Client.User GetUserByMail(string mail);
        Microsoft.SharePoint.Client.ClientResult<Microsoft.SharePoint.Client.Utilities.PrincipalInfo> GetUserPrincipalInfoByMail(string mail);
        List<Model.Domain.User> SiteGroupListMembers(string groupName);
        Task<bool> SiteGroupAddMembers(Model.Domain.User addUser);
        bool SiteGroupCheckMemberExists(Model.Domain.User User);
        Task<bool> SiteGroupRemoveMembers(Model.Domain.User removeUser);
    }
}
