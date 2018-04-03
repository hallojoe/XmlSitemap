using System.IO;
using System.Text;

namespace HalloJoe.XmlSitemap
{
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}