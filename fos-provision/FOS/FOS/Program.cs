using System;
using Microsoft.SharePoint.Client;
using System.Configuration;
using System.Security;
using System.Linq;
using FOS.Constant;
namespace FOS
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {
            try
            {
                string siteUrl = Constant.Constant.siteUrl;
                var loginName = ConfigurationSettings.AppSettings["loginName"];
                var passWord = ConfigurationSettings.AppSettings["passWord"];

                var securePassword = new SecureString();
                passWord.ToCharArray().ToList().ForEach(c => securePassword.AppendChar(c));

                var authManager = new OfficeDevPnP.Core.AuthenticationManager();
                ClientContext context = authManager.GetWebLoginClientContext(siteUrl);

                String ChooseInput = "";

                int ChooseAction = 3;
                do
                {
                    Console.Write("Action: \t 1.Add Event List \t 2.Delete Event List" +
                        "\n  \t\t 3.Add Calendar \t 4.Delete Calendar \t 0.Exit: ");
                    ChooseInput = Console.ReadLine();

                    ChooseAction = Convert.ToInt32(ChooseInput);

                    if (ChooseAction == 1)
                    {
                        Console.WriteLine("Waiting...");
                        Event.EventCreate.Create(context);
                    }
                    else if (ChooseAction == 2)
                    {
                        Console.WriteLine("Waiting...");
                        Event.EventDelete.Delete(context);
                    }
                    else if (ChooseAction == 3)
                    {
                        Console.WriteLine("Waiting...");
                        bool check = EventCalendar.EventCalendar.CreateEventCalendarContentType(context, Constant.Constant.eventContentTypeName, Constant.Constant.calendarContentTypeName,
                            Constant.Constant.calendarContentTypeGroupName);
                        if (check)
                        {
                            Console.WriteLine("create content type success");
                        }
                        else
                        {
                            Console.WriteLine("create content type fail");
                        }
                    }
                    else if (ChooseAction == 4)
                    {
                        Console.WriteLine("Waiting...");
                        bool check = EventCalendar.EventCalendarList.DeleteEventCalendarList(context,Constant.Constant.eventListName);
                        if (check)
                        {
                            Console.WriteLine("remove content type success");
                        }
                        else
                        {
                            Console.WriteLine("remove content type fail");
                        }
                    }
                    else if (ChooseAction == 0)
                    {
                        return;
                    }
                } while (true);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
