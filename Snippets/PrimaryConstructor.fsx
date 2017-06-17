
type Point(x : int, y : int) =
    member this.X = x
    member this.Y = y
    override this.ToString() =
        x.ToString() + "," + y.ToString()

type IShape =
    abstract member Area : float

type Rectangle(corner1 : Point, corner2 : Point) =
    member this.Width = abs(corner2.X - corner1.X)
    member this.Height = abs(corner2.Y - corner1.Y)
    interface IShape with
        member this.Area = float(this.Width * this.Height)