using BackendApi.DbContexts;
using BackendApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlDatabase")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SchoolContext>();
        InitDb(context);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }
}

app.Run();

void InitDb(SchoolContext context)
{
    context.Database.EnsureCreated();
    if (context.Students.Any())
    {
        return;
    }

    var students = new StudentModel[]
            {
            new StudentModel{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new StudentModel{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new StudentModel{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new StudentModel{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new StudentModel{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new StudentModel{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new StudentModel{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new StudentModel{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
    foreach (StudentModel s in students)
    {
        context.Students.Add(s);
    }
    context.SaveChanges();

    var courses = new CourseModel[]
    {
            new CourseModel{CourseID=1050,Title="Chemistry",Credits=3},
            new CourseModel{CourseID=4022,Title="Microeconomics",Credits=3},
            new CourseModel{CourseID=4041,Title="Macroeconomics",Credits=3},
            new CourseModel{CourseID=1045,Title="Calculus",Credits=4},
            new CourseModel{CourseID=3141,Title="Trigonometry",Credits=4},
            new CourseModel{CourseID=2021,Title="Composition",Credits=3},
            new CourseModel{CourseID=2042,Title="Literature",Credits=4}
    };
    foreach (CourseModel c in courses)
    {
        context.Courses.Add(c);
    }
    context.SaveChanges();

    var enrollments = new EnrollmentModel[]
    {
            new EnrollmentModel{StudentID=1,CourseID=1050,Grade=Grade.A},
            new EnrollmentModel{StudentID=1,CourseID=4022,Grade=Grade.C},
            new EnrollmentModel{StudentID=1,CourseID=4041,Grade=Grade.B},
            new EnrollmentModel{StudentID=2,CourseID=1045,Grade=Grade.B},
            new EnrollmentModel{StudentID=2,CourseID=3141,Grade=Grade.F},
            new EnrollmentModel{StudentID=2,CourseID=2021,Grade=Grade.F},
            new EnrollmentModel{StudentID=3,CourseID=1050},
            new EnrollmentModel{StudentID=4,CourseID=1050},
            new EnrollmentModel{StudentID=4,CourseID=4022,Grade=Grade.F},
            new EnrollmentModel{StudentID=5,CourseID=4041,Grade=Grade.C},
            new EnrollmentModel{StudentID=6,CourseID=1045},
            new EnrollmentModel{StudentID=7,CourseID=3141,Grade=Grade.A},
    };
    foreach (EnrollmentModel e in enrollments)
    {
        context.Enrollments.Add(e);
    }
    context.SaveChanges();
}
