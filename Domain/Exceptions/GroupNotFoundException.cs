﻿using System;

namespace Portfotolio.Domain.Exceptions
{
    public class GroupNotFoundException : PortfotolioException
    {
        public override bool IsWarning { get { return true; } }

        public string GroupId { get; private set; }

        public GroupNotFoundException(string groupId)
        {
            GroupId = groupId;
        }

        public GroupNotFoundException(string groupId, Exception innerException) : base(innerException)
        {
            GroupId = groupId;
        }

        public override string Message
        {
            get
            {
                return string.Format("Sorry, we cannot find group '{0}'...", GroupId);
            }
        }
    }
}