using Assets.Scripts.Data.Map;
using Assets.Scripts.GameLogic.Map.Infrastructure;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Mover.Comand
{
    public class ExpandWorldCommand : Command
    {
        private readonly IExpandingWorldChanger _expandingWorldChanger;
        private readonly IMapData _worldData;
        private readonly Vector2Int _size;
        private readonly Vector2Int _targetSize;
        private readonly Command _previousCommand;

        public ExpandWorldCommand(
            IExpandingWorldChanger expandingWorldChanger,
            IMapData worldData,
            Vector2Int targetSize,
            Command previousCommand,
            NextBuildingForPlacingCreator nextBuildingForPlacingCreator)
            : base(expandingWorldChanger, worldData, nextBuildingForPlacingCreator)
        {
            _expandingWorldChanger = expandingWorldChanger;
            _worldData = worldData;
            _size = worldData.Size;
            _targetSize = targetSize;
            _previousCommand = previousCommand;
        }

        public override void Execute()
        {
            _worldData.Size = _targetSize;
            _expandingWorldChanger.Expand(_targetSize);
        }

        public override void Undo()
        {
            _worldData.Size = _size;
            _previousCommand?.Undo();
            _expandingWorldChanger.Expand(_size);
        }
    }
}
