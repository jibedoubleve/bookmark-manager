namespace FavManager.Core;

public interface IBookmarkNavigator
{
    #region Methods

    IEnumerable<Bookmark>  Flatten();

    #endregion
}