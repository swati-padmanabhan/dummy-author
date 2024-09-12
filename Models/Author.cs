using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LoginAuthorDemo.Models
{
    public class Author
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string UserName { get; set; }

        [Required]
        public virtual string Email { get; set; }

        public virtual int Age { get; set; }

        [Required]
        public virtual string Password { get; set; }

        public virtual IList<Book> Books { get; set; } = new List<Book>(); //many relationship 

        public virtual AuthorDetails AuthorDetails { get; set; } = new AuthorDetails();
    }
}