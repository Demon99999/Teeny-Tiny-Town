using Assets.Scripts.Data;

namespace Assets.Scripts.Services.SaveLoadServices
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        PlayerProgres LoadProgres();
    }
}