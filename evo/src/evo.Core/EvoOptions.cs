
namespace evo.Core
{
    public class EvoOptions
    {
        private const string DefaultScriptDirectory = "migrations";
        private const string DefaultConfigPath = DefaultScriptDirectory + "\\evo.config";

        public EvoOptions()
        {
            ScriptDirectory = DefaultScriptDirectory;
            ConfigPath = DefaultConfigPath;
            TrustedConnection = true;
        }

        //settings
        public string ConfigPath { get; set; }
        public string ScriptDirectory { get; set; }
        public string ServerName { get; set; }
        public string Database { get; set; }
        public bool TrustedConnection { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //command / behavior
        public string Command { get; set; }
        public string[] AdditionalArgs { get; set; }
    }
}
