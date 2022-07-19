namespace UnitOfMeasures.Application.Utilities
{
    public static class Patterns
    {
        public const string SimpleFormulaPattern = @"^\s*([-+]?\s*((\d+([.]?\d+)?)|[a])(\s*[-+*/]\s*([-+]?\s*((\d+([.]?\d+)?)|[a]))))\s*$";
        public const string FolrmulaPattern = @"^\s*([\(]*([-+]?\s*((\d+([.]?\d+)?)|[a]))(\s*[-+*/]\s*[\(]*([-+]?\s*((\d+([.]?\d+)?)|[a]))[\)]*)*[\)]*)\s*$"; //supprot float numbers
        public const string FormulaParenthesesLessPattern = @"^\s*([-+]?\s*((\d+([.]?\d+)?)|[a])(\s*[-+*/]\s*([-+]?\s*((\d+([.]?\d+)?)|[a])))+)\s*$";
        public const string ParenthesesPattern = @"^[^()]*(((?'Open'\()[^()]*)+((?'Close-Open'\))[^()]*)+)*(?(Open)(?!))$";
        public const string LeftSideFormulaPattern = @"\s*([-+]?\s*((\d+([.]?\d+)?)|[a])(\s*[-+*/]\s*))\s*";
    }
}
