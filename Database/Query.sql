use LyPlanDatabase

select * from Task
select * from [Status]
select * from [Type]
select * from Work

select top 1 Id from Task where TypeId = 1 order by Id desc

select t.Id as Id, t.Title as Title, w.Description, w.StatusId 
from Task t inner join Work w on t.Id = w.TaskId 
where TypeId = 1 and StatusId = 1