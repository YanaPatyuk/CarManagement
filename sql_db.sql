
 BEGIN;

CREATE USER IF NOT EXISTS 'carsDB'@'localhost' IDENTIFIED BY 'carsDB';
CREATE DATABASE IF NOT EXISTS carsDB;
GRANT ALL PRIVILEGES ON carsDB.* TO 'carsDB'@'localhost' WITH GRANT OPTION;
GRANT FILE ON *.* to 'carsDB'@'localhost';
USE carsDB;
SET GLOBAL local_infile = 1;


CREATE TABLE IF NOT EXISTS CAR ( -- replicate
    id                      integer AUTO_INCREMENT, -- PK
    license_plate                    integer,
    car_type                		 integer,
    fourdb							bool,
    engine_capacity			integer,
    manufacture_year		int,
    notes					 VARCHAR(255),
    employee					integer,
    car_care_date			DATETIME,
    edit_date				DATETIME
) CHARACTER SET utf8 COLLATE utf8_general_ci;

CREATE TABLE IF NOT EXISTS CAR_TYPE ( -- replicate (verbose)
    id              INTEGER NOT NULL, -- PK
    type_name      		VARCHAR(64) NOT NULL
) CHARACTER SET utf8 COLLATE utf8_general_ci;


CREATE TABLE IF NOT EXISTS EMPLOYEE ( -- replicate (verbose)
    id              INTEGER NOT NULL, -- references
    first_name       VARCHAR(64) NOT NULL, -- PK
    last_name			varcharacter(64)
) CHARACTER SET utf8 COLLATE utf8_general_ci;




COMMIT;

-- vi: set ts=4 sw=4 et :
