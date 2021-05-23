using System;

namespace BookApi.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }

        // nullable
        public DateTime? DatePublished { get; set; }
    }
}
