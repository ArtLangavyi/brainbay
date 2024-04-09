using RickAndMortyApiCrawler.Domain.Abstractions;

using System.ComponentModel.DataAnnotations;

namespace RickAndMortyApiCrawler.Domain.Models;

public class BaseEntity : BaseEntity<int>
{
}

public class BaseEntity<IdType> : IBase<IdType>
{
    [Key]
    public required IdType Id { get; set; }
}
