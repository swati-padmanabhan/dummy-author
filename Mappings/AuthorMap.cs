using FluentNHibernate.Mapping;
using LoginAuthorDemo.Models;


namespace LoginAuthorDemo.Mappings
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Table("Authors");
            Id(a => a.Id).GeneratedBy.GuidComb();
            Map(a => a.UserName);
            Map(a => a.Email);
            Map(a => a.Age);
            Map(a => a.Password);
            HasMany(a => a.Books).Inverse().Cascade.All();
            HasOne(a => a.AuthorDetails).Cascade.All().PropertyRef(a => a.Author)
                .Constrained();
        }
    }
}