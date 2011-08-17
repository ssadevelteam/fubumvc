using System;
using System.Collections.Generic;
using HtmlTags;

namespace FubuMVC.ActivityStream.Testing
{
    public class FirstActivity : Activity
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public bool Equals(FirstActivity other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Name, Name) && other.Age == Age;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (FirstActivity)) return false;
            return Equals((FirstActivity) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0)*397) ^ Age;
            }
        }
    }
    public class SecondActivity : Activity{}

    [ActivityTypeName("Third")]
    public class ThirdActivity : Activity{}

    public class NotAnActivity{}

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

    public class Contact{}

    public class Case{}

    public class Site{}

    public class ContactStream : IActivityStream<Contact>
    {
        public IEnumerable<IActivityItem> Fetch(Contact subject)
        {
            throw new NotImplementedException();
        }
    }

    public class CaseStream : IActivityStream<Case>
    {
        public IEnumerable<IActivityItem> Fetch(Case subject)
        {
            throw new NotImplementedException();
        }
    }

    public class SiteStream : IActivityStream<Site>
    {
        public IEnumerable<IActivityItem> Fetch(Site subject)
        {
            throw new NotImplementedException();
        }
    }
}