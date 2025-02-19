namespace Marx.Utilities
{
    public abstract class Cleaner<T> : ICleaner
    {
        public abstract int Priority { get; }

        public bool TryClean(object obj)
        {
            if (obj is T casted)
            {
                Clean(casted);
                return true;
            }
            return false;
        }

        public abstract void Clean(T input);
    }

    public interface ICleaner
    {
        public int Priority { get; }

        public bool TryClean(object obj);
    }

}