using System;

namespace ELearn.Domain
{
    public class AppUser: IEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public Boolean IsConfirmed { get; set; }
        
    }
}