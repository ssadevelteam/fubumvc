using System;
using FubuMVC.Core.UI;
using System.Linq;
using System.Collections.Generic;
using FubuMVC.Core.View;

namespace FubuMVC.ActivityStream
{
    public static class ActivityStreamPageExtensions
    {
        public static void RenderActivityStreamFor<T>(this IFubuPage page, T subject, int maxRecords)
        {
            page.Get<ActivityStreamRunner<T>>().Render(subject, new ActivityStreamViewOptions(){
                MaxRecords = maxRecords
            });   
        }
    }

    public class ActivityStreamViewOptions
    {
        public int MaxRecords { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// TODO -- this gets enhanced a lot, later
    /// </summary>
    public class ActivityStreamRunner<T>
    {
        private readonly IActivityStream<T> _stream;
        private readonly IPartialInvoker _invoker;

        public ActivityStreamRunner(IActivityStream<T> stream, IPartialInvoker invoker)
        {
            _stream = stream;
            _invoker = invoker;
        }

        public void Render(T subject, ActivityStreamViewOptions options)
        {
            var queryable = BuildQueryableFor(subject, options);
            queryable.Each(renderItem);
        }

        // TODO -- this gets fancier later
        private void renderItem(IActivityItem item)
        {
            var activity = ActivityTypes.Hydrate(item);
            _invoker.InvokeObject(activity);
        }

        public IQueryable<IActivityItem> BuildQueryableFor(T subject, ActivityStreamViewOptions options)
        {
            var queryable = _stream.Fetch(subject);

            if (options.MaxRecords > 0)
            {
                queryable = queryable.Take(options.MaxRecords);
            }

            return queryable;
        }
    }
}