using Level;

namespace Signals.Level
{
    public class OnElementClickSignal
    {
        public readonly Element Element;
        
        public OnElementClickSignal(Element element)
        {
            Element = element;
        } 
    }
}