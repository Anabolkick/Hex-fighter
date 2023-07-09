using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Builders;
using CrashKonijn.Goap.Configs.Interfaces;
using CrashKonijn.Goap.Resolver;
using Enemies;

public class GoapSetConfigFactory : GoapSetFactoryBase
{
    public override IGoapSetConfig Create()
    {
        var builder = new GoapSetBuilder("GettingStartedSet");
        
        // Goals
        builder.AddGoal<MoveGoal>()
            .AddCondition<IsMoving>(Comparison.GreaterThanOrEqual, 1);

        // Actions
        builder.AddAction<MoveAction>()
            .SetTarget<MoveTarget>()
            .AddEffect<IsMoving>(true)
            .SetBaseCost(1)
            .SetInRange(0.3f);

        // Target Sensors
        builder.AddTargetSensor<MoveTargetSensor>()
            .SetTarget<MoveTarget>();

        // World Sensors
        // This example doesn't have any world sensors. Look in the examples for more information on how to use them.

        return builder.Build();
    }
}