create database szakdolgozat;
use szakdolgozat;


CREATE TABLE jatekos (
  id INT PRIMARY KEY AUTO_INCREMENT,
  nyelvid INT NOT NULL,
  felhasznalonev VARCHAR(255) NOT NULL,
  jelszo VARCHAR(255) NOT NULL,
  jatekosnev VARCHAR(255) NOT NULL,
  email VARCHAR(255) NOT NULL
);

CREATE TABLE ranglista (
  id INT FOREIGN KEY REFERENCES jatekos(id),
  pontszam INT NOT NULL,
  ido INT NOT NULL,
  hibakszama INT NOT NULL
);

CREATE TABLE nyelv (
  id INT FOREIGN KEY REFERENCES jatekos(nyelvid) AUTO_INCREMENT NOT NULL,
  nyelv VARCHAR(255) NOT NULL
);
