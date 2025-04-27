using System;

namespace Assets.Scripts.GameLogic.Map.Infrastructure
{
    public interface IBuildingsUpdatable
    {
        event Action UpdateFinished;
    }
}