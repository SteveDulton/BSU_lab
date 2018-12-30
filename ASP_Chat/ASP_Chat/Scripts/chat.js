(function () {

    var userId;

    $('#listOfUsers').hide();
    $('#chatBody').hide();
    $('#loginBlock').show();

    var chat = $.connection.chatHub;

    chat.client.addMessage = function (name, message) {
        $('#chatroom').append('<p><b>' + htmlEncode(name)
            + '</b>: ' + htmlEncode(message) + '</p>');
    };

    chat.client.onConnected = function (id, userName, allUsers) {

        $('#loginBlock').remove();
        $('#chatBody').show();
        $('#listOfUsers').show();
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('<h3>Welcome, ' + userName + '</h3>');

        for (i = 0; i < allUsers.length; i++) {
            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }

    //Add new user
    chat.client.onNewUserConnected = function (id, name) {
        AddUser(id, name);
    }

    // Remove user
    chat.client.onUserDisconnected = function (id, userName) {
        console.log('It\'s my id:' + id);
        $('#' + id).remove();
    }

    // Open the connection
    $.connection.hub.start().done(function () {

        $('#sendmessage').click(function () {

            // Invokes method "send" inside the hub
            chat.server.send(userId, $('#message').val());
            $('#message').val('');
        });
        //Invokes on LogIn click
        $("#btnLogin").click(function () {

            var name = $("#txtUserName").val();
            if (name.length > 0) {
                chat.server.connect(name);
            }
            else {
                alert("Enter your nick!");
            }
        });
    });
    function AddUser(id, name) {

        userId = $('#hdId').val();

        if (userId != id) {
            $("#chatusers").append('<li id="' + id + '"><p class="circled-user-logo">' + name[0] + '</p>' +'<span class="user-name">' + name + '</span></li>');
            $('#' + id).addClass('login-name');
        }
    }

    // html-tag encoding
    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }
})();