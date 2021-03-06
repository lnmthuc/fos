﻿using FOS.Common;
using FOS.Model.Domain;
using FOS.Model.Util;
using FOS.Services.Providers;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Services.SPListService
{
    public class SPListService : ISPListService
    {
        IGraphApiProvider _graphApiProvider;
        ISharepointContextProvider _sharepointContextProvider;
        public SPListService(IGraphApiProvider graphApiProvider, ISharepointContextProvider sharepointContextProvider)
        {
            _graphApiProvider = graphApiProvider;
            _sharepointContextProvider = sharepointContextProvider;
        }

        //public async Task<ApiResponse> GetList(string Id)
        //{
        //    var list = await _graphApiProvider.SendAsync(HttpMethod.Get, "sites/lists/" + Id, null);
        //    return ApiUtil.CreateSuccessfulResult();
        //}

        public async Task AddListItem(string Id, JSONRequest item)
        {
            await _graphApiProvider.SendAsync(HttpMethod.Post, "sites/lists/" + Id + "/items/", item.data);
        }
        public void AddEventListItem(string Id, EventListItem item)
        {
            var eventData = item;
            using (ClientContext context = _sharepointContextProvider.GetSharepointContextFromUrl(APIResource.SHAREPOINT_CONTEXT + "/sites/FOS/"))
            {
                Web web = context.Web;
                var loginName = item.eventHost;
                //var loginName = "i:0#.f|membership|" + item.eventHost;
                //string email = eventData.eventHost;
                //PeopleManager peopleManager = new PeopleManager(context);
                //ClientResult<PrincipalInfo> principal = Utility.ResolvePrincipal(context, web, email, PrincipalType.User, PrincipalSource.All, web.SiteUsers, true);
                //context.ExecuteQuery();

                Microsoft.SharePoint.Client.User newUser = context.Web.EnsureUser(loginName);
                context.Load(newUser);
                context.ExecuteQuery();

                FieldUserValue userValue = new FieldUserValue();
                userValue.LookupId = newUser.Id;

                List members = context.Web.Lists.GetByTitle("Event List");
                Microsoft.SharePoint.Client.ListItem listItem = members.AddItem(new ListItemCreationInformation());
                listItem["EventHost"] = userValue;
                listItem["EventTitle"] = eventData.eventTitle;
                listItem["EventId"] = 1;
                listItem["EventRestaurant"] = eventData.eventRestaurant;
                listItem["EventMaximumBudget"] = eventData.eventMaximumBudget;
                listItem["EventTimeToClose"] = eventData.eventTimeToClose;
                listItem["EventTimeToReminder"] = eventData.eventTimeToReminder;
                listItem["EventParticipants"] = eventData.eventParticipants;
                listItem["EventCategory"] = eventData.eventCategory;

                listItem["EventRestaurantId"] = eventData.eventRestaurantId;
                listItem["EventServiceId"] = eventData.eventServiceId;
                listItem["EventDeliveryId"] = eventData.eventDeliveryId;
                listItem["EventCreatedUserId"] = eventData.eventCreatedUserId;
                listItem["EventHostId"] = eventData.eventHostId;
                listItem.Update();
                context.ExecuteQuery();
            }
        }
    }
}
