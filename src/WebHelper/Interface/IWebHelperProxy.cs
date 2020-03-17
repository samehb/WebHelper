namespace WebHelper.Interface
{
    interface IWebHelperProxy
    {
        void Start(string filter); // filter is the filter that we use to display records on the gridview.
        void Stop();
    }
}
