using System.Collections.Generic;
using evo.Core;
using evo.Core.Extensions;

namespace evo
{
    public class ArgumentParser
    {
        IList<string> _args;

        IDictionary<string, string> _settings;

        public ArgumentParser(IEnumerable<string> args)
        {
            _args = new List<string>(args);
            IsValid = true;
        }

        public ArgumentParser(string commandLine)
            :this(commandLine.Split(' '))
        {
        }

        public string Option(string name)
        {
            if(_settings == null)
                Parse();

            return _settings.ContainsKey(name) ? _settings[name] : null;
        }

        void Parse()
        {
            _settings = new Dictionary<string, string>();

            while (_args.Count > 0)
            {
                string arg = _args.ObtainAndRemove(0);

                if (arg.StartsWith("--"))
                {
                    arg = arg.TrimStart('-');                    
                    if (_args.Count == 0)
                    {
                        IsValid = false;
                        ErrorMessage = "Expected value for: " + arg;
                    }

                    string value = _args.ObtainAndRemove(0);
                    _settings.Add(arg, value);                    
                }
                else
                {
                    _settings.Add(arg, string.Empty);
                }
            }
        }

        public bool IsValid { get; private set; }

        public void SetOptions(EvoOptions options)
        {
            options.Command = _args.ObtainAndRemove(0);

            if(_settings == null)
                Parse();

            foreach (var key in _settings.Keys)
            {
                var value = _settings[key];
                if(! value.IsNullOrEmpty())
                    ParseArgumentAndValue(key, value, options);
                else
                    options.AdditionalArgs.Add(key);
            }
        }

        public string ErrorMessage { get; private set; }
        
        private void ParseArgumentAndValue(string arg, string value, EvoOptions options)
        {
            switch (arg)
            {
                case "server":
                    options.ServerName = value;
                    break;

                case "db":
                    options.Database = value;
                    break;

                case "username":
                    options.Database = value;
                    break;

                case "password":
                    options.Database = value;
                    break;

                default:
                    IsValid = false;
                    ErrorMessage = "Unknown argument:" + arg;
                    break;
            }            
        }
    }
}