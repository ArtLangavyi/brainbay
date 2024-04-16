using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.WebApi.Models.Responses.Characters;
public class CharacterResponseBase
{
    public int? NextPageNumber { get; set; }
    public int? PreviousPageNumber { get; set; }
    public IEnumerable<CharacterResponse> Characters { get; set; }
}
