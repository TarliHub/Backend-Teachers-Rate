namespace TeacherRate.Api.Models.Requests;

public class PageRequest
{
    private int _page;
    private int _size;

    public int Page
    {
        get => _page;
        set
        {
            if (value < 0)
                throw new ArgumentException("Page must be greater than zero", nameof(_page));

            _page = value;
        }
    }
    public int Size
    {
        get => _size;
        set
        {
            if (value < 0)
                throw new ArgumentException("Page must be greater than zero", nameof(_size));

            _size = value;
        }
    }
}
