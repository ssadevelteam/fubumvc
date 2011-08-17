using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.ActivityStream
{
    public interface IActivityStream<T>
    {
        IQueryable<IActivityItem> Fetch(T subject);
    }
}