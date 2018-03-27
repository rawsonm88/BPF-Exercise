using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using FileData.Exceptions;

namespace FileData.Parsers
{
    /// <summary>
    /// Parser to parse command line arguments and return a list of matched actions
    /// </summary>
    internal class CommandLineParser
    {
        private readonly List<string> _actionTypes;

        internal CommandLineParser()
        {
            this._actionTypes = new List<string>();
        }

        /// <summary>
        /// Add actions to look for when parsing command line arguments
        /// </summary>
        /// <param name="actionName"></param>
        internal void AddActionType(string actionName)
        {
            if(!this._actionTypes.Contains(actionName))
                this._actionTypes.Add(actionName);
        }

        /// <summary>
        /// Parse command line arguments
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        internal List<string> Parse(string[] args)
        {
            var actions = new List<string>();

            // If there are no arguments, return an empty list
            if (args == null) return actions;

            // Regular expression parses -a, --a, /a or --argument
            Regex argumentNameRegex = new Regex(@"^\-{2}(?<argName>[\w]+)|\-(?<argName>[\w]{1})|/(?<argName>[\w]{1})$");

            foreach(var arg in args)
            {
                // Get the argument name
                var argumentName = argumentNameRegex.Match(arg).Groups["argName"].Value;

                // If we didn't recognise this argument, quietly ignore
                if (string.IsNullOrWhiteSpace(argumentName))
                {
                    Debug.WriteLine($"Argument \"{arg}\" not recognised");
                    continue;
                }

                // Check if it matches the beginning or completely an action (case insensitive)
                var actionTypes = this._actionTypes.Where(actionTypeName => actionTypeName.Equals(argumentName, StringComparison.InvariantCultureIgnoreCase) || actionTypeName.StartsWith(argumentName, StringComparison.InvariantCultureIgnoreCase)).ToList();
                
                // If more than one action type is returned, there's something wrong with the parser setup
                if (actionTypes.Count > 1) throw new AmbiguousArgumentException(argumentName);

                // Add to list of actions
                if (actionTypes.Count == 1) actions.Add(actionTypes[0]);
            }

            return actions;
        }
    }
}
