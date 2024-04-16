using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMorty.Web.Models;
public class CharacterResponseBase
{
    public int? nextPageNumber { get; set; }
    public int? previousPageNumber { get; set; }
    public IEnumerable<CharacterResponse> characters { get; set; }
}
