insert TaskCategory values ('Навчально-методична робота');
insert TaskApprove values ('Протокол ЦК');
insert RatingTask values ('Підготовка підручників', 50, null, 1, 1);

select * from TaskApprove;

select * from [User];

update [User]
set [Role] = 1
where id = 1;