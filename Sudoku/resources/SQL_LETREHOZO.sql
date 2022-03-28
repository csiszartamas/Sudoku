-- create
create database szakdolgozat;
use szakdolgozat;

-- teszt
create database music;
use music;
drop database szakdolgozat;
drop table ranglista;
drop table jatekos;
-- teszt


CREATE TABLE ranglista (
  id INT IDENTITY(1,1) Primary KEY NOT NULL,
  jatekosid INT FOREIGN KEY REFERENCES jatekos(id) NOT NULL,
  nehezseg INT NOT NULL,
  ido INT NOT NULL
);


CREATE TABLE jatekos (
  id INT IDENTITY(1,1) PRIMARY KEY,
  felhasznalonev VARCHAR(255) NOT NULL,
  jelszo VARCHAR(255) NOT NULL,
  jatekosnev VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  nyelv INT NOT NULL
);

INSERT INTO ranglista(jatekosid,nehezseg,ido) VALUES
(1,0,100),
(2,0,200),
(2,1,300),
(1,1,400),
(1,2,500),
(2,2,600);