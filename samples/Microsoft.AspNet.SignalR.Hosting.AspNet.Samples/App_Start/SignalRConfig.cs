﻿using System.Diagnostics;
using Microsoft.AspNet.SignalR.Hubs;

namespace Microsoft.AspNet.SignalR.Samples
{
    public static class SignalRConfig
    {
        public static void ConfigureSignalR(IDependencyResolver dependencyResolver, IHubPipeline hubPipeline)
        {
            // Uncomment the following line to enable scale-out using SQL Server
            // dependencyResolver.UseSqlServer(ConfigurationManager.ConnectionStrings["SignalRSamples"].ConnectionString);

            // Uncomment the following line to enable scale-out using Redis 
            // dependencyResolver.UseRedis("127.0.0.1", 6379, "", new[] { "SignalRSamples" }); 

            hubPipeline.AddModule(new SamplePipelineModule());
            hubPipeline.EnableAutoRejoiningGroups();
        }

        private class SamplePipelineModule : HubPipelineModule
        {
            protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
            {
                Debug.WriteLine("=> Invoking " + context.MethodDescriptor.Name + " on hub " + context.MethodDescriptor.Hub.Name);
                return base.OnBeforeIncoming(context);
            }

            protected override bool OnBeforeOutgoing(IHubOutgoingInvokerContext context)
            {
                Debug.WriteLine("<= Invoking " + context.Invocation.Method + " on client hub " + context.Invocation.Hub);
                return base.OnBeforeOutgoing(context);
            }
        }
    }
}