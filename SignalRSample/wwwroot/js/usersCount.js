// Create connection
const userCountConnection =
    new signalR.HubConnectionBuilder().withUrl('/hubs/userCount').build();

// listen to hub methods invoke
userCountConnection.on('updateTotalViews', (totalViews) => {
    const totalViewsCounterSpan = document.getElementById('totalViewsCounter');
    totalViewsCounterSpan.innerText = totalViews.toString();
})

//  invoke hub methods
const newWindowLoadedOnClient = () => {
    userCountConnection.send('NewWindowLoaded');
}

// Start Connection
userCountConnection
    .start()
    .then(() => {
        console.log("---Connection succeeded---");
        newWindowLoadedOnClient();
    }).catch(() => {
    console.log("---Connection failed---")
})