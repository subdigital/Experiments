using System.Collections.Generic;
using evo.Core;
using evo.Core.Extensions;

namespace evo
{
    public class ArgumentParser
    {
        IList<string> _args;

        public ArgumentParser(IEnumerable<string> args)
        {
            _args = new List<string>(args);
            IsValid = true;
        }

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public EvoOptions BuildEvoOptions()
        {
            var options = new EvoOptions { Command = _args.ObtainAndRemove(0) };

            bool argsValid = ParseArgs(options);
            if (!argsValid)
                return null;

            return options;
        }

        private bool ParseArgs(EvoOptions options)
        {
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
                    ParseArgumentAndValue(arg, value, options);
                }
                else
                {
                    options.AdditionalArgs.Add(arg);
                }
            }

            return true;
        }

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