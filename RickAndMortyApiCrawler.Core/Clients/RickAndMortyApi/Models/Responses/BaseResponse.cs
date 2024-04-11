using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RickAndMortyApiCrawler.Core.Clients.RickAndMortyApi.Models.Responses;
public class BaseResponse
{
    public BaseResponse() { }
    public ResponseInfo Info { get; set; }
    //public List<T> Results { get; set; }
}
