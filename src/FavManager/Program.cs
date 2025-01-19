using FavManager.Core;
using Spectre.Console;

var bookmarks = new ChromeBookmarkNavigator().Flatten();

var table = new Table();
table.AddColumn(new("Index"));
table.AddColumn(new("Name"));
table.AddColumn(new("Url"));
table.AddColumn(new("Order"));

var index = 0;
foreach (var bookmark in bookmarks) table.AddRow((++index).ToString(), bookmark.Name, bookmark.Url, bookmark.Order);
AnsiConsole.Write(table);