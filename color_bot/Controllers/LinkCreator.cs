namespace color_bot.Controllers
{
    internal class LinkCreator
    {
        private readonly string _hexWebLink = @"https://www.colorhexa.com/";

        public LinkCreator() { }

        /// <summary>
        /// Creates a link to a hex color value based on the hex input.
        /// Currently usees https://www.colorhexa.com/
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>web address to desired hex color</returns>
        public string CreateLink(string hex) => _hexWebLink + hex;
    }
}
