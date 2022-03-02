-- create
create database szakdolgozat;
use szakdolgozat;

-- teszt
use music;
drop database szakdolgozat;
drop table ranglista;
drop table nyelv;
drop table jatekos;
-- teszt


CREATE TABLE ranglista (
  id INT IDENTITY(1,1) Primary KEY NOT NULL,
  pontszam INT NOT NULL,
  ido INT NOT NULL,
  hibakszama INT NOT NULL,
);

CREATE TABLE nyelv (
  id INT IDENTITY(1,1) PRIMARY KEY not null,
  nyelv VARCHAR(255) NOT NULL
);

CREATE TABLE jatekos (
  id INT IDENTITY(1,1) PRIMARY KEY,
  felhasznalonev VARCHAR(255) NOT NULL,
  jelszo VARCHAR(255) NOT NULL,
  jatekosnev VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  rangid INT FOREIGN KEY REFERENCES ranglista(id), -- rangid INT FOREIGN KEY REFERENCES ranglista(id) not null
  nyelvid INT FOREIGN KEY REFERENCES nyelv(id)
);

insert into nyelv(nyelv) values
('Magyar'),
('Angol');