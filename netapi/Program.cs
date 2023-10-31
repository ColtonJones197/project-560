using Microsoft.Data.SqlClient;
using netapi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//try
//{
//    const string connectionString = @"Server=DESKTOP-VT4KSCJ\SQLEXPRESS;Database=ChessLocal;Integrated Security=SSPI;Encrypt=False";

//    using (SqlConnection connection = new SqlConnection(connectionString))
//    {
//        connection.Open();
//        string sql = "SELECT Username, Avatar FROM Chesscom.Player";

//        using (SqlCommand command = new SqlCommand(sql, connection))
//        {
//            using (SqlDataReader reader = command.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    //Console.WriteLine($"{reader.GetString(0)} {reader.GetString(1)}");
//                }
//            }
//        }
//    }

//}
//catch(SqlException e)
//{
//    //Console.WriteLine(e.ToString());
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
