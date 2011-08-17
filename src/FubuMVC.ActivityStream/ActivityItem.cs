namespace FubuMVC.ActivityStream
{
    public class ActivityItem : IActivityItem
    {
        public string Type { get; set; }

        public string Json { get; set; }
    }
}