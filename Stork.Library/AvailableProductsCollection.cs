using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stork.Library
{
    public class AvailableProductsCollection : List<AvailableProduct>
    {
        private AvailableProductsCollection()
        {


        }


        static public async Task<AvailableProductsCollection> FromUrlAsync(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var result = await httpClient.GetAsync(url);
                var text = await result.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                using (var reader = new StringReader(text))
                using (var csv = new CsvReader(reader))
                {
                    var records = csv.GetRecords<AvailableProduct>();

                    AvailableProductsCollection availableProducts = new AvailableProductsCollection();
                    availableProducts.AddRange(records);
                    return availableProducts;
                }

            }
        }
    }
}

