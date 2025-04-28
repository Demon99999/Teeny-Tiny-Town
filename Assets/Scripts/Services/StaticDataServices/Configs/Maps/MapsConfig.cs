using Assets.Scripts.Audio;
using Assets.Scripts.GameLogic.Collections;
using Assets.Scripts.GameLogic.Map;
using Assets.Scripts.UI.Screens.Map.Panels;
using Assets.Scripts.UI.Screens.SelectionMap;
using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs.Maps
{
    [CreateAssetMenu(fileName = "MapsConfig", menuName = "StaticData/Create new maps config", order = 51)]
    public class MapsConfig : ScriptableObject
    {
        [SerializeField] private Vector3 _currentWorldPosition;
        [SerializeField] private float _distanceBetweenWorlds;
        [SerializeField] private uint _availableMovesCount;
        [SerializeField] private uint[] _goals;
        [SerializeField] private string _educationMapdId;
        [SerializeField] private Map _educationMap;
        [SerializeField] private RotationPanel _rotationPanel;
        [SerializeField] private WorldWalletSoundPlayer _worldWalletSoundPlayer;
        [SerializeField] private UiSoundPlayer _uiSoundPlayer;
        [SerializeField] private CollectionItemCreator _collectionItemCreator;
        [SerializeField] private PeculiarityIconPanel _peculiarityIconPanel;

        public Vector3 CurrentWorldPosition => _currentWorldPosition;

        public uint AvailableMovesCount => _availableMovesCount;

        public float DistanceBetweenWorlds => _distanceBetweenWorlds;

        public uint[] Goals => _goals;

        public string EducationMapdId => _educationMapdId;

        public Map EducationMap => _educationMap;

        public RotationPanel RotationPanel => _rotationPanel;

        public WorldWalletSoundPlayer WorldWalletSoundPlayer => _worldWalletSoundPlayer;

        public UiSoundPlayer UiSoundPlayer => _uiSoundPlayer;

        public CollectionItemCreator CollectionItemCreator => _collectionItemCreator;

        public PeculiarityIconPanel PeculiarityIconPanel => _peculiarityIconPanel;
    }
}