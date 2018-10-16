namespace PluralsightWinFormsDemoApp
{
    public interface ISettingsService
    {
        void Save();
        bool FirstRun { get; set; }
    }
}