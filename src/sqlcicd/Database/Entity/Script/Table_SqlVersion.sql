CREATE TABLE SqlVersion
(
  Id              INT IDENTITY (1, 1) PRIMARY KEY,
  RepositoryType  INT         NOT NULL,
  Version         VARCHAR(50) NOT NULL,
  DeliveryTime    DATETIME    NOT NULL,
  TransactionCost FLOAT       NOT NULL,
  LastVersion     INT,
  IsLatest        BIT         NOT NULL,
  IsRollBacked    BIT         NOT NULL,
  IsDeleted       BIT         NOT NULL,

  FOREIGN KEY (Id) REFERENCES SqlVersion (Id)
);