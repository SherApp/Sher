using Sher.Core.Base;

namespace Sher.Core.Access.Rules
{
    public class PasswordSecurityRule : IBusinessRule
    {
        private readonly string _password;

        public PasswordSecurityRule(string password)
        {
            _password = password;
        }

        public string Check()
        {
            return _password switch
            {
                {Length: < 6} => "Password must have at least 6 characters",
                _ => null
            };
            
        }
    }
}