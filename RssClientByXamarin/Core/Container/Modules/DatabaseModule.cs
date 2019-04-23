using Autofac;
using Core.Database;
using Core.Extensions;

namespace Core.Container.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RealmDatabase>().NotNull().AsSelf().NotNull().SingleInstance();
        }
    }
}
