-- Insert rows into table 'Proizvodi' in schema '[dbo]'
INSERT INTO [dbo].[Proizvodi]
( -- Columns to insert data into
 Ime, Opis, Kategorija, Cena, SlikaPutanja
)
VALUES
( -- First row: values for the columns in the list above
 'Gamestar Ninja', 'Intel Corei5', 'Racunar', 150000, 'gamestarninja.jpg'
),
( -- Second row: values for the columns in the list above
 'HP Pavilion All-in-One računar', 'Intel Corei5', 'Racunar', 149999, 'hpallinone.jpg'
),
( 'APPLE računar iMac Pro', 'Intel Xeon Proccessor', 'Racunar', 7404999, 'appleimac.jpg'),
( 'REDRAGON tastatura', 'mehanička tastatura', 'Tastatura', 68999, 'redragontastatura.jpg'),
( 'LOGITECH tastatura', 'bežična tastatura', 'Tastatura', 34999, 'logitechtastatura.jpg'),
( 'LG GSX961NEAZ', 'Side by side frižider', 'Bela tehnika', 249999, 'lgfrizider.jpg'),
( 'BOSCH WAN24260BY', 'Mašina za pranje veša', 'Bela tehnika', 39999, 'boschmasina.jpg'),
( 'DELL XPS 15', 'Laptop Intel Core i7', 'Racunar', 379999, 'dellxps.jpg'),
( 'BOSCH BGS05A220', 'Usisivač', 'Mali kucni aparat', 11499, 'boschusisivac.jpg')
GO