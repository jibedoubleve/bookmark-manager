using System.Text.Json.Nodes;

namespace FavManager.Core;

public class ChromeBookmarkNavigator
    : IBookmarkNavigator
{
    #region Fields

    private static readonly string Path = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Google\Chrome\User Data\Default\Bookmarks");

    #endregion

    #region Methods

    private void Flatten(JsonNode? jsonNode, List<Bookmark> results)
    {
        if (jsonNode is null) return;

        if (jsonNode is JsonArray array)
        {
            foreach (var item in array) Flatten(item, results);
            return;
        }

        if (jsonNode is JsonObject jsonObject)
        {
            if (jsonObject.ContainsKey("children"))
            {
                Flatten(jsonObject["children"], results);
                return;
            }

            var name = jsonObject["name"]?.ToString();
            var url = jsonObject["url"]?.ToString();
            var order = jsonObject["date_last_used"]?.ToString() ?? "0";

            if (name is not null && url is not null) results.Add(new(name, url, order));
        }
    }

    public IEnumerable<Bookmark>  Flatten()
    {
        var json = File.ReadAllText(Path);
        var node = JsonNode.Parse(json);
        var results = new List<Bookmark>();

        var startNode = node?["roots"]?["bookmark_bar"];
        if (startNode is not null) Flatten(startNode, results);
        return results.OrderByDescending(e => e.Order);
    }

    #endregion
}