using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.ComponentModel.Composition;
using System;
using System.Linq;
using System.Diagnostics.Contracts;

namespace Utilla.Web.Protocols
{
    public class Sitemap : List<SitemapUrl>
    {
        #region Constructors
        public Sitemap()
        { 
        }
        
        public Sitemap(int capacity) 
            : base(capacity) 
        {
            Contract.Requires<ArgumentOutOfRangeException>(capacity >= 0, "capacity is less than zero");
        }

        public Sitemap(IEnumerable<SitemapUrl> collection): base(collection){ }
        #endregion Constructors

        public string Xmlns
        {
            get;
            set;
        }

        public void Write(TextWriter output)
        {
            Contract.Requires<ArgumentNullException>(output != null, "output is null");

            using (XmlTextWriter writer = new XmlTextWriter(output))
            {
                //writer.WriteStartDocument();
                writer.WriteStartElement("urlset");
                writer.WriteAttributeString("xmlns", this.Xmlns);
                writer.WriteAttributeString("xmlns:geo", "http://www.google.com/geo/schemas/sitemap/1.0");

                foreach (var url in this.OrderBy(item => item.Loc))
                {
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", url.Loc);

                    WriteLastMod(writer, url);
                    WriteChangeFreq(writer, url);
                    WritePriority(writer, url);
                    WriteKeyholeData(writer, url);

                    writer.WriteEndElement();

                }

                writer.WriteEndElement();
                writer.Flush();
            }
        }

        private static void WriteKeyholeData(XmlTextWriter writer, SitemapUrl url)
        {
            if (url.Loc.EndsWith(".kml")) // See http://code.google.com/apis/kml/documentation/kmlSearch.html
            {
                writer.WriteStartElement("geo:geo");
                writer.WriteElementString("geo:format", "kml");
                writer.WriteEndElement();
            }
        }

        private static void WritePriority(XmlTextWriter writer, SitemapUrl url)
        {
            if (!string.IsNullOrEmpty(url.Priority))
            {
                writer.WriteElementString("priority", url.Priority);
            }
        }

        private static void WriteChangeFreq(XmlTextWriter writer, SitemapUrl url)
        {
            if (!string.IsNullOrEmpty(url.ChangeFreq))
            {
                writer.WriteElementString("changefreq", url.ChangeFreq);
            }
        }

        private static void WriteLastMod(XmlTextWriter writer, SitemapUrl url)
        {
            if (!string.IsNullOrEmpty(url.LastMod))
            {
                writer.WriteElementString("lastmod", url.LastMod);
            }
        }

        public override string ToString()
        {
            using (StringWriter result = new StringWriter())
            {
                this.Write(result);
                return result.ToString();
            }
        }
    }
}
