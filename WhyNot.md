* Compilation order
* Main reason by far: frameworks -- no template for ASP.Net MVC, not suitable for declaring EF POCOs
* Big language with lots of syntax since it has 90% of C# features + features borrowed from OCaml + new features
* Missing features:
  * No string interpolation (C# 6 feature)
  * No covariance/contravariance (an IEnumerable<Cat> cannot be used as an IEnumerable<Animal> without explicitly converting)
  * No implicit casts