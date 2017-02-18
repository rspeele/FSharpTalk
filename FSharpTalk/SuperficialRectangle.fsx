/// Some differences between languages are just superficial.
type Rectangle(top : int, left : int, width : int, height : int) =
    // Calculcate area when constructed.
    let precomputedArea = width * height

    member this.Top : int = top
    member this.Left : int = left
    member this.Width : int = width
    member this.Height : int = height
    member this.Right : int = left + width
    member this.Bottom : int = top + height
    member this.Area : int = precomputedArea
    member this.Perimeter : int = width * 2 + height * 2
    /// Returns true if argument is fully within the bounds of this
    /// rectangle
    member this.Contains(inner : Rectangle) : bool =
        inner.Top <= this.Top
        && inner.Bottom >= this.Bottom
        && inner.Left >= this.Left
        && inner.Right <= this.Right
    override this.ToString() : string =
        sprintf "(%d,%d),(%d,%d)"
            this.Top this.Left
            this.Bottom this.Right
