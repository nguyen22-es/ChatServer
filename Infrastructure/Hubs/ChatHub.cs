
using Application.Dot;
using AutoMapper;
using Chatserver.Application.Queries;
using Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;



namespace Infrastructure.ChatHub
{

    [Authorize]
    public class ChatHub : Hub
    {
        private readonly static Dictionary<int, string> _ConnectionsMap = new Dictionary<int, string>();
        private readonly static List<UserDot> _Connections = new List<UserDot>();
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;

        }
   
        public async Task creatMessages(MessagesDot messagesDot)
        {                       
            await Clients.All.SendAsync("newMessage", messagesDot);
        }


      
        public async Task Join(string Room, int UserID)
        {


            try
            {
                var user = _Connections.Where(u => u.Id == UserID).FirstOrDefault();

                await Groups.AddToGroupAsync(Context.ConnectionId, Room);


                await Clients.OthersInGroup(Room).SendAsync("AddUser", user);

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

                var user = await _mediator.Send(new GetUserByIdQuery(IdentityName()));   //.Where(u => u.ID == IdentityName()).FirstOrDefault();
                var Rooms = await _mediator.Send(new GetRoomsByIdQuery(IdentityName()));


                if (!_Connections.Any(u => u.Id == user.Data.Id))
                {
                    _Connections.Add(user.Data);
                    _ConnectionsMap.Add(user.Data.Id, Context.ConnectionId);
                }

                foreach (var item in Rooms.Data)
                {
                    Console.WriteLine("đã thêm vào phòng"+item.Id);
                  await  Groups.AddToGroupAsync(Context.ConnectionId, item.Id.ToString());
                }


               await Clients.Caller.SendAsync("getProfileInfo", user.Data.Name);
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
