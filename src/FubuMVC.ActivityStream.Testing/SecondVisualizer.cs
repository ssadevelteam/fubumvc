using HtmlTags;

namespace FubuMVC.ActivityStream.Testing
{
    public class SecondVisualizer
    {
        public HtmlDocument Show(SecondActivity activity)
        {
            return new HtmlDocument();
        }

        public HtmlTag Visualize(ThirdActivity activity)
        {
            return new HtmlTag("div");
        }
    }
}