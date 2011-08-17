namespace FubuMVC.ActivityStream
{
    public static class javascript
    {
        public static JavascriptFunction function(string functionName)
        {
            return new JavascriptFunction(functionName);
        }

        #region Nested type: JavascriptFunction

        public class JavascriptFunction
        {
            private readonly string _functionName;

            public JavascriptFunction(string functionName)
            {
                _functionName = functionName;
            }

            public string javascriptFunction
            {
                get { return "You must use JsonUtil.ToUnsafeJson() to serialize this properly"; }
            }

            public override string ToString()
            {
                return _functionName;
            }
        }

        #endregion
    }
}