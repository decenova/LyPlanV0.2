use LyPlanDatabase

select * from Task
select * from [Status]
select * from [Type]
select * from Work

select top 1 Id from Task where TypeId = 1 order by Id desc
select top 1 Id from Work order by Id desc

select t.Id as Id, t.Title as Title, w.Description, w.StatusId 
from Task t inner join Work w on t.Id = w.TaskId 
where TypeId = 1 and StatusId = 1

select count(w.Id)
                 from Work w inner join Task t on w.TaskId = t.Id
                 where t.TypeId = 2 
				 and StartTime between '2017-11-06' and '2017-11-14' 
				 and StatusId in (1,2,3,4,5)
select count(w.Id)
                 from Work w inner join Task t on w.TaskId = t.Id
                 where t.TypeId = 2 
				 and StartTime between '2017-11-06' and '2017-11-14' 
				 and StatusId = 5
select t.Title, w.[Description]
                 from Work w inner join Task t on w.TaskId = t.Id
                 where t.TypeId = 2 
				 and AlertTime between '2017-11-07 02:27:00' and '2017-11-07 09:27:00' 
				 and StatusId in (1,2,3,4)

delete Work
delete Task