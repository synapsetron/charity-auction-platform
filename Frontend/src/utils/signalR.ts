import * as signalR from "@microsoft/signalr";

export const connectToAuctionHub = (auctionId: string) => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl(`${process.env.REACT_APP_BACKEND_URL}/auctionHub`, {
            withCredentials: false,
            transport: signalR.HttpTransportType.WebSockets,
        })
        .withAutomaticReconnect()
        .build();

    return connection;
};
