using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Person
    {
        [Key]
        public virtual string _id { get; set; }

        public virtual int index { get; set; }
        public virtual Guid guid { get; set; }
        public virtual bool isActive { get; set; }
        public virtual string balance { get; set; }
        public virtual string picture { get; set; }
        public virtual int age { get; set; }
        public virtual string eyeColor { get; set; }
        public virtual string name { get; set; }
        public virtual string gender { get; set; }
        public virtual string company { get; set; }
        public virtual string email { get; set; }
        public virtual string phone { get; set; }
        public virtual string address { get; set; }
        public virtual string about { get; set; }
        public virtual string registered { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
        public virtual string greeting { get; set; }
        public virtual string favoriteFruit { get; set; }
    }
}