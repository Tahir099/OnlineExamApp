create table works_(id int not null identity(1,1) , work_name nvarchar(50))
create table users(id int not null identity(1,1),username_ nvarchar(50) , password_ nvarchar(50))
create table work_group(id int not null identity(1,1),user_id_ int , work_id int)
create table guestion(id int not null identity(1,1) , guestion nvarchar(50) , work_id int)

select * from works_
insert into works_ (work_name) values ('Hekim') , ('Muellim') , ('Programci')
insert into users (username_ , password_) values ('Tahir' ,'1234') , ('Kenan' ,'1234') ,('Fuad' ,'1234') ,('Orxan' ,'1234') ,('Tural' ,'1234') ,('Ferid' ,'1234') ,('Ayxan' ,'1234') ,('Vaqif' ,'1234') ,('Togrul' ,'1234') ,('Elsen' ,'1234')
insert into guestion(guestion , work_id) values ('guestion1' , 1) , ('guestion2' , 2) ,('guestion3' , 3) ,('guestion4' , 1) ,('guestion5' , 2) ,('guestion6' , 3) ,('guestion7' , 1) ,('guestion8' , 2) ,('guestion9' , 3) 
insert into work_group(user_id_ , work_id) values (1 , 1) , (1 , 3) , (2 , 2) , (1 , 3) , (3 , 1) , (4 , 2) , (5 , 3) , (6 , 3) , (7 , 1) , (7 , 2) , (8 , 1) , (9 , 2) , (10 , 3)  

select * from users 
select * from works_
select * from work_group
select * from guestion

ALTER proc check_user
@username nvarchar(50),
@password nvarchar(50)
as
begin transaction
begin try
select * from users where username_ = @username and @password = password_
commit transaction
end try
begin catch
if @@TRANCOUNT > 0
begin 
rollback transaction
end
end catch

select u.id, u.username_ , w.work_name , g.guestion from work_group wg
left join users u on wg.user_id_ = u.id
left join works_ w on wg.work_id = w.id 
left join guestion g on w.id = g.work_id

select w.work_name , g.guestion from guestion g 
left join works_ w on g.work_id = w.id order by w.work_name




create proc get_guestion
@username nvarchar(50)
as
begin transaction
begin try
select  g.guestion from work_group wg
left join users u on wg.user_id_ = u.id
left join works_ w on wg.work_id = w.id 
left join guestion g on w.id = g.work_id where u.username_ =  @username
commit transaction
end try
begin catch
if @@TRANCOUNT > 0
begin 
rollback transaction
end
end catch


create table answers (user_ nvarchar(50) , guestion nvarchar(50) , answer nvarchar(50))

create proc insert_answers
@user_ nvarchar(50),
@guestion nvarchar(50),
@answer nvarchar(50)
as
begin transaction
begin try
insert into answers(user_ , guestion ,answer) values(@user_,@guestion,@answer)
commit transaction
end try
begin catch
if @@TRANCOUNT > 0
begin 
rollback transaction
end
end catch


