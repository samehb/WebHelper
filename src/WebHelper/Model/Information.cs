namespace WebHelper.Model
{
    // This model represents the information associated with urls. We instantiate it, and pass it to the gridview to display the data.
    public class Information
    {
        public string Url { get ; set; }
        public string Method { get; set; }
        public string Agent { get; set; }
        public string Program { get; set; }
        public string Action { get; set; }

        public Information(string url, string method, string agent, string program, string action)
        {
            Url = url;
            Method = method;
            Agent = agent;
            Program = program;
            Action = action;
        }

    }
}
