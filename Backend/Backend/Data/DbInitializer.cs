using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(ApplicationContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Doctors.AnyAsync()) return;
        
        var doctors = new List<Doctor>
        {
            new() { FullName = "Иванов Иван Иванович", DateOfBirth = new DateTime(1980, 5, 20), Experience = 15, Specialty = "Терапевт" }, // doctors[0]
            new() { FullName = "Петрова Анна Сергеевна", DateOfBirth = new DateTime(1985, 8, 12), Experience = 10, Specialty = "Хирург" },   // doctors[1]
            new() { FullName = "Сидоров Петр Петрович", DateOfBirth = new DateTime(1975, 3, 15), Experience = 20, Specialty = "Невролог" }  // doctors[2]
        };
        await context.Doctors.AddRangeAsync(doctors);
        await context.SaveChangesAsync();

        var diseases = new List<Disease>
        {
            new() { Code = "J11", Name = "Грипп", Description = "Вирусная инфекция, вирус не идентифицирован" },
            new() { Code = "J03", Name = "Ангина", Description = "Острый тонзиллит" },
            new() { Code = "I10", Name = "Гипертония", Description = "Повышенное артериальное давление" },
            new() { Code = "E11", Name = "Сахарный диабет", Description = "Нарушение обмена веществ" } 
        };
        await context.Diseases.AddRangeAsync(diseases);
        await context.SaveChangesAsync();
        
        var patients = new List<Patient>
        {
            new() { 
                FullName = "Смирнов Алексей", 
                DateOfBirth = new DateTime(1970, 2, 3),
                Address = "ул. Ленина 1", 
                WorkPlace = "IT Corp", 
                DoctorId = doctors[0].Id, 
                Diseases = [diseases[0], diseases[2]] 
            },
            new() { 
                FullName = "Кузнецова Мария",
                DateOfBirth = new DateTime(1999, 3, 6), 
                Address = "ул. Гагарина 5", 
                WorkPlace = "Сбербанк", 
                DoctorId = doctors[1].Id, 
                Diseases = [diseases[1]]
            },
            new() { 
                FullName = "Попов Дмитрий", 
                DateOfBirth = new DateTime(2002, 7, 9), 
                Address = "ул. Мира 10", 
                WorkPlace = "Завод 'Металлист'", 
                DoctorId = doctors[2].Id, 
                Diseases = [diseases[3]]
            },
            new() { 
                FullName = "Соколова Екатерина", 
                DateOfBirth = new DateTime(1985, 11, 15), 
                Address = "пр. Победы 22", 
                WorkPlace = "Школа №5", 
                DoctorId = doctors[0].Id, 
                Diseases = [diseases[2]] 
            },
            new() { 
                FullName = "Волков Сергей", 
                DateOfBirth = new DateTime(1990, 1, 25), 
                Address = "ул. Кирова 45", 
                WorkPlace = "Фриланс", 
                DoctorId = doctors[0].Id, 
                Diseases = [diseases[0]] 
            },
            new() { 
                FullName = "Морозова Анна", 
                DateOfBirth = new DateTime(1965, 6, 30), 
                Address = "пер. Тихий 3", 
                WorkPlace = "Пенсионер", 
                DoctorId = doctors[2].Id, 
                Diseases = [diseases[2], diseases[3]]
            },
            new() { 
                FullName = "Лебедев Максим", 
                DateOfBirth = new DateTime(2000, 9, 12), 
                Address = "бул. Роз 8", 
                WorkPlace = "Студент", 
                DoctorId = doctors[1].Id, 
                Diseases = [diseases[1]]
            },
            new() { 
                FullName = "Новиков Игорь", 
                DateOfBirth = new DateTime(1978, 4, 18), 
                Address = "ул. Пушкина 12", 
                WorkPlace = "Автосервис", 
                DoctorId = doctors[2].Id, 
                Diseases = []
            },
            new() { 
                FullName = "Козлова Елена", 
                DateOfBirth = new DateTime(1995, 12, 5), 
                Address = "ул. Садовая 67", 
                WorkPlace = "Салон красоты", 
                DoctorId = doctors[0].Id, 
                Diseases = [diseases[0], diseases[1]]
            },
            new() { 
                FullName = "Григорьев Артем", 
                DateOfBirth = new DateTime(1988, 8, 22), 
                Address = "пр. Ленинградский 90", 
                WorkPlace = "Логистика", 
                DoctorId = doctors[1].Id, 
                Diseases = [diseases[2]]
            }
        };
        await context.Patients.AddRangeAsync(patients);
        await context.SaveChangesAsync();
    }
}