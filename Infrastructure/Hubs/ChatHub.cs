
using Application.Authentication.Commands;
using Application.Dot;
using AutoMapper;
using Azure.Core;
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
        private readonly IMediator _mediator;
        public ChatHub(ApplicationDbContext applicationDbContext,IMediator mediator)
        {
            _applicationDbContext = applicationDbContext;
            _mediator = mediator;
        }
   
        public async Task sendMessages(CreateMessagesCommand request)
        {
            var MessagesDot =await _mediator.Send(request);
            await Clients.Group(request.RoomId.ToString()).SendAsync("receiveMessages", MessagesDot.Data);
        }


        public async Task CreateRooms(CreateRoomTrueCommand request) // tạo room chat với bạn bè
        {
            try
            {

                var authResult = await _mediator.Send(request);

                foreach(var i in request.ListId)

                if (_ConnectionsMap.Any(n => n.Key == i))
                {

                    await Groups.AddToGroupAsync(Context.ConnectionId, authResult.Data.id.ToString());


                    await Clients.OthersInGroup(authResult.Data.ToString()).SendAsync("AddUser", authResult.Data);
                }


            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }

        }
      
        public async Task Join(RoomDot roomDot)
        {


            try
            {
             
               

                if (_ConnectionsMap.Any(n => n.Key == IdentityName()))
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
