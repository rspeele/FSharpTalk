/// Represents an immutable rectangle.
type Rectangle(top, left, width, height) =
    // Calculate area when constructed.
    let precomputedArea = width * height

    member this.Top = top
    member this.Left = left
    member this.Width = width
    member this.Height = height
    member this.Right = left + width
    member this.Bottom = top + height
    member this.Area = precomputedArea
    member this.Perimeter = width * 2 + height * 2
    /// Returns true if argument is fully within the bounds of this
    /// rectangle
    member this.Contains(inner : Rectangle) =
        inner.Top <= this.Top
        && inner.Bottom >= this.Bottom
        && inner.Left >= this.Left
        && inner.Right <= this.Right
    override this.ToString() =
        sprintf "(%d,%d),(%d,%d)" this.Top this.Left this.Bottom this.Right
