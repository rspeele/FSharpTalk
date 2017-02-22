[<AbstractClass>]
type Expression() =
    abstract member Evaluate : unit -> int

type LiteralExpression(value) =
    inherit Expression()
    override this.Evaluate() = value
    override this.ToString() = string value

type AdditionExpression(left : Expression, right : Expression) =
    inherit Expression()
    override this.Evaluate() = left.Evaluate() + right.Evaluate()
    override this.ToString() = sprintf "(%O + %O)" left right

type MultiplicationExpression(left : Expression, right : Expression) =
    inherit Expression()
    override this.Evaluate() = left.Evaluate() * right.Evaluate()
        override this.ToString() = sprintf "(%O * %O)" left right

// etc. for each of our expression types -- usually these would be written in separate files