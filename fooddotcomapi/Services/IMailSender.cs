namespace fooddotcomapi.Services
{
    public interface IMailSender
    {
        public (bool IsSuccess, string ErrorMessage) SendEmail(string userEmail, string subject, string confirmationLink);
        public (bool IsSuccess, string ErrorMessage) SendEmail(string senderEmail, string userEmail, string subject, string confirmationLink);
    }
}
