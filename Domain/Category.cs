using System;

namespace ELearn.Domain
{
    public class Category: IEntity
    {
        public Guid Id { get; private set; }
        public String Name { get; set; }
    }
}