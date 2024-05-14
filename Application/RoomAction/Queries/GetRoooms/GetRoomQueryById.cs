using Application.Common.Interfaces;
using Application.Dot;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatSrever.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Linq;


namespace Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;

public record GetRoomsByIdQuery(int UserId) : IRequestWrapper<List<RoomDot>>;

    public class GetRoomByIdQueryHandler : IRequestHandlerWrapper<GetRoomsByIdQuery, List<RoomDot>>
    {
        private readonly IApplicationDbContext _context;
        private static string connectionString = "Server=SON;Database=ChatServerApi;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
        private readonly IMapper _mapper;


    public GetRoomByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResult<List<RoomDot>>> Handle(GetRoomsByIdQuery request, CancellationToken cancellationToken)
        {

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var query = "SELECT * FROM Rooms INNER JOIN RoomUsers ON RoomUsers.RoomId = Rooms.Id WHERE RoomUsers.UserId = @UserId";
                var paramObject = new { UserId = request.UserId };

                var result = await connection.QueryAsync<Rooms>(query, paramObject);

                connection.Close();

             

                return ServiceResult.Success(_mapper.Map<List<RoomDot>>(result.OrderByDescending(r => r.LastModified).ToList()));
            }
        }
        catch (Exception ex)
        {
            // Xử lý các ngoại lệ nếu cần
            Console.WriteLine(ex.Message);
            return ServiceResult.Failed<List<RoomDot>>(ServiceError.DefaultError);
        }




    }






}


