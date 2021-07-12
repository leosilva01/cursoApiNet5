using System;

namespace Api.Domain.Models
{
    public class BaseModel
    {
        private Guid _id;
        private DateTime _createAt;
        private DateTime _updateAt;
        
        public Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public DateTime CreateAt
        {
            get { return _createAt; }
            set { _createAt = value == null ? DateTime.UtcNow : value; }
        }

        public DateTime UpdateAt 
        {
             get {return _updateAt; } 
             set { _updateAt = value; }
        }
    }
}