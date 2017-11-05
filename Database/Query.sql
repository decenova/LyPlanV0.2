use LyPlanDatabase

select * from Task
select * from [Status]
select * from [Type]
select * from Work

select top 1 Id from Task where TypeId = 1 order by Id desc

select t.Id as Id, t.Title as Title, w.Description, w.StatusId 
from Task t inner join Work w on t.Id = w.TaskId 
where TypeId = 1 and StatusId = 1

select w.Id, TaskId, t.Title, w.[Description], StartTime, AlertTime, Deadline, StatusId
                 from Work w inner join Task t on w.TaskId = t.Id
                 where t.TypeId = 2