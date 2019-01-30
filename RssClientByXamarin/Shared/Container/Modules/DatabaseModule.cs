using Autofac;
using Shared.Database;

namespace Shared.Container.Modules
{
    public class DatabaseModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<RealmDatabase>().AsSelf().SingleInstance();
        }
    }
}