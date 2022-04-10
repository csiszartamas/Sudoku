-- create
create database szakdolgozat;
use szakdolgozat;

CREATE TABLE jatekos (
  id INT IDENTITY(1,1) PRIMARY KEY,
  felhasznalonev VARCHAR(255) NOT NULL,
  jelszo VARCHAR(255) NOT NULL,
  jatekosnev VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL,
  nyelv INT NOT NULL
);

CREATE TABLE ranglista (
  id INT IDENTITY(1,1) Primary KEY NOT NULL,
  jatekosid INT FOREIGN KEY REFERENCES jatekos(id) NOT NULL,
  nehezseg INT NOT NULL,
  ido INT NOT NULL
);