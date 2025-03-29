using DevExpress.Data.Browsing;
using System.Data.Linq;
using System.Diagnostics;

namespace ProjectTimelyIn.Services
{
    public class EmailService 
    {
        private readonly DataContext _dataContext;
        public EmailService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Login(string message)
        {
            Debug.WriteLine($"Sent from Email Login {message}");
        }

        public void Logout(string message)
        {
            Debug.WriteLine($"Sent from Email Logout {message}");
        }
    }
}
