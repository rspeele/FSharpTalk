/// <summary>
/// Some differences between languages are just superficial.
/// </summary>
public class Rectangle
{
    private readonly int _top;
    private readonly int _left;
    private readonly int _width;
    private readonly int _height;
    private readonly int _precomputedArea;

    public Rectangle(int top, int left, int width, int height)
    {
        _top = top;
        _left = left;
        _width = width;
        _height = height;
        // Calculate area when constructed.
        _precomputedArea = width * height;
    }

    public int Top => _top;
    public int Left => _left;
    public int Width => _width;
    public int Height => _height;
    public int Right => _left + _width;
    public int Bottom => _top + _height;
    public int Area => _precomputedArea;
    public int Perimeter => _width * 2 + _height * 2;
    /// <summary>
    /// Returns true if argument is fully within the bounds of this
    /// rectangle.
    /// </summary>
    public bool Contains(Rectangle inner) =>
        inner.Top <= this.Top
        && inner.Bottom >= this.Bottom
        && inner.Left >= this.Left
        && inner.Right <= this.Right;
    public override string ToString()
        => $"({this.Top},{this.Left}),({this.Bottom},{this.Right})";
}
