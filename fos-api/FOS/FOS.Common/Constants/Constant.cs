﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Common.Constants
{
    public class Constant
    {
        public const string email_template = @"~/App_Data/email_template.txt";
        public const string ReminderSubject = "Reminder";
        public const string RemindEventEmailTemplate = @"~/App_Data/RemindEventEmailTemplate.json";
        public const string FeedbackEmailTemplate = @"~/App_Data/FeedbackEmailTemplate.json";
        public const string ReorderEmailTemplate = @"~/App_Data/ReorderEmailTemplate.json";
        public const string CancelEmailTemplate = @"~/App_Data/CancelEventEmailTemplate.json";
        public const string UserNotPerission = "You don't have permission to do this action";
        public const string NotValidEventInfo = "Your event information is not valid!";
    }
}
