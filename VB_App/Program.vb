Imports System.Net
Imports Microsoft.EntityFrameworkCore
Imports VBLib_Common
Imports VBLib_Common.Model


Module Program
    Sub Main(args As String())
'        SeedDb()
        Console.ForegroundColor = ConsoleColor.Green
        ExecuteQueries()
        Console.ForegroundColor = ConsoleColor.White

        Using context = New AnimalContext()
        End Using
    End Sub

    Private Sub ExecuteQueries()
        Using context = New AnimalContext()
            Dim animals = context.Animals.Include("Clubs").Include("Grades")
            Console.ForegroundColor = ConsoleColor.Magenta

            For Each animal In animals
                Console.WriteLine(animal)

                If animal.Clubs IsNot Nothing Then

                    For Each club In animal.Clubs
                        Console.Write(vbTab)
                        Console.WriteLine(club)
                    Next
                End If

                If animal.Grades IsNot Nothing Then

                    For Each grade In animal.Grades
                        Console.Write(vbTab)
                        Console.WriteLine(grade)
                    Next
                End If
            Next

            Console.ForegroundColor = ConsoleColor.White
        End Using
    End Sub

    Private Async Function ExecuteQueriesAsync() As Task
    End Function

    Private Sub SeedDb()
        Using context = New AnimalContext()
            context.Database.EnsureDeleted()
            context.Database.EnsureCreated()
            Dim beaver1 = New Beaver With {
                    .Name = "SomeBeavers1",
                    .Age = 27,
                    .Fluffiness = FluffinessEnum.VeryFluffy,
                    .Size = 15,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim beaver2 = New Beaver With {
                    .Name = "SomeBeavers2",
                    .Age = 26,
                    .Fluffiness = FluffinessEnum.Fluffy,
                    .Size = 14,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim beaver3 = New Beaver With {
                    .Name = "SomeBeavers3",
                    .Age = 25,
                    .Fluffiness = FluffinessEnum.NotFluffy,
                    .Size = 13,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim beaver4 = New Beaver With {
                    .Name = "SomeBeavers4",
                    .Age = 24,
                    .Fluffiness = FluffinessEnum.Fluffy,
                    .Size = 12,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim beaver5 = New Beaver With {
                    .Name = "SomeBeavers5",
                    .Age = 23,
                    .Fluffiness = FluffinessEnum.VeryFluffy,
                    .Size = 11,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim crow1 = New Crow With {
                    .Name = "Crowly",
                    .Age = 5,
                    .Color = "black",
                    .Size = 1,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim crow2 = New Crow With {
                    .Name = "Crowly1",
                    .Age = 5,
                    .Color = "black",
                    .Size = 1,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim crow3 = New Crow With {
                    .Name = "Crowly2",
                    .Age = 22,
                    .Color = "black",
                    .Size = 4,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim crow4 = New Crow With {
                    .Name = "Crowly3",
                    .Age = 50,
                    .Color = "white",
                    .Size = 10,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim crow5 = New Crow With {
                    .Name = "Crowly4",
                    .Age = 5,
                    .Color = "pink",
                    .Size = 1,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim deer1 = New Deer With {
                    .Name = "Dasher",
                    .Age = 1,
                    .Horns = True,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim deer2 = New Deer With {
                    .Name = "Dancer",
                    .Age = 2,
                    .Horns = True,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim deer3 = New Deer With {
                    .Name = "Prancer",
                    .Age = 1,
                    .Horns = False,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim deer4 = New Deer With {
                    .Name = "Vixen",
                    .Age = 1,
                    .Horns = True,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim deer5 = New Deer With {
                    .Name = "Comet",
                    .Age = 1,
                    .Horns = True,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim deer6 = New Deer With {
                    .Name = "Cupid",
                    .Age = 1,
                    .Horns = False,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim deer7 = New Deer With {
                    .Name = "Donder ",
                    .Age = 1,
                    .Horns = True,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            Dim deer8 = New Deer With {
                    .Name = "Blitzen",
                    .Age = 1,
                    .Horns = True,
                    .IpAddress = IPAddress.Parse("127.0.0.1")
                    }
            context.Beavers.Add(beaver1)
            context.Beavers.Add(beaver2)
            context.Beavers.Add(beaver3)
            context.Beavers.Add(beaver4)
            context.Beavers.Add(beaver5)
            context.Crows.Add(crow1)
            context.Crows.Add(crow2)
            context.Crows.Add(crow3)
            context.Crows.Add(crow4)
            context.Crows.Add(crow5)
            context.Deers.Add(deer1)
            context.Deers.Add(deer2)
            context.Deers.Add(deer3)
            context.Deers.Add(deer4)
            context.Deers.Add(deer5)
            context.Deers.Add(deer6)
            context.Deers.Add(deer7)
            context.Deers.Add(deer8)
            Dim club1 = New Club With {
                    .Title = "TreesWorshipers",
                    .Animals = New List(Of Animal) From {
                    beaver1,
                    beaver2,
                    beaver3,
                    beaver4,
                    beaver5,
                    crow4
                    },
                    .Locations = New List(Of Location) From {
                    New Location With {
                    .Address = "North America"
                    },
                    New Location With {
                    .Address = "Canada"
                    },
                    New Location() With {
                    .Address = "Russia"
                    }
                    }
                    }
            Dim club2 = New Club With {
                    .Title = "CornLovers",
                    .Animals = New List(Of Animal) From {
                    crow1,
                    crow2,
                    crow3,
                    crow4,
                    crow5
                    },
                    .Locations = New List(Of Location) From {
                    New Location With {
                    .Address = "Westeros"
                    }
                    }
                    }
            Dim club3 = New Club With {
                    .Title = "ChristmasTeam",
                    .Animals = New List(Of Animal) From {
                    beaver1,
                    beaver2,
                    beaver3,
                    beaver4,
                    beaver5,
                    crow1,
                    crow2,
                    crow3,
                    crow4,
                    crow5,
                    deer1,
                    deer2,
                    deer3,
                    deer4,
                    deer5,
                    deer6,
                    deer7,
                    deer8
                    },
                    .Locations = New List(Of Location) From {
                    New Location With {
                    .Address = "North Pole"
                    }
                    }
                    }
            context.Clubs.Add(club1)
            context.Clubs.Add(club2)
            context.Clubs.Add(club3)
            Dim grade1 = New Grade With {
                    .TheGrade = 5,
                    .Club = club1,
                    .Animal = beaver1
                    }
            Dim grade2 = New Grade With {
                    .TheGrade = 4,
                    .Club = club1,
                    .Animal = beaver2
                    }
            Dim grade3 = New Grade With {
                    .TheGrade = 3,
                    .Club = club1,
                    .Animal = beaver3
                    }
            Dim grade4 = New Grade With {
                    .TheGrade = 3,
                    .Club = club1,
                    .Animal = beaver4
                    }
            Dim grade5 = New Grade With {
                    .TheGrade = 2,
                    .Club = club1,
                    .Animal = beaver5
                    }
            Dim grade6 = New Grade With {
                    .TheGrade = 1,
                    .Club = club1,
                    .Animal = crow4
                    }
            Dim grade7 = New Grade With {
                    .TheGrade = 5,
                    .Club = club2,
                    .Animal = crow1
                    }
            Dim grade8 = New Grade With {
                    .TheGrade = 4.5,
                    .Club = club2,
                    .Animal = crow2
                    }
            Dim grade9 = New Grade With {
                    .TheGrade = 2.1,
                    .Club = club2,
                    .Animal = crow3
                    }
            Dim grade10 = New Grade With {
                    .TheGrade = 4.3,
                    .Club = club2,
                    .Animal = crow4
                    }
            Dim grade27 = New Grade With {
                    .TheGrade = 4.5,
                    .Club = club3,
                    .Animal = beaver1
                    }
            Dim grade26 = New Grade With {
                    .TheGrade = 4.5,
                    .Club = club3,
                    .Animal = beaver2
                    }
            Dim grade25 = New Grade With {
                    .TheGrade = 4.5,
                    .Club = club3,
                    .Animal = beaver3
                    }
            Dim grade24 = New Grade With {
                    .TheGrade = 4.5,
                    .Club = club3,
                    .Animal = beaver4
                    }
            Dim grade23 = New Grade With {
                    .TheGrade = 4.5,
                    .Club = club3,
                    .Animal = beaver5
                    }
            Dim grade22 = New Grade With {
                    .TheGrade = 4.5,
                    .Club = club3,
                    .Animal = crow1
                    }
            Dim grade21 = New Grade With {
                    .TheGrade = 3.5,
                    .Club = club3,
                    .Animal = crow2
                    }
            Dim grade20 = New Grade With {
                    .TheGrade = 2.5,
                    .Club = club3,
                    .Animal = crow3
                    }
            Dim grade19 = New Grade With {
                    .TheGrade = 1.5,
                    .Club = club3,
                    .Animal = crow4
                    }
            Dim grade28 = New Grade With {
                    .TheGrade = 4.9,
                    .Club = club3,
                    .Animal = crow5
                    }
            Dim grade11 = New Grade With {
                    .TheGrade = 4.8,
                    .Club = club3,
                    .Animal = deer1
                    }
            Dim grade12 = New Grade With {
                    .TheGrade = 4.7,
                    .Club = club3,
                    .Animal = deer2
                    }
            Dim grade13 = New Grade With {
                    .TheGrade = 4.6,
                    .Club = club3,
                    .Animal = deer3
                    }
            Dim grade14 = New Grade With {
                    .TheGrade = 4.5,
                    .Club = club3,
                    .Animal = deer4
                    }
            Dim grade15 = New Grade With {
                    .TheGrade = 4.4,
                    .Club = club3,
                    .Animal = deer5
                    }
            Dim grade16 = New Grade With {
                    .TheGrade = 4.3,
                    .Club = club3,
                    .Animal = deer6
                    }
            Dim grade17 = New Grade With {
                    .TheGrade = 4.2,
                    .Club = club3,
                    .Animal = deer7
                    }
            Dim grade18 = New Grade With {
                    .TheGrade = 4.1,
                    .Club = club3,
                    .Animal = deer8
                    }
            context.Grades.Add(grade1)
            context.Grades.Add(grade2)
            context.Grades.Add(grade3)
            context.Grades.Add(grade4)
            context.Grades.Add(grade5)
            context.Grades.Add(grade6)
            context.Grades.Add(grade7)
            context.Grades.Add(grade8)
            context.Grades.Add(grade9)
            context.Grades.Add(grade10)
            context.Grades.Add(grade11)
            context.Grades.Add(grade12)
            context.Grades.Add(grade13)
            context.Grades.Add(grade14)
            context.Grades.Add(grade15)
            context.Grades.Add(grade16)
            context.Grades.Add(grade17)
            context.Grades.Add(grade18)
            context.Grades.Add(grade19)
            context.Grades.Add(grade20)
            context.Grades.Add(grade21)
            context.Grades.Add(grade22)
            context.Grades.Add(grade23)
            context.Grades.Add(grade24)
            context.Grades.Add(grade25)
            context.Grades.Add(grade26)
            context.Grades.Add(grade27)
            context.Grades.Add(grade28)
            Dim job1 = New Job With {
                    .Title = "Builder",
                    .Salary = 1,
                    .Animals = New List(Of Animal) From {
                    beaver1,
                    beaver2,
                    beaver3,
                    beaver4,
                    beaver5
                    }
                    }
            Dim job2 = New Job With {
                    .Title = "Messenger",
                    .Salary = 10,
                    .Animals = New List(Of Animal) From {
                    crow1,
                    crow2,
                    crow3,
                    crow4
                    }
                    }
            Dim job3 = New Job With {
                    .Title = "Delivery",
                    .Salary = 100,
                    .Animals = New List(Of Animal) From {
                    deer1,
                    deer2,
                    deer3,
                    deer4,
                    deer5,
                    deer6,
                    deer7,
                    deer8
                    }
                    }
            context.Jobs.Add(job1)
            context.Jobs.Add(job2)
            context.Jobs.Add(job3)
            Dim food1 = New NormalFood With {
                    .Title = "Elm",
                    .Animal = beaver1,
                    .Taste = Taste.Normal
                    }
            Dim food2 = New VeganFood With {
                    .Title = "Daphne laureola",
                    .Animal = beaver2,
                    .Calories = 100
                    }
            Dim food3 = New VeganFood With {
                    .Title = "Carpinus betulus",
                    .Animal = beaver3,
                    .Calories = 1001
                    }
            Dim food4 = New VeganFood With {
                    .Title = "Hornbeam",
                    .Animal = beaver4,
                    .Calories = 101
                    }
            Dim food5 = New NormalFood With {
                    .Title = "Pizza",
                    .Animal = beaver5,
                    .Taste = Taste.Excellent
                    }
            Dim food6 = New NormalFood With {
                    .Title = "Steak",
                    .Animal = crow1,
                    .Taste = Taste.Excellent
                    }
            Dim food7 = New NormalFood With {
                    .Title = "Meat",
                    .Animal = crow2,
                    .Taste = Taste.Good
                    }
            Dim food8 = New NormalFood With {
                    .Title = "Pizza",
                    .Animal = crow3,
                    .Taste = Taste.VeryGood
                    }
            Dim food9 = New VeganFood With {
                    .Title = "Corn",
                    .Animal = crow4,
                    .Calories = 1
                    }
            Dim food10 = New NormalFood With {
                    .Title = "Pizza",
                    .Animal = crow5,
                    .Taste = Taste.Normal
                    }
            Dim food11 = New VeganFood With {
                    .Title = "Pizza",
                    .Animal = deer1,
                    .Calories = 10
                    }
            Dim food12 = New VeganFood With {
                    .Title = "Pizza",
                    .Animal = deer2,
                    .Calories = 10
                    }
            Dim food13 = New VeganFood With {
                    .Title = "Pizza",
                    .Animal = deer3,
                    .Calories = 10
                    }
            Dim food14 = New VeganFood With {
                    .Title = "Pizza",
                    .Animal = deer4,
                    .Calories = 10
                    }
            Dim food15 = New VeganFood With {
                    .Title = "Pizza",
                    .Animal = deer5,
                    .Calories = 10
                    }
            Dim food16 = New VeganFood With {
                    .Title = "Pizza",
                    .Animal = deer6,
                    .Calories = 10
                    }
            Dim food17 = New NormalFood With {
                    .Title = "Elves",
                    .Animal = deer7,
                    .Taste = Taste.Excellent
                    }
            Dim food18 = New VeganFood With {
                    .Title = "Pizza",
                    .Animal = deer8,
                    .Calories = 10
                    }
            context.Food.Add(food1)
            context.Food.Add(food2)
            context.Food.Add(food3)
            context.Food.Add(food4)
            context.Food.Add(food5)
            context.Food.Add(food6)
            context.Food.Add(food7)
            context.Food.Add(food8)
            context.Food.Add(food9)
            context.Food.Add(food10)
            context.Food.Add(food11)
            context.Food.Add(food12)
            context.Food.Add(food13)
            context.Food.Add(food14)
            context.Food.Add(food15)
            context.Food.Add(food16)
            context.Food.Add(food17)
            context.Food.Add(food18)
            Dim drawback1 = New Drawback With {
                    .Title = "Crowdy",
                    .Foods = New List(Of Food) From {
                    food1,
                    food2,
                    food3,
                    food4,
                    food6,
                    food7,
                    food8,
                    food9,
                    food10,
                    food11,
                    food12,
                    food13,
                    food14,
                    food15,
                    food16,
                    food17,
                    food18
                    },
                    .Clubs = New List(Of Club) From {
                    club1,
                    club2,
                    club3
                    },
                    .Consequence = New Consequence With {
                    .Name = "Nervousness"
                    }
                    }
            Dim drawback2 = New Drawback With {
                    .Title = "Windy",
                    .Foods = New List(Of Food) From {
                    food1,
                    food2,
                    food3,
                    food4,
                    food5,
                    food6,
                    food7,
                    food8,
                    food9,
                    food10,
                    food11,
                    food12,
                    food13,
                    food14,
                    food15,
                    food16,
                    food17,
                    food18
                    },
                    .Clubs = New List(Of Club) From {
                    club1,
                    club2,
                    club3
                    },
                    .Consequence = New Consequence With {
                    .Name = "Teleportation to Land of Oz"
                    }
                    }
            Dim drawback3 = New Drawback With {
                    .Title = "Soggy",
                    .Foods = New List(Of Food) From {
                    food1,
                    food2,
                    food3,
                    food4,
                    food5,
                    food6,
                    food7,
                    food8,
                    food9,
                    food10,
                    food11,
                    food12,
                    food13,
                    food14,
                    food15,
                    food16,
                    food17,
                    food18
                    },
                    .Clubs = New List(Of Club) From {
                    club1,
                    club2,
                    club3
                    },
                    .Consequence = New Consequence With {
                    .Name = "Wet clothes"
                    }
                    }
            Dim drawback4 = New Drawback With {
                    .Title = "Hardy",
                    .Foods = New List(Of Food) From {
                    food1,
                    food2,
                    food3,
                    food4,
                    food5,
                    food6,
                    food7,
                    food8,
                    food9,
                    food10,
                    food11,
                    food12,
                    food13,
                    food14,
                    food15,
                    food16,
                    food17,
                    food18
                    },
                    .Clubs = New List(Of Club) From {
                    club1,
                    club2,
                    club3
                    },
                    .Consequence = New Consequence With {
                    .Name = "Sadness"
                    }
                    }
            context.Drawbacks.Add(drawback1)
            context.Drawbacks.Add(drawback2)
            context.Drawbacks.Add(drawback3)
            context.Drawbacks.Add(drawback4)
            Dim jobDrawback1 = New JobDrawback With {
                    .Job = job1,
                    .Drawback = drawback1
                    }
            Dim jobDrawback2 = New JobDrawback With {
                    .Job = job1,
                    .Drawback = drawback2
                    }
            Dim jobDrawback3 = New JobDrawback With {
                    .Job = job1,
                    .Drawback = drawback3
                    }
            Dim jobDrawback4 = New JobDrawback With {
                    .Job = job1,
                    .Drawback = drawback4
                    }
            Dim jobDrawback5 = New JobDrawback With {
                    .Job = job2,
                    .Drawback = drawback1
                    }
            Dim jobDrawback6 = New JobDrawback With {
                    .Job = job2,
                    .Drawback = drawback2
                    }
            Dim jobDrawback7 = New JobDrawback With {
                    .Job = job3,
                    .Drawback = drawback1
                    }
            Dim jobDrawback8 = New JobDrawback With {
                    .Job = job3,
                    .Drawback = drawback2
                    }
            context.JobDrawbacks.Add(jobDrawback1)
            context.JobDrawbacks.Add(jobDrawback2)
            context.JobDrawbacks.Add(jobDrawback3)
            context.JobDrawbacks.Add(jobDrawback4)
            context.JobDrawbacks.Add(jobDrawback5)
            context.JobDrawbacks.Add(jobDrawback6)
            context.JobDrawbacks.Add(jobDrawback7)
            context.JobDrawbacks.Add(jobDrawback8)
            context.SaveChanges()

            Dim person1 = New Person With {
                    .Name = "BeaverPerson",
                    .AnimalsLoved = New List(Of Animal) From {
                    beaver1,
                    beaver2,
                    beaver3,
                    beaver4,
                    beaver5
                    },
                    .AnimalsHated = New List(Of Animal) From {
                    deer1,
                    deer2,
                    deer3,
                    deer4,
                    deer5,
                    deer6,
                    deer7,
                    deer8
                    }
                    }
            context.Persons.Add(person1)
            context.SaveChanges()
        End Using
    End Sub
End Module
