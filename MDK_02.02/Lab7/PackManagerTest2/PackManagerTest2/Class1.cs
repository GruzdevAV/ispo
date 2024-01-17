namespace PackManagerTest2
{
    public class Lab7Class
    {
        public string PublicString { get; set; } = "Public";
        internal string InternalString { get; set; } = "Internal";
        public string AccessToInternalString { get => InternalString; set => InternalString = value; }
    }
}
