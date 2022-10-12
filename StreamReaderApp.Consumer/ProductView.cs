namespace StreamReaderApp.Consumer
{
    public class Context
    {
        public string source { get; set; }
    }

    public class Properties
    {
        public string productid { get; set; }
    }

    public class ProductView
    {
        public string @event { get; set; }
        public string messageid { get; set; }
        public string userid { get; set; }
        public DateTime ViewedDate { get; set; }
        public Properties properties { get; set; }
        public Context context { get; set; }
    }

}
