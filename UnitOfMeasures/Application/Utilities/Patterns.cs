namespace UnitOfMeasures.Application.Utilities
{
    public static class Patterns
    {
       // public const string FolrmulaPattern = @"^\s*([\(]*([-+]?\s*(\d+|[a]))(\s*[-+*/]\s*[\(]*([-+]?\s*(\d+|[a]))[\)]*)*[\)]*)\s*$";
        public const string FolrmulaPattern = @"^\s*([\(]*([-+]?\s*((\d+([.]?\d+)?)|[a]))(\s*[-+*/]\s*[\(]*([-+]?\s*((\d+([.]?\d+)?)|[a]))[\)]*)*[\)]*)\s*$"; //supprot float numbers

        public const string ParenthesesPattern = @"^[^()]*(((?'Open'\()[^()]*)+((?'Close-Open'\))[^()]*)+)*(?(Open)(?!))$";
    }
}
