namespace Assets.Scripts.GameLogic.Map.Screens
{
    public interface IWorldWindows
    {
        bool IsRegistered { get; }

        void Register();
        void Remove();
    }
}