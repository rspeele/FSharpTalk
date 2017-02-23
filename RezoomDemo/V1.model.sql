create table Users
	( Id int primary key autoincrement
	, Email string
	, Name string
	);

create table ToDos
	( Id int primary key autoincrement
	, ParentId int null references ToDos(Id) -- for hierarchical to-do items
	, CreatedById int references Users(Id)
	, Heading string(256)
	, Paragraph string(512) null
	, Completed bool
	);

create view ActiveToDos as
	select * from ToDos where Completed = true;