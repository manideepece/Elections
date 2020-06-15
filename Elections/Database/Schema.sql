Drop table Vote
Drop table CandidateCategory
Drop table Voters
Drop table Candidates
Drop table Category
Drop table Client

Create table Client
(
	ClientId varchar(255),
	ClientSecret varchar(max)
)


Create table Voters
(
	Id int Identity(1,1) primary key,
	FirstName varchar(50) Not null,
	LastName varchar(50) not null,
	age int not null, 
	check (age >= 18)
)

Create table Candidates
(
	Id int Identity(1,1) primary key,
	FirstName varchar(50) Not null,
	LastName varchar(50) not null,
	age int not null, 
	check (age >= 18)
)

Create table Category
(
	Id int Identity(1,1) primary key,
	Name varchar(50) not null
)

Create table CandidateCategory
(
	Id int Identity(1,1) primary key,
	CategoryId int not null,
	UserId int not null unique,
	FOREIGN KEY (CategoryId) REFERENCES Category(Id),
	FOREIGN KEY (UserId) REFERENCES Candidates(Id)  
)

Create table Vote
(
	Id int Identity(1,1) primary key,
	VoterId int not null,
	CandidateId int not null,
	CategoryId int not null,
	UserId int not null,
	FOREIGN KEY (CategoryId) REFERENCES Category(Id),
	FOREIGN KEY (UserId) REFERENCES Candidates(Id),
	FOREIGN KEY (CandidateId) REFERENCES CandidateCategory(Id),
)
