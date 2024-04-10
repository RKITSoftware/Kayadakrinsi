create database nlogDemo;

CREATE TABLE log02 (
  Id int PRIMARY KEY AUTO_INCREMENT,
  Application text DEFAULT NULL,
  Logged datetime DEFAULT NULL,
  LogLevel varchar(20) DEFAULT NULL,
  Message text DEFAULT NULL,
  Logger text DEFAULT NULL,
  Callsite text DEFAULT NULL,
  Exception text DEFAULT NULL
);

create table log01(
	id int primary key auto_increment not null,
    logged datetime default '2024-02-04',
    log_level varchar(20) default null,
    logger varchar(512) default null,
    message varchar(10000) default null 
);