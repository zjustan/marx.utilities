namespace Marx.Utilities
{
    public interface IContract
    {
        bool IsValidOn(object target); 
        void Setup(object target);
    }
    
}