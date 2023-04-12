var userName = document.getElementById("user-name").value;


$(document).ready(function () {
    // Load chat messages on page load
    loadChatMessages();

    // Listen for form submit event
    $("#chat-form").submit(function (event) {
        event.preventDefault();

        var messageContent = $("#chat-message-input").val().trim();
        if (messageContent) {
            sendMessage(messageContent);
        }
    });
});

function loadChatMessages() {
    $.ajax({
        url: "/Home/GetMessages",
        type: "GET",
        dataType: "json",
        success: function (response) {
            $("#chat-messages").html("");
            var currentUser = $("#chat-messages").data("user-name");

            var messages = response.$values; // Ajoutez cette ligne pour accéder à $values

            messages.forEach(function (message) {
                var userName = message.userName === currentUser ? message.userName : message.userName;
                var messageHtml = `<li><strong>${userName}:</strong> ${message.content}</li>`;
                $("#chat-messages").append(messageHtml);
            });

            // Scroll to the bottom of the chat window
            $("#chat-container").scrollTop($("#chat-container")[0].scrollHeight);
        },
        error: function (error) {
            console.log("Error retrieving chat messages: ", error);
        }
    });
}



function sendMessage(content) {
    $.ajax({
        url: "/Home/SendMessage",
        type: "POST",
        data: {
            userName: userName, 
            content: content
        },
        success: function () {
            $("#chat-message-input").val("");
            loadChatMessages();
        },
        error: function (error) {
            console.log("Error sending chat message: ", error);
        }
    });
}