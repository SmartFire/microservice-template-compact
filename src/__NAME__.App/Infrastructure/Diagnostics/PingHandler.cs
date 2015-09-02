﻿using log4net;
using NServiceBus;
using __NAME__.Messages.Diagnostics;

namespace __NAME__.App.Infrastructure.Diagnostics
{
    public class PingHandler : IHandleMessages<PingCommand>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PingHandler));

        public void Handle(PingCommand message)
        {
            Log.InfoFormat("Ping received from {0} with Id {1} created on {2}", 
                message.Sender, 
                message.Id, 
                message.DateCreated);
        }
    }
}
