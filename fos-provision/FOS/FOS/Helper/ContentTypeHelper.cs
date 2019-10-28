using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Helper
{
    public static class ContentTypeHelper
    {
        public static ContentType GetContentTypeByName(ClientContext clientContext, string Name)
        {
            ContentTypeCollection contentTypeCollection = clientContext.Web.ContentTypes;

            clientContext.Load(contentTypeCollection);
            clientContext.ExecuteQuery();

            ContentType targetContentType = (from contentType in contentTypeCollection where contentType.Name == Name select contentType).FirstOrDefault();

            clientContext.ExecuteQuery();
            return targetContentType;
        }
    }
}
