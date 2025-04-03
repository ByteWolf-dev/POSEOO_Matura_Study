-unicode
-nouseregional
#exporttablecsv CruiseShip.csv=select Id,Name,YearOfConstruction,Tonnage,Length,Cabins,Passengers,Crew,Remark,ShippingCompanyId from CruiseShip
go
#exporttablecsv ShipName.csv=select Id,Name,CruiseShipId from ShipName
go
#exporttablecsv ShippingCompany.csv=select Id,Name,City,PLZ,Street,StreetNo from ShippingCompany
go