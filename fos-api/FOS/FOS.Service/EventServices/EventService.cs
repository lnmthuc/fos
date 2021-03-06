﻿using FOS.Services.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.Services.Providers;
using FOS.Model.Util;
using FOS.Common;

namespace FOS.Services.EventServices
{
    public class EventService : IEventService
    {
        IGraphApiProvider _graphApiProvider;
        ISharepointContextProvider _sharepointContextProvider;
        public EventService(IGraphApiProvider graphApiProvider, ISharepointContextProvider sharepointContextProvider)
        {
            _graphApiProvider = graphApiProvider;
            _sharepointContextProvider = sharepointContextProvider;
        }
        public IEnumerable<EventModel> GetAllEvent()
        {
            using (ClientContext clientContext = _sharepointContextProvider.GetSharepointContextFromUrl(APIResource.SHAREPOINT_CONTEXT + "sites/FOS/"))
            {
                var web = clientContext.Web;
                var list = web.Lists.GetByTitle("Event List");
                clientContext.Load(list);
                clientContext.ExecuteQuery();

                var collListItem = list.GetItems(CamlQuery.CreateAllItemsQuery());
                clientContext.Load(collListItem);
                clientContext.ExecuteQuery();

                var listEvent = new List<EventModel>();
                foreach (var element in collListItem)
                {
                    var eventModel = ListItemToEventModel(element);
                    listEvent.Add(eventModel);
                }
                return listEvent;
            }
        }
        private string ElementAttributeToString(Object element)
        {
            return element != null ? element.ToString() : "";
        }

        public EventModel ListItemToEventModel(ListItem element)
        {
            var host = element["EventHost"] as FieldLookupValue;
            if (host == null)
            {
                host = new FieldLookupValue();
            }
            var closeDateString = element["EventTimeToClose"].ToString();
            Nullable<DateTime> closeDate = DateTime.Parse(closeDateString).ToLocalTime();

            var remindDateString = element["EventTimeToReminder"].ToString();
            Nullable<DateTime> remindDate = DateTime.Parse(remindDateString).ToLocalTime();

            var eventModel = new EventModel();

            eventModel.Name = ElementAttributeToString(element["EventTitle"]);
            eventModel.Restaurant = ElementAttributeToString(element["EventRestaurant"]);
            eventModel.Category = ElementAttributeToString(element["EventCategory"]);
            eventModel.Date = closeDate;
            eventModel.Participants = ElementAttributeToString(element["EventParticipants"]);
            eventModel.MaximumBudget = ElementAttributeToString(element["EventMaximumBudget"]);
            eventModel.EventId = ElementAttributeToString(element["ID"]);
            eventModel.HostName = ElementAttributeToString(host.LookupValue);
            eventModel.HostId = ElementAttributeToString(element["EventHostId"]);
            eventModel.CreatedBy = ElementAttributeToString(element["EventCreatedUserId"]);
            eventModel.Status = ElementAttributeToString(element["status"]);
            eventModel.TimeToRemind = remindDate;

            return eventModel;
        }

        public EventModel GetEvent(int id)
        {
            using (ClientContext clientContext = _sharepointContextProvider.GetSharepointContextFromUrl(APIResource.SHAREPOINT_CONTEXT + "sites/FOS/"))
            {
                var web = clientContext.Web;
                var list = web.Lists.GetByTitle("Event List");
                clientContext.Load(list);
                clientContext.ExecuteQuery();

                var item = list.GetItemById(id);
                clientContext.Load(item);
                clientContext.ExecuteQuery();

                var result = ListItemToEventModel(item);

                return result;
            }
        }
    }
}
