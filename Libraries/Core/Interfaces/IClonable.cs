// Date: 2025-07-28



namespace Rune
{
    public interface IClonable<T>
    {
        public T Clone();
    }



    /* Example of IClonable */
    public class IClonableExample : IClonable<IClonableExample>
    {
        public IClonableExample Clone()
        {
            return new()
            {

            };
        }
    }
}