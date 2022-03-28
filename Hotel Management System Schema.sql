create database HotelManagement;

drop database HotelManagement;
use HotelManagement;

create table Customer 
(
    CustId char(7) NOT NULL,
	CustName varchar(20) NOT NULL,
	CNIC char(15) NOT NULL,
	Pass_word varchar(9)NOT NULL,  
	Phone char(12) NOT NULL,
	Gender varchar(6),
	primary key(CustId)
);

create table Staff
(
    StaffId char(7) NOT NULL,
    StaffName varchar(20) NOT NULL,
	Phone char(12) NOT NULL,
	CNIC char(15) NOT NULL,
	Pass_word varchar(9)NOT NULL,
	Gender varchar(6),
	Position varchar(13),
	CONSTRAINT check_position CHECK (Position IN ('Manager', 'Room Booker', 'Cook')),
	Salary int,
	primary key (StaffId)

);

create table Rooms 
(
    RoomId int NOT NULL,
	RType varchar(30) NOT NULL,
	numbeds int NOT NULL,
    registered CHAR(1),
    CONSTRAINT ck_testbool_ischk CHECK (registered IN ('T', 'F')),
	CHECK(numbeds > 0 AND numbeds < 6),
    primary key(RoomId),
	--foreign key(RType) references RoomType
);

create table RoomType
(
	RType varchar(30) NOT NULL,
	Primary key(RType),
);


create table BookedRooms
(
    RoomId int NOT NULL,
    CustId char(7) NOT NULL,
    Checkin Date NOT NULL,
    Checkout Date NOT NULL,
    numGuests int NOT NULL,
    CHECK(numGuests > 0 AND numGuests < 6),
    primary key(RoomId),
    foreign key(CustId) references Customer,
    foreign key(RoomId) references Rooms,

);

create table RequestRecord
(
    StaffId char(7) NOT NULL,
	CustId char(7) NOT NULL,
	RType varchar(30) NOT NULL,
	RequestDate Date NOT NULL,
	RequestDateout Date,
	stat CHAR(1) NOT NULL,
	Numguests int,
    CONSTRAINT ck_testbool_ischk1 CHECK (stat IN ('A', 'R', 'P')),
    primary key(CustId,StaffId,RequestDate),
    foreign key(CustId) references Customer,
	foreign key(StaffId) references Staff(StaffId) on delete no action
);

create table Food
(
    FoodName varchar(30) NOT NULL,
	FType varchar(30) NOT NULL,
	Price int NOT NULL,
	primary key(FoodName),
	--foreign key(FType) references FoodType
);

create table FoodType
(
	FType varchar(30) NOT NULL,
	Primary key(FType),
);

create table FoodRecord
(
    StaffId char(7) NOT NULL,
	CustId char(7) NOT NULL,
	FType varchar(30) NOT NULL,
	FoodName varchar(30) NOT NULL,
	NumItems int NOT NULL,
	stat char(1) NOT NULL,
    CONSTRAINT ck_testbool_ischk2 CHECK (stat IN ('A', 'R', 'P')),
	CHECK(NumItems > 0 AND NumItems < 6),
    primary key(StaffId,CustId,FoodName),
    foreign key(CustId) references Customer,
    foreign key(StaffId) references Staff(StaffId) on delete no action,
	foreign key(FoodName) references Food,
	
);

alter table rooms add constraint fk1 foreign key (RType) references RoomType
alter table Food add constraint fk2 foreign key (FType) references FoodType


create table feedback(
	CustId char(7) NOT NULL,
	About char(4) NOT NULL,
	CONSTRAINT check_feedback_about CHECK (About IN ('Food', 'Room')),
	Comment char(500),
	Ratings char(5) NOT NULL,
	CONSTRAINT check_rating CHECK (Ratings IN ('*', '**','***','****','*****')),
	primary key(CustId,About),
    foreign key(CustId) references Customer,
);

go
create procedure enter_feedback 
@CustId char(7),
@About char(4),
@comment char(500),
@ratings char(5),
@out int output
as 
begin
declare @myvar bit
set @myvar=1
if exists(
select CustId
from Customer
where CustId=@CustId
)
begin
if exists(
select CustId,About
from feedback
where CustId=@CustId and About=@About
)
begin
update feedback
set Comment=@comment, Ratings=@ratings
where CustId=@CustId and About=@About 
end
else
begin
insert into feedback(CustId,About,Comment,Ratings)
values (@CustId,@About,@comment,@ratings)
end
end
else
begin 
set @myvar=0
end
set @out=@myvar
end




