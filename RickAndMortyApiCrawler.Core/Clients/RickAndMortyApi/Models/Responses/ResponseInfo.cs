using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;
public class ResponseInfo
{
    public int count { get; set; }
    public int pages { get; set; }
    public string next { get; set; }
    public string prev { get; set; }
}
