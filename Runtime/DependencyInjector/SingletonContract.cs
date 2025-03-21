namespace Marx.Utilities
{
    public class SingletonContract : IContract
    {
        public bool IsValidOn(object target) => true;

        public void Setup(object target) {  }
    
    }
}