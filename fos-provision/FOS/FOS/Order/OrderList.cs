﻿using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Order
{
    class OrderList
    {
        const string contentTypeId = "0x0101009189AB5D3D2647B580F011DA2F356FB1";
        const string ListName = "Order List";

        public static void CreateList(ClientContext context)
        {
            if (Helper.CheckHelper.isExist_Helper(context, ListName, "list") == 0)
            {
                ListCreationInformation creationInfo = new ListCreationInformation();
                creationInfo.Title = ListName;
                creationInfo.Description = ListName;
                creationInfo.TemplateType = (int)ListTemplateType.GenericList;

                List createList;

                createList = context.Web.Lists.Add(creationInfo);
                context.Load(createList);
                context.ExecuteQuery();
                Console.WriteLine("Create list!");
                AddContentTypeToList(createList, context);
                CreateOrderListView(createList, context);
            }
            else
            {
                Console.WriteLine("List already exist!");
            }
        }

        public static void AddContentTypeToList(List targetList, ClientContext context)
        {
            targetList.ContentTypesEnabled = true;
            targetList.Update();
            context.Load(targetList);

            var contentType = context.Site.RootWeb.ContentTypes.GetById(contentTypeId);
            targetList.ContentTypes.AddExistingContentType(contentType);
            context.ExecuteQuery();

            Console.WriteLine("Add content type to list!");
        }
        public static void CreateOrderListView(List targetList, ClientContext context)
        {
            ViewCollection viewColl = targetList.Views;

            string[] viewFields = { "OrderEventId", "OrderUser", "OrderUserDelegate", "OrderInfo" };

            ViewCreationInformation creationInfo = new ViewCreationInformation();
            creationInfo.Title = "Order View";
            creationInfo.RowLimit = 50;
            creationInfo.ViewFields = viewFields;
            creationInfo.ViewTypeKind = ViewType.None;
            creationInfo.SetAsDefaultView = true;
            viewColl.Add(creationInfo);
            context.ExecuteQuery();
            Console.WriteLine("created view");
        }
    }
}