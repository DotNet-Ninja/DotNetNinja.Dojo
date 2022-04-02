namespace DotNetNinja.Dojo.Models;

public abstract class Page
{
    public int TotalItemCount { get; set; }
    public int Number { get; set; }
    public int Size { get; set; }
    public int Count
    {
        get
        {
            if (Size == 0)
            {
                return 0;
            }
            return TotalItemCount % Size == 0 ? TotalItemCount / Size : TotalItemCount / Size + 1;
        }
    }

    public bool CanGoForward => Number < Count;
    public bool CanGoBack => Number > 1;

    public bool HasMultiplePages => Number > 1;

    public int NextPage => (CanGoForward) ? Number + 1 : Number;
    public int PreviousPage => (CanGoBack) ? Number - 1 : Number;
}

public class Page<T> : Page where T : class
{
    public Page()
    {
    }

    public Page(int number, int size, int totalCount, List<T> items)
    {
        Number = number;
        Size = size;
        TotalItemCount = totalCount;
        Items = items;
    }
    public List<T> Items { get; set; } = new();
}