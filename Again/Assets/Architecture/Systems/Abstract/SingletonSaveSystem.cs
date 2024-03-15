namespace DeepSystem.General
{
    public abstract class SingletonSaveSystem<T> : SingletonSystem<T> where T : SingletonSaveSystem<T>
    {
        protected abstract void SaveSettings();
        protected abstract void LoadSettings();
        protected abstract void DropSettings();
    }
}
