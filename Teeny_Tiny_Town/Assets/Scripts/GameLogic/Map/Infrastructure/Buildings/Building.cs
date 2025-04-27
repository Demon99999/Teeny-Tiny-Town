using Assets.Scripts.GameLogic.Map.Representation.Tiles;
using Assets.Scripts.Services.StaticDataServices.Configs.Buildings;

namespace Assets.Scripts.GameLogic.Map.Infrastructure.Buildings
{
    public class Building
    {
        public Building(BuildingType type)
        {
            Type = type;
        }

        public BuildingType Type { get; protected set; }

        public virtual void CreateRepresentation(TileRepresentation tileRepresentation, bool isAnimate, bool waitForCompletion)
        {
            tileRepresentation.TryChangeBuilding<BuildingRepresentation>(Type, isAnimate, waitForCompletion);
        }
    }
}