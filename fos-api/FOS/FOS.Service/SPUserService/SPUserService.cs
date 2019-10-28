﻿using FOS.Common;
using FOS.Model.Mapping;
using FOS.Services.Providers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FOS.Model.Domain;
using FOS.Services.EventServices;
using Microsoft.SharePoint.Client;
using Group = Microsoft.SharePoint.Client.Group;

namespace FOS.Services.SPUserService
{
    public class SPUserService : ISPUserService
    {
        IGraphApiProvider _graphApiProvider;
        ISharepointContextProvider _sharepointContextProvider;
        IEventService _eventService;
        public SPUserService(IGraphApiProvider graphApiProvider, ISharepointContextProvider sharepointContextProvider, IUserDtoMapper userDtoMapper, IEventService eventService)
        {
            _graphApiProvider = graphApiProvider;
            _sharepointContextProvider = sharepointContextProvider;
            _eventService = eventService;
        }

        public async Task<List<Model.Domain.User>> GetUsers()
        {
            var result = await _graphApiProvider.SendAsync(HttpMethod.Get, "users", null);
            if (result.IsSuccessStatusCode)
            {
                var resultGroup = await result.Content.ReadAsStringAsync();
                dynamic response = JsonConvert.DeserializeObject(resultGroup);

                List<Model.Domain.User> jsonUsers = response.value.ToObject<List<Model.Domain.User>>();

                return jsonUsers;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }

        public async Task<Model.Dto.GraphUser> GetCurrentUserGraph()
        {
            var result = await _graphApiProvider.SendAsync(HttpMethod.Get, "me", null);
            if (result.IsSuccessStatusCode)
            {
                var resultGroup = await result.Content.ReadAsStringAsync();
                Model.Dto.GraphUser response = JsonConvert.DeserializeObject<Model.Dto.GraphUser>(resultGroup);

                return response;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }
        public async Task<Model.Domain.User> GetCurrentUser()
        {
            var result = await _graphApiProvider.SendAsync(HttpMethod.Get, "me", null);
            if (result.IsSuccessStatusCode)
            {
                var resultGroup = await result.Content.ReadAsStringAsync();
                Model.Domain.User response = JsonConvert.DeserializeObject<Model.Domain.User>(resultGroup);

                return response;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }
        public async Task<Model.Domain.User> GetUserById(string Id)
        {
            var result = await _graphApiProvider.SendAsync(HttpMethod.Get, "users/" + Id, null);
            if (result.IsSuccessStatusCode)
            {
                var resultGroup = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Model.Domain.User>(resultGroup);

                return response;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }

        public async Task<List<Model.Domain.User>> GetGroups()
        {
            var result = await _graphApiProvider.SendAsync(HttpMethod.Get, "groups", null);
            if (result.IsSuccessStatusCode)
            {
                var resultGroup = await result.Content.ReadAsStringAsync();
                dynamic response = JsonConvert.DeserializeObject(resultGroup);

                List<Model.Domain.User> jsonGroup = response.value.ToObject<List<Model.Domain.User>>();

                return jsonGroup;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }

        public async Task<byte[]> GetAvatar(string Id, string avatarName)
        {
            var result = await _graphApiProvider.SendAsync(HttpMethod.Get, "users/" + Id + "/photos/48x48/$value", null);

            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsByteArrayAsync();
            }
            else
            {
                String[] spearator = { " " };

                String[] strlist = avatarName.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                string avatarUrl = "";
                if (strlist.Length == 1)
                {
                    string firstName = strlist[0];
                    avatarUrl = "https://ui-avatars.com/api/?name=" + firstName;
                }
                else
                {
                    string firstName = strlist[0];
                    string lastName = strlist[1];

                    string fullNameUrl = firstName + "+" + lastName;
                    avatarUrl = "https://ui-avatars.com/api/?name=" + fullNameUrl;
                }

                var http = new HttpClient();
                byte[] response = { };
                await Task.Run(async () => response = await http.GetByteArrayAsync(avatarUrl));

                return response;
            }
        }

        public async Task<List<Model.Domain.User>> GetUsersByName(string searchName)
        {
            var result = await _graphApiProvider.SendAsync(HttpMethod.Get, "me/people/?$search=" + searchName, null);
            if (result.IsSuccessStatusCode)
            {
                var resultGroup = await result.Content.ReadAsStringAsync();
                dynamic response = JsonConvert.DeserializeObject(resultGroup);

                List<Model.Domain.User> jsonUsers = response.value.ToObject<List<Model.Domain.User>>();

                return jsonUsers;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }

        public async Task<List<Model.Domain.User>> GroupListMemers(string groupId)
        {
            var result = await _graphApiProvider.SendAsync(HttpMethod.Get, "groups/" + groupId + "/members", null);
            if (result.IsSuccessStatusCode)
            {
                var resultGroup = await result.Content.ReadAsStringAsync();
                dynamic response = JsonConvert.DeserializeObject(resultGroup);

                List<Model.Domain.User> jsonUsers = response.value.ToObject<List<Model.Domain.User>>();
                foreach (var u in jsonUsers)
                {
                    if (u.Mail == null)
                    {
                        u.Mail = u.UserPrincipalName;
                    }
                }
                return jsonUsers;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }
        public async Task<List<Model.Domain.User>> SearchGroupOrUserByName(string searchName)
        {
            List<Model.Domain.User> listSearchUser = new List<Model.Domain.User>();
            String queryString = String.Format("users?$filter=startswith(displayName,'{0}')", searchName);
            var resultSearchUser = await _graphApiProvider.SendAsync(HttpMethod.Get, queryString, null);

            if (resultSearchUser.IsSuccessStatusCode)
            {
                var resultUser = await resultSearchUser.Content.ReadAsStringAsync();
                dynamic responseUser = JsonConvert.DeserializeObject(resultUser);

                var newList = responseUser.value.ToObject<List<Model.Domain.User>>();
                if (newList.Count > 0)
                {
                    listSearchUser.AddRange(newList);

                }
            }
            else
            {
                throw new Exception(await resultSearchUser.Content.ReadAsStringAsync());
            }

            List<Model.Domain.User> listSearchGroup = new List<Model.Domain.User>();
            String queryStringGroup = String.Format("groups?$filter=startswith(displayName,'{0}')", searchName);
            var resultSearchGroup = await _graphApiProvider.SendAsync(HttpMethod.Get, queryStringGroup, null);

            if (resultSearchGroup.IsSuccessStatusCode)
            {
                var resultGroup = await resultSearchGroup.Content.ReadAsStringAsync();
                dynamic responseUser = JsonConvert.DeserializeObject(resultGroup);

                List<Model.Domain.User> newList = responseUser.value.ToObject<List<Model.Domain.User>>();
                if (newList.Count > 0)
                {
                    listSearchUser.AddRange(newList);

                }
            }
            else
            {
                throw new Exception(await resultSearchUser.Content.ReadAsStringAsync());
            }

            return listSearchUser;
        }
        public async Task<bool> ValidateIsHost(int eventId)
        {
            try
            {
                //get host's event
                Event eventInfo =  _eventService.GetEvent(eventId);
                var hostId = eventInfo.HostId;
                //get current user
                Model.Domain.User currentUser = await GetCurrentUser();

                if(hostId != currentUser.Id)
                {
                    return false;
                }

                return true;
            }catch(Exception e)
            {
                throw e;
            }
        }
        public Microsoft.SharePoint.Client.User GetUserByMail(string mail)
        {
            using (var context = _sharepointContextProvider.GetSharepointContextFromUrl(APIResource.SHAREPOINT_CONTEXT + "/sites/FOS/"))
            {
                var result = GetUserPrincipalInfoByMail(mail);
                if (result != null)
                {
                    var user = context.Web.EnsureUser(result.Value.LoginName);
                    context.Load(user);
                    context.ExecuteQuery();
                    return user;
                }
                return null;
            }
        }
        public Microsoft.SharePoint.Client.ClientResult<Microsoft.SharePoint.Client.Utilities.PrincipalInfo> GetUserPrincipalInfoByMail(string mail)
        {
            using (var context = _sharepointContextProvider.GetSharepointContextFromUrl(APIResource.SHAREPOINT_CONTEXT + "/sites/FOS/"))
            {
                var result = Microsoft.SharePoint.Client.Utilities.Utility.ResolvePrincipal(context, context.Web, mail, Microsoft.SharePoint.Client.Utilities.PrincipalType.User, Microsoft.SharePoint.Client.Utilities.PrincipalSource.All, null, true);
                context.ExecuteQuery();

                return result;
            }
        }
        public List<Model.Domain.User> SiteGroupListMembers(string groupName)
        {
            try
            {
                using (var context = _sharepointContextProvider.GetSharepointContextFromUrl(APIResource.SHAREPOINT_CONTEXT + "/sites/FOS/"))
                {
                    List<Model.Domain.User> listUser = new List<Model.Domain.User>();
                    Web web = context.Web;
                    Microsoft.SharePoint.Client.Group group = web.SiteGroups.GetByName(groupName);
                    context.Load(web, w => w.Title);
                    context.Load(group, grp => grp.Users);
                    context.ExecuteQuery();
                    foreach (Microsoft.SharePoint.Client.User usr in group.Users)
                    {
                        if (usr.Email != "")
                        {
                            Model.Domain.User newUserInfo = new Model.Domain.User();
                            newUserInfo.Mail = usr.Email;
                            newUserInfo.DisplayName = usr.Title;
                            newUserInfo.LoginName = usr.LoginName;
                            listUser.Add(newUserInfo);
                        }                    }
                    if(listUser.Count > 0)
                    {
                        return listUser;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> SiteGroupAddMembers(Model.Domain.User addUser)
        {
            try
            {
                using (var context = _sharepointContextProvider.GetSharepointContextFromUrl(APIResource.SHAREPOINT_CONTEXT + "/sites/FOS/"))
                {
                    GroupCollection Groups = context.Web.SiteGroups;
                    Group ownersGroup = Groups.GetByName(Common.Constants.Constant.AdminGroupName);
                    Microsoft.SharePoint.Client.User newUser = context.Web.EnsureUser(addUser.Mail);
                    ownersGroup.Users.AddUser(newUser);
                    context.ExecuteQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool SiteGroupCheckMemberExists(Model.Domain.User checkUser)
        {
            try
            {
                bool check = false;

                List<Model.Domain.User> listUser = SiteGroupListMembers(Common.Constants.Constant.AdminGroupName);
                if(listUser != null && listUser.Count > 0)
                {
                    foreach (Model.Domain.User usr in listUser)
                    {
                        if (usr.Mail == checkUser.Mail)
                        {
                            return true;
                        }
                    }
                    return check;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> SiteGroupRemoveMembers(Model.Domain.User removeUser)
        {
            try
            {
                using (var context = _sharepointContextProvider.GetSharepointContextFromUrl(APIResource.SHAREPOINT_CONTEXT + "/sites/FOS/"))
                {
                    GroupCollection Groups = context.Web.SiteGroups;
                    Group ownersGroup = Groups.GetByName(Common.Constants.Constant.AdminGroupName);
                    Microsoft.SharePoint.Client.User user = context.Web.EnsureUser(removeUser.Mail);
                    context.Load(user);
                    context.ExecuteQuery();
                    ownersGroup.Users.RemoveByLoginName(user.LoginName);
                    context.ExecuteQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
