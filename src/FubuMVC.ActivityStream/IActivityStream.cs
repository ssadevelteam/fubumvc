using System.Collections.Generic;

namespace FubuMVC.ActivityStream
{
    public interface IActivityStream<T>
    {
        IEnumerable<IActivityItem> Fetch(T subject);
    }
}