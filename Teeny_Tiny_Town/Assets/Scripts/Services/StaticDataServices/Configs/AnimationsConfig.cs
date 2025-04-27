using UnityEngine;

namespace Assets.Scripts.Services.StaticDataServices.Configs
{
    [CreateAssetMenu(fileName = "AnimationsConfig", menuName = "StaticData/Create new animations config", order = 51)]
    public class AnimationsConfig : ScriptableObject
    {
        [SerializeField] private float _buildingJumpDestroyPower;
        [SerializeField] private float _buildingJumpDestroyDuration;
        [SerializeField] private Vector3 _buildingJumpDestroyOffset;
        [SerializeField] private Vector3 _buildingShakeOffset;
        [SerializeField] private float _tileUpdatingDuration;
        [SerializeField] private float _buildingPutDuration;
        [SerializeField] private float _buildingPutMaxScale;
        [SerializeField] private float _buildingPutMinScale;
        [SerializeField] private float _buildingBlinkingDuration;
        [SerializeField] private float _buildingBlinkingScale;
        [SerializeField] private AnimationCurve _buildingShakeCurve;
        [SerializeField] private int _buildingShakesCount;
        [SerializeField] private float _buildingShakesDuration;
        [SerializeField] private float _windowOpeningStateDuration;
        [SerializeField] private float _worldRotateDuration;
        [SerializeField] private float _worldRotateToStarDuration;
        [SerializeField] private float _worldSimpleRotateDuration;
        [SerializeField] private float _worldMoveDuration;
        [SerializeField] private float _worldMoveToCenterDuration;
        [SerializeField] private float _cameraMoveDuration;
        [SerializeField] private Color _defaultActionHandlerButtonIconColor;
        [SerializeField] private Color _activeActionHandlerButtonIconColor;
        [SerializeField] private Color _activeGainButtonColor;
        [SerializeField] private Color _defaultGainButtonColor;
        [SerializeField] private float _changeGainButtonActiveDuration;
        [SerializeField] private Color _purchasePermittingColor;
        [SerializeField] private Color _prohibitingPurchaseColor;
        [SerializeField] private float _themeChangingDuration;
        [SerializeField] private float _collectionItemMoveDuration;

        public Vector3 BuildingJumpDestroyOffset => _buildingJumpDestroyOffset;

        public float BuildingJumpDestroyPower => _buildingJumpDestroyPower;

        public float BuildingJumpDestroyDuration => _buildingJumpDestroyDuration;

        public float TileUpdatingDuration => _tileUpdatingDuration;

        public float BuildingPutMaxScale => _buildingPutMaxScale;

        public float BuildingPutMinScale => _buildingPutMinScale;

        public float BuildingPutDuration => _buildingPutDuration;

        public float BuildingBlinkingScale => _buildingBlinkingScale;

        public float BuildingBlinkingDuration => _buildingBlinkingDuration;

        public AnimationCurve BuildingShakeCurve => _buildingShakeCurve;

        public int BuildingShakesCount => _buildingShakesCount;

        public float BuildingShakesDuration => _buildingShakesDuration;

        public Vector3 BuildingShakeOffset => _buildingShakeOffset;

        public float WindowOpeningStateDuration => _windowOpeningStateDuration;

        public float WorldRotateDuration => _worldRotateDuration;

        public float WorldRotateToStarDuration => _worldRotateToStarDuration;

        public float WorldSimpleRotateDuration => _worldSimpleRotateDuration;

        public float WorldMoveDuration => _worldMoveDuration;

        public float WorldMoveToCenterDuration => _worldMoveToCenterDuration;

        public float CameraMoveDuration => _cameraMoveDuration;

        public Color ActiveGainButtonColor => _activeGainButtonColor;

        public Color DefaultGainButtonColor => _defaultGainButtonColor;

        public Color ActiveActionHandlerButtonIconColor => _activeActionHandlerButtonIconColor;

        public Color DefaultActionHandlerButtonIconColor => _defaultActionHandlerButtonIconColor;

        public float ChangeGainButtonActiveDuration => _changeGainButtonActiveDuration;

        public Color PurchasePermittingColor => _purchasePermittingColor;

        public Color ProhibitingPurchaseColor => _prohibitingPurchaseColor;

        public float CollectionItemMoveDuration => _collectionItemMoveDuration;

        public float ThemeChangingDuration => _themeChangingDuration;

        public float BuildingShakeTweenDuration => BuildingShakesDuration / BuildingShakesCount;
    }
}