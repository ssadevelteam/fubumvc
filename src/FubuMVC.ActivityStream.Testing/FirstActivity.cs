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
}