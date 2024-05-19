using CsvHelper;
using MicroShare.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Reflection;

namespace MicroFeedCsv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CsvReadController : ControllerBase
    {
        private readonly Dictionary<int, string> FileDictionary = new();
        private readonly string FileLocation = @"\PriceFiles\";
        private readonly ILogger<CsvReadController> _logger;

        public CsvReadController(ILogger<CsvReadController> logger)
        {
            _logger = logger;
            FileDictionary[1704] = "NortwindPrices.csv";
            FileDictionary[1881] = "NortwindPrices2.csv";
        }

        [HttpGet(Name = "ProcessCsv")]
        public IEnumerable<PriceModel> Get(int indexFile)
        {
            var runDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var file = $"{runDir}{FileLocation}{FileDictionary[indexFile]}";
            using (var reader = new StreamReader(file))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<PriceModel>();
                //TBD File data to be processed/verified here
                return records.ToList();
            }
        }
    }
}
