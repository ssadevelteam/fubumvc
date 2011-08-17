using System;
using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.ActivityStream.Testing
{
    public class ContactStream : IActivityStream<Contact>
    {
        public IQueryable<IActivityItem> Fetch(Contact subject)
        {
            throw new NotImplementedException();
        }
    }
}