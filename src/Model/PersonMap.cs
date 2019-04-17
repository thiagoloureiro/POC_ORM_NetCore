using FluentNHibernate.Mapping;

namespace Model
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(b => b._id);
            Map(b => b.name);
            Table("Person");
        }
    }
}