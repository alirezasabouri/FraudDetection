
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS
, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS
, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE
, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema FraudDetection
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema FraudDetection
-- -----------------------------------------------------
CREATE SCHEMA
IF NOT EXISTS `FraudDetection` DEFAULT CHARACTER
SET utf8 ;
USE `FraudDetection`
;

-- -----------------------------------------------------
-- Table `FraudDetection`.`Persons`
-- -----------------------------------------------------

CREATE TABLE
IF NOT EXISTS `FraudDetection`.`Persons`
(
  `Id` INT NOT NULL AUTO_INCREMENT,
  `IdentificationNumber` CHAR (30) NULL,
  `FirstName` VARCHAR (255) NOT NULL,
  `LastName` VARCHAR (255) NOT NULL,
  `DateOfBirth` DATETIME NULL,
  
  PRIMARY KEY
(`Id`),
  UNIQUE INDEX `Id_UNIQUE`
(`Id` ASC))
ENGINE = InnoDB;


SET SQL_MODE
=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS
=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS
=@OLD_UNIQUE_CHECKS;
