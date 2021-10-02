namespace LocalizationInvestigation.Application.Services
{
    public interface ITranslator<TService>
    {
        string Translate(string key);
        string Translate(string key, params object[] args);
    }
}
