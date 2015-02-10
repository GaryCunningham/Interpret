BEGIN TRANSACTION;
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
CREATE TABLE Text(
	textID INTEGER PRIMARY KEY AUTOINCREMENT, 
	text TEXT
);
CREATE TABLE Joint(
	jointID INTEGER PRIMARY KEY  AUTOINCREMENT , 
	jointName TEXT,
	jointOrientation TEXT,
	transformID,
	FOREIGN KEY(transformID) REFERENCES Transform(transformID) 
);
CREATE TABLE GestureHasText(
	gestureHasTextID INTEGER PRIMARY KEY AUTOINCREMENT,
	gestureID INTEGER , 
	textID INTEGER , 

	FOREIGN KEY(gestureID) REFERENCES Gesture(gestureID),
	FOREIGN KEY(textID) REFERENCES Text(textID)	
);
CREATE TABLE GestureHasJoint(
	gestureHasJointID INTEGER PRIMARY KEY AUTOINCREMENT,
	gestureID INTEGER , 
	jointID INTEGER , 

	FOREIGN KEY(gestureID) REFERENCES Gesture(gestureID),
	FOREIGN KEY(jointID) REFERENCES Joint(jointID)	
);
CREATE TABLE Gesture(
	gestureID INTEGER PRIMARY KEY AUTOINCREMENT , 
	gestureTimeSeq FLOAT, 
	gestureImage BLOB,
	gestureVideoLoc TEXT
);
CREATE UNIQUE INDEX transformIndex1 ON Transform(transformID COLLATE nocase);
CREATE UNIQUE INDEX textIndex1 ON Text(textID COLLATE nocase);
CREATE UNIQUE INDEX jointIndex1 ON Joint(jointID COLLATE nocase);
CREATE UNIQUE INDEX gestureIndex1 ON Gesture(gestureID COLLATE nocase);
COMMIT;
