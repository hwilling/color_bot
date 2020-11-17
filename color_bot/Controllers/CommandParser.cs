using System.Text.RegularExpressions;

namespace color_bot.Controllers
{
    internal class CommandParser
    {
        private readonly string _hexRegex = @"(?i)[0-9a-f]{6}";

        public CommandParser() { }

        /// <summary>
        /// Check if the expression produces a match with the regular expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>true if color is considered valid, elsewise false</returns>
        public bool IsValidColor(string expression) => new Regex(_hexRegex).IsMatch(expression);
    }
}
