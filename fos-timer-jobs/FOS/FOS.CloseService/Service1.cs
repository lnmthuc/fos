﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using Unity;
using Microsoft.SharePoint.Client;
using System.Configuration;
using FOS.Services.FosCoreService;
using FOS.Model.Domain.ServiceModel;
using FOS.CoreService.Constants;
using FOS.Common.Constants;
using FOS.CloseService.UnityConfig;

namespace FOS.CloseService
{
    public partial class Service1 : ServiceBase
    {
        Timer timer = new Timer();
        FosCoreService coreService;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            CloseEvent();

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 60000;
            timer.Enabled = true;
        }

        protected override void OnStop()
        {
        }
        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            try
            {
                timer.Enabled = false;
                CloseEvent();
                timer.Enabled = true;
            }
            catch (Exception ex)
            {
                WriteToFile("Service failed: " + ex.StackTrace + " - " + ex.Message);
            }
        }
        public void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!System.IO.File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = System.IO.File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = System.IO.File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }
        private void CloseEvent()
        {
            var container = new UnityContainer();
            RegisterUnity.Register(container);
            coreService = container.Resolve<FosCoreService>();

            using (var clientContext = coreService.GetClientContext())
            {
                try
                {
                    var events = coreService.GetListEventOpened(clientContext);

                    foreach (var element in events)
                    {
                        var closeTimeString = element[EventConstantWS.EventTimeToClose] != null
                            ? element[EventConstantWS.EventTimeToClose].ToString() : "";
                        if (closeTimeString.Length > 0)
                        {
                            var closeTime = DateTime.Parse(closeTimeString).ToLocalTime();

                            if (DateTime.Now >= closeTime)
                            {
                                CloseAnEvent(clientContext, element);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //WriteToFile("Exception: " + e.Message);
                }
            }
        }
        private void CloseAnEvent(ClientContext clientContext, ListItem element)
        {
            var clientUrl = ConfigurationSettings.AppSettings["clientUrl"];
            var noReplyEmail = ConfigurationSettings.AppSettings["noReplyEmail"];

            coreService.ChangeStatusToClose(clientContext, element);

            CloseEventEmailTemplate emailTemplate = new CloseEventEmailTemplate();
            var host = element[EventConstantWS.EventHost] as FieldUserValue;
            emailTemplate.HostName = host.LookupValue;
            emailTemplate.EventTitle = element[EventConstantWS.EventTitle].ToString();
            emailTemplate.EventSummaryLink = clientUrl + "/events/summary/" + element["ID"];
            var emailTemplateDictionary = coreService.GetEmailTemplate(EventConstantWS.CloseEventEmailTemplate);
            emailTemplateDictionary.TryGetValue(EventEmail.Body, out string body);
            body = coreService.Parse(body, emailTemplate);
            emailTemplateDictionary.TryGetValue(EventEmail.Subject, out string subject);
            subject = coreService.Parse(subject, emailTemplate);

            coreService.SendEmail(clientContext, noReplyEmail, host.Email, body, subject);
        }
    }
}
