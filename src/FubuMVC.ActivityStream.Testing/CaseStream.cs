using System;
using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.ActivityStream.Testing
{
    public class CaseStream : IActivityStream<Case>
    {
        public IQueryable<IActivityItem> Fetch(Case subject)
        {
            throw new NotImplementedException();
        }
    }
}