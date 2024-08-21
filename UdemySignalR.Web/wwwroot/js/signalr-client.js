$(document).ready(function () {

    const broadcastMessageToAllClientHubMethodCall = "BroadCastMessageToAllCient";
    const broadcastMessageToCallerClientHubMethodCall = "BroadCastMessageToCallerCient";
    const broadcastMessageToOtherClients = "BroadcastMessageToOthersClient"


    const receiveMessageToAllClientClientMethodCall = "ReceiveMessageForAllClient";
    const receiveConnectedClientCountAllClient = "ReceiveConnectedClientCountAllClient";
    const receiveMessageForCallerClient = "ReceiveMessageForCallerClient";
    const receiveMessageForOthersClient = "ReceiveMessageForOthersClient"

    const connection = new signalR
        .HubConnectionBuilder()
        //.withUrl("/examplehub")
        .withUrl("/exampleTypeSafeHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();


    function start() {
        connection.start().then(() => console.log("hub ile bağlantı kuruldu"));
    }

    try {

        start();

    }
    catch (e) {
        setTimeout(() => start(), 5000);
    }




    connection.on(receiveMessageToAllClientClientMethodCall, (message) => {
        console.log("gelen mesaj",message);
    })

    connection.on(receiveMessageForCallerClient, (message) => {
        console.log("(caller) gelen mesaj", message);
    })


    const span_client_count = $("#span-connected-client-count");

    connection.on(receiveConnectedClientCountAllClient, (count) => {

        span_client_count.text(count);

        console.log("gelen mesaj",count);
    })

    connection.on(receiveMessageForOthersClient, (message) => {

        console.log("(others) gelen mesaj",message)

    })




    $('#btn-send-message-all-client').click(function () {

        const message = "hello world";

        connection
            .invoke(broadcastMessageToAllClientHubMethodCall, message)
            .catch(err=> console.log("hata",err))

    });


    $('#btn-send-message-caller-client').click(function () {

        const message = "hello world";

        connection
            .invoke(broadcastMessageToCallerClientHubMethodCall, message)
            .catch(err => console.log("hata", err))

    });

    $('#btn-send-message-others-client').click(function () {

        const message = "hello world";

        connection
            .invoke(broadcastMessageToOtherClients, message)
            .catch(err => console.log("hata", err))

    });


});