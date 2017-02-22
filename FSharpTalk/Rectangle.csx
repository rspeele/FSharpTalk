/// <summary>
/// Represents an immutable rectangle.
/// </summary>
public class Rectangle
{
    private readonly int _precomputedArea;
    public Rectangle(int top, int left, int width, int height)
    {
        Top = top;
        Left = left;
        Width = width;
        Height = height;
        // Calculate area when constructed.
        _precomputedArea = width * height;
    }
    public int Top { get; }
    public int Left { get; }
    public int Width { get; }
    public int Height { get; }
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
