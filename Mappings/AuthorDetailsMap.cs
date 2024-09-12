using FluentNHibernate.Mapping;
using LoginAuthorDemo.Models;

namespace LoginAuthorDemo.Mappings
{
    public class AuthorDetailsMap : ClassMap<AuthorDetails>
    {
        public AuthorDetailsMap()
        {
            Table("AuthorDetails");
            Id(ad => ad.Id).GeneratedBy.Identity();
            Map(ad => ad.Street);
            Map(ad => ad.City);
            Map(ad => ad.State);
            Map(ad => ad.Country);
            References(ad => ad.Author).Column("AuthorId").Unique().Cascade.None();
        }
    }
}