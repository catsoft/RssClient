﻿using Autofac;
using RssClient.Repository;
using Shared.Repository;

namespace Shared.Container.Modules
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RssRepository>().As<IRssRepository>().SingleInstance();
            builder.RegisterType<RssMessagesRepository>().As<IRssMessagesRepository>().SingleInstance();
        }
    }
}
