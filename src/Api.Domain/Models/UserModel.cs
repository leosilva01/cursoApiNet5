using System;

namespace Api.Domain.Models
{
    public class UserModel : BaseModel
    {

        private string _name;
        private string _email;
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        

    }
}