-- declare @MyVariable int
-- exec enter_feedback @CustId='1234568',
--@About='Food',
--@comment='Food quality was not good',
--@ratings='*',
--@out=@MyVariable output

 
-- select  @MyVariable


-- select *
-- from feedback



 --Remove staff
 go
 create procedure RemoveStaff
 @StaffId char(7),
 @position char(7),
 @out int output
 as
 begin
 if exists(
 select StaffId
 from Staff
 where StaffId=@StaffId
 )
 begin
 delete from Staff
 where StaffId=@StaffId and Position=@position
 set @out=1
 end
 else
 begin
 set @out=0
 end
 end



--Add staff
go
create procedure [dbo].proce1   --add staff
@Id char(7),
@Name varchar(20),
@CNIC char(15),
@phone char(12),
@password varchar(9),
@gender varchar(6),
@position char(12),
@tankha int,
@status int output
AS
Begin
if Exists (Select *
From Staff
Where Staff.StaffId = @Id )

begin
set @status=0
end
else

begin
set @status = 1
INSERT [Staff] ([StaffId], [StaffName], [CNIC], [Pass_word],[Phone], [Gender],[Position], [Salary]) VALUES (@Id, @Name, @CNIC, @password, @phone, @gender,@position, @tankha)
end
END




--declare @stat int
--Execute [dbo].proce1
--@Id = '7123121',
--@Name = 'Hakeem Naeem',
--@CNIC = '35202-1234567-1',
--@phone = '0300-7860112',
--@password = 'Wr1st',
--@gender = 'Male',
--@tankha = '24,000',
--@status = @stat Output

--Select @Stat

--Customer sign up
go
create procedure [dbo].signup
@Id char(7),
@Name varchar(20),
@CNIC char(15),
@phone char(12),
@password varchar(9),
@gender varchar(6),
@status int output
AS
Begin
if Exists (Select *
From Customer
Where Customer.CustId = @Id )

begin
set @status=0
end
else

begin
set @status = 1
INSERT [Customer] ([CustId], [CustName], [CNIC], [Pass_word],[Phone], [Gender]) VALUES (@Id, @Name, @CNIC, @password, @phone, @gender)
end
END


--Customer sign in
go
create procedure [dbo].signin
@Id char(7),
@password varchar(9),
@status int output
AS
Begin
if Exists (Select *
From Customer
Where Customer.CustId = @Id AND Customer.Pass_word = @password )

begin
set @status=1
end
else

begin
set @status = 0
end
END





go
create procedure UpdateStaff
@Id char(7),
@Name varchar(20),
@CNIC char(15),
@phone char(12),
@password varchar(9),
@gender varchar(6),
@position varchar(7),
@salary int,
@status int output
AS
Begin

if Exists (Select *
From Staff
Where Staff.StaffId = @Id )

begin
set @status=1
update staff
set StaffName=@Name,
	Phone=@phone,
	CNIC=@CNIC,
	Pass_word=@password,
	Gender=@gender,
	position=@position,
	Salary=@salary
	where StaffId=@id
	 
end
else

begin
set @status = 0
end
END


--declare @out int
--Execute UpdateStaff
--@Id = '5',
--@Name = 'Katrina',
--@CNIC = '1111111111111',
--@phone = '111111111111',
--@password = '111111',
--@gender = 'Female',
--@position='Cook',
--@salary=32000,
--@status = @out Output

--Select @out




go
create procedure [dbo].[BookRoom]
@roomid int,
@custid char(7),
@checkin date,
@checkout date,
@staffid char(7),
@choice char(1), 
@numguests int,
@status int output
AS
Begin
if Exists
    (
    Select *
    From rooms r
    Where r.roomId = @roomId AND r.registered='F'
	)

begin
set @status=1
if @choice = 'A'
begin
INSERT into BookedRooms VALUES (@roomid, @custid, @checkin, @checkout, @numguests)
update rooms
set registered='T'
where RoomId=@roomid
end

update RequestRecord
set stat=@choice
where StaffId = @staffid AND CustId= @custid AND RequestDate = @checkin
end

else
begin
set @status = 0
end
END

--declare @out int

--Execute BookRoom
--@roomid = 1,
--@custid = 1,
--@checkin = '2020-4-11',
--@checkout = '2020-4-15',
--@numguests=3,
--@status = @out Output

--Select @out
--select * from rooms
--select * from BookedRooms
--select * from RoomType

--insert into Customer(CustId,CustName,CNIC,Pass_word,Phone,Gender)
--values ('1234567','saif','12345-1234567-1','123Ums13','123456789012','M')

