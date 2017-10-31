use LyPlanDatabase;
go

insert into Type (Name) values ('Todo')
insert into Type (Name) values ('Daily')
go

insert into Status (Name) values ('NotDone')
insert into Status (Name) values ('Early')
insert into Status (Name) values ('Doing')
insert into Status (Name) values ('Later')
insert into Status (Name) values ('Done')
go

insert into Task (Title, [Description], TypeId, SuperTask) values ('Cleaning house', 'Cleaning the house becasue it is too dirty', 1, null)
insert into Task (Title, [Description], TypeId, SuperTask) values ('Buying mouse', 'Buying new computer mouse because the last one is broken', 1, null)
go

insert into Task (Title, [Description], TypeId, SuperTask) values ('Teach math for baby', 'Thắng need to learn math', 2, null)
insert into Task (Title, [Description], TypeId, SuperTask) values ('Number', 'Teach Thắng number from 1 to 10', 2, 3);
insert into Task (Title, [Description], TypeId, SuperTask) values ('Add operation', 'Teach Thắng add number', 2, 3);
insert into Task (Title, [Description], TypeId, SuperTask) values ('Sub operation', 'Teach Thắng sub number', 2, 3);
go

insert into Task (Title, [Description], TypeId, SuperTask) values ('Feed baby', 'Feed Thắng', 2, null)
insert into Task (Title, [Description], TypeId, SuperTask) values ('Breakfast', 'Feed Breakfast for Thắng', 2, 7)
insert into Task (Title, [Description], TypeId, SuperTask) values ('Lunch', 'Feed Lunch for Thắng', 2, 7)
insert into Task (Title, [Description], TypeId, SuperTask) values ('Dinner', 'Feed Dinner for Thắng', 2, 7)
go
