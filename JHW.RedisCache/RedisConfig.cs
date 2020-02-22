using System.Xml;

namespace JHW.RedisCache
{
    class RedisConfig : System.Configuration.IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            var options = new StackExchange.Redis.ConfigurationOptions();
            foreach (var item in section.ChildNodes)
            {
                var node = item as XmlNode;
                if (null == node || node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                var host = node.Attributes["host"].Value;
                if (!string.IsNullOrEmpty(host) && int.TryParse(node.Attributes["port"].Value, out int port))
                {
                    options.EndPoints.Add(host, port);
                }
            }

            return options;
        }
    }
}