--insert into Customer(CustId,CustName,CNIC,Pass_word,Phone,Gender)
--values ('1234564','saify','12345-1234567-1','123Ums137','123456789012','M')

--select * from Customer

go
create procedure viewUsers
As
Begin
select * from Customer
end
go

create procedure checkstaff
@userid char(7),
@password varchar(9),
@position varchar(13),
@output int output
as 
begin
if exists(
select *
from Staff
where StaffId=@userid and Pass_word=@password and Position=@position
)
begin
set @output=1
end
else
begin
set @output=0
end
end



--Add staff
Create procedure [dbo].[addstaff]
@Id char(7),
@Name varchar(20),
@CNIC char(15),
@phone char(12),
@password varchar(9),
@gender varchar(6),
@position varchar(7),
@tankha int,
@status int output
AS
Begin
	if Exists (Select *
				From Staff
				Where Staff.StaffId = @Id )

				begin 
					set @status=0
				end
				else

				begin
				set @status = 1 
				INSERT [Staff] ([StaffId], [StaffName], [CNIC], [Pass_word],[Phone], [Gender], [Position],[Salary]) VALUES (@Id, @Name, @CNIC, @password, @phone, @gender, @position,@tankha)
				end
END

--View whole Staff
create procedure [dbo].[viewStaff]
As
Begin
select * from Staff
end

Insert [FoodType]([FType]) Values (1)
Insert [FoodType]([FType]) Values (2)
Insert [FoodType]([FType]) Values (3)
Insert [FoodType]([FType]) Values (4)
Insert [FoodType]([FType]) Values (5)
Insert [FoodType]([FType]) Values (6)
Insert [FoodType]([FType]) Values (7)
Insert [FoodType]([FType]) Values (8)
Insert [FoodType]([FType]) Values (9)


Insert [Food]([FType], [FoodName], [Price]) Values ('1', 'king deal', 50)
Insert [Food]([FType], [FoodName], [Price]) Values ('2', 'Tikka Kabab', 10)
Insert [Food]([FType], [FoodName], [Price]) Values ('3', 'Qorma with Rice', 23)
Insert [Food]([FType], [FoodName], [Price]) Values ('4', 'Samosa', 5)
Insert [Food]([FType], [FoodName], [Price]) Values ('5', 'Kheer', 15)
Insert [Food]([FType], [FoodName], [Price]) Values ('6', 'Halwa', 14)
Insert [Food]([FType], [FoodName], [Price]) Values ('7', 'Chapli Kabab', 12)
Insert [Food]([FType], [FoodName], [Price]) Values ('8', 'Karahi Gosht', 31)
Insert [Food]([FType], [FoodName], [Price]) Values ('9', 'Biryani', 27)



Insert [Customer]([CustId], [CNIC], [CustName], [Gender], [Pass_word], [Phone]) Values ('23','35101-4456782-9','Saif Saee','female', 'mnop!0','0312-9087651')
Insert [Customer]([CustId], [CNIC], [CustName], [Gender], [Pass_word], [Phone]) Values ('12','35202-1766712-1','Hamza Program','male', 'Wqer%1','0399-1982652')
Insert [Customer]([CustId], [CNIC], [CustName], [Gender], [Pass_word], [Phone]) Values ('54','35111-4512999-3','Fatima Sadiq','female', '@0lkjh','0300-6437322')

Insert [RoomType](RType) values ('Luxury Room')
Insert [RoomType](RType) values ('Deluxe Room')
Insert [RoomType](RType) values ('Double Room')
Insert [RoomType](RType) values ('Premium Room')
Insert [RoomType](RType) values ('Premium King Room')
Insert [RoomType](RType) values ('Room with View')

