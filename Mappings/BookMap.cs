using FluentNHibernate.Mapping;
using LoginAuthorDemo.Models;


namespace LoginAuthorDemo.Mappings
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Table("Books");
            Id(b => b.Id).GeneratedBy.Identity();
            Map(b => b.Name);
            Map(b => b.Genre);
            Map(b => b.Description);
            References(b => b.Author).Column("AuthorId").Cascade.None().Nullable();
        }
    }
}