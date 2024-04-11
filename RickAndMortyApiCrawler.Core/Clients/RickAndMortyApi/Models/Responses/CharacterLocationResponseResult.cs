using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;

public class CharacterLocationResponseResult
{
    public int id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string dimension { get; set; }
    public List<string> residents { get; set; }
    public string url { get; set; }
    public DateTime created { get; set; }
}