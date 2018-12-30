using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using WebSockets.Models;

namespace WebSockets.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly List<User> Users;

        static ChatHub()
        {
            Users = new List<User>();
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        /// Send message to the all users
        /// </summary>
        /// <param name="id">name of recipient</param>
        /// <param name="message">content of the mail</param>
        public void Send(string id, string message)
        {
            //var reciever = Users.First(x => x.ConnectionId == id);
            //var sender = Users.First(x => x.ConnectionId == Context.ConnectionId);

            //if (!(reciever is null))
            //{
            //    //// Send message to user, that they have been already connected
            //    //Clients.Caller.onConnected(id, userName, Users);
            //    Clients.Caller.addMessage(sender.Name, message);
            //    Clients.Client(reciever.ConnectionId).addMessage(sender.Name, message);
            //    //// Send message to evrebody, that new user has been connected
            //    //Clients.AllExcept(id).onNewUserConnected(id, userName);
            //}
            var user = Users.First(x => x.ConnectionId == id);

            // First version
            Clients.All.addMessage(user.Name, message);
        }

        /// <summary>
        /// Connect new user by name to the chat
        /// </summary>
        /// <param name="userName">Name of the user</param>
        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!Users.Any(x => x.ConnectionId == id))
            {
                Users.Add(new User { ConnectionId = id, Name = userName });

                // Send message to user, that they have been already connected
                Clients.Caller.onConnected(id, userName, Users);

                // Send message to evrebody, that new user has been connected
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        /// <summary>
        /// Invokes when user has been disconnected
        /// </summary>
        public System.Threading.Tasks.Task onDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }
            System.Diagnostics.Debug.WriteLine("Name: " + item.Name);
            System.Diagnostics.Debug.WriteLine("Id: " + item.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }
    }
}