Insert [Rooms](RoomId, RType, numbeds, registered) Values (12,'Luxury Room',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (16,'Deluxe Room',2,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (11,'Luxury Room',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (25,'Room with View',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (13,'Luxury Room',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (14,'Premium Room',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (20,'Premium King Room',2,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (26,'Room with View',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (27,'Room with View',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (28,'Premium King Room',2,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (19,'Premium Room',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (18,'Premium Room',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (8,'Double Room',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (9,'Double Room',1,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (7,'Deluxe Room',2,'F')
Insert [Rooms](RoomId, RType, numbeds, registered) Values (5,'Deluxe Room',2,'F')


Insert RequestRecord (StaffId, CustId, RType, RequestDate, RequestDateOut, stat, Numguests) Values('2','12','Premium Room','5/30/2020', '6/4/2020', 'P', 3)

Insert RequestRecord (StaffId, CustId, RType, RequestDate, RequestDateOut, stat, Numguests) Values('2','23','Luxury Room','5/31/2020', '6/6/2020', 'P', 1)

Insert RequestRecord (StaffId, CustId, RType, RequestDate, RequestDateOut, stat, Numguests) Values('2','54','Deluxe Room','6/1/2020', '6/10/2020', 'P', 2)

--RoomRequests procedure
Create procedure [dbo].[viewRoomRequests]
@userid char(7)
AS
Begin
				Select *
				From RequestRecord
				Where RequestRecord.StaffId = @userid
END


--FoodRequests procedure
Create procedure [dbo].[viewFoodRequests]
@userid char(7)
AS
Begin
				Select *
				From FoodRecord
				Where FoodRecord.StaffId = @userid
END


create procedure savecookchoice
@custid char(7),
@cookid char(7),
@name varchar(30),
@choice char(1)
as
begin
Update FoodRecord
Set stat = @choice
Where FoodRecord.CustId = @custid AND FoodRecord.StaffId = @cookid And FoodRecord.FoodName = @name
end

create procedure [dbo].[free_room]
@RType varchar(30),
@Room_Id int output
as
begin
if exists((
select top 1 T.RoomId
From (
Select RoomId
from Rooms
where RType=@RType
except(
select RoomId
from BookedRooms)) As T))
begin
set @Room_Id=((
select top 1 T.RoomId
From (
Select RoomId
from Rooms
where RType=@RType
except(
select RoomId
from BookedRooms)) As T))
end
else
begin
set @Room_Id=0  --No room available
end
end

--declare @rid int
--execute free_room
--@RType=1,
--@Room_Id=@rid output
--select @rid


--select * from Staff

insert into Staff values('qwerty','John 123','0321-1234567','12345-1234567-1','Abc123','male','Manager',100000);
insert into Staff values('asdfgh','sam 123','0321-1234567','12345-1234567-1','Abc123','male','Room Booker',10000);
insert into Staff values('zxcvbn','Ali 123','0321-1234567','12345-1234567-1','Abc123','male','Cook',10000);
insert into Staff values('zxcvbq','Ahmed 123','0321-1234568','12345-1234567-1','Abc123','male','Cook',10000);
insert into Staff values('zxcvbw','Abdullah 123','0321-1234569','12345-1234567-1','Abc123','male','Cook',10000);

--select * from Customer

--select * from feedback

create procedure checkcustomer
@CustId char(7),
@RoomId int,
@out int output
as 
begin
if not exists(
select *
from BookedRooms
where RoomId=@RoomId and CustId=@CustId
)
begin
set @out=0
end
else
begin
set @out=1
end

end

create procedure request_for_food
@CustId char(7),
@FType varchar(30),
@quantity char(7),
@out int output
as
begin
declare @staffid char(7)
set @staffid=(
select top 1 StaffId
from Staff
where Position='Cook'
order by NEWID()   --Gives a random staff member
)
declare @foodname varchar(30)
set @foodname=(
select FoodName
from Food
where FType=@FType
)
insert into FoodRecord values(@staffid,@CustId,@FType,@foodname,@quantity,'P');
set @out=1

end

--select * from Staff


create procedure request_for_room
@CustId char(7),
@rtype varchar(30),
@requestdate date,
@requestdateout date,
@numguests int,
@out int output
as
begin
if(@requestdate<@requestdateout)
begin
if exists(
select *
from Rooms
where RType=@rtype and numbeds=@numguests
)
begin
declare @staffid char(7)
set @staffid=(
select top 1 StaffId
from Staff
where Position='Cook'
order by NEWID()   --Gives a random staff member
)
insert into RequestRecord values(@staffid,@CustId,@rtype,@requestdate,@requestdateout,'P',@numguests);
set @out=1
end
else
begin
set @out=0
end
end
else
begin
set @out=0
end
end


create procedure checkdate
@r1 date,
@r2 date,
@out int output
as
begin
if(@r1<@r2)
begin
set @out=1
end
else
begin
set @out=0
end
end
insert into Staff values('1','Aziz Ahmad','0316-4696298','35202-9965121-2','abcd3#','male','Manager',100000);
insert into Staff values('2','Hamza Program','0321-1234567','12345-1234567-1','Abc123','male','Room Booker',35000);
insert into Staff values('3','Ali Ahmad','0321-1234567','12345-1234567-1','Abc12#','female','Cook',36000)

select * from RequestRecord
select * from FoodRecord
select * from Customer
select * from Rooms
select * from BookedRooms
select * from feedback

insert into BookedRooms values(5,'12','2020-2-2','2020-2-2',2)
