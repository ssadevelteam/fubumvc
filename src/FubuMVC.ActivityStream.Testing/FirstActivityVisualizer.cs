using HtmlTags;

namespace FubuMVC.ActivityStream.Testing
{
    public class FirstActivityVisualizer
    {
        public HtmlTag Visualize(FirstActivity activity)
        {
            return new HtmlTag("div");
        }

        public object WrongWithTooManyParameters(FirstActivity activity, int count)
        {
            return null;
        }

        public object WrongBecauseTheInputIsNotAnActivity(NotAnActivity not)
        {
            return null;
        }

        public void WrongBecauseThereIsNotOutput(FirstActivity activity)
        {
            
        }
    }
}