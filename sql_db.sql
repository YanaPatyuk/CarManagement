
 BEGIN;

CREATE USER IF NOT EXISTS 'carsDB'@'localhost' IDENTIFIED BY 'carsDB';
CREATE DATABASE IF NOT EXISTS carsDB;
GRANT ALL PRIVILEGES ON carsDB.* TO 'carsDB'@'localhost' WITH GRANT OPTION;
GRANT FILE ON *.* to 'carsDB'@'localhost';
USE carsDB;
SET GLOBAL local_infile = 1;


CREATE TABLE IF NOT EXISTS CAR ( -- replicate
    id                      MEDIUMINT NOT NULL AUTO_INCREMENT,
    license_plate                    VARCHAR(255)  NOT NULL,
    car_type                		 integer  NOT NULL,
    fourdb							bool  NOT NULL,
    engine_capacity			integer  NOT NULL,
    manufacture_year		integer,
    notes					 VARCHAR(255),
    employee					integer,
    car_care_date			DATETIME,
    edit_date				DATETIME,
	PRIMARY KEY (id,license_plate )
) CHARACTER SET utf8 COLLATE utf8_general_ci;

CREATE TABLE IF NOT EXISTS CAR_TYPE ( -- replicate (verbose)
    id              INTEGER NOT NULL AUTO_INCREMENT,
    type_name      		VARCHAR(64) NOT NULL,
     PRIMARY KEY (id)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE IF NOT EXISTS EMPLOYEE ( -- replicate (verbose)
    id              INTEGER NOT NULL AUTO_INCREMENT, 
    first_name       VARCHAR(64) NOT NULL, -- PK
    last_name			varcharacter(64),
     PRIMARY KEY (id)
) CHARACTER SET utf8 COLLATE utf8_general_ci;


COMMIT;

-- vi: set ts=4 sw=4 et :
