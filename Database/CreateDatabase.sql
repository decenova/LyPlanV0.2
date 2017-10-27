create database LyPlanDatabase;
go
use LyPlanDatabase;
go

create table [Type](
	Id int primary key identity,
	Name Nvarchar(255) not null
)
create table [Status](
	Id int primary key identity,
	Name Nvarchar(255) not null
)
go
create table Task(
	Id int primary key identity,
	Title Nvarchar(255) not null,
	[Description] nvarchar(max),
	TypeId int not null,
	SuperTask int,
	constraint FK_TASK_TASK foreign key (SuperTask) references Task(Id),
	constraint FK_TASK_TYPE foreign key (TypeId) references [Type](Id)
)
go
create table Work(
	Id int primary key identity,
	TaskId int not null,
	[Description] nvarchar(max),
	StartTime datetime not null,
	Deadline datetime,
	AlertTime datetime,
	StatusId int not null,
	constraint FK_WORK_STATUS foreign key (StatusId) references [Status](Id),
	constraint FK_WORK_TASK foreign key (TaskId) references Task(Id)
)