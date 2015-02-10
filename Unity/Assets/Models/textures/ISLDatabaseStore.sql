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
INSERT INTO `Transform` VALUES ('1','-0.02605','0.0417','-0.00095','343.666','65.72845','54.21008','1.0','1.0','1.0');
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
INSERT INTO `Joint` VALUES ('1','Thumb','Right
','1');
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