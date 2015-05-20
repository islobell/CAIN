DROP DATABASE IF EXISTS `MusicDB`;
CREATE DATABASE `MusicDB`;
USE `MusicDB`;

/* 
Tabla 'Albums', que guardará la información relativa a los álbumes
Tiene los siguientes campos:
 - ID: clave principal
 - MBID: identificador univoco del album en MusicBrainz
 - Title: título del albúm
 - Cover: imagen de la carátula
 */
CREATE TABLE IF NOT EXISTS `Albums` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `MBID` VARCHAR(36) DEFAULT NULL,  
  `Title` VARCHAR(100) NOT NULL, 
  `Year` INT UNSIGNED DEFAULT 0,
  `Cover` LONGBLOB DEFAULT NULL,
  PRIMARY KEY (`ID`),
  INDEX (`MBID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* 
Tabla 'Tracks', que guardará la información relativa a las pistas de audio
Tiene los siguientes campos:
 - ID: clave principal
 - MBID: identificador univoco de la pista de audio en MusicBrainz
 - Title: título de la pista
 - Duration: duración de la pista en ms
 - PathDst: ubicación en disco del archivo una vez catalogado
 - Status: estado de la canción: 1) sin resultados; 2) catalogada; 3) no catalogada
 */
CREATE TABLE IF NOT EXISTS `Tracks` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `MBID` VARCHAR(36) DEFAULT NULL,  
  `Title` VARCHAR(100) NOT NULL,
  `Duration` INT UNSIGNED DEFAULT 0,
  `Path` VARCHAR(260) DEFAULT NULL,
  `Status` INT UNSIGNED DEFAULT 0,
  `Reliability` INT UNSIGNED DEFAULT 0, 
  PRIMARY KEY (`ID`),
  INDEX (`MBID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* 
Tabla 'Artists', que guardará la información relativa a los artistas
Tiene los siguientes campos:
 - ID: clave principal
 - MBID: identificador univoco del artista en MusicBrainz
 - Name: nombre de la etiqueta
 */
CREATE TABLE IF NOT EXISTS `Artists` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `MBID` VARCHAR(36) DEFAULT NULL,  
  `Name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`ID`),
  INDEX (`MBID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* 
Tabla 'Tags', que guardará la información relativa a las etiquetas
Tiene los siguientes campos:
 - ID: clave principal
 - Name: nombre de la etiqueta
 - Content: contenido de la etiqueta
 - Description: Texto explicativo para hacerse una idea a qué se refiere
 */
CREATE TABLE IF NOT EXISTS `Tags` (
  `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(100) NOT NULL,
  `Value` VARCHAR(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* 
Tabla 'AlbumTracks', que guardará las relaciones entre álbumes y las pistas de audio
Tiene los siguientes campos:
 - TrackID: clave foránea referida a la tabla 'Tracks'
 - AlbumID: clave foránea referida a la tabla 'Albums'
 */
CREATE TABLE IF NOT EXISTS `TrackAlbums` (
  `TrackID` INT UNSIGNED NOT NULL,
  `AlbumID` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`TrackID`, `AlbumID`),
  FOREIGN KEY (`TrackID`) REFERENCES Tracks(`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (`AlbumID`) REFERENCES Albums(`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* 
Tabla 'TrackArtists', que guardará las relaciones entre las pistas de audio y los artistas
Tiene los siguientes campos:
 - TrackID: clave foránea referida a la tabla 'Tracks'
 - ArtistID: clave foránea referida a la tabla 'Artists'
 */
CREATE TABLE IF NOT EXISTS `TrackArtists` (
  `TrackID` INT UNSIGNED NOT NULL,
  `ArtistID` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`TrackID`, `ArtistID`),
  FOREIGN KEY (`TrackID`) REFERENCES Tracks(`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (`ArtistID`) REFERENCES Artists(`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/* 
Tabla 'TrackTags', que guardará las relaciones entre las pistas de audio y etiquetas
Tiene los siguientes campos:
 - TrackID: clave foránea referida a la tabla 'Tracks'
 - TagID: clave foránea referida a la tabla 'Tags'
 */
CREATE TABLE IF NOT EXISTS `TrackTags` (
  `TrackID` INT UNSIGNED NOT NULL,
  `TagID` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`TrackID`, `TagID`),
  FOREIGN KEY (`TrackID`) REFERENCES Tracks(`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (`TagID`) REFERENCES Tags(`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
