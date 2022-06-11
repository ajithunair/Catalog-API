using System;

namespace Catalog.DTOs
{
    public record ItemDto
    {
        public Guid Id { get; set; }
        public string Name {get; set;}
        public decimal Price {get; set;}
        public DateTimeOffset CreatedDate {get; set;}
    }
}