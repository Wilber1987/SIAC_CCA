using APPCORE;
using APPCORE.BDCore.Abstracts;
using DataBaseModel;

public class EmailAccountService : TransactionalClass
{
    public EmailAccounts GetAvailableEmailAccount()
    {
        var today = DateTime.Today;

        var accounts = new EmailAccounts().withConection(this.MDataMapper)
            .Where<EmailAccounts>(FilterData.Or(
                FilterData.Less("SentCount", 250),
                FilterData.Less("LastUsedDate", today)
            ))
            .ToList();

        // Reinicia los contadores para cuentas con fecha anterior a hoy
        foreach (var account in accounts.Where(a => a.LastUsedDate < today))
        {
            account.SentCount = 0;
            account.LastUsedDate = today;
            account.Update();
        }

        // Filtrar y obtener cuenta disponible
        return accounts
            .Where(a => a.SentCount < 300 && (a.LastUsedDate == today || a.LastUsedDate == null))
            .OrderBy(a => a.Id)
            .FirstOrDefault() ?? throw new Exception("No hay cuentas de correo disponibles.");
    }


    public void IncrementEmailSentCount(string email)
    {
        var filter = FilterData.Equal("Email", email);

        var account = new EmailAccounts().withConection(this.MDataMapper).Where<EmailAccounts>(filter).FirstOrDefault();
        if (account != null)
        {
            account.SentCount = (account.SentCount ?? 0) + 1;
            account.LastUsedDate = DateTime.Today;
            account.Update();
        }
        else
        {
            throw new Exception($"No se encontró la cuenta de correo: {email}");
        }
    }

}
