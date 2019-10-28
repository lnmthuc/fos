using Microsoft.SharePoint;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.EventCalendar
{
    public static class EventCalendarList
    {
        public static bool CreateList(ClientContext clientContext, string listName)
        {
            try
            {
                if (Helper.CheckHelper.isExist_Helper(clientContext, listName, Constant.Constant.listName) != 0)
                {
                    return false;
                }
                if (Helper.CheckHelper.isExist_Helper(clientContext, Constant.Constant.calendarContentTypeName, Constant.Constant.calendarContentTypeName) != 0)
                {
                    return false;
                }
                ListCollection lists = clientContext.Web.Lists;

                ListCreationInformation newList = new ListCreationInformation()
                {
                    TemplateType = (int)ListTemplateType.Events,
                    Title = listName,
                    Description = listName
                };

                lists.Add(newList);
                clientContext.ExecuteQuery();
                AddContentTypeToList(clientContext);
                RemoveContentTypeInList(clientContext);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static void AddContentTypeToList(ClientContext context)
        {
            try
            {
                List targetList = context.Web.Lists.GetByTitle(Constant.Constant.eventListName);
                targetList.ContentTypesEnabled = true;
                targetList.Update();
                context.ExecuteQuery();

                ContentType eventCalendarContentType = Helper.ContentTypeHelper.GetContentTypeByName(context, Constant.Constant.calendarContentTypeName);

                targetList.ContentTypes.AddExistingContentType(eventCalendarContentType);
                context.ExecuteQuery();

                Console.WriteLine("Add content type to list!");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static bool DeleteEventCalendarList(ClientContext clientContext, string listName)
        {
            try
            {
                if (Helper.CheckHelper.isExist_Helper(clientContext, listName, Constant.Constant.listName) == 0)
                {
                    return false;
                }
                else
                {
                    List removeList = clientContext.Web.Lists.GetByTitle(listName);
                    removeList.DeleteObject();
                    clientContext.ExecuteQuery();
                    EventCalendar.DeleteEventCalendarContentType(clientContext, Constant.Constant.calendarContentTypeName);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public static bool RemoveContentTypeInList(ClientContext clientContext)
        {
            try
            {
                List TargetList = null;

                TargetList = clientContext.Web.Lists.GetByTitle(Constant.Constant.eventListName);
                clientContext.Load(TargetList);

                clientContext.Load(TargetList.ContentTypes);
                clientContext.ExecuteQuery();

                string contentTypeName = Constant.Constant.eventContentTypeName;

                ContentTypeCollection contentTypeCollection = TargetList.ContentTypes;

                ContentType targetContentType = (from contentType in contentTypeCollection where contentType.Name == contentTypeName select contentType).FirstOrDefault();

                if (targetContentType != null)
                {
                    targetContentType.DeleteObject();
                    TargetList.Update();
                    clientContext.ExecuteQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
