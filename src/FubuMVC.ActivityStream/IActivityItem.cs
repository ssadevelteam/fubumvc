namespace FubuMVC.ActivityStream
{
    // TODO -- probably want date on this later
    public interface IActivityItem
    {
        string Type { get; set; }
        string Json { get; set; }
    }
}