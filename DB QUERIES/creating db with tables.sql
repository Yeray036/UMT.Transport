CREATE TABLE "Bedrijven" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Naam"	TEXT UNIQUE,
	PRIMARY KEY("Id" AUTOINCREMENT)
)

CREATE TABLE "Depots" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Depot"	TEXT UNIQUE,
	PRIMARY KEY("Id" AUTOINCREMENT)
)

CREATE TABLE "Functies" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Functie"	TEXT UNIQUE,
	PRIMARY KEY("Id" AUTOINCREMENT)
)

CREATE TABLE "Personeel" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Voornaam"	TEXT NOT NULL,
	"Achternaam"	TEXT NOT NULL,
	"PersNr"	INTEGER NOT NULL UNIQUE,
	"Bedrijf"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("Bedrijf") REFERENCES "Bedrijven"("Naam"),
	UNIQUE("Voornaam","Achternaam","PersNr","Bedrijf")
)

CREATE TABLE "PersoneelHasDepot" (
	"PersNr"	INTEGER NOT NULL,
	"Depot"	INTEGER NOT NULL,
	FOREIGN KEY("PersNr") REFERENCES "Personeel"("PersNr"),
	FOREIGN KEY("Depot") REFERENCES "Depots"("Id"),
	UNIQUE("PersNr","Depot")
)

CREATE TABLE "PersoneelHasFunctie" (
	"PersNr"	INTEGER NOT NULL,
	"Functie"	INTEGER NOT NULL,
	FOREIGN KEY("Functie") REFERENCES "Functies"("Id"),
	FOREIGN KEY("PersNr") REFERENCES "Personeel"("PersNr"),
	UNIQUE("Functie","PersNr")
)

CREATE TABLE "Verlof" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Verlof_start"	DATE NOT NULL,
	"Verlof_eind"	DATE NOT NULL,
	"PersNr"	INTEGER NOT NULL,
	FOREIGN KEY("PersNr") REFERENCES "Personeel"("PersNr"),
	PRIMARY KEY("Id" AUTOINCREMENT)
)

CREATE TABLE "Werkdagen" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"Datum"	DATE NOT NULL,
	"Begin_tijd"	TEXT NOT NULL,
	"Eind_tijd"	TEXT NOT NULL,
	"PersId"	INTEGER NOT NULL,
	"Depot"	INTEGER,
	"Functie"	INTEGER,
	PRIMARY KEY("Id" AUTOINCREMENT),
	FOREIGN KEY("PersId") REFERENCES "Personeel"("PersNr"),
	FOREIGN KEY("Functie") REFERENCES "Functies"("Id"),
	FOREIGN KEY("Depot") REFERENCES "Depots"("Id"),
	UNIQUE("PersId","Datum","Begin_tijd","Eind_tijd")
)

