namespace Dukkantek.Inventory.Infrastructure.Services
{
     public class AuthMessageSenderOptions
     {
          public string SendGridUser { get; set; } = "Dukkantek SMTP";
          public string SendGridKey { get; set; } = "";
          public string SendingEmail { get; set; } = "noreply@dukkantek.com";
          public string ReplyToEmail { get; set; } = "info@dukkantek.com";
          public string SendingName { get; set; } = "Dukkantek";
          public string TemplateId { get; set; } = "Dukkantek";
          public bool AllowEmail { get; set; } = true;
     }
}
