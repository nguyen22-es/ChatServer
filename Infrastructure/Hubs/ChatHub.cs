
using Application.Dot;
using AutoMapper;
using Chatserver.Application.Queries;
using Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;
using ChatServer.Infrastructure.Data;
using ChatSrever.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;



namespace ServerSingalr.ChatHub
{

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly static Dictionary<int, string> _ConnectionsMap = new Dictionary<int, string>();
       // private readonly static List<UserDot> _Connections = new List<UserDot>();
        private readonly ApplicationDbContext _applicationDbContext;

        public ChatHub(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }
   
        public async Task creatMessages(MessagesDot messagesDot,int roomId)
        {                       
            await Clients.Group(roomId.ToString()).SendAsync("newMessage", messagesDot);
        }


      
        public async Task Join(RoomDot roomDot, int UserID)
        {


            try
            {
             
               

                if (_ConnectionsMap.Any(n => n.Key == UserID))
                {
               
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomDot.id.ToString());


                    await Clients.OthersInGroup(roomDot.id.ToString()).SendAsync("AddUser", roomDot);
                }


            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }
        public override async Task OnConnectedAsync()
        {

            try
            {

                // var user = await _mediator.Send(new GetUserByIdQuery(IdentityName()));  

                var user = await _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == IdentityName());

                //.Where(u => u.ID == IdentityName()).FirstOrDefault();
                //  var Rooms = await _mediator.Send(new GetRoomsByIdQuery(IdentityName()));

                var rooms = await _applicationDbContext.RoomUsers.Where(u => u.UserId == IdentityName()).ToListAsync();


                if (!_ConnectionsMap.Any(u => u.Key == user.Id))
                {
                
                    _ConnectionsMap.Add(user.Id, Context.ConnectionId);
                }

                foreach (var item in rooms)
                {
                 
                  await  Groups.AddToGroupAsync(Context.ConnectionId, item.RoomId.ToString());
                }

                Console.WriteLine("đã thêm vào phòng" + user.Name);
                await Clients.Caller.SendAsync("getProfileInfo", user.Name);
            }
            catch (Exception ex)
            {
               await Clients.Caller.SendAsync("onError", "OnConnected:" + ex.Message);
            }
            await base.OnConnectedAsync();
         
        }


        private int IdentityName()
        {
            var claim = Context.User.Claims.ToList();
            var ID = claim.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier);

            string strNumber = ID.Value;
            int UserId = int.Parse(strNumber);


            return UserId;
        }
    }
}
