create database LyPlanDatabase;
go
use LyPlanDatabase;
go
create table Task(
	Id int primary key identity,
	Title Nvarchar(255) not null,
	Description
)