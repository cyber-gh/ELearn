using System;

namespace ELearn.Domain
{
    public class Category: IEntity
    {
        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public String Name { get; set; }
    }
}