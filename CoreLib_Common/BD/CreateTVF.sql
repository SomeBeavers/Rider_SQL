Create Function GetAnimalLocation(@animalId int)
Returns @locations Table
(
Name nvarchar(50) NOT NULL,
Address nvarchar(50) NOT NULL
)
As
Begin 
With animalLocation as(
select a.Id, a.Name, l.Address from Animals a
inner join AnimalClub ac on a.Id=ac.AnimalId
inner join Clubs c on ac.ClubId = c.Id
left join Location l on c.Id=l.ClubId
)
  INSERT INTO @locations
    SELECT Name, Address
    FROM animalLocation
    WHERE Id = @animalId
    RETURN
END