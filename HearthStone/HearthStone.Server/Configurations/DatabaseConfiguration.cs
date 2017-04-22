using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace HearthStone.Server.Configurations
{
    public class DatabaseConfiguration
    {
        public static bool Load(string filePath, out DatabaseConfiguration configuration)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DatabaseConfiguration));
            if (File.Exists(filePath))
            {
                using (XmlReader reader = XmlReader.Create(filePath))
                {
                    if (serializer.CanDeserialize(reader))
                    {
                        configuration = (DatabaseConfiguration)serializer.Deserialize(reader);
                        return true;
                    }
                    else
                    {
                        configuration = null;
                        return false;
                    }
                }
            }
            else
            {
                configuration = new DatabaseConfiguration
                {
                    Hostname = "not set",
                    Username = "not set",
                    Password = "not set",
                    Database = "not set"
                };
                using (XmlWriter writer = XmlWriter.Create(filePath))
                {
                    serializer.Serialize(writer, configuration);
                }
                return true;
            }
        }

        [XmlElement]
        public string Hostname { get; set; }
        [XmlElement]
        public string Username { get; set; }
        [XmlElement]
        public string Password { get; set; }
        [XmlElement]
        public string Database { get; set; }
    }
}
