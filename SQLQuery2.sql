CREATE TABLE [Product] (
    p_id         INT          IDENTITY (1, 1)NOT NULL,
    Title VARCHAR (50) NOT NULL,
    Author VARCHAR          NOT NULL,
    Publisher    VARCHAR          NOT NULL,
    isbn VARCHAR(20) NOT NULL,
    Price      MONEY          NOT NULL,
    p_Date	VARCHAR		 NOT NULL,
    PRIMARY KEY CLUSTERED ([p_id] ASC)
);