using VContainer;
using VContainer.Unity;

namespace General
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterEntryPoint<EventBus.EventBus>(Lifetime.Singleton).AsSelf();
        }
    }
}