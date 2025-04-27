using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using Assets.Scripts.Services.PersistantProgrssService;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public abstract class MoveCommand : Command
    {
        protected readonly IPersistantProgrss PersistentProgressService;

        private readonly uint _ramainMovesCount;

        public MoveCommand(
            IWorldChanger worldChanger,
            IMapData mapData,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator,
            IPersistantProgrss persistentProgressService)
            : base(worldChanger, mapData, nextBuildingForPlacingCreator)
        {
            PersistentProgressService = persistentProgressService;
            _ramainMovesCount = PersistentProgressService.Progress.GameplayMovesCounter.RemainingMovesCount;
        }

        public override void Execute()
        {
            PersistentProgressService.Progress.GameplayMovesCounter.Move();
        }

        public override void Undo()
        {
            PersistentProgressService.Progress.GameplayMovesCounter.SetCount(_ramainMovesCount);
            base.Undo();
        }
    }
}
