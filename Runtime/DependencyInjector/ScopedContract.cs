namespace Marx.Utilities
{
    public class ScopedContract : IContract
    {
        public bool IsValidOn(object target) => false;

        public void Setup(object target) {}
    }
}