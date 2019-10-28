using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.EventCalendar
{
    public static class EventCalendar
    {
        public static bool CreateEventCalendarContentType(ClientContext clientContext, string parentContentTypeName, string contentTypeName, string contentTypeGroup)
        {
            try
            {

                if (Helper.CheckHelper.isExist_Helper(clientContext, contentTypeName, contentTypeName) != 0)
                {
                    return false;
                }
                else
                {
                    Web rootWeb = clientContext.Site.RootWeb;

                    ContentType eventContentType = Helper.ContentTypeHelper.GetContentTypeByName(clientContext, Constant.Constant.eventContentTypeName);

                    if (eventContentType != null)
                    {
                        rootWeb.ContentTypes.Add(new ContentTypeCreationInformation
                        {
                            Name = contentTypeName,
                            Group = contentTypeGroup,
                            ParentContentType = eventContentType
                        });
                        clientContext.ExecuteQuery();
                        AddSiteColumn(clientContext);
                        EventCalendarList.CreateList(clientContext, Constant.Constant.eventListName);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool DeleteEventCalendarContentType(ClientContext clientContext, string contentTypeName)
        {
            try
            {
                if (Helper.CheckHelper.isExist_Helper(clientContext, contentTypeName, Constant.Constant.contentTypeName) == 0)
                {
                    return false;
                }
                else
                {
                    ContentType targetContentType = Helper.ContentTypeHelper.GetContentTypeByName(clientContext, Constant.Constant.calendarContentTypeName);

                    targetContentType.DeleteObject();

                    clientContext.ExecuteQuery();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static void AddSiteColumn(ClientContext clientContext)
        {
            try
            {
                Web rootWeb = clientContext.Site.RootWeb;
                Field EventHost = rootWeb.Fields.GetByInternalNameOrTitle("EventHost");
                Field EventRestaurant = rootWeb.Fields.GetByInternalNameOrTitle("EventRestaurant");
                Field EventMaximumBudget = rootWeb.Fields.GetByInternalNameOrTitle("EventMaximumBudget");
                Field EventTimeToReminder = rootWeb.Fields.GetByInternalNameOrTitle("EventTimeToReminder");
                Field EventParticipants = rootWeb.Fields.GetByInternalNameOrTitle("EventParticipants");
                Field EventCategory = rootWeb.Fields.GetByInternalNameOrTitle("EventCategory");
                Field EventRestaurantId = rootWeb.Fields.GetByInternalNameOrTitle("EventRestaurantId");
                Field EventServiceId = rootWeb.Fields.GetByInternalNameOrTitle("EventServiceId");
                Field EventDeliveryId = rootWeb.Fields.GetByInternalNameOrTitle("EventDeliveryId");
                Field EventCreatedUserId = rootWeb.Fields.GetByInternalNameOrTitle("EventCreatedUserId");
                Field EventHostId = rootWeb.Fields.GetByInternalNameOrTitle("EventHostId");
                Field EventType = rootWeb.Fields.GetByInternalNameOrTitle("EventTypes");
                Field EventStatus = rootWeb.Fields.GetByInternalNameOrTitle("EventStatus");
                Field EventParticipantsJson = rootWeb.Fields.GetByInternalNameOrTitle("EventParticipantsJson");
                Field EventIsReminder = rootWeb.Fields.GetByInternalNameOrTitle("EventIsReminder");
                ContentType sessionContentType = Helper.ContentTypeHelper.GetContentTypeByName(clientContext, Constant.Constant.calendarContentTypeName);

                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventHost
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventRestaurant
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventMaximumBudget
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventTimeToReminder
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventParticipants
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventCategory
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventRestaurantId

                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventServiceId
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventDeliveryId
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventCreatedUserId
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventHostId
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventType
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventStatus
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventParticipantsJson
                });
                sessionContentType.FieldLinks.Add(new FieldLinkCreationInformation
                {
                    Field = EventIsReminder
                });
                sessionContentType.Update(true);
                clientContext.ExecuteQuery();

                Console.WriteLine("Add site columns to content type");
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
