using System;
using System.Linq;
using System.Xml.Linq;
using evo.Core;

namespace evo
{
    public class ConfigParser
    {
        IFileSystem _fileSystem;
        readonly string _configFilename;

        public ConfigParser(IFileSystem fileSystem, string configFilename)
        {
            _fileSystem = fileSystem;
            _configFilename = configFilename;
        }

        public bool ConfigFileExists()
        {
            return _fileSystem.FileExists(_configFilename);
        }

        public void SetOptions(EvoOptions options)
        {
            var xdoc = XDocument.Load(_configFilename);

            if (xdoc.Root == null)
                throw new EvoException("The configuration file has no root element. Expecting root element <evo>");

            if (xdoc.Root.Name != "evo")
                throw new EvoException("Invalid evo config. The root node should be <evo>");

            try
            {
                ParseSettings(xdoc, options);                
            }
            catch(Exception exc)
            {
                throw new EvoException("Error parsing evo config file", exc);
            }
        }

        void ParseSettings(XDocument xdoc, EvoOptions options)
        {
            var settings = from node in xdoc.Root.Nodes()
                           where node is XElement
                           select new {((XElement) node).Name, ((XElement) node).Value};

            foreach (var setting in settings)
            {
                var settingName = setting.Name.ToString();
                switch (settingName)
                {
                    case "db":
                        options.Database = setting.Value;
                        break;

                    case "server":
                        options.ServerName = setting.Value;
                        break;

                    case "trusted":
                        options.TrustedConnection = bool.Parse(setting.Value);
                        break;

                    case "username":
                        options.Username = setting.Value;
                        break;

                    case "password":
                        options.Password = setting.Value;
                        break;

                    default:
                        throw new Exception("Unrecognized configuration option: " + setting.Name);
                }
            }
        }
    }
}