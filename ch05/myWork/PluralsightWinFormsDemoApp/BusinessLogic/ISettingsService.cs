namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    public interface ISettingsService
    {
        void Save();
        bool FirstRun { get; set; }
    }
}