namespace Models
{
    public class EmailConfig
    {
        public string FromName { get; set; }
        public string FromAddress { get; set; }

        public string MailServerAddress { get; set; }
        public int MailServerPort { get; set; }

        public bool EnableSsl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class AdministratorContact
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}