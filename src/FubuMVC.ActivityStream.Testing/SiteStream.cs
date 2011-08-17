using System;
using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.ActivityStream.Testing
{
    public class SiteStream : IActivityStream<Site>
    {
        public IQueryable<IActivityItem> Fetch(Site subject)
        {
            throw new NotImplementedException();
        }
    }
}