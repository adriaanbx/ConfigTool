-- Drop table

-- DROP TABLE "DataBlock";

CREATE TABLE "DataBlock" (
	"Name" VARCHAR(120),
	"Number" VARCHAR(120),
	ID INTEGER NOT NULL,
	"Remarks" VARCHAR(250),
	"Enable" BOOLEAN,
	CONSTRAINT "INTEG_2058                     " PRIMARY KEY (ID),
	CONSTRAINT "UQ_DATABLOCKNAME               " UNIQUE ("Name"),
	CONSTRAINT "UQ_DATABLOCKNUMBER             " UNIQUE ("Number")
);
CREATE UNIQUE INDEX RDB$PRIMARY910 ON "DataBlock" (ID);
CREATE UNIQUE INDEX UQ_DATABLOCKNAME ON "DataBlock" ("Name");
CREATE UNIQUE INDEX UQ_DATABLOCKNUMBER ON "DataBlock" ("Number");

-- Drop table

-- DROP TABLE "Text";

CREATE TABLE "Text" (
	ID INTEGER NOT NULL,
	"Remarks" VARCHAR(250),
	CONSTRAINT "INTEG_2069                     " PRIMARY KEY (ID)
);
CREATE UNIQUE INDEX RDB$PRIMARY916 ON "Text" (ID);


-- Drop table

-- DROP TABLE "ValueType";

CREATE TABLE "ValueType" (
	ID SMALLINT NOT NULL,
	"Name" VARCHAR(16),
	"Remarks" VARCHAR(250),
	CONSTRAINT "INTEG_2120                     " PRIMARY KEY (ID),
	CONSTRAINT "UQ_VALUETYPENAME               " UNIQUE ("Name")
);
CREATE UNIQUE INDEX RDB$PRIMARY934 ON "ValueType" (ID);
CREATE UNIQUE INDEX UQ_VALUETYPENAME ON "ValueType" ("Name");

-- Drop table

-- DROP TABLE "PLCTag";

CREATE TABLE "PLCTag" (
	"ArraySize" INTEGER,
	"DefaultValue" VARCHAR(20),
	"Log" BOOLEAN,
	"Statistics" VARCHAR(250),
	"Screensaver" VARCHAR(250),
	"CycleTime" INTEGER,
	"Remarks" VARCHAR(250),
	"Name" VARCHAR(120),
	"Number" VARCHAR(120),
	ID INTEGER NOT NULL,
	"TextID" INTEGER,
	"DataBlockID" INTEGER,
	"ValueTypeID" SMALLINT,
	"UnitCategoryID" INTEGER,
	CONSTRAINT "INTEG_2430                     " PRIMARY KEY (ID),
	CONSTRAINT "UQ_PLCTAGDBIDNUMBER            " UNIQUE ("DataBlockID","Number"),
	CONSTRAINT "UQ_PLCTAGNAMEBDID              " UNIQUE ("Name","DataBlockID"),
	CONSTRAINT FK_PLCTAG_DATABLOCKID FOREIGN KEY ("DataBlockID") REFERENCES "DataBlock"(ID),
	CONSTRAINT FK_PLCTAG_TEXTID FOREIGN KEY ("TextID") REFERENCES "Text"(ID),
	--CONSTRAINT FK_PLCTAG_UNITCATEGORYID FOREIGN KEY ("UnitCategoryID") REFERENCES "UnitCategory"(ID),
	CONSTRAINT FK_PLCTAG_VALUETYPEID FOREIGN KEY ("ValueTypeID") REFERENCES "ValueType"(ID)
);
CREATE INDEX FK_PLCTAG_DATABLOCKID ON "PLCTag" ("DataBlockID");
CREATE INDEX FK_PLCTAG_TEXTID ON "PLCTag" ("TextID");
--CREATE INDEX FK_PLCTAG_UNITCATEGORYID ON "PLCTag" ("UnitCategoryID");
CREATE INDEX FK_PLCTAG_VALUETYPEID ON "PLCTag" ("ValueTypeID");
CREATE UNIQUE INDEX RDB$PRIMARY1229 ON "PLCTag" (ID);
CREATE UNIQUE INDEX UQ_PLCTAGDBIDNUMBER ON "PLCTag" ("DataBlockID","Number");
CREATE UNIQUE INDEX UQ_PLCTAGNAMEBDID ON "PLCTag" ("Name","DataBlockID");
