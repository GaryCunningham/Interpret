PRAGMA SQLITE_DEFAULT_FOREIGN_KEYS=1;
PRAGMA foreign_keys = ON;

DROP Table if exists Gesture;
CREATE TABLE Gesture(
	gestureID INTEGER PRIMARY KEY AUTOINCREMENT , 
	gestureTimeSeq FLOAT, 
	gestureImage BLOB,
	gestureVideoLoc TEXT
);

CREATE UNIQUE INDEX gestureIndex1 ON Gesture(gestureID COLLATE nocase);


DROP Table if exists Text;
CREATE TABLE Text(
	textID INTEGER PRIMARY KEY AUTOINCREMENT, 
	text TEXT
);
CREATE UNIQUE INDEX textIndex1 ON Text(textID COLLATE nocase);

DROP Table if exists Transform;
CREATE TABLE Transform(
	transformID INTEGER PRIMARY KEY AUTOINCREMENT  , 
	posX FLOAT,
	posY FLOAT,
	posZ FLOAT,
	rotX FLOAT,
	rotY FLOAT,
	rotZ FLOAT,
	scaleX FLOAT,
	scaleY FLOAT,
	scaleZ FLOAT
);
CREATE UNIQUE INDEX transformIndex1 ON Transform(transformID COLLATE nocase);

DROP Table if exists Joint;
CREATE TABLE Joint(
	jointID INTEGER PRIMARY KEY  AUTOINCREMENT , 
	jointName TEXT,
	jointOrientation TEXT,
	transformID,
	FOREIGN KEY(transformID) REFERENCES Transform(transformID) 
);
CREATE UNIQUE INDEX jointIndex1 ON Joint(jointID COLLATE nocase);

DROP Table if exists GestureHasJoint;
CREATE TABLE GestureHasJoint(
	gestureHasJointID INTEGER PRIMARY KEY AUTOINCREMENT,
	gestureID INTEGER , 
	jointID INTEGER , 

	FOREIGN KEY(gestureID) REFERENCES Gesture(gestureID),
	FOREIGN KEY(jointID) REFERENCES Joint(jointID)	
);

DROP Table if exists GestureHasText;
CREATE TABLE GestureHasText(
	gestureHasTextID INTEGER PRIMARY KEY AUTOINCREMENT,
	gestureID INTEGER , 
	textID INTEGER , 

	FOREIGN KEY(gestureID) REFERENCES Gesture(gestureID),
	FOREIGN KEY(textID) REFERENCES Text(textID)	
);


