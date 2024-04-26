using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatSrever.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Linq;


namespace Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;

public class GetRoomsByIdQuery : IRequestWrapper<List<Rooms>>
{
    public int UserId { get; set; }
}

    public class GetRoomByIdQueryHandler : IRequestHandlerWrapper<GetRoomsByIdQuery, List<Rooms>>
    {
        private readonly IApplicationDbContext _context;
    private static string connectionString = "Server=SON;Database=ChatServerApi;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";



    public GetRoomByIdQueryHandler(IApplicationDbContext context)
        {
        
            _context = context;
        }


        public async Task<ServiceResult<List<Rooms>>> Handle(GetRoomsByIdQuery request, CancellationToken cancellationToken)
        {
        //  var rooms = await _context.RoomUsers.Where(a => a.UserId == request.UserId).Include(r => r.Rooms).ToListAsync();
        //     if (rooms == null)
        // return ServiceResult.Failed<List<RoomUser>>(ServiceError.NotFound);


        //   return ServiceResult.Success(rooms);

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Rooms  INNER JOIN RoomUsers ON RoomUsers.RoomId = Rooms.Id WHERE RoomUsers.UserId = @UserId";
                var paramObject = new { UserId = request.UserId };

                var result = await connection.QueryAsync<Rooms>(query, paramObject);

                connection.Close();

                return ServiceResult.Success(result.OrderByDescending(r => r.LastModified).ToList());
            }
        }
        catch (Exception ex)
        {
            // Xử lý các ngoại lệ nếu cần
            Console.WriteLine(ex.Message);
            return ServiceResult.Failed<List<Rooms>>(ServiceError.DefaultError);
        }




    }






}


