CREATE TABLE [Transactions] (
	-- TODO: 1. Transaction ID should be a Primary Key, fields with a b-tree index
	TransactionId int IDENTITY NOT NULL /*Complete this*/ ,
	VendingMachineId char(36),
	ItemName varchar(255),
	ItemId int,
	PurchasePrice smallmoney,
	TransactionStatus int,
	TransactionDate datetime, 
	-- TODO: 2. This table should have a columnar index
	INDEX Transactions_CCI /*Complete this*/ 
) WITH (
	-- TODO: 3. This should be an in-memory table
	/*Complete this*/  
);

-- TODO: 4. In-memory tables should auto-elevate their transaction level to Snapshot
ALTER DATABASE CURRENT SET /*Complete this*/=ON ; 
