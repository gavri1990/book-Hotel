INSERT INTO CurrencySet VALUES
('AUD', 'Australian Dollar'),
('CNY', 'Chinese Yuan'),
('EUR', 'Euro'),
('GBP', 'British Pound'),
('JPY', 'Japanese Yen'),
('MXN', 'Mexican Peso'),
('RUB', 'Russian Ruble'),
('USD', 'US Dollar');

------------------------------------------------------------------------------------


SET IDENTITY_INSERT CustomerSet ON

INSERT INTO CustomerSet(Id, Name, Email, Phone, Password, StatusName, BookingsMadeTotal, Currency_ISOcode) VALUES
(1, 'Alex', 'alex@email.com', '6943991526', '123', 'Standard', 4, 'EUR'),
(2, 'George', 'george@email.com', '6945663552', '678', 'Premium', 9, 'USD'),
(3, 'John Doe', 'jd111@mail.com', '6976213226', '111', 'Standard', 1, 'EUR'),
(4, 'Gregory', 'greg@mail.com', '6943225526', '222', 'Standard', 0, 'EUR'),
(5, 'Mark', 'mark@mail.com', '69436635526', '333', 'Standard', 0, 'USD'),
(6, 'Julio', 'julio@mail.com', '6943213226', '444', 'Premium', 6, 'EUR'),
(7, 'Pavel', 'pav@mail.com', '6943663616', '555', 'Standard', 0, 'RUB'),
(8, 'Tom', 'tommy@mail.com', '6943977616', '666', 'Standard', 2, 'GBP');


SET IDENTITY_INSERT CustomerSet OFF

------------------------------------------------------------------------------------

SET IDENTITY_INSERT CreditCardSet ON

INSERT INTO CreditCardSet(Id, CreditCardNumber, CreditCardType, CardholderName, Customer_Id) VALUES
(1, '1253671892340852', 'Visa', 'Alex E.', 1),
(2, '4256718290365536', 'MasterCard', 'Giorgos G.', 2),
(3, '7586839625346999', 'Diners', 'John D.', 3),
(4, '7763552290871222', 'MasterCard', 'Greg Sager.', 4),
(5, '6665788290887654', 'Diners', 'Mark C.', 5),
(6, '1255899876563452', 'Visa', 'Julio S.', 6),
(7, '8887766623144030', 'American Express', 'Pavel Prohorov', 7),
(8, '9080233367908821', 'American Express', 'Tom W.', 8);


SET IDENTITY_INSERT CreditCardSet OFF
------------------------------------------------------------------------------------


SET IDENTITY_INSERT HotelOwnerSet ON

INSERT INTO HotelOwnerSet (Id, Email, Password, Currency_ISOcode) VALUES
(1, 'giannis@gmail.com', '87746', 'EUR'),
(2, 'marios@gmail.com', '87686', 'EUR'),
(3, 'monte@email.com', '333', 'EUR'),
(4, 'vlad@gmail.com', '444', 'RUB'),
(5, 'juan@email.com', '555', 'EUR'),
(6, 'jordi@gmail.com', '666', 'EUR'),
(7, 'penelope@email.com', '777', 'EUR'),
(8, 'yamamoto@gmail.com', '888', 'JPY'),
(9, 'andrea@email.com', '999', 'EUR'),
(10, 'fernando@gmail.com', '101010', 'RUB'),
(11, 'giorgos@email.com', '111111', 'EUR'),
(12, 'mike@gmail.com', '121212', 'GBP');


SET IDENTITY_INSERT HotelOwnerSet OFF


------------------------------------------------------------------------------------




SET IDENTITY_INSERT HotelSet ON

INSERT INTO HotelSet (Id, HotelName, StreetNameNumber, PostalCode, City, Country, HotelPhotoUrl, AverageRating, HotelOwner_Id) VALUES
(1, 'Sun Rise Apartments', 'I. Foka 36', '543', 'Rethimno', 'Greece', 'sunRiseApartments.jpg', NULL, 1),
(2, 'Parilio Hotel', 'Naoussas 49', '34453', 'Paros', 'Greece', 'parilioHotel.png', NULL, 2),
(5, 'Proa Playa Del Cura', 'Calle Maestro Parada 8', '12243', 'Torrevieja', 'Spain', 'proaPlayadelCura.jpg', NULL, 5);



SET IDENTITY_INSERT HotelSet OFF






------------------------------------------------------------------------------------



SET IDENTITY_INSERT RoomSet ON

INSERT INTO RoomSet (Id, RoomName, Rate, MaxPersons, RoomNumber, RoomPhotoUrl, Hotel_Id) VALUES
(1, 'Suite with see view', 78, 4, 1, 'suiteSeeView.jpg', 1),
(2, 'Room with garden view', 55, 4, 2, 'roomGardenView.jpg', 1),
(3, 'Superior Suite', 85, 2, 3, 'superiorSuite.jpg', 1),
(4, 'White Cube Room', 70, 2, 1, 'whiteCubeRoom.jpg', 2),
(5, 'Aurora Suite', 88, 3, 2, 'auroraSuite.jpg', 2);





SET IDENTITY_INSERT RoomSet OFF




--------------------------------------------------------------------------------------




SET IDENTITY_INSERT ServiceSet ON

INSERT INTO ServiceSet (Id, ServiceName, Room_Id) VALUES
(1, 'Cable TV', 1),
(2, 'Breakfast', 1),
(3, 'Air-Conditioning', 1),
(4, 'Parking', 2),
(5, 'Air-Conditioning', 2),
(6, 'Cable TV', 3),
(7, 'Breakfast', 3),
(8, 'Air-Conditioning', 3),
(9, 'Wifi', 3),
(10, 'Breakfast', 4),
(11, 'Air-Conditioning', 4),
(12, 'Parking', 5),
(13, 'Air-Conditioning', 5),
(14, 'Cable TV', 5);


SET IDENTITY_INSERT ServiceSet OFF
