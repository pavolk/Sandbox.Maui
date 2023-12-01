using Prins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace ProductInformationApp.Services
{
    public class PrinsService : IPrinsService
    {
        XmlSerializer _serializer = new XmlSerializer(typeof(Prins.Prins));

        public PrinsService() { }

        public async Task<Product> GetByIdAsync(int id)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(@"testdata.xml");
            using var reader = new StreamReader(stream);
            var root = _serializer.Deserialize(reader) as Prins.Prins;
            return root.Product;
        }
    }
}
