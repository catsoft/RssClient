using Autofac;
using Shared.Database;
using Shared.Extensions;

namespace Shared.Container.Modules
